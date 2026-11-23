using Contracts.DTOs.review;

namespace BLL.Interfaces
{
    public interface IReviewService
    {
        Task CreateReviewAsync(int tenantId, CreateReviewRequest request);
    }
}
