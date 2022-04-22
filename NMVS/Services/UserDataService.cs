﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NMVS.Common;
using NMVS.Models;
using NMVS.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NMVS.Services
{
    public class UserDataService : IUserDataService
    {
        private readonly ApplicationDbContext _db;
        RoleManager<ApplicationRole> _roleManager;
        UserManager<ApplicationUser> _userManager;

        public UserDataService(ApplicationDbContext db, UserManager<ApplicationUser> userManager, 
            RoleManager<ApplicationRole> roleManager)
        {
            _db = db;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public List<UserVm> GetUserList()
        {
            List<UserVm> userList = (from user in _db.Users.Where(x => x.UserName != "nmvadmin")
                                     select new UserVm
                                     {
                                         UserEmail = user.Email,
                                         UserName = user.UserName,
                                         FullName = user.FullName,
                                         Active = user.Active
                                     }).ToList();

            return userList;

        }

        public List<UserRoleVm> GetUserRoleList()
        {
            var users = _userManager.Users.Where(x => x.UserName != "nmvadmin")
            .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role).AsNoTracking();

            var userRolesList2 = (from user in users
                                  select new
                                  {
                                      Username = user.UserName,
                                      UserLock = user.Active,
                                      RoleNames = (from userRole in user.UserRoles
                                                   select userRole.Role.Name).ToList()
                                  }).ToList().Select(p => new UserRoleVm()

                                  {
                                      UserName = p.Username,
                                      Guard = p.RoleNames.Contains(Helper.Guard),
                                      QC = p.RoleNames.Contains(Helper.QC),
                                      AppSO = p.RoleNames.Contains(Helper.AppSO),
                                      CreateSO = p.RoleNames.Contains(Helper.CreateSO),
                                      RequestInv = p.RoleNames.Contains(Helper.RequestInv),
                                      HandleRequest = p.RoleNames.Contains(Helper.HandleRequest),
                                      ReceiveInv = p.RoleNames.Contains(Helper.ReceiveInv),
                                      RegVehicle = p.RoleNames.Contains(Helper.RegVehicle),
                                      UserManagement = p.RoleNames.Contains(Helper.UserManagement),
                                      ArrangeInventory = p.RoleNames.Contains(Helper.ArrangeInventory),
                                      MoveInv = p.RoleNames.Contains(Helper.MoveInventory),
                                      WoReporter = p.RoleNames.Contains(Helper.WoReporter),
                                      WoCreation = p.RoleNames.Contains(Helper.WoCreation),
                                      Active = p.UserLock
                                  }).ToList();

            return userRolesList2;
        }

        public UserRoleVm GetUserRole(string userName)
        {
            var users = _userManager.Users
            .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role).AsNoTracking().Where(x => x.UserName == userName);

            var userRolesList2 = (from user in users
                                  select new
                                  {
                                      Username = user.UserName,
                                      UserLock = user.Active,
                                      RoleNames = (from userRole in user.UserRoles
                                                   select userRole.Role.Name).ToList()
                                  }).ToList().Select(p => new UserRoleVm()

                                  {
                                      UserName = p.Username,
                                      Guard = p.RoleNames.Contains(Helper.Guard),
                                      QC = p.RoleNames.Contains(Helper.QC),
                                      AppSO = p.RoleNames.Contains(Helper.AppSO),
                                      CreateSO = p.RoleNames.Contains(Helper.CreateSO),
                                      RequestInv = p.RoleNames.Contains(Helper.RequestInv),
                                      HandleRequest = p.RoleNames.Contains(Helper.HandleRequest),
                                      ReceiveInv = p.RoleNames.Contains(Helper.ReceiveInv),
                                      RegVehicle = p.RoleNames.Contains(Helper.RegVehicle),
                                      UserManagement = p.RoleNames.Contains(Helper.UserManagement),
                                      ArrangeInventory = p.RoleNames.Contains(Helper.ArrangeInventory),
                                      MoveInv = p.RoleNames.Contains(Helper.MoveInventory),
                                      WoReporter = p.RoleNames.Contains(Helper.WoReporter),
                                      WoCreation = p.RoleNames.Contains(Helper.WoCreation),
                                      Active = p.UserLock
                                  }).FirstOrDefault();

            return userRolesList2;
        }

        public async Task InitRoleAsync()
        {
            if (!_roleManager.RoleExistsAsync(Helper.UserManagement).GetAwaiter().GetResult())
            {
                await _roleManager.CreateAsync(new ApplicationRole(Helper.UserManagement));
            }

            if (!_roleManager.RoleExistsAsync(Helper.MoveInventory).GetAwaiter().GetResult())
            {
                await _roleManager.CreateAsync(new ApplicationRole(Helper.MoveInventory));
            }

            if (!_roleManager.RoleExistsAsync(Helper.ArrangeInventory).GetAwaiter().GetResult())
            {
                await _roleManager.CreateAsync(new ApplicationRole(Helper.ArrangeInventory));
            }

            if (!_roleManager.RoleExistsAsync(Helper.Guard).GetAwaiter().GetResult())
            {
                await _roleManager.CreateAsync(new ApplicationRole(Helper.Guard));
            }

            if (!_roleManager.RoleExistsAsync(Helper.ReceiveInv).GetAwaiter().GetResult())
            {
                await _roleManager.CreateAsync(new ApplicationRole(Helper.ReceiveInv));
            }

            if (!_roleManager.RoleExistsAsync(Helper.QC).GetAwaiter().GetResult())
            {
                await _roleManager.CreateAsync(new ApplicationRole(Helper.QC));
            }

            if (!_roleManager.RoleExistsAsync(Helper.HandleRequest).GetAwaiter().GetResult())
            {
                await _roleManager.CreateAsync(new ApplicationRole(Helper.HandleRequest));

            }

            if (!_roleManager.RoleExistsAsync(Helper.RequestInv).GetAwaiter().GetResult())
            {
                await _roleManager.CreateAsync(new ApplicationRole(Helper.RequestInv));
            }

            if (!_roleManager.RoleExistsAsync(Helper.AppSO).GetAwaiter().GetResult())
            {
                await _roleManager.CreateAsync(new ApplicationRole(Helper.AppSO));
            }

            if (!_roleManager.RoleExistsAsync(Helper.CreateSO).GetAwaiter().GetResult())
            {
                await _roleManager.CreateAsync(new ApplicationRole(Helper.CreateSO));
            }

            if (!_roleManager.RoleExistsAsync(Helper.RegVehicle).GetAwaiter().GetResult())
            {
                await _roleManager.CreateAsync(new ApplicationRole(Helper.RegVehicle));
            }

            if (!_roleManager.RoleExistsAsync(Helper.WoCreation).GetAwaiter().GetResult())
            {
                await _roleManager.CreateAsync(new ApplicationRole(Helper.WoCreation));
            }

            if (!_roleManager.RoleExistsAsync(Helper.WoReporter).GetAwaiter().GetResult())
            {
                await _roleManager.CreateAsync(new ApplicationRole(Helper.WoReporter));
            }

            if (!_roleManager.RoleExistsAsync("SuperUser").GetAwaiter().GetResult())
            {
                await _roleManager.CreateAsync(new ApplicationRole("SuperUser"));
            }
        }

        public async Task SeedingRole(UserRoleVm usr)
        {

            ApplicationUser user = await _userManager.FindByNameAsync(usr.UserName);

            //1. Guard
            if (usr.Guard)
            {
                if (!await _userManager.IsInRoleAsync(user, Helper.Guard))
                {
                    await _userManager.AddToRoleAsync(user, Helper.Guard);
                }
            }
            else
            {
                if (await _userManager.IsInRoleAsync(user, Helper.Guard))
                {
                    await _userManager.RemoveFromRoleAsync(user, Helper.Guard);
                }
            }

            //2. User management
            if (usr.UserManagement)
            {
                if (!await _userManager.IsInRoleAsync(user, Helper.UserManagement))
                {
                    await _userManager.AddToRoleAsync(user, Helper.UserManagement);
                }
            }
            else
            {
                if (await _userManager.IsInRoleAsync(user, Helper.UserManagement))
                {
                    await _userManager.RemoveFromRoleAsync(user, Helper.UserManagement);
                }
            }

            //3. Request
            if (usr.RequestInv)
            {
                if (!await _userManager.IsInRoleAsync(user, Helper.RequestInv))
                {
                    await _userManager.AddToRoleAsync(user, Helper.RequestInv);
                }
            }
            else
            {
                if (await _userManager.IsInRoleAsync(user, Helper.RequestInv))
                {
                    await _userManager.RemoveFromRoleAsync(user, Helper.RequestInv);
                }
            }

            //4. Handle Request
            if (usr.HandleRequest)
            {
                if (!await _userManager.IsInRoleAsync(user, Helper.HandleRequest))
                {
                    await _userManager.AddToRoleAsync(user, Helper.HandleRequest);
                }
            }
            else
            {
                if (await _userManager.IsInRoleAsync(user, Helper.HandleRequest))
                {
                    await _userManager.RemoveFromRoleAsync(user, Helper.HandleRequest);
                }
            }

            //5. Receive
            if (usr.ReceiveInv)
            {
                if (!await _userManager.IsInRoleAsync(user, Helper.ReceiveInv))
                {
                    await _userManager.AddToRoleAsync(user, Helper.ReceiveInv);
                }
            }
            else
            {
                if (await _userManager.IsInRoleAsync(user, Helper.ReceiveInv))
                {
                    await _userManager.RemoveFromRoleAsync(user, Helper.ReceiveInv);
                }
            }

            //6. QC
            if (usr.QC)
            {
                if (!await _userManager.IsInRoleAsync(user, Helper.QC))
                {
                    await _userManager.AddToRoleAsync(user, Helper.QC);
                }
            }
            else
            {
                if (await _userManager.IsInRoleAsync(user, Helper.QC))
                {
                    await _userManager.RemoveFromRoleAsync(user, Helper.QC);
                }
            }

            //7. Reg
            if (usr.RegVehicle)
            {
                if (!await _userManager.IsInRoleAsync(user, Helper.RegVehicle))
                {
                    await _userManager.AddToRoleAsync(user, Helper.RegVehicle);
                }
            }
            else
            {
                if (await _userManager.IsInRoleAsync(user, Helper.RegVehicle))
                {
                    await _userManager.RemoveFromRoleAsync(user, Helper.RegVehicle);
                }
            }

            //8. Create SO
            if (usr.CreateSO)
            {
                if (!await _userManager.IsInRoleAsync(user, Helper.CreateSO))
                {
                    await _userManager.AddToRoleAsync(user, Helper.CreateSO);
                }
            }
            else
            {
                if (await _userManager.IsInRoleAsync(user, Helper.CreateSO))
                {
                    await _userManager.RemoveFromRoleAsync(user, Helper.CreateSO);
                }
            }

            //9. Approve SO
            if (usr.AppSO)
            {
                if (!await _userManager.IsInRoleAsync(user, Helper.AppSO))
                {
                    await _userManager.AddToRoleAsync(user, Helper.AppSO);
                }
            }
            else
            {
                if (await _userManager.IsInRoleAsync(user, Helper.AppSO))
                {
                    await _userManager.RemoveFromRoleAsync(user, Helper.AppSO);
                }
            }

            //10. Arrange inventory
            if (usr.ArrangeInventory)
            {
                if (!await _userManager.IsInRoleAsync(user, Helper.ArrangeInventory))
                {
                    await _userManager.AddToRoleAsync(user, Helper.ArrangeInventory);
                }
            }
            else
            {
                if (await _userManager.IsInRoleAsync(user, Helper.ArrangeInventory))
                {
                    await _userManager.RemoveFromRoleAsync(user, Helper.ArrangeInventory);
                }
            }

            //11. Arrange inventory
            if (usr.MoveInv)
            {
                if (!await _userManager.IsInRoleAsync(user, Helper.MoveInventory))
                {
                    await _userManager.AddToRoleAsync(user, Helper.MoveInventory);
                }
            }
            else
            {
                if (await _userManager.IsInRoleAsync(user, Helper.MoveInventory))
                {
                    await _userManager.RemoveFromRoleAsync(user, Helper.MoveInventory);
                }
            }

            //12. Create workorder
            if (usr.WoCreation)
            {
                if (!await _userManager.IsInRoleAsync(user, Helper.WoCreation))
                {
                    await _userManager.AddToRoleAsync(user, Helper.WoCreation);
                }
            }
            else
            {
                if (await _userManager.IsInRoleAsync(user, Helper.WoCreation))
                {
                    await _userManager.RemoveFromRoleAsync(user, Helper.WoCreation);
                }
            }

            //13. report workorder
            if (usr.WoReporter)
            {
                if (!await _userManager.IsInRoleAsync(user, Helper.WoReporter))
                {
                    await _userManager.AddToRoleAsync(user, Helper.WoReporter);
                }
            }
            else
            {
                if (await _userManager.IsInRoleAsync(user, Helper.WoReporter))
                {
                    await _userManager.RemoveFromRoleAsync(user, Helper.WoReporter);
                }
            }

            //10. Active
            if (usr.Active)
            {
                var dbUser = _db.Users.FirstOrDefault(x => x.UserName == user.UserName);
                dbUser.Active = true;

            }
            else
            {
                var dbUser = _db.Users.FirstOrDefault(x => x.UserName == user.UserName);
                dbUser.Active = false;

            }


            //admin
            ApplicationUser admin = await _userManager.FindByNameAsync("nmvadmin");

            if (!await _userManager.IsInRoleAsync(admin, "SuperUser"))
            {
                await _userManager.AddToRoleAsync(admin, "SuperUser");
            }

            await _db.SaveChangesAsync();
        }

        public async Task<CommonResponse<string>> ResetPassword(string usrName)
        {
            CommonResponse<string> common = new();
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789abcdefghijklmnopqrstuvwxyz!@#$*";
            var stringChars = new char[12];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }
            var newPassword = new String(stringChars);
            var currentUser = await _db.Users.FirstOrDefaultAsync(x=>x.UserName == usrName);
            if (currentUser != null)
            {
                string hashedNewPassword = _userManager.PasswordHasher.HashPassword(currentUser, newPassword);
                currentUser.PasswordHash = hashedNewPassword;
                _db.Users.Update(currentUser);
                await _db.SaveChangesAsync();

                common.dataenum = newPassword;
                common.status = 1;
            }
            else
            {
                common.message = "User not found";
                common.status = -1;
            }


            return common;
        }
    }
}
