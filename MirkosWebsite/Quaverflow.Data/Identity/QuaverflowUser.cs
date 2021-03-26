using Microsoft.AspNetCore.Identity;
using Quaverflow.Data.MusicModels;
using System;
using System.Collections.Generic;


namespace MusicTechnologies.Data
{
    public class QuaverflowUser : IdentityUser
    {
        [PersonalData]
        public DateTime DOB { get; set; }
        [PersonalData]
        public string FullName { get; set; }    
        public List<Scale> Scales { get; set; }
    }
}
