
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
using EnergiApp.Domain.BusinessActions;


public class Runner
{
    private readonly NordPoolApiOptions _apiOptions; // Configuration to be populated from appsettings.json
    private readonly NordPoolSsoOptions _ssoOptions; // Configuration to be populated from appsettings.json
    private readonly IRefitClientFactory _refitFactory; // Factory for creating Refit REST API clients, which will be used to create instances of the API and SSO clients.

    private INordPoolApiClient _apiClient = default!; // client used to make requests to the NordPool API.
    private INordPoolSsoClient _ssoClient = default!;                                                  // 
        
    private IEnumerable<Auction> _availableAuctions = default!; // auctions that are fetched for a date range. 
    private Auction _selectedAuction = default!; // This variable holds the currently selected auction. 
    private readonly IAuctionRepository _auctionRepository; // Auction repository for data access operations related to auctions.

    public Runner(
        IOptions<NordPoolApiOptions> apiOptions, // IOptions<T> bliver injiceret af .NET DI-containeren
        IOptions<NordPoolSsoOptions> ssoOptions,
        IAuctionRepository auctionRepository,
        IRefitClientFactory refitFactory
        ) // Constructor takes in the necessary dependencies through dependency injection.
          // The configuration options for the API and SSO clients are injected using IOptions<T>,
          // and the auction repository and Refit client factory are also injected for use in the application logic.
    {
        _apiOptions = apiOptions.Value;
        _ssoOptions = ssoOptions.Value;
        _auctionRepository = auctionRepository;
        _refitFactory = refitFactory;

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

    public interface IRefitClientFactory
    {
        T CreateClient<T>(HttpClient httpClient);
    }

    public class RefitClientFactory : IRefitClientFactory
    {
        public T CreateClient<T>(HttpClient httpClient)
            => RestService.For<T>(httpClient);
    }

    public void InitializeClients() // Internal giver åbenhed i samme assembly.
    {
        // Creating an instance of the SSO client using Refit, which will be used to authenticate and obtain tokens for API requests. Data provided from the configuration options.
        _ssoClient = _refitFactory.CreateClient<INordPoolSsoClient>(
            new HttpClient { BaseAddress = new Uri(_ssoOptions.BaseUrl) });

        // Creating an instance of the token provider, which will handle the retrieval and caching of access tokens for authenticating API requests.
        // It takes the SSO client and its configuration options as parameters. 
        var tokenProvider = new NordPoolTokenProvider(_ssoClient, Options.Create(_ssoOptions));

        // Creating an instance of the API client using Refit, which will be used to make requests to the NordPool API.
        // The HttpClient is configured with an AuthenticatedHttpClientHandler that uses the token provider to ensure that all requests are authenticated.
        // The base URL for the API is provided from the configuration options.
        _apiClient = _refitFactory.CreateClient<INordPoolApiClient>(
            new HttpClient(new AuthenticatedHttpClientHandler(tokenProvider))
            {
                BaseAddress = new Uri(_apiOptions.BaseUrl)
            });
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

