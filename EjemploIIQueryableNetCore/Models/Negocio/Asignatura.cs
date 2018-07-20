using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EjemploIIQueryableNetCore.Models.Negocio
{
    public class Asignatura
    {
        [Key]
        public int IdAsignatura { get; set; }

        [StringLength(maximumLength:50)]
        public string Nombre { get; set; }

        public virtual List<EstudianteAsignatura> EstudianteAsignatura { get; set; }
    }
}
