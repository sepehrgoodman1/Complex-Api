namespace Taav.Contracts.Rest;

public interface IRestRequestConfig
{
    RestRequestMethod Method { get; }
    string? BaseUrl { get; }
    string? Resource { get; }
    Dictionary<string, string>? Headers { get; }
    Dictionary<string, string>? Parameters { get; }
}