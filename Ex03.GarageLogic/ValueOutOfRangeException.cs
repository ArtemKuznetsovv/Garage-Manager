using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class ValueOutOfRangeException : Exception
    {
        private readonly float m_MinValue;

        public float MinValue
        {
            get
            {
                return this.m_MinValue;
            }
        }

        private readonly float m_MaxValue;

        public float MaxValue
        {
            get
            {
                return this.m_MaxValue;
            }
        }

        public ValueOutOfRangeException()
        {
        }

        public ValueOutOfRangeException(float i_MinValue, float i_MaxValue, Exception i_InnerException = null)
            : base(string.Format(@"You exceeded the amount . Enter Amount between {0} and {1} ", i_MinValue, i_MaxValue), i_InnerException)
        {
            this.m_MaxValue = i_MaxValue;
            this.m_MinValue = i_MinValue;
        }

        public ValueOutOfRangeException(string i_Message, float i_MinValue, float i_MaxValue)
            : base(i_Message)
        {
            this.m_MaxValue = i_MaxValue;
            this.m_MinValue = i_MinValue;
        }
    }
}
