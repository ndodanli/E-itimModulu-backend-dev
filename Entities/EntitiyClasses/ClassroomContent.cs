namespace Entities
{
    public class ClassroomContent
    {
        public int ClassroomId { get; set; }
        public Classroom Classroom { get; set; }
        public int ContentId { get; set; }
        public Content Content { get; set; }
    }
}