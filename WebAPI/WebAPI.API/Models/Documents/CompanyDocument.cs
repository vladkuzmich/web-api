using FluentValidation;

namespace WebAPI.API.Models.Documents
{
    public class CompanyDocument
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class CompanyDocumentValidator : AbstractValidator<CompanyDocument>
    {
        public CompanyDocumentValidator()
        {
            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty()
                .WithMessage($"{nameof(UserDocument.Name)} is required.");
        }
    }
}
