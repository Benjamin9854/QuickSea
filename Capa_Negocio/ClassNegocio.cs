using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using Capa_Datos;
using Capa_Entidad;

namespace Capa_Negocio
{
    public class ClassNegocio
    {
        ClassDatos objd = new ClassDatos();

        public DataTable N_listar_productos()
        {
            return objd.D_listar_productos();
        }

        public DataTable N_buscar_productos(ClassEntidad obje)
        {
            return objd.D_buscar_productos(obje);
        }

        public String N_mantenimiento_productos(ClassEntidad obje)
        {
            return objd.D_mantenimiento_productos(obje);
        }
    }
}
