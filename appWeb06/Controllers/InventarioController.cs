using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using appWeb06.Models;
using Newtonsoft.Json;

namespace appWeb06.Controllers
{
    public class InventarioController : Controller
    {
        static string jcoleccion = @"[  /// creamos el objeto estatico que alamcenara los datos
        {
         'idproducto':1,
         'descripcion':'laptop',
         'medida':'unidad',
         'precio':1500,
         'stock':20,
         },                                    
        {
         'idproducto':2,
         'descripcion':'impresora portatil',
         'medida':'unidad',
         'precio':1000,
         'stock':7,
    },]";
        public ActionResult Index()             //se evaluara el contenido de jcoleccion, si esta vacio o si no
        {
            List<Producto> temporal;
            if (string.IsNullOrEmpty(jcoleccion))
                temporal = new List<Producto>();
            else
                temporal = JsonConvert.DeserializeObject<List<Producto>>(jcoleccion);

            return View(temporal);
        }

        public ActionResult Create()   //creamos un nuevo producto
        {
            return View(new Producto());
        }
        [HttpPost]public ActionResult Create(Producto reg)
        {
            string mensaje = "";
            if (!ModelState.IsValid) return View(reg); //para que me retorne los errores

            try
            {
                List<Producto> temporal = JsonConvert.DeserializeObject<List<Producto>>(jcoleccion); //deserializamos el objeto jcoleccion de tipo list
                temporal.Add(reg); // lo almacenamos en el reg add(aderimos)
                jcoleccion = JsonConvert.SerializeObject(temporal); //serializamos la lista
                mensaje = "Producto Resgiatrado";//que me muestre un mensjae que se serializo
            }
            catch(JsonException ex) { mensaje = ex.Message; } //por si no sale como planeo que se guarde el error
            ViewBag.mensaje = mensaje; //para que se pueda ver el mensjae
            return View(reg);//para que se pueda ver lo que hicimos
        }

        public ActionResult Details(int id = 0)
        {
            //deserealizar
            List<Producto> temporal = JsonConvert.DeserializeObject<List<Producto>>(jcoleccion);
            //buscar por idproducto
            Producto reg = temporal.FirstOrDefault(p => p.idproducto == id);

            return View(reg);
        }
    }
}