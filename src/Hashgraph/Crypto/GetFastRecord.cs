﻿using Grpc.Core;
using Hashgraph.Implementation;
using Proto;
using System;
using System.Threading.Tasks;

namespace Hashgraph
{
    public partial class Client
    {
        // Preliminary Implementation not to rely upon yet
        private async Task<Proto.TransactionRecord> GetFastRecordAsync(TransactionID transactionId, ContextStack context)
        {
            var query = new Query
            {
                TransactionGetFastRecord = new TransactionGetFastRecordQuery
                {
                    TransactionID = transactionId
                }
            };
            var response = await Transactions.ExecuteRequestWithRetryAsync(context, query, instantiateGetTransactionReceiptsAsyncMethod, checkForRetry);
            if (response.TransactionRecord is null)
            {
                var expiration = Protobuf.FromTimestamp(transactionId.TransactionValidStart).Add(context.TransactionDuration);
                if(expiration < DateTime.UtcNow)
                {
                    throw new ConsensusException("Network failed to reach concensus before transaction request exired.", Protobuf.FromTransactionId(transactionId), (ResponseCode)response.Header.NodeTransactionPrecheckCode);
                }
                throw new PrecheckException("Failed to receive response from server within the given retry interval.", Protobuf.FromTransactionId(transactionId), (ResponseCode)response.Header.NodeTransactionPrecheckCode);
            }
            return response.TransactionRecord;

            static Func<Query, Task<TransactionGetFastRecordResponse>> instantiateGetTransactionReceiptsAsyncMethod(Channel channel)
            {
                var client = new CryptoService.CryptoServiceClient(channel);
                return async (Query query) => (await client.getFastTransactionRecordAsync(query)).TransactionGetFastRecord;
            }

            static bool checkForRetry(TransactionGetFastRecordResponse response)
            {
                return
                    response.Header.NodeTransactionPrecheckCode == ResponseCodeEnum.Busy ||
                    response.TransactionRecord?.Receipt?.Status == ResponseCodeEnum.Unknown;
            }
        }
    }
}
