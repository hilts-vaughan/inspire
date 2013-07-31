using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inspire.Shared.Models
{
    public class Account
    {

        public Account(int accountId, string username, string password, DateTime creationDate)
        {
            AccountId = accountId;
            Username = username;
            Password = password;
            CreationDate = creationDate;
        }


        public int AccountId { get; set; }

        public string Username { get; set; }

        /// <summary>
        /// The hashed password associated with this account (SHA-512)  
        /// </summary>
        public string Password { get; set; }


        /// <summary>
        /// The date this particular account was created, in UTC time.
        /// </summary>
        public DateTime CreationDate { get; set; }


    }
}
