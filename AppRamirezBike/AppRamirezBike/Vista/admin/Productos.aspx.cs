using AppRamirezBike.Logica;
using AppRamirezBike.Modelo;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppRamirezBike.Vista.Admin
{
    public partial class Productos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Se deja vacío, todo se carga por AJAX
        }

        // --- WEB METHODS DE PRODUCTOS (SE MANTIENEN IGUAL) ---

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static List<Producto> ListarTodosProductos()
        {
            ClProductosDashboardLogica logica = new ClProductosDashboardLogica();
            return logica.ListarTodosProductos();
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static Producto ObtenerProducto(int idProducto)
        {
            ClProductosDashboardLogica logica = new ClProductosDashboardLogica();
            return logica.ObtenerProductoPorId(idProducto);
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string GuardarProducto(Producto objProducto)
        {
            ClProductosDashboardLogica logica = new ClProductosDashboardLogica();
            return logica.GuardarProducto(objProducto);
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string EliminarProducto(int idProducto)
        {
            ClProductosDashboardLogica logica = new ClProductosDashboardLogica();
            return logica.EliminarProducto(idProducto);
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static List<Categoria> ListarCategorias()
        {
            ClCategoriasDashboardLogica logicaCategoria = new ClCategoriasDashboardLogica();
            return logicaCategoria.ListarTodasCategorias();
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false)]
        public static string SubirImagen()
        {
            // Este truco hace que funcione con FormData
            HttpContext context = HttpContext.Current;
            if (context.Request.Files.Count == 0) return null;

            var file = context.Request.Files[0];
            string nombre = "prod_" + Guid.NewGuid().ToString().Substring(0, 8) + Path.GetExtension(file.FileName);
            string ruta = context.Server.MapPath("~/Vista/img/") + nombre;
            file.SaveAs(ruta);
            return nombre;
        }
    }
}
