using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolidPrincipleAssigment1
{
    class TradeProcessor
    {
        private ITradeRecordReader tradeRecordReader;
        private ITradeRecordWriter tradeRecordWriter;
        public TradeProcessor(ITradeRecordReader tradeRecordReader, ITradeRecordWriter tradeRecordWriter)
        {
            this.TradeRecordReader = tradeRecordReader;
            this.TradeRecordWriter = tradeRecordWriter;
        }
        internal ITradeRecordReader TradeRecordReader { get => tradeRecordReader; set => tradeRecordReader = value; }
        internal ITradeRecordWriter TradeRecordWriter { get => tradeRecordWriter; set => tradeRecordWriter = value; }

        public void ProcessTrade(Stream stream)
        {
            List<string> lines = this.TradeRecordReader.GetTradeRecords(stream);

            var trades = new List<TradeRecord>();
            var lineCount = 1;
            foreach (var line in lines)
            {
                var fields = line.Split(new char[] { ',' });

                if (fields.Length != 3)
                {
                    Console.WriteLine("WARN: Line {0} malformed. Only {1} field(s) found.", lineCount, fields.Length);
                    continue;
                }

                if (fields[0].Length != 6)
                {
                    Console.WriteLine("WARN: Trade currencies on line {0} malformed: '{1}'", lineCount, fields[0]);
                    continue;
                }

                int tradeAmount;
                if (!int.TryParse(fields[1], out tradeAmount))
                {
                    Console.WriteLine("WARN: Trade amount on line {0} not a valid integer:'{1}'", lineCount, fields[1]);
                }

                decimal tradePrice;
                if (!decimal.TryParse(fields[2], out tradePrice))
                {
                    Console.WriteLine("WARN: Trade price on line {0} not a valid decimal:'{1}'", lineCount, fields[2]);
                }

                var sourceCurrencyCode = fields[0].Substring(0, 3);
                var destinationCurrencyCode = fields[0].Substring(3, 3);
                var trade = new TradeRecord
                {
                    SourceCurrency = sourceCurrencyCode,
                    DestinationCurrency = destinationCurrencyCode,
                    Lots = tradeAmount / LotSize,
                    Price = tradePrice
                };
                trades.Add(trade);
                lineCount++;
            }
            this.TradeRecordWriter.WriteTradeRecords(trades);
         }
        private static float LotSize = 100000f;

    }
}
