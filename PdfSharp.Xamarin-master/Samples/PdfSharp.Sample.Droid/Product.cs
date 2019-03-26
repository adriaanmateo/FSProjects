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
        public string nombre;
        public string descripcion;
        public double unidadMedia;
        public int cantidad;
        public double precio;
        public double descuento;
        public double subtotal;

        public Product(int contador)
        {
            this.nombre = "Producto" + contador;
            this.descripcion = "Descripcion" + contador;
            this.unidadMedia = contador + 1.27;
            this.cantidad = contador + 2;
            this.precio = contador + 3;
            this.descuento = 10.5;
            this.subtotal = contador + 20;
        }

        public string getNombre()
        {
            return nombre;
        }

        public string getDescripcion()
        {
            return descripcion;
        }

        public double getUnidadMedia()
        {
            return unidadMedia;
        }

        public int getCantidad()
        {
            return cantidad;
        }

        public double getPrecio()
        {
            return precio;
        }

        public double getDescuento()
        {
            return descuento;
        }

        public double getSubtotal()
        {
            return subtotal;
        }
    }
}