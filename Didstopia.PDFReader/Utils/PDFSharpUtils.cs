using System.Collections.Generic;
using Didstopia.PDFSharp.Pdf;
using Didstopia.PDFSharp.Pdf.Content;
using Didstopia.PDFSharp.Pdf.Content.Objects;

namespace Didstopia.PDFReader.Utils
{
    public static class PDFSharpUtils
    {
        public static IEnumerable<string> ExtractText(this PdfPage page)
        {
            var content = ContentReader.ReadContent(page);
            var text = content.ExtractText();
            return text;
        }

        public static IEnumerable<string> ExtractText(this CObject cObject)
        {
            if (cObject is COperator)
            {
                var cOperator = cObject as COperator;
                if (cOperator.OpCode.Name == OpCodeName.Tj.ToString() ||
                    cOperator.OpCode.Name == OpCodeName.TJ.ToString())
                {
                    foreach (var cOperand in cOperator.Operands)
                        foreach (var text in ExtractText(cOperand))
                            yield return text;
                }
            }
            else if (cObject is CSequence)
            {
                var cSequence = cObject as CSequence;
                foreach (var element in cSequence)
                    foreach (var text in ExtractText(element))
                        yield return text;
            }
            else if (cObject is CString)
            {
                var cString = cObject as CString;
                yield return cString.Value;
            }
        }
    }
}
