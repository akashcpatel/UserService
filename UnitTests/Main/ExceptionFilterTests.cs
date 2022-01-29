using Microsoft.AspNetCore.Mvc.Filters;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Main.Tests
{
    public class ExceptionFilterTests
    {
        private ExceptionFilter _exceptionFilter;

        [SetUp]
        public void SetUp()
        { 
            _exceptionFilter = new ExceptionFilter();
        }

        [Test]
        [Ignore("Need to create instance of HttpContext")]
        public void Test_OnException()
        {
            var context = new ExceptionContext(new Microsoft.AspNetCore.Mvc.ActionContext(), new List<IFilterMetadata>())
            {
                Exception = new ArgumentException("Some Message")
            };

            _exceptionFilter.OnException(context);

            Assert.That(context.Result, Is.EqualTo(null));
        }
    }
}
