using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentPortal.Data;
using StudentPortal.Models;
using StudentPortal.Models.Entieis;

namespace StudentPortal.Controllers;

public class StudentsController : Controller
{
    private readonly AplicationDbContext dbcontext;

    public StudentsController(AplicationDbContext dbContext)
    {
        this.dbcontext = dbContext;
    }
    [HttpGet]
    public IActionResult Add()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Add(AddStudentViewModel viewModel)
    {
        var student = new students
        {
            Name = viewModel.Name,
            Email = viewModel.Email,
            Phone = viewModel.Phone
        };
        
        await dbcontext.Students.AddAsync(student);
        await dbcontext.SaveChangesAsync();
        
        return RedirectToAction("List","Students");
    }

    [HttpGet]
    public async Task<IActionResult> List()
    {
        var students = await dbcontext.Students.ToListAsync();
        
        return View(students);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int Id)
    {
        var foundstudent = await dbcontext.Students.FindAsync(Id);
        return View(foundstudent);
    }
    
    [HttpPost]
    public async Task<IActionResult> Edit(students viewModel)
    {
        
        var student = await dbcontext.Students.FindAsync(viewModel.Id);

        if (student != null)
        {
            student.Name = viewModel.Name;
            student.Phone = viewModel.Phone;
            student.Email = viewModel.Email;
            
            await dbcontext.SaveChangesAsync();
        }
        
        return RedirectToAction("List", "Students");
    }

    public async Task<IActionResult> Delete(int Id)
    {
        var students = await dbcontext.Students.FindAsync(Id);
        if (students is not null)
        {
            dbcontext.Students.Remove(students);
            await dbcontext.SaveChangesAsync();
        }

        return RedirectToAction("List", "Students");
    }
}