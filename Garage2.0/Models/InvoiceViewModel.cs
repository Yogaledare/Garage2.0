namespace Garage2._0.Models
{
    public class InvoiceViewModel
    {
        public  string License;
        public DateTime ArriveD;
        public DateTime LeaveD;
        public InvoiceViewModel(string licenseP, DateTime arriveD)
        {
            License = licenseP;
            ArriveD = arriveD;
            LeaveD = DateTime.Now;
        }
    }
}
