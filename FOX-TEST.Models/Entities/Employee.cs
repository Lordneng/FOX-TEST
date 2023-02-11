using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace FOX_TEST.Models.Entities;

public partial class Employee
{
    public int Id { get; set; }

    public int? ParentId { get; set; }

    public string Position { get; set; } = null!;

    public string FullName { get; set; } = null!;

    public virtual ICollection<Employee> Children { get; set; } = new List<Employee>();

    public virtual Employee? Parent { get; set; }
}
