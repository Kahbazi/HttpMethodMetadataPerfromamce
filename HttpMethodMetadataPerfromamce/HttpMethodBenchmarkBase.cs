using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Routing.Matching;

namespace HttpMethodMetadataPerfromamce
{
    public abstract class HttpMethodBenchmarkBase
    {
        protected readonly AfterHttpMethodMatcherPolicy afterMatcher = new AfterHttpMethodMatcherPolicy();
        protected readonly BeforeHttpMethodMatcherPolicy beforeMatcher = new BeforeHttpMethodMatcherPolicy();

        public async Task Benchmark(IEndpointSelectorPolicy selector, params HttpMethodMetadata[] metadatas)
        {
            var httpContext = new DefaultHttpContext();
            httpContext.Request.Method = HttpMethods.Options;

            var candidateSet = CreateCandidateSet(metadatas);

            await selector.ApplyAsync(httpContext, candidateSet);
        }

        private static CandidateSet CreateCandidateSet(HttpMethodMetadata[] httpMethodMetadata)
        {
            var candidateSet = new CandidateSet(
                httpMethodMetadata.Select(metadata => CreateEndpoint(metadata)).ToArray(),
                httpMethodMetadata.Select(metadata => new RouteValueDictionary()).ToArray(),
                new int[httpMethodMetadata.Length]);
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
