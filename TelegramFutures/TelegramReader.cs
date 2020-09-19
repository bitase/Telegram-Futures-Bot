using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Telegram.Bot;
using TelegramFuturesClass;


namespace TelegramFutures
{
    public partial class TelegramReader : UserControl
    {

        private static readonly TelegramBotClient bot = new TelegramBotClient("762817967:AAH6G4fDma5dnsQnzFOfe8jlOurEbfbPiCs");

        delegate void SetTextCallback(string text);

        public TelegramReader()
        {
            InitializeComponent();
        }

        private void TelegramReader_Load(object sender, EventArgs e)
        {
            bot.OnMessage += Bot_OnMessage;
            bot.StartReceiving();
        }

        private void Bot_OnMessage(object sender, Telegram.Bot.Args.MessageEventArgs e)
        {
            if (e.Message.Type == Telegram.Bot.Types.Enums.MessageType.Text)
            {
                Signal Señal = new Signal(e.Message.Text, e.Message.ForwardFromChat.Title, "");
                

                if (e.Message.ForwardFromChat is null)
                {

                }
                else
                { 
                    string texto = e.Message.ForwardFromChat.Title + "\r\n" + e.Message.Text + "\r\n" + "\r\n" + Señal.ToString();
                    SetText(texto);
                }

            }
            //throw new NotImplementedException();
        }

        private void TelegramReader_Leave(object sender, EventArgs e)
        {
            bot.StopReceiving();
        }

        private void SetText(string text)
        {
            if (this.txtRead.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.txtRead.Text = text;
            }
        }
    }
}

