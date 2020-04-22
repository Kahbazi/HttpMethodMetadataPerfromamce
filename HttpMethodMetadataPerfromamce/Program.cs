using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Routing.Matching;

namespace HttpMethodMetadataPerfromamce
{
    public partial class Program
    {
        static async Task Main(string[] args)
        {
            //await new HttpMethodPerdormance().After();
            //await new HttpMethodPerdormance().Before();
            var summary = BenchmarkRunner.Run<HttpMethodPerdormance>();
        }

        public class HttpMethodPerdormance
        {
            private readonly AfterHttpMethodMatcherPolicy afterMatcher = new AfterHttpMethodMatcherPolicy();
            private readonly BeforeHttpMethodMatcherPolicy beforeMatcher = new BeforeHttpMethodMatcherPolicy();


            [Benchmark]
            public async Task After()
            {
                await Match_HttpMethod(HttpMethods.Get, afterMatcher);
            }

            [Benchmark]
            public async Task Before()
            {
                await Match_HttpMethod("GET", beforeMatcher);
            }

            public async Task Match_HttpMethod(string httpMethod, IEndpointSelectorPolicy selector)
            {
                var httpContext = new DefaultHttpContext();
                httpContext.Request.Method = HttpMethods.Get;

                var metadata = new HttpMethodMetadata(new[] { httpMethod });

                var candidateSet = CreateCandidateSet(metadata);

                await selector.ApplyAsync(httpContext, candidateSet);
            }

            private static CandidateSet CreateCandidateSet(HttpMethodMetadata httpMethodMetadata)
            {
                var routevalues = new RouteValueDictionary();

                var candidateSet = new CandidateSet(
                    new[] { CreateEndpoint(httpMethodMetadata) },
                    new[] { routevalues },
                    new int[1]);
                return candidateSet;
            }

            private static Endpoint CreateEndpoint(HttpMethodMetadata httpMethodMetadata)
            {
                var metadata = new List<object>() { httpMethodMetadata };
                return new Endpoint(
                    (context) => Task.CompletedTask,
                    new EndpointMetadataCollection(metadata),
                    $"test: {httpMethodMetadata?.HttpMethods[0]}");
            }

        }
    }
}
