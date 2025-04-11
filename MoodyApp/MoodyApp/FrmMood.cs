using DataAccessLayer.DataAccessObject;
using DataAccessLayer.DataTransferObject;
using MoodyApp.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MoodyApp
{
    public partial class FrmMood : Form
    {
        public FrmMood()
        {
            InitializeComponent();
        }

        public bool moodIsUpadate = false;
        public DateTime dateOfUpdateMood = DateTime.Today;
        DayInformationsDetailsDTO dayInformationsDetails = new DayInformationsDetailsDTO();

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pb5_Click(object sender, EventArgs e)
        {
            SetMoodRating(5);
        }

        private void pb4_Click(object sender, EventArgs e)
        {
            SetMoodRating(4);
        }

        private void pb3_Click(object sender, EventArgs e)
        {
            SetMoodRating(3);
        }

        private void pb2_Click(object sender, EventArgs e)
        {
            SetMoodRating(2);
        }

        private void pb1_Click(object sender, EventArgs e)
        {
            SetMoodRating(1);
        }

        private void SetMoodRating(int rating)
        {
            pb1.BackgroundImage = Resources.ratingNotClick1;
            pb2.BackgroundImage = Resources.ratingNotClick2;
            pb3.BackgroundImage = Resources.ratingNotClick3;
            pb4.BackgroundImage = Resources.ratingNotClick4;
            pb5.BackgroundImage = Resources.ratingNotClick5;

            switch (rating)
            {
                case 1:
                    pb1.BackgroundImage = Resources.ratingClick1;
                    dayInformationsDetails.MoodRate = 1;
                    break;
                case 2:
                    pb1.BackgroundImage = Resources.ratingClick1;
                    pb2.BackgroundImage = Resources.ratingClick2;
                    dayInformationsDetails.MoodRate = 2;
                    break;
                case 3:
                    pb1.BackgroundImage = Resources.ratingClick1;
                    pb2.BackgroundImage = Resources.ratingClick2;
                    pb3.BackgroundImage = Resources.ratingClick3;
                    dayInformationsDetails.MoodRate = 3;
                    break;
                case 4:
                    pb1.BackgroundImage = Resources.ratingClick1;
                    pb2.BackgroundImage = Resources.ratingClick2;
                    pb3.BackgroundImage = Resources.ratingClick3;
                    pb4.BackgroundImage = Resources.ratingClick4;
                    dayInformationsDetails.MoodRate = 4;
                    break;
                case 5:
                    pb1.BackgroundImage = Resources.ratingClick1;
                    pb2.BackgroundImage = Resources.ratingClick2;
                    pb3.BackgroundImage = Resources.ratingClick3;
                    pb4.BackgroundImage = Resources.ratingClick4;
                    pb5.BackgroundImage = Resources.ratingClick5;
                    dayInformationsDetails.MoodRate = 5;
                    break;
            }

            dayInformationsDetails.MoodRate = rating;
        }

        private void FrmMood_Load(object sender, EventArgs e)
        {
            SetLabelsBackground();
            if (moodIsUpadate)
            {
                dateTimePicker1.Value = dateOfUpdateMood;
                dayInformationsDetails = DayInformationsDAO.Get(dateOfUpdateMood.ToString("yyyy/MM/dd"));
                SetMoodRating(dayInformationsDetails.MoodRate);

                CheckBox[] emotionChecks = new CheckBox[]
                {
                    chkEmotion1, chkEmotion2, chkEmotion3, chkEmotion4,
                    chkEmotion5, chkEmotion6, chkEmotion7, chkEmotion8
                };

                List<DayEmotionDTO> dayEmotionDTOList = DayEmotionDAO.GetAll(dateOfUpdateMood.ToString("yyyy/MM/dd"));

                foreach (var checkBoxes in emotionChecks)
                    checkBoxes.Checked = false;

                foreach (var emotion in dayEmotionDTOList)
                    emotionChecks[emotion.EmotionId].Checked = true;

                chkExercise.Checked = dayInformationsDetails.IsExercise;

                cbExercises.SelectedIndex = -1;
                if (dayInformationsDetails.ExerciseName != null && cbExercises.Items.Contains(dayInformationsDetails.ExerciseName) && chkExercise.Checked)
                    cbExercises.SelectedItem = dayInformationsDetails.ExerciseName;
                else
                    MessageBox.Show("Error. Invalid Exercise Name.");
            }
            else
                dayInformationsDetails.MoodRate = 0;
        }

        private void SetLabelsBackground()
        {
            Color semiTransparentColor = Color.FromArgb(80, 255, 255, 255);
            label1.BackColor = semiTransparentColor;
            label2.BackColor = semiTransparentColor;
            label3.BackColor = semiTransparentColor;
            label4.BackColor = semiTransparentColor;
            chkEmotion1.BackColor = semiTransparentColor;
            chkEmotion2.BackColor = semiTransparentColor;
            chkEmotion3.BackColor = semiTransparentColor;
            chkEmotion4.BackColor = semiTransparentColor;
            chkEmotion5.BackColor = semiTransparentColor;
            chkEmotion6.BackColor = semiTransparentColor;
            chkEmotion7.BackColor = semiTransparentColor;
            chkEmotion8.BackColor = semiTransparentColor;
            chkExercise.BackColor = semiTransparentColor;
            pictureBox1.BackColor = semiTransparentColor;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (moodIsUpadate)
            {
                if (dateTimePicker1.Value != dateOfUpdateMood)
                    MessageBox.Show("You can't change the date. Please set the date to: " + dateOfUpdateMood + " or cancel.");
                else
                {
                    dayInformationsDetails.IsExercise = chkExercise.Checked;
                    if (chkExercise.Checked)
                        dayInformationsDetails.ExerciseName = cbExercises.Text;
                    else
                        dayInformationsDetails.ExerciseName = "";

                    DayInformationsDAO.EditDayInformations(dayInformationsDetails);

                    DayEmotionDTO dayEmotionDTO = new DayEmotionDTO();
                    dayEmotionDTO.DayDate = dateTimePicker1.Value;

                    DayEmotionDAO.DeleteAll(dateOfUpdateMood.ToString("yyyy/MM/dd"));

                    CheckBox[] emotionChecks = new CheckBox[]
                    {
                        chkEmotion1, chkEmotion2, chkEmotion3, chkEmotion4,
                        chkEmotion5, chkEmotion6, chkEmotion7, chkEmotion8
                    };

                    for (int i = 0; i < emotionChecks.Length; i++)
                    {
                        if (emotionChecks[i].Checked)
                        {
                            dayEmotionDTO.EmotionId = i;
                            DayEmotionDAO.AddDayEmotion(dayEmotionDTO);
                        }
                    }
                    this.Close();
                }
            }
            else
            {
                if (IsDateAlreadyInDatabase(dateTimePicker1.Value))
                    MessageBox.Show("You already saved your mood for this day. Please change the date");

                else
                {

                    dayInformationsDetails.DayDate = dateTimePicker1.Value;
                    dayInformationsDetails.IsExercise = chkExercise.Checked;
                    if (chkExercise.Checked)
                        dayInformationsDetails.ExerciseName = cbExercises.Text;
                    else
                        dayInformationsDetails.ExerciseName = "";

                    DayInformationsDAO.AddDayInformations(dayInformationsDetails);

                    DayEmotionDTO dayEmotionDTO = new DayEmotionDTO();
                    dayEmotionDTO.DayDate = dateTimePicker1.Value;

                    CheckBox[] emotionChecks = new CheckBox[]
                    {
                        chkEmotion1, chkEmotion2, chkEmotion3, chkEmotion4,
                        chkEmotion5, chkEmotion6, chkEmotion7, chkEmotion8
                    };

                    for (int i = 0; i < emotionChecks.Length; i++)
                    {
                        if (emotionChecks[i].Checked)
                        {
                            dayEmotionDTO.EmotionId = i;
                            DayEmotionDAO.AddDayEmotion(dayEmotionDTO);
                        }
                    }
                    this.Close();
                }
            }
        }

        private bool IsDateAlreadyInDatabase(DateTime value)
        {

            if(DayInformationsDAO.Get(value.ToString("yyyy/MM/dd")).DayDate.ToString("yyyy/MM/dd") == value.ToString("yyyy/MM/dd"))
                return true;
            return false;
        }
    }
}
