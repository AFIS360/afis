using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFIS360
{
    public class PageEventHelper : PdfPageEventHelper
    {
        PdfContentByte cb;
        PdfTemplate template;

        public override void OnOpenDocument(PdfWriter writer, Document document)
        {
            cb = writer.DirectContent;
            template = cb.CreateTemplate(80, 50);
        }

        public override void OnEndPage(PdfWriter writer, Document document)
        {
            base.OnEndPage(writer, document);

            float effectiveStrWidthFirstPart = 0.0f;


            int pageN = writer.PageNumber;
            String text = "Page " + pageN.ToString() + " of ";
            Console.WriteLine("###-->> OnEndPageText = " + text);
            float len = 10.5f;

            iTextSharp.text.Rectangle pageSize = document.PageSize;

            cb.SetRGBColorFill(100, 100, 100);

            cb.BeginText();
            cb.SetFontAndSize(BaseFont.CreateFont(), 10.5f);
            cb.SetTextMatrix(document.LeftMargin, pageSize.GetBottom(document.BottomMargin - 10));
            cb.ShowText(text);
            effectiveStrWidthFirstPart = cb.GetEffectiveStringWidth(text, true);
            Console.WriteLine("###-->> Effective Str Width (first part) = " + effectiveStrWidthFirstPart);
            cb.EndText();

            cb.AddTemplate(template, document.LeftMargin + len, pageSize.GetBottom(document.BottomMargin - 10));
        }

        public override void OnCloseDocument(PdfWriter writer, Document document)
        {
            base.OnCloseDocument(writer, document);

            template.BeginText();
            template.SetFontAndSize(BaseFont.CreateFont(), 10.5f);
            string text = "" + (writer.PageNumber);
            template.SetTextMatrix(47.5f + getXofTextMatrix(writer.PageNumber), 0);
            Console.WriteLine("###-->> onCloseDocText = " + text);
            template.ShowText(text);
            template.EndText();
        }

        private int getXofTextMatrix(int maxPageNumber)
        {
            int XofTextMatrix = 0;

            if (maxPageNumber > 9)
            {
                XofTextMatrix = 1;
            }
            else if (maxPageNumber > 99)
            {
                XofTextMatrix = 2;
            }
            else if (maxPageNumber > 999)
            {
                XofTextMatrix = 3;
            }
            else if (maxPageNumber > 9999)
            {
                XofTextMatrix = 4;
            }
            return XofTextMatrix * 6;
        }
    }
}
