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
    public partial class AddEditNews : Form
    {
        private Boolean IsEdit;
        private News OriginalMovie;
        public News EditedMovie;
        public News NewMovie;
        public Boolean DataSaved;
        public AddEditNews()
        {
            InitializeComponent();
            IsEdit = false;
        }



        public AddEditNews(News movie)
        {
            InitializeComponent();
            IsEdit = true;
            OriginalMovie = movie;
        }

        private void AddEditMovie_Load(object sender, EventArgs e)
        {
            DataSaved = false;

            if (IsEdit)
            {
                PopulateOriginalMovie();
                this.Text = "Edit";
            }

            else
            {
                ClearInput();
                this.Text = "Add";
            }
        }

        private void PopulateOriginalMovie()
        {
            TxtTitle.Text = OriginalMovie.Title;
            TxtDescription.Text = OriginalMovie.Description;
            DtpReleaseDate.Text = OriginalMovie.ReleaseDate.ToString();
        }

        private void ClearInput()
        {
            TxtTitle.Clear();
            TxtDescription.Clear();
            DtpReleaseDate.Text = DateTime.Now.ToString();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            List<String> errors;

            errors = ValidateInput();

            if (errors.Count > 0)
            {
                ShowErrors(errors, 5);
                return;
            }

            StoreInput();
            DataSaved = true;
            this.Close();
        }

        private List<string> ValidateInput()
        {
            List<String> errors = new List<string>();

            if (string.IsNullOrWhiteSpace(TxtTitle.Text))
                errors.Add("Title required");

            if (string.IsNullOrWhiteSpace(TxtDescription.Text))
                errors.Add("Description Path required");

            return errors;
        }

        private void StoreInput()
        {
            string title;
            string description;
            DateTime releaseDate;
            int id;

            title = TxtTitle.Text;
            description = TxtDescription.Text;
            releaseDate = DateTime.Parse(DtpReleaseDate.Text.ToString());


            if (IsEdit)
                EditedMovie = new News(OriginalMovie.Id,
                                        title, description, releaseDate);
            else
            {
                id = Convert.ToInt32(DateTime.Now.ToString("ddHHmmss"));
                NewMovie = new News(id, title, description, releaseDate);
            }

        }

        private void ShowErrors(List<string> errors, int max)
        {
            MessageBoxIcon icon;
            MessageBoxButtons buttons;
            string text = null;

            icon = MessageBoxIcon.Error;
            buttons = MessageBoxButtons.OK;

            if (max > errors.Count)
                max = errors.Count;

            for (int i = 0; i < max; i++)
            {
                text += errors[i] + "\n";
            }

            MessageBox.Show(text, "", buttons, icon);
        }

        private void label4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
