namespace Furlaneti.Finance.ChangeBase.Model
{
    public class ValuationIndicators
    {
        public string Symbol { get; set; }
        public decimal Price { get; set; }
        public decimal EvEbitda { get; set; }
        public decimal DividendYield { get; set; }
        public decimal PriceProfit { get; set; }
        public decimal PriceOverAssetValue { get; set; }
        public decimal ReturnOnEquity { get; set; }
        public decimal DlEbitda { get; set; }
        public decimal CompoundAnnualGrowthRate { get; set; }
        public decimal EarningsPerShare { get; set; }
        public decimal EquityValuePerShare { get; set; }

    }
}
