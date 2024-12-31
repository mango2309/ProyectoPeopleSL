﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace ProyectoPeopleSL.Models
{
    [Table("people")]
    public class PersonSL
    {
        [PrimaryKey, AutoIncrement]
        public int Id {  get; set; }

        [MaxLength(250), Unique]
        public string Name { get; set; }
    }
}
