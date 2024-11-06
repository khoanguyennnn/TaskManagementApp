using System.Runtime.CompilerServices;
using TaskManagementApp.Dtos.User;
using TaskManagementApp.Models;

namespace TaskManagementApp.Mappers
{
    public static class UserMappers
    {
        public static UserDto ToUserDto(this User userModel)
        {
            return new UserDto
            {
                Id = userModel.Id,
                Name = userModel.Name,
                Email = userModel.Email,
                Tasks = userModel.Tasks.Select(c => c.ToTaskDto()).ToList(),
            };
        }
    }
}
