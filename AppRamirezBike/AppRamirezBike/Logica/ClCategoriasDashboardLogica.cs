using AppRamirezBike.Datos;
using AppRamirezBike.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppRamirezBike.Logica
{
    public class ClCategoriasDashboardLogica
    {
        ClCategoriasDashboardDatos datos = new ClCategoriasDashboardDatos();

        // LISTAR TODAS LAS CATEGORÍAS
        public List<Categoria> ListarTodasCategorias()
        {
            return datos.MtListarCategorias();
        }
    }
}