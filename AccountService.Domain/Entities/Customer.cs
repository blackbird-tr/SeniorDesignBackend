
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Domain.Entities
{
    public class Customer : BaseEntity
    {
    public int CustomerId { get; set; }
    public string Address { get; set; }
}
}