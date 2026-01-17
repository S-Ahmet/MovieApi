using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApi.Domain.Entities
{
    public class MediaPhoto
    {
        public int Id { get; set; }
        public int MediaId { get; set; }
        public string PhotoUrl { get; set; }

        public Media Media { get; set; }
    }

}
