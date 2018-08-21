using AlloyAdvanced.Models.DDS;
using AlloyAdvanced.Models.Pages;
using AlloyAdvanced.Models.ViewModels;
using EPiServer.Core;
using EPiServer.Security;
using EPiServer.Web.Routing;
using System.Web.Mvc;

namespace AlloyAdvanced.Controllers
{
    public class FavoritesController : Controller
    {
        private readonly UrlResolver urlResolver;

        public FavoritesController(UrlResolver urlResolver)
        {
            this.urlResolver = urlResolver;
        }

        public ActionResult Manager(SitePageData currentPage)
        {
            var model = new FavoriteViewModel();

            model.Favorites = FavoriteRepository
                .GetFavorites(PrincipalInfo.Current.Name);

            model.CurrentPageContentReference = currentPage.ContentLink;

            return PartialView("FavoritesManager", model);
        }

        public void Add(ContentReference page)
        {
            var favorite = FavoriteRepository.GetFavorite(
                page, PrincipalInfo.Current.Name);

            if (favorite == null)
            {
                var newFavorite = new Favorite(page, PrincipalInfo.Current.Name);
                FavoriteRepository.Save(newFavorite);
            }

            Response.Redirect(urlResolver.GetUrl(page));
        }

        public void Delete(ContentReference page, ContentReference fav)
        {
            var favorite = FavoriteRepository.GetFavorite(
                fav, PrincipalInfo.Current.Name);

            if (favorite != null)
            {
                FavoriteRepository.Delete(favorite);
            }

            Response.Redirect(urlResolver.GetUrl(page));
        }
    }
}