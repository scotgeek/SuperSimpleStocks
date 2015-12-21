using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SuperSimpleStocks
{
    class PE_Ratio
    {
        #region CalculatePE_Ratio

        public static string CalculatePE_Ratio(string pStrStockSymbol, int pIntLastDividend)
        {
            if (pIntLastDividend != 0)
            {
                // get the ticker price from the StockSymbol parameter
                switch (pStrStockSymbol.ToUpper())
                {
                    case "TEA":
                        Globals.mDecTickerPrice = Convert.ToDecimal(Globals.mStrTEATicker);
                        break;
                    case "POP":
                        Globals.mDecTickerPrice = Convert.ToDecimal(Globals.mStrPOPTicker);
                        break;
                    case "ALE":
                        Globals.mDecTickerPrice = Convert.ToDecimal(Globals.mStrALETicker);
                        break;
                    case "GIN":
                        Globals.mDecTickerPrice = Convert.ToDecimal(Globals.mStrGINTicker);
                        break;
                    case "JOE":
                        Globals.mDecTickerPrice = Convert.ToDecimal(Globals.mStrJOETicker);
                        break;
                    default:
                        LogFile.WriteLogFile("Invalid stock symbol (" + pStrStockSymbol + ")  - unable to obtain ticker price - Exiting Program");
                        LogFile.WriteLogFile("------- Job Completed (with Errors) -------");

                        Environment.Exit(1);
                        break;
                }

                // P/E Ratio = Ticker Price / Last Dividend
                Globals.mDecPE_Ratio = (Globals.mDecTickerPrice / pIntLastDividend);

                return Globals.mDecPE_Ratio.ToString("####.##");
            }
            else
            {
                return "0";
            }
        }

        #endregion
    }
}
