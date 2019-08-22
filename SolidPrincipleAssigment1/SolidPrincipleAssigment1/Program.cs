using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolidPrincipleAssigment1
{
    class Program
    {
        static void Main(string[] args)
        {
            Stream path=null;
            TradeProcessor tradeProcessor = new TradeProcessor(new TradeRecordReader(), new TradeRecordWriter());
            tradeProcessor.ProcessTrade(path);
        }
    }
}