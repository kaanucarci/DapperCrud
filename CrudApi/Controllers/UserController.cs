using CrudApi.Models;
using CrudApi.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CrudApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserRepository _repository;

    public UserController(IUserRepository repository)
    {
        _repository = repository;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var users = await _repository.GetAll();
        return Ok(users);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var user = await _repository.GetById(id);
        if (user == null) return NotFound();
        return Ok(user);
    }
    
    [HttpPost]
    public async Task<IActionResult> Create(Users user)
    {
        await _repository.Create(user);
        return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Users user)
    {
        var existingUser = await _repository.GetById(id);
        if (existingUser == null) return NotFound();

        user.Id = id;
        await _repository.Update(user);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var user = await _repository.GetById(id);
        if (user == null) return NotFound();

        await _repository.Delete(id);
        return NoContent();
    }
 
}