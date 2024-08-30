using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Furlaneti.Finance.ChangeBase.Model
{
    public class ValuationIndicators
    {
        public string Symbol { get; set; }
        public string Price { get; set; }
        public string DividendYield { get; set; }
        public string PriceProfit { get; set; }
        public string PriceOverAssetValue { get; set; }
        public string ReturnOnEquity { get; set; }
    }
}
