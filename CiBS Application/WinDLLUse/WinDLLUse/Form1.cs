using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using AyandehPCPosDLL;

namespace WinDLLUse
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

    

        private void button1_Click(object sender, EventArgs e)
        {
            string pos_ip = Txt_pos_ip.Text.Trim();
            int pos_port = Convert.ToInt32(Txt_pos_port.Text);
            int is_serial = Convert.ToInt32(Txt_is_serial.Text);
            string pos_serial = Txt_pos_serial.Text.Trim();
            int time_out = Convert.ToInt32(Txt_time_out.Text);
            int terminal_order=Convert.ToInt32(Txt_terminal_order.Text);

  
           // try
           // { 
            AyandehPcPos AyandehPcPos1 = new AyandehPcPos(pos_ip, pos_port, is_serial, pos_serial, time_out, terminal_order);

            //send data
                string Amount = TxtAmount.Text;
                AyandehPcPos1.Sale(Amount);


            //receive data
            TxtHasError.Text = Convert.ToString(AyandehPcPos1.HasError);
            TxtRespCode.Text = AyandehPcPos1.RespCode;
            TxtRespMessage.Text = AyandehPcPos1.RespMessage;
            TxtTrace.Text = AyandehPcPos1.Trace;
            TxtRefrence.Text = AyandehPcPos1.Refrence;
            TxtCardNo.Text = AyandehPcPos1.CardNo;
            TxtTrDate.Text = AyandehPcPos1.TrDate;
            TxtTrTime.Text = AyandehPcPos1.TrTime;
            TxtCardType.Text = AyandehPcPos1.CardType;
            TxtTerminal.Text = AyandehPcPos1.Terminal;
            TxtMerchant.Text = AyandehPcPos1.Merchant;
            TxtTrAmount.Text = AyandehPcPos1.TrAmount;
          //  } catch (Exception ex) { MessageBox.Show(ex.ToString()); }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            TxtHasError.Text = "";
            TxtRespCode.Text = "";
            TxtRespMessage.Text = "";
            TxtTrace.Text = "";
            TxtRefrence.Text = "";
            TxtCardNo.Text = "";
            TxtTrDate.Text = "";
            TxtTrTime.Text = "";
            TxtCardType.Text = "";
            TxtTerminal.Text = "";
            TxtMerchant.Text = "";
            TxtTrAmount.Text = "";
        }
    }
}
