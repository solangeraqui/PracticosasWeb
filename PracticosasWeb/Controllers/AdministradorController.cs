using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PracticosasWeb.Models;
using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using PracticosasWeb.Models.DB;
using Org.BouncyCastle.Asn1.Ocsp;
using System.Drawing;
using Microsoft.IdentityModel.Tokens;
using System.Data.Entity;

namespace PracticosasWeb.Controllers
{
    public class AdministradorController : Controller
    {
        private readonly BdpracticosasContext _context;

        public AdministradorController(BdpracticosasContext context)
        {
            _context = context;
        }
        // GET: AdministradorController
        [HttpGet]
        [HttpPost]
        public ActionResult ADmin(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {

                int productId;
                if (int.TryParse(id, out productId))
                {
                    // Realiza la consulta a la base de datos utilizando el ID
                    Producto producto;

                    using (var dbContext = new BdpracticosasContext()) // Reemplaza "TuDbContext" con el contexto de tu base de datos
                    {
                        producto = dbContext.Productos.FirstOrDefault(p => p.ProductoId == productId);
                    }

                    if (producto != null)
                    {
                        return Json(producto);
                    }
                }
                return RedirectToAction("Error", "Home");
            }
            else
            {
                // Si la solicitud no es AJAX, puedes devolver un error o redirigir a otra acción
                var productos = _context.Productos.ToList();
                var categorias = _context.Categoria.ToList();

                var viewModel = new AdminView
                {
                    Productos = productos,
                    Categorias = categorias
                };

                return View(viewModel);
            }
        }
           
        // GET: AdministradorController/Details/5
        public ActionResult Clientes(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {

                int clienteId;
                if (int.TryParse(id, out clienteId))
                {
                    // Realiza la consulta a la base de datos utilizando el ID
                    Cliente clientes;

                    using (var dbContext = new BdpracticosasContext()) // Reemplaza "TuDbContext" con el contexto de tu base de datos
                    {
                        clientes = dbContext.Clientes.FirstOrDefault(p => p.ClienteId == clienteId);
                    }

                    if (clientes != null)
                    {
                        return Json(clientes);
                    }
                }
                return RedirectToAction("Error", "Home");
            }
            else
            {
                return View(_context.Clientes);
            }
        }
        public ActionResult Categorias(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {

                int categoriaId;
                if (int.TryParse(id, out categoriaId))
                {
                    // Realiza la consulta a la base de datos utilizando el ID
                    Categorium categoria;

                    using (var dbContext = new BdpracticosasContext()) // Reemplaza "TuDbContext" con el contexto de tu base de datos
                    {
                        categoria = dbContext.Categoria.FirstOrDefault(p => p.CategoriaId == categoriaId);
                    }

                    if (categoria != null)
                    {
                        return Json(categoria);
                    }
                }
                return RedirectToAction("Error", "Home");
            }
            else
            {
                return View(_context.Categoria);
            }
        }
        public ActionResult Usuarios(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {

                int usuarioId;
                if (int.TryParse(id, out usuarioId))
                {
                    // Realiza la consulta a la base de datos utilizando el ID
                    Usuario usuario;

                    using (var dbContext = new BdpracticosasContext()) // Reemplaza "TuDbContext" con el contexto de tu base de datos
                    {
                        usuario = dbContext.Usuarios.FirstOrDefault(p => p.UsuarioId == usuarioId);
                    }

                    if (usuario != null)
                    {
                        return Json(usuario);
                    }
                }
                return RedirectToAction("Error", "Home");
            }
            else
            {
                return View(_context.Usuarios);
            }
        }
        public JsonResult GuardarCategoria(string nombre, string estado, string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                int CategoriaId;
                if (int.TryParse(id, out CategoriaId))
                {
                    Categorium entidad;
                    using (var dbContext = new BdpracticosasContext()) // Reemplaza "TuDbContext" con el contexto de tu base de datos
                    {
                        // Obtener la entidad existente desde la base de datos
                        entidad = dbContext.Categoria.FirstOrDefault(e => e.CategoriaId == CategoriaId);

                        if (entidad != null)
                        {
                            // Modificar los datos de la entidad
                            entidad.Nombre = nombre;
                            entidad.Estado = estado;

                            // Guardar los cambios en la base de datos
                            dbContext.SaveChanges();
                            return Json("hola");
                        }
                        return Json("hola");
                    }
                }
                else
                {
                    var Nombre = nombre;
                    var Estado = estado;
                    var datosAGuardar = new Categorium
                    {
                        Nombre = Nombre,
                        Estado = Estado,
                        // Asignar otros valores a las propiedades
                    };
                    // Agregar el objeto al contexto de base de datos
                    _context.Add(datosAGuardar);

                    // Guardar los cambios en la base de datos

                    int registrosGuardados = _context.SaveChanges();

                    if (registrosGuardados > 0)
                    {
                        return Json("hola");
                        // Los datos se han guardado correctamente
                        // Puedes realizar alguna acción adicional si es necesario
                    }
                    else
                    {
                        return Json("no");
                        // No se guardó ningún registro
                        // Puedes manejar este caso de acuerdo a tus necesidades
                    }
                }
            }
            return Json("error");
        }
        [HttpPost]
        public JsonResult GuardarProductos(string nombre, int categoria, int precio, string estado, string codigo, string descripcion, int cantidad, string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                int productoid;
                if (int.TryParse(id, out productoid))
                {
                    Producto entidad;
                    using (var dbContext = new BdpracticosasContext()) // Reemplaza "TuDbContext" con el contexto de tu base de datos
                    {
                        // Obtener la entidad existente desde la base de datos
                        entidad = dbContext.Productos.FirstOrDefault(e => e.ProductoId == productoid);

                        if (entidad != null)
                        {
                            // Modificar los datos de la entidad
                            entidad.Nombre = nombre;
                            entidad.CategoriaId = categoria;
                            entidad.PrecioVenta = precio;
                            entidad.Estado = estado;
                            entidad.Codigo = codigo;
                            entidad.Descripcion = descripcion;
                            entidad.Cantidad = cantidad;
                            // entidad.Venta = estado;

                            // Guardar los cambios en la base de datos
                            dbContext.SaveChanges();
                            return Json("hola");
                        }
                        return Json("hola");
                    }
                }
                else
                {
                    var Nombre = nombre;
                    var Categoria = categoria;
                    var Precio = precio;
                    var Estado = estado;
                    var Codigo = codigo;
                    var Descripcion = descripcion;
                    var Cantidad = cantidad;
                    var datosAGuardar = new Producto
                    {
                        Nombre = Nombre,
                        CategoriaId = Categoria,
                        PrecioVenta = Precio,
                        Estado = Estado,
                        Codigo = Codigo,
                        Descripcion = Descripcion,
                        Cantidad = Cantidad,
                        // Asignar otros valores a las propiedades
                    };
                    // Agregar el objeto al contexto de base de datos
                    _context.Add(datosAGuardar);

                    // Guardar los cambios en la base de datos

                    int registrosGuardados = _context.SaveChanges();

                    if (registrosGuardados > 0)
                    {
                        return Json("hola");
                        // Los datos se han guardado correctamente
                        // Puedes realizar alguna acción adicional si es necesario
                    }
                    else
                    {
                        return Json("no");
                        // No se guardó ningún registro
                        // Puedes manejar este caso de acuerdo a tus necesidades
                    }
                }
            }
                return Json("error");

            }
        
        public JsonResult GuardarCliente(string nombre, string apellidos, string correo, string direccion, string distrito, string departamento, string telefono, string estado, string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                int clienteid;
                if (int.TryParse(id, out clienteid))
                {
                    Cliente entidad;
                    using (var dbContext = new BdpracticosasContext()) // Reemplaza "TuDbContext" con el contexto de tu base de datos
                    {
                        // Obtener la entidad existente desde la base de datos
                        entidad = dbContext.Clientes.FirstOrDefault(e => e.ClienteId == clienteid);

                        if (entidad != null)
                        {
                            // Modificar los datos de la entidad
                            entidad.Nombre = nombre;
                            entidad.Apellidos = apellidos;
                            entidad.Correo = correo;
                            entidad.Direccion = direccion;
                            entidad.Distrito = distrito;
                            entidad.Departamento = departamento;
                            entidad.Telefono = telefono;
                            entidad.Estado = estado;
                            // entidad.Venta = estado;

                            // Guardar los cambios en la base de datos
                            dbContext.SaveChanges();
                            return Json("hola");
                        }
                        return Json("hola");
                    }
                }
                else
                {
                    var Nombre = nombre;
                    var Apellidos = apellidos;
                    var Correo = correo;
                    var Direccion = direccion;
                    var Distrito = distrito;
                    var Departamento = departamento;
                    var Telefono = telefono;
                    var Estado = estado;
                    var datosAGuardar = new Cliente
                    {
                        Nombre = Nombre,
                        Apellidos = Apellidos,
                        Correo = Correo,
                        Direccion = Direccion,
                        Distrito = Distrito,
                        Departamento = Departamento,
                        Telefono = Telefono,
                        Estado = Estado,
                        // Asignar otros valores a las propiedades
                    };
                    // Agregar el objeto al contexto de base de datos
                    _context.Add(datosAGuardar);

                    // Guardar los cambios en la base de datos

                    int registrosGuardados = _context.SaveChanges();

                    if (registrosGuardados > 0)
                    {
                        return Json("hola");
                        // Los datos se han guardado correctamente
                        // Puedes realizar alguna acción adicional si es necesario
                    }
                    else
                    {
                        return Json("no");
                        // No se guardó ningún registro
                        // Puedes manejar este caso de acuerdo a tus necesidades
                    }

                }
            }
            return Json("error");
        }
        public JsonResult GuardarUsuario(string nombre, string contrasena, int tipo, string id)
        {
            var Nombre = nombre;
            var Contrasena = contrasena;
            var Tipo = tipo;
            var datosAGuardar = new Usuario
            {
                Nombre = Nombre,
                Contrasena = Contrasena,
                TipoUsuarioId = Tipo,

                // Asignar otros valores a las propiedades
            };
            // Agregar el objeto al contexto de base de datos
            _context.Add(datosAGuardar);

            // Guardar los cambios en la base de datos

            int registrosGuardados = _context.SaveChanges();

            if (registrosGuardados > 0)
            {
                return Json("hola");
                // Los datos se han guardado correctamente
                // Puedes realizar alguna acción adicional si es necesario
            }
            else
            {
                return Json("no");
                // No se guardó ningún registro
                // Puedes manejar este caso de acuerdo a tus necesidades
            }

        }
        // GET: AdministradorController/Create
        //public ActionResult Pedidos()
        //{
        //    return View();
        //}

        // POST: AdministradorController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AdministradorController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AdministradorController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AdministradorController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AdministradorController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
