namespace FreshHub_BE.Validation
{
    public class ErrorCode
    {
        public static string Empty { get; } = nameof(Empty);
        public static string Length { get; } = nameof(Length);
        public static string Negative { get; } = nameof(Negative);
       
    }
}
