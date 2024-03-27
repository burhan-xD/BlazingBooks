using BlazingBooks.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazingBooks.Shared.Interfaces
{
    public interface IBookService
    {
        Task<BookDetailsDto> GetBookAsync(int bookId);
        Task<PageResult<BookListDto>> GetBooksAsync(int pageNo, int pageSize, string? genreSlug = null);
        Task<PageResult<BookListDto>> GetBooksByAuthorAsync(int pageNo, int pageSize, string authorSlug);
        Task<GenreDto[]> GetGenresAsync(bool topOnly);
        Task<BookListDto[]> GetPopulerBooksAsync(int count, string? genreSlug = null);
        Task<BookListDto[]> GetSimilarBooksAsync(int bookId, int count);
    }
}
