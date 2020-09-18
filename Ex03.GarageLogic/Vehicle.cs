using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Ex03.GarageLogic
{
    public abstract class Vehicle
    {
        protected EnergySource m_EnergySourceType;
        private readonly string r_LicensePlate;
        private readonly List<Wheel> r_Wheels;
        private string m_Model;

        public Vehicle(string i_LicensePlate, int i_NumberOfWheels)
        {
            r_Wheels = new List<Wheel>(i_NumberOfWheels);
            r_LicensePlate = i_LicensePlate;
        }

        public string LicensePlate
        {
            get
            {
                return r_LicensePlate;
            }
        }

        public eEnergySource EnergySourceType
        {
            get
            {
                if (m_EnergySourceType is FuelEnergy)
                {
                    return eEnergySource.Fuel;
                }
                else
                {
                    return eEnergySource.Electric;
                }
            }
        }

        public string Model
        {
            get
            {
                return m_Model;
            }

            set
            {
                m_Model = value;
            }
        }

        public static StringBuilder EnumToMessage<T>()
        {
            Type t = typeof(T);
            if (!t.IsEnum)
            {
                throw new InvalidOperationException("Type is not Enum");
            }

            StringBuilder eNumMessage = new StringBuilder();
            int numberOfItems = 1;
            foreach (var value in Enum.GetValues(t))
            {
                eNumMessage.AppendLine(string.Format("{0}) {1}", numberOfItems, value.ToString()));
                numberOfItems++;
            }

            return eNumMessage;
        }

        public void AddEnergy(float i_EnergyToAdd, eFuelTypes i_FuelType)
        { 
            m_EnergySourceType.AddEnergy(i_EnergyToAdd, i_FuelType);
        }

        public void InflateWheelsToMax()
        {
            foreach (Wheel wheel in r_Wheels)
            {
                wheel.IncreasePressure(wheel.MaxAirPressure - wheel.CurrentAirPressure);
            }
        }

        protected void CheckIfValueInRange(int i_MinValue, int i_MaxValue, int i_Value, string i_ValueType)
        {
            if (i_Value < i_MinValue || i_Value > i_MaxValue)
            {
                string errorMsg = string.Format(
                    @"You exceeded the amount for {0} . Enter Amount between {1} and {2} ",
                    i_ValueType,
                    i_MinValue,
                    i_MaxValue);
                throw new ValueOutOfRangeException(errorMsg, i_MinValue, i_MaxValue);
            }
        }

        public virtual Dictionary<string, string> GetRemainingVehicleInfo()
        {
            Dictionary<string, string> fieldsToFill = new Dictionary<string, string>();
            fieldsToFill.Add("CarModel", string.Format("Please enter your vehicle's model"));
            fieldsToFill.Add("CurrentEnergy", m_EnergySourceType.GetCurrentEnergyMessage());
            return fieldsToFill;
        }

        public virtual void ParseUserAnswers(Dictionary<string, string> i_UserAnswers)
        {
            m_Model = i_UserAnswers["CarModel"];
            if (float.TryParse(i_UserAnswers["CurrentEnergy"], out float currentEnergy) == false)
            {
                throw new FormatException("Current Energy should be a float number");
            }

            m_EnergySourceType.CurrentEnergy = currentEnergy;
        }

        protected void GetWheelsInfo(float i_MaxWheelAirPressure, Dictionary<string, string> i_FieldsToFill)
        {
            i_FieldsToFill.Add(
                "AirPressure",
                string.Format("Please Enter current air pressure in this wheel, max is : {0}", i_MaxWheelAirPressure));
            i_FieldsToFill.Add("Manufacturer", "Please enter this wheel's Manufacturer");
        }

        protected void ParseWheelsInfo(Dictionary<string, string> i_UserAnswers, float i_MaxWheelAirPressure)
        {
            if (float.TryParse(i_UserAnswers["AirPressure"], out float airPressure) == false)
            {
                throw new FormatException("Wheel Air Pressure should be a float number");
            }

            for (int i = 0; i < r_Wheels.Capacity; i++)
            {
                r_Wheels.Add(new Wheel(i_UserAnswers["Manufacturer"], airPressure, i_MaxWheelAirPressure));
            }
        }
         
        public virtual StringBuilder Print()
        {
            StringBuilder output = new StringBuilder();
            output.AppendLine("License plate: " + this.LicensePlate);
            output.AppendLine("Modedl Name: " + this.Model);
            output.AppendLine("Number of wheels in car: " + this.r_Wheels.Count());
            output.Append(this.r_Wheels[0].Print());
            output.Append(this.m_EnergySourceType.Print());
            return output;
        }

        protected class Wheel
        {
            private readonly float r_MaxAirPressure;
            private string m_Manufacturer;
            private float m_CurrentAirPressure;

            public Wheel(string i_Manufacturer, float i_CurrentAirPressure, float i_MaxAirPressure)
            {
                m_Manufacturer = i_Manufacturer;
                m_CurrentAirPressure = i_CurrentAirPressure;
                r_MaxAirPressure = i_MaxAirPressure;
            }

            public string Manufacturer
            {
                get
                {
                    return m_Manufacturer;
                }

                set
                {
                    m_Manufacturer = value;
                }
            }

            public float CurrentAirPressure
            {
                get
                {
                    return m_CurrentAirPressure;
                }

                set
                {
                    if (value > r_MaxAirPressure)
                    {
                        float currMax = r_MaxAirPressure - m_CurrentAirPressure;
                        throw new ValueOutOfRangeException(string.Format(@"You exceeded the amount of Air Pressure . Enter Amount between {0} and {1} ", 0f, currMax), 0, currMax);
                    }

                    m_CurrentAirPressure = value;
                }
            }

            public float MaxAirPressure
            {
                get
                {
                    return r_MaxAirPressure;
                }
            }

            public void IncreasePressure(float i_Pressure)
            {
                this.m_CurrentAirPressure += i_Pressure;
            }

            public virtual StringBuilder Print()
            {
                StringBuilder output = new StringBuilder();
                output.AppendLine("Wheels Manufacturer: " + this.Manufacturer);
                output.AppendLine("Current Air Pressure: " + this.m_CurrentAirPressure);
                output.AppendLine("Maximum Air Pressure: " + this.MaxAirPressure);
                return output;
            }
        }
    }
}
