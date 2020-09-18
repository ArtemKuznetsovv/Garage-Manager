using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class VehicleAllocator
    {
        public static List<string> GetCurrentVehicleTypes()
        {
            Array eVehicleArray = Enum.GetValues(typeof(eVehicleType));
            List<string> m_VehicleTypes = new List<string>(eVehicleArray.Length);
            int numberInList = 1;
            foreach (eVehicleType vehicleType in eVehicleArray)
            {
                m_VehicleTypes.Add(string.Format("{0}) {1}", numberInList, vehicleType.ToString()));
                numberInList++;
            }

            return m_VehicleTypes;
        }

        public static Vehicle CreateVehicle(eVehicleType i_VehicleType, string i_LicensePlate)
        {
            Vehicle newVehicle = null;

            switch (i_VehicleType)
            {
                case eVehicleType.FuelCar:
                    newVehicle = new FuelCar(i_LicensePlate);
                    break;

                case eVehicleType.ElectricCar:
                    newVehicle = new ElectricCar(i_LicensePlate);
                    break;

                case eVehicleType.FuelMotorcycle:
                    newVehicle = new FuelMotorcycle(i_LicensePlate);
                    break;

                case eVehicleType.ElectricMotorcycle:
                    newVehicle = new ElectricMotorcycle(i_LicensePlate);
                    break;

                case eVehicleType.Truck:
                    newVehicle = new Truck(i_LicensePlate);
                    break;
            }

            return newVehicle;
        }
    }
}
