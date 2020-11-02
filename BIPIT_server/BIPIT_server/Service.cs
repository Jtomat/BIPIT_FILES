using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading;

namespace BIPIT_server
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени класса "Service" в коде и файле конфигурации.
    //[ServiceContractAttribute]
    public class Service : IService
    {
        SqlConnection con = new SqlConnection("workstation id=OrdersBIPIT.mssql.somee.com;packet size=4096;user id=jtomatos_SQLLogin_1;pwd=bvl14xaa5f;data source=OrdersBIPIT.mssql.somee.com;persist security info=False;initial catalog=OrdersBIPIT");

        [return: MessageParameter(Name = "Data")]
        public bool Add_Rec(int id_b, int id_u, string dateFrom, string dateTo, string dateFact = "")
        {
            var data = true;
            try
            {
                var res = Exect($"insert into Orders (id_user,id_book,from_date,to_date,to_date_fact) values ('{id_u}','{id_b}',"+
                    $"'{DateTime.Parse(dateFrom).ToString("MM-dd-yyyy HH:mm:ss")}','{DateTime.Parse(dateTo).ToString("MM-dd-yyyy HH:mm:ss")}',"+
                    $"'{DateTime.Parse(dateFact).ToString("MM-dd-yyyy HH:mm:ss")}')");

                Program.PrintMessage($"Success operation: Add_Rec",GetPorts());
            }
            catch (Exception ex)
            {

                Program.PrintMessage($" Error on operation: Add_Rec",GetPorts());
                data = false;
            }
            return data;

        }

        [return: MessageParameter(Name = "Data")]
        public List<List<string>> Get_Data()
        {
            try
            {
                var data = Exect("select Orders.id,Users.name,Books.name,(CONVERT(varchar,from_date,4))" +
                    " as [From],(CONVERT(varchar,to_date,4)) as [To],(CONVERT(varchar,to_date_fact,4))" +
                    " as [Fact],(case when ((GETDATE() between from_date and to_date) and (to_date_fact>GETDATE()) or (to_date_fact>GETDATE()))" +
                    " Then 'Na rukah' when (GETDATE()>to_date) then 'V nalichii' when (from_date>GETDATE())" +
                    " then 'Zarezervirovano' end) as status, (case when(DATEDIFF(day, to_date, to_date_fact) > 0)" +
                    " Then Concat(DATEDIFF(day, to_date, to_date_fact), '') when(DATEDIFF(day, to_date, to_date_fact) < 1)" +
                    " Then 'Vo vremya' end) as days from Orders join Users on Users.id=id_user join Books on Books.id= id_book");
                Program.PrintMessage($"Success operation: Get_all",GetPorts());
                return data;
            }
            catch (Exception ex)
            {

                Program.PrintMessage($"Error on operation: Get_all",GetPorts());
                return new List<List<string>> { new List<string> { "Error", ex.Message, ex.HelpLink }, new List<string>() { null, null, null } };
            }
        }

        [return: MessageParameter(Name = "Data")]
        public List<List<string>> Books()
        {
            try
            {
                var data = Exect("select * from Books");

                Program.PrintMessage($"Success operation: Get_books_list",GetPorts());
                return data;
            }
            catch (Exception ex)
            {

                Program.PrintMessage($"Error on operation: Get_books_list",GetPorts());
                return new List<List<string>> { new List<string> { "Error", ex.Message, ex.HelpLink }, new List<string>() { null, null, null } };
            }
        }
        public string GetPorts()
        {
            return OperationContext.Current.Channel.LocalAddress.Uri.ToString().Remove(
                OperationContext.Current.Channel.LocalAddress.Uri.ToString().IndexOf("/D"),
                OperationContext.Current.Channel.LocalAddress.Uri.ToString().Length - 1 - OperationContext.Current.Channel.LocalAddress.Uri.ToString().IndexOf("/D"));
        }
        private List<List<string>> Exect(string que)
        {
            var result = new List<List<string>>();
            try { con.Open(); } catch { }
            try
            {
                var command = new SqlCommand(que, con);
                var reader = command.ExecuteReader();
                while (reader == null)
                    Thread.Sleep(10);
                while (reader.Read())
                {
                    List<string> support_memory = new List<string>();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        support_memory.Add(reader[i].ToString());
                    }
                    result.Add(support_memory);
                }
                reader.Close();
                return result;
            }
            catch (Exception ex)
            {
                Program.PrintMessage($"Error on operation: ExecuteQuery: {ex.Message}",GetPorts());
                return null;
            }
        }

        [return: MessageParameter(Name = "Data")]
        public List<List<string>> Users()
        {
            try
            {
                var data = Exect("select * from Users");

                Program.PrintMessage($"Success operation: Get_users_list",GetPorts());
                return data;
            }
            catch (Exception ex)
            {

                Program.PrintMessage($"Error on operation: Get_users_list",GetPorts());
                return new List<List<string>> { new List<string> { "Error", ex.Message, ex.HelpLink }, new List<string>() { null, null, null } };
            }
        }
    }
}
