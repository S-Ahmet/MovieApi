namespace MovieApi.Dto.Dtos.MovieDtos
{
    public class ResultReviewDto
    {
        public int ReviewID { get; set; } 

        public string ReviewComment { get; set; }
        public int UserRating { get; set; }
        public DateTime ReviewDate { get; set; }
        public string ReviewerName { get; set; }
        public int StarCount { get; set; }
        public int MovieId { get; set; }
    }

}
