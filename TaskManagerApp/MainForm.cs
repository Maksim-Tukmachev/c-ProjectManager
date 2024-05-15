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
    public partial class MainForm : Form
    {
        
        private List<News> Movies = new List<News>();
        
        public MainForm()
        {
            InitializeComponent();
        }

        private void closeBtn1_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
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

        private void label2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


        private void panel3_MouseClick(object sender, MouseEventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void AccountBtn_Click(object sender, EventArgs e)
        {
            this.Close();
            AccountForm accountForm = new AccountForm();
            accountForm.Show();
        }

        private void comandBtn_Click(object sender, EventArgs e)
        {
            this.Close();
            TeamForm teamForm = new TeamForm();
            teamForm.Show();
        }

        private void AddMovieToUI(News movie)
        {
            //Create panel
            Panel panel;
            panel = new Panel();
            panel.Name = String.Format("PnlMovie{0}", movie.Id);
            panel.BackColor = Color.White;
            panel.Size = new Size(235, 335);
            panel.Margin = new Padding(10);
            panel.Tag = movie.Id;

            //Create title label
            Label labelTitle;
            labelTitle = new Label();
            labelTitle.Name = String.Format("LblMovieTitle{0}", movie.Id);
            labelTitle.Text = movie.Title;
            labelTitle.Location = new Point(12, 15);
            labelTitle.ForeColor = Color.Black;
            labelTitle.Font = new Font(this.Font.FontFamily, 15f, FontStyle.Bold);
            labelTitle.AutoEllipsis = true;
            labelTitle.AutoSize = false;
            labelTitle.Size = new Size(210, 65);
            labelTitle.Tag = movie.Id;

            // Create description label
            Label labelDescription;
            labelDescription = new Label();
            labelDescription.Name = String.Format("LblMovieDescription{0}", movie.Id);
            labelDescription.Text = movie.Description;
            labelDescription.Location = new Point(12, 80); // Adjust the location as needed
            labelDescription.ForeColor = Color.Black;
            labelDescription.Font = new Font(this.Font.FontFamily, 9.5f, FontStyle.Regular);
            labelDescription.AutoSize = false; // Set AutoSize to false
            labelDescription.AutoEllipsis = true; // Enable AutoEllipsis
            labelDescription.Size = new Size(210, 220); // Set maximum width to 200 pixels (or any other value you prefer)
            labelDescription.Tag = movie.Id;


            //Create year label
            Label labelYear;
            labelYear = new Label();
            labelYear.Name = String.Format("LblMovieYear{0}", movie.Id);
            labelYear.Text = movie.ReleaseDate.Year.ToString();
            labelYear.Location = new Point(12, 310);
            labelYear.ForeColor = Color.Gray;
            labelYear.Font = new Font(this.Font.FontFamily, 9.5f, FontStyle.Regular);
            labelYear.Tag = movie.Id;

            //Set Context Menu
            panel.ContextMenuStrip = contextMenuStrip1;

            //Add controls to panel 
            panel.Controls.Add(labelTitle);
            panel.Controls.Add(labelDescription);
            panel.Controls.Add(labelYear);

            //Add Event Handlers 
            panel.DoubleClick += new EventHandler(Edit_DoubleClick);

            foreach (Control c in panel.Controls)
            {
                c.DoubleClick += new EventHandler(Edit_DoubleClick);
            }

            //Add panel to flowlayoutpanel
            flowLayoutPanel1.Controls.Add(panel);
            flowLayoutPanel1.Controls.SetChildIndex(panel, 0);
        }

        // private void BtnAdd_Click_1(object sender, EventArgs e)
        // {
        //     AddEditMovie form = new AddEditMovie();
        //
        //     form.ShowDialog();
        //
        //     //Add new movie to list and UI
        //     if (form.DataSaved)
        //     {
        //         Movies.Add(form.NewMovie);
        //         AddMovieToUI(form.NewMovie);
        //     }
        // }
        private void BtnAdd_Click_1(object sender, EventArgs e)
        {
            AddEditNews form = new AddEditNews();

            form.ShowDialog();

            //Add new movie to list and UI
            if (form.DataSaved)
            {
                // Add the new movie to the database
                News.AddMovieToDB(form.NewMovie);
                AddMovieToUI(form.NewMovie);
            }
        }

        private void UpdateMovieInUI(News movie)
        {
            Control control;
            PictureBox picBox;
            string name;

            //Find picturebox and update movie image
            name = String.Format("PbMovieImage{0}", movie.Id);
            control = this.Controls.Find(name, true).FirstOrDefault();
            picBox = (PictureBox)control;

            if (File.Exists(movie.Description))
                picBox.Image = Image.FromFile(movie.Description);
            else
                picBox.Image = null;

            //Find movie title label and update text
            name = String.Format("LblMovieTitle{0}", movie.Id);
            control = this.Controls.Find(name, true).FirstOrDefault();
            control.Text = movie.Title;

            //Find movie year label and update text
            name = String.Format("LblMovieYear{0}", movie.Id);
            control = this.Controls.Find(name, true).FirstOrDefault();
            control.Text = movie.ReleaseDate.Year.ToString();
        }

        //private void Edit_DoubleClick(object sender, EventArgs e)
        //{
        //    Control c;
        //    int id;
        //    int index;
        //    Movie movie;
        //    AddEditMovie form;
        //
        //    //Get movie using control tag/id
        //    c = (Control)sender;
        //    id = (int)c.Tag;
        //    movie = Movies.Find(x => x.Id == id);
        //
        //    //Open Add/Edit form
        //    form = new AddEditMovie(movie);
        //    form.ShowDialog();
        //
        //    //Update movie in list and UI
        //    if (form.DataSaved)
        //    {
        //        index = Movies.FindIndex(x => x.Id == id);
        //        Movies[index].Copy(form.EditedMovie);
        //        UpdateMovieInUI(Movies[index]);
        //    }
        //}
        private void Edit_DoubleClick(object sender, EventArgs e)
        {
            Control c;
            int id;
            News movie;
            AddEditNews form;

            //Get movie using control tag/id
            c = (Control)sender;
            id = (int)c.Tag;
            movie = News.GetMovieFromDB(id);

            //Open Add/Edit form
            form = new AddEditNews(movie);
            form.ShowDialog();

            //Update movie in list and UI
            if (form.DataSaved)
            {
                // Update the movie in the database
                News.UpdateMovieInDB(form.EditedMovie);
                UpdateMovieInUI(form.EditedMovie);
            }
        }
        private void DeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem menuItem;
            ContextMenuStrip menuStrip;
            Control control;
            int id;
            int index = -1;
            News movie;

            //Find selected control
            menuItem = (ToolStripMenuItem)sender;
            menuStrip = (ContextMenuStrip)menuItem.GetCurrentParent();
            control = menuStrip.SourceControl;

            //Get movie from control tag/id
            id = (int)control.Tag;
            movie = Movies.Find(x => x.Id == id);

            //Delete movie from list 
            index = Movies.FindIndex(x => x.Id == id);
            Movies.RemoveAt(index);

            //Delete movie from UI
            DeleteMovieFromUI(movie);
        }

        private void DeleteMovieFromUI(News movie)
        {
            Control panel;
            string name;

            //Find panel 
            name = String.Format("PnlMovie{0}", movie.Id);
            panel = this.Controls.Find(name, true).FirstOrDefault();

            //Remove event handlers
            panel.DoubleClick -= new EventHandler(Edit_DoubleClick);

            foreach (Control c in panel.Controls)
            {
                c.DoubleClick -= new EventHandler(Edit_DoubleClick);
            }

            //Remove panel
            flowLayoutPanel1.Controls.Remove(panel);
            panel.Dispose();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            List<News> moviesFromDB = News.GetAllMovies();

            // Add each movie to the UI
            foreach (News movie in moviesFromDB)
            {
                AddMovieToUI(movie);
            }
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void DeleteToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            ToolStripMenuItem menuItem;
            ContextMenuStrip menuStrip;
            Control control;
            int id;
            int index = -1;
            News movie;

            //Find selected control
            menuItem = (ToolStripMenuItem)sender;
            menuStrip = (ContextMenuStrip)menuItem.GetCurrentParent();
            control = menuStrip.SourceControl;

            //Get movie from control tag/id
            id = (int)control.Tag;
            movie = Movies.Find(x => x.Id == id);

            //Delete movie from list 
            index = Movies.FindIndex(x => x.Id == id);
            Movies.RemoveAt(index);

            //Delete movie from UI
            DeleteMovieFromUI(movie);
        }

        private void projectBtn_Click(object sender, EventArgs e)
        {
            this.Close();
            ProjectForm pr = new ProjectForm();
            pr.Show();
        }
    }
}
