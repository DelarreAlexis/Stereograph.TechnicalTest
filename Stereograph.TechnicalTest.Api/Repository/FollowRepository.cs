using Stereograph.TechnicalTest.Api.Models;
using System;

namespace Stereograph.TechnicalTest.Api.Repository
{
    public interface IFollowRepository
    {
        void AddFollow();
    }
    public class FollowRepository : IFollowRepository
    {
        private readonly ApplicationDbContext _context;

        public FollowRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void AddFollow()
        {

        }
    }
}
