using MiniPinterest.Web.Models.Domain;

namespace MiniPinterest.Web.Helpers.Abstract
{
    public interface IPinHelper
    {
        List<Pin> GetPublicPins(IEnumerable<Pin> pins);
        List<List<Pin>>? DivideIntoColumns(IEnumerable<Pin> pins, int colNumber);
    }
}
