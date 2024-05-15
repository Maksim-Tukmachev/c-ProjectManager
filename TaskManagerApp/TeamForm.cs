using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TaskManagerApp
{
    public partial class TeamForm : Form
    {
        public TeamForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void TeamForm_Load(object sender, EventArgs e)
        {
            
            DB db = new DB();
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();

            db.openConnection();
            MySqlCommand command = new MySqlCommand("SELECT `role`, `team` FROM `users` WHERE `login` = @login", db.getConnection());
            command.Parameters.Add("@login", MySqlDbType.VarChar).Value = User.Instance.Login;

            adapter.SelectCommand = command;
            adapter.Fill(table);
            string role = ""; 
            string team = "";

            if (table.Rows.Count > 0)
            {
                DataRow row = table.Rows[0];
                role = row["role"].ToString();
                team = row["team"].ToString();   
            }
            db.closeConnection();
            
            panel4.Visible = false;        
            flowLayoutPanel1.Visible = false;
            
           if (role == "TeamLead" && team == "")
           {
               panel4.Visible = true;
           
           }
           else if (role != "TeamLead" && team == "" )
           {
               panel4.Visible = true;
               BtnAdd.Visible = false;
               label4.Text = "Попросите вашего TeamLead добавить вас в команду";
           }
           else
           {
               flowLayoutPanel1.Visible = true;
           }
           
           List<Team> teamFromDB = Team.GetTeammatesFromDB(team);
           
           // Add each movie to the UI
           foreach (Team movie in teamFromDB)
           {
               AddTeamToUI(movie);
           }

        }

        private void label2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            this.Hide();
            MainForm mainForm = new MainForm();
            mainForm.Show();
        }

        private void AccountBtn_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            AccountForm accountForm = new AccountForm();
            accountForm.Show();
        }
        Point lastPoint;
        private void panel23_MouseDown_1(object sender, MouseEventArgs e)
        {
            lastPoint = new Point(e.X, e.Y);
        }

        private void panel23_MouseMove_1(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastPoint.X;
                this.Top += e.Y - lastPoint.Y;
            }
        }

        private void panel3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void AddTeamToUI(Team movie)
        {
            //Create panel
            Panel panel;
            panel = new Panel();
            panel.Name = String.Format("PnlMovie{0}", movie.Id);
            panel.BackColor = Color.White;
            panel.Size = new Size(170, 220);
            panel.Margin = new Padding(10);
            panel.Tag = movie.Id;

            //Create picture box
            PictureBox pictureBox;
            pictureBox = new PictureBox();
            pictureBox.Name = String.Format("PicBoxMovie{0}", movie.Id);
            pictureBox.Size = new Size(150, 150); 
            pictureBox.Location = new Point(10, 10); 
            pictureBox.SizeMode = PictureBoxSizeMode.StretchImage; 
            pictureBox.Image = Image.FromFile(Path.Combine(Application.StartupPath, "C:\\Users/User/source/repos/TaskManagerApp/TaskManagerApp/user(1).png")); // Загрузка изображения из файла
            pictureBox.Tag = movie.Id;

            //Create title label
            Label labelTitle;
            labelTitle = new Label();
            labelTitle.Name = String.Format("LblMovieTitle{0}", movie.Id);
            labelTitle.Text = movie.Login;
            labelTitle.Location = new Point(15, 160);
            labelTitle.ForeColor = Color.Black;
            labelTitle.Font = new Font(this.Font.FontFamily, 15f, FontStyle.Bold);
            labelTitle.AutoEllipsis = true;
            labelTitle.AutoSize = true; // Set AutoSize to false
            labelTitle.Size = new Size(100, 17);
            labelTitle.Tag = movie.Id;

            // Create description label
            Label labelDescription;
            labelDescription = new Label();
            labelDescription.Name = String.Format("LblMovieDescription{0}", movie.Id);
            labelDescription.Text = movie.Role;
            labelDescription.Location = new Point(15, 190); // Adjust the location as needed
            labelDescription.ForeColor = Color.Black;
            labelDescription.Font = new Font(this.Font.FontFamily, 9.5f, FontStyle.Regular);
            labelDescription.AutoSize = true; // Set AutoSize to false
            labelDescription.AutoEllipsis = true; // Enable AutoEllipsis
            labelDescription.Size = new Size(100, 30); // Set maximum width to 200 pixels (or any other value you prefer
            labelDescription.Tag = movie.Id;


            //Create year label
            //Label labelYear;
            //labelYear = new Label();
            //labelYear.Name = String.Format("LblMovieYear{0}", movie.Id);
            //labelYear.Text = movie.ReleaseDate.Year.ToString();
            //labelYear.Location = new Point(12, 310);
            //labelYear.ForeColor = Color.Gray;
            //labelYear.Font = new Font(this.Font.FontFamily, 9.5f, FontStyle.Regular);
            //labelYear.Tag = movie.Id;

            //Set Context Menu
            panel.ContextMenuStrip = contextMenuStrip1;

            //Add controls to panel 
            panel.Controls.Add(labelTitle);
            panel.Controls.Add(labelDescription);
            panel.Controls.Add(pictureBox);
            //panel.Controls.Add(labelYear);

            //Add Event Handlers 
            //panel.DoubleClick += new EventHandler(Edit_DoubleClick);
            //
            //foreach (Control c in panel.Controls)
            //{
            //    c.DoubleClick += new EventHandler(Edit_DoubleClick);
            //}

            //Add panel to flowlayoutpanel
            flowLayoutPanel1.Controls.Add(panel);
            flowLayoutPanel1.Controls.SetChildIndex(panel, 0);
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            AddTeam team = new AddTeam();
            team.ShowDialog();
        }
    }
}
