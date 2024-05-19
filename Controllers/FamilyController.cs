using _334_group_project_web_api.Helpers;
using _334_group_project_web_api.Models;
using Microsoft.AspNetCore.Mvc;

namespace _334_group_project_web_api.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class FamilyController : Controller
{
    private readonly FamilyService _familyService;

    public FamilyController(FamilyService familyService)
    {
        _familyService = familyService;
    }

    [HttpGet("{adminId:length(24)}")]
    public async Task<ActionResult<Family>> GetFamilyId(string adminId)
    {
        var family = await _familyService.GetFamilyById(adminId);
        return family;
    }
        


}