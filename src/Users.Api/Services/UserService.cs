using System;
using Bogus;
using Users.Api.Models;

namespace Users.Api.Services
{
    public interface IUserService
    {
        /// <summary>
        /// Search a user by id.
        /// </summary>
        /// <param name="id">Id of the user.</param>
        /// <returns>An user if there is a user with given id. If no user found, return null.</returns>
        User? GetUser(Guid id);
    }

    public class FakeUserService : IUserService
    {
        private Faker<User> _faker { get; }

        public FakeUserService()
        {
            _faker = new Faker<User>();
        }

        public User? GetUser(Guid id)
        {
            if (id == Guid.Empty)
            {
                return null;
            }
            return _faker
                .RuleFor(u => u.Id, f => id)
                .RuleFor(u => u.FirstName, f => f.Name.FirstName())
                .RuleFor(u => u.FamilyName, f => f.Name.LastName())
                .Generate();
        }
    }
}