﻿using System.Collections.Generic;

namespace ServerUpload.DAL.Entities
{
    public class Material
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Category { get; set; }
        public ICollection<Version> Versions { get; set; }

    }
}
