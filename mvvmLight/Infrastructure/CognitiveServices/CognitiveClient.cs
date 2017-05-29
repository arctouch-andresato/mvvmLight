using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.ProjectOxford.Vision;
using Microsoft.ProjectOxford.Vision.Contract;

namespace mvvmLight {
    public class CognitiveClient : ICognitiveClient
    {
        public async Task<AnalysisResult> GetImageDescription(Stream stream)
        {
            VisionServiceClient visionClient = new VisionServiceClient(Config.CognitiveClientApiKey);
            VisualFeature[] features = { VisualFeature.Tags, VisualFeature.Categories, VisualFeature.Description };
            return await visionClient.AnalyzeImageAsync(stream, features.ToList(), null);
        }
    }
}
