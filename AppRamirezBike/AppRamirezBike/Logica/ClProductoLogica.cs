using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AppRamirezBike.Modelo;
using AppRamirezBike.Datos;

namespace AppRamirezBike.Logica
{
    public class ClProductoLogica
    {


            public List<Producto> MtDatosVistaProducto(int salto, int tamañoPagina, out int totalRegistros,int idCategoria)
            {
                ClProductoDatos objProductoDatos = new ClProductoDatos();

              
                List<Producto> productosPaginados = objProductoDatos.MtListarProductosPaginados(
                    salto,
                    tamañoPagina,
                    out totalRegistros,
                    idCategoria
                    
                );

                return productosPaginados;
            }


        }
    }
