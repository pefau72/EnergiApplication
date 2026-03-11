namespace EnergiApp.Presentation.Utils
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using EnergiApp.Domain;
    

    public static class ConsoleHelper
    {
        private const string Format = "yyyy-MM-ddTHH:mm:ssZ";

        public static void WriteAuctionsInfo(IEnumerable<Auction> auctions)
        {
            Console.WriteLine("Open auctions:");

            foreach (var auction in auctions.Where(x => x.state == Auction.AuctionStateType.Open))
                Console.WriteLine(auction.id);

            Console.WriteLine();

            Console.WriteLine("Closed auctions:");

            foreach (var auction in auctions.Where(x => x.state == Auction.AuctionStateType.Closed))
                Console.WriteLine(auction.id);

            Console.WriteLine();


            Console.WriteLine("ResultsPublished auctions:");

            foreach (var auction in auctions.Where(x => x.state == Auction.AuctionStateType.Resultspublished))
                Console.WriteLine(auction.id);
        }

        public static void WriteDetailedAuctionInfo(Auction auction)
        {
            Console.WriteLine($"Selected auction: {auction.id}");
            Console.WriteLine($"Supported currencies with Min and Max prices are following:");

            foreach (var currencyInfo in auction.currencies)
            {
                WriteCurrencyInfo(currencyInfo);
            }

            Console.WriteLine($"Available portfolios for selected auction are following:");

            foreach (var portfolio in auction.portfolios)
            {
                WritePortfolioInfo(portfolio);
            }
        }

        private static void WriteCurrencyInfo(Currency currency)
        {
            Console.WriteLine($"\t{currency.CurrencyCode}");
            Console.WriteLine($"\tMin: {currency.MinPrice} - Max: {currency.MaxPrice}");
            Console.WriteLine("---");
        }

        private static void WritePortfolioInfo(Portfolio portfolio)
        {
            Console.WriteLine($"\tId: {portfolio.Id}");
            Console.WriteLine($"\tName: {portfolio.Name}");
            Console.WriteLine($"\tCompany: {portfolio.CompanyName}");
            Console.WriteLine($"\tCurrency: {portfolio.Currency}");
            Console.WriteLine($"\tPermission: {portfolio.Permission}");
            Console.WriteLine("\tTradable areas for portfolio (area code - name - eic code - curve min volume limit - curve max volume limit):");
            foreach (var area in portfolio.Areas)
            {
                Console.WriteLine($"\t\t{area.Code} - {area.Name} - {area.EicCode} - ({area.CurveMinVolumeLimit}) - ({area.CurveMaxVolumeLimit})");
            }
            Console.WriteLine("---");
        }

        public static void WriteOrdersInfo(OrdersResponse orders)
        {
            Console.WriteLine($"Listing {orders.CurveOrders.Count} curve orders:");
            foreach (var curveOrder in orders.CurveOrders) WriteCurveOrder(curveOrder);
            Console.WriteLine();
        }

        
        public static void WriteCurveOrder(CurveOrder curveOrder)
        {
            Console.WriteLine("---");
            Console.WriteLine($"OrderId: {curveOrder.OrderId}");
            Console.WriteLine($"Area: {curveOrder.AreaCode}");
            Console.WriteLine($"State: {curveOrder.State}");
            Console.WriteLine($"Portfolio: {curveOrder.Portfolio}");
            Console.WriteLine($"Company: {curveOrder.CompanyName}");
            Console.WriteLine($"Comment: {curveOrder.Comment}");
            WriteCurves(curveOrder);
            Console.WriteLine("---");
        }
        
        

        public static void WriteCurveOrderRequest(CurveOrderRequest curveOrderRequest)
        {
            Console.WriteLine("---");
            Console.WriteLine($"AuctionId: {curveOrderRequest.AuctionId}");
            Console.WriteLine($"AreaCode: {curveOrderRequest.AreaCode}");
            Console.WriteLine($"Portfolio: {curveOrderRequest.Portfolio}");
            Console.WriteLine($"Comment: {curveOrderRequest.Comment}");
            WriteCurves(curveOrderRequest);
            Console.WriteLine("---");
        }

        private static void WriteCurves(CurveOrder curveOrder)
        {
            Console.WriteLine("Curves:");
            foreach (var curve in curveOrder.Curves)
            {
                Console.WriteLine($"Curve contract: {curve.ContractId}");
                Console.WriteLine($"Prices:\t\t{string.Join("\t", curve.CurvePoints.Select(x => x.Price))}");
                Console.WriteLine($"Volumes:\t{string.Join("\t", curve.CurvePoints.Select(x => x.Volume))}");
                Console.WriteLine("");
            }
        }
        
        
        private static void WriteCurves(CurveOrderRequest curveOrderRequest)
        {
            Console.WriteLine("Curves:");
            foreach (var curve in curveOrderRequest.Curves)
            {
                Console.WriteLine($"Curve contract: {curve.ContractId}");
                Console.WriteLine($"Prices:\t\t{string.Join("\t", curve.CurvePoints.Select(x => x.Price))}");
                Console.WriteLine($"Volumes:\t{string.Join("\t", curve.CurvePoints.Select(x => x.Volume))}");
                Console.WriteLine("");
            }
        }

        public static CommandType RequestSelectedAuctionCommand(Auction auction)
        {
            if (auction.state == Auction.AuctionStateType.Open)
            {
                Console.WriteLine(
                    "Available options: \"Orders\", \"PlaceCurve\", \"GetAllCurveOrderVersions\",  \"Auctions\", \"AuctionContracts\",   \"Exit\". Specify one of the options:");
                CommandType command;
                while (!Enum.TryParse(Console.ReadLine(), out command))
                    Console.WriteLine("Incorrect option specified! Try again.");

                return command;
            }

            if (auction.state == Auction.AuctionStateType.Resultspublished)
            {
                Console.WriteLine(
                    "Available options: \"Orders\", \"Trades\", \"Prices\", \"PortfolioVolumes\", \"Auctions\", \"AuctionContracts\",  \"Exit\". Specify one of the options:");
                CommandType command;
                while (!Enum.TryParse(Console.ReadLine(), out command))
                    Console.WriteLine("Incorrect option specified! Try again.");

                return command;
            }

            if (auction.state == Auction.AuctionStateType.Closed)
            {
                Console.WriteLine("Available options: \"Orders\", \"Auctions\", \"AuctionContracts\",  \"Exit\". Specify one of the options:");
                CommandType command;
                while (!Enum.TryParse(Console.ReadLine(), out command))
                    Console.WriteLine("Incorrect option specified! Try again.");

                return command;
            }

            return CommandType.None;
        }

        public static void WriteTradesInfo(IEnumerable<TradesSummary> trades)
        {
            Console.WriteLine("Trades fetched:");
            Console.WriteLine("Trades for Curve Orders");
            foreach (var tradesSummary in trades.Where(x => x.OrderResultType == OrderResultType.Curve)) WriteTradeSummary(tradesSummary, OrderResultType.Curve);

            
        }

        private static void WriteTradeSummary(TradesSummary tradesSummary, OrderResultType orderResultType)
        {
            Console.WriteLine($"AuctionId: {tradesSummary.AuctionId}");
            Console.WriteLine($"AreaCode: {tradesSummary.AreaCode}");
            Console.WriteLine($"Company: {tradesSummary.CompanyName}");
            Console.WriteLine($"Portfolio: {tradesSummary.Portfolio}");
            Console.WriteLine($"Currency: {tradesSummary.CurrencyCode}");
            

            WriteTrades(tradesSummary.Trades);
        }

        private static void WriteTrades(IEnumerable<Trade> trades)
        {
            foreach (var trade in trades)
            {
                Console.WriteLine($"TradeId: {trade.TradeId}");
                Console.WriteLine($"Contract: {trade.ContractId}");
                Console.WriteLine($"Price: {trade.Price}");
                Console.WriteLine($"Volumes: {trade.Volume}");
                Console.WriteLine($"TradeSide: {trade.Side}");
                Console.WriteLine($"Status: {trade.Status}");
                Console.WriteLine("");
            }
        }

        public static void WritePricesInfo(PricesResponse prices)
        {
            Console.WriteLine($"\nAuctionId: {prices.Auction}");
            Console.WriteLine();

            foreach (var contract in prices.Contracts) WritePrice(contract);
        }

        private static void WritePrice(ContractPrice contract)
        {
            Console.WriteLine($"ContractId: {contract.ContractId}");
            Console.WriteLine($"DeliveryStart: {contract.DeliveryStart}");
            Console.WriteLine($"DeliveryEnd: {contract.DeliveryEnd}");

            foreach (var area in contract.Areas)
            {
                Console.WriteLine($"\tAreaCode: {area.AreaCode}");
                foreach (var price in area.Prices)
                {
                    if (price.MarketPrice.HasValue)
                    {
                        Console.WriteLine($"\tCurrencyCode: {price.CurrencyCode}");
                        Console.WriteLine($"\tMarketPrice: {price.MarketPrice}");
                        Console.WriteLine($"\tStatus: {price.Status}");
                    }
                    else
                    {
                        Console.WriteLine("\tNo price for area");
                    }
                }
                Console.WriteLine();
            }
        }

        

        public static void WriteAuctionContractsInfo(AuctionMultiResolutionResponse auction)
        {
            Console.WriteLine($"Auction: {auction.Id}");
            Console.WriteLine($"Name: {auction.Name}");
            Console.WriteLine($"State: {auction.State}");
            Console.WriteLine($"CloseForBidding: {auction.CloseForBidding.ToString(Format)}");
            Console.WriteLine($"DeliveryStart: {auction.DeliveryStart.ToString(Format)}");
            Console.WriteLine($"DeliveryEnd: {auction.DeliveryEnd.ToString(Format)}");
            Console.WriteLine("---");
            Console.WriteLine("AvailableOrderTypes:");
            Console.WriteLine();
            
            foreach (var orderType in auction.AvailableOrderTypes)
            {
                Console.WriteLine($"\tOrderType Id: {orderType.Id}");
                Console.WriteLine($"\tOrderType Name: {orderType.Name}");
            }
            Console.WriteLine("---");

            Console.WriteLine("Currencies:");
            Console.WriteLine();
            foreach (var currency in auction.Currencies)
            {
                WriteCurrencyInfo(currency);
            }
            Console.WriteLine("Portfolios:");
            foreach (var portfolio in auction.Portfolios)
            {
                WritePortfolioInfo(portfolio);
            }
            Console.WriteLine("AreaContractGroup:");
            foreach (var areaContractGroup in auction.Contracts)
            {
                Console.WriteLine($"\tAreaCode: {areaContractGroup.AreaCode}");
                WriteContracts(areaContractGroup.Contracts);
            }
            Console.WriteLine();
        }

        private static void WriteContracts(IEnumerable<Contract> contracts)
        {
            foreach (var contract in contracts)
            {
                Console.WriteLine($"\tId: {contract.Id}");
                Console.WriteLine($"\tDeliveryStart: {contract.DeliveryStart.ToString(Format)}");
                Console.WriteLine($"\tDeliveryEnd: {contract.DeliveryEnd.ToString(Format)}");
            }
            Console.WriteLine("---");
        }
    }

    public enum CommandType
    {
        None,
        Orders,
        GetAllCurveOrderVersions,
        Trades,
        Prices,
        PortfolioVolumes,
        PlaceCurve,
        Auctions,
        Exit,
        AuctionContracts,       

    }
}