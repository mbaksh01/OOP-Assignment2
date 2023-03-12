using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Movies.Application.DAL;
using Movies.Application.Models;

namespace Movies.Application.Extensions;

/// <summary>
/// Extensiopn methods for <see cref="IHost"/>.
/// </summary>
public static class HostExtensions
{
    /// <summary>
    /// Adds test data to the database if it is empty.
    /// </summary>
    /// <param name="host">
    /// <see cref="IHost"/> of the running application.
    /// </param>
    /// <returns>The provided instance of the <see cref="IHost"/>.</returns>
    public static IHost SeedDatabase(this IHost host)
    {
        using var scope = host.Services.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<MoviesApiContext>();

        if (context.Movies.Any())
        {
            return host;
        }

        context.Movies.AddRange(new Movie
        {
            Id = Guid.NewGuid(),
            Title = "CREED III",
            Description = """
                Still dominating the boxing world, Adonis Creed is thriving in his career and family life. When Damian,
                a childhood friend and former boxing prodigy resurfaces after serving time in prison, he's eager to prove
                that he deserves his shot in the ring. The face-off between former friends is more than just a fight. To settle
                the score, Adonis must put his future on the line to battle Damian -- a fighter who has nothing to lose.
                """,
            DateOfRelease = new DateTime(2023, 03, 03),
            CreatedDate = DateTime.UtcNow,
            UpdatedDate = DateTime.UtcNow,
        },
        new Movie
        {
            Id = Guid.NewGuid(),
            Title = "ANT-MAN AND THE WASP: QUANTUMANIA",
            Description = """
                Ant-Man and the Wasp find themselves exploring the Quantum Realm, interacting with strange new
                creatures and embarking on an adventure that pushes them beyond the limits of what they thought
                was possible.
                """,
            DateOfRelease = new DateTime(2023, 02, 17),
            CreatedDate = DateTime.UtcNow,
            UpdatedDate = DateTime.UtcNow,
        },
        new Movie
        {
            Id = Guid.NewGuid(),
            Title = "We Have a Ghost",
            Description = """
                The discovery that their house is haunted by a ghost named Ernest makes Kevin's family a social
                media sensation. But when Kevin and Ernest get to the bottom of the mystery of Ernest's past,
                they become targets of the CIA.
                """,
            DateOfRelease = new DateTime(2023, 02, 24),
            CreatedDate = DateTime.UtcNow,
            UpdatedDate = DateTime.UtcNow,
        },
        new Movie
        {
            Id = Guid.NewGuid(),
            Title = "The Whale",
            Description = """
                In a town in Idaho, Charlie, a reclusive and unhealthy English teacher, hides out in his
                flat and eats his way to death. He is desperate to reconnect with his teenage daughter for
                a last chance at redemption.
                """,
            DateOfRelease = new DateTime(2023, 03, 02),
            CreatedDate = DateTime.UtcNow,
            UpdatedDate = DateTime.UtcNow,
        },
        new Movie
        {
            Id = Guid.NewGuid(),
            Title = "Knock at the Cabin",
            Description = """
                While vacationing at a remote cabin in the woods, a young girl and her parents are taken
                hostage by four armed strangers who demand they make an unthinkable choice to avert the apocalypse.
                Confused, scared and with limited access to the outside world, the family must decide what they
                believe before all is lost.
                """,
            DateOfRelease = new DateTime(2023, 02, 03),
            CreatedDate = DateTime.UtcNow,
            UpdatedDate = DateTime.UtcNow,
        });

        context.SaveChanges();

        return host;
    }
}
