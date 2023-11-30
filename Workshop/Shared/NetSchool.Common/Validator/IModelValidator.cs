namespace NetSchool.Common.Validator;

public interface IModelValidator<T> where T : class
{
    void Check(T model);
    Task CheckAsync(T model);
}