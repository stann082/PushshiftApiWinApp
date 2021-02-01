using Domain;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation
{
    public class PushshiftApiPresenter
    {

        #region Constants

        private static readonly DateTime START_DATE = new(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

        #endregion

        #region Constructors

        public PushshiftApiPresenter()
        {
            Service = ServiceFactoryProxy.Singleton.CreateRedditService();
        }

        #endregion

        #region Properties

        public string Counter { get; private set; }
        public string Response { get; private set; }

        private IRedisConnector Connector => RedisConnector.Singleton;
        private IRedditApiService Service { get; set; }

        #endregion

        #region Public Methods

        public async Task BuildResponseContent(ISearchOptions options)
        {
            UrlParameterBuilder builder = new(options);
            string requestUri = builder.Build(options.QueryType.ToString().ToLower());

            IRedditData data = await GetRedditData(requestUri, options.QueryType);
            if (data == null)
            {
                Response = "Something went wrong...";
                return;
            }

            Connector.CacheResults(data, 1);

            IContent[] contents = data.Contents;
            if (options.ShowExactMatches)
            {
                contents = data.Contents.Where(c => c.Text.Contains(options.Query)).ToArray();
            }

            if (contents.Length == 0)
            {
                Response = "Nothing found...";
                return;
            }

            Counter = $"Showing {contents.Length} items";

            StringBuilder sb = new();

            sb.AppendLine($"API call: {Constants.BASE_URL}{requestUri}");
            sb.AppendLine();

            foreach (IContent content in contents)
            {
                sb.AppendLine($"Subreddit: {content.Subreddit}");
                sb.AppendLine($"    Score: {content.Score}");
                sb.AppendLine($"  Created: {FromUnixTime(content.CreatedUtc)}");

                DateTime updatedTime = FromUnixTime(content.UpdatedUtc);
                if (updatedTime > START_DATE)
                {
                    sb.AppendLine($"  Updated: {updatedTime}");
                }

                if (string.IsNullOrEmpty(options.UserName))
                {
                    sb.AppendLine($"   Author: https://www.reddit.com/u/{content.Author}");
                }

                sb.AppendLine();

                string text = content.Text.Replace("&gt;", "*****");
                sb.AppendLine(text);
                sb.AppendLine($"Link: https://www.reddit.com{content.Permalink}");

                sb.AppendLine();
                sb.AppendLine();
            }

            Response = sb.ToString();
        }

        private async Task<IRedditData> GetRedditData(string requestUri, QueryType queryType)
        {
            switch (queryType)
            {
                case QueryType.Comment:
                    return await Service.GetCommentData(requestUri);
                case QueryType.Submission:
                    return await Service.GetSubmissionData(requestUri);
                default:
                    return null;
            }
        }

        #endregion

        #region Helper Methods

        private DateTime FromUnixTime(double? unixTimeStamp)
        {
            if (!unixTimeStamp.HasValue)
            {
                return START_DATE;
            }

            DateTime dtDateTime = START_DATE;
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp.Value).ToLocalTime();
            return dtDateTime;
        }

        #endregion

    }
}
