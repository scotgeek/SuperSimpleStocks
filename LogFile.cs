using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;

namespace SuperSimpleStocks
{
    class LogFile
    {
        #region WriteLogFile

        public static void WriteLogFile(string p_LogMessage)
        {           
            try
            {
                // write message to log file - if it does not exist it will be created anyway
                using (StreamWriter logMessage = File.AppendText(Globals.mStrLogFile))
                {
                    logMessage.WriteLine(DateTime.Now.ToString(Globals.mStrLogFileTimeStamp) + " - " + p_LogMessage);
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

        #region CheckLogFileSize

        public static void CheckLogFileSize()
        {
            LogFile.WriteLogFile("Checking Size of Log File"); // WriteLogFile method will create log file anyway

            try
            {
                int intLogFileSize = Convert.ToInt32(Globals.mStrLogFile.Length);

                if (intLogFileSize >= Globals.mMaxLogFileSize)
                {
                    // write data to log file
                    LogFile.WriteLogFile(Globals.mStrLogFile + " is too large - renaming");

                    // get fileinfo for both files to chck for locking
                    FileInfo FiOldLogFile = new FileInfo(Globals.mStrOldLogFile);
                    FileInfo FiLogFile = new FileInfo(Globals.mStrLogFile);

                    // try to delete old log file
                    if (File.Exists(Globals.mStrOldLogFile))
                    {
                        try
                        {
                            if (!FileLocked.IsFileLocked(FiOldLogFile))
                            {
                                // delete existing old log file
                                File.Delete(Globals.mStrOldLogFile);

                                // write data to log file
                                LogFile.WriteLogFile("Deleted " + Globals.mStrOldLogFile);

                                if (!FileLocked.IsFileLocked(FiLogFile))
                                {
                                    try
                                    {
                                        // rename log file to old log file
                                        File.Move(Convert.ToString(Globals.mStrLogFile), Convert.ToString(Globals.mStrOldLogFile));

                                        // write data to log file
                                        LogFile.WriteLogFile(Globals.mStrLogFile + " renamed to " + Globals.mStrOldLogFile);
                                    }
                                    catch (IOException IOEx) // log file error
                                    {
                                        LogFile.WriteLogFile(Globals.mStrLogFile + " locked or being used by another process. Details:" + IOEx.Message.ToString() + " - Continuing");
                                    }
                                    catch (Exception ex)
                                    {
                                        LogFile.WriteLogFile("FAILED to rename " + Globals.mStrLogFile + " to " + Globals.mStrOldLogFile + ". Details: " + ex.Message.ToString() + " - Continuing");
                                    }
                                }
                                else
                                {
                                    // log file is locked
                                    Console.WriteLine("Log file " + Globals.mStrLogFile + " is locked - continuing without renaming log file");
                                }
                            }
                            else
                            {
                                // old log file is locked
                                Console.WriteLine("Old log file " + Globals.mStrOldLogFile + " is locked - continuing without renaming old log file");
                            }
                        }
                        catch (IOException IOEx) // old log file error
                        {
                            LogFile.WriteLogFile(Globals.mStrOldLogFile + " is locked or being used by another process. Details:" + IOEx.Message.ToString() + " - Continuing");
                        }
                        catch (Exception exDeleteOldLogFile)
                        {
                            LogFile.WriteLogFile("FAILED to delete " + Globals.mStrOldLogFile + ". Details:" + exDeleteOldLogFile.Message.ToString() + " - Continuing");
                        }
                    }
                    else
                    {
                        // no old log file exists - rename log file to old log file only
                        if (!FileLocked.IsFileLocked(FiLogFile))
                        {
                            try
                            {
                                File.Move(Convert.ToString(Globals.mStrLogFile), Convert.ToString(Globals.mStrOldLogFile));

                                // write data to log file
                                LogFile.WriteLogFile(Globals.mStrLogFile + " renamed to " + Globals.mStrOldLogFile);
                            }
                            catch (IOException IOEx)
                            {
                                Console.WriteLine(Globals.mStrLogFile + " is locked or being used by another process. Details:" + IOEx.Message.ToString() + " - Continuing");
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("FAILED to rename " + Globals.mStrLogFile + " to " + Globals.mStrOldLogFile + ". Details: " + ex.Message.ToString() + " - Continuing");
                            }
                        }
                        else
                        {
                            // log file is locked
                            Console.WriteLine("Log file " + Globals.mStrLogFile + " is locked - continuing without renaming log file");
                        }
                    }
                }
                else
                {
                    LogFile.WriteLogFile(Globals.mStrLogFile + " is (" + intLogFileSize + ") and does not exceed maximum size of (" + Globals.mMaxLogFileSize + ") - Continuing");
                }
            }
            catch (IOException IOEx)
            {
                Console.WriteLine("An IOException Occurred with Log File(s). Details:" + IOEx.Message.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine("An Error Ocurred with Log File(s) " + Globals.mStrLogFile + ". Details: " + ex.Message.ToString() + " - Continuing");
            }
        }

        #endregion
    }
}
