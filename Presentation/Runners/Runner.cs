
using Application.SSO;
using EnergiApp.Application;
using EnergiApp.Application.Exceptions; // This namespace contains custom exceptions related to the Auction API.
using EnergiApp.Application.Utils;
using EnergiApp.Domain;
using EnergiApp.Domain.Repositories;
using EnergiApp.Presentation.Utils; // Utility classes and methods for the console application.
using Microsoft.Extensions.Options; // used for accessing the configuration options 

using System.Globalization;
using EnergiApp.Domain.BusinessActions;

namespace EnergiApp.Presentation.Runners; // This namespace contains the Runner class, which is responsible for executing the main logic of the console application.

public class Runner
{
    private readonly INordPoolApiClient _apiClient;
    private readonly INordPoolSsoClient _ssoClient;
    private readonly IAuctionRepository _auctionRepository;
    private readonly NordPoolApiOptions _apiOptions;
    private readonly NordPoolSsoOptions _ssoOptions;

    private IEnumerable<Auction> _availableAuctions = default!;
    private Auction _selectedAuction = default!;

    public Runner(
        INordPoolApiClient apiClient,
        INordPoolSsoClient ssoClient,
        IAuctionRepository auctionRepository,
        IOptions<NordPoolApiOptions> apiOptions,
        IOptions<NordPoolSsoOptions> ssoOptions)
    {
        _apiClient = apiClient;
        _ssoClient = ssoClient;
        _auctionRepository = auctionRepository;
        _apiOptions = apiOptions.Value;
        _ssoOptions = ssoOptions.Value;
    }

    public async Task RunAsync()
    {
        Console.WriteLine($"Fetching auctions for today {DateTime.Today:d}...");

        _availableAuctions = await _apiClient.GetAuctionsAsync(
            DateTime.UtcNow.Date,
            DateTime.UtcNow.Date.AddDays(1));

        HandleAuctionsCommand();

        var command = ConsoleHelper.RequestSelectedAuctionCommand(_selectedAuction);

        while (command != CommandType.Exit)
        {
            switch (command)
            {
                case CommandType.PlaceCurve:
                    await HandlePlaceCurveCommand();
                    break;

                case CommandType.Trades:
                    await HandleTradesCommand();
                    break;
            }

            command = ConsoleHelper.RequestSelectedAuctionCommand(_selectedAuction);
        }

        Console.WriteLine("Press any key to exit...");
        Console.ReadLine();
    }

    private void HandleAuctionsCommand()
    {
        ConsoleHelper.WriteAuctionsInfo(_availableAuctions);

        Console.WriteLine("Select an auction for further requests:");
        var selectedAuctionId = Console.ReadLine();
        _selectedAuction = _availableAuctions.First(x => x.id == selectedAuctionId);
        ConsoleHelper.WriteDetailedAuctionInfo(_selectedAuction);
    }
    private async Task HandlePlaceCurveCommand()
    {
        Console.WriteLine("Provide portfolio name:");
        var portfolioName = Console.ReadLine();

        Console.WriteLine("Provide area code:");
        var areaCode = Console.ReadLine();

        Console.WriteLine("Provide min price:");
        var minPrice = double.Parse(Console.ReadLine() ?? "-500", CultureInfo.InvariantCulture);

        Console.WriteLine("Provide max price:");
        var maxPrice = double.Parse(Console.ReadLine() ?? "3000", CultureInfo.InvariantCulture);

        var curveOrderRequest =
            OrderGenerator.GenerateStaticCurveOrderRequest(portfolioName, areaCode, _selectedAuction, minPrice, maxPrice);

        Console.WriteLine("Generated curve order:");
        ConsoleHelper.WriteCurveOrderRequest(curveOrderRequest);

        try
        {
            var response = await _apiClient.PostCurveOrderAsync(curveOrderRequest);
            Console.WriteLine("Curve order placed successfully:");
            ConsoleHelper.WriteCurveOrder(response);
        }
        
        catch (NordPoolApiException ex)
        {
            WriteException(ex);
        }
    }
    private async Task HandleTradesCommand()
    {
        Console.WriteLine($"Fetching trades for auction {_selectedAuction.id}...");
        var trades = await _apiClient.GetTradesAsync(_selectedAuction.id, null, null); 
        ConsoleHelper.WriteTradesInfo(trades);
    }        
        
    private static void WriteException(NordPoolApiException ex)
    {
        Console.WriteLine($"Request failed: HTTP STATUS: {ex.HttpStatusCode}");
        Console.WriteLine(ex.Message);
        
    }
    
}    

