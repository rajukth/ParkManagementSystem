using Microsoft.AspNetCore.Mvc;
using ParkManagementSystem.ApplicationUser.Repositories.Interfaces;
using ParkManagementSystem.ApplicationUser.Services.Interfaces;
using ParkManagementSystem.Core.Entities.Organization;
using ParkManagementSystem.Web.Areas.Admin.ViewModels;
using ParkManagementSystem.Web.Attributes;
using ParkManagementSystem.Web.Helper;
using ParkManagementSystem.Web.Helpers;

namespace ParkManagementSystem.Web.Areas.Admin.Controllers;

[Area("Admin")]
[AppAuthorize("SysAdmin,Admin")]
public class OrganizationController : Controller
{
    private readonly IOrganizationRepository _organizationRepository;
    private readonly IOrganizationService _organizationService;

    private readonly IWebHostEnvironment _env;

    // GET
    public OrganizationController(IOrganizationRepository organizationRepository,
        IOrganizationService organizationService, IWebHostEnvironment env)
    {
        _organizationRepository = organizationRepository;
        _organizationService = organizationService;
        _env = env;
    }

    public async Task<IActionResult> Index()
    {
        var orgDetail = await _organizationRepository.GetOrganizationDetail();
        if (orgDetail == null)
        {
            return RedirectToAction(nameof(Register));
        }

        var vm = new OrgDetailVm
        {
            Name = orgDetail.Name,
            Address = orgDetail.Address,
            ContactNumber = orgDetail.ContactNumber,
            Email = orgDetail.Email,
            Slogan = orgDetail.Slogan,
            LogoUrl = orgDetail.LogoUrl,
            ContactPerson = orgDetail.ContactPerson,
            PanNumber = orgDetail.PanNumber,
            RegistrationNumber = orgDetail.RegistrationNumber,
        };
        return View(vm);
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View(new OrgDetailVm());
    }

    [HttpPost]
    public async Task<IActionResult> Register(OrgDetailVm vm)
    {
        if (!ModelState.IsValid)
        {
            return View(vm);
        }

        try
        {
            var org = new Organization
            {
                Name = vm.Name,
                Address = vm.Address,
                ContactNumber = vm.ContactNumber,
                Email = vm.Email,
                Slogan=vm.Slogan,
                LogoUrl = null,
                ContactPerson = vm.ContactPerson,
                PanNumber = vm.PanNumber,
                RegistrationNumber = vm.RegistrationNumber
            };
            await _organizationService.CreateAsync(org);
            if (vm.Logo != null)
            {
                var path = await ImageHelper.UploadImageAsync(vm.Logo, "uploads/org", _env);
                await _organizationService.UploadLogoAsync(path);
            }
        }
        catch (Exception e)
        {
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> UploadLogo(IFormFile? logo)
    {
        string? path = null;
        if (logo != null)
        {
            path = await ImageHelper.UploadImageAsync(logo, "uploads/org", _env);
        }

        await _organizationService.UploadLogoAsync(path);
        return RedirectToAction(nameof(Index));
    }
}