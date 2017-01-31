﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using WireMock.Matchers;
using WireMock.Matchers.Request;
using WireMock.Util;

namespace WireMock.RequestBuilders
{
    /// <summary>
    /// The requests.
    /// </summary>
    public class Request : RequestMessageCompositeMatcher, IRequestBuilder
    {
        private readonly IList<IRequestMatcher> _requestMatchers;

        /// <summary>
        /// Creates this instance.
        /// </summary>
        /// <returns>The <see cref="IRequestBuilder"/>.</returns>
        public static IRequestBuilder Create()
        {
            return new Request(new List<IRequestMatcher>());
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Request"/> class.
        /// </summary>
        /// <param name="requestMatchers">The request matchers.</param>
        private Request(IList<IRequestMatcher> requestMatchers) : base(requestMatchers)
        {
            _requestMatchers = requestMatchers;
        }

        /// <summary>
        /// Gets the request message matchers.
        /// </summary>
        /// <typeparam name="T">Type of IRequestMatcher</typeparam>
        /// <returns>A List{T}</returns>
        public IList<T> GetRequestMessageMatchers<T>() where T : IRequestMatcher
        {
            return new ReadOnlyCollection<T>(_requestMatchers.Where(rm => rm is T).Cast<T>().ToList());
        }

        /// <summary>
        /// Gets the request message matcher.
        /// </summary>
        /// <typeparam name="T">Type of IRequestMatcher</typeparam>
        /// <returns>A RequestMatcher</returns>
        public T GetRequestMessageMatcher<T>() where T : IRequestMatcher
        {
            return _requestMatchers.Where(rm => rm is T).Cast<T>().FirstOrDefault();
        }

        /// <summary>
        /// The with path.
        /// </summary>
        /// <param name="matchers">The matchers.</param>
        /// <returns>The <see cref="IRequestBuilder"/>.</returns>
        public IRequestBuilder WithPath(params IMatcher[] matchers)
        {
            _requestMatchers.Add(new RequestMessagePathMatcher(matchers));
            return this;
        }

        /// <summary>
        /// The with path.
        /// </summary>
        /// <param name="paths">The paths.</param>
        /// <returns>The <see cref="IRequestBuilder"/>.</returns>
        public IRequestBuilder WithPath(params string[] paths)
        {
            _requestMatchers.Add(new RequestMessagePathMatcher(paths));
            return this;
        }

        /// <summary>
        /// The with path.
        /// </summary>
        /// <param name="funcs">The path func.</param>
        /// <returns>The <see cref="IRequestBuilder"/>.</returns>
        public IRequestBuilder WithPath(params Func<string, bool>[] funcs)
        {
            _requestMatchers.Add(new RequestMessagePathMatcher(funcs));
            return this;
        }

        /// <summary>
        /// The with url.
        /// </summary>
        /// <param name="matcher">The matcher.</param>
        /// <returns>The <see cref="IRequestBuilder"/>.</returns>
        public IRequestBuilder WithUrl(params IMatcher[] matcher)
        {
            _requestMatchers.Add(new RequestMessageUrlMatcher(matcher));
            return this;
        }

        /// <summary>
        /// The with url.
        /// </summary>
        /// <param name="urls">The urls.</param>
        /// <returns>The <see cref="IRequestBuilder"/>.</returns>
        public IRequestBuilder WithUrl(params string[] urls)
        {
            _requestMatchers.Add(new RequestMessageUrlMatcher(urls));
            return this;
        }

        /// <summary>
        /// The with url.
        /// </summary>
        /// <param name="funcs">The url func.</param>
        /// <returns>The <see cref="IRequestBuilder"/>.</returns>
        public IRequestBuilder WithUrl(params Func<string, bool>[] funcs)
        {
            _requestMatchers.Add(new RequestMessageUrlMatcher(funcs));
            return this;
        }

        /// <summary>
        /// The using get.
        /// </summary>
        /// <returns>
        /// The <see cref="IRequestBuilder"/>.
        /// </returns>
        public IRequestBuilder UsingGet()
        {
            _requestMatchers.Add(new RequestMessageMethodMatcher("get"));
            return this;
        }

        /// <summary>
        /// The using post.
        /// </summary>
        /// <returns>
        /// The <see cref="IRequestBuilder"/>.
        /// </returns>
        public IRequestBuilder UsingPost()
        {
            _requestMatchers.Add(new RequestMessageMethodMatcher("post"));
            return this;
        }

        /// <summary>
        /// The using put.
        /// </summary>
        /// <returns>
        /// The <see cref="IRequestBuilder"/>.
        /// </returns>
        public IRequestBuilder UsingPut()
        {
            _requestMatchers.Add(new RequestMessageMethodMatcher("put"));
            return this;
        }

        /// <summary>
        /// The using delete.
        /// </summary>
        /// <returns>
        /// The <see cref="IRequestBuilder"/>.
        /// </returns>
        public IRequestBuilder UsingDelete()
        {
            _requestMatchers.Add(new RequestMessageMethodMatcher("delete"));
            return this;
        }

        /// <summary>
        /// The using head.
        /// </summary>
        /// <returns>The <see cref="IRequestBuilder"/>.</returns>
        public IRequestBuilder UsingHead()
        {
            _requestMatchers.Add(new RequestMessageMethodMatcher("head"));
            return this;
        }

        /// <summary>
        /// The using any verb.
        /// </summary>
        /// <returns>
        /// The <see cref="IRequestBuilder"/>.
        /// </returns>
        public IRequestBuilder UsingAnyVerb()
        {
            var matchers = _requestMatchers.Where(m => m is RequestMessageMethodMatcher).ToList();
            foreach (var matcher in matchers)
            {
                _requestMatchers.Remove(matcher);
            }

            return this;
        }

        /// <summary>
        /// The using verb.
        /// </summary>
        /// <param name="verbs">The verbs.</param>
        /// <returns>The <see cref="IRequestBuilder"/>.</returns>
        public IRequestBuilder UsingVerb(params string[] verbs)
        {
            _requestMatchers.Add(new RequestMessageMethodMatcher(verbs));
            return this;
        }

        /// <summary>
        /// The with body.
        /// </summary>
        /// <param name="body">
        /// The body.
        /// </param>
        /// <returns>The <see cref="IRequestBuilder"/>.</returns>
        public IRequestBuilder WithBody(string body)
        {
            _requestMatchers.Add(new RequestMessageBodyMatcher(body));
            return this;
        }

        /// <summary>
        /// The with body byte[].
        /// </summary>
        /// <param name="body">
        /// The body as byte[].
        /// </param>
        /// <returns>The <see cref="IRequestBuilder"/>.</returns>
        public IRequestBuilder WithBody(byte[] body)
        {
            _requestMatchers.Add(new RequestMessageBodyMatcher(body));
            return this;
        }

        /// <summary>
        /// The with body.
        /// </summary>
        /// <param name="func">
        /// The body function.
        /// </param>
        /// <returns>The <see cref="IRequestBuilder"/>.</returns>
        public IRequestBuilder WithBody(Func<string, bool> func)
        {
            _requestMatchers.Add(new RequestMessageBodyMatcher(func));
            return this;
        }

        /// <summary>
        /// The with body.
        /// </summary>
        /// <param name="func">
        /// The body function.
        /// </param>
        /// <returns>The <see cref="IRequestBuilder"/>.</returns>
        public IRequestBuilder WithBody(Func<byte[], bool> func)
        {
            _requestMatchers.Add(new RequestMessageBodyMatcher(func));
            return this;
        }

        /// <summary>
        /// The with body.
        /// </summary>
        /// <param name="matcher">The matcher.</param>
        /// <returns>The <see cref="IRequestBuilder" />.</returns>
        public IRequestBuilder WithBody(IMatcher matcher)
        {
            _requestMatchers.Add(new RequestMessageBodyMatcher(matcher));
            return this;
        }

        /// <summary>
        /// The with parameters.
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <param name="values">
        /// The values.
        /// </param>
        /// <returns>The <see cref="IRequestBuilder"/>.</returns>
        public IRequestBuilder WithParam(string key, params string[] values)
        {
            _requestMatchers.Add(new RequestMessageParamMatcher(key, values));
            return this;
        }

        /// <summary>
        /// The with parameters.
        /// </summary>
        /// <param name="funcs">The funcs.</param>
        /// <returns>The <see cref="IRequestBuilder"/>.</returns>
        public IRequestBuilder WithParam(params Func<IDictionary<string, WireMockList<string>>, bool>[] funcs)
        {
            _requestMatchers.Add(new RequestMessageParamMatcher(funcs));
            return this;
        }

        /// <summary>
        /// With header.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="pattern">The pattern.</param>
        /// <param name="ignoreCase">if set to <c>true</c> [ignore case].</param>
        /// <returns>The <see cref="IRequestBuilder"/>.</returns>
        public IRequestBuilder WithHeader(string name, string pattern, bool ignoreCase = true)
        {
            _requestMatchers.Add(new RequestMessageHeaderMatcher(name, pattern, ignoreCase));
            return this;
        }

        /// <summary>
        /// With header.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="matchers">The matchers.</param>
        /// <returns>The <see cref="IRequestBuilder"/>.</returns>
        public IRequestBuilder WithHeader(string name, params IMatcher[] matchers)
        {
            _requestMatchers.Add(new RequestMessageHeaderMatcher(name, matchers));
            return this;
        }

        /// <summary>
        /// With header.
        /// </summary>
        /// <param name="funcs">The funcs.</param>
        /// <returns>The <see cref="IRequestBuilder"/>.</returns>
        public IRequestBuilder WithHeader(params Func<IDictionary<string, string>, bool>[] funcs)
        {
            _requestMatchers.Add(new RequestMessageHeaderMatcher(funcs));
            return this;
        }

        /// <summary>
        /// With cookie.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="pattern">The pattern.</param>
        /// <param name="ignoreCase">if set to <c>true</c> [ignore case].</param>
        /// <returns>The <see cref="IRequestBuilder"/>.</returns>
        public IRequestBuilder WithCookie(string name, string pattern, bool ignoreCase = true)
        {
            _requestMatchers.Add(new RequestMessageCookieMatcher(name, pattern, ignoreCase));
            return this;
        }

        /// <summary>
        /// With cookie.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="matchers">The matchers.</param>
        /// <returns>The <see cref="IRequestBuilder"/>.</returns>
        public IRequestBuilder WithCookie(string name, params IMatcher[] matchers)
        {
            _requestMatchers.Add(new RequestMessageCookieMatcher(name, matchers));
            return this;
        }

        /// <summary>
        /// With header.
        /// </summary>
        /// <param name="funcs">The funcs.</param>
        /// <returns>The <see cref="IRequestBuilder"/>.</returns>
        public IRequestBuilder WithCookie(params Func<IDictionary<string, string>, bool>[] funcs)
        {
            _requestMatchers.Add(new RequestMessageCookieMatcher(funcs));
            return this;
        }
    }
}