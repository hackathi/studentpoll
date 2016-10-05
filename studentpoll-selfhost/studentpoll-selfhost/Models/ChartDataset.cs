namespace studentpoll.Models
{
    public class ChartDataset
    {
        public string label { get; set; }
        public string[] backgroundColor { get; set; }
        public string[] borderColor { get; set; }
        public int borderWidth { get; set; }
        public int[] data { get; set; }
    }
}