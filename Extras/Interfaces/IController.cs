using Microsoft.AspNetCore.Mvc;

namespace LeCiel.Extras.Interfaces;

public interface IController<T, U>
    where T : class
    where U : class
{
    Task<IActionResult> Create(T createDto);
    Task<IActionResult> Index(int page, int pageSize);
    Task<IActionResult> Find(uint id);
    Task<IActionResult> Update(uint id, U updateDto);
    Task<IActionResult> Delete(uint id);
}
