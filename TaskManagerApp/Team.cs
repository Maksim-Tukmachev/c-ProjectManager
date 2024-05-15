using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagerApp
{
    class Team
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Role { get; set; }


        public Team()
        {
            Id = -1;
            Login = string.Empty;
            Role = string.Empty;
        }

        public Team(int id, string login, string role)
        {
            Id = id;
            Login = login;
            Role = role;
        }

        public void Copy(Team team)
        {
            Id = team.Id;
            Login = team.Login;
            Role = team.Role;
        }
        public static List<Team> GetTeammatesFromDB(string teamName)
        {
            DB db = new DB();
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();

            db.openConnection();
            MySqlCommand command = new MySqlCommand("SELECT * FROM `users` WHERE `team` = @team", db.getConnection());
            command.Parameters.Add("@team", MySqlDbType.VarChar).Value = teamName;

            adapter.SelectCommand = command;
            adapter.Fill(table);

            List<Team> teammates = new List<Team>();
            if (table.Rows.Count > 0)
            {
                foreach (DataRow row in table.Rows)
                {
                    int id = Convert.ToInt32(row["id"]);
                    string login = row["login"].ToString();
                    string role = row["role"].ToString();
                    teammates.Add(new Team(id, login, role));
                }
            }

            db.closeConnection();

            return teammates;
        }


        public static void AddTeammateToDB(int userId, int teamId)
        {
            DB db = new DB();
            db.openConnection();
            MySqlCommand command = new MySqlCommand("UPDATE `users` SET `team` = @team WHERE `id` = @id", db.getConnection());
            command.Parameters.Add("@id", MySqlDbType.Int32).Value = userId;
            command.Parameters.Add("@team", MySqlDbType.Int32).Value = teamId;
            command.ExecuteNonQuery();
            db.closeConnection();
        }

        public static void RemoveTeammateFromDB(int userId)
        {
            DB db = new DB();
            db.openConnection();
            MySqlCommand command = new MySqlCommand("UPDATE `users` SET `team` = NULL WHERE `id` = @id", db.getConnection());
            command.Parameters.Add("@id", MySqlDbType.Int32).Value = userId;
            command.ExecuteNonQuery();
            db.closeConnection();
        }
    }
}


