using AlloyDemo.Models.Pages;
using EPiServer;
using EPiServer.Core;
using EPiServer.PlugIn;
using EPiServer.Scheduler;
using EPiServer.ServiceLocation;

namespace AlloyDemo.Business.ScheduledJobs
{
    [ScheduledPlugIn(DisplayName = "Bad Word Scanner", 
        Description = "Scan for bad words in pages and censor them.")]
    public class BadWordScannerScheduledJob : ScheduledJobBase
    {
        private bool _stopSignaled;

        // you could load this from a file or service
        private string[] badWords = new[] { "frak" };

        public BadWordScannerScheduledJob()
        {
            IsStoppable = true;
        }

        /// <summary>
        /// Called when a user clicks on Stop for a manually started job, or when ASP.NET shuts down.
        /// </summary>
        public override void Stop()
        {
            _stopSignaled = true;
        }
        
        /// <summary>
        /// Called when a scheduled job executes
        /// </summary>
        /// <returns>A status message to be stored in the database log and visible from admin mode</returns>
        public override string Execute()
        {
            OnStatusChanged(string.Format("Starting execution of {0}", this.GetType()));

            var repo = ServiceLocator.Current.GetInstance<IContentRepository>();

            var finder = ServiceLocator.Current.GetInstance<IPageCriteriaQueryService>();

            int pageCount = 0;

            foreach (string word in badWords)
            {
                var criteria = new PropertyCriteriaCollection();

                criteria.Add(new PropertyCriteria
                {
                    Type = PropertyDataType.LongString,
                    Name = "PageName",
                    Condition = EPiServer.Filters.CompareCondition.Contained,
                    Value = word
                });

                criteria.Add(new PropertyCriteria
                {
                    Type = PropertyDataType.LongString,
                    Name = "MetaTitle",
                    Condition = EPiServer.Filters.CompareCondition.Contained,
                    Value = word
                });

                criteria.Add(new PropertyCriteria
                {
                    Type = PropertyDataType.LongString,
                    Name = "MetaDescription",
                    Condition = EPiServer.Filters.CompareCondition.Contained,
                    Value = word
                });

                PageDataCollection results = finder.FindPagesWithCriteria(
                    ContentReference.RootPage as PageReference, criteria);

                foreach (SitePageData page in results)
                {
                    var clone = page.CreateWritableClone() as SitePageData;

                    clone.Name = page.Name.Replace(word, "[censored]");
                    clone.MetaTitle = page.MetaTitle.Replace(word, "[censored]");
                    clone.MetaDescription = page.MetaDescription.Replace(word, "[censored]");

                    repo.Save(clone, 
                        EPiServer.DataAccess.SaveAction.CheckIn, 
                        EPiServer.Security.AccessLevel.NoAccess);

                    pageCount++;
                }

                //For long running jobs periodically check if stop is signaled and if so stop execution
                if (_stopSignaled)
                {
                    return "Stop of job was called";
                }
            }

            return $"{pageCount} pages containing one of the following bad words: '{string.Join("' or '", badWords)}' have been censored.";
        }
    }
}
