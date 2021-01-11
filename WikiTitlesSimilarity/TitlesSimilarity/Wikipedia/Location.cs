namespace TitlesSimilarity.Wikipedia
{
    struct Location
    {
        private double latitude, longitude;

        public double Latitude
        {
            get => latitude;
            set => latitude = value % 90;
        }

        public double Longitude
        {
            get => longitude;
            set => longitude = value % 180;
        }

        public uint Radius { get; set; }
        public string Name { get; set; }

        public string WikiQueryCoordinates => $"{Latitude}|{Longitude}";
    }
}
