using GameDayShiftScheduler.Data;
using GameDayShiftScheduler.Models;
using GameDayShiftScheduler.Models.Input;
using GameDayShiftScheduler.Models.InputForms;
using GameDayShiftScheduler.Models.Messages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GameDayShiftScheduler.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleStore<IdentityRole> _roleStore;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UsersController(ApplicationDbContext context,
                               UserManager<ApplicationUser> userManager,
                               RoleStore<IdentityRole> roleStore,
                               RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleStore = roleStore;
            _roleManager = roleManager;
        }

        // GET: UsersController
        public async Task<IActionResult> Index(string? role)
        {
            try
            {
                List<ApplicationUser> users;
                if (role != null)
                {
                    var usersInRole = await _userManager.GetUsersInRoleAsync(role);
                    users = usersInRole.ToList();
                }
                else
                    users = _userManager.Users.ToList();

                var roles = _roleManager.Roles.ToList();
                ViewBag.Roles = new SelectList(roles, "Name", "Name");

                return View(users);
            }
            catch (Exception ex)
            {
                var errorMessage = new ErrorViewModel
                {
                    Description = ex.Message
                };
                return View("Error", errorMessage);
            }
        }

        // GET: UsersController/Details/5
        public async Task<IActionResult> Details(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
                return NotFound();
            
            return View(user);
        }

        // GET: UsersController/Create
        public IActionResult Create(Guid organizationId)
        {
            ViewBag.OrganizationId = organizationId;
            ViewBag.Roles = _roleManager.Roles.ToList();

            return View();
        }

        // POST: UsersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserCreateForm form)
        {
            try
            {
                // If the two passwords match...
                if (form.Password == form.ConfirmPassword)
                {
                    // Create a new user
                    var user = new ApplicationUser();

                    // Set its organization ID to the one passed into the form
                    user.OrganizationId = form.OrganizationId;
                    user.Organization = _context.Organizations.FirstOrDefault(o => o.Id == form.OrganizationId)!;
                    
                    // Set the user's username and email address to the email address passed in from the form
                    await _userManager.SetUserNameAsync(user, form.EmailAddress);
                    await _userManager.SetEmailAsync(user, form.EmailAddress);

                    // Add the user to the database
                    var result = await _userManager.CreateAsync(user, form.Password);
                    // If this operation succeeded...
                    if (result.Succeeded)
                    {
                        // For each role the user is being added to...
                        foreach (var role in form.Roles)
                        {
                            // Add the user to the current role
                            result = await _userManager.AddToRoleAsync(user, role);

                            // If there was an error in adding the user to the current role...
                            if (!result.Succeeded)
                            {
                                // Send a message to the view stating the error
                                ViewBag.Message = new DangerMessage($"There was an error in adding the user to the '{role}' role.");
                                return View();
                            }
                        }   

                        // If we've reached the end of the loop, the user has been successfully created and
                        // added to their necessary roles
                        ViewBag.Message = new SuccessMessage($"Successfully created '{user.FullName}'!");
                    }
                }
                // If the two passwords DO NOT match...
                else
                {
                    // Send a message to the view stating that the passwords don't match
                    ViewBag.Message = new DangerMessage("The passwords do not match. Please try again.");

                    // Send the available roles and organization ID back to the view
                    ViewBag.Roles = _roleManager.Roles.ToList();
                    ViewBag.OrganizationId = form.OrganizationId;

                    // Return the Create form again
                    return View();
                }
                
                // If we are here, the user has been created successfully and we can now return to the Users Index page
                return RedirectToAction(nameof(Index));
            }
            // If an exception occurs...
            catch (Exception ex)
            {
                // Send the error message to the Error page
                var message = new ErrorViewModel { Description = ex.Message };
                return View("Error", message);
            }
        }

        // GET: UsersController/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id);

                if (user == null)
                    return NotFound();

                return View(user);
            }
            catch (Exception ex)
            {
                var message = new ErrorViewModel
                {
                    Description = ex.Message
                };
                return View("Error", message);
            }
        }

        // POST: UsersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, UserEditForm form)
        {
            try
            {
                // Find the user by their ID
                var user = await _userManager.FindByIdAsync(id);
                if (user == null)
                    return NotFound();

                // Get their current roles
                var currentRoles = await _userManager.GetRolesAsync(user);

                // For each role the user is currently in...
                foreach (var role in currentRoles)
                {
                    // If the user is being removed from this role, remove them from the role
                    if (!form.Roles.Contains(role))
                        await _userManager.RemoveFromRoleAsync(user, role);
                }

                // For each of the roles passed in from the form...
                foreach (var role in form.Roles)
                {
                    // If the user is not in the current role, add them to that role
                    if (!await _userManager.IsInRoleAsync(user, role))
                        await _userManager.AddToRoleAsync(user, role);
                }
                
                // Return to the Users Index page
                return RedirectToAction(nameof(Index));
            }
            // If an exception occurs, simply return to the Edit page
            catch
            {
                return View();
            }
        }

        // GET: UsersController/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var user = _userManager.FindByIdAsync(id);
                if (user == null)
                    return NotFound();

                return View(user);
            }
            catch (Exception ex)
            {
                var message = new ErrorViewModel
                {
                    Description = ex.Message
                };
                return View("Error", message);
            }
        }

        // POST: UsersController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id, IFormCollection collection)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id);
                if (user == null)
                    return NotFound();

                var result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    ViewBag.Message = new SuccessMessage("The user has been successfully deleted!");
                }
                
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                var message = new ErrorViewModel { Description = ex.Message };
                return View("Error", message);
            }
        }

        public async Task<IActionResult> Roles(string? search, 
                                               string sort = "first-name", 
                                               string order = "asc")
        {
            Dictionary<ApplicationUser, List<bool>> rolesDictionary = new();

            var roles = _roleManager.Roles
                .OrderBy(r => r.Name)
                .ToList();
            ViewBag.Roles = roles;
            
            var users = _userManager.Users;
            if (search != null)
                users = users.Where(u => u.FirstName.Contains(search) || u.LastName.Contains(search));

            switch (order)
            {
                case "desc":
                    switch (sort)
                    {
                        case "last-name":
                            users = users.OrderBy(u => u.LastName)
                                         .ThenBy(u => u.FirstName);
                            break;

                        default:
                            users = users.OrderBy(u => u.FirstName)
                                         .ThenBy(u => u.LastName);
                            break;
                    }

                    break;

                default:
                    switch (sort)
                    {
                        case "last-name":
                            users = users.OrderByDescending(u => u.LastName)
                                         .ThenByDescending(u => u.FirstName);
                            break;

                        default:
                            users = users.OrderByDescending(u => u.FirstName)
                                         .ThenByDescending(u => u.LastName);
                            break;
                    }

                    break;
            }

            foreach (var user in users)
            {
                List<bool> userRoles = new();
                foreach (var role in roles)
                {
                    bool userIsInRole = await _userManager.IsInRoleAsync(user, role.Name!);
                    userRoles.Add(userIsInRole);
                }

                rolesDictionary.Add(user, userRoles);
            }

            return View(rolesDictionary);
        }
    }
}
