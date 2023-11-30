﻿namespace NetSchool.Common.Validator;

using FluentValidation;

public class ModelValidator<T> : IModelValidator<T> where T : class
{
    private readonly IValidator<T> validator;

    public ModelValidator(IValidator<T> validator)
    {
        this.validator = validator;
    }

    public void Check(T model)
    {
        var result = validator.Validate(model);
        if (!result.IsValid)
            throw new ValidationException(result.Errors);
    }

    public async Task CheckAsync(T model)
    {
        var result = await validator.ValidateAsync(model);
        if (!result.IsValid)
            throw new ValidationException(result.Errors);
    }
}