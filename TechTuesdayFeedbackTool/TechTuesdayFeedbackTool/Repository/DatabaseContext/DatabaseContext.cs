using System.Data.Entity;
using TechTuesdayFeedbackTool.Domain;

namespace TechTuesdayFeedbackTool.Repository
{
    public class DatabaseContext : DbContext
    {
        public DbSet<User> UsersContext { get; set; }
        public DbSet<UserRolesMaster> UserRolesMasterContext { get; set; }
        public DbSet<PresentationDetails> PresentationDetailsContext { get; set; }
        public DbSet<UserAssociationWithPresentation> UserAssociationWithPresentationContext { get; set; }
        public DbSet<Ratings> RatingsContext { get; set; }
        public DbSet<ScoresMaster> ScoreMasterContext { get; set; }
        public DbSet<PresentationsRepository> PresentationsRepositoryContext { get; set; }
        public DbSet<Comments> CommentsContext { get; set; }
    }
}