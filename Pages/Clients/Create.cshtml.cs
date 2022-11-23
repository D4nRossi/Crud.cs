using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Crud.Pages.Clients
{
    public class CreateModel : PageModel
    {
        //Variavel sera iniciada com o post
        public ClientInfo clientInfo = new ClientInfo();
        //Mensagem caso algo esteja vazio
        public string errorMessage = "";
        //Mensagem de sucesso
        public string successMessage = "";

        public void OnGet()
        {
        }

        //Acao do post
        public void OnPost() {
            clientInfo.name = Request.Form["name"];
            clientInfo.email = Request.Form["email"];
            clientInfo.phone = Request.Form["phone"];
            clientInfo.address = Request.Form["address"];

            //Verificar se esta vazio
            if(clientInfo.name.Length == 0 || clientInfo.email.Length == 0 || clientInfo.phone.Length == 0 || clientInfo.address.Length == 0) {
                errorMessage = "All the fields are required";
                return;
            }

            //Salvar no banco
            try {
                //Conectando com o BD
                string connectionString = "Data Source=(localdb)\\Daniel;Initial Catalog=Crud;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString)) { 
                    connection.Open();
                    string sql = "insert into clients" + "(name, email, phone, address) values" + "(@name, @email, @phone, @address);";
                //Mapeando os parametros
                using (SqlCommand command = new SqlCommand(sql, connection)) {
                        command.Parameters.AddWithValue("@name", clientInfo.name);
                        command.Parameters.AddWithValue("@email", clientInfo.email);
                        command.Parameters.AddWithValue("@phone", clientInfo.phone);
                        command.Parameters.AddWithValue("@address", clientInfo.address);

                        command.ExecuteNonQuery();
                    }
                }


            } catch (Exception ex) { 
                errorMessage = ex.Message;
                return;
            }



            clientInfo.name = "";
            clientInfo.email = "";
            clientInfo.phone = "";
            clientInfo.address = "";
            successMessage = "Tudo certo meu parcinha";

            Response.Redirect("/Clients/Index");

        }

    }
}
