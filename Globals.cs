using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Reflection;
using System.Configuration;
using System.Collections;

namespace SuperSimpleStocks
{
    class Globals
    {
        public static bool mDebug = true;

        // format setup
        public static string mStrLogFileTimeStamp = "dd MMM yyyy HH:mm:ss";

        // files, directory setup etc
        public static string mStrProjectName = Assembly.GetCallingAssembly().GetName().Name;       
        public static string mStrDirectoryName = @"D:\Development\work\" + mStrProjectName + @"\";
        public static string mStrLogFile = mStrDirectoryName + @"Logs\" + mStrProjectName + @".log";
        public static string mStrOldLogFile = mStrDirectoryName + @"\Logs\" + mStrProjectName + @".old";
        public static string mStrParameterFile = String.Empty; 
        public static string mStrStocksFile = String.Empty;
        public static int mMaxLogFileSize = Convert.ToInt32( ConfigurationManager.AppSettings["MaxLogFileSize"]);

        // ticker prices
        public static string mStrTEATicker = ConfigurationManager.AppSettings["TEA"];
        public static string mStrPOPTicker = ConfigurationManager.AppSettings["POP"];
        public static string mStrALETicker = ConfigurationManager.AppSettings["ALE"];
        public static string mStrGINTicker = ConfigurationManager.AppSettings["GIN"];
        public static string mStrJOETicker = ConfigurationManager.AppSettings["JOE"];
        public static decimal mDecTickerPrice;
        public static decimal mDecDividendYield;
        public static decimal mDecPE_Ratio;
        
        // buy sell
        public static StringBuilder mSbCsvLines = new StringBuilder();
        public static string mStrCsvFileName = "SuperSimpleStocks.csv";
        public static string mStrTransType = String.Empty;

        // stock price
        public static double mDblStockPrice = 0.00;
        public static double mDblSigmaTradePriceTimesQuantity = 0.00;
        public static int mIntSigmaQuantity = 0;

        // geometric mean
        public static string mStrEarningsFile = String.Empty;
        public static ArrayList mAlEarnings = new ArrayList();
        public static double mDblYearCount;
        public static double mDblGeometricMean;
        public static double mDblRootIndice;
        public static double mDblRunningTotal = 1;
    }
}
