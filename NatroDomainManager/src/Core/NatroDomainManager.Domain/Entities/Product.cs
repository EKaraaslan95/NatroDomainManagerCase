using NatroDomainManager.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace NatroDomainManager.Domain.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Stock { get; set; }
        public int Price { get; set; }
    }
}
