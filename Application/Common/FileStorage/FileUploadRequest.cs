using Application.Common.Validation;
using FluentValidation;
using Microsoft.Extensions.Localization;
using System.ComponentModel;

namespace Application.Common.FileStorage;

public class FileUploadRequest
{
    public string Name { get; set; } = default!;
    public string Extension { get; set; } = default!;
    public string Data { get; set; } = default!;

   // public UploadType? UploadType { get; set; }
}

public class FileUploadRequestValidator : CustomValidator<FileUploadRequest>
{
    public FileUploadRequestValidator(IStringLocalizer<FileUploadRequestValidator> T)
    {
        RuleFor(p => p.Name)
            .NotEmpty()
                .WithMessage(T["File Name cannot be empty!"])
            .MaximumLength(150);

        RuleFor(p => p.Extension)
            .NotEmpty()
                .WithMessage(T["File Extension cannot be empty!"])
            .MaximumLength(5);

        RuleFor(p => p.Data)
            .NotEmpty()
                .WithMessage(T["File Data cannot be empty!"]);
    }
}

// public enum UploadType : byte
// {
//    [Description("Temp")]
//    Temp,
//    [Description(@"Images\Products")]
//    Product,
//    [Description(@"Images\ProfilePictures")]
//    ProfilePicture,
//    [Description("Documents")]
//    Document
// }