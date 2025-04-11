using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DataTransferObject
{
    public class DayInformationsDTO
    {
        public List<DayEmotionDTO> Emotions { get; set; }
        public List<DayInformationsDetailsDTO> DayInformationsDetailsDTOs { get; set; }
    }
}
