using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessPension
{
    public class ClientInput
    {
        
        public string Name { get; set; }

        public DateTime DateOfBirth { get; set; }
       
        public string Pan { get; set; }
       
        public string AadharNumber { get; set; }
        
        public PensionType PensionType { get; set; }
        
    }


    public class ValueforCalCulation
    {
        public int BankType { get; set; }
        public int SalaryEarned { get; set; }
        public int Allowances { get; set; }
        public PensionType PensionType { get; set; }
    }

    public enum PensionType
    {
        Self=1,
        Family=2
    }
}
