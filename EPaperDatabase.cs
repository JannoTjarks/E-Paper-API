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

        public List<EPaper> GetNewestEpaper(CultureInfo cultureInfo) {
            var ePapers = new List<EPaper>();

            var sql = @"SELECT epaper_file_path, epaper_frontpage_path, dt, category_name, name_name
                        FROM NewestEpaper
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

        public List<EPaper> GetAllEPaper(CultureInfo cultureInfo) {
            var ePapers = new List<EPaper>();

            var sql = @"SELECT epaper_file_path, epaper_frontpage_path, dt, category_name, name_name
                        FROM AllEPaper                        
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
        //public String GetEPaperByName(CultureInfo cultureInfo, String name) {
        public List<EPaper> GetEPaperByName(CultureInfo cultureInfo, String name) {
            var ePapers = new List<EPaper>();

            var sql = "SELECT epaper_file_path, epaper_frontpage_path, dt, category_name, name_name FROM AllEPaper WHERE name_name LIKE '%" + name + "%';";

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