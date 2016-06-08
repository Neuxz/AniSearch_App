using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AniSearch_App.Console
{
    class Program
    {
        private static ApiRead apr = new ApiRead();
        static void Main(string[] args)
        {
            if(args.Length > 0)
            {
                apr.Search(args[0], ApiRead.SearchKit.anime);
            }
            apr.Search(System.Console.ReadLine(), ApiRead.SearchKit.anime).ContinueWith(a => System.Console.Write(a));
        }
    }
}
