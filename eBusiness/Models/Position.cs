﻿using eBusiness.Models.Base;

namespace eBusiness.Models
{
    public class Position:BaseEntity
    {
        public string Name { get; set; }
        public List<Employee> Employees { get; set; }
    }
}
