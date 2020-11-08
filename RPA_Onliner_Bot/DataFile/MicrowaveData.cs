namespace RPA_Onliner_Bot.DataFile
{
    public class MicrowaveData
    {
        public MicrowaveData(string name, string price, string link)
        {
            this.Name = name;
            this.Price = price;
            this.Link = link;
        }

        public MicrowaveData()
        {
        }

        public string Name { get; set; }

        public string Price { get; set; }

        public string Link { get; set; }
    }
}
