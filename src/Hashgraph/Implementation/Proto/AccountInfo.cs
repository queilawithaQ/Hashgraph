﻿namespace Proto
{
    public sealed partial class CryptoGetInfoResponse
    {
        public static partial class Types
        {
            public sealed partial class AccountInfo
            {
                internal Hashgraph.AccountInfo ToAccountInfo()
                {
                    return new Hashgraph.AccountInfo
                    {
                        Address = AccountID.AsAddress(),
                        SmartContractId = ContractAccountID,
                        Deleted = Deleted,
                        Proxy = ProxyAccountID.AsAddress(),
                        ProxiedToAccount = ProxyReceived,
                        Endorsement = Key.ToEndorsement(),
                        Balance = Balance,
                        Tokens = TokenRelationships.ToBalances(),
                        ReceiveSignatureRequired = ReceiverSigRequired,
                        AutoRenewPeriod = AutoRenewPeriod.ToTimeSpan(),
                        Expiration = ExpirationTime.ToDateTime(),
                        Memo = Memo
                    };
                }
            }
        }
    }
}
