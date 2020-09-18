using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class FuelMotorcycle : Motorcycle
    {
        private const float k_MaxFuelAmount = 5.5f;
        private const eFuelTypes k_FuelType = eFuelTypes.Octan95;

        public FuelMotorcycle(string i_LicensePlate)
            : base(i_LicensePlate)
        {
            this.m_EnergySourceType = new FuelEnergy(k_MaxFuelAmount, k_FuelType);
        }       
    }
}
