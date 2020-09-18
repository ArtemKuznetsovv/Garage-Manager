using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{

    public class FuelCar : Car
    {
        private const float k_MaxFuelAmount = 50;
        private const eFuelTypes k_FuelType = eFuelTypes.Octan96;

        public FuelCar(string i_LicensePlate)
            : base(i_LicensePlate)
        {
            this.m_EnergySourceType = new FuelEnergy(k_MaxFuelAmount, k_FuelType);
        }
    }
}
