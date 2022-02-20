﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notipet.Domain
{
    public enum DocumentTypeId
    {
        Cedula,
        Passport
    }

    // This class just exists to create an equivalent table to the above enum
    public class DocumentType
    {
        public DocumentTypeId Id { get; set;}

        [Required]
        [StringLength(10)]
        public string Name { get; set;}

        //Pretty much useless, just to follow EF core conventions and create the Foreign Key
        public ICollection<User> Users { get; set;}
    }
}
