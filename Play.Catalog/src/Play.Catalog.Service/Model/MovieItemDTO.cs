namespace Model
{
   public record MovieItemDTO(Guid id, string name);
   public record CreateMovieItemDTO(Guid id, string name);
   public record UpdateMovieItemDTO(Guid id, string name);
}