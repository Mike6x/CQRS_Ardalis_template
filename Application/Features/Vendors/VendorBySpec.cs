using Ardalis.Specification;
using Domain.Entities;

namespace Application.Features.Vendors
{

    public class VendorByIdSpec : Specification<Vendor, VendorDetailsDto>, ISingleResultSpecification
    {
        public VendorByIdSpec(int id) =>
            Query
                .Where(e => e.Id == id);
    }

    public class VendorByCodeSpec : Specification<Vendor>, ISingleResultSpecification
    {
        public VendorByCodeSpec(string code) =>
            Query
                .Where(e => e.Code == code);
    }

    public class VendorByNameSpec : Specification<Vendor>, ISingleResultSpecification
    {
        public VendorByNameSpec(string name) =>
            Query
                .Where(e => e.Name == name);
    }
}
