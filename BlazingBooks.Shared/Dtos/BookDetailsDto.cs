namespace BlazingBooks.Shared.Dtos
{
    public record BookDetailsDto(int Id, string Title, string Image, AuthorDto Author, int NumPages, string Format, string Description, GenreDto[] Genres, string? BuyLink);
}
