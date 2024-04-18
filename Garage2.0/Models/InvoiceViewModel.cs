namespace Garage2._0.Models
{
    public class InvoiceViewModel
    {
        public  string License;
        public DateTime ArriveD;
        public DateTime LeaveD;
        public double TotalHours;
        public int Price;
        public InvoiceViewModel(string licenseP, DateTime arriveD)
        {
            License = licenseP;
            ArriveD = arriveD;
            LeaveD = DateTime.Now;
            TotalHours = (LeaveD - arriveD).TotalHours;
            Price = (int)Math.Round(TotalHours)*15;
        }
    }
}
    