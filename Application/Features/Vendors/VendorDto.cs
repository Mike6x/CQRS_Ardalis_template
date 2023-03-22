using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Vendors
{

    public class VendorDto 
    {
        public int Id { get; set; }

        public string Code { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        public bool IsActive { get; set; }

        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public string? ContactPerson { get; set; }

        public string? TaxCode { get; set; }
    }

    public class VendorDetailsDto
    {
        public int Id { get; set; }

        public string Code { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        public bool IsActive { get; set; }

        // public string? Phone { get; set; }
        // public string? Email { get; set; }
        // public string? Address { get; set; }
        // public string? ContactPerson { get; set; }

        // public string? TaxCode { get; set; }
    }

    public class VendorExportDto
    {
        public int Id { get; set; }
        public string Code { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        public bool IsActive { get; set; }

        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public string? ContactPerson { get; set; }

        public string? TaxCode { get; set; }

        //public int CreatedBy { get; set; }
        //public DateTime CreatedOn { get; set; }
        //public int LastModifiedBy { get; set; }
        //public DateTime? LastModifiedOn { get; set; }
    }
}
