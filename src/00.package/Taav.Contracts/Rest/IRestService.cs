namespace Taav.Contracts.Rest;

public interface IRestService
{
    Task<TResponse> Send<TResponse, TRequest>(
        TRequest request, IRestRequestConfig config)
        where TRequest : class;
    Task Send<TRequest>(
        TRequest request, IRestRequestConfig config)
        where TRequest : class;
    Task<TResponse> Send<TResponse>(
        IRestRequestConfig config);
    Task Send(
        IRestRequestConfig config);
}
