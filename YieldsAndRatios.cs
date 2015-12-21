using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;

namespace SuperSimpleStocks
{
    class YieldsAndRatios
    {
        #region CalculateYieldsAndRatios

        public static void CalculateYieldsAndRatios()
        {
            // write log file
            LogFile.WriteLogFile("Calculating Yields and Ratios");

            // build the complete filename for source data
            Globals.mStrStocksFile = Globals.mStrDirectoryName + Globals.mStrParameterFile;

            try
            {
                using (var varStocksFile = new FileStream(Globals.mStrStocksFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    LogFile.WriteLogFile("Opening file " + Globals.mStrStocksFile);

                    string[] strAllLines = File.ReadAllLines(Globals.mStrStocksFile);

                    int intColumns = strAllLines.Count();

                    if (intColumns == 6)
                    {
                        // skip first header line - Stock Symbol,Type,Last Dividend,Fixed Dividend,Par Value
                        var query = from line in strAllLines.Skip(1)
                                    let data = line.Split(',')

                                    select new
                                    {
                                        // use the same names as in csv columns
                                        StockSymbol = data[0],
                                        Type = data[1],
                                        LastDividend = data[2],
                                        FixedDividend = data[3],
                                        ParValue = data[4],
                                    };

                        foreach (var s in query)
                        {
                            
                            // this matches the order from the file
                            Console.WriteLine("{0} {1} {2} {3} {4}", s.StockSymbol, s.Type, s.LastDividend, s.FixedDividend, s.ParValue);
                            
                            // get the dividend yield
                            string strDividendYield = DividendYield.CalculateDividendYield(s.StockSymbol, s.Type, Convert.ToDecimal(s.LastDividend), s.FixedDividend, Convert.ToDecimal(s.ParValue));

                            // log results
                            LogFile.WriteLogFile(s.Type + " Dividend Yield for " + s.StockSymbol + " is " + strDividendYield);
                            
                            // display the dividend yield results
                            Console.WriteLine("{0} Dividend Yield for {1} is {2} ", s.Type, s.StockSymbol, strDividendYield);

                            // get the P/E Ratio
                            string strPE_Ratio = PE_Ratio.CalculatePE_Ratio(s.StockSymbol, Convert.ToInt32(s.LastDividend));

                            // log results
                            LogFile.WriteLogFile("P/E Ratio for " + s.StockSymbol + " is " + strPE_Ratio);

                            // display the P/E Ratio results
                            Console.WriteLine("P/E Ratio for {0} is {1} ", s.StockSymbol, strPE_Ratio);
                            Console.WriteLine("");
                        }
                    }
                    else
                    {
                        LogFile.WriteLogFile("Too many columns in source file  - Exiting Program");
                        LogFile.WriteLogFile("------- Job Completed (with Errors) -------");

                        Environment.Exit(1);
                    }
                }
            }
            catch (FileNotFoundException FileNotFoundEx)
            {
                // log results
                LogFile.WriteLogFile("File " + Globals.mStrStocksFile + " not found - Exiting Program");
                LogFile.WriteLogFile("Details: " + FileNotFoundEx.Message.ToString());
                LogFile.WriteLogFile("------- Job Completed (with Errors) -------");

                Environment.Exit(1);
            }
            catch (IOException IOEx)
            {
                // log results
                LogFile.WriteLogFile("An IOException occured with file " + Globals.mStrStocksFile + " - Exiting Program");
                LogFile.WriteLogFile("Details: " + IOEx.Message.ToString());
                LogFile.WriteLogFile("------- Job Completed (with Errors) -------");

                Environment.Exit(1);
            }
            catch (Exception ex)
            {
                // log results
                LogFile.WriteLogFile("An error occurred - - Exiting Program");
                LogFile.WriteLogFile("Details: " + ex.Message.ToString());
                LogFile.WriteLogFile("------- Job Completed (with Errors) -------");

                Environment.Exit(1);
            }
        }

        #endregion
    }
}
