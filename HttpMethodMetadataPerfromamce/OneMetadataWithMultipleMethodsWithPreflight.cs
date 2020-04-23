using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace HttpMethodMetadataPerfromamce
{
    public class OneMetadataWithMultipleMethodsWithPreflight : HttpMethodBenchmarkBase
    {
        [Benchmark]
        public async Task After()
        {
            var metadatas = new[]
            {
                new HttpMethodMetadata(new [] {HttpMethods.Put,HttpMethods.Delete,HttpMethods.Post,HttpMethods.Get}, true)
            };

            await Benchmark(afterMatcher, metadatas);
        }

        [Benchmark]
        public async Task Before()
        {
            var metadatas = new[]
            {
                new HttpMethodMetadata(new [] {"PUT","DELETE","POST","GET"}, true)
            };

            await Benchmark(beforeMatcher, metadatas);
        }
    }
}
