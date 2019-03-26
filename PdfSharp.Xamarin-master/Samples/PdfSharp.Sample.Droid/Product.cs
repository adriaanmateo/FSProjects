using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace PdfSharp.Sample.Droid
{
    class Product
    {
        string name;
        string descripcion;
        float unidadMedia;
        int cantidad;
        float precio;
        float descuento;
        float subtotal;

        public Product(int contador)
        {
            this.name = "Producto" + contador;
            this.descripcion = "Descripcion" + contador;
        }
    }
}