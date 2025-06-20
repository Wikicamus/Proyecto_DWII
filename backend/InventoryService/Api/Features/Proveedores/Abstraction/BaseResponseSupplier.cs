using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Features.Proveedores.Abstraction
{
    public class BaseResponseSupplier : SupplierDto
    {
        public int Id { get; set; }
    }
}