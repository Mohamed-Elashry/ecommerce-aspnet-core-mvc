namespace E_Commerce.Data.ViewModels
{
    public class RegisterVM
    {
        public string FullName { get; set; }
        public string EmailAddress { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
