/************************************************************************
* ViewController.cs
* Page: ViewController IOs
* Autor: Adrian Mateo
* Fecha: 26/03/2019
* Copyright FarAndSoft S.L 2018
*************************************************************************/

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
        partial void agregar(UIButton sender)
        {
            ShowToast("Prova", View);
        }
        public void ShowToast(String message, UIView view)
        {
            UIView residualView = view.ViewWithTag(1989);
            if (residualView != null)
                residualView.RemoveFromSuperview();

            var viewBack = new UIView(new CoreGraphics.CGRect(83, 0, 300, 100));
            viewBack.BackgroundColor = UIColor.Black;
            viewBack.Tag = 1989;
            UILabel lblMsg = new UILabel(new CoreGraphics.CGRect(0, 20, 300, 60));
            lblMsg.Lines = 2;
            lblMsg.Text = message;
            lblMsg.TextColor = UIColor.White;
            lblMsg.TextAlignment = UITextAlignment.Center;
            viewBack.Center = view.Center;
            viewBack.AddSubview(lblMsg);
            view.AddSubview(viewBack);
            UIView.BeginAnimations("Toast");
            UIView.SetAnimationDuration(3.0f);
            viewBack.Alpha = 0.0f;
            UIView.CommitAnimations();
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
            XPen pen = new XPen(XColors.Black, 1);
            gfx.DrawImage(image, 10, 10, 100, 100);
            XSolidBrush greyBrush = new XSolidBrush(XColor.FromGrayScale(20));

            gfx.DrawRectangle(pen, greyBrush, new XRect(new XPoint(10, 250), (new XPoint(600, 280))));
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
            gfx.DrawString("Producto", font, new XSolidBrush(XColor.FromArgb(0, 0, 0)), 14, 274);
            gfx.DrawString("Descripción", font, new XSolidBrush(XColor.FromArgb(0, 0, 0)), 79, 274);
            gfx.DrawString("Ud Medida", font, new XSolidBrush(XColor.FromArgb(0, 0, 0)), 280, 274);
            gfx.DrawString("Cantidad", font, new XSolidBrush(XColor.FromArgb(0, 0, 0)), 355, 274);
            gfx.DrawString("Precio", font, new XSolidBrush(XColor.FromArgb(0, 0, 0)), 418, 274);
            gfx.DrawString("% Dto.", font, new XSolidBrush(XColor.FromArgb(0, 0, 0)), 477, 274);
            gfx.DrawString("Subtotal", font, new XSolidBrush(XColor.FromArgb(0, 0, 0)), 536, 274);
            gfx.DrawString("UNIDADES", font, new XSolidBrush(XColor.FromArgb(0, 0, 0)), 130, 683);
            gfx.DrawString("Subtotal", font, new XSolidBrush(XColor.FromArgb(0, 0, 0)), 350, 683);
            gfx.DrawString("Dto P.P.", font, new XSolidBrush(XColor.FromArgb(0, 0, 0)), 350, 698);
            gfx.DrawString("Base", font, new XSolidBrush(XColor.FromArgb(0, 0, 0)), 350, 713);
            gfx.DrawString("IVA", font, new XSolidBrush(XColor.FromArgb(0, 0, 0)), 350, 728);
            gfx.DrawString("Forma de pago", fontBold, new XSolidBrush(XColor.FromArgb(0, 0, 0)), 14, 730);
            gfx.DrawString("Importe total", font, new XSolidBrush(XColor.FromArgb(0, 0, 0)), 350, 745);

            //gfx.DrawString("Test of PdfSharp on iOS in italic", font, new XSolidBrush(XColor.FromArgb(0, 0, 0)), 10, 210);

            
            XPoint[] puntostabla =
             {
                 new XPoint(10,  250),
                 new XPoint(10, 750),
                 new XPoint(600,  750),
                 new XPoint(600, 250),
                 new XPoint(10, 250)
             };
            gfx.DrawLines(pen, puntostabla);
            
            
            gfx.DrawLine(pen, 10, 280, 600, 280);  //Línea horizontal1
            gfx.DrawLine(pen, 10, 670, 600, 670);  //Línea horizontal2
            gfx.DrawLine(pen, 530, 730, 600, 730);  //Línea horizontal2
            gfx.DrawLine(pen, 75, 250, 75, 670);   //Línea vertical1
            gfx.DrawLine(pen, 350, 250, 350, 670); //Línea vertical2
            gfx.DrawLine(pen, 410, 250, 410, 670); //Línea vertical3
            gfx.DrawLine(pen, 470, 250, 470, 670); //Línea vertical4
            gfx.DrawLine(pen, 530, 250, 530, 750); //Línea vertical5

            var fileName = Path.Combine(Path.GetTempPath(), "test.pdf");

            document.Save(fileName);

            pdfView.ScalesPageToFit = true;
            pdfView.LoadRequest(new NSUrlRequest(new NSUrl(fileName, false)));
        }
    }
}
 