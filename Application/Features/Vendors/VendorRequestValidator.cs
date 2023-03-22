using Application.Common.Persistence;
using Application.Common.Validation;
using Domain.Entities;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Application.Features.Vendors
{
    public class CreateVendorRequestValidator : CustomValidator<CreateVendorRequest>
    {
        public CreateVendorRequestValidator(IReadRepository<Vendor> entityRepo, IStringLocalizer<CreateVendorRequestValidator> T)
        {
            RuleFor(e => e.Code)
                .NotEmpty()
                .MaximumLength(75)
                .MustAsync(async (code, ct) => await entityRepo.FirstOrDefaultAsync(new VendorByCodeSpec(code), ct) is null)
                    .WithMessage((_, code) => T["Vendor with Code{0} already exists.", code]);

            RuleFor(e => e.Name)
                .NotEmpty()
                .MaximumLength(75)
                .MustAsync(async (name, ct) => await entityRepo.FirstOrDefaultAsync(new VendorByNameSpec(name), ct) is null)
                    .WithMessage((_, name) => T["Vendor with Name {0} already exists.", name]);
        }
    }

    public class UpdateVendorRequestValidator : CustomValidator<UpdateVendorRequest>
    {
        public UpdateVendorRequestValidator(IReadRepository<Vendor> entityRepo, IStringLocalizer<UpdateVendorRequestValidator> T)
        {
            RuleFor(e => e.Code)
                .NotEmpty()
                .MaximumLength(75)
                .MustAsync(async (entity, code, ct) =>
                        await entityRepo.FirstOrDefaultAsync(new VendorByCodeSpec(code), ct)
                            is not Vendor existingEntity || existingEntity.Id == entity.Id)
                    .WithMessage((_, code) => T["using Domain.Purchase;\r\n\r\nnamespace Application.Purchase.Vendors; {0} already Exists.", code]);

            RuleFor(e => e.Name)
                .NotEmpty()
                .MaximumLength(75)
                .MustAsync(async (entity, name, ct) =>
                        await entityRepo.FirstOrDefaultAsync(new VendorByNameSpec(name), ct)
                            is not Vendor existingEntity || existingEntity.Id == entity.Id)
                    .WithMessage((_, name) => T["Vendor {0} already Exists.", name]);
        }
    }
}