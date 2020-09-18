using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{  
    public class ElectricCar : Car
    {
        private const float k_MaxBatteryLife = 4.8f;

        public ElectricCar(string i_LicensePlate) : base(i_LicensePlate)
        {
            this.m_EnergySourceType = new ElectricEnergy(k_MaxBatteryLife);
        }
    }
}
