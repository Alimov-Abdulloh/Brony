using Brony.Domain;
using Brony.Models;
using Brony.Models.Users;

namespace Brony.Services.Users;

public interface IUserService
{
    void Register(UserRegisterModel model);

    int Login(UserLoginModel model);
    
    UserViewModel Get(int id);
    
    void Update(UserUpdateModel model);
    
    void Delete(int id);
    
    List<UserViewModel> GetAll(string search);
    
    void ChangePassword(int userId, string oldPassword, string newPassword);
}