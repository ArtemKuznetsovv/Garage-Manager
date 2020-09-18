using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class ElectricMotorcycle : Motorcycle
    {
        private const float k_BatteryLife = 1.6f;

        public ElectricMotorcycle(string i_LicensePlate)
            : base(i_LicensePlate)
        {
            this.m_EnergySourceType = new ElectricEnergy(k_BatteryLife);
        }
    }
}
