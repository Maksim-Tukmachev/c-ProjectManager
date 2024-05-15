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
    public partial class ProjectForm : Form
    {
        public ProjectForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddProject pr = new AddProject();
            pr.ShowDialog();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void ProjectForm_Load(object sender, EventArgs e)
        {
            DB db = new DB();
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();


            db.openConnection();
            // Получить команду пользователя из таблицы users
            MySqlCommand getUserCommand = new MySqlCommand("SELECT team FROM users WHERE login = @login", db.getConnection());
            getUserCommand.Parameters.AddWithValue("@login", User.Instance.Login);
            string userTeam = getUserCommand.ExecuteScalar().ToString();


            MySqlCommand command = new MySqlCommand("SELECT `name`, `description`, `team`, `task` FROM `project` WHERE `team` = @team", db.getConnection());
            command.Parameters.Add("@team", MySqlDbType.VarChar).Value = userTeam;

            MySqlDataReader reader = command.ExecuteReader();

            dataGridView1.Rows.Clear();

            while (reader.Read())
            {
                dataGridView1.Rows.Add(reader["name"], reader["description"], reader["team"], reader["task"]);
            }

            db.closeConnection();
        }
    }
}
