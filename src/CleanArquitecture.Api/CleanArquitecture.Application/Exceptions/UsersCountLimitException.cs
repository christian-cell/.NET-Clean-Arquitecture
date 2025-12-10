namespace CleanArquitecture.Application.Exceptions
{
    public class UsersCountLimitException: Exception
    {
        public UsersCountLimitException(int number, string entity): base($"Has reached the limit number {number} of {entity}"){}
    }
};

