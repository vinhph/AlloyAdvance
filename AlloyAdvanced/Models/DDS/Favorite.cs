using EPiServer.Data.Dynamic;
using System;
using EPiServer.Data;
using EPiServer.Core;

namespace AlloyAdvanced.Models.DDS
{
    public class Favorite : IDynamicData
    {
        public Identity Id { get; set; }
        public string UserName { get; set; }
        public ContentReference FavoriteContentReference { get; set; }

        public Favorite()
        {
            Id = Identity.NewIdentity(Guid.NewGuid());
            UserName = string.Empty;
            FavoriteContentReference = ContentReference.EmptyReference;
        }

        public Favorite(ContentReference contentReferenceToAdd,
            string userName) : this() // calls the default constructor first
        {
            UserName = userName;
            FavoriteContentReference = contentReferenceToAdd;
        }
    }
}