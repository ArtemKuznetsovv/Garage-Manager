using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class Garage
    {
        private readonly Dictionary<string, VehicleEntry> r_ListedVehicles = new Dictionary<string, VehicleEntry>();

        public bool FindEntryByPlateNum(string i_FindPlateNumber, out VehicleEntry o_VehicleEntry)
        {
            return this.r_ListedVehicles.TryGetValue(i_FindPlateNumber, out o_VehicleEntry);
        }

        /// <summary>
        /// throws an argumentException if vehicle not found
        /// </summary>
        /// <param name="i_PlateNumber"></param>
        /// <param name="i_CarState"></param>
        public void ChangeCarStatus(string i_PlateNumber, eCarState i_CarState)
        {
            VehicleEntry vehicleEntry;
            bool isVehicleFound = FindEntryByPlateNum(i_PlateNumber, out vehicleEntry);
            if (!isVehicleFound)
            {
                throw new ArgumentException("Vehicle Was Not Found");
            }

            vehicleEntry.CarState = i_CarState;
        }

        /// <summary>
        /// throws an argumentException if vehicle not found
        /// </summary>
        /// <param name="i_PlateNumber"></param>
        public void InflateWheelsPressureToMaximum(string i_PlateNumber)
        {
            VehicleEntry vehicleEntry;
            bool isVehicleFound = FindEntryByPlateNum(i_PlateNumber, out vehicleEntry);
            if (!isVehicleFound)
            {
                throw new ArgumentException("Vehicle Was Not Found");
            }

            vehicleEntry.VehicleProperties.InflateWheelsToMax();
        }

        /// <summary>
        /// throws an argumentException if vehicle not found
        /// </summary>
        /// <param name="i_PlateNumber"></param>
        /// <param name="i_AmountToFill"></param>
        /// <param name="i_FuelType"></param>
        /// <param name="i_EnergySourceCondition"></param>
        public void FillEnergy(string i_PlateNumber, float i_AmountToFill, eEnergySource i_EnergySourceCondition, eFuelTypes i_FuelType)
        {
            VehicleEntry vehicleEntry;
            bool isVehicleFound = FindEntryByPlateNum(i_PlateNumber, out vehicleEntry);
            if (!isVehicleFound)
            {
                throw new ArgumentException("Vehicle Was Not Found");
            }

            if (i_EnergySourceCondition != vehicleEntry.VehicleProperties.EnergySourceType)
            {
                throw new ArgumentException("Vehicle does not have the correct energy type for this action");
            }

            vehicleEntry.VehicleProperties.AddEnergy(i_AmountToFill, i_FuelType);
        }

        /// <summary>
        /// throws an argumentException if vehicle not found
        /// </summary>
        /// <param name="i_PlateNumber"></param>
        /// <returns></returns>
        public StringBuilder GetVehicleInfo(string i_PlateNumber)
        {
            StringBuilder output;
            VehicleEntry vehicleEntry;
            bool isVehicleFound = FindEntryByPlateNum(i_PlateNumber, out vehicleEntry);
            if (!isVehicleFound)
            {
                throw new System.ArgumentException("Vehicle Was Not Found");
            }

            output = vehicleEntry.Print();
            return output;
        }

        public StringBuilder GetAllEntriesPlateNumberByState(eCarState i_CarState)
        {
            StringBuilder output = new StringBuilder();
            foreach (var entry in r_ListedVehicles)
            {
                if (entry.Value.CarState == i_CarState || i_CarState == eCarState.All)
                {
                    output.AppendLine(entry.Key);
                }
            }

            if (output.Length == 0)
            {
                throw new ArgumentException("No Vehicle In This State");
            }

            return output;
        }

        public void AddVehicleEntry(VehicleEntry i_VehicleToAdd)
        {
            this.r_ListedVehicles.Add(i_VehicleToAdd.VehicleProperties.LicensePlate, i_VehicleToAdd);
        }
    }
}
