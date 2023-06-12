using PracticosasWeb.Models.DB;

namespace PracticosasWeb.Models
{
    public class AdminView
    {
        public List<Producto> Productos { get; set; }
        public List<Categorium> Categorias { get; set; }
        public List<Usuario> Usuarios { get; set; }
        public string Usuario { get; set; }
    }
}
