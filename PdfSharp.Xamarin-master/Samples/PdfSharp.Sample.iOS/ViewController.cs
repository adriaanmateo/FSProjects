using Foundation;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System;
using System.IO;
using UIKit;

namespace PdfSharp.Sample.iOS
{
    public partial class ViewController : UIViewController
    {
        public ViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
        
        partial void UIButton3_TouchUpInside(UIButton sender)
        {
            var document = new PdfDocument();

            var page = document.AddPage();

            var gfx = XGraphics.FromPdfPage(page);

            var font = new XFont("Helvetica", 12);
            var fontBold = new XFont("Helvetica", 12, XFontStyle.Bold);
            var fontFACTURA = new XFont("Helvetica", 20, XFontStyle.Bold);
            var fontItalic = new XFont("Helvetica", 12, XFontStyle.Italic);

            var image = XImage.FromFile("frogs.jpg");

            gfx.DrawImage(image, 10, 10, 100, 100);

            //Debajo de la imagen
            gfx.DrawString("EXPRESS TOUR, SA", fontBold, new XSolidBrush(XColor.FromArgb(0, 0, 0)), 10, 130);
            gfx.DrawString("EXPRESS FOODS, SA", font, new XSolidBrush(XColor.FromArgb(0, 0, 0)), 10, 145);
            gfx.DrawString("B1122334455", font, new XSolidBrush(XColor.FromArgb(0, 0, 0)), 10, 160);
            gfx.DrawString("Avda. America 10", font, new XSolidBrush(XColor.FromArgb(0, 0, 0)), 10, 175);
            gfx.DrawString("MATARÓ", font, new XSolidBrush(XColor.FromArgb(0, 0, 0)), 10, 190);
            gfx.DrawString("BARCELONA", font, new XSolidBrush(XColor.FromArgb(0, 0, 0)), 10, 205);
            gfx.DrawString("937569330", font, new XSolidBrush(XColor.FromArgb(0, 0, 0)), 10, 220);
            gfx.DrawString("info@expresstoursaa.com", font, new XSolidBrush(XColor.FromArgb(0, 0, 0)), 10, 235);

            //Factura
            gfx.DrawString("Factura", fontFACTURA, new XSolidBrush(XColor.FromArgb(0, 0, 0)), 350, 70);
            gfx.DrawString("Nº factura / Fecha", font, new XSolidBrush(XColor.FromArgb(0, 0, 0)), 350, 85);
            gfx.DrawString("FAC11 396 / 22-01-2018", font, new XSolidBrush(XColor.FromArgb(0, 0, 0)), 350, 100);
            gfx.DrawString("Agente: Juan Antonio", font, new XSolidBrush(XColor.FromArgb(0, 0, 0)), 350, 115);
            gfx.DrawString("10", font, new XSolidBrush(XColor.FromArgb(0, 0, 0)), 350, 130);
            gfx.DrawString("THE SERVICE TOUR S.A.", font, new XSolidBrush(XColor.FromArgb(0, 0, 0)), 350, 145);
            gfx.DrawString("Servicios al viajero TST", font, new XSolidBrush(XColor.FromArgb(0, 0, 0)), 350, 160);
            gfx.DrawString("9999999J", font, new XSolidBrush(XColor.FromArgb(0, 0, 0)), 350, 175);
            gfx.DrawString("Passeig de Maragall, 140", font, new XSolidBrush(XColor.FromArgb(0, 0, 0)), 350, 190);
            gfx.DrawString("0827 BARCELONA", font, new XSolidBrush(XColor.FromArgb(0, 0, 0)), 350, 205);
            gfx.DrawString("BARCELONA", font, new XSolidBrush(XColor.FromArgb(0, 0, 0)), 350, 220);
            gfx.DrawString("999999999", font, new XSolidBrush(XColor.FromArgb(0, 0, 0)), 350, 235);

            //Tabla

            //gfx.DrawString("Test of PdfSharp on iOS in italic", font, new XSolidBrush(XColor.FromArgb(0, 0, 0)), 10, 210);
            XPen pen = new XPen(XColors.Black, 1);
            //PRODUCTO
            XPoint[] puntostabla =
             {
                 new XPoint(10,  250),
                 new XPoint(10, 750),
                 new XPoint(602,  750),
                 new XPoint(602, 250),
                 new XPoint(10, 250)
             };
            gfx.DrawLines(pen, puntostabla);
            XSolidBrush greyBrush = new XSolidBrush(XColor.FromGrayScale(20));

            gfx.DrawRectangle(greyBrush, new XRect(new XPoint(10, 250), (new XPoint(602, 280))));
            //gfx.DrawLine(pen, new XPoint(10, 250), new XPoint(10, 750));
            //TABLA
            XPoint[] points =
             {
                 new XPoint(10,  250),
                 new XPoint(10, 280),
                 new XPoint(100,  280),
                 new XPoint(100, 250),
                 new XPoint(10, 250)
             };
            gfx.DrawLines(pen, points);

            var fileName = Path.Combine(Path.GetTempPath(), "test.pdf");

            document.Save(fileName);

            pdfView.ScalesPageToFit = true;
            pdfView.LoadRequest(new NSUrlRequest(new NSUrl(fileName, false)));
        }
    }
}
 