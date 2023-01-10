namespace E_Commerce.Data.ViewModels
{
    public class LoginVM
    {
        
        public string EmailAddress { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
