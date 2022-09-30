namespace BookStore.Services
{
    public interface IUserService
    {
        string GetLoggedInUserId();
        bool IsAuthenticated();
    }
}