namespace EnergiApp.Application
{
    using EnergiApp.Domain;
    using Refit;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Threading.Tasks;

    [Headers("Authorization: Bearer")]  // This header is added automatically by Refit
    public interface INordPoolApiClient
    {
        /// <summary>
        ///     Lists all auctions that have "closeForBiddingTime" within provided timespan
        /// </summary>
        /// <param name="closeBiddingFrom">CloseForBidding start period</param>
        /// <param name="closeBiddingTo">CloseForBidding end period</param>
        /// <returns>Collection of auctions <see cref="IEnumerable{Auction}" /></returns>
        [Get("/auctions")]
        Task<IEnumerable<Auction>> GetAuctionsAsync([Query] DateTime closeBiddingFrom,
            [Query] DateTime closeBiddingTo);

        ///     Get all trades for selected auction. Auction needs to be in "ResultsPublished" state in order to have trades.
        /// <param name="auctionId">Auction id for which the trades should be requested</param>
        /// <param name="portfolios">Collection of portfolios for additional filtering (optional). Provide null for no filtering</param>
        /// <param name="areas">Collection of areas for additional filtering (optional). Provide null for no filtering</param>
        /// <returns>Collection of trades <see cref="IEnumerable{TradeSummary}" /></returns>
        [Get("/auctions/{auctionId}/trades")]
        Task<IEnumerable<TradesSummary>> GetTradesAsync([Url] string auctionId,
            [Query(CollectionFormat.Multi)] string[] portfolios, [Query(CollectionFormat.Multi)] string[] areas);

               
        ///     Post a new curve order through Auction API
        /// <param name="curveOrderRequest">Curve order to be placed<see cref="CurveOrderRequest" /></param>
        /// <returns>Placed curve order<see cref="CurveOrder" /></returns>
        [Post("/curveorders")]
        Task<CurveOrder> PostCurveOrderAsync([Body] CurveOrderRequest curveOrderRequest);
              
        
    }
    

 
    
}