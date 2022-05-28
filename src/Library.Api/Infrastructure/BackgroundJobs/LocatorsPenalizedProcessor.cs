namespace Library.Api.Infrastructure.BackgroundJobs;

using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BookRentals;
using Domain.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

public class LocatorsPenalizedProcessor : BackgroundService
{
    private readonly BookRentalDbContext _bookRentalDbContext;
    private readonly ISystemClock _clock;

    public LocatorsPenalizedProcessor(BookRentalDbContext bookRentalDbContext, ISystemClock clock)
    {
        this._bookRentalDbContext = bookRentalDbContext;
        this._clock = clock;
    }

    protected async override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var penalizedLocators = await this._bookRentalDbContext.Locators
            .Where(c => c.IsPenalized() && c.ActivePenalty.EndDate <= this._clock.UtcNow)
            .ToListAsync(stoppingToken);

        foreach (var locator in penalizedLocators) locator.DeactivatePenaltyDueToExpiration("Penalty is expired.", this._clock.UtcNow);
    }
}
