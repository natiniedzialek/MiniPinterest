using MiniPinterest.Web.Helpers.Abstract;
using MiniPinterest.Web.Models.Domain;

namespace MiniPinterest.Web.Helpers
{
    public class PinHelper : IPinHelper
    {
        public List<Pin> GetPublicPins(IEnumerable<Pin> pins)
        {
            if (pins == null)
                return new List<Pin>();

            return pins.Where(x => x.IsPublic).ToList();
        }

        public List<List<Pin>>? DivideIntoColumns(IEnumerable<Pin> pins, int colNumber)
        {
            List<List<Pin>>? cols = null;

            if (pins != null && pins.Any())
            {
                cols = new();

                for (int i = 0; i < colNumber; i++)
                {
                    var col = pins.Where((x, j) => j % 5 == i).ToList();
                    cols.Add(col);
                }
            }

            return cols;
        }
    }
}
