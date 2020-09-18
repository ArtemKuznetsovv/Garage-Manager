using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public abstract class Motorcycle : Vehicle
    {
        private const int k_NumOfWheels = 2;
        private const float k_MaxWheelAirPressure = 28;
        private int m_EngineCapacity;
        private eLicenseType m_LicenseType;

        public Motorcycle(string i_LicensePlate) : base(i_LicensePlate, k_NumOfWheels)
        {
        }

        public eLicenseType licenseType
        {
            get
            {
                return this.m_LicenseType;
            }

            set
            {
                this.m_LicenseType = value;
            }
        }

        public int EngineCapacity
        {
            get
            {
                return this.m_EngineCapacity;
            }

            set
            {
                this.m_EngineCapacity = value;
            }
        }

        public override Dictionary<string, string> GetRemainingVehicleInfo()
        {
            Dictionary<string, string> fieldsToFill = base.GetRemainingVehicleInfo();
            StringBuilder licenseMessage = new StringBuilder();
            licenseMessage.AppendLine("Please enter your motorcycle License Type from this selection:");
            licenseMessage.Append(Vehicle.EnumToMessage<eLicenseType>());

            fieldsToFill.Add("LicenseType", licenseMessage.ToString());
            fieldsToFill.Add("EngineCapacity", "Please enter your engine capacity");
            this.GetWheelsInfo(k_MaxWheelAirPressure, fieldsToFill);
            return fieldsToFill;
        }

        public override void ParseUserAnswers(Dictionary<string, string> i_UserAnswers)
        {
            int engineCapacity;
            eLicenseType licenseSelection;
            base.ParseUserAnswers(i_UserAnswers);
            this.ParseWheelsInfo(i_UserAnswers, k_MaxWheelAirPressure);
            int numOfLicenseTypes = Enum.GetValues(typeof(eLicenseType)).Length;
            if (Enum.TryParse(i_UserAnswers["LicenseType"], out licenseSelection) == false)
            {
                throw new FormatException("License Type Answer should consist only of one digit");
            }

            this.CheckIfValueInRange(1, numOfLicenseTypes, (int)licenseSelection, "License Selection");
            if (int.TryParse(i_UserAnswers["EngineCapacity"], out engineCapacity) == false)
            {
                throw new FormatException("Engine Capacity should be an integer");
            }

            this.m_LicenseType = licenseSelection;
            this.m_EngineCapacity = engineCapacity;
        }

        public override StringBuilder Print()
        {
            StringBuilder output = new StringBuilder();
            output.Append(base.Print());
            output.AppendLine("Vehicle Type: " + this.GetType().Name);
            output.AppendLine("License Type: " + this.m_LicenseType.ToString());
            output.AppendLine("Engine Capacity: " + this.m_EngineCapacity); // which is equal to Append(Environment.NewLine);
            return output;
        }
    }
}
