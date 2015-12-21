using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;

namespace SuperSimpleStocks
{
    // Super Simple Stocks Homework
    // Stewart Campbell - Dec 2015
    
    // Assumptions / Notes : See ReadMe.txt

    class Program
    {
        static void Main(string[] args)
        {
            // lets start
            LogFile.WriteLogFile("------- Job Started -------");

            #region HouseKeeping

            // check the size of the log file
            LogFile.CheckLogFileSize();

            #endregion

            #region Check Parameter File

            // get data from filename in argument
            if (args.Length < 1)
            {
                LogFile.WriteLogFile("No filename given  - Exiting Program");
                LogFile.WriteLogFile("------- Job Completed (With Errors) -------");

                Environment.Exit(1);
            }
            else
            {
                // we have parameter filename
                Globals.mStrParameterFile = args[0].ToString();

                // build full name with directory
                Globals.mStrStocksFile = Globals.mStrDirectoryName + Globals.mStrParameterFile;

                // check if file exists
                try
                {
                    using (var varStocksFile = new FileStream(Globals.mStrStocksFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    {
                        LogFile.WriteLogFile("Parameter file supplied - " + Globals.mStrParameterFile + " - Continuing");
                    }
                }
                catch (FileNotFoundException FileNotFoundEx)
                {
                    // no file found
                    LogFile.WriteLogFile("Unable to find file " + Globals.mStrEarningsFile + " - Exiting Program");
                    LogFile.WriteLogFile("Details: " + FileNotFoundEx.Message.ToString());
                    LogFile.WriteLogFile("------- Job Completed (With Errors) -------");

                    Environment.Exit(1);
                }
                catch (IOException IOEx)
                {
                    // read error
                    LogFile.WriteLogFile("Unable to read file " + Globals.mStrEarningsFile + " - Exiting Program");
                    LogFile.WriteLogFile("Details: " + IOEx.Message.ToString());
                    LogFile.WriteLogFile("------- Job Completed (With Errors) -------");

                    Environment.Exit(1);
                }
                catch (Exception ex)
                {
                    LogFile.WriteLogFile("An exception has occurred");
                    LogFile.WriteLogFile("Details: " + ex.Message.ToString());
                    LogFile.WriteLogFile("------- Job Completed (With Errors) -------");

                    Environment.Exit(1);
                }
            }

            #endregion

            #region Calculate Yields and Ratios

            YieldsAndRatios.CalculateYieldsAndRatios();

            #endregion

            #region Buy/Sell Trade

            // add header line to stringbuilder for csv file
            Globals.mSbCsvLines.Append("Date/Time,Stock Symbol,Quantity,Buy/Sell,Price").AppendLine();

            // craete dummy transactions with random quantities and prices toggling BUY and SELL for Stock Price calculation later
            for (int i = 1; i <= 25; i++)
            {
                int intQuantity = 0;
                double dblPrice = 0.00;

                if (i % 2 != 0)
                {
                    string strTransaction = "BUY";
                    intQuantity = new Random().Next(1, 1000);
                    dblPrice = (new Random().NextDouble() * 100);

                    BuySell.BuyAndSell("JPM", intQuantity, strTransaction, Math.Round(dblPrice, 4));
                }
                else
                {
                    string strTransaction = "SELL";
                    intQuantity = new Random().Next(1, 1000);
                    dblPrice = (new Random().NextDouble() * 100);

                    BuySell.BuyAndSell("JPM", intQuantity, strTransaction, Math.Round(dblPrice, 4));
                }
            }

            #endregion

            #region Get Trades

            string strStockPrice = StockPrice.GetTrades().ToString();

            if (!String.IsNullOrEmpty(strStockPrice))
            {
                    // display the stock price
                    Console.WriteLine("Stock Price is {0} ", strStockPrice);
                    Console.WriteLine("");
            }
            else
            {
                LogFile.WriteLogFile("A NULL or empty string returned for Stock Price");
            }

            #endregion

            #region Geometric Mean

            // get geometric mean
            Globals.mDblGeometricMean = GeometricMean.CalculateGeometricMean();

            // display the geometric mean
            Console.WriteLine("Geometric Mean is {0} ", Globals.mDblGeometricMean.ToString() + "%");

            if (Globals.mDebug)
            {
                Console.WriteLine("");
                Console.WriteLine("Press Any Key to Continue");
                Console.ReadKey();
            }

            #endregion

            LogFile.WriteLogFile("------- Job Completed -------");
        }
    }
}
