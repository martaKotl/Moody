using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.DataTransferObject;

namespace DataAccessLayer.DataAccessObject
{
    public class QuoteDAO
    {
        public static QuoteDTO GetQuote(int quoteId)
        {
            QuoteDTO quote = new QuoteDTO();
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT [QuoteId], [QName] FROM [dbo].[Quotes] " +
                        "WHERE [QuoteId] = @quoteId";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@quoteId", quoteId);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                quote.QuoteId = reader.GetInt32(0);
                                quote.QuoteName = reader.GetString(1);
                            }
                        }
                    }

                    return quote;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}
