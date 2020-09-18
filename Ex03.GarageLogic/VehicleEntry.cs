using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class VehicleEntry
    {
        private readonly Vehicle r_VehicleProperties;
        private string m_OwnerName;
        private string m_OwnerPhone;
        private eCarState m_CarState;

        public Vehicle VehicleProperties
        {
            get
            {
                return r_VehicleProperties;
            }
        }

        public string OwnerName
        {
            get
            {
                return m_OwnerName;
            }
        }
        
        public string OwnerPhone
        {
            get
            {
                return m_OwnerPhone;
            }

            set
            {
                m_OwnerPhone = value;
            }
        }

        public eCarState CarState
        {
            get
            {
                return m_CarState;
            }

            set
            {
                m_CarState = value;
            }
        }

        public Dictionary<string, string> GetFieldsToFill()
        {
            Dictionary<string, string> fieldsToFill = new Dictionary<string, string>();
            fieldsToFill.Add("OwnerName", string.Format("Please Enter the owner's name "));
            fieldsToFill.Add("OwnerPhone", string.Format("Please Enter the owner's phone "));
            return fieldsToFill;
        }

        public void ParseUserAnswers(Dictionary<string, string> i_UserAnswers)
        {
            m_OwnerName = i_UserAnswers["OwnerName"];
            if (i_UserAnswers["OwnerPhone"].All(char.IsDigit) == false)
            {
                throw new FormatException("Phone number should consist only from digits");
            }

            if (i_UserAnswers["OwnerPhone"].Length != 10)
            {
                throw new FormatException("Phone number should be 10 digits");
            }

            m_OwnerPhone = i_UserAnswers["OwnerPhone"];
        }

        public VehicleEntry(Vehicle i_VehicleProperties)
        {
            this.r_VehicleProperties = i_VehicleProperties;
            this.m_CarState = eCarState.InProgress;
        }

        public virtual StringBuilder Print()
        {
            StringBuilder output = new StringBuilder();
            output.Append(VehicleProperties.Print());
            output.AppendLine("Owner Name: " + this.OwnerName);
            output.AppendLine("Car status: " + this.CarState.ToString());
            return output;
        }
    }
}
