using System.Collections.Concurrent;
using MessageBroker.Microservices.MessageQueue_B.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace MessageBroker.Microservices.MessageQueue_B.DataAccess;

public class MessageQueueDbContext : DbContext
{
    public MessageQueueDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
    {
    }
    public DbSet<MessageNode> MessageNodes { get; set; }

    /// <summary>
    ///     Override this method to set defaults and configure conventions before they run. This method is invoked before
    ///     <see cref="M:Microsoft.EntityFrameworkCore.DbContext.OnModelCreating(Microsoft.EntityFrameworkCore.ModelBuilder)" />.
    /// </summary>
    /// <remarks>
    ///     If a model is explicitly set on the options for this context (via <see cref="M:Microsoft.EntityFrameworkCore.DbContextOptionsBuilder.UseModel(Microsoft.EntityFrameworkCore.Metadata.IModel)" />)
    ///     then this method will not be run.
    /// </remarks>
    /// <remarks>
    ///     See <see href="https://aka.ms/efcore-docs-pre-convention">Pre-convention model building in EF Core</see> for more information.
    /// </remarks>
    /// <param name="modelBuilder">
    ///     The builder being used to set defaults and configure conventions that will be used to build the model for this context.
    /// </param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Add the Postgres Extension for UUID generation
        modelBuilder.HasPostgresExtension("uuid-ossp");
        
        modelBuilder.HasDefaultSchema("message_queue");
        
        modelBuilder.Entity<MessageNode>()
            .ToTable("message_node")
            .HasKey(p => p.Id)
            .HasName("message_node__id__pkey");
        
        modelBuilder.Entity<MessageNode>()
            .Property(p => p.Id)
            .HasColumnName("id")
            .HasColumnType("uuid")
            .HasDefaultValueSql("uuid_generate_v4()")
            .IsRequired();
        
        modelBuilder.Entity<MessageNode>()
            .Property(p => p.Content)
            .HasColumnName("content")
            .HasMaxLength(500)
            .IsRequired();
        
        modelBuilder.Entity<MessageNode>()
            .Property(p => p.Priority)
            .HasColumnName("priority")
            .IsRequired();
        
        modelBuilder.Entity<MessageNode>()
            .Property(p => p.DateUpdate)
            .HasColumnName("date_update")
            .IsRequired();
    }
}