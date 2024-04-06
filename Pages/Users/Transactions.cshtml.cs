using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Transactions;

namespace BCCM1.Pages.Users
{
    public class TransactionsModel : PageModel
    {
        public UsersInfo userInfo = new UsersInfo();
        public string errorMessage = "";

        public List<Categories> listCate = new List<Categories>();
        public List<TransCate> transCates = new List<TransCate>();
        public void OnGet()
        {
            String id = Request.Query["id"];
            try
            {
                string connectionString = "Data Source = THANHHOA\\MSSQLSERVER01;Initial Catalog=HFINANCE02;" + "Integrated Security=True;Pooling=False;TrustServerCertificate=True";
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
                                userInfo.email = reader.GetString(1);
                            }
                        }
                    }

                    string sqlCate = "  Select categoryId,cateName from Categories WHERE userId IS NULL OR userId=@id ";
                    using (SqlCommand command = new SqlCommand(sqlCate, connection))
                    {
                        command.Parameters.AddWithValue("id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Categories cate = new Categories();
                                cate.cateId = "" + reader.GetInt32(0);
                                cate.cateName= reader.GetString(1);
                                listCate.Add(cate);
                            }
                        }
                    }

                    string sqlTrans = "Select transactionId,amount,cateName,transType,transDate,description from Transactions INNER JOIN Categories ON Transactions.categoryId=Categories.categoryId where Transactions.userId=@id";
                    using (SqlCommand command = new SqlCommand(sqlTrans, connection))
                    {
                        command.Parameters.AddWithValue("id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                TransCate transCate = new TransCate();
                                transCate.transId = "" + reader.GetInt32(0);
                                transCate.amount = reader.GetDecimal(1).ToString();
                                transCate.cateName = reader.GetString(2);
                                transCate.transType = reader.GetString(3);
                                transCate.transDate = reader.GetString(4);
                                transCate.description = reader.GetString(5);
                                transCates.Add(transCate);
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


        public void OnPost()
        {

            string id = Request.Query["id"];

            Transactions trans = new Transactions();
            trans.amount = Request.Form["amount"];
            trans.cateId = Request.Form["cateId"];
            trans.transDate = Request.Form["transDate"];
            trans.description = Request.Form["description"];

            //check all fields are filled 
            if (string.IsNullOrEmpty(trans.amount) || string.IsNullOrEmpty(trans.cateId) || string.IsNullOrEmpty(trans.transDate) || string.IsNullOrEmpty(trans.description))
            {
                Response.Redirect("/Users/Transactions?id=" + id);
                return;
            }

            //if ok, save new client to database 
            try
            {
                string connectionString = "Data Source=THANHHOA\\MSSQLSERVER01;Initial Catalog=HFINANCE02;Integrated Security = True; Pooling = False; TrustServerCertificate = True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string addTrans = "INSERT INTO Transactions(userId,amount,categoryId,transDate,description) VALUES (@id, @amount, @cateId, @transDate,@description)";
                    using (SqlCommand command = new SqlCommand(addTrans, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        command.Parameters.AddWithValue("@amount", trans.amount);
                        command.Parameters.AddWithValue("@cateId", trans.cateId);
                        command.Parameters.AddWithValue("@transDate", trans.transDate);
                        command.Parameters.AddWithValue("@description", trans.description);

                        command.ExecuteNonQuery();
                    }


                }

                Response.Redirect("/Users/Transactions?id=" + id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }
        }

    }
}
