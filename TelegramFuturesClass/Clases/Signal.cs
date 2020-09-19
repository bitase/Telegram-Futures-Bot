using System;
using System.ComponentModel.Design;
using FutureBinanceAPI.Models.Enums;

namespace TelegramFuturesClass
{
    public class Signal
    {
        public string SignalFrom; //Bitmex, Free trading
        public Side Side; // BUY, SELL
        public MarginType MarginType; // CROSSED, ISOLATED
        public TraidingPair TradingPair; // BTCUSDT; TRXUSDT
        public TimeInForceType TimeInForceType = TimeInForceType.GTC; // GTC
        public OrderStatus OrderStatus; //New, Filled, Expired
        public OrderType OrderType; //LIMIT, MARKET, STOP, TAKE_PROFIT, STOP_MARKET, TAKE_PROFIT_MARKET, TRAILING_STOP_MARKET
        public string EntradaDesde;
        public string EntradaHasta;
        public string TP1;
        public string TP2;
        public string TP3;
        public string TP4;
        public string StopLoss;        

        public static string from_BitmexCrossPremiumClubID = "Bitmex Cross Premium Club";

        public Signal() { }

        public Signal(string p_mensaje, string p_from, string p_datetime)
        {

            if (p_from is null)
            {
                
            }
            else
            if (p_from.ToLower().IndexOf(from_BitmexCrossPremiumClubID.ToLower()) >= 0)
            {
                from_BitmexCrossPremiumClub(p_mensaje);

            }
            
        }

        private Signal from_BitmexCrossPremiumClub (string p_mensaje)
        {

            string resultado = "";
            string Mensaje = p_mensaje.ToLower();
            

            #region "Busca datos genericos"

            // busca tipo de apalancamiento
            if (Mensaje.IndexOf("cross") >= 0) this.MarginType = MarginType.CROSSED;


            // busca moneda
            if (Mensaje.IndexOf("BTCUSD".ToLower()) >= 0) this.TradingPair = TraidingPair.BTCUSDT;
            else if (Mensaje.IndexOf("XBT".ToLower()) >= 0) this.TradingPair = TraidingPair.BTCUSDT;

            #endregion



            /* FLAGS */
            string flag_avanzar = "OK"; // SI ES OK, CONTINUA, SI NO, INDICA EL VALOR NO ENCONTRADO EN EL TEXTO.


            /* VARIABLES */
            string s_PrecioEntrada1 = "", s_PrecioEntrada2 = "";
            int i_PrecioEntradaDesde = -1, i_PrecioEntradaHasta = -1, Substring_StopLoss_Desde = -1;
            string[] d_TakeProfit = new string[4];
            string d_StopLoss = "";
            int i = 0;

            /* BUSCA TIPO DE OPERACION BUY/SELL */
            int i_buy = Mensaje.IndexOf("buy".ToLower()) - 1;
            int i_sell = Mensaje.IndexOf("sell".ToLower()) - 1;
            if (i_buy > 0)
            {
                // indica BUY
                i_PrecioEntradaDesde = i_buy + 3;
                this.Side = Side.BUY;
            }
            else if (i_sell > 0)
            {
                // Indica SELL
                i_PrecioEntradaDesde = i_sell + 4;
                this.Side = Side.SELL;
            }
            else
            {
                flag_avanzar = "No encuentra el tipo de entra Buy/Sell";
            }


            /* BUSCA EL RANGO DE PRECIO DE ENTRADA */
            i_PrecioEntradaHasta = Mensaje.IndexOf("tp".ToLower(), i_PrecioEntradaDesde) - 1;
            if (i_PrecioEntradaDesde > 0 && i_PrecioEntradaHasta > 0)
            {
                //Busca el precio de entrada
                string[] a_PrecioEntrada = Mensaje.Substring(i_PrecioEntradaDesde, i_PrecioEntradaHasta - i_PrecioEntradaDesde).Replace("\n", " ").Split(" ");
                foreach (string item in a_PrecioEntrada)
                {
                    // si es decimal lo guarda en las variables 1 y 2
                    var isDecimal = Decimal.TryParse(item, out decimal n);
                    if (isDecimal)
                    {
                        if (s_PrecioEntrada1 == "") s_PrecioEntrada1 = item; else s_PrecioEntrada2 = item;
                    }
                }

                // Chequea que haya encontrado los valores correspondientes, caso contrario lanza un error
                if (s_PrecioEntrada1 == "" && s_PrecioEntrada2 == "")
                {
                    // todo: lanza un error porque no encontro el formato que esperaba
                    flag_avanzar = "No encontró precio de entrada";
                }
            }
            else
            {
                // No encontró el formato que esperaba
                // todo: deberiamos tirar un error
                flag_avanzar = "No encontró precio de entrada";
            }


            /* BUSCA LOS TAKE PROFIT */
            if (flag_avanzar == "OK")
            {
                int Substring_Desde = Mensaje.IndexOf("tp".ToLower(), i_PrecioEntradaHasta) - 1;
                int Substring_Hasta = Mensaje.IndexOf("stop loss".ToLower(), Substring_Desde) - 1;
                string[] a_TakeProfits = Mensaje.Substring(Substring_Desde, Substring_Hasta - Substring_Desde).Replace("\n", " ").Split("tp");
                i = 0;
                foreach (string item in a_TakeProfits)
                {
                    string[] a_TakeProfit2 = item.Split(" ");
                    foreach (var item2 in a_TakeProfit2)
                    {
                        if (item2.Contains("%"))
                        {
                            // si es decimal lo guarda en el array de take profit
                            string s_item = item2.Replace("%", "").Trim();
                            var isDecimal = Decimal.TryParse(s_item, out decimal n);
                            if (isDecimal)
                            {
                                d_TakeProfit[i] = s_item; //  Decimal.Parse(s_item,);
                                i++;
                            }
                        }
                    }
                }

                if (i > 0)
                    Substring_StopLoss_Desde = Substring_Hasta;
                else
                    flag_avanzar = "No encontro el TakeProfit";
            }


            /* BUSCA EL STOP LOSS */
            if (flag_avanzar == "OK")
            {
                int Substring_StopLoss_Hasta = Mensaje.IndexOf("%".ToLower(), Substring_StopLoss_Desde) - 1;
                string[] a_StopLoss = Mensaje.Substring(Substring_StopLoss_Desde, Substring_StopLoss_Hasta - Substring_StopLoss_Desde).Replace("\n", " ").Split(" ");
                foreach (string item in a_StopLoss)
                {

                    // si es decimal lo guarda en el array de take profit
                    string s_item = item.Replace("%", "").Trim();
                    var isDecimal = Decimal.TryParse(s_item, out decimal n);
                    if (isDecimal)
                    {
                        d_StopLoss = s_item; // Decimal.Parse(s_item);
                        break;
                    }

                }

                // Chequea que haya encontrado el StopLoss, caso contrario informa un error
                if (d_StopLoss == "")
                {
                    // todo: lanza un error porque no encontro el formato que esperaba
                    flag_avanzar = "No encontro el stop Loss";
                }

                if (flag_avanzar == "OK")
                {

                    Console.WriteLine("Señal:" + from_BitmexCrossPremiumClubID);
                    Console.WriteLine("Tipo entrada:" + this.Side);
                    Console.WriteLine("Precio Entrada:" + s_PrecioEntrada1 + " - " + s_PrecioEntrada2);
                    Console.WriteLine("TakeProfit:" + d_TakeProfit[0] + "%, " + d_TakeProfit[1] + "%, " + d_TakeProfit[2] + "%, " + d_TakeProfit[3] + "%, ");
                    Console.WriteLine("StopLoss:" + d_StopLoss);


                    //Completa el objeto
                    this.SignalFrom = from_BitmexCrossPremiumClubID;
                    this.EntradaDesde = s_PrecioEntrada1;
                    this.EntradaHasta = s_PrecioEntrada2;
                    this.TP1 = d_TakeProfit[0];
                    this.TP2 = d_TakeProfit[1];
                    this.TP3 = d_TakeProfit[2];
                    this.TP4 = d_TakeProfit[3];
                    this.StopLoss = d_StopLoss;

                    flag_avanzar = "OK";
                }
                Console.WriteLine(flag_avanzar);
            }


            return this;
            

        }

#pragma warning disable CS0114 // Member hides inherited member; missing override keyword
        public string ToString()
#pragma warning restore CS0114 // Member hides inherited member; missing override keyword
        {
            string resultado = "";
            resultado += "Señal: " + this.SignalFrom + "\r\n";
            resultado += "Tipo Apalancamiento: " + this.MarginType + "\r\n";
            resultado += "Moneda: " + this.TradingPair + "\r\n";
            resultado += "Tipo entrada: " + this.Side + "\r\n";
            resultado += "Precio Entrada: " + this.EntradaDesde + " - " + this.EntradaHasta + "\r\n";
            resultado += "TakeProfit: " + this.TP1 + "%, " + this.TP2 + "%, " + this.TP3 + "%, " + this.TP4 + "%, " + "\r\n";
            resultado += "StopLoss: " + this.StopLoss + "\r\n";
            return resultado;
        }
    }


}
