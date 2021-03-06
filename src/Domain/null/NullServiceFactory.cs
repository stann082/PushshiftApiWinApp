﻿namespace Domain
{
    public class NullServiceFactory : IServiceFactory
    {

        #region Public Methods

        public virtual IRedditApiService CreateRedditService()
        {
            return NullRedditApiService.Singleton;
        }

        #endregion

    }
}
