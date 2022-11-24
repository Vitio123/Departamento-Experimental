using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//  public class Doctor
//     {
//         public bool activo { get; set; }
//         public string _id { get; set; }
//         public string IdDoctor { get; set; }
//         public string nombre { get; set; }
//         public string apellido { get; set; }
//         public string telefono { get; set; }
//         public DateTime published_at { get; set; }
//         public DateTime createdAt { get; set; }
//         public DateTime updatedAt { get; set; }
//         public int __v { get; set; }
//         public string especialidad { get; set; }
//         public string sexo { get; set; }
//         public string id { get; set; }
//     }

    public class InformacionResponse
    {
        public bool estado { get; set; }
        public string _id { get; set; }
        public string Lugar { get; set; }
        public string Id { get; set; }
        public string Edificio { get; set; }
        public string Descripcion { get; set; }
        public DateTime published_at { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
        public int __v { get; set; }
        public string IdLugares { get; set; }
        public string doctor { get; set; }
        public string id { get; set; }
    }