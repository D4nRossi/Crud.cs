using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Crud.Pages.Clients
{
    public class IndexModel : PageModel
    {
        //Criar uma lista com base na class ClientInfo para armazenar todos os clientes
        public List<ClientInfo> ListClients = new List<ClientInfo>();


        public void OnGet()
        {
            try {
                //Conectar com o BD
                string connectionString = "Data Source=(localdb)\\Daniel;Initial Catalog=Crud;Integrated Security=True";

                //Criando a conexao
                using (SqlConnection connection = new SqlConnection(connectionString)) {
                    connection.Open();

                    //Criando a Query
                    String sql = "select * from clients";

                    //Executando a Query
                    using (SqlCommand command = new SqlCommand(sql, connection)) {
                        using (SqlDataReader reader = command.ExecuteReader()) {
                            while (reader.Read()) {
                            ClientInfo clientInfo = new ClientInfo();
                                clientInfo.id = "" + reader.GetInt32(0);
                                clientInfo.name = reader.GetString(1);
                                clientInfo.email = reader.GetString(2);
                                clientInfo.phone = reader.GetString(3);
                                clientInfo.address = reader.GetString(4);
                                clientInfo.created_at = reader.GetDateTime(5).ToString();

                                //Adicioando na lista
                                ListClients.Add(clientInfo);
                        }
                        }
                    }
                }

            }catch (Exception ex) {
                Console.WriteLine("Exception: " + ex.ToString());
            }


        }
    }
    //Clase para armazenar um Cliente
    public class ClientInfo {
        public string id;
        public string name;
        public string email;
        public string phone;
        public string address;
        public string created_at;
    }

}
