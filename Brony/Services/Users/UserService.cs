using Brony.Constants;
using Brony.Domain;
using Brony.Helpers;
using Brony.Models.Users;

namespace Brony.Services.Users;

public class UserService : IUserService
{
    public void Register(UserRegisterModel model)
    {
        var users = FileHelper.ReadFromFile<User>(PathHolder.UsersFilePath);

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
            Password = PasswordHasher.HashPassword(model.Password),
        };
        
        users.Add(user);

        FileHelper.WriteToFile(PathHolder.UsersFilePath, users);
    }
    
    public int Login(UserLoginModel model)
    {
        var users = FileHelper.ReadFromFile<User>(PathHolder.UsersFilePath);
        
        var existUser = users.Find(u => u.PhoneNumber == model.PhoneNumber)
            ?? throw new Exception("Phone or password is incorrect.");
        
        if (PasswordHasher.Verify(model.Password, existUser.Password))
        {
            throw new Exception("Phone or password is incorrect.");
        }
    
        return existUser.Id;
    }

    public UserViewModel Get(int id)
    {
        var users = FileHelper.ReadFromFile<User>(PathHolder.UsersFilePath);
    
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
    
    public void Update(int id, UserUpdateModel model)
    {
        var users = FileHelper.ReadFromFile<User>(PathHolder.UsersFilePath);
    
        var existUser = users.Find(u => u.Id == id)
            ?? throw new Exception("User is not found.");
    
        var alreadyExistUser = users.Find(u => u.PhoneNumber == model.PhoneNumber);
        if (alreadyExistUser != null)
            throw new Exception("User already exists with this phone number.");

        existUser.FirstName = model.FirstName;
        existUser.LastName = model.LastName;
        existUser.PhoneNumber = model.PhoneNumber;

        FileHelper.WriteToFile(PathHolder.UsersFilePath, users);
    }
    
    public void Delete(int id)
    {
        var users = FileHelper.ReadFromFile<User>(PathHolder.UsersFilePath);
    
        var existUser = users.Find(u => u.Id == id)
            ?? throw new Exception("User is not found.");
    
        users.Remove(existUser);
    
        FileHelper.WriteToFile(PathHolder.UsersFilePath, users);
    }
    
    public List<UserViewModel> GetAll(string search)
    {
        var users = FileHelper.ReadFromFile<User>(PathHolder.UsersFilePath);
    
        if (!string.IsNullOrEmpty(search))
        {
            search = search.ToLower();
            
            users = users.Where(u =>
                u.FirstName.ToLower().Contains(search) ||
                u.LastName.ToLower().Contains(search))
                .ToList();
        }

        return users.Select(user => new UserViewModel()
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            PhoneNumber = user.PhoneNumber
        }).ToList();
    }
    
    public void ChangePassword(int userId, string oldPassword, string newPassword)
    {
        var users = FileHelper.ReadFromFile<User>(PathHolder.UsersFilePath);

        var existUser = users.Find(u => u.Id == userId)
            ?? throw new Exception("User is not found.");

        if (PasswordHasher.Verify(oldPassword, existUser.Password))
            throw new Exception("Passwords do not match.");
    
        existUser.Password = PasswordHasher.HashPassword(newPassword);
    
        FileHelper.WriteToFile(PathHolder.UsersFilePath, users);
    }
}