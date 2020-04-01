using System;
using System.Collections.Generic;
using DataAccesLayer;
using Model;

namespace MyProject
{
    class Program
    {
        static void Main(string[] args)
        {
            UsersDAL usersDAL = new UsersDAL();
            usersDAL.ReadAll();

            Guid userUid = new Guid("590932d2-0f9a-9709-332a-15d71940a32b");
            usersDAL.ReadByUid(userUid);

            Guid userUid1 = new Guid("590932d2-0f9a-9709-332a-15d71940a32b");
            usersDAL.DeleteByUid(userUid1);

            Guid userUid2 = new Guid("590932d2-0f9a-9709-332a-15d71940a32b");
            int userPhoneNumber = 0749110163;
            usersDAL.UpdatePhoneNumberByUid(userUid2, userPhoneNumber);

            Guid userUid3 = new Guid("590932d2-0f9a-9709-332a-15d71940a32b");
            string firstName = "Alexandra";
            string lastName = "Roamn";
            int phoneNumber = 0749110163;
            string emailAddress = "alexandra.roman400@gmail.com";
            DateTime birthday = new DateTime(1998, 04, 30);            
            usersDAL.InsertUser(userUid3, firstName, lastName, emailAddress, phoneNumber, birthday);
        }
    }
}
