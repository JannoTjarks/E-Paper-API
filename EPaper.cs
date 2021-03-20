using System;

namespace EPaperApi
{
    public class EPaper
    {
        private string _name = String.Empty;
        private string _publicationDate = String.Empty;
        private string _category = String.Empty;
        private string _filePath = String.Empty;        
        private string _weekday;
        private string _imagePath;

        EPaper(string name, string publicationDate, string category, 
               string filePath, string weekday, string imagePath) {
            _name = name;
            _publicationDate = publicationDate;
            _category = category;
            _filePath = filePath;
            _weekday = weekday;
            _imagePath = imagePath;
        }

        public string Name { get => _name; }
        public string PublicationDate { get => _publicationDate; }
        public string Category { get => _category; }
        public string FilePath { get => _filePath; }
        public string Weekday { get => _weekday; }
        public string ImagePath { get => _imagePath; }
    }
}