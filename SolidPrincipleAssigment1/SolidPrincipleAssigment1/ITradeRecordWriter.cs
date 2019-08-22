using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolidPrincipleAssigment1
{
    interface ITradeRecordWriter
    {
        void WriteTradeRecords(List<TradeRecord> tradeRecords);
    }
}
