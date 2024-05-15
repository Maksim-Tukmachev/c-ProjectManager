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
    public partial class RegisterForm : Form
    {
        public RegisterForm()
        {
            InitializeComponent();
        }

        private void closeBtn_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        Point lastPoint;
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
                if (e.Button == MouseButtons.Left)
                {
                    this.Left += e.X - lastPoint.X;
                    this.Top += e.Y - lastPoint.Y;
            }
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = new Point(e.X, e.Y);
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox1_MouseDown_1(object sender, MouseEventArgs e)
        {
            lastPoint = new Point(e.X, e.Y);
        }

        private void pictureBox1_MouseMove_1(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastPoint.X;
                this.Top += e.Y - lastPoint.Y;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "Логин")
            {
                MessageBox.Show("Введите логин");
                return;
            }
            if (textBox2.Text == "Пароль")
            {
                MessageBox.Show("Введите пароль");
                return;
            }
            if (textBox3.Text == "Почта")
            {
                MessageBox.Show("Введите почту");
                return;
            }

            if (isUserExists())
                return;

            DB db = new DB();

            MySqlCommand command = new MySqlCommand("INSERT INTO `users` (`login`, `pass`, `mail`) VALUES (@login, @pass, @mail);", db.getConnection());
            command.Parameters.Add("@login", MySqlDbType.VarChar).Value = textBox1.Text;
            command.Parameters.Add("@pass", MySqlDbType.VarChar).Value = textBox2.Text;
            command.Parameters.Add("@mail", MySqlDbType.VarChar).Value = textBox3.Text;

            
            db.openConnection();

            if (command.ExecuteNonQuery() == 1)
            {
                this.Hide();
                Form1 form1 = new Form1();
                form1.Show();
            }
                
            else
                MessageBox.Show("Аккаунт не был создан");

            db.closeConnection();
        }

        public Boolean isUserExists()
        {
            DB db = new DB();
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();

            db.openConnection();
            MySqlCommand command = new MySqlCommand("SELECT * FROM `users` WHERE `login` = @login ", db.getConnection());
            command.Parameters.Add("@login", MySqlDbType.VarChar).Value = textBox1.Text;

            adapter.SelectCommand = command;
            adapter.Fill(table);

            if (table.Rows.Count > 0)
            {
                MessageBox.Show("Такой логин уже используется");
                return true;
            }

            else
                return false;
            db.closeConnection();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 form1 = new Form1();
            form1.Show();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
