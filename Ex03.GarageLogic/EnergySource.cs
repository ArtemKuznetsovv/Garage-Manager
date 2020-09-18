using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public abstract class EnergySource
    {
        protected readonly float r_MaxEnergyCapacity; // fuel or electric
        protected float m_CurrentEnergy;

        public float CurrentEnergyPercentage
        {
            get
            {
                return m_CurrentEnergy / r_MaxEnergyCapacity * 100;
            }
        }

        public EnergySource(float i_MaxEnergyCapacity, float i_CurrentEnergy)
        {
            r_MaxEnergyCapacity = i_MaxEnergyCapacity;
            m_CurrentEnergy = i_CurrentEnergy;
        }

        public EnergySource(float i_MaxEnergyCapacity)
        {
            r_MaxEnergyCapacity = i_MaxEnergyCapacity;
        }

        public abstract string GetCurrentEnergyMessage();

        public float MaxEnergyCapacity
        {
            get
            {
                return r_MaxEnergyCapacity;
            }
        }

        public float CurrentEnergy
        {
            get
            {
                return m_CurrentEnergy;
            }

            set
            {
                if (value > r_MaxEnergyCapacity)
                {
                    float currMax = r_MaxEnergyCapacity - m_CurrentEnergy;
                    throw new ValueOutOfRangeException(string.Format("You entered more then the max energy . Enter amount between {0} and {1} ", 0f, currMax)
                        , 0f, currMax);  
                }

                m_CurrentEnergy = value;
            }
        }

        public virtual StringBuilder Print()
        {
            StringBuilder output = new StringBuilder();
            output.AppendLine("Energy Left: " + this.CurrentEnergyPercentage + "%");
            return output;
        }

        public void AddEnergy(float i_EnergyToAdd, eFuelTypes i_FuelType)
        {
            if (this is ElectricEnergy electricEnergy)
            {
                electricEnergy.Charge(i_EnergyToAdd);
            }
            else if (this is FuelEnergy fuelEnergy)
            {
                fuelEnergy.FillFuel(i_FuelType, i_EnergyToAdd);
            }
            else
            {
                throw new ArgumentException("no engine found");
            }
        }
    }
}
