using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApi.Dto.Dtos.MovieDtos
{
    /// <summary>
    /// MediaPhoto tablosundaki tek bir fotoğrafın temel aktarım nesnesi.
    /// </summary>
    public class ResultPhotoDto
    {
        public int PhotoId { get; set; }   // MediaPhoto.Id
        public int MediaId { get; set; }   // Bağlı olduğu Media(Id)
        public string Url { get; set; }   // PhotoUrl
    }
}
