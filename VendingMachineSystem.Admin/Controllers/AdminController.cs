using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Text;
using VendingMachineSystem.AppDbContextModels;
using VendingMachineSystem.Models.Admin;

namespace VendingMachineSystem.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public AdminController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> AdminList(int pageIndex = 1, int pageSize = 10)
        {
            PaginatedList<AdminUser> model = null;
            try
            {
                var count = await _dbContext.AdminUsers.CountAsync();
                var admins = await _dbContext.AdminUsers
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                model = new PaginatedList<AdminUser>(admins, count, pageIndex, pageSize);
            }
            catch (Exception ex)
            {
                model = null;
            }

            return View(model);
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> AdminRegister()
        {
            ViewBag.RoleList = await GetRoles();
            return View();
        }

        [HttpGet]
        public async Task<List<SelectListItem>> GetRoles()
        {
            var roleList = await _dbContext.AdminRoles.AsNoTracking().Select(r => new SelectListItem
            {
                Value = r.Id.ToString(),
                Text = r.Name
            }).ToListAsync();

            return roleList;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> AdminRegister(AdminRequestModel reqModel)
        {
            string respMessage;
            try
            {
                bool isExist = _dbContext.AdminUsers.Any(x => x.UserName.Equals(reqModel.UserName.Trim()));
                if (isExist)
                {
                    ViewBag.ErrorMessage = "Duplicate User";
                    ViewBag.RoleList = await GetRoles();
                    return View(reqModel);
                }

                using (var tran = await _dbContext.Database.BeginTransactionAsync())
                {
                    string password = PasswordGenerate.SHA256HexHashString(reqModel.UserName, reqModel.Password);
                    AdminUser adminuser = new AdminUser
                    {
                        Id = Guid.NewGuid().ToString(),
                        UserName = reqModel.UserName,
                        PasswordHash = password,
                        AccessFailedCount = 0,
                        Delflag = false
                    };

                    _dbContext.AdminUsers.Add(adminuser);
                    _dbContext.SaveChanges();

                    AdminUserRole userRole = new AdminUserRole
                    {
                        UserId = adminuser.Id,
                        RoleId = reqModel.UserRole,
                    };
                    _dbContext.AdminUserRoles.Add(userRole);
                    _dbContext.SaveChanges();

                    await tran.CommitAsync();
                }
            }
            catch (Exception ex)
            {
                return View(reqModel);
            }
            return RedirectToAction("AdminList");
        }

        [HttpGet]
        public IActionResult Signin(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View(new SigninRequestModel());
        }

        [HttpPost]
        public IActionResult Signin(SigninRequestModel reqModel)
        {
            string respMessage;

            try
            {
                string passwordHash = PasswordGenerate.SHA256HexHashString(reqModel.UserName, reqModel.Password);
                var adminUser = _dbContext.AdminUsers.FirstOrDefault(a => a.UserName == reqModel.UserName
                                              & a.PasswordHash == passwordHash
                                              & a.Delflag == false);
                if (adminUser is null)
                {
                    respMessage = "SignIn fail.";
                    goto Result;
                }
            }
            catch (Exception ex)
            {
                respMessage = "SignIn fail.";
                return View(reqModel);
            }
        Result:
            return RedirectToAction("AdminList", "Admin");
        }
    }
}
