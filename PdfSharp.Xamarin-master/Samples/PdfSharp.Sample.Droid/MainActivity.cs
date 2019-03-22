/************************************************************************
* MainActivity.cs
* Page: MainActivity Android
* Autor: Adrian Mateo
* Fecha: 18/03/2019
* Copyright FarAndSoft S.L 2018
*************************************************************************/

using Android.App;
using Android.Widget;
using Android.OS;
using PdfSharp.Pdf;
using PdfSharp.Drawing;
using System.IO;
using Android.Webkit;
using System.Net;
using Android.Support.V7.App;
using Android.Views;
using System.Reflection;
using System.Collections;
using System;

namespace PdfSharp.Sample.Droid
{
    [Activity(MainLauncher = true, Icon = "@drawable/icon", Theme ="@style/Theme.AppCompat.Light")]
    public class MainActivity : AppCompatActivity
    {
        //Variables
        static int espacio = 250;
        static string fileName = "";
        static string textProva = "";
        static ArrayList a = new ArrayList();

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            //SetContentView del layout main
            SetContentView(Resource.Layout.Main);

            //Asignar id al button
            Button button = FindViewById<Button>(Resource.Id.MyButton);
            Button buttonGo = FindViewById<Button>(Resource.Id.button1);
            Button buttonText = FindViewById<Button>(Resource.Id.text2);

            //Asignar id al webview y configurarlo
            WebView webview = FindViewById<WebView>(Resource.Id.MyWebView);
            webview.Settings.AllowUniversalAccessFromFileURLs = true;
            webview.Settings.AllowFileAccessFromFileURLs = true;
            webview.Settings.JavaScriptEnabled = true;
            webview.Settings.DomStorageEnabled = true;

            ///Metodo onclick para pedir texto al usuario
            buttonText.Click += delegate {
                LayoutInflater layoutInflaterAndroid = LayoutInflater.From(this);
                View androidv = layoutInflaterAndroid.Inflate(Resource.Layout.userInputAlertDialog, null);
                Android.Support.V7.App.AlertDialog.Builder alertD = new Android.Support.V7.App.AlertDialog.Builder(this);
                alertD.SetView(androidv);
                var textoUser = androidv.FindViewById<EditText>(Resource.Id.text);
                alertD.SetCancelable(false)
                .SetPositiveButton("Enviar", delegate
                {
                    textProva = textoUser.Text;
                    a.Add(textProva);
                }).SetNegativeButton("Cancelar", delegate
                {
                    alertD.Dispose();
                });
                Android.Support.V7.App.AlertDialog alertDShow = alertD.Create();
                alertDShow.Show();
            };

            ///Metodo onclick del button generar pdf
            button.Click += delegate
            {
                //Declarar pdf
                var document = new PdfDocument();

                //Declarar pagina y añadirla al pdf
                var page = document.AddPage();
                
                //Declarar xgraphics de la pagina y los asigno a la pagina
                var gfx = XGraphics.FromPdfPage(page);

                //Declarar diferentes xfont
                var font = new XFont("sans-serif", 20);
                var fontBold = new XFont("sans-serif", 20, XFontStyle.Bold);
                var fontItalic = new XFont("sans-serif", 20, XFontStyle.Italic);

                //Intento de coger y poner foto en el pdf 
                var assembly = typeof(MainActivity).GetTypeInfo().Assembly;
                var imageName = assembly.GetName().Name + ".frogs.jpg";
                XImage image;
                using (var stream = assembly.GetManifestResourceStream(imageName))
                {
                    image = XImage.FromStream(stream);
                }
                gfx.DrawImage(image, new XRect(new XPoint(30, 150), new XSize(300, 250)));
                image.Dispose();
                //XImage imageFrog = XImage.FromFile("C:/imagen.jpg");

                Assembly asd = Assembly.GetAssembly(typeof(MainActivity));
                //Stream imgStream = asd.GetManifestResourceStream("Assembly.Resources.frogs.jpg");
                Stream imgStream = this.GetType().Assembly.GetManifestResourceStream("PdfSharp.Sample.Droid.frogs.jpg");
                XImage img = XImage.FromStream(imgStream);
                const double dx = 250, dy = 140;
                double width = img.PixelWidth * 72 / img.HorizontalResolution;
                double height = img.PixelHeight * 72 / img.HorizontalResolution;
                gfx.DrawImage(img, 30, 150, 300, 250);

                //string logopath = Path.GetTempPath();
                //'Determines what the path to the temp folder is and what the path to the image is (if it actually exists - see next step).
                //logopath = logopath + "frogs.jpg";
                //'Checks to see if the file already exists in the Temp folder, if not, then it will create it.


                //'Creates the xImage and sets the image's source to be the logo image that we just saved to the Temp folder
                //XImage logoimage = XImage.FromFile(logopath);
                //gfx.DrawImage(logoimage, 60, 37, 40, 19);
                //gfx.DrawImage(imageFrog, 30, 150, 30, 150);
                //Dibujo strings en el pdf con una separacion de 40
                XPen pen = new XPen(XColors.Black, 9);
                gfx.DrawString("Ejemplo de PDF primera linea", font, new XSolidBrush(XColor.FromArgb(0, 0, 0)), 10, 130);
                gfx.DrawString("Ejemplo de PDF segunda linea", fontBold, new XSolidBrush(XColor.FromArgb(0, 0, 0)), 10, 170);
                gfx.DrawString("Ejemplo de PDF tercera linea", fontItalic, new XSolidBrush(XColor.FromArgb(0, 0, 0)), 10, 210);
                gfx.DrawString("Ejemplo de PDF cuarta linea", font, new XSolidBrush(XColor.FromArgb(120, 0, 0)), 10, 250);
                //gfx = XGraphics.FromPdfPage(page2, XGraphicsPdfPageOptions.Append);
                //page2.Width = XUnit.FromMillimeter(210);
                //page2.Height = XUnit.FromMillimeter(297);
                
                    XPen lineRed = new XPen(XColors.Red, 5);
                    gfx.DrawLine(lineRed, 30, 300, 250, 300);
                if (a.Count > 1)
                    drawArrayString(font, 30, espacio + 80);
                else
                {
                    drawString(textProva, font, 30, espacio + 40);
                    gfx.DrawLine(XPens.Black, 30, 300, 30, page.Height);
                    gfx.DrawLine(XPens.Black, 250, 300, 250, page.Height);
                }
                //Metodo para pintar string en el archivo
                void drawString(string text, XFont fuente, int x, int y)
                {
                    gfx.DrawString(page.Height.ToString(), fuente, new XSolidBrush(XColor.FromArgb(0, 0, 0)), x, y);
                }
                //Metodo para pintar varios string de un arrylist
                void drawArrayString(XFont fuente, int x, int y) {
                    //Punto de pintado en la segunda pagina
                    int yy=130;
                    bool e = false;
                    //Recorrer el arraylist de string introducidos
                    foreach (object o in a) {
                        string s = (string)o;
                        //Si ya no cabe mas texto en la primera pagina
                        if (y > 792)
                        {
                            //Utilizo un bool para cambiar el XGraphics de pagina a la segunda y lo pongo en true para que solo lo haga una vez
                            if (!e)
                            {
                                var page2 = document.AddPage();
                                gfx = XGraphics.FromPdfPage(page2);
                            }
                            e = true;
                            //Pinta en la segunda pagina con un nuevo integer que empieza desde el inicio con un espaciado de 40p al igual que el otro
                            gfx.DrawString(s, fuente, new XSolidBrush(XColor.FromArgb(0, 0, 0)), x, yy);
                            yy = yy + 40;
                        }
                        //Si la pagina sigue sin estar llena, pinta en la primera
                        else {
                            gfx.DrawString(s, fuente, new XSolidBrush(XColor.FromArgb(0, 0, 0)), x, y);
                            y = y + 240;
                        }
                    }
                }
                //Guardo el path y el nombre del archivo
                fileName = Path.Combine(Path.GetTempPath(), "test.pdf");
                //Lo guardo en el pdf
                document.Save(fileName);
                //File.Open(fileName, FileMode.Open);
            };
            //Onclick para abrir la webview
            buttonGo.Click += delegate
            {
                if (!string.IsNullOrEmpty(fileName))
                {
                    webview.LoadUrl(string.Format("file:///android_asset/pdfjs/web/viewer.html?file={0}", string.Format("file:///{0}", WebUtility.UrlEncode(fileName))));
                }
                else { Toast.MakeText(this, "Error al abrir el archivo pdf", ToastLength.Short).Show(); }
            };
        }
    }
}
    

