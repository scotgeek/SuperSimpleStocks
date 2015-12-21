using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SuperSimpleStocks
{
    class BuySell
    {
        #region BuyAndSell

        public static void BuyAndSell(string pStrStockSymbol, int pIntQuantity, string pStrTransaction, double pDblPrice)
        {
            // write log file
            LogFile.WriteLogFile("Running BUY/SELL Transactions");

            try
            {
                Trade.BuySell(DateTime.Now.AddMinutes(new Random().Next(-50, -1)).ToString(Globals.mStrLogFileTimeStamp), pStrStockSymbol, pIntQuantity, pStrTransaction, pDblPrice);

                // log results
                switch (pStrTransaction.ToUpper())
                {
                    case "BUY":
                        Globals.mStrTransType = "BOUGHT";
                        break;
                    case "SELL":
                        Globals.mStrTransType = "SOLD";
                        break;
                    default:
                        LogFile.WriteLogFile("Invalid transaction type (" + pStrTransaction + ") - Exiting Program");
                        LogFile.WriteLogFile("------- Job Completed (with Errors) -------");

                        Environment.Exit(1);
                        break;
                }

                LogFile.WriteLogFile(Globals.mStrTransType + " " + pIntQuantity + " Shares of " + pStrStockSymbol + " at " + pDblPrice);
            }
            catch (Exception BuySellException)
            {
                // log results
                LogFile.WriteLogFile("An error occurred running Buy/Sell transaction. Details: " + BuySellException.Message.ToString() + " - Exiting Program");
                LogFile.WriteLogFile("------- Job Completed (with Errors) -------");

                Environment.Exit(1);
            }
        }

        #endregion
    }
}
