using AppRamirezBike.Datos;
using AppRamirezBike.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppRamirezBike.Logica
{
	public class ClCategoriaLogica
	{
		ClCategoriaDatos objCategoria = new ClCategoriaDatos();
		public List<Categoria> MtObtenerCategorias()
		{
			List<Categoria> listaCategoria = objCategoria.MtListarCategorias();
			return listaCategoria;
		}
	}
}