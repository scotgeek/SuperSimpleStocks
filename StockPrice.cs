using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;

namespace SuperSimpleStocks
{
    class StockPrice
    {
        #region GetTrades

        public static string GetTrades()
        {
            // write log file
            LogFile.WriteLogFile("Calculating Stock Price");

            try
            {
                using (var varCsvFile = new FileStream(Globals.mStrDirectoryName + @"CsvFiles\\" + Globals.mStrCsvFileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    LogFile.WriteLogFile("File " + Globals.mStrDirectoryName + @"CsvFiles\\" + Globals.mStrCsvFileName + " Found - Continuing");

                    using (BufferedStream bsCsvFile = new BufferedStream(varCsvFile))
                    using (StreamReader srCsvFile = new StreamReader(bsCsvFile))
                    {
                        string strLine;
                        string[] strRow;

                        while ((strLine = srCsvFile.ReadLine()) != null)
                        {
                            strRow = strLine.Split(',');

                            // check to see if first element contains text 'Stock Symbol' - i.e. header line
                            if (strRow[0].ToString() != "Date/Time")
                            {
                                // check the date/time - only interested in past 15 minutes
                                TimeSpan tsStartTime = DateTime.Parse(DateTime.Now.ToString()).Subtract(DateTime.Parse(strRow[0]));
                                TimeSpan tsInterval = new TimeSpan(0, 0, 15, 0, 0);

                                if (tsStartTime <= tsInterval)
                                {
                                    LogFile.WriteLogFile("Stock Price record selected with a Date/Time of " + strRow[0].ToString());

                                    if (strRow[1].ToString() == "JPM")
                                    {
                                        Globals.mDblSigmaTradePriceTimesQuantity += Convert.ToInt32(strRow[2].ToString()) * Convert.ToDouble(strRow[4].ToString());
                                        Globals.mIntSigmaQuantity += Convert.ToInt32(strRow[2].ToString());
                                    }
                                }
                            }
                        }

                        // do the calculation 
                        Globals.mDblStockPrice = (Globals.mDblSigmaTradePriceTimesQuantity / Globals.mIntSigmaQuantity);

                        return Globals.mDblStockPrice.ToString("######.####");
                    }
                }
            }
            catch (FileNotFoundException FileNotFoundEx)
            {
                // no file found
                LogFile.WriteLogFile("Unable to find file " + Globals.mStrDirectoryName + @"CsvFiles\\" + Globals.mStrCsvFileName + " - Exiting Program");
                LogFile.WriteLogFile("Details: " + FileNotFoundEx.Message.ToString());
                LogFile.WriteLogFile("------- Job Completed (with Errors) -------");

                Environment.Exit(1);
            }
            catch (IOException IOEx)
            {
                // file read error
                LogFile.WriteLogFile("Unable to read file " + Globals.mStrDirectoryName + @"CsvFiles\\" + Globals.mStrCsvFileName + " - Exiting Program");
                LogFile.WriteLogFile("Details: " + IOEx.Message.ToString());
                LogFile.WriteLogFile("------- Job Completed (with Errors) -------");

                Environment.Exit(1);
            }
            catch (Exception ex)
            {
                LogFile.WriteLogFile("An exception has occurred");
                LogFile.WriteLogFile("Details: " + ex.Message.ToString());
                LogFile.WriteLogFile("------- Job Completed (with Errors) -------");

                Environment.Exit(1);
            }

            return null;
        }

        #endregion
    }
}
