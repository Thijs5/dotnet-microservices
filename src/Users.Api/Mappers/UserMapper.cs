namespace Users.Api.Mappers
{
    public interface IUserMapper
    {
        Models.User? Map(Persistence.Models.User? user);
        Persistence.Models.User Map(Models.NewUser user);
    }

    public class UserMapper : IUserMapper
    {
        public Models.User? Map(Persistence.Models.User? user)
        {
            if (user is null) { return null; }
            return new Models.User
            {
                Id = user.Id,
                FirstName = user.FirstName,
                FamilyName = user.LastName,
            };
        }

        public Persistence.Models.User Map(Models.NewUser user)
        {
            return new Persistence.Models.User
            {
                FirstName = user.FirstName,
                LastName = user.FamilyName,
            };
        }
    }
}