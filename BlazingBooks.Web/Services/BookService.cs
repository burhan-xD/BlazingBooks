using BlazingBooks.Shared.Dtos;
using BlazingBooks.Shared.Interfaces;
using BlazingBooks.Web.Data;
using Microsoft.EntityFrameworkCore;

namespace BlazingBooks.Web.Services
{
    public class BookService : IBookService
    {
        private readonly IDbContextFactory<BookContext> _dbContextFactory;

        public BookService(IDbContextFactory<BookContext> dbContextFactory)
        {
            this._dbContextFactory = dbContextFactory;
        }
        public async Task<GenreDto[]> GetGenresAsync(bool topOnly)
        {
            using var context = _dbContextFactory.CreateDbContext();
            var query = context.Genres.AsQueryable();
            if (topOnly)
            {
                query = query.Where(q => q.IsTop);
            }

            var genres = await query.Select(g => new GenreDto(g.Name, g.Slug)).ToArrayAsync();
            return genres;

        }

        public async Task<PageResult<BookListDto>> GetBooksAsync(int pageNo, int pageSize, string? genreSlug = null)
        {
            using var context = _dbContextFactory.CreateDbContext();
            var query = context.Books.AsQueryable();
            if (!string.IsNullOrWhiteSpace(genreSlug))
            {
                query = context.Genres.Where(g => g.Slug == genreSlug).SelectMany(g => g.GenreBooks).Select(gb => gb.Book);
            }

            var totalCount = await query.CountAsync();
            var books = await query.OrderByDescending(b => b.Id)
                .Skip((pageNo - 1) * pageSize)
                .Take(pageSize)
                .Select(b => new BookListDto(b.Id, b.Title, b.Image, new AuthorDto(b.Author.Name, b.Author.Slug)))
                .ToArrayAsync();

            return new PageResult<BookListDto>(books, totalCount);


        }

        public async Task<BookListDto[]> GetPopulerBooksAsync(int count, string? genreSlug = null)
        {
            using var context = _dbContextFactory.CreateDbContext();
            var query = context.Books.AsQueryable();
            if (!string.IsNullOrWhiteSpace(genreSlug))
            {
                query = context.Genres.Where(g => g.Slug == genreSlug).SelectMany(g => g.GenreBooks).Select(gb => gb.Book);
            }

            var books = await query.Select(b => new BookListDto(b.Id, b.Title, b.Image, new AuthorDto(b.Author.Name, b.Author.Slug))).OrderBy(b => Guid.NewGuid()).Take(count).ToArrayAsync();

            if (books.Length < count)
            {
                var alreadyFetchedBookIds = books.Select(b => b.Id);
                var additionalBooks = await context.Books
                    .Where(b => !alreadyFetchedBookIds.Contains(b.Id))
                    .Select(b => new BookListDto(b.Id, b.Title, b.Image, new AuthorDto(b.Author.Name, b.Author.Slug)))
                    .OrderBy(b => Guid.NewGuid())
                    .Take(count - books.Length)
                    .ToArrayAsync();

                books = [.. books, .. additionalBooks];
            }
            return books;


        }

        public async Task<BookDetailsDto> GetBookAsync(int bookId)
        {
            using var context = _dbContextFactory.CreateDbContext();
            var book = await context.Books.Where(b => b.Id == bookId)
                            .Select(b => new BookDetailsDto(b.Id, b.Title, b.Image,
                                        new AuthorDto(b.Author.Name, b.Author.Slug), b.NumPages, b.Format, b.Description, b.BookGenres
                                        .Select(bg => new GenreDto(bg.Genre.Name, bg.Genre.Slug)).ToArray(), b.BuyLink
                                        )
                            )
                            .FirstOrDefaultAsync();
            return book;
        }

        public async Task<BookListDto[]> GetSimilarBooksAsync(int bookId, int count)
        {
            using var context = _dbContextFactory.CreateDbContext();
            var similarBooks = await context.GenreBooks.Where(gb => gb.BookId == bookId)
                .SelectMany(gb => gb.Genre.GenreBooks)
                .Select(gb => gb.Book).Where(b => b.Id != bookId)
                .Select(b => new BookListDto(b.Id, b.Title, b.Image, new AuthorDto(b.Author.Name, b.Author.Slug)))
                .OrderBy(b => Guid.NewGuid()).Take(count)
                .ToArrayAsync();

            return similarBooks;

        }

        public async Task<PageResult<BookListDto>> GetBooksByAuthorAsync(int pageNo, int pageSize, string authorSlug)
        {
            using var context = _dbContextFactory.CreateDbContext();
            var query = context.Books.Where(b => b.Author.Slug == authorSlug);
            var totalCount = await query.CountAsync();
            var books = await query.Select(b => new BookListDto(b.Id, b.Title, b.Image, new AuthorDto(b.Author.Name, b.Author.Slug)))
                .OrderByDescending(b => b.Id)
                .Skip((pageNo - 1) * pageSize)
                .Take(pageSize)
                .ToArrayAsync();

            return new PageResult<BookListDto>(books, totalCount);


        }

    }
}
