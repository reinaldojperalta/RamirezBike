using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppRamirezBike.Datos
{
    public partial class SubirImagen : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Clear();
            Response.ContentType = "application/json; charset=utf-8";

            try
            {
                if (Request.Files.Count > 0 && Request.Files[0].ContentLength > 0)
                {
                    var file = Request.Files[0];
                    string extension = Path.GetExtension(file.FileName);
                    string nombre = "prod_" + Guid.NewGuid().ToString().Substring(0, 8) + extension;

                    // ¡RUTA CORRECTA AHORA!
                    string rutaCarpeta = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Vista", "img"); rutaCarpeta = HttpContext.Current.Server.MapPath("~/Vista/img/");
                    if (!Directory.Exists(rutaCarpeta))
                        Directory.CreateDirectory(rutaCarpeta);

                    string rutaCompleta = Path.Combine(rutaCarpeta, nombre);
                    file.SaveAs(rutaCompleta);

                    Response.Write(JsonConvert.SerializeObject(new { success = true, filename = nombre }));
                }
                else
                {
                    Response.Write("{\"success\": false, \"error\": \"No file\"}");
                }
            }
            catch (Exception ex)
            {
                Response.Write("{\"success\": false, \"error\": \"" + ex.Message.Replace("\"", "\\\"") + "\"}");
            }

            Response.End();
        }
    }
}