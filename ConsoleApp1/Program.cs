using System.Threading;

namespace getCurrentWx
{
    class Program
    {
        static void Main(string[] args)
        {
            var CallWx = new CallWXApi("Detroit");

            while (CallWx.ProcessComplete == false)
            {
                Thread.Sleep(100);
            }
        }     
    }
}
