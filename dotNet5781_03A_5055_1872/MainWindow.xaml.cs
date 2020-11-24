using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using dotNet_02_5055_1872;
namespace dotNet5781_03A_5055_1872
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private BusLine currentDisplayBusLine;
        private static Random r = new Random(DateTime.Now.Millisecond);
        private CollectionOfBusLines collectionOfbusLines = new CollectionOfBusLines();
        private List<BusLineStation> buslinestation = new List<BusLineStation>();
        public MainWindow()
        {
            int numberOfStatian;
            int numberLine;

            for (int i = 0; i < 10; i++)
            {
                numberLine = r.Next(999);
                numberOfStatian = r.Next(2, 10);
                BusLine busLine = new BusLine(numberLine);
                collectionOfbusLines.CollectionOfLines = AddLineFirstly(ref busLine, ref buslinestation, numberOfStatian);
                Console.WriteLine("good the Line insrted to list ");
            }

            for (int i = 0; i < 2; i++)
            {
                numberLine = r.Next(999);
                BusLine busLine1 = new BusLine(numberLine)
                {
                    RouteTheLine = buslinestation
                };
                collectionOfbusLines.CollectionOfLines = busLine1;
            }
            InitializeComponent();
            cbBusLines.ItemsSource = collectionOfbusLines;
            cbBusLines.DisplayMemberPath = "LineNumber";
            cbBusLines.SelectedIndex = -1;
            cbBusLines.SelectionChanged += CbBusLines_SelectionChanged;
        }

        private void ShowBusLine(int index)
        {
            currentDisplayBusLine = collectionOfbusLines[index];
            UpGrid.DataContext = currentDisplayBusLine;
            lbBusLineStations.DataContext = currentDisplayBusLine.RouteTheLine;
        }

        private void CbBusLines_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ShowBusLine((cbBusLines.SelectedValue as BusLine).LineNumber);
        }

        //private void TbArea_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    UpGrid.DataContext = currentDisplayBusLine.Area1;
        //}

        /// <summary>
        /// A function that receives an object of the bus line type, a list of objects of line stations, and the number of stations in the line.
        /// and makes an initial boot to ten lines and includes the requirements.
        /// </summary>
        /// <param name="busLine"></param>
        /// <param name="buslinestation"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static BusLine AddLineFirstly(ref BusLine busLine, ref List<BusLineStation> buslinestation, int count)
        {
            List<BusLineStation> buslinestation1 = new List<BusLineStation>();
            int stationNumber;

            string[] stationaddress1 = new string[42];
            AddAdress(ref stationaddress1);

            for (int i = 0; i < count; i++)
            {
                stationNumber = r.Next(999999);
                buslinestation1.Add(new BusLineStation(stationNumber, stationaddress1[r.Next(42)]));
            }

            foreach (BusLineStation item in buslinestation1)
            {
                buslinestation.Add(item);
            }

            busLine.RouteTheLine = buslinestation1;
            return busLine;
        }

        /// <summary>
        /// A function designed to initialize the station addresses is called in an initial initialization function.
        /// </summary>
        /// <param name="stationaddress1"></param>
        public static void AddAdress(ref string[] stationaddress1)
        {
            stationaddress1[0] = " כנסת ישראל/רות ";
            stationaddress1[1] = " כנסת ישראל/הושע";
            stationaddress1[2] = "יואל/כנסת ישראל ";
            stationaddress1[3] = " כנסת ישראל/עובדיה";
            stationaddress1[4] = " הרב עובדיה יוסף/דרך מנחם בגין";
            stationaddress1[5] = "שדה תעופה בן גוריון/טרמינל 3";
            stationaddress1[6] = "תרכבת בני ברק";
            stationaddress1[7] = "הרב אלישיב/באר יעקב ";
            stationaddress1[8] = "ת.רכבת מזכרת בתיה/איסוף ";
            stationaddress1[9] = "בר כוכבא/ת.מרכזית פתח תקווה ";
            stationaddress1[10] = "הרצל/החלוצים ";
            stationaddress1[11] = " הרצל/החלוצים";
            stationaddress1[12] = "הרצל/התמרים  ";
            stationaddress1[13] = "הרצל/הפרחים ";
            stationaddress1[14] = "הרצל/התמנים ";
            stationaddress1[15] = " חזון אי''ש/הרב אלישיב";
            stationaddress1[16] = "חזון אי''ש/הרב קנייבסקי ";
            stationaddress1[17] = "חזון אי''ש/הרב שטיינמן ";
            stationaddress1[18] = " חזון אי''ש/הרב פוברסקי";
            stationaddress1[19] = "חזון אי''ש/הרב ברמן ";
            stationaddress1[20] = " שדרות רימון/שדרות חיטה";
            stationaddress1[21] = "שדרות רימון/שדרות שעורה ";
            stationaddress1[22] = "שדרות רימון/שדרות גפן ";
            stationaddress1[23] = " שדרות רימון/שדרות תאנה";
            stationaddress1[24] = " שדרות רימון/שדרות תמר";
            stationaddress1[25] = "עמנואל זיסמן/דוד כהן ";
            stationaddress1[26] = "עמנואל זיסמן/אהרון כהן ";
            stationaddress1[27] = "עמנואל זיסמן/משה כהן ";
            stationaddress1[28] = " עמנואל זיסמן/יצחק כהן";
            stationaddress1[29] = "עמנואל זיסמן/יעקב כהן ";
            stationaddress1[30] = "נעמי שמר/דר' ג'ין קלוס פישמן ";
            stationaddress1[31] = " נעמי שמר/דר' ג'ין חדוה פישמן";
            stationaddress1[32] = "נעמי שמר/דר' ג'ין קרלוס השודד פישמן ";
            stationaddress1[33] = " קוקו השמן";
            stationaddress1[34] = "יונתן הקטן  ";
            stationaddress1[35] = "איראן בוחרת ישראל ";
            stationaddress1[36] = "טראמפ הלך באסההה ";
            stationaddress1[37] = "מה הלוז פה ";
            stationaddress1[38] = " גני תקווה";
            stationaddress1[39] = "קרית אונו  ";
            stationaddress1[40] = " האדמור מנדי כהנא";
        }

    }
}
