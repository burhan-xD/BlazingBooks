
using BlazingBooks.Shared.Dtos;
using Refit;

namespace BlazingBooks.Mobile.Services
{
    public interface IBookApi
    {
        [Get("/api/books/{bookId}")]
        Task<BookDetailsDto> GetBookAsync(int bookId);

        [Get("/api/books")]
        Task<PageResult<BookListDto>> GetBooksAsync(int pageNo, int pageSize, string? genreSlug = null);

        [Get("/api/authors/{authorSlug}/books")]
        Task<PageResult<BookListDto>> GetBooksByAuthorAsync(int pageNo, int pageSize, string authorSlug);

        [Get("/api/genres")]
        Task<GenreDto[]> GetGenresAsync(bool topOnly);

        [Get("/api/books/populer")]
        Task<BookListDto[]> GetPopulerBooksAsync(int count, string? genreSlug = null);

        [Get("/api/books/{bookId}/similar")]
        Task<BookListDto[]> GetSimilarBooksAsync(int bookId, int count);
    }
}
