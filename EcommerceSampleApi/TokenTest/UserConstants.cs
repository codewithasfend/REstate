namespace TokenTest
{
    public class UserConstants
    {
        public static List<UserModel> Users = new List<UserModel>()
        {
            new UserModel() { Username = "jason_admin", EmailAddress = "jason.admin@email.com", Password = "MyPass_w0rd" },
            new UserModel() { Username = "elyse_seller", EmailAddress = "elyse.seller@email.com", Password = "MyPass_w0rd" },
        };
    }
}
