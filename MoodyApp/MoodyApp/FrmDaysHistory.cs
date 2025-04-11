using DataAccessLayer.DataAccessObject;
using DataAccessLayer.DataTransferObject;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MoodyApp
{
    public partial class FrmDaysHistory : Form
    {
        public FrmDaysHistory()
        {
            InitializeComponent();
        }

        DayInformationsDetailsDTO dayInformationsDetailsDTO = new DayInformationsDetailsDTO();

        private void History_Load(object sender, EventArgs e)
        {
            dataGridView1.AllowUserToAddRows = false;
            DisplayDataInDatabase();
        }

        private void DisplayDataInDatabase()
        {
            DataView dv = DayInformationsDAO.DisplayAllInDataGridView();
            dataGridView1.DataSource = dv;

            dataGridView1.Columns[0].HeaderText = "Date";
            dataGridView1.Columns[1].HeaderText = "Mood Rate (out of 5)";
            dataGridView1.Columns[2].HeaderText = "I did exercise";
            dataGridView1.Columns[3].HeaderText = "Exercise Name";
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
                updateCurrentDayInformations();
            if (dayInformationsDetailsDTO.DayDate == DateTime.MinValue)
                MessageBox.Show("Choose transaction from the list");
            else
            {
                FrmMood frmMood = new FrmMood();
                frmMood.moodIsUpadate = true;
                frmMood.dateOfUpdateMood = dayInformationsDetailsDTO.DayDate;
                this.Hide();
                frmMood.ShowDialog();
                this.Visible = true;
                DisplayDataInDatabase();
            }
        }

        private void updateCurrentDayInformations()
        {
            int rowIndex = dataGridView1.CurrentCell.RowIndex;
            var selectedRow = dataGridView1.Rows[rowIndex];
            dayInformationsDetailsDTO.DayDate = Convert.ToDateTime(selectedRow.Cells["DayDate"].Value);
            dayInformationsDetailsDTO.MoodRate = Convert.ToInt32(selectedRow.Cells["MoodRate"].Value);
            dayInformationsDetailsDTO.IsExercise = Convert.ToBoolean(selectedRow.Cells["IsExercise"].Value);
            dayInformationsDetailsDTO.ExerciseName = selectedRow.Cells["ExerciseName"].Value.ToString();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if(dataGridView1.Rows.Count > 0)
                updateCurrentDayInformations();
            if (dayInformationsDetailsDTO.DayDate == DateTime.MinValue)
                MessageBox.Show("Choose transaction from the list");
            else
            {
                DayEmotionDAO.DeleteAll(dayInformationsDetailsDTO.DayDate.ToString("yyyy/MM/dd"));
                DayInformationsDAO.DeleteDayInformation(dayInformationsDetailsDTO.DayDate.ToString("yyyy/MM/dd"));
                DisplayDataInDatabase();
            }
        }
    }
}
