﻿using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(StravaApiTest.Startup))]
namespace StravaApiTest
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            
        }
    }
}
