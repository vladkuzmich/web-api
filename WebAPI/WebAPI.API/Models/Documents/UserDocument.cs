using System;
using FluentValidation;

namespace WebAPI.API.Models.Documents
{
    public class UserDocument
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; } 
        public DateTime? BirthDate { get; set; }
    }

    public class UserDocumentValidator : AbstractValidator<UserDocument>
    {
        public UserDocumentValidator()
        {
            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty()
                .WithMessage($"{nameof(UserDocument.Name)} is required.");

            RuleFor(x => x.Surname)
                .NotNull()
                .NotEmpty()
                .WithMessage($"{nameof(UserDocument.Surname)} is required.");

            RuleFor(x => x.Email)
                .NotNull()
                .NotEmpty()
                .WithMessage($"{nameof(UserDocument.Email)} is required.")
                .EmailAddress()
                .WithMessage("Enter, please, valid email address.");

        }
    }
}
