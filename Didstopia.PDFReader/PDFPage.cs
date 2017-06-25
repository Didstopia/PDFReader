using System;
using System.Collections.Generic;

namespace Didstopia.PDFReader
{
    public class PDFPage
    {
        #region Properties
        public string Contents { get; }
        #endregion

        #region Constructor
        public PDFPage(string contents)
        {
            Contents = contents;
        }
        #endregion
    }
}
