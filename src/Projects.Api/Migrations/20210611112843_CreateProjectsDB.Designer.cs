// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Projects.Api.Persistence;

namespace Projects.Api.Migrations
{
    [DbContext(typeof(ProjectsDbContext))]
    [Migration("20210611112843_CreateProjectsDB")]
    partial class CreateProjectsDB
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.7")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Projects.Api.Persistence.Models.Contributor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasComment("Internal id (only used for joins, etc.).")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ProjectId")
                        .HasColumnType("int")
                        .HasComment("The id of the linked project.");

                    b.Property<int>("UserId")
                        .HasColumnType("int")
                        .HasComment("User id of the contributor.");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.ToTable("Contributors");
                });

            modelBuilder.Entity("Projects.Api.Persistence.Models.Project", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasComment("Unique id of the project.")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasComment("Projectname.");

                    b.HasKey("Id");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("Projects.Api.Persistence.Models.Contributor", b =>
                {
                    b.HasOne("Projects.Api.Persistence.Models.Project", "Project")
                        .WithMany("Contributors")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Project");
                });

            modelBuilder.Entity("Projects.Api.Persistence.Models.Project", b =>
                {
                    b.Navigation("Contributors");
                });
#pragma warning restore 612, 618
        }
    }
}
