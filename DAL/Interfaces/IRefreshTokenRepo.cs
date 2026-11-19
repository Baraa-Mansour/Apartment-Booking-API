using DAL.Models;


namespace DAL.Interfaces
{
    public interface IRefreshTokenRepo
    {
        public Task AddRefreshTokenAsync(RefreshToken token);
    }
}
