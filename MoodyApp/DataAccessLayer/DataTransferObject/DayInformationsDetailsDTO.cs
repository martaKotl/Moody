using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DataTransferObject
{
    public class DayInformationsDetailsDTO
    {
        public DateTime DayDate { get; set; }
        public int MoodRate { get; set; }
        public Boolean IsExercise { get; set; }
        public string ExerciseName { get; set; }
    }
}
