using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    public class UserInterface
    {
        private const string k_ElectricChargeUnits = "Minutes";
        private const string k_FuelUnits = "Liter";
        private const int k_MinimumPlateLength = 5;
        private const int k_MaximumPlateLength = 8;
        private const bool v_EnableVehicleStateAll = true;
        private const bool v_IsProgramOver = false;
        private const int k_NumOfActions = 8;
        private static readonly string[] sr_ActionText = 
            { 
            "Insert a new car to the garage",
            "Print the plate number of every vehicle in the garage",
            "Change a specific vehicle's status",
            "Pump air in the wheels of a vehicle",
            "Fuel a vehicle (vehicle must be fuel - powered)",
            "Charge a vehicle (vehicle must be an electric vehicle",
            "Print specific car's details",
            "Exit system" 
        };

        private Action[] m_ActionMethods;

        private Garage m_Garage;
      
        public void StartUI() 
        {
            m_ActionMethods = new Action[] 
            { 
             () => insertNewVehicle(),
             () => printAllPlateNumbers(),
             () => changeVehicleStatus(),
             () => pumpWheelsAir(),
             () => fillVehicleEnergy(eEnergySource.Fuel),
             () => fillVehicleEnergy(eEnergySource.Electric),
             () => printVehicleDetails()
            };

            bool isInProgram = true;
            m_Garage = new Garage();
            while (isInProgram)
            {
                getNextAction(out isInProgram);
            }
        }

        private void getNextAction(out bool o_IsInProgram)
        {
            printMenu();
            bool isInputValid = false;
            eActionType actionType = eActionType.NotValid;
            while (!isInputValid)
            {
                string inputAction = Console.ReadLine();
                isInputValid = checkIfActionInputIsValid(inputAction, out actionType);
                if (!isInputValid)
                {
                    Console.WriteLine("Invalid Input. please insert a number between 1 and 8");
                }
            }

            doAction(actionType, out o_IsInProgram);
        }

        private void printMenu()
        {
            Console.WriteLine("What would you like to do?");
            for (int i = 0; i < k_NumOfActions; i++)
            {
                Console.WriteLine("{0}: {1}", i + 1, sr_ActionText[i]);
            }

            Console.WriteLine("Please enter the fitting number to the action of your choice.");
        }

        private bool checkIfActionInputIsValid(string i_InputAction, out eActionType o_ActionType)
        {
            bool isInputValid = false;
            try
            {
                int inputAsInt = int.Parse(i_InputAction);
                isInputValid = inputAsInt > 0 && inputAsInt <= 8 && i_InputAction.Length == 1;
                if (isInputValid)
                {
                    o_ActionType = (eActionType)(inputAsInt - 1);
                }
                else
                {
                    o_ActionType = eActionType.NotValid;
                }
            }
            catch (FormatException)
            {
                o_ActionType = eActionType.NotValid;
            }

            return isInputValid;
        }

        private void doAction(eActionType i_ActionType, out bool o_IsInProgram)
        {
            o_IsInProgram = !v_IsProgramOver;
            if (i_ActionType == eActionType.Exit)
            {
                o_IsInProgram = v_IsProgramOver;
            }
            else
            {
                m_ActionMethods[(int)i_ActionType]();
            }
        }

        private void insertNewVehicle()
        {
            bool isInputValid = false;
            string vehiclePlate = getInputVehiclePlate();
            VehicleEntry vehicleEntryToAdd;
            if (m_Garage.FindEntryByPlateNum(vehiclePlate, out vehicleEntryToAdd) == true)
            { 
                Console.WriteLine(@"This plate number is already registered in the garage
Vehicle status is : In Progress");
                m_Garage.ChangeCarStatus(vehicleEntryToAdd.VehicleProperties.LicensePlate, eCarState.InProgress);
            }
            else
            {
                while (!isInputValid)
                {
                    try
                    {
                        vehicleEntryToAdd =
                            new VehicleEntry(VehicleAllocator.CreateVehicle(getUserVehicle(), vehiclePlate));
                        vehicleEntryToAdd.ParseUserAnswers(getInfoFromUser(vehicleEntryToAdd.GetFieldsToFill()));
                        Dictionary<string, string> questionsForUser =
                            vehicleEntryToAdd.VehicleProperties.GetRemainingVehicleInfo();
                        vehicleEntryToAdd.VehicleProperties.ParseUserAnswers(getInfoFromUser(questionsForUser));
                        m_Garage.AddVehicleEntry(vehicleEntryToAdd);
                        isInputValid = true;
                        Console.WriteLine("Vehicle was added successfully, current status: In Progress");
                    }
                    catch (ValueOutOfRangeException outOfRange)
                    {
                        Console.WriteLine(outOfRange.Message);
                    }
                    catch (FormatException outOfRange)
                    {
                        Console.WriteLine(outOfRange.Message);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
        }

        private Dictionary<string, string> getInfoFromUser(Dictionary<string, string> i_FieldsToFill)
        {
            Dictionary<string, string> userInfo = new Dictionary<string, string>();
            string userInput;
            foreach (KeyValuePair<string, string> keyValuePair in i_FieldsToFill)
            {
                Console.WriteLine(keyValuePair.Value);
                userInput = Console.ReadLine();
                while(userInput.Length == 0)
                {
                    Console.WriteLine("Text was empty , Try again");
                    userInput = Console.ReadLine();
                }

                userInfo[keyValuePair.Key] = userInput;
            }

            return userInfo;
        }

        private void printVehicleTypes()
        {
            Console.WriteLine(@"Please enter the number representing your vehicle type :");
            List<string> vehicleTypes = VehicleAllocator.GetCurrentVehicleTypes();
            foreach (string currentVehicleType in vehicleTypes)
            {
                Console.WriteLine(currentVehicleType);
            }
        }

        private eVehicleType getUserVehicle()
        {
            bool isInputValid = false;
            int selection = 1;
            List<string> vehicleTypes = VehicleAllocator.GetCurrentVehicleTypes();
            while (!isInputValid)
            {
                    printVehicleTypes();
                    string userSelection = Console.ReadLine();
                    int.TryParse(userSelection, out selection);
                    if (selection >= 1 && selection <= vehicleTypes.Count)
                    {
                        isInputValid = true;
                    }
                    else
                    {
                        Console.WriteLine("Invalid Input. please insert a valid number");
                    }
            }

            return (eVehicleType)selection;
        }

        private void printVehicleDetails()
        {
            string vehiclePlate = getInputVehiclePlate();
            try
            {
                Console.WriteLine(m_Garage.GetVehicleInfo(vehiclePlate));
            }
            catch (ArgumentException ae)
            {
                Console.WriteLine(ae.Message);
            }
        }

        private string getInputVehiclePlate()
        {
            Console.WriteLine("Please insert the vehicle's plate number. a valid plate number is a 5-8 long word containing digits and letters only");
            bool isInputValid = false;
            string inputPlateNumber = null;
            while (!isInputValid)
            {
                inputPlateNumber = Console.ReadLine();
                isInputValid = checkIfPlateIsValid(inputPlateNumber);
                if (!isInputValid)
                {
                    Console.WriteLine("Invalid Input. please insert a 5-8 length string containing digits and letters only");
                }
            }

            return inputPlateNumber;
        }

        private bool checkIfPlateIsValid(string i_InputPlateNumber)
        {
            return (i_InputPlateNumber.Length >= k_MinimumPlateLength) &&
                   (i_InputPlateNumber.Length <= k_MaximumPlateLength) &&
                   i_InputPlateNumber.All(char.IsLetterOrDigit);
        }

        private void fillVehicleEnergy(eEnergySource i_EnergySourceType)
        {
            const int k_Minutes = 60;
            string vehiclePlate = getInputVehiclePlate();
            float energyToFill;
            eFuelTypes fuelType = eFuelTypes.Blank;
            if (i_EnergySourceType == eEnergySource.Electric)
            {
                energyToFill = getInputFillEnergyUnit(k_ElectricChargeUnits);
                energyToFill /= k_Minutes;
            }
            else
            {
                energyToFill = getInputFillEnergyUnit(k_FuelUnits);
                fuelType = getInputFuelType();
            }

            try
            {
                m_Garage.FillEnergy(vehiclePlate, energyToFill, i_EnergySourceType, fuelType);
                Console.WriteLine("Vehicle was fueled successfully");
            }
            catch (ArgumentException ae)
            {
                Console.WriteLine(ae.Message);
            }
            catch (ValueOutOfRangeException ve)
            {
                Console.WriteLine(ve.Message);
            }
        }

        private float getInputFillEnergyUnit(string i_Units)
        {
            Console.WriteLine("Please insert the amount of {0} you whould like to fill", i_Units);
            bool isInputValid = false;
            float inputAmount = 0;
            while (!isInputValid)
            {
                try
                {
                    inputAmount = float.Parse(Console.ReadLine());
                    isInputValid = inputAmount > 0;
                    if (!isInputValid)
                    {
                        Console.WriteLine("Invalid Input. please insert a positive number");
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid Input. please insert a number");
                }
            }

            return inputAmount;
        }

        private eFuelTypes getInputFuelType()
        {
            Console.WriteLine("Please choose the type of fuel you want to fill");
            foreach (int i in Enum.GetValues(typeof(eFuelTypes)))
            {
                string FuelName = Enum.GetNames(typeof(eFuelTypes))[i];
                if (FuelName != eFuelTypes.Blank.ToString())
                {
                    Console.WriteLine($"{i + 1} - {Enum.GetNames(typeof(eFuelTypes))[i]}");
                }
            }

            bool isInputValid = false;
            int inputFuelIndex = 0;
            while (!isInputValid)
            {
                try
                {
                    inputFuelIndex = int.Parse(Console.ReadLine()) - 1;
                    isInputValid = isEnumValueValid(inputFuelIndex, Enum.GetNames(typeof(eFuelTypes)).Length - 1);
                    if (!isInputValid)
                    {
                        Console.WriteLine("Invalid Input. please insert a valid number");
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid Input. please insert a number");
                }
            }
            
            return (eFuelTypes)inputFuelIndex;
        }

        private bool isEnumValueValid(int i_InputFuelIndex, int i_MaxValue)
        {
            return i_InputFuelIndex >= 0 && i_InputFuelIndex < i_MaxValue;
        }

        private void pumpWheelsAir()
        {
            string vehiclePlate = getInputVehiclePlate();
            try
            {
                m_Garage.InflateWheelsPressureToMaximum(vehiclePlate);
                Console.WriteLine("Wheel air pressure filled successfully");
            }
            catch (ArgumentException ae)
            {
                Console.WriteLine(ae.Message);
            }
        }

        private void changeVehicleStatus()
        {
            string vehiclePlate = getInputVehiclePlate();
            eCarState newCarState = getInputCarState(!v_EnableVehicleStateAll);
            try
            {
                m_Garage.ChangeCarStatus(vehiclePlate, newCarState);
                Console.WriteLine("Status changed to {0} , successfully ", newCarState);
            }
            catch (ArgumentException ae)
            {
                Console.WriteLine(ae.Message);
            }
        }

        private eCarState getInputCarState(bool i_WithAllOption)
        {
            Console.WriteLine("Please choose the vehicle status");
            foreach (int i in Enum.GetValues(typeof(eCarState)))
            {
                string currentStateValue = Enum.GetNames(typeof(eCarState))[i];
                if (i_WithAllOption || currentStateValue != eCarState.All.ToString())
                {
                    Console.WriteLine($"{i + 1} - {currentStateValue}");
                }
            }

            bool isInputValid = false;
            int inputStatusIndex = 0;
            while (!isInputValid)
            {
                try
                {
                    int enumMaxValue = i_WithAllOption ? Enum.GetNames(typeof(eCarState)).Length : Enum.GetNames(typeof(eCarState)).Length - 1;
                    inputStatusIndex = int.Parse(Console.ReadLine()) - 1;
                    isInputValid = isEnumValueValid(inputStatusIndex, enumMaxValue);
                    if (!isInputValid)
                    {
                        Console.WriteLine("Invalid Input. please insert a valid number");
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid Input. please insert a number");
                }
            }

            return (eCarState)inputStatusIndex;
        }

        private void printAllPlateNumbers()
        {
            eCarState newCarState = getInputCarState(v_EnableVehicleStateAll);
            try
            {
                Console.WriteLine(m_Garage.GetAllEntriesPlateNumberByState(newCarState)); // no exceptions?
            }
            catch (ArgumentException ae)
            {
                Console.WriteLine(ae.Message);
            }
        }
    }
}
