using System;

using Didstopia.PDFReader;
using Xunit;

namespace Didstopia.PDFReader.Tests
{
    public class BookTests
    {
        #region Constants
        private const string SampleBookLocalPath = "Samples/pdf-sample.pdf";
        private const string SampleBookRemotePath = "http://css4.pub/2015/textbook/somatosensory.pdf";
        private const string SampleBookWithPasswordRemotePath = "http://www.orimi.com/pdf-test.pdf";
        #endregion

        #region Tests
        [Theory]
        [InlineData(SampleBookLocalPath)]
        [InlineData(SampleBookRemotePath)]
        // TODO: Pending on an issue with our custom PDFSharp library:
        //       https://github.com/Didstopia/PDFReader/issues/9
        //[InlineData(SampleBookWithPasswordRemotePath)]
        public void TestBookParsing(string filePath)
        {
            PDFBook book = PDFReader.OpenBook(filePath);
            TestBook(book);
        }

        [Theory]
        [InlineData(SampleBookLocalPath)]
        [InlineData(SampleBookRemotePath)]
        // TODO: Pending on an issue with our custom PDFSharp library:
        //       https://github.com/Didstopia/PDFReader/issues/9
        //[InlineData(SampleBookWithPasswordRemotePath)]
        public async void TestBookParsingAsync(string filePath)
        {
            // Create the book and start testing it
            PDFBook book = await PDFReader.OpenBookAsync(filePath);
            TestBook(book);
        }
        #endregion

        #region Test helpers
        private void TestBook(PDFBook book)
        {
            // Test the book
            Assert.False(book == null, "Book should not be null");
            Assert.False(string.IsNullOrEmpty(book.Title), "Book title should not be null or empty");
            //Assert.False(string.IsNullOrEmpty(book.Author), "Book author should not be null or empty");
            Assert.False(book.Pages == null || book.Pages.Count == 0, "Book pages should not be null or empty");

            // Test each page recursively
            foreach (PDFPage page in book.Pages)
                TestPage(page);
        }

        private void TestPage(PDFPage page)
        {
            // Test the page
            Assert.NotNull(page);
            Assert.False(string.IsNullOrEmpty(page.Contents), "Book page contents should not be null or empty");
        }
        #endregion
    }
}
