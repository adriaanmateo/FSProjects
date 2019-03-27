using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;

namespace PdfSharp.Sample.iOS
{
    class FSProductos
    {
        private string producto;
        private string descripcion;
        private string udMedida;
        private double cantidad;
        private double precio;
        private double descuento;
        private double subtotal;

        public FSProductos(string producto, string descripcion, string udMedida, double cantidad, double precio, double descuento, double subtotal)
        {
            this.producto = producto;
            this.descripcion = descripcion;
            this.udMedida = udMedida;
            this.cantidad = cantidad;
            this.precio = precio;
            this.descuento = descuento;
            this.subtotal = subtotal;
        }

        public string getProducto()
        {
            return producto;
        }

        public string getDescripcion()
        {
            return descripcion;
        }

        public string getUnidadMedia()
        {
            return udMedida;
        }

        public double getCantidad()
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