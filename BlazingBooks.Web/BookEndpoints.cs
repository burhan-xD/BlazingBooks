using BlazingBooks.Shared.Interfaces;

namespace BlazingBooks.Web
{
    public static class BookEndpoints
    {
        public static IEndpointRouteBuilder MapBookEndPoints(this IEndpointRouteBuilder app)
        {
            app.MapGet("/api/books/{bookId:int}", async (int bookId, IBookService bookService) => TypedResults.Ok(await bookService.GetBookAsync(bookId))
            );

            app.MapGet("/api/books", async (IBookService bookService, int pageNo, int pageSize, string? genreSlug = null) => TypedResults.Ok(await bookService.GetBooksAsync(pageNo, pageSize, genreSlug))
            );

            app.MapGet("/api/authors/{authorSlug}/books", async (IBookService bookService, int pageNo, int pageSize, string authorSlug) => TypedResults.Ok(await bookService.GetBooksByAuthorAsync(pageNo, pageSize, authorSlug))
            );

            app.MapGet("/api/genres", async (IBookService bookService,bool topOnly) => TypedResults.Ok(await bookService.GetGenresAsync(topOnly))
            );

            app.MapGet("/api/books/populer", async (IBookService bookService, int count, string? genreSlug = null) => TypedResults.Ok(await bookService.GetPopulerBooksAsync(count, genreSlug))
            );

            app.MapGet("/api/books/{bookId:int}/similar", async (int bookId, int count, IBookService bookService) => TypedResults.Ok(await bookService.GetSimilarBooksAsync(bookId, count))
            );




            return app;
        }
    }
}
