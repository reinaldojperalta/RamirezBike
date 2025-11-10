using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppRamirezBike.Modelo
{
	public class Producto
	{
		public int idProducto { get; set; }
		public string nombre { get; set; }
		public string descripcion { get; set; }
		public int precio { get; set; }
		public string imgUrl { get; set; }
		public int stock { get; set; }
		public bool estado { get; set; }
		public DateTime CreacionFecha { get; set; }
		public DateTime ModificacionFecha { get; set; }
	}
}