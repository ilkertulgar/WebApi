using FluentValidation;

namespace WebApi.BookOperations.CreateBook;

public class CreateBookCommandValidator : AbstractValidator<CreateBookCommand>
{
    public CreateBookCommandValidator()
    {
        RuleFor(c => c.Model.GenreId).GreaterThan(0);
        RuleFor(c => c.Model.PageCount).GreaterThan(0);
        RuleFor(x => x.Model.PublishDate).NotEmpty().LessThan(DateTime.Now.Date);
        RuleFor(x => x.Model.Title).NotEmpty().MinimumLength(4);
    }
}