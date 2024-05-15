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
    public partial class AddProject : Form
    {
        public AddProject()
        {
            InitializeComponent();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            DB db = new DB();
            db.openConnection();

            MySqlCommand command = new MySqlCommand("SELECT `team` FROM `users` WHERE `login` = @login", db.getConnection());
            command.Parameters.Add("@login", MySqlDbType.VarChar).Value = User.Instance.Login;

            object result = command.ExecuteScalar();
            db.closeConnection();

             string userTeam = result.ToString();

            if (string.IsNullOrEmpty(userTeam))
            {
                this.Close();
                MessageBox.Show("Проект не создан, у вас нет команды.");
            }
           
            else
            {
                db.openConnection();
                MySqlCommand command2 = new MySqlCommand("INSERT INTO `project` (`name`, `description`, `team`, `task`) VALUES (@name, @description, @team, @task);", db.getConnection());
                command2.Parameters.Add("@name", MySqlDbType.VarChar).Value = TxtTitle.Text;
                command2.Parameters.Add("@description", MySqlDbType.VarChar).Value = TxtDescription.Text;
                command2.Parameters.Add("@team", MySqlDbType.VarChar).Value = userTeam;
                command2.Parameters.Add("@task", MySqlDbType.VarChar).Value = 0;

                if (command2.ExecuteNonQuery() == 1)
                {
                    this.Close();
                }

                else
                    MessageBox.Show("Проект не создан.");
            }
            db.closeConnection();

        }
    }
}
