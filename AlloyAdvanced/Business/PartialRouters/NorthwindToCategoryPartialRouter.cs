﻿using AlloyAdvanced.Models.NorthwindEntities;
using AlloyAdvanced.Models.Pages;
using EPiServer;
using EPiServer.Core;
using EPiServer.ServiceLocation;
using EPiServer.Web.Routing;
using EPiServer.Web.Routing.Segments;
using System.Linq;
using System.Web.Routing;
using System.Data.Entity;

namespace AlloyAdvanced.Business.PartialRouters
{
    public class NorthwindToCategoryPartialRouter : 
        IPartialRouter<NorthwindPage, Category>
    {
        // this method must convert a Category entity into a partial URL path
        // e.g. the category named "Meat/Poultry" 
        // into the URL "/Northwind/Meat_Poultry"
        public PartialRouteData GetPartialVirtualPath(Category content, 
            string language, RouteValueDictionary routeValues, 
            RequestContext requestContext)
        {
            var northwindPages = ServiceLocator.Current.GetInstance<IContentLoader>()
                .GetChildren<NorthwindPage>(ContentReference.StartPage);

            // the base of the URL will be the URL for the NorthwindPage instance
            var basePath = ContentReference.EmptyReference;
            if (northwindPages.Count() > 0)
            {
                basePath = northwindPages.First().ContentLink;
            }
            
            var partialRouteData = new PartialRouteData
            {
                BasePathRoot = basePath,
                PartialVirtualPath = content.CategoryName.Replace('/','_') + "/"
            };

            return partialRouteData;
        }

        // this method must convert a partial URL path into a Category entity
        // e.g. the URL "/Northwind/Meat_Poultry" 
        // into the category named "Meat/Poultry"
        public object RoutePartial(NorthwindPage content, 
            SegmentContext segmentContext)
        {
            // get the next part from the URL, i.e. what comes after "/Northwind/"
            var categorySegment = segmentContext.GetNextValue(
                segmentContext.RemainingPath);
            var categoryName = categorySegment.Next;

            // find the matching Category entity
            var db = new Northwind();
            db.Configuration.ProxyCreationEnabled = false;
            db.Configuration.LazyLoadingEnabled = false;

            var alternativeCategory = categoryName.Replace('_', '/');
            var category = db.Categories
                .Include(c => c.Products)
                .SingleOrDefault(c => 
                    (c.CategoryName == categoryName) || 
                    (c.CategoryName == alternativeCategory));

            if (category != null)
            {
                // store the found Category in the route data
                // so it can be passed into a view
                segmentContext.SetCustomRouteData("category", category);

                // store the remaining path so it could be processed 
                // by another partial router, perhaps for Products
                segmentContext.RemainingPath = categorySegment.Remaining;
            }

            return category;
        }
    }
}