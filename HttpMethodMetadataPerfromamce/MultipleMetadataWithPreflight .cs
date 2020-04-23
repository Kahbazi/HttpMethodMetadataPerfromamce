using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace HttpMethodMetadataPerfromamce
{
    public class MultipleMetadataWithPreflight : HttpMethodBenchmarkBase
    {
        [Benchmark]
        public async Task After()
        {
            var metadatas = new[]
            {
                new HttpMethodMetadata(new [] {HttpMethods.Put}, true),
                new HttpMethodMetadata(new [] {HttpMethods.Delete}, true),
                new HttpMethodMetadata(new [] {HttpMethods.Post}, true),
                new HttpMethodMetadata(new [] {HttpMethods.Get}, true),
            };

            await Benchmark(afterMatcher, metadatas);
        }

        [Benchmark]
        public async Task Before()
        {
            var metadatas = new[]
            {
                new HttpMethodMetadata(new [] {"PUT"}, true),
                new HttpMethodMetadata(new [] {"DELETE"}, true),
                new HttpMethodMetadata(new [] {"POST"}, true),
                new HttpMethodMetadata(new [] {"GET"}, true),
            };

            await Benchmark(beforeMatcher, metadatas);
        }
    }
}
