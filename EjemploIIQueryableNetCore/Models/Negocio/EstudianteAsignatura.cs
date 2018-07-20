using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EjemploIIQueryableNetCore.Models.Negocio
{
    public class EstudianteAsignatura
    {
        [Key]
        public int IdEstudianteAsignatura { get; set; }

        [Range(1,100)]
        public double Nota { get; set; }

        public int IdEstudiante { get; set; }
        public virtual Estudiante Estudiante { get; set; }

        public int IdAsignatura { get; set; }
        public virtual Asignatura Asignatura { get; set; }
    }
}
