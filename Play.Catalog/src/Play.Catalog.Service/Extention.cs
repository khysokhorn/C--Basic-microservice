using Model;

namespace Entities
{
    public static class Extention
    {
        public static MovieItemDTO AsDto(this MovieItem item)
        {
            return new MovieItemDTO(id: item.Id, name: item.Title, Imdb: item.Imdb);
        }
    }
}