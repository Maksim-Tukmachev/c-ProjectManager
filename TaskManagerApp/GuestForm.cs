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
    public partial class GuestForm : Form
    {
        public GuestForm()
        {
            InitializeComponent();
        }

        private void GuestForm_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            DB db = new DB();

            MySqlCommand command = new MySqlCommand("INSERT INTO `feedback` (`rate`, `comment`) VALUES (@rate, @comment);", db.getConnection());
            command.Parameters.Add("@rate", MySqlDbType.VarChar).Value = textBox1.Text;
            command.Parameters.Add("@comment", MySqlDbType.VarChar).Value = textBox2.Text;

            db.openConnection();

            if (command.ExecuteNonQuery() == 1)
            {
                MessageBox.Show("Отзыв был отправлен");
            }

            else
                MessageBox.Show("Отзыв не был отправлен");

            db.closeConnection();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DB db = new DB();

            MySqlCommand command = new MySqlCommand("INSERT INTO `callback` (`name`, `phone`) VALUES (@name, @phone);", db.getConnection());
            command.Parameters.Add("@name", MySqlDbType.VarChar).Value = textBox4.Text;
            command.Parameters.Add("@phone", MySqlDbType.VarChar).Value = textBox3.Text;

            db.openConnection();

            if (command.ExecuteNonQuery() == 1)
            {
                MessageBox.Show("Обратный звонок заказан.");
            }

            else
                MessageBox.Show("Обратный звонок не заказан.");

            db.closeConnection();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DB db = new DB();

            MySqlCommand command = new MySqlCommand("INSERT INTO `summary` (`name`, `link`) VALUES (@name, @link);", db.getConnection());
            command.Parameters.Add("@name", MySqlDbType.VarChar).Value = textBox6.Text;
            command.Parameters.Add("@link", MySqlDbType.VarChar).Value = textBox5.Text;

            db.openConnection();

            if (command.ExecuteNonQuery() == 1)
            {
                MessageBox.Show("Резюме отправлено.");
            }

            else
                MessageBox.Show("Резюме не было отправлен.");

            db.closeConnection();
        }

        private void panel3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void label2_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        Point lastPoint;
        private void panel2_MouseDown_1(object sender, MouseEventArgs e)
        {
            lastPoint = new Point(e.X, e.Y);
        }

        private void panel2_MouseMove_1(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastPoint.X;
                this.Top += e.Y - lastPoint.Y;
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 form1 = new Form1();
            form1.Show();
        }
    }
}
