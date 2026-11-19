using DAL.Models;


namespace DAL.Interfaces
{
    public interface IRefreshTokenRepo
    {
        public Task AddRefreshToken(RefreshToken token);
    }
}
