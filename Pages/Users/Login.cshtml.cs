using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace BCCM1.Pages.Users
{
    public class LoginModel : PageModel
    {
        public UsersInfo userInfo = new UsersInfo();
        public string errorMessage = ""; //hiển thị thông báo lỗi khi người dùng ko nhập đủ thông tin 

        public void OnGet()
        {
        }

        public void OnPost() //xử lý http method: POST xảy ra khi người dùng click Submit button 
        {
            //property Request biểu diễn các thông tin do user gửi yêu cầu (request) đến Server 
            userInfo.email = Request.Form["email"];
            userInfo.pass = Request.Form["pass"];

            //check all fields are filled 
            if (string.IsNullOrEmpty(userInfo.email) || string.IsNullOrEmpty(userInfo.pass))
            {
                errorMessage = "All fields are required!!!";
                return;
            }

            //if ok, save new client to database 
            try
            {
                string connectionString = "Data Source=THANHHOA\\MSSQLSERVER01;Initial Catalog=HFINANCE;Integrated Security = True; Pooling = False; TrustServerCertificate = True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "INSERT INTO Users" +
                        "(email, passWord) VALUES" +
                        "(@email, @passWord;";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@email", userInfo.email);
                        command.Parameters.AddWithValue("@phone", userInfo.pass);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }

            //clear info for next input 
            userInfo.email = "";
            userInfo.pass = "";
            Response.Redirect("/Users/Homepage");
            //sau khi thêm thành công 1 row thì redirect về trang /clients/index để hiển thị danh sách chi tiết clients


        }
    }
}
