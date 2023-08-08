namespace Taav.Contracts.Rest;

public interface IRestRequestConfigBuilder
{
    IRestRequestConfig Build();
    IRestRequestConfigBuilder WithResource(string resource);
    IRestRequestConfigBuilder WithParameter(string name, string value);
    IRestRequestConfigBuilder WithHeader(string name, string value);
}
