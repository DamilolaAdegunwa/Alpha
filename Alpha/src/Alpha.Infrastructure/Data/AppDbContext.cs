using Alpha.Core.CommentAggregate;
using Alpha.Core.ConnectorAggregate;
using Alpha.Core.NotificationAggregate;
using Alpha.Core.ProjectAggregate;
using Alpha.Core.SessionAggregate;
using Alpha.Infrastructure.UserAggregate;
using Alpha.SharedKernel;
using Ardalis.EFCore.Extensions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Alpha.Infrastructure.Data
{
    public class AppDbContext : IdentityDbContext//Identitydbcontext//DbContext
  {
        private readonly IMediator? _mediator;

        //public AppDbContext(DbContextOptions options) : base(options)
        //{
        //}

        public AppDbContext(DbContextOptions<AppDbContext> options, IMediator? mediator)
            : base(options)
        {
            _mediator = mediator;
        }

        public DbSet<ToDoItem> ToDoItems => Set<ToDoItem>();
        public DbSet<Project> Projects => Set<Project>();

      protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyAllConfigurationsFromCurrentAssembly();

            // alternately this is built-in to EF Core 2.2
            //modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            int result = await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            // ignore events if no dispatcher provided
            if (_mediator == null) return result;

            // dispatch events only if save was successful
            var entitiesWithEvents = ChangeTracker.Entries<BaseEntity>()
                .Select(e => e.Entity)
                .Where(e => e.Events.Any())
                .ToArray();

            foreach (var entity in entitiesWithEvents)
            {
                var events = entity.Events.ToArray();
                entity.Events.Clear();
                foreach (var domainEvent in events)
                {
                    await _mediator.Publish(domainEvent).ConfigureAwait(false);
                }
            }

            return result;
        }

        public override int SaveChanges()
        {
            return SaveChangesAsync().GetAwaiter().GetResult();
        }

        public DbSet<ApplicationUser> ApplicationUsers => Set<ApplicationUser>();
      public DbSet<Session> Sessions=> Set<Session>();
      public DbSet<Comment> Comments => Set<Comment>();
      public DbSet<Connector> Connectors => Set<Connector>();
      public DbSet<Notification> Notifications => Set<Notification>();
   }
}
