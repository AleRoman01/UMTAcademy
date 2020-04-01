using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class User
    {
        public Guid ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public DateTime Birthday { get; set; }

    }
}
