using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IBLibrary.Messages
{
    public class AccountDownloadEndMessage
    {
        private string account;
        
        public AccountDownloadEndMessage(string account)
        {
            Account = account;
        }

        public string Account
        {
            get { return account; }
            set { account = value; }
        }
    }
}
