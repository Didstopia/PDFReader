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

        public bool IsOCR { get; private set; }
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
            IsOCR = DetectOCR(pdfDocument);

            // Separate the PDF contents to page objects
            foreach (var page in pdfDocument.Pages)
            {
                // TODO: Apply formatting to the text (escape, replace etc.)

                // Construct the page contents
                var pageContents = string.Empty;
                foreach (string text in page.ExtractText())
                    pageContents += text;

                // Page is empty or requires OCR
                if (string.IsNullOrWhiteSpace(pageContents))
                {
                    
                }

                // Create a new page object, passing in the string contents
                if (!string.IsNullOrWhiteSpace(pageContents))
                    Pages.Add(new PDFPage(pageContents));
            }
        }

        // TODO: This is a rather poor implementation of detecting OCR content
        private bool DetectOCR(PdfDocument pdfDocument)
        {
            var foundText = false;
            foreach (var page in pdfDocument.Pages)
                foreach (var text in page.ExtractText())
                    if (!string.IsNullOrWhiteSpace(text))
                        foundText = true;
            
            return !foundText;
        }
        #endregion
    }
}
