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

        private List<EPaper> GetEpaperFromDatabase(string sql) 
        {
            var ePapers = new List<EPaper>();            

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
                        
                        string weekday = CultureInfo.DefaultThreadCurrentCulture.DateTimeFormat
                                            .GetDayName(Convert.ToDateTime(epaper_date).DayOfWeek);

                        var ePaper = new EPaper(name_name, epaper_date, 
                                                category_name, epaper_file_path,
                                                weekday, epaper_frontpage_path);
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

        public List<String> GetEpaperNames() 
        {
            var names = new List<String>();
            var sql = @"SELECT name_name
                        FROM name
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
                        string name = reader.GetString(0);
                        
                        names.Add(name);
                    }
                }
                finally
                {
                    reader.Close();
                    _connection.Close();
                }            
            }

            return names;
        }

        public List<EPaper> GetNewestEpaper() 
        {
            var sql = @"SELECT epaper_file_path, epaper_frontpage_path, 
                        dt, category_name, name_name
                        FROM NewestEpaper
                        ;";

            return GetEpaperFromDatabase(sql);
        }

        public List<EPaper> GetAllEPaper() 
        {            
            var sql = @"SELECT epaper_file_path, epaper_frontpage_path, dt, 
                        category_name, name_name
                        FROM AllEPaper                        
                        ;";

            return GetEpaperFromDatabase(sql);
        }        
        public List<EPaper> GetEPaperByName(String name, string all) 
        {            
            var sql = String.Empty;

            if(all == "true") 
            {
                sql = @"SELECT epaper_file_path, epaper_frontpage_path, dt, 
                        category_name, name_name
                        FROM AllEPaper WHERE name_name = '" + name + 
                        "' ORDER BY dt DESC;";
            }
            else
            {
                sql = @"SELECT epaper_file_path, epaper_frontpage_path, dt, 
                        category_name, name_name
                        FROM AllEPaper WHERE name_name = '" + name + 
                        "' ORDER BY dt DESC LIMIT 10;";
            }

            return GetEpaperFromDatabase(sql);     
        }
    }
}