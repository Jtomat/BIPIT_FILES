using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace BIPIT_front
{
    public partial class Form1 : Form
    {
        private class CombobxRec
        {
            public string Text { get; set; }
            public int Value { get; set; }
            public override string ToString()
            {
                return Text;
            }
        }
        ServiceRef.ServiceClient serv = new ServiceRef.ServiceClient();
        public Form1()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            UpdateTable();
            InsertUsers();
            InsertBooks();
        }

        private void InsertUsers()
        {
            comboBox1.Items.Clear();
            var data = serv.Users();
            for (int i = 0; i < data.Length; i++)
            {
                comboBox1.Items.Add(new CombobxRec { Text = data[i][1], Value = int.Parse(data[i][0]) });
            }
        }
        private void InsertBooks()
        {
            comboBox2.Items.Clear();
            var data = serv.Books();
            for (int i = 0; i < data.Length; i++)
            {
                comboBox2.Items.Add(new CombobxRec { Text = data[i][1], Value = int.Parse(data[i][0]) });
            }
        }
        private void UpdateTable()
        {
            dataGridView1.Rows.Clear();
            var data = serv.Get_Data();
            for (int i = 0; i < data.Length; i++)
            {
                var par=new object[data[i].Length];
                for (int j = 0; j < data[i].Length-2; j++)
                {
                    par[j] = data[i][j];
                }
                dataGridView1.Rows.Add(par);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(dateTimePicker1.Value==null||
                dateTimePicker2.Value==null||
                dateTimePicker3.Value==null||
                comboBox1.SelectedIndex==-1||
                comboBox2.SelectedIndex==-1)
            {
                MessageBox.Show("Одно или несколько полей не заполнены.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (dateTimePicker1.Value > dateTimePicker2.Value || dateTimePicker1.Value > dateTimePicker3.Value)
            {
                MessageBox.Show("Дата приемки не может быть раньше, чем дата выдачи.","Error!",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }
            var res = serv.Add_Rec(
                id_u:(comboBox1.SelectedItem as CombobxRec).Value, 
                id_b:(comboBox2.SelectedItem as CombobxRec).Value,
                dateFrom: dateTimePicker1.Value.ToShortDateString(),
                dateTo: dateTimePicker2.Value.ToShortDateString(),
                dateFact: dateTimePicker3.Value.ToShortDateString());
            if (res)
            {
                UpdateTable();
                MessageBox.Show("Запись успешно добавлена.", "Success.", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Произошла ошибка на сервере в ходе добавления записи.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
