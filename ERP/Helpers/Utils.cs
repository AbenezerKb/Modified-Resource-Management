namespace ERP.Helpers
{
    public class Utils
    {
        public static bool isValidDateRange(DateTime startDate, DateTime endDate)
        {
            return (startDate >= endDate || endDate <= startDate) ? false : true;

        }
        public static Task<List<int>> GetDifference(List<int> sample, List<int> all)
        {
            return Task.FromResult(sample.Where(s => !all.Contains(s)).ToList());
        }
    }

}