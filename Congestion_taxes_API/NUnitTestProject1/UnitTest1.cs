using Congestion_taxes_API;
using NUnit.Framework;
using System;

namespace NUnitTestProject1
{
    public class Tests
    {

        private Congestion_taxes_API.Congestion_Taxes_API_interface API; 
        [SetUp]
        public void Setup()
        {
            //Create Bilregistret

            VehicleDB vehicalDB = VehicleDB.getInstance();

            vehicalDB.AddVechical(new Vehicle("ABC123", VehicleType.personalCar));
            vehicalDB.AddVechical(new Vehicle("MCC111", VehicleType.motorbike));
            vehicalDB.AddVechical(new Vehicle("EFG55A", VehicleType.personalCar));
            vehicalDB.AddVechical(new Vehicle("34567", VehicleType.militaryVehical));
            vehicalDB.AddVechical(new Vehicle("BUS123", VehicleType.heavyBuss));
            vehicalDB.AddVechical(new Vehicle("SOS112", VehicleType.bluelightVehical));
            vehicalDB.AddVechical(new Vehicle("CDX123", VehicleType.diplomaticVehical));
            

            API = new Congestion_Taxes_API();

        }

        [Test]
        /* req 1. Max cost per day 60 kr */
        public void Test_Requirment1_Max60krperday()
        {
            API.AddPassing("ABC123", StringToDate("2020-12-01 07:01:00"));  //22 kr
            API.AddPassing("ABC123", StringToDate("2020-12-01 12:22:00"));  //9 kr
            API.AddPassing("ABC123", StringToDate("2020-12-01 15:00:10"));  //16 kr
            API.AddPassing("ABC123", StringToDate("2020-12-01 16:15:10"));  //22 kr: Total 66 kr

            int costOfDay = API.GetBillingInfoForVehicalAndDay("ABC123", StringToDate("2020-12-01 00:00:00"));
            Assert.AreEqual(60, costOfDay); 
        }
        [Test]
        /* req2. No cost for some vehical types */
        public void Test_Requirment2_NoChargeForSomeVechicaletypes()
        {

            //Bluelight
            API.AddPassing("SOS112", StringToDate("2020-12-01 07:01:00"));  //22 kr
            API.AddPassing("SOS112", StringToDate("2020-12-01 12:22:00"));  //9 kr
            API.AddPassing("SOS112", StringToDate("2020-12-01 15:00:10"));  //16 kr
            API.AddPassing("SOS112", StringToDate("2020-12-01 16:15:10"));  //22 kr: Total 66 kr

            int costOfDay = API.GetBillingInfoForVehicalAndDay("SOS112", StringToDate("2020-12-01 00:00:00"));
            Assert.AreEqual(0, costOfDay);

            //Bus > 14 ton
            API.AddPassing("BUS123", StringToDate("2020-12-03 07:01:00"));  //22 kr
            API.AddPassing("BUS123", StringToDate("2020-12-03 12:22:00"));  //9 kr
           
            costOfDay = API.GetBillingInfoForVehicalAndDay("BUS123", StringToDate("2020-12-03 00:00:00"));
            Assert.AreEqual(0, costOfDay);
          
            //Military
            API.AddPassing("34567", StringToDate("2020-12-08 07:01:00"));  //22 kr
            API.AddPassing("34567", StringToDate("2020-12-08 12:22:00"));  //9 kr

            costOfDay = API.GetBillingInfoForVehicalAndDay("34567", StringToDate("2020-12-08 00:00:00"));
            Assert.AreEqual(0, costOfDay);


            //MC
            API.AddPassing("MCC111", StringToDate("2020-12-08 07:01:00"));  //22 kr
            API.AddPassing("MCC111", StringToDate("2020-12-08 12:22:00"));  //9 kr

            costOfDay = API.GetBillingInfoForVehicalAndDay("MCC111", StringToDate("2020-12-08 00:00:00"));
            Assert.AreEqual(0, costOfDay);
            //MC
            API.AddPassing("CDX123", StringToDate("2020-12-08 07:01:00"));  //22 kr
            API.AddPassing("CDX123", StringToDate("2020-12-08 12:22:00"));  //9 kr

            costOfDay = API.GetBillingInfoForVehicalAndDay("CDX123", StringToDate("2020-12-08 00:00:00"));
            Assert.AreEqual(0, costOfDay);
            

        }

        [Test]
        /* req 3. Only charge once per hour */
        public void Test_Requirment3_OnlyOneChargePerHour()
        {
            API.AddPassing("ABC123", StringToDate("2020-12-02 06:22:00"));  //9 kr
            API.AddPassing("ABC123", StringToDate("2020-12-02 06:30:00"));  //16 kr
            API.AddPassing("ABC123", StringToDate("2020-12-02 06:48:10"));  //16 kr
            API.AddPassing("ABC123", StringToDate("2020-12-02 07:15:15"));  //22 kr: Total 22 kr

            int costOfDay = API.GetBillingInfoForVehicalAndDay("ABC123", StringToDate("2020-12-02 00:00:00"));
            Assert.AreEqual(22, costOfDay);
        }

        [Test]
        /* req 4. no Charge in July, weekends or holydays */
        public void Test_Requirment4_NoChargePeriods()
        {
            API.AddPassing("ABC123", StringToDate("2020-12-24 06:22:00"));  //0 kr
            API.AddPassing("ABC123", StringToDate("2020-01-01 06:22:00"));  //0 kr
            API.AddPassing("ABC123", StringToDate("2020-07-02 06:30:00"));  //0 kr
            API.AddPassing("ABC123", StringToDate("2020-12-05 06:48:10"));  //0 kr
            API.AddPassing("ABC123", StringToDate("2020-12-06 07:15:15"));  //0 kr: Total 22 kr

            int costOfDay = API.GetBillingInfoForVehicalAndDay("ABC123", StringToDate("2020-12-24 00:00:00"));
            Assert.AreEqual(0, costOfDay);
            costOfDay = API.GetBillingInfoForVehicalAndDay("ABC123", StringToDate("2020-01-01 00:00:00"));
            Assert.AreEqual(0, costOfDay);
            costOfDay = API.GetBillingInfoForVehicalAndDay("ABC123", StringToDate("2020-07-02 00:00:00"));
            Assert.AreEqual(0, costOfDay);
            costOfDay = API.GetBillingInfoForVehicalAndDay("ABC123", StringToDate("2020-12-05 00:00:00"));
            Assert.AreEqual(0, costOfDay);
            costOfDay = API.GetBillingInfoForVehicalAndDay("ABC123", StringToDate("2020-12-06 00:00:00"));
            Assert.AreEqual(0, costOfDay);
        }

        [Test]
        /* req 5. Diffrentiated Cost during day */
        public void Test_Requirment5_Differentiatedcosts()
        {
            API.AddPassing("ABC123", StringToDate("2020-11-02 05:01:00"));  //0 kr
            int costOfDay = API.GetBillingInfoForVehicalAndDay("ABC123", StringToDate("2020-11-02 00:00:00"));
            Assert.AreEqual(0, costOfDay);

            API.AddPassing("ABC123", StringToDate("2020-11-03 06:22:00"));  //9 kr
            costOfDay = API.GetBillingInfoForVehicalAndDay("ABC123", StringToDate("2020-11-03 00:00:00"));
            Assert.AreEqual(9, costOfDay);
            
            API.AddPassing("ABC123", StringToDate("2020-11-04 06:50:10"));  //16 kr
            costOfDay = API.GetBillingInfoForVehicalAndDay("ABC123", StringToDate("2020-11-04 00:00:00"));
            Assert.AreEqual(16, costOfDay);

            API.AddPassing("ABC123", StringToDate("2020-11-05 07:15:10"));  //22 kr: 
            costOfDay = API.GetBillingInfoForVehicalAndDay("ABC123", StringToDate("2020-11-05 00:00:00"));
            Assert.AreEqual(22, costOfDay);

            API.AddPassing("ABC123", StringToDate("2020-11-09 08:01:00"));  //16 kr
            costOfDay = API.GetBillingInfoForVehicalAndDay("ABC123", StringToDate("2020-11-09 00:00:00"));
            Assert.AreEqual(16, costOfDay);

            API.AddPassing("ABC123", StringToDate("2020-11-10 09:22:00"));  //9 kr
            costOfDay = API.GetBillingInfoForVehicalAndDay("ABC123", StringToDate("2020-11-10 00:00:00"));
            Assert.AreEqual(9, costOfDay);

            API.AddPassing("ABC123", StringToDate("2020-11-11 15:20:10"));  //16 kr
            costOfDay = API.GetBillingInfoForVehicalAndDay("ABC123", StringToDate("2020-11-11 00:00:00"));
            Assert.AreEqual(16, costOfDay);

            API.AddPassing("ABC123", StringToDate("2020-11-12 15:50:10"));  //22 kr: 
            costOfDay = API.GetBillingInfoForVehicalAndDay("ABC123", StringToDate("2020-11-12 00:00:00"));
            Assert.AreEqual(22, costOfDay);

            
            API.AddPassing("ABC123", StringToDate("2020-11-16 17:20:10"));  //16 kr: 
            costOfDay = API.GetBillingInfoForVehicalAndDay("ABC123", StringToDate("2020-11-16 00:00:00"));
            Assert.AreEqual(16, costOfDay);

            API.AddPassing("ABC123", StringToDate("2020-11-17 18:20:10"));  //9 kr: 
            costOfDay = API.GetBillingInfoForVehicalAndDay("ABC123", StringToDate("2020-11-17 00:00:00"));
            Assert.AreEqual(9, costOfDay);

            API.AddPassing("ABC123", StringToDate("2020-11-18 23:20:10"));  //0 kr: 
            costOfDay = API.GetBillingInfoForVehicalAndDay("ABC123", StringToDate("2020-11-18 00:00:00"));
            Assert.AreEqual(0, costOfDay);

        }


        private DateTime StringToDate(string s)
        {
            DateTime myDate = DateTime.ParseExact(s, "yyyy-MM-dd HH:mm:ss",
                                       System.Globalization.CultureInfo.InvariantCulture);

            return myDate; 
        }
    }
}