using System.Collections.Generic;
using EPaperApi.Model;
using MySql.Data.MySqlClient;
using System.Globalization;
using System;

namespace EPaperSammlung 
{
    public class EPaperDatabase 
    {
        private readonly MySqlConnection _connection;
        
        public EPaperDatabase(string connectionString) 
        {
            this._connection = new MySqlConnection(connectionString);
        }

        public List<EPaper> GetAllEPaper(CultureInfo cultureInfo) {
            var ePapers = new List<EPaper>();

            var sql = @"SELECT epaper_file_path, epaper_frontpage_path, epaper_date, category_name, name_name
                        FROM epaper
                        INNER JOIN category 
                            ON epaper.epaper_category_id = category.category_id
                        INNER JOIN name 
                            ON epaper.epaper_name_id = name.name_id
                        ;";

            using (var command = new MySqlCommand(sql,_connection))
            {                
                _connection.Open();                                
                
                MySqlDataReader reader;
                reader = command.ExecuteReader();
                try
                {
                    while(reader.Read())
                    {
                        string epaper_file_path = reader.GetString(0);
                        string epaper_frontpage_path = reader.GetString(1);
                        string epaper_date = reader.GetString(2);
                        string category_name = reader.GetString(3);
                        string name_name = reader.GetString(4);

                        CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
                        CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;   
                        string weekday = CultureInfo.DefaultThreadCurrentCulture.DateTimeFormat.GetDayName(Convert.ToDateTime(epaper_date).DayOfWeek);

                        var ePaper = new EPaper(name_name, epaper_date, category_name, epaper_file_path, weekday, epaper_frontpage_path);
                        ePapers.Add(ePaper);
                    }
                }
                finally
                {
                    reader.Close();
                    _connection.Close();
                }            
            }

            return ePapers;
        }
    }
}