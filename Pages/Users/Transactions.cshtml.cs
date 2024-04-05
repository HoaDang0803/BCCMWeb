using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace BCCM1.Pages.Users
{
    public class TransactionsModel : PageModel
    {
        public UsersInfo userInfo = new UsersInfo();
        public Accounts account = new Accounts();

        public List<Transactions> listTrans = new List<Transactions>();
        public void OnGet()
        {
            String id = Request.Query["id"];
            try
            {
                string connectionString = "Data Source = THANHHOA\\MSSQLSERVER01;Initial Catalog=HFINANCE01;" + "Integrated Security=True;Pooling=False;TrustServerCertificate=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "Select * from Users where userId=@id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                userInfo.id = "" + reader.GetInt32(0);
                            }
                        }
                    }

                    string sqlTrans = "Select transactionId,amount,Type,category,transDate,description from Transactions where userId=@id";
                    using (SqlCommand command = new SqlCommand(sqlTrans, connection))
                    {
                        command.Parameters.AddWithValue("id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                 Transactions transaction = new Transactions();
                                transaction.transId = "" + reader.GetInt32(0);
                                transaction.amount = reader.GetDecimal(1).ToString();
                                transaction.type = reader.GetString(2);
                                transaction.category = reader.GetString(3);
                                transaction.transDate = reader.GetDateTime(4).ToString();
                                transaction.description = reader.GetString(5);
                                listTrans.Add(transaction);
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
