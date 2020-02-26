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
            using(var con = await DbHelper.GetDbConnection().ConfigureAwait(false)) {
                var tran = con.BeginTransaction();
                var command = new SqlCommand(CommandHelper.GetSqlForUserById(userId), con, tran);
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
                
            using(var con = await DbHelper.GetDbConnection().ConfigureAwait(false)) {
                var tran = con.BeginTransaction();
                var command = new SqlCommand(CommandHelper.GetSqlForAllUser(), con, tran);
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

        public async Task<UserDetails> CreateUser(UserDetails ud)
        {
            using(var con = await DbHelper.GetDbConnection().ConfigureAwait(false)) {
            
                var tran = con.BeginTransaction();
                var insertSql = $@"INSERT INTO USER_DETAILS
                (name,
                address,
                createdDate,
                createdBy
                ) 
                VALUES(
                {DbHelper.GetEnquotedString(ud.name)}, 
                {DbHelper.GetEnquotedString(ud.address)}, 
                {DbHelper.GetFormattedDate(ud.createdDate)}, 
                {DbHelper.GetEnquotedString(ud.createdBy)}
                ); SELECT SCOPE_IDENTITY(); ";

                var command2 = new SqlCommand(insertSql, con, tran);
                using (command2) {
                    Console.WriteLine("Insert SQL : "+ insertSql);
                    var id = await command2.ExecuteScalarAsync();
                    tran.Commit();
                    con.Close();
                    ud.id = id.ToInt();
                }
                return ud; 
            }
        }
    }
}
