using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class ElectricEnergy : EnergySource
    {
        public ElectricEnergy(float i_MaxElectricEnergy, float i_CurrentElectricEnergy)
            : base(i_MaxElectricEnergy, i_CurrentElectricEnergy)
        {
        }

        public ElectricEnergy(float i_MaxEnergyCapacity) : base(i_MaxEnergyCapacity)
        {
        }

        public override string GetCurrentEnergyMessage()
        {
            return string.Format(
                @"Enter the amount of hours left in the battery,
Value should be between {0} and {1}",
                0,
                this.r_MaxEnergyCapacity);
        }

        public void Charge(float i_HoursToAdd)
        {
            float currMaxEnergy = r_MaxEnergyCapacity - m_CurrentEnergy;
            if(i_HoursToAdd > currMaxEnergy)
            {
                string errorMsg = string.Format(
                    "You entered more then the max energy(In Minutes). Enter number of Minutes between {0} and {1}",
                    0f,
                    currMaxEnergy * 60);
                throw new ValueOutOfRangeException(errorMsg, 0f, currMaxEnergy);
            }

            this.m_CurrentEnergy += i_HoursToAdd;
        }

        public override StringBuilder Print()
        {
            StringBuilder output = new StringBuilder();
            output.Append(base.Print());
            output.AppendLine("Energy Source Type: Electric");
            return output;
        }
    }
}
