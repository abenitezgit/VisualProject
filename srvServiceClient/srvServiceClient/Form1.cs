using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.IO;


namespace srvServiceClient
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public class DataResponse {
            
        
        }

        public class DataRequest {
            private string TableName;
            private string FechaDesde;
            private string FechaHasta;
            private string Skill;
            private int Limit;

            public string tableName
            {
                get { return TableName; }
                set { TableName = value; }
            }

            public string fechaDesde
            {
              get { return FechaDesde; }
              set { FechaDesde = value; }
            }

            public string fechaHasta
            {
                get { return FechaHasta; }
                set { FechaHasta = value; }
            }

            public string skill
            {
                get { return Skill; }
                set { Skill = value; }
            }

            public int limit
            {
                get { return Limit; }
                set { Limit = value; }
            }
 
        };

        private void button1_Click(object sender, EventArgs e)
        {

            //Setea parametros y objetos del request
            //

            Uri address = new Uri(@"http://10.240.8.15:8080/wsCloudera/webapi/grabaciones");       
            HttpWebRequest request = WebRequest.Create(address) as HttpWebRequest;       
            request.Method = "POST";      
            request.ContentType = "application/json";

            //Instancia clase de objeto de la data que se enviara
            DataRequest drq = new DataRequest();

            drq.fechaDesde = "2017011200";
            drq.fechaHasta = "2017011210";
            drq.skill = "SKILL_103EPH";
            drq.limit = 10;
            drq.tableName = "grabaciones";

            //Se parsea la data dejandola en string con estructura json
            string parseDRQ = JsonConvert.SerializeObject(drq);

            //Se manda a ejecutar el request
            byte[] bytes = Encoding.UTF8.GetBytes(parseDRQ); 
            request.ContentLength = bytes.Length; 
            System.IO.Stream stream = request.GetRequestStream();
            stream.Write(bytes, 0, bytes.Length); 
            stream.Close();
            
            //Se obtiene la respuesta 
            using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            {
                StreamReader reader = new StreamReader(response.GetResponseStream());
                Console.WriteLine(reader.ReadToEnd());
            }

        }
    }
}
