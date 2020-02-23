using System.Collections.Generic;
using first_rest_api.Models;
using System.Threading.Tasks;
using System;
using Microsoft.Data.SqlClient;
using first_rest_api.Utilities;
using first_rest_api.ResponseObjects;

namespace first_rest_api.Repositories {

    public class UserDetailsRepository : IUserDetailsRepository {

        public async Task<UserDetails> GetUserDetailsById(int userId)
        {
            string commandText = @"SELECT * FROM [dbo].[USER_DETAILS]  
                                        where [id] = " + userId ;

            using(var con = await DbHelper.GetDbConnection().ConfigureAwait(false)) {
                var tran = con.BeginTransaction();
                var command = new SqlCommand(commandText, con, tran);
                SqlDataReader rdr = command.ExecuteReader(); 
                
                List<UserDetails> userDetailsList = new List<UserDetails>();
                while (rdr.Read()) {
                    var _userDetail = new UserDetails();
                    _userDetail.id = Convert.ToInt32(rdr["id"]);
                    _userDetail.name = rdr["name"].ToString();
                    _userDetail.address = rdr["address"].ToString();
                    _userDetail.createdBy = rdr["createdBy"].ToString();
                    _userDetail.createdDate = (DateTime) rdr["createdDate"];
                    userDetailsList.Add(_userDetail);
                }
                
                return userDetailsList[0]; 
            }
               
        }

        public async Task<ResultModels<UserDetails>> GetAllUsers() {
                
            string commandText = @"SELECT * FROM [dbo].[USER_DETAILS]  
                                    ORDER BY createdDate DESC";

            using(var con = await DbHelper.GetDbConnection().ConfigureAwait(false)) {
                var tran = con.BeginTransaction();
                var command = new SqlCommand(commandText, con, tran);
                SqlDataReader rdr = command.ExecuteReader(); 
                List<UserDetails> userDetailsList = new List<UserDetails>();
                while (rdr.Read()) {
                    var _userDetail = new UserDetails();
                    _userDetail.id = Convert.ToInt32(rdr["id"]);
                    _userDetail.name = rdr["name"].ToString();
                    _userDetail.address = rdr["address"].ToString();
                    _userDetail.createdBy = rdr["createdBy"].ToString();
                    _userDetail.createdDate = (DateTime) rdr["createdDate"];
                    userDetailsList.Add(_userDetail);
                }
                con.Close();
                return new ResultModels<UserDetails>(userDetailsList);
            }
            
        }

        public int CreateUser(UserDetails ud)
        {
            var maxIdSql = "select max(id) as maxid from USER_DETAILS";
            var maxid = 0;
            
            string connetionString = @"Data Source=localhost;Initial Catalog=first_rest_api;User ID=sa;Password=Niamul@2012";

            SqlConnection connection = new SqlConnection(connetionString);
            
            
            connection.Open();
            var tran = connection.BeginTransaction();

                var command1 = new SqlCommand(maxIdSql, connection, tran);
                
                SqlDataReader rdr1 = command1.ExecuteReader(); 
                List<int> maxidList = new List<int>();
                while (rdr1.Read()) {
                    
                    var id = Convert.ToInt32(rdr1["maxid"]);
                    maxidList.Add(id);
                }
                
                maxid = maxidList[0] + 1;
                rdr1.Close();

            var insertSql = $@"INSERT INTO USER_DETAILS
            (id,
            name,
            address,
            createdDate,
            createdBy
            ) 
            VALUES(
            {maxid}, 
            {DbHelper.GetEnquotedString(ud.name)}, 
            {DbHelper.GetEnquotedString(ud.address)}, 
            {DbHelper.GetFormattedDate(ud.createdDate)}, 
            {DbHelper.GetEnquotedString(ud.createdBy)}
            )";


            var command2 = new SqlCommand(insertSql, connection, tran);
            using (command2) {
                Console.WriteLine("Insert SQL : "+ insertSql);
                command2.ExecuteNonQuery();
                tran.Commit();
                connection.Close();
            }
            return maxid; 
        }
    }
}
