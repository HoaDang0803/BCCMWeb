using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace BCCM1.Pages.Users
{
 
      public class UsersInfo
      {
            public string  id{ get; set; }
            public string fName { get; set; }
            public string lName { get; set; }
            public string email { get; set; }
            public string pass { get; set; }
            public string gender { get; set; }
            public string birthDay {  get; set; }
            public string address { get; set; }
            public string phone { get; set; }
      }

    public class HomepageModel : PageModel
    {
        public List<UsersInfo> listUser = new List<UsersInfo>();
            public void OnGet()
            {
                try
                {
                    string connectionString = "Data Source = THANHHOA\\MSSQLSERVER01;Initial Catalog=HFINANCE;" + "Integrated Security=True;Pooling=False;TrustServerCertificate=True";
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        string sql = "Select * from Users";
                        using (SqlCommand command = new SqlCommand(sql, connection))
                        {
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    UsersInfo userInfo = new UsersInfo();
                                    userInfo.id = "" + reader.GetInt32(0);
                                    userInfo.fName = reader.GetString(1);
                                    userInfo.lName = reader.GetString(2);
                                    userInfo.email = reader.GetString(3);
                                    userInfo.pass = reader.GetString(4);
                                    userInfo.gender = reader.GetString(5);
                                    userInfo.birthDay = reader.GetDateTime(6).ToString();
                                    userInfo.address = reader.GetString(7);
                                    userInfo.phone = reader.GetString(8);
                                    listUser.Add(userInfo);
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    throw;
                }
            }
    }
}
