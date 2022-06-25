using TVShowTraker.Models.ViewModels;

namespace TVShowTraker.Models.Mappers
{
    public static class UserMapper
    {
        public static UserVM ParseModelToVM(User user) => new UserVM()
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            Username = user.Username,
            Password = user.Password
        };

        public static User ParseVMToModel(UserVM vm) => new User()
        {
            Id = vm.Id,
            Name = vm.Name,
            Email = vm.Email,
            Username = vm.Username,
            Password = vm.Password
        };
    }
}