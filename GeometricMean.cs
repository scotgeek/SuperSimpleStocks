using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;
using System.Collections;

namespace SuperSimpleStocks
{
    class GeometricMean
    {
        #region CalculateGeometricMean

        public static double CalculateGeometricMean()
        {
            // write log file
            LogFile.WriteLogFile("Calculating Geometric Mean");

            // build the complete filename for source data
            Globals.mStrEarningsFile = Globals.mStrDirectoryName + @"\CsvFiles\Earnings.csv";

            try
            {
                using (var varEarningsFile = new FileStream(Globals.mStrEarningsFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    LogFile.WriteLogFile("File " + Globals.mStrEarningsFile + " found - Continuing");

                    using (BufferedStream bsEarningsFile = new BufferedStream(varEarningsFile))
                    using (StreamReader srEarningsFile = new StreamReader(bsEarningsFile))
                    {
                        string strLine;
                        string[] strRow;

                        while ((strLine = srEarningsFile.ReadLine()) != null)
                        {
                            strRow = strLine.Split(',');

                            if (strRow[0].ToString() != "Year1")
                            {
                                for (int i = 0; i <= strRow.Length - 1; i++)
                                {
                                    Globals.mAlEarnings.Add(Convert.ToDouble(strRow[i].ToString()));

                                    LogFile.WriteLogFile("Added " + Convert.ToDouble(strRow[i].ToString()) + " to array list for Geometric Mean");
                                }
                            }
                        }
                    }
                }
            }
            catch (FileNotFoundException FileNotFoundEx)
            {
                // no file found
                LogFile.WriteLogFile("Unable to find file " + Globals.mStrEarningsFile + " - Exiting Program");
                LogFile.WriteLogFile("Details: " + FileNotFoundEx.Message.ToString());
                LogFile.WriteLogFile("------- Job Completed (with Errors) -------");

                Environment.Exit(1);
            }
            catch (IOException IOEx)
            {
                // unable to read file
                LogFile.WriteLogFile("Unable to read file " + Globals.mStrEarningsFile + " - Exiting Program");
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

            if (Globals.mAlEarnings.Count > 0)
            {
                LogFile.WriteLogFile("Reading array list values");

                // do gemetric mean calculation
                Globals.mDblYearCount = Globals.mAlEarnings.Count;
                Globals.mDblRootIndice = (1 / Globals.mDblYearCount);

                for (int j = 0; j <= (Globals.mAlEarnings.Count - 1); j++)
                {
                    double dblYearEarnings = Convert.ToDouble(Globals.mAlEarnings[j]);

                    // + 1 for calculation
                    dblYearEarnings = dblYearEarnings + 1;

                    // multiply each value together
                    Globals.mDblRunningTotal = (Globals.mDblRunningTotal * dblYearEarnings);

                    //reset value
                    dblYearEarnings = 0;
                }

                LogFile.WriteLogFile("Calculating Geometric Mean");

                // calculate geometric mean
                Globals.mDblGeometricMean = Math.Round(((Math.Pow(Globals.mDblRunningTotal, Globals.mDblRootIndice) - 1) * 100), 2);

                LogFile.WriteLogFile ("Geometric Mean = " + Globals.mDblGeometricMean.ToString());

                return Globals.mDblGeometricMean;
            }
            else
            {
                LogFile.WriteLogFile("Array list " + Globals.mAlEarnings + " has no entries - Exiting Program");
                LogFile.WriteLogFile("------- Job Completed (with Errors) -------");
                Environment.Exit(1);

                return 0;
            }
        }

        #endregion
    }
}
