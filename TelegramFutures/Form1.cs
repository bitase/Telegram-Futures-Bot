using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FutureBinanceAPI.API;
using FutureBinanceAPI.Endpoints;
using FutureBinanceAPI.Models;
using FutureBinanceAPI.Models.Orders;
using FutureBinanceAPI.Models.Enums;
using System.Configuration;

namespace TelegramFutures
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
         
            
        }

        private void testBinanceFuture()
        {
            /* web de referencia:
             * https://github.com/fman42/FutureBinanceAPI
             * */

            var Api_Clave = "e3a7d6025708b6c3008c2cb56b209d9a4a180d8af9869533c0fe55719b88da5b";
            var API_secreta = "8c6e0568ab0a2d3a9340486b602c87805ccdd1863bee0c4e8060f6959a35e85e";

            AuthClient authClient = new AuthClient(Api_Clave, API_secreta, true); // el parametro True indica que usa la testnet.

            TickerEndPoint Ticker = new TickerEndPoint(authClient);

            var GetPrice = Task.Run(async () => await Ticker.GetPriceTickerAsync(TraidingPair.BTCUSDT));
            GetPrice.Wait();

            /* OBTIENE EL PRECIO */
            OrderEndPoint order = new OrderEndPoint(authClient); // Create an order endpoint

            decimal Precio = decimal.Round( GetPrice.Result.Price * Convert.ToDecimal("0,98"),2);
            decimal Cantidad = Convert.ToDecimal("0,001");

            /* ESTABLE EL APALANCAMIENTO */
            TradeEndPoint trade = new TradeEndPoint(authClient); // Create a trade endpoint

            var setLeverage = Task.Run(async () => await trade.SetLeverageAsync(TraidingPair.BTCUSDT, 25)); // Set 25 leverage for BTCUSDT
            setLeverage.Wait();

            var setMargin = Task.Run(async () => await trade.SetMarginTypeAsync(TraidingPair.BTCUSDT, MarginType.CROSSED));
            setMargin.Wait();


            /* GENERA LA ORDEN */
            LimitOrder limitOrder = new LimitOrder(TraidingPair.BTCUSDT, Side.SELL, Cantidad, Precio, TimeInForceType.GTC);
            var limitOrderResult = Task.Run(async () => await order.SetAsync(limitOrder));
            limitOrderResult.Wait();

            /* CANCELA LA ORDEN */
            var cancelOrderResult = Task.Run(async () => await order.CancelAsync(TraidingPair.BTCUSDT, limitOrderResult.Result.OrderId));
            cancelOrderResult.Wait();

            var aaa = 1;
            aaa = aaa + 2;

        }

        private void btnEnviarOrdenBinance_Click(object sender, EventArgs e)
        {
            testBinanceFuture();
        }
    }
}
