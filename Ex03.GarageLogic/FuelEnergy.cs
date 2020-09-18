using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class FuelEnergy : EnergySource
    {
        private readonly eFuelTypes r_FuelType;

        public FuelEnergy(float i_MaxFuel, float i_CurrentFuel, eFuelTypes i_FuelType)
            : base(i_MaxFuel, i_CurrentFuel)
        {
            this.r_FuelType = i_FuelType;
        }

        public FuelEnergy(float i_MaxEnergyCapacity, eFuelTypes i_FuelType) : base(i_MaxEnergyCapacity)
        {
            this.r_FuelType = i_FuelType;
        }

        public override string GetCurrentEnergyMessage()
        {
            return string.Format(
                @"Enter the amount of Fuel left in the tank,
Value should be between {0} and {1}",
                0,
                this.r_MaxEnergyCapacity);
        }

        public void FillFuel(eFuelTypes i_FuelType, float i_FuelAmount)
        {
            if (i_FuelType != this.r_FuelType)
            {
                throw new ArgumentException("this fuel type is incorrect");
            }

            this.CurrentEnergy += i_FuelAmount;            
        }

        public override StringBuilder Print()
        {
            StringBuilder output = new StringBuilder();
            output.Append(base.Print());
            output.AppendLine("Energy Source Type: Fuel");
            output.AppendLine("Type Of Fuel: " + this.r_FuelType);
            return output;
        }
    }
}
