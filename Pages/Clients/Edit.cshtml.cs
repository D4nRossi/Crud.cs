using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Crud.Pages.Clients
{
    public class EditModel : PageModel
    {

        public ClientInfo clientInfo = new ClientInfo();
        public string errorMessage = "";
        public string successMessage = "";

        public void OnGet()
        {
            //Ler o ID da pessoa
            string id = Request.Query["id"];

            //Conectando com o BD
            try {
                string connectionString = "Data Source=(localdb)\\Daniel;Initial Catalog=Crud;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString)) {
                    connection.Open();

                    //Criando a Query
                    String sql = "select * from clients where id=@id";

                    //Executando a Query
                    using (SqlCommand command = new SqlCommand(sql, connection)) {
                        command.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = command.ExecuteReader()) {
                            if (reader.Read()) {
                                clientInfo.id = "" + reader.GetInt32(0);
                                clientInfo.name = reader.GetString(1);
                                clientInfo.email = reader.GetString(2);
                                clientInfo.phone = reader.GetString(3);
                                clientInfo.address = reader.GetString(4);
                            }
                        }
                    }
                }

            } catch (Exception ex) { 
                errorMessage = ex.Message;
                return;
            }
        }

        public void OnPost() {
            clientInfo.id = Request.Form["id"];
            clientInfo.name = Request.Form["name"];
            clientInfo.email = Request.Form["email"];
            clientInfo.phone = Request.Form["phone"];
            clientInfo.address = Request.Form["address"];

            if (clientInfo.name.Length == 0 || clientInfo.email.Length == 0 || clientInfo.phone.Length == 0 || clientInfo.address.Length == 0) {
                errorMessage = "All the fields are required";
                return;
            }

            try {
                //Conectando com o BD
                string connectionString = "Data Source=(localdb)\\Daniel;Initial Catalog=Crud;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString)) {
                    connection.Open();
                    string sql = "update clients " + "set name=@name, email=@email, phone=@phone, address=@address " + "where id=@id";
                    //Mapeando os parametros
                    using (SqlCommand command = new SqlCommand(sql, connection)) {
                        command.Parameters.AddWithValue("@name", clientInfo.name);
                        command.Parameters.AddWithValue("@email", clientInfo.email);
                        command.Parameters.AddWithValue("@phone", clientInfo.phone);
                        command.Parameters.AddWithValue("@address", clientInfo.address);
                        command.Parameters.AddWithValue("@id", clientInfo.id);

                        command.ExecuteNonQuery();
                    }
                }

            } catch(Exception ex) {
                errorMessage = ex.Message;
                return;
            }

            Response.Redirect("/Clients/Index");
        }
    }
}
