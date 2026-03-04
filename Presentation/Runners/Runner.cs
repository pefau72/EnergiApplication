
using Application.SSO;
using EnergiApp.Application;
using EnergiApp.Application.Exceptions; // This namespace contains custom exceptions related to the Auction API.
using EnergiApp.Application.Utils;
using EnergiApp.Domain;
using EnergiApp.Domain.Repositories;
using EnergiApp.Presentation.Utils; // Utility classes and methods for the console application.
using Microsoft.Extensions.Options; // used for accessing the configuration options 
using Refit; // Refit is a REST library for .NET that allows you to define API clients using interfaces
using System.Globalization;

public class Runner
{
    private readonly NordPoolApiOptions _apiOptions; // Configuration to be populated from appsettings.json
    private readonly NordPoolSsoOptions _ssoOptions;
        
    private INordPoolApiClient _apiClient = default!; // client used to make requests to the NordPool API.
    private INordPoolSsoClient _ssoClient = default!;                                                  // 
        
    private IEnumerable<Auction> _availableAuctions = default!; // auctions that are fetched for a date range. 
    private Auction _selectedAuction = default!; // This variable holds the currently selected auction. 
    private readonly IAuctionRepository _auctionRepository; // Auction repository for data access operations related to auctions.

    public Runner(
        IOptions<NordPoolApiOptions> apiOptions, // IOptions<T> bliver injiceret af .NET DI-containeren
        IOptions<NordPoolSsoOptions> ssoOptions,
        IAuctionRepository auctionRepository) // Constructor takes in the Auction API and the SSO service. 
    {
        _apiOptions = apiOptions.Value;
        _ssoOptions = ssoOptions.Value;
        _auctionRepository = auctionRepository;

    }
    public async Task RunAsync() // This is the main method that runs the application logic. 
    {
        InitializeClients(); 
        Console.WriteLine($"Fetching auctions for today {DateTime.Today:d}..."); 
        _availableAuctions = await _apiClient.GetAuctionsAsync(DateTime.UtcNow.Date, DateTime.UtcNow.Date.AddDays(1)); 
            
        HandleAuctionsCommand(); 
            
        var command = ConsoleHelper.RequestSelectedAuctionCommand(_selectedAuction); 
            
        while (command != CommandType.Exit) 
        { 
            switch (command) 
            { 
                case CommandType.PlaceCurve: await HandlePlaceCurveCommand();
                    break;
                case CommandType.Trades: await HandleTradesCommand();
                    break;
            } 
            
            command = ConsoleHelper.RequestSelectedAuctionCommand(_selectedAuction); }
            
        Console.WriteLine("Press any key to exit..."); Console.ReadLine();
    }

    private void InitializeClients()
    { 
        _ssoClient = RestService.For<INordPoolSsoClient>(new HttpClient { BaseAddress = new Uri(_ssoOptions.BaseUrl) });

        var tokenProvider = new NordPoolTokenProvider(_ssoClient, Options.Create(_ssoOptions));

        _apiClient = RestService.For<INordPoolApiClient>(new HttpClient(new AuthenticatedHttpClientHandler(tokenProvider)) { BaseAddress = new Uri(_apiOptions.BaseUrl) });
    }

    private void HandleAuctionsCommand()
    {
        ConsoleHelper.WriteAuctionsInfo(_availableAuctions);

        Console.WriteLine("Select an auction for further requests:");
        var selectedAuctionId = Console.ReadLine();
        _selectedAuction = _availableAuctions.First(x => x.Id == selectedAuctionId);
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
        Console.WriteLine($"Fetching trades for auction {_selectedAuction.Id}...");
        var trades = await _apiClient.GetTradesAsync(_selectedAuction.Id, null, null); 
        ConsoleHelper.WriteTradesInfo(trades);
    }        
        
    private static void WriteException(NordPoolApiException ex)
    {
        Console.WriteLine($"Request failed: HTTP STATUS: {ex.HttpStatusCode}");
        Console.WriteLine(ex.Message);
        
    }
    
}    

