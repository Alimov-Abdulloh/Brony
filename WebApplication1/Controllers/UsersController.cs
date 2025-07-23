using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers;


[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private List<User> users = new();

    [HttpGet("{id:int}")]
    public IActionResult Get(int id)
    {
        return Ok(users.Find(t => t.Id == id));
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(users);
    }
    
    [HttpPost]
    public IActionResult Create(User user)
    {
        users.Add(user);
        return Ok();
    }

    [HttpPut("{id:int}")]
    public IActionResult Update(int id, User user)
    {
        var userToUpdate = users.Find(t => t.Id == id); 
        
        userToUpdate.Name = user.Name;
        
        return Ok();
    }
    
    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        var userToDelete = users.Find(t => t.Id == id);
        users.Remove(userToDelete);
        return Ok();
    }
}