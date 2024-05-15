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
    public partial class AddTeam : Form
    {
        public AddTeam()
        {
            InitializeComponent();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            DB db = new DB();

            MySqlCommand command = new MySqlCommand("INSERT INTO `teamRequest` (`name`, `description`, `login`) VALUES (@name, @description, @login);", db.getConnection());
            command.Parameters.Add("@name", MySqlDbType.VarChar).Value = TxtTitle.Text;
            command.Parameters.Add("@description", MySqlDbType.VarChar).Value = TxtDescription.Text;
            command.Parameters.Add("@name", MySqlDbType.VarChar).Value = User.Instance.Login;

            db.openConnection();

            if (command.ExecuteNonQuery() == 1)
            {
                MessageBox.Show("Заявка отправлена.");
            }

            else
                MessageBox.Show("Заявка не была отправлена.");

            db.closeConnection();
        }

        private void TxtTitle_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
