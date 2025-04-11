using DataAccessLayer.DataTransferObject;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DataAccessLayer.DataAccessObject
{
    public class DayInformationsDAO
    {
        public static void AddDayInformations(DayInformationsDetailsDTO dayInformationsDetails)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "INSERT INTO [dbo].[DayInformations] ([DayDate],[MoodRate],[IsExercise],[ExerciseName]) " +
                        "VALUES (@date, @moodRate, @isExercise, @exerciseName)";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@date", dayInformationsDetails.DayDate);
                        command.Parameters.AddWithValue("@moodRate", dayInformationsDetails.MoodRate);
                        command.Parameters.AddWithValue("@isExercise", dayInformationsDetails.IsExercise);
                        command.Parameters.AddWithValue("@exerciseName", dayInformationsDetails.ExerciseName);
                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public static void DeleteDayInformation(string date)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "DELETE FROM [dbo].[DayInformations] WHERE [DayDate] = @date";
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

        public static DataView DisplayAllInDataGridView()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            string query = @"SELECT 
                di.DayDate,
                di.MoodRate,
                di.IsExercise,
                di.ExerciseName,
                STRING_AGG(e.EmotionName, ', ') AS Emotions
            FROM DayInformations di
            LEFT JOIN DayEmotions de ON di.DayDate = de.DayDate
            LEFT JOIN Emotions e ON de.EmotionId = e.EmotionId
            GROUP BY di.DayDate, di.MoodRate, di.IsExercise, di.ExerciseName";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataSet set = new DataSet();

                        adapter.Fill(set, "Items");
                        DataView dv = set.Tables["Items"].DefaultView;
                        return dv;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public static DataView DisplayInDataGridView()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            string query = @"SELECT 
                di.DayDate,
                di.MoodRate,
                di.IsExercise,
                di.ExerciseName,
                STRING_AGG(e.EmotionName, ', ') AS Emotions
            FROM DayInformations di
            LEFT JOIN DayEmotions de ON di.DayDate = de.DayDate
            LEFT JOIN Emotions e ON de.EmotionId = e.EmotionId
            WHERE di.DayDate = @date
            GROUP BY di.DayDate, di.MoodRate, di.IsExercise, di.ExerciseName";
            
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@date", DateTime.Today.ToString("yyyy/MM/dd"));
                       
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataSet set = new DataSet();

                        adapter.Fill(set, "Items");
                        DataView dv = set.Tables["Items"].DefaultView;
                        return dv;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public static void EditDayInformations(DayInformationsDetailsDTO dayInformationsDetails)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "UPDATE [dbo].[DayInformations] SET [MoodRate] = @moodRate, [IsExercise] = @isExercise, " +
                        "[ExerciseName] = @exerciseName WHERE [DayDate] = @date";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@date", dayInformationsDetails.DayDate);
                        command.Parameters.AddWithValue("@moodRate", dayInformationsDetails.MoodRate);
                        command.Parameters.AddWithValue("@isExercise", dayInformationsDetails.IsExercise);
                        command.Parameters.AddWithValue("@exerciseName", dayInformationsDetails.ExerciseName);
                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public static DayInformationsDetailsDTO Get(string date)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT [DayDate],[MoodRate],[IsExercise],[ExerciseName] FROM DayInformations WHERE [DayDate] = @date";
                    DayInformationsDetailsDTO dayDetail = new DayInformationsDetailsDTO();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@date", date);
                        object result = command.ExecuteScalar();
                        if (result != null)
                        {

                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    dayDetail.DayDate = reader.GetDateTime(0);
                                    dayDetail.MoodRate = reader.GetInt32(1);
                                    dayDetail.IsExercise = reader.IsDBNull(2) ? false : reader.GetBoolean(2);
                                    dayDetail.ExerciseName = reader.IsDBNull(2) ? null : reader.GetString(3);

                                }
                            }
                        }
                        else
                            return dayDetail;
                    }

                    return dayDetail;
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
        }
    }
}
