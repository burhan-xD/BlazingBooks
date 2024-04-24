using BlazingBooks.Shared.Dtos;
using BlazingBooks.Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazingBooks.Mobile.Services
{
    public class ApiBookFetcher : IBookService
    {
        private readonly IBookApi _bookApi;

        public ApiBookFetcher(IBookApi bookApi)
        {
            _bookApi = bookApi;
        }
        public async Task<BookDetailsDto> GetBookAsync(int bookId)
        {
            return await _bookApi.GetBookAsync(bookId);
        }

        public async Task<PageResult<BookListDto>> GetBooksAsync(int pageNo, int pageSize, string? genreSlug = null)
        {
            return await _bookApi.GetBooksAsync(pageNo, pageSize, genreSlug);
        }

        public async Task<PageResult<BookListDto>> GetBooksByAuthorAsync(int pageNo, int pageSize, string authorSlug)
        {
            return await _bookApi.GetBooksByAuthorAsync(pageNo,pageSize, authorSlug);
        }

        public async Task<GenreDto[]> GetGenresAsync(bool topOnly)
        {
            return await _bookApi.GetGenresAsync(topOnly);
        }

        public async Task<BookListDto[]> GetPopulerBooksAsync(int count, string? genreSlug = null)
        {
            return await _bookApi.GetPopulerBooksAsync(count, genreSlug);
        }

        public async Task<BookListDto[]> GetSimilarBooksAsync(int bookId, int count)
        {
            return await _bookApi.GetSimilarBooksAsync(bookId, count);
        }
    }
}
