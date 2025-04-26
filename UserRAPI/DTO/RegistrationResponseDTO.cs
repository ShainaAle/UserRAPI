namespace UserRAPI.DTO
{
    public class RegistrationResponseDTO
    {
        public bool IsSuccessful { get; set; }
        public IEnumerable<string>? Errors { get; set; }
    }
}
