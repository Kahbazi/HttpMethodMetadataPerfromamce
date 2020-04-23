using System.Threading.Tasks;
using BenchmarkDotNet.Running;

namespace HttpMethodMetadataPerfromamce
{
    public partial class Program
    {
        static async Task Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<MultipleMetadataWithPreflight>();
            //var summary = BenchmarkRunner.Run<OneMetadataWithMultipleMethodsWithPreflight>();
        }
    }
}
