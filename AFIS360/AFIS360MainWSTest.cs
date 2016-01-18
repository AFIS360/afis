using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using AFIS360.AFIS360MainWSRef;

namespace AFIS360
{
    public partial class AFIS360MainWSTest : Form
    {
//        private AFIS360MainWSSoapClient ws = new AFIS360MainWSSoapClient();

        public AFIS360MainWSTest()
        {
            InitializeComponent();
        }

        private void btnAFIS360MainWSGET_Click(object sender, EventArgs e)
        {
//            Console.WriteLine("###-->>Response from WS = " + ws.HelloWorld());
//            Console.WriteLine("###-->>Response from WS = " + ws.HelloWorldSaySomething("Hello Mohsin"));
        }
    }
}
