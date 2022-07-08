

namespace HiFiRadio
{
    public class RadioStation
    {
        public RadioStation()
        {
            Id = new System.Random().Next(1, 10000); // generate new random Id!
        }
        public int Id { get; set; }
        public string StationName { get; set; }
        public string StationUrl { get; set; }

    }
}