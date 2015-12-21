using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;

namespace SuperSimpleStocks
{
    class FileLocked
    {
        #region IsFileLocked

        public static Boolean IsFileLocked(FileInfo file)
        {
            FileStream stream = null;

            try
            {
                stream = file.Open(FileMode.Open, FileAccess.Read, FileShare.None);
            }
            catch (IOException)
            {
                //the file is locked
                return true;
            }
            finally
            {
                if (stream != null) stream.Close();
            }

            //file is not locked 
            return false;
        }

        #endregion
    }
}
