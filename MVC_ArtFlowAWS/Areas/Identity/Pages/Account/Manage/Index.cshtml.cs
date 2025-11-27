// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MVC_ArtFlowAWS.Areas.Identity.Data;

namespace MVC_ArtFlowAWS.Areas.Identity.Pages.Account.Manage
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<MVC_ArtFlowAWSUser> _userManager;
        private readonly SignInManager<MVC_ArtFlowAWSUser> _signInManager;

        public IndexModel(
            UserManager<MVC_ArtFlowAWSUser> userManager,
            SignInManager<MVC_ArtFlowAWSUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        /// <summary>
        ///       This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///       directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        ///       This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///       directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [TempData]
        public string StatusMessage { get; set; }

        /// <summary>
        ///       This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///       directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///       This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///       directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            /// <summary>
            ///       This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///       directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }


            [Required(ErrorMessage = "You must enter the name first before submitting your form!")]
            [StringLength(256, ErrorMessage = "You must enter the value between 6 - 256 chars", MinimumLength = 6)]
            [Display(Name = "Your Full Name")] //label
            public string customerfullname { get; set; }
            [Required]
            [Display(Name = "Your DOB")]
            [DataType(DataType.Date)]
            public DateTime DoB { get; set; }
            [Required(ErrorMessage = "You must enter the age first before submitting your form!")]
            [Range(18, 100, ErrorMessage = "You must be 18 years old when register this member!")]
            [Display(Name = "Your Age")] //label
            public int age { get; set; }
            [Required]
            [DataType(DataType.MultilineText)]
            [Display(Name = "Your Address")]
            public string Address { get; set; }
        }

        private async Task LoadAsync(MVC_ArtFlowAWSUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

            Username = userName;

            Input = new InputModel
            {
                PhoneNumber = phoneNumber,
                customerfullname = user.CustomerFullName,
                age = user.CustomerAge,
                Address = user.CustomerAddress,
                DoB = user.CustomerDOB

            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            // --- Start of Fixes ---

            // 1. Phone number logic was missing closing braces and had a logical error
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);

                if (!setPhoneResult.Succeeded)
                {
                    // If setting phone number fails, set status message and redirect/return page.
                    // The logic below was moved from outside the if/else structure.
                    StatusMessage = "Unexpected error when trying to set phone number.";
                    await LoadAsync(user); // Re-load data if returning to page
                    return Page();
                }
            }

            // 2. Update user properties
            bool changesMade = false;

            if (Input.customerfullname != user.CustomerFullName)
            {
                user.CustomerFullName = Input.customerfullname;
                changesMade = true;
            }
            if (Input.DoB != user.CustomerDOB)
            {
                user.CustomerDOB = Input.DoB;
                changesMade = true;
            }
            if (Input.Address != user.CustomerAddress)
            {
                user.CustomerAddress = Input.Address;
                changesMade = true;
            }
            if (Input.age != user.CustomerAge)
            {
                user.CustomerAge = Input.age;
                changesMade = true;
            }

            // 3. Update the user in the database only if necessary
            if (changesMade)
            {
                var updateResult = await _userManager.UpdateAsync(user);
                if (!updateResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to update user profile.";
                    await LoadAsync(user);
                    return Page();
                }
            }

            // The stray brace and code block (which contained the phone error message)
            // was removed from here. The phone error handling is now correctly placed
            // inside the phone check block.

            // --- End of Fixes ---

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}