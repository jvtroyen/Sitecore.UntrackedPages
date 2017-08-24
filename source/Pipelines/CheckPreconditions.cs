using Sitecore;
using Sitecore.Analytics;
using Sitecore.Analytics.Tracking.Diagnostics.PerformanceCounters;
using Sitecore.Diagnostics;
using Sitecore.Layouts;
using Sitecore.Pipelines;
using Sitecore.Sites;
using TheReference.DotNet.Sitecore.UntrackedPages.Configuration;

namespace TheReference.DotNet.Sitecore.UntrackedPages.Pipelines
{
    public class CheckPreconditions
    {
        private static object excludeListSync = new object();
        private static UntrackedPageList excludeList;

        public static UntrackedPageList ExcludeList
        {
            get
            {
                if (excludeList == null)
                {
                    lock (excludeListSync)
                    {
                        if (excludeList == null)
                            excludeList = UntrackedPageList.GetUntrackedPages();
                    }
                }
                return excludeList;
            }
            internal set
            {
                excludeList = value;
            }
        }

        public virtual void Process(PipelineArgs args)
        {
            Assert.ArgumentNotNull((object)args, "args");
            if (!Tracker.Enabled)
            {
                args.AbortPipeline();
            }
            else
            {
                SiteContext site = Context.Site;
                if (site == null)
                {
                    args.AbortPipeline();
                }
                else
                {
                    PageContext page = Context.Page;
                    if (page == null)
                        args.AbortPipeline();
                    else if (!TrackingSiteContextExtension.Tracking(site).EnableTracking)
                        args.AbortPipeline();
                    else if (this.IsUntrackedPage(page.FilePath, Context.RawUrl))
                        args.AbortPipeline();
                    else
                        AnalyticsTrackingCount.CollectionTotalRequests.Increment(1L);
                }
            }
        }

        protected bool IsUntrackedPage(string filePath, string rawUrl)
        {
            Assert.ArgumentNotNull((object)filePath, "filePath");
            Assert.ArgumentNotNull((object)rawUrl, "rawUrl");

            var value = filePath;
            if (string.IsNullOrEmpty(value))
            {
                value = rawUrl;
            }

            if (ExcludeList.ContainsUntrackedPage(value))
            {
                Log.Warn("UntrackedPage - " + value, this);
                return true;
            }

            Log.Info("TrackedPage - " + value, this);
            return false;
        }
    }
}
