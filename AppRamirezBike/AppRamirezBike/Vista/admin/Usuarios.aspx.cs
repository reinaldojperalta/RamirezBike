using AppRamirezBike.Logica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppRamirezBike.Vista.admin
{
    public partial class Usuarios : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static List<Modelo.UsuarioDashBoard> ListarUsuarios()
        {
            // 1. Instanciamos la clase de LÓGICA DE NEGOCIO.
            Logica.ClUsuarioDashBoardLogica logica = new Logica.ClUsuarioDashBoardLogica();

            // 2. Llamamos al método de la capa de lógica, que a su vez llamará a la capa de datos.
            return logica.MtListarUsuariosDashBoard();
        }
        [WebMethod]
        public static string InhabilitarUsuario(int idUsuario)
        {
            ClUsuarioLogica logica = new ClUsuarioLogica();
            return logica.MtInhabilitarUsuario(idUsuario);
        }

        // --- WEBMETHOD PARA OBTENER USUARIO POR ID ---
        [WebMethod]
        public static Modelo.Usuario ObtenerUsuarioPorId(int idUsuario)
        {
            ClUsuarioLogica logica = new ClUsuarioLogica();
            return logica.MtObtenerUsuarioPorId(idUsuario);
        }

        // --- WEBMETHOD PARA ACTUALIZAR USUARIO ---
        [WebMethod]
        public static string ActualizarUsuario(Modelo.Usuario oUsuario)
        {
            ClUsuarioLogica logica = new ClUsuarioLogica();
            return logica.MtActualizarUsuario(oUsuario);
        }
        [WebMethod]
        public static List<Modelo.Rol> ListarRoles()
        {
            // 1. Instanciamos tu capa de Lógica de Roles.
            ClRolLogica logicaRoles = new ClRolLogica();

            // 2. Llamamos a tu método existente y devolvemos la lista.
            return logicaRoles.MtObtenerRoles();
        }
    }
}