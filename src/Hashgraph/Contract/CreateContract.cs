﻿using Proto;
using System;
using System.Threading.Tasks;

namespace Hashgraph
{
    public partial class Client
    {
        /// <summary>
        /// Creates a new contract instance with the given create parameters.
        /// </summary>
        /// <param name="createParameters">
        /// Details regarding the contract to instantiate.
        /// </param>
        /// <param name="configure">
        /// Optional callback method providing an opportunity to modify 
        /// the execution configuration for just this method call. 
        /// It is executed prior to submitting the request to the network.
        /// </param>
        /// <returns>
        /// A transaction receipt with a description of the newly created contract.
        /// and receipt information.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">If required arguments are missing.</exception>
        /// <exception cref="InvalidOperationException">If required context configuration is missing.</exception>
        /// <exception cref="PrecheckException">If the gateway node create rejected the request upon submission.</exception>
        /// <exception cref="ConsensusException">If the network was unable to come to consensus before the duration of the transaction expired.</exception>
        /// <exception cref="TransactionException">If the network rejected the create request as invalid or had missing data.</exception>
        public async Task<CreateContractReceipt> CreateContractAsync(CreateContractParams createParameters, Action<IContext>? configure = null)
        {
            return new CreateContractReceipt(await ExecuteTransactionAsync(new ContractCreateTransactionBody(createParameters), configure, false, createParameters.Signatory).ConfigureAwait(false));
        }
        /// <summary>
        /// Creates a new contract instance with the given create parameters 
        /// returning a detailed record.
        /// </summary>
        /// <param name="createParameters">
        /// Details regarding the contract to instantiate.
        /// </param>
        /// <param name="configure">
        /// Optional callback method providing an opportunity to modify 
        /// the execution configuration for just this method call. 
        /// It is executed prior to submitting the request to the network.
        /// </param>
        /// <returns>
        /// A transaction record with a description of the newly created contract.
        /// and receipt information.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">If required arguments are missing.</exception>
        /// <exception cref="InvalidOperationException">If required context configuration is missing.</exception>
        /// <exception cref="PrecheckException">If the gateway node create rejected the request upon submission.</exception>
        /// <exception cref="ConsensusException">If the network was unable to come to consensus before the duration of the transaction expired.</exception>
        /// <exception cref="TransactionException">If the network rejected the create request as invalid or had missing data.</exception>
        public async Task<CreateContractRecord> CreateContractWithRecordAsync(CreateContractParams createParameters, Action<IContext>? configure = null)
        {
            return new CreateContractRecord(await ExecuteTransactionAsync(new ContractCreateTransactionBody(createParameters), configure, true, createParameters.Signatory).ConfigureAwait(false));
        }
    }
}
