using System;

namespace EPaperApi.Model
{
    public class EPaper
    {
        private readonly string _name;
        private readonly DateTime _publicationDate;
        private readonly string _category;
        private readonly string _filePath;
        private readonly string _weekday;
        private readonly string _imagePath;

        public EPaper(string name, string publicationDate, string category, 
                      string filePath, string weekday, string imagePath) {
            _name = name;
            _publicationDate = DateTime.Parse(publicationDate);
            _category = category;
            _filePath = filePath;
            _weekday = weekday;
            _imagePath = imagePath;
        }

        public string Name { get => _name; }
        public DateTime PublicationDate { get => _publicationDate; }
        public string Category { get => _category; }
        public string FilePath { get => _filePath; }
        public string Weekday { get => _weekday; }
        public string ImagePath { get => _imagePath; }
    }
}