using System;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.SpecializedProperties;

namespace AlloyAdvanced.Models.Pages
{
    [ContentType(DisplayName = "Security", GroupName = Global.GroupNames.Specialized, GUID = "e05c8719-ba80-41a7-a9da-0f2d9b93eaf5", Description = "Use this page to see security details of the system and current user.")]
    [AvailableContentTypes(IncludeOn = new[] { typeof(StartPage) })]
    public class SecurityPage : SitePageData
    {
        [Ignore]
        public User SecurityUser { get; set; }
        [Ignore]
        public System SecuritySystem { get; set; }
        public class System
        {
            public string Provider { get; set; }
            public string[] VirtualRoles { get; set; }
            public string[] StoredRoles { get; set; }
        }
        public class User
        {
            public string Name { get; set; }
            public bool IsAnonymous { get; set; }
            public bool IsAdministrator { get; set; }
            public bool IsEditor { get; set; }
            public bool HasAccessToPlugins { get; set; }
            public Claim[] Claims { get; set; }
            public string[] Roles { get; set; }
        }
    }
}