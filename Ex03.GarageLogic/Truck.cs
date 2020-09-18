using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class Truck : Vehicle
    {
        private const int k_NumOfWheels = 16;
        private const float k_MaxWheelAirPressure = 30;
        private const eFuelTypes k_FuelType = eFuelTypes.Soler;
        private const float k_MaxFuelAmount = 105f;
        private bool m_IsDangerousMaterials;
        private float m_LoadCapacity;

        public Truck(string i_LicensePlate)
            : base(i_LicensePlate, k_NumOfWheels)
        {
            this.m_EnergySourceType = new FuelEnergy(k_MaxFuelAmount, k_FuelType);
        }

        public override Dictionary<string, string> GetRemainingVehicleInfo()
        {
            Dictionary<string, string> fieldsToFill = base.GetRemainingVehicleInfo();
            fieldsToFill.Add("DangerousMaterials", 
                string.Format(
@"Does truck have dangerous materials ? 
1)yes
2)no"));
            fieldsToFill.Add("LoadCapacity", string.Format("Please enter an integer representing the trucks load capacity"));
            this.GetWheelsInfo(k_MaxWheelAirPressure, fieldsToFill);

            return fieldsToFill;
        }

        public override void ParseUserAnswers(Dictionary<string, string> i_UserAnswers)
        {
            int isDangerous;
            float capacity;
            base.ParseUserAnswers(i_UserAnswers);
            this.ParseWheelsInfo(i_UserAnswers, k_MaxWheelAirPressure);
            if (int.TryParse(i_UserAnswers["DangerousMaterials"], out isDangerous) == false)
            {
                throw new FormatException("Dangerous materials answer should consist only from one digit");
            }

            if (float.TryParse(i_UserAnswers["LoadCapacity"], out capacity) == false)
            {
                throw new FormatException("Load Capacity of the truck should be a float number");
            }

            this.CheckIfValueInRange(1, 2, isDangerous, "Dangerous Materials");

            this.m_IsDangerousMaterials = isDangerous != 2;
            this.m_LoadCapacity = capacity;
        }

        public bool IsDangerousMaterials
        {
            get
            {
                return this.m_IsDangerousMaterials;
            }

            set
            {
                this.m_IsDangerousMaterials = value;
            }
        }

        public float LoadCapacity
        {
            get
            {
                return this.m_LoadCapacity;
            }

            set
            {
                this.m_LoadCapacity = value;
            }
        }

        public override StringBuilder Print()
        {
            StringBuilder output = new StringBuilder();
            output.Append(base.Print());
            output.AppendLine("Vehicle Type: " + this.GetType().Name);
            output.AppendLine("Is the truck transfering dangerous materials: " + this.m_IsDangerousMaterials);
            output.AppendLine("The truck's load capacity: " + this.m_LoadCapacity);
            return output;
        }
    }
}
