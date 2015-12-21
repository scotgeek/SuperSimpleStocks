using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Configuration;

namespace SuperSimpleStocks
{
    class DividendYield
    {
        #region CalculateDividendYield

        public static string CalculateDividendYield(string pStrStockSymbol, string pStrType, decimal pDecLastDividend, string pStrFixedDividend, decimal pDecParValue)
        {
            if (pDecLastDividend != 0)
            {
                if (pStrType == "Common")
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
                            LogFile.WriteLogFile("Invalid stock symbol (" + pStrStockSymbol + ")  - Unable to obtain ticker price - Exiting Program");
                            LogFile.WriteLogFile("------- Job Completed (With Errors) -------");

                            Environment.Exit(1);
                            break;
                    }

                    // dividend yield = Last Dividend / Ticker Price
                    Globals.mDecDividendYield = (pDecLastDividend / Globals.mDecTickerPrice);

                    return Globals.mDecDividendYield.ToString("######.####");
                }
                else // we have a preferred type
                {
                    // dividend yield = fixed dividend (as %) . Par Value / Ticker Price
                    Globals.mDecDividendYield = ((pDecParValue / 100) * Convert.ToDecimal(pStrFixedDividend));

                    return Globals.mDecDividendYield.ToString("######.####");
                }
            }
            else
            {
                return "0";
            }
        }

        #endregion
    }
}
