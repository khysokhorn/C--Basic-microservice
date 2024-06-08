namespace Model
{
   public record MovieItemDTO(string id, string name, string Imdb);
   public record CreateMovieItemDTO(Guid id, string name);
   public record UpdateMovieItemDTO(Guid id, string name);
}