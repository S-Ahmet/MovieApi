using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApi.Domain.Entities
{
    public class Media
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public string VideoThumbnail { get; set; }
        public string VideoUrl { get; set; }
        public string Title { get; set; }
        public string Duration { get; set; }

        public ICollection<MediaPhoto> MediaPhotos { get; set; }
        public Movie Movie { get; set; }
    }

}
