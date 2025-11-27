using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace MVC_ArtFlowAWS.Areas.Identity.Data;

// Add profile data for application users by adding properties to the MVC_ArtFlowAWSUser class
public class MVC_ArtFlowAWSUser : IdentityUser
{
    [PersonalData]
    public string? CustomerFullName { get; set; }
    [PersonalData]
    public int CustomerAge { get; set; }
    [PersonalData]
    public string? CustomerAddress { get; set; }
    [PersonalData]
    public DateTime CustomerDOB { get; set; }   
}

