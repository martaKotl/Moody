using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataAccessLayer.DataAccessObject;
using DataAccessLayer.DataTransferObject;

namespace MoodyApp
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        QuoteDTO quoteDTO = new QuoteDTO();

        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to exit?", "", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
                this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            FrmMood frmMood = new FrmMood();
            this.Hide();
            frmMood.ShowDialog();
            FillAllData();
            this.Visible = true;
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            SetLabelsBackground();
            dataGridView1.AllowUserToAddRows = false;
            FillAllData();
            lblQuote.Left = (this.Width - lblQuote.Width) / 2;
        }

        private void SetLabelsBackground()
        {
            Color semiTransparentColor = Color.FromArgb(80, 255, 255, 255);
            label1.BackColor = semiTransparentColor;
            label2.BackColor = semiTransparentColor;
            label3.BackColor = semiTransparentColor;
            label4.BackColor = semiTransparentColor;
            label5.BackColor = semiTransparentColor;
            label6.BackColor = semiTransparentColor;

        }

        private void FillAllData()
        {
            quoteDTO = QuoteDAO.GetQuote(Convert.ToInt32(DateTime.Now.Day));
            lblQuote.Text = quoteDTO.QuoteName;

            DataView dv = DayInformationsDAO.DisplayInDataGridView();
            dataGridView1.DataSource = dv;

            dataGridView1.Columns[0].HeaderText = "Date";
            dataGridView1.Columns[1].HeaderText = "Mood Rate (out of 5)";
            dataGridView1.Columns[2].HeaderText = "I did exercise";
            if ((dataGridView1.Rows.Count > 0) && (dataGridView1.Rows[0].Cells[2].Value.Equals(false)))
            {
                dataGridView1.Columns[3].Visible = false;
            }
            else 
                dataGridView1.Columns[3].HeaderText = "Exercise Name";
        }

        private void btnEditTodaysMood_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count <= 0)
                MessageBox.Show("You didn't add your mood today yet");
            else
            {
                FrmMood frmMood = new FrmMood();
                frmMood.moodIsUpadate = true;
                this.Hide();
                frmMood.ShowDialog();
                this.Visible = true;
            }
        }

        private void btnHistory_Click(object sender, EventArgs e)
        {
            FrmDaysHistory frmDaysHistory = new FrmDaysHistory();
            this.Hide();
            frmDaysHistory.ShowDialog();
            FillAllData();
            this.Visible = true;
        }
    }
}
