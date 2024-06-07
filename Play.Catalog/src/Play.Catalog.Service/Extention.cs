using Model;

namespace Entities
{
    public static class Extention
    {
        public static MovieItemDTO AsDto(this MovieItem item)
        {
            return new MovieItemDTO(id: Guid.NewGuid(), name: item.Title);
        }
    }
}