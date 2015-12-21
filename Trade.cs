using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;

namespace SuperSimpleStocks
{
    class Trade
    {
        #region BuySell

        public static void BuySell(string pStrTradeTime, string pStrStock, int pIntQuantity, string pStrBuySell, double pDblPrice)
        {
            string strFilePath = Globals.mStrDirectoryName + @"CsvFiles\\" + Globals.mStrCsvFileName;

            // add details to stringbuilder for csv file
            Globals.mSbCsvLines.Append(pStrTradeTime + "," + pStrStock + "," + pIntQuantity.ToString() + "," + pStrBuySell + "," + pDblPrice.ToString()).AppendLine();

            try
            {
                using (StreamWriter swTradeData = new StreamWriter(Globals.mStrDirectoryName + @"CsvFiles\\" + Globals.mStrCsvFileName))
                {
                    try
                    {
                        swTradeData.Write(Globals.mSbCsvLines);
                    }
                    catch (FileNotFoundException FileNotFoundEx)
                    {
                        LogFile.WriteLogFile("Unable to locate CSV file " + Globals.mSbCsvLines + " - Exiting Program");
                        LogFile.WriteLogFile("Details: " + FileNotFoundEx.Message.ToString());
                        LogFile.WriteLogFile("------- Job Completed (with Errors) -------");

                        Environment.Exit(1);
                    }
                    catch (IOException IOEx)
                    {
                        LogFile.WriteLogFile("An IOException occurred writing CSV File " + Globals.mSbCsvLines + " - Exiting Program");
                        LogFile.WriteLogFile("Details: " + IOEx.Message.ToString());
                        LogFile.WriteLogFile("------- Job Completed (with Errors) -------");

                        Environment.Exit(1);
                    }
                    catch (Exception ex)
                    {
                        LogFile.WriteLogFile("An Error Occurred Writing CSV file" + Globals.mSbCsvLines + " - Exiting Program");
                        LogFile.WriteLogFile("Details: " + ex.Message.ToString());
                        LogFile.WriteLogFile("------- Job Completed (with Errors) -------");

                        Environment.Exit(1);
                    }
                }
            }
            catch (IOException IOEx)
            {
                Console.WriteLine("An IOException has occurred updating " + Globals.mStrLogFile + "\nContinuing with logging\nDetails: {0}", IOEx.Message.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine("An Exception has occurred updating " + Globals.mStrLogFile + "\nContinuing without logging\nDetails: {0}", ex.Message.ToString());
            }
        }

        #endregion
    }
}
