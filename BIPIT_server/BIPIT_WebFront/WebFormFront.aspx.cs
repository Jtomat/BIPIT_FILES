using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BIPIT_WebFront
{
    public partial class WebFormFront : System.Web.UI.Page
    {
        ServiceWTCP.ServiceClient serv = new ServiceWTCP.ServiceClient();
        protected void Page_Load(object sender, EventArgs e)
        {
            UpdateTable();
            InsertUsers();
            InsertBooks();
            ;
        }
        private class CombobxRec
        {
            public string Text { get; set; }
            public int Value { get; set; }
            public override string ToString()
            {
                return Text;
            }
        }
        

        private void InsertUsers()
        {
            DropDownList1.Items.Clear();
            var data = serv.Users();
            var sourse = new Dictionary<int, string>();
            for (int i = 0; i < data.Length; i++)
            {
                //sourse.Add(int.Parse(data[i][0]), Text = data[i][1])
               DropDownList1.Items.Add(new ListItem{ Text = data[i][1], Value = (data[i][0]) });
            }
        }
        private void InsertBooks()
        {
            DropDownList2.Items.Clear();
            var data = serv.Books();
            for (int i = 0; i < data.Length; i++)
            {
                DropDownList2.Items.Add(new ListItem { Text = data[i][1], Value = (data[i][0]) });
            }
        }
        public class RowRec
        {
            public string id { get; set; }
            public string User { get; set; }
            public string Book { get; set; }
            public string DFR { get; set; }
            public string DT { get; set; }
            public string DFA { get; set; }
        }
        private void UpdateTable()
        {
            GridView1.DataSource=null;
            var data = serv.Get_Data();
            var sourse = new RowRec[data.Length];
            for (int i = 0; i < data.Length; i++)
            {
                //var par = new object[data[i].Length];
                sourse[i] = new RowRec
                {
                    id = data[i][0],
                    User = data[i][1],
                    Book = data[i][2],
                    DFR = data[i][3],
                    DT = data[i][4],
                    DFA = data[i][5]
                };

            }
            GridView1.DataSource = sourse;
            GridView1.DataBind();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Calendar1.SelectedDate == null ||
                Calendar2.SelectedDate == null ||
                Calendar3.SelectedDate == null ||
                DropDownList1.SelectedIndex == -1 ||
                DropDownList2.SelectedIndex == -1)
            {
                //MessageBox.Show("Одно или несколько полей не заполнены.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (Calendar1.SelectedDate > Calendar2.SelectedDate || Calendar1.SelectedDate > Calendar3.SelectedDate)
            {
                //MessageBox.Show("Дата приемки не может быть раньше, чем дата выдачи.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var res = serv.Add_Rec(
                id_u:int.Parse( (DropDownList1.SelectedItem as ListItem).Value),
                id_b:int.Parse( (DropDownList2.SelectedItem as ListItem).Value),
                dateFrom:Calendar1.SelectedDate.ToShortDateString(),
                dateTo: Calendar2.SelectedDate.ToShortDateString(),
                dateFact: Calendar3.SelectedDate.ToShortDateString());
            if (res)
            {
                UpdateTable();
                //MessageBox.Show("Запись успешно добавлена.", "Success.", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            //else
               // MessageBox.Show("Произошла ошибка на сервере в ходе добавления записи.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        protected void Button1_Click1(object sender, EventArgs e)
        {
            if (Calendar1.SelectedDate == null ||
    Calendar2.SelectedDate == null ||
    Calendar3.SelectedDate == null ||
    DropDownList1.SelectedIndex == -1 ||
    DropDownList2.SelectedIndex == -1)
            {
                //MessageBox.Show("Одно или несколько полей не заполнены.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (Calendar1.SelectedDate > Calendar2.SelectedDate || Calendar1.SelectedDate > Calendar3.SelectedDate)
            {
                //MessageBox.Show("Дата приемки не может быть раньше, чем дата выдачи.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var res = serv.Add_Rec(
                id_u: int.Parse((DropDownList1.SelectedItem as ListItem).Value),
                id_b: int.Parse((DropDownList2.SelectedItem as ListItem).Value),
                dateFrom: Calendar1.SelectedDate.ToShortDateString(),
                dateTo: Calendar2.SelectedDate.ToShortDateString(),
                dateFact: Calendar3.SelectedDate.ToShortDateString());
            if (res)
            {
                UpdateTable();
                
                //MessageBox.Show("Запись успешно добавлена.", "Success.", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}