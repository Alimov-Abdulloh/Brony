using Brony.Constants;
using Brony.Domain;
using Brony.Extensions;
using Brony.Helpers;
using Brony.Models;
using Brony.Models.Users;

namespace Brony.Services.Users;

public class UserService : IUserService
{
    public void Register(UserRegisterModel model)
    {
        var text = FileHelper.ReadFromFile(PathHolder.UsersFilePath);

        var users = text.ToUser();

        var existUser = users.Find(u => u.PhoneNumber == model.PhoneNumber);
        if (existUser != null)
        {
            throw new Exception("User with this phone number already exists.");
        }

        var user = new User
        {
            FirstName = model.FirstName,
            LastName = model.LastName,
            PhoneNumber = model.PhoneNumber,
            Password = model.Password
        };
        
        users.Add(user);

        FileHelper.WriteToFile(PathHolder.UsersFilePath, users.ConvertToString());
    }
    
    public int Login(UserLoginModel model)
    {
        var text = FileHelper.ReadFromFile(PathHolder.UsersFilePath);
    
        var users = text.ToUser();
        
        var existUser = users.Find(u => u.PhoneNumber == model.PhoneNumber)
            ?? throw new Exception("Phone or password is incorrect.");
        
        if (existUser.Password != model.Password)
        {
            throw new Exception("Phone or password is incorrect.");
        }
    
        return existUser.Id;
    }

    public UserViewModel Get(int id)
    {
        var text = FileHelper.ReadFromFile(PathHolder.UsersFilePath);
    
        var users = text.ToUser();
    
        var existUser = users.Find(u => u.Id == id)
            ?? throw new Exception("User is not found.");
    
        return new UserViewModel()
        {
            Id = existUser.Id,
            FirstName = existUser.FirstName,
            LastName = existUser.LastName,
            PhoneNumber = existUser.PhoneNumber
        };
    }
    
    public void Update(UserUpdateModel model)
    {
        var text = FileHelper.ReadFromFile(PathHolder.UsersFilePath);
    
        var users = text.ToUser();
    
        var existUser = users.Find(u => u.Id == model.Id)
            ?? throw new Exception("User is not found.");
    
        var alreadyExistUser = users.Find(u => u.PhoneNumber == model.PhoneNumber);
        
        if (alreadyExistUser != null)
        {
            throw new Exception("User already exists with this phone number.");
        }

        existUser.FirstName = model.FirstName;
        existUser.LastName = model.LastName;
        existUser.PhoneNumber = model.PhoneNumber;

        FileHelper.WriteToFile(PathHolder.UsersFilePath, users.ConvertToString());
    }
    
    public void Delete(int id)
    {
        var text = FileHelper.ReadFromFile(PathHolder.UsersFilePath);
    
        var users = text.ToUser();
    
        var existUser = users.Find(u => u.Id == id)
            ?? throw new Exception("User is not found.");
    
        users.Remove(existUser);
    
        FileHelper.WriteToFile(PathHolder.UsersFilePath, users.ConvertToString());
    }
    
    public List<UserViewModel> GetAll(string search)
    {
        var text = FileHelper.ReadFromFile(PathHolder.UsersFilePath);
    
        var users = text.ToUser();

        var result = new List<UserViewModel>();

        if (!string.IsNullOrEmpty(search))
        {
            users = Search(users, search);
        }
        
        foreach (var user in users)
        {
            result.Add(new UserViewModel()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber
            });
        }
        
        return result;
    }
    
    public void ChangePassword(int userId, string oldPassword, string newPassword)
    {
        var text = FileHelper.ReadFromFile(PathHolder.UsersFilePath);
    
        var users = text.ToUser();
    
        var existUser = users.Find(u => u.Id == userId)
            ?? throw new Exception("User is not found.");

        if (existUser.Password != oldPassword)
            throw new Exception("Passwords do not match.");
    
        existUser.Password = newPassword;
    
        FileHelper.WriteToFile(PathHolder.UsersFilePath, users.ConvertToString());
    }
    
    private List<User> Search(List<User> users, string search)
    {
        var result = new List<User>();
    
        if (!string.IsNullOrEmpty(search))
        {
            foreach (var user in users)
            {
                if (
                    user.FirstName.ToLower().Contains(search.ToLower()) ||
                    user.LastName.ToLower().Contains(search.ToLower()) ||
                    user.PhoneNumber.ToLower().Contains(search.ToLower()))
                {
                    result.Add(user);
                }
            }
        }
    
        return result;
    }
}
