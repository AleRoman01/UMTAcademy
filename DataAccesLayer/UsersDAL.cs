using Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DataAccesLayer
{
    public class UsersDAL
    {
        private const string _connectionString = "Server=romanale;Database=GaleryPhoto;Trusted_Connection=True;";
        private const string USERS_READ_ALL = "dbo.Users_ReadAll";
        private const string USER_READ_BY_GUID = "dbo.Users_ReadByID";
        private const string USER_DELETE_BY_GUID = "dbo.Users_DeleteByID";
        private const string USER_UPDATE_PHONE_NUMBER_BY_GUID = "dbo.Users_UpdatePhoneNumberByID";
        private const string USER_INSERT = "dbo.Users_Insert";

        public List<User> ReadAll()
        {
            List<User> users = new List<User>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command =  new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = USERS_READ_ALL;
                    using(SqlDataReader dataReader = command.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            User user = new User();
                            user = ConvertToModel(dataReader);
                            users.Add(user);
                        }
                    }
                }

            }

            return users;
        }

        public User ReadByUid(Guid userUid)
        {
            User user = new User();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = USER_READ_BY_GUID;
                    command.Parameters.Add(new SqlParameter("@UserID", userUid));
                    using (SqlDataReader dataReader = command.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            user = ConvertToModel(dataReader);
                        }
                    }
                }

            }

            return user;
        }

        public void DeleteByUid(Guid userUid)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Connection = connection;                    
                    command.Parameters.Add(new SqlParameter("@UserID", userUid));
                    command.CommandText = USER_DELETE_BY_GUID;
                    command.ExecuteNonQuery();
                }
            }
        }

        public void UpdatePhoneNumberByUid(Guid userUid, int phoneNumber)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Connection = connection;
                    command.Parameters.Add(new SqlParameter("@UserID", userUid));
                    command.Parameters.Add(new SqlParameter("@PhoneNumber", phoneNumber));
                    command.CommandText = USER_UPDATE_PHONE_NUMBER_BY_GUID;
                    command.ExecuteNonQuery();
                }
            }
        }
        public void InsertUser(Guid userUid, string firstName, string lastName,string address, int phoneNumber, DateTime birthday )
        {
            User user = new User();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Connection = connection;
                    command.Parameters.Add(new SqlParameter("@UserID", userUid));
                    command.Parameters.Add(new SqlParameter("@FirstName", firstName));
                    command.Parameters.Add(new SqlParameter("@LastName", lastName));
                    command.Parameters.Add(new SqlParameter("@Address", address));
                    command.Parameters.Add(new SqlParameter("@PhoneNumber", phoneNumber));
                    command.Parameters.Add(new SqlParameter("@Birthday", birthday));
                    command.CommandText = USER_INSERT;
                    command.ExecuteNonQuery();
                }
            }
        }

        private User ConvertToModel(SqlDataReader dataReader)
        {
            User user = new User();
            
            user.ID = dataReader.GetGuid(dataReader.GetOrdinal("UserID"));
            user.FirstName = dataReader.GetString(dataReader.GetOrdinal("FirstName"));
            user.LastName = dataReader.GetString(dataReader.GetOrdinal("LastName"));
            user.PhoneNumber = dataReader.GetInt32(dataReader.GetOrdinal("PhoneNumber"));
            user.Birthday = dataReader.GetDateTime(dataReader.GetOrdinal("Birthday"));

            return user;
        }
    }
}
