﻿using System.Threading.Tasks;

namespace Domain
{
    public class NullRedditApiService : IRedditApiService
    {

        public static IRedditApiService Singleton = new NullRedditApiService();

        #region Public Methods

        public virtual Task<IRedditData> GetCommentData(string requestUri)
        {
            return (Task<IRedditData>)Task.CompletedTask;
        }

        public virtual Task<IRedditData> GetSubmissionData(string requestUri)
        {
            return (Task<IRedditData>)Task.CompletedTask;
        }

        #endregion

    }
}
