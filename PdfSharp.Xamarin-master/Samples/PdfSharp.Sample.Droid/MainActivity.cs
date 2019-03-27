/************************************************************************
* MainActivity.cs
* Page: MainActivity Android
* Autor: Adrian Mateo i Adrià Mestres
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
using System.Collections.Generic;

namespace PdfSharp.Sample.Droid
{
    [Activity(MainLauncher = true, Icon = "@drawable/icon", Theme ="@style/Theme.AppCompat.Light")]
    public class MainActivity : AppCompatActivity
    {
        //Variables
        static string fileName = "";
        static string textProva = "";
        static int contadorProductes = 0;
        static ArrayList a = new ArrayList();
        static List<Product> productos = new List<Product>();

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            //SetContentView del layout main
            SetContentView(Resource.Layout.Main);

            //Asignar id al button
            Button button = FindViewById<Button>(Resource.Id.MyButton);
            Button buttonGo = FindViewById<Button>(Resource.Id.button1);
            Button buttonProducts = FindViewById<Button>(Resource.Id.button2);
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
                alertD.SetCancelable(false).SetPositiveButton("Enviar", delegate
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

            buttonProducts.Click += delegate
            {
                Product producto = new Product(contadorProductes);
                productos.Add(producto);
                contadorProductes++;
            };

            ///Metodo onclick del button generar pdf
            button.Click += delegate
            {
                //Declarar pdf
                var document = new PdfDocument();

                //Declarar pagina y añadirla al pdf
                var page = document.AddPage();
                
                //Declarar xgraphics de la página y los asigno a la página
                var gfx = XGraphics.FromPdfPage(page);

                //Declarar diferentes xfont
                var font = new XFont("sans-serif", 12);
                var fontBold = new XFont("sans-serif", 12, XFontStyle.Bold);
                var fontBoldGran = new XFont("sans-serif", 14, XFontStyle.Bold);
                var fontBoldTitol = new XFont("sans-serif", 20, XFontStyle.Bold);
                var fontItalic = new XFont("sans-serif", 20, XFontStyle.Italic);

                //Intento de coger y poner foto en el pdf 
                var assembly = typeof(MainActivity).GetTypeInfo().Assembly;
                var imageName = assembly.GetName().Name + ".frogs.jpg";
                XImage image;
                using (var stream = assembly.GetManifestResourceStream(imageName))
                {
                    image = XImage.FromStream(stream);
                }
                gfx.DrawImage(image, new XRect(new XPoint(10, 10), new XSize(100, 100)));
                image.Dispose();
                //XImage imageFrog = XImage.FromFile("C:/imagen.jpg");

                Assembly asd = Assembly.GetAssembly(typeof(MainActivity));
                //Stream imgStream = asd.GetManifestResourceStream("Assembly.Resources.frogs.jpg");
                Stream imgStream = this.GetType().Assembly.GetManifestResourceStream("PdfSharp.Sample.Droid.frogs.jpg");
                XImage img = XImage.FromStream(imgStream);
                const double dx = 250, dy = 140;
                double width = img.PixelWidth * 72 / img.HorizontalResolution;
                double height = img.PixelHeight * 72 / img.HorizontalResolution;
                //gfx.DrawImage(img, 30, 150, 300, 250);

                //string logopath = Path.GetTempPath();
                //Determines what the path to the temp folder is and what the path to the image is (if it actually exists - see next step).
                //logopath = logopath + "frogs.jpg";
                //Checks to see if the file already exists in the Temp folder, if not, then it will create it.


                //'Creates the xImage and sets the image's source to be the logo image that we just saved to the Temp folder
                //XImage logoimage = XImage.FromFile(logopath);
                //gfx.DrawImage(logoimage, 60, 37, 40, 19);
                //gfx.DrawImage(imageFrog, 30, 150, 30, 150);
                //Dibujo strings en el pdf con una separacion de 40

                XSolidBrush greyBrush = new XSolidBrush(XColor.FromGrayScale(20));
                gfx.DrawRectangle(greyBrush, new XRect(new XPoint(10, 270), new XPoint(600, 290)));
                gfx.DrawRectangle(greyBrush, new XRect(new XPoint(530, 730), new XPoint(600, 750)));

                XPen pen = new XPen(XColors.Black, 9);
                gfx.DrawString("EXPRESS TOUR, SA", fontBold, new XSolidBrush(XColor.FromArgb(0, 0, 0)), 10, 130);
                gfx.DrawString("EXPRESS FOOD, SA", font, new XSolidBrush(XColor.FromArgb(0, 0, 0)), 10, 145);
                gfx.DrawString("B1122334455", font, new XSolidBrush(XColor.FromArgb(0, 0, 0)), 10, 160);
                gfx.DrawString("Avda. America 10", font, new XSolidBrush(XColor.FromArgb(0, 0, 0)), 10, 175);
                gfx.DrawString("MATARÓ", font, new XSolidBrush(XColor.FromArgb(0, 0, 0)), 10, 190);
                gfx.DrawString("BARCELONA", font, new XSolidBrush(XColor.FromArgb(0, 0, 0)), 10, 205);
                gfx.DrawString("937569330", font, new XSolidBrush(XColor.FromArgb(0, 0, 0)), 10, 220);
                gfx.DrawString("info@expresstoursaa.com", font, new XSolidBrush(XColor.FromArgb(0, 0, 0)), 10, 235);

                gfx.DrawString("FACTURA", fontBoldTitol, new XSolidBrush(XColor.FromArgb(0, 0, 0)), 375, 50);
                gfx.DrawString("Nº factura / Fecha", font, new XSolidBrush(XColor.FromArgb(0, 0, 0)), 375, 65);
                gfx.DrawString("FAC11 396 / 22-01-2018", font, new XSolidBrush(XColor.FromArgb(0, 0, 0)), 375, 80);
                gfx.DrawString("Agente : Juan Antonio", font, new XSolidBrush(XColor.FromArgb(0, 0, 0)), 375, 95);
                gfx.DrawString("10", font, new XSolidBrush(XColor.FromArgb(0, 0, 0)), 375, 125);
                gfx.DrawString("THE SERVICE TOUR S.A.", font, new XSolidBrush(XColor.FromArgb(0, 0, 0)), 375, 140);
                gfx.DrawString("Servicios al viajero TST", font, new XSolidBrush(XColor.FromArgb(0, 0, 0)), 375, 155);
                gfx.DrawString("9999999J", font, new XSolidBrush(XColor.FromArgb(0, 0, 0)), 375, 170);
                gfx.DrawString("Paseig de Maragall, 140", font, new XSolidBrush(XColor.FromArgb(0, 0, 0)), 375, 185);
                gfx.DrawString("08027 BARCELONA", font, new XSolidBrush(XColor.FromArgb(0, 0, 0)), 375, 200);
                gfx.DrawString("BARCELONA", font, new XSolidBrush(XColor.FromArgb(0, 0, 0)), 375, 215);
                gfx.DrawString("999999999", font, new XSolidBrush(XColor.FromArgb(0, 0, 0)), 375, 230);

                //Strings de la tabla
                gfx.DrawString("Producto", font, new XSolidBrush(XColor.FromArgb(0, 0, 0)), 14, 284);
                gfx.DrawString("Descripción", font, new XSolidBrush(XColor.FromArgb(0, 0, 0)), 79, 284);
                gfx.DrawString("Ud Medida", font, new XSolidBrush(XColor.FromArgb(0, 0, 0)), 280, 284);
                gfx.DrawString("Cantidad", font, new XSolidBrush(XColor.FromArgb(0, 0, 0)), 355, 284);
                gfx.DrawString("Precio", font, new XSolidBrush(XColor.FromArgb(0, 0, 0)), 418, 284);
                gfx.DrawString("% Dto.", font, new XSolidBrush(XColor.FromArgb(0, 0, 0)), 477, 284);
                gfx.DrawString("Subtotal", font, new XSolidBrush(XColor.FromArgb(0, 0, 0)), 536, 284);
                gfx.DrawString("UNIDADES", font, new XSolidBrush(XColor.FromArgb(0, 0, 0)), 130, 683);
                gfx.DrawString("Subtotal", font, new XSolidBrush(XColor.FromArgb(0, 0, 0)), 350, 683);
                gfx.DrawString("Dto P.P.", font, new XSolidBrush(XColor.FromArgb(0, 0, 0)), 350, 698);
                gfx.DrawString("Base", font, new XSolidBrush(XColor.FromArgb(0, 0, 0)), 350, 713);
                gfx.DrawString("IVA", font, new XSolidBrush(XColor.FromArgb(0, 0, 0)), 350, 728);
                gfx.DrawString("Forma de pago", fontBold, new XSolidBrush(XColor.FromArgb(0, 0, 0)), 14, 730);
                gfx.DrawString("Importe total", fontBoldGran, new XSolidBrush(XColor.FromArgb(0, 0, 0)), 350, 745);

                //Recuadro
                XPen line = new XPen(XColors.Black, 1); 
                gfx.DrawLine(line, 10, 270, 600, 270);  //Línea superior
                gfx.DrawLine(line, 10, 270, 10, 750);   //Línea izquierda
                gfx.DrawLine(line, 10, 750, 600, 750);  //Línea inferior
                gfx.DrawLine(line, 600, 750, 600, 270); //Línea derecha

                //Líneas separatoreas
                gfx.DrawLine(line, 10, 290, 600, 290);  //Línea horizontal1
                gfx.DrawLine(line, 10, 670, 600, 670);  //Línea horizontal2
                gfx.DrawLine(line, 530, 730, 600, 730);   //Línea horizontal2
                gfx.DrawLine(line, 75, 270, 75, 670);   //Línea vertical1
                gfx.DrawLine(line, 350, 270, 350, 670); //Línea vertical2
                gfx.DrawLine(line, 410, 270, 410, 670); //Línea vertical3
                gfx.DrawLine(line, 470, 270, 470, 670); //Línea vertical4
                gfx.DrawLine(line, 530, 270, 530, 750); //Línea vertical5

                int linea = 300;
                foreach (Product a in productos)
                {
                    string nombre = a.getNombre();
                    string descripcion = a.getDescripcion();
                    string unidadMedia = a.getUnidadMedia().ToString();
                    string cantidad = a.getCantidad().ToString();
                    string precio = a.getPrecio().ToString();
                    string descuento = a.getDescuento().ToString();
                    string subtotal = a.getSubtotal().ToString();

                    gfx.DrawString(nombre, font, new XSolidBrush(XColor.FromArgb(0, 0, 0)), 14, linea);
                    gfx.DrawString(descripcion, font, new XSolidBrush(XColor.FromArgb(0, 0, 0)), 79, linea);
                    gfx.DrawString(unidadMedia, font, new XSolidBrush(XColor.FromArgb(0, 0, 0)), 280, linea);
                    gfx.DrawString(cantidad, font, new XSolidBrush(XColor.FromArgb(0, 0, 0)), 355, linea);
                    gfx.DrawString(precio, font, new XSolidBrush(XColor.FromArgb(0, 0, 0)), 418, linea);
                    gfx.DrawString(descuento, font, new XSolidBrush(XColor.FromArgb(0, 0, 0)), 477, linea);
                    gfx.DrawString(subtotal, font, new XSolidBrush(XColor.FromArgb(0, 0, 0)), 536, linea);
                    linea = linea+15;
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
    

