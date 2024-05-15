using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagerApp
{
    public class News
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Description { get; set; }

        public News()
        {
            Id = -1;
            Title = string.Empty;
            Description = string.Empty;
            ReleaseDate = DateTime.MinValue;
            
        }

        public News(int id, string title, string description, DateTime releaseDate)
        {
            Id = id;
            Title = title;
            Description = description;
            ReleaseDate = releaseDate;
        }

        public void Copy(News movie)
        {
            Id = movie.Id;
            Title = movie.Title;
            Description = movie.Description;
            ReleaseDate = movie.ReleaseDate;
        }
        public static List<News> GetAllMovies()
        {
            DB db = new DB();
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();

            db.openConnection();
            MySqlCommand command = new MySqlCommand("SELECT * FROM `news`", db.getConnection());

            adapter.SelectCommand = command;
            adapter.Fill(table);

            List<News> movies = new List<News>();
            foreach (DataRow row in table.Rows)
            {
                int id = Convert.ToInt32(row["id"]);
                string title = row["header"].ToString();
                DateTime releaseDate = Convert.ToDateTime(row["date"]);
                string description = row["description"].ToString();

                movies.Add(new News(id, title, description, releaseDate));
            }

            db.closeConnection();

            return movies;
        }

        public static void AddMovieToDB(News movie)
        {
            DB db = new DB();
            db.openConnection();
            MySqlCommand command = new MySqlCommand("INSERT INTO `news` (`header`, `date`, `description`) VALUES (@title, @releaseDate, @description)", db.getConnection());
            command.Parameters.Add("@title", MySqlDbType.VarChar).Value = movie.Title;
            command.Parameters.Add("@releaseDate", MySqlDbType.DateTime).Value = movie.ReleaseDate;
            command.Parameters.Add("@description", MySqlDbType.VarChar).Value = movie.Description;
            command.ExecuteNonQuery();
            db.closeConnection();
        }

        public static News GetMovieFromDB(int id)
        {
            DB db = new DB();
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();

            db.openConnection();
            MySqlCommand command = new MySqlCommand("SELECT * FROM `news` WHERE `id` = @id ORDER BY `id` DESC", db.getConnection());
            command.Parameters.Add("@id", MySqlDbType.Int32).Value = id;

            adapter.SelectCommand = command;
            adapter.Fill(table);

            if (table.Rows.Count > 0)
            {
                DataRow row = table.Rows[0];
                string title = row["header"].ToString();
                DateTime releaseDate = Convert.ToDateTime(row["date"]);
                string description = row["description"].ToString();

                return new News(id, title, description, releaseDate);
            }
            else
            {
                return null;
            }
        }

        public static void UpdateMovieInDB(News movie)
        {
            DB db = new DB();
            db.openConnection();
            MySqlCommand command = new MySqlCommand("UPDATE `news` SET `header` = @title, `date` = @releaseDate, `description` = @description WHERE `id` = @id", db.getConnection());
            command.Parameters.Add("@id", MySqlDbType.Int32).Value = movie.Id;
            command.Parameters.Add("@title", MySqlDbType.VarChar).Value = movie.Title;
            command.Parameters.Add("@releaseDate", MySqlDbType.DateTime).Value = movie.ReleaseDate;
            command.Parameters.Add("@description", MySqlDbType.VarChar).Value = movie.Description;
            command.ExecuteNonQuery();
            db.closeConnection();
        }

        public static void DeleteMovieFromDB(int id)
        {
            DB db = new DB();
            db.openConnection();
            MySqlCommand command = new MySqlCommand("DELETE FROM `news` WHERE `id` = @id", db.getConnection());
            command.Parameters.Add("@id", MySqlDbType.Int32).Value = id;
            command.ExecuteNonQuery();
            db.closeConnection();
        }
    }
}


