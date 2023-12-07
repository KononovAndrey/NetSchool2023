using AutoMapper;
using FluentValidation;
using NetSchool.Settings;
using NetSchool.Context.Entities;

namespace NetSchool.Services.Books;

public class UpdateModel
{
    public string Title { get; set; }
    public string Description { get; set; }
}

public class UpdateModelProfile : Profile
{
    public UpdateModelProfile()
    {
        CreateMap<UpdateModel, Book>();
    }
}

public class UpdateModelValidator : AbstractValidator<UpdateModel>
{
    public UpdateModelValidator()
    {
        RuleFor(x => x.Title).BookTitle();

        RuleFor(x => x.Description)
            .MaximumLength(1000).WithMessage("Maximum length is 1000");
    }
}