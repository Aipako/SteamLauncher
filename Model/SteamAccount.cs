using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SteamLauncher.Model
{
    public class SteamAccount : IEquatable<SteamAccount>
    {
        public string AccountName { get; set; }
        public string PersonalName { get; set; }
        public override bool Equals(object obj)
        {
            if (obj == null) 
                return false;
            SteamAccount objAsAccount = obj as SteamAccount;
            if (objAsAccount == null)
                return false;
            else
                return Equals(objAsAccount);
        }
        public override int GetHashCode()
        {
            return AccountName.GetHashCode();
        }
        public bool Equals(SteamAccount otherAccount)
        {
            if(AccountName == otherAccount.AccountName) 
                return true;
            else 
                return false;
        }
        public SteamAccount()
        {

        }
        public SteamAccount(string accountName, string personalName)
        {
            AccountName = accountName;
            PersonalName = personalName;
        }
        
    }
}
