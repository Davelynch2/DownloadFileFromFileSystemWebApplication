using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DownloadFileFromFileSystemWebApplication.Models
{
    public class DocumentContext : DbContext
    {
        private const string _documentCurrentView = @"
            CREATE VIEW Documents_Current AS
            WITH s AS
            (
	            SELECT Id, Revision, Name, Location, FileType, Size, Status, LastTimeModified, ROW_NUMBER() OVER (PARTITION BY Id ORDER BY Revision DESC) as rn
	            FROM Documents
            )
            SELECT s.Id, s.Revision, s.Name, s.Location, s.FileType, s.Size, s.Status, s.LastTimeModified,	
            FROM  s
            WHERE rn = 1 AND Status != 2";
        public DocumentContext(DbContextOptions<DocumentContext> options)
            : base(options)
        {
        }
        public DbSet<Document> Documents { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Document>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.Revision })
                      .IsClustered(false);

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Revision)
                      .IsRequired();

                entity.Property(e => e.Location)
                 .HasColumnType("NVarchar(250)")
                 .HasMaxLength(250);

                entity.Property(e => e.Name)
                  .HasColumnType("NVarchar(100)")
                  .IsRequired()
                  .HasMaxLength(100);

                entity.Property(e => e.FileType)
                .IsRequired()
                .HasColumnType("NVarchar(50)")
                .HasMaxLength(50);

                entity.ToTable("Documents");
            });
        }

        public void EnsureDatabaseCreated()
        {
            try
            {
                Database.EnsureCreated();
                //Database.ExecuteSqlRaw(_documentCurrentView);
            }
            catch
            {
                throw;
            }
        }
    }
}
