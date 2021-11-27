﻿using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComeX.Lib.Auth
{
    public static class ServicesSetupExtention
    {
        public static IServiceCollection AddComeXAuth(this IServiceCollection services, Uri userWebApiUri)
        {
            var connectionCache = new ConnectionCache();
            services.AddSingleton<IUserApiRequestFactory, UserApiRequestFactory>();
            services.AddSingleton<IUserApiManager>(x => new UserApiManager(x.GetRequiredService<IUserApiRequestFactory>(), userWebApiUri));
            services.AddSingleton<IConnectionCache>(connectionCache);
            services.AddSingleton<ILoginManager>(x => new LoginManager(x.GetRequiredService<IUserApiManager>(), connectionCache));
            return services;
        }
    }
}