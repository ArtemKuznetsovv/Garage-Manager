using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public abstract class Car : Vehicle
    {
        private const float k_MaxWheelAirPressure = 32;
        private const int k_NumOfWheels = 4;
        private eCarColor m_CarColor;
        private eNumOfDoors m_NumOfDoors;

        public Car(string i_LicensePlate) : base(i_LicensePlate, k_NumOfWheels) 
        {
        }

        public eCarColor CarColor
        {
            get
            {
                return this.m_CarColor;
            }
        }

        public eNumOfDoors NumOfDoors
        {
            get
            {
                return this.m_NumOfDoors;
            }
        }

        public override Dictionary<string, string> GetRemainingVehicleInfo()
        {
            Dictionary<string, string> fieldsToFill = base.GetRemainingVehicleInfo();
            StringBuilder carColorMsg = new StringBuilder();
            carColorMsg.AppendLine("Please enter your car's color ");
            carColorMsg.Append(Vehicle.EnumToMessage<eCarColor>());

            fieldsToFill.Add("ColorType", carColorMsg.ToString());
            fieldsToFill.Add("NumOfDoors", string.Format("Please enter number of doors, from {0} to {1}", (int)eNumOfDoors.TwoDoors, (int)eNumOfDoors.FiveDoors));
            this.GetWheelsInfo(k_MaxWheelAirPressure, fieldsToFill);
            return fieldsToFill;
        }

        public override void ParseUserAnswers(Dictionary<string, string> i_UserAnswers)
        {
            eNumOfDoors doorSelection;
            eCarColor colorSelection;
            base.ParseUserAnswers(i_UserAnswers);
            int numOfDoors = Enum.GetValues(typeof(eNumOfDoors)).Cast<int>().Max();
            int numOfColors = Enum.GetValues(typeof(eCarColor)).Length;
            this.ParseWheelsInfo(i_UserAnswers, k_MaxWheelAirPressure);
            if (Enum.TryParse(i_UserAnswers["NumOfDoors"].ToString(), out doorSelection) == false)
            {
                throw new FormatException("Door Selection should consist only of one digit");
            }

            this.CheckIfValueInRange((int)eNumOfDoors.TwoDoors, numOfDoors, (int)doorSelection, "Number Of Doors");
            if (Enum.TryParse(i_UserAnswers["ColorType"].ToString(), out colorSelection) == false)
            {
                throw new FormatException("Color selection should consist of one digit");
            }

            this.CheckIfValueInRange(1, numOfColors, (int)colorSelection, "Color Selection");
            this.m_CarColor = colorSelection;
            this.m_NumOfDoors = doorSelection;
        }

        public override StringBuilder Print()
        {
            StringBuilder output = new StringBuilder();
            output.Append(base.Print());
            output.AppendLine("Vehicle Type: " + this.GetType().Name);
            output.AppendLine("The car's color: " + this.m_CarColor.ToString());
            output.AppendLine("Number of doors in car: " + this.m_NumOfDoors); // which is equal to Append(Environment.NewLine);
            return output;
        }
    }
}
