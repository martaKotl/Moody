using DataAccessLayer.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DataAccessObject
{
    public class DayEmotionDAO
    {
        public static void AddDayEmotion(DayEmotionDTO dayEmotionDTO)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "INSERT INTO [dbo].[DayEmotions] ([DayDate],[EmotionId]) " +
                        "VALUES (@date, @emotionId)";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@date", dayEmotionDTO.DayDate);
                        command.Parameters.AddWithValue("@emotionId", dayEmotionDTO.EmotionId);
                        
                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public static void DeleteAll(string date)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "DELETE FROM [dbo].[DayEmotions] WHERE [DayDate] = @date";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@date", date);

                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public static List<DayEmotionDTO> GetAll(string date)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            List<DayEmotionDTO> dayEmotionsList = new List<DayEmotionDTO>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = " SELECT [DayDate],[EmotionId] FROM [Moody].[dbo].[DayEmotions] WHERE [DayDate] = @date";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@date", date);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                DayEmotionDTO emotion = new DayEmotionDTO
                                {
                                    DayDate = reader.GetDateTime(0),
                                    EmotionId = reader.GetInt32(1)
                                };
                                dayEmotionsList.Add(emotion);
                            }
                        }
                    }

                    return dayEmotionsList;
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
        }
    }
}
