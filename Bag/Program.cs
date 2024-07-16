using System.Globalization;
using System.Threading;

namespace BagAssignment
{
    class Program
    {
        static void Main()
        {
            //Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");

            Menu m = new();
            m.Run();
        }
    }
}