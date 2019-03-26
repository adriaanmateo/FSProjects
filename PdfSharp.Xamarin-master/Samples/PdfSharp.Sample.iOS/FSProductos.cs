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
        private string producto { get; set; }
        private string descripcion { get; set; }
        private string udMedida { get; set; }
        private float cantidad { get; set; }
        private float precio { get; set; }
        private float descuento { get; set; }
        private float subtotal { get; set; }

        public FSProductos(string producto, string descripcion, string udMedida, float cantidad, float precio, float descuento, float subtotal)
        {
            this.producto = producto;
            this.descripcion = descripcion;
            this.udMedida = udMedida;
            this.cantidad = cantidad;
            this.precio = precio;
            this.descuento = descuento;
            this.subtotal = subtotal;
        }
    }
}