using System.Collections.Generic;
using System.Linq;

using Didstopia.PDFReader.Utils;
using Didstopia.PDFSharp.Pdf;
using Didstopia.PDFSharp.Pdf.IO;

namespace Didstopia.PDFReader
{
    public class PDFBook
    {
        #region Properties
        public List<PDFPage> Pages { get; }

        public string Author { get; }
        public string Title { get; }

        public int PageCount => Pages.Count();
        #endregion

        #region Constructor
        public PDFBook(string filePath)
        {
            // Initialize our list of pages
            Pages = new List<PDFPage>();

            // Load a PDF from the filepath
            var pdfDocument = PdfReader.Open(filePath, PdfDocumentOpenMode.ReadOnly);

            // Store detailed information from the PDF
            Author = pdfDocument.Info.Author;
            Title = pdfDocument.Info.Title;

            // TODO: If we move to async methods at some point we might want to force
            //       users to call the process/load function themselves (async or not)

            // Start processing the PDF contents
            ProcessPDF(pdfDocument);

            // Close and dispose the document when we're done
            pdfDocument.Close();
            pdfDocument.Dispose();
        }
        #endregion

        #region Utility methods
        private void ProcessPDF(PdfDocument pdfDocument)
        {
            // Separate the PDF contents to page objects
            foreach (var page in pdfDocument.Pages)
            {
                // TODO: Apply formatting to the text (escape, replace etc.)

                // Construct the page contents
                var pageContents = "";
                foreach (string text in page.ExtractText())
                    pageContents += text;

                // Create a new page object, passing in the string contents
                Pages.Add(new PDFPage(pageContents));
            }
        }
        #endregion
    }
}
