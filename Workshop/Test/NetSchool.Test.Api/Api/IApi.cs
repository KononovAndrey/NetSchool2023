namespace NetSchool.Test.Api;

using NetSchool.Services.Books;
using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;

public partial interface IApi
{
    #region Books

    [Get("/v1/book")]
    Task<IEnumerable<BookModel>> V1_Book_GetAll([Authorize("Bearer")] string token);

    [Get("/v1/book/{id}")]
    Task<BookModel> V1_Book_Get([Authorize("Bearer")] string token, Guid id);

    [Post("/v1/book")]
    Task<BookModel> V1_Book_Create([Authorize("Bearer")] string token, CreateModel request);

    [Delete("/v1/book")]
    Task V1_Book_Delete([Authorize("Bearer")] string token, Guid id);

    #endregion
}