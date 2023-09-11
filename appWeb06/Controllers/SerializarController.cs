using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using appWeb06.Models;
using Newtonsoft.Json;

namespace appWeb06.Controllers
{
    public class SerializarController : Controller
    {
        static string jproducto = @"{
                  'idproducto':1,
                  'descripcion':'laptop',
                  'medida':'unidad',
                  'precio':1500,
                  'stock':20}";

        public ActionResult SerializaProducto(int op = 0)
        {
            if(op == 0)
            {
                return View(new Producto()); //viwe = que pueda verlo , a la vista
            }
            else
            {
                Producto p=JsonConvert.DeserializeObject<Producto>(jproducto); //Deserealizamos el objeto que se encuentra en p
            }
            return View(); //para que el resultado se pueda ver
        }

        [HttpPost]public ActionResult SerializaProducto(Producto reg)
        {
            //en este proceso recibimos el contenido del objeto reg y 
            //serializamos los datos en jproducto
            string mensaje = "";
            try
            {
                jproducto = JsonConvert.SerializeObject(reg);
                mensaje = "Producto Serializado";
            }
            catch (JsonException ex) { mensaje = ex.Message; }

            ViewBag.mensaje = mensaje;
            return View(reg);
        }
    }
}
