using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pasquinelli.Martina._5H.SecondaWeb.DTO;
using Pasquinelli.Martina._5H.SecondaWeb.Models;

namespace Pasquinelli.Martina._5H.SecondaWeb.Controllers
{
  public class AccountController : Controller
  {
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
    {
      _userManager = userManager;
      _signInManager = signInManager;
    }

    public IActionResult LogIn()
    {
      return View();
    }
    [HttpPost]
    public async Task<IActionResult> Login(LoginDTO user)
    {
      if (ModelState.IsValid)
      {
        var result = await _signInManager.PasswordSignInAsync(user.Email, user.Password, user.RememberMe, false);

        if (result.Succeeded)
        {
          // Se l'utente fa login correttamente, entra.
          return RedirectToAction("Prova", "Home");
        }
        else
        {

          // ...altrimenti meglio non dare troppo info a chi ci prova
          // meglio un generico errore,
          ModelState.AddModelError(string.Empty, "Login error");
        }
      }
      return View(user);
    }


    public IActionResult SignIn()
    {
      return View();
    }

    [HttpPost]
    public async Task<IActionResult> SignIn(SigInDTO model)
    {
      if (ModelState.IsValid)
      {
        var user = new IdentityUser
        {
          UserName = model.Email,
          Email = model.Email,
        };

        var result = await _userManager.CreateAsync(user, model.Password);

        if (result.Succeeded)
        {
          //await _signInManager.SignInAsync(user, isPersistent: false);
          return RedirectToAction("Prova", "Home");
        }
        foreach (var error in result.Errors)
          ModelState.AddModelError("", error.Description);

        ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
      }
      return View(model);
    }
    public async Task<IActionResult> Logout()
    {
      await _signInManager.SignOutAsync();
      return RedirectToAction("Index", "Home");
    }

  }
}
