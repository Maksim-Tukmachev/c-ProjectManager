using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TaskManagerApp
{
    public partial class AccountForm : Form
    {
        public AccountForm()
        {
            InitializeComponent();
        }

        private void AccountForm_Load(object sender, EventArgs e)
        {
            String login = User.Instance.Login;
            string password = "";
            string email = "";


            DB db = new DB();
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();


            MySqlCommand command = new MySqlCommand("SELECT * FROM `users` WHERE `login` = @login", db.getConnection());
            command.Parameters.Add("@login", MySqlDbType.VarChar).Value = login;
            

            adapter.SelectCommand = command;
            adapter.Fill(table);
            if (table.Rows.Count > 0)
            {
                DataRow row = table.Rows[0];
                password = row["pass"].ToString();
                email = row["mail"].ToString();

                
            }   

            textBox2.Text = password;
            textBox3.Text = email;
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void panel3_MouseClick(object sender, MouseEventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string login = User.Instance.Login;
            string password = textBox2.Text;
            string email = textBox3.Text;

            DB db = new DB();
            MySqlCommand command = new MySqlCommand("UPDATE `users` SET `pass` = @pass, `mail` = @mail WHERE `login` = @login", db.getConnection());
            command.Parameters.Add("@login", MySqlDbType.VarChar).Value = login;
            command.Parameters.Add("@pass", MySqlDbType.VarChar).Value = password;
            command.Parameters.Add("@mail", MySqlDbType.VarChar).Value = email;

            db.openConnection();

            if (command.ExecuteNonQuery() == 1)
            {
                MessageBox.Show("Данные обновлены");
            }
            else
            {
                MessageBox.Show("Ошибка обновления данных");
            }

            db.closeConnection();

        }

        Point lastPoint;
        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = new Point(e.X, e.Y);
        }

        private void panel2_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastPoint.X;
                this.Top += e.Y - lastPoint.Y;
            }
        }

        private void newsBtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            MainForm mainForm = new MainForm();
            mainForm.Show();
        }
    }
}
