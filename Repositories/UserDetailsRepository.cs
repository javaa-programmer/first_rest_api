using System.Collections.Generic;
using first_rest_api.Models;
using System.Threading.Tasks;
using System;
using Microsoft.Data.SqlClient;
using first_rest_api.Utilities;
using first_rest_api.ResponseObjects;
using first_rest_api.CustomExceptions;

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
                    var userDetail = new UserDetails();
                    userDetail.id = Convert.ToInt32(rdr["id"]);
                    userDetail.firstname = rdr["firstname"].ToString();
                    userDetail.lastname = rdr["lastname"].ToString();
                    userDetail.address = rdr["address"].ToString();
                    userDetail.city = rdr["city"].ToString();
                    userDetail.country = rdr["country"].ToString();
                    userDetail.pincode = Convert.ToInt32(rdr["pincode"]);
                    userDetail.createdby = rdr["createdby"].ToString();
                    userDetail.creationdate = (DateTime) rdr["creationdate"];
                    userDetailsList.Add(userDetail);
                }
                
                if (userDetailsList.Count > 0) {
                    return userDetailsList[0];
                } else {
                    Console.WriteLine("No record found for id : " + userId);
                    throw new UserDetailsException("User not found for id : "+ userId);
                } 
            }
               
        }

        public async Task<ResultModels<UserDetails>> GetAllUsers() {
                
            using(var con = await DbHelper.GetDbConnection().ConfigureAwait(false)) {
                var tran = con.BeginTransaction();
                var command = new SqlCommand(CommandHelper.GetSqlForAllUser(), con, tran);
                SqlDataReader rdr = command.ExecuteReader(); 
                List<UserDetails> userDetailsList = new List<UserDetails>();
                while (rdr.Read()) {
                    var userDetail = new UserDetails();
                    userDetail.id = Convert.ToInt32(rdr["id"]);
                    userDetail.firstname = rdr["firstname"].ToString();
                    userDetail.lastname = rdr["lastname"].ToString();
                    userDetail.address = rdr["address"].ToString();
                    userDetail.city = rdr["city"].ToString();
                    userDetail.country = rdr["country"].ToString();
                    userDetail.pincode = Convert.ToInt32(rdr["pincode"]);
                    userDetail.createdby = rdr["createdby"].ToString();
                    userDetail.creationdate = (DateTime) rdr["creationdate"];
                    userDetailsList.Add(userDetail);
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
                (firstname,
                lastname,
                address,
                city,
                country,
                pincode,
                creationdate,
                createdby
                ) 
                VALUES(
                {DbHelper.GetEnquotedString(ud.firstname)}, 
                {DbHelper.GetEnquotedString(ud.lastname)}, 
                {DbHelper.GetEnquotedString(ud.address)}, 
                {DbHelper.GetEnquotedString(ud.city)}, 
                {DbHelper.GetEnquotedString(ud.country)}, 
                {ud.pincode},
                {DbHelper.GetFormattedDate(ud.creationdate)}, 
                {DbHelper.GetEnquotedString(ud.createdby)}
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
