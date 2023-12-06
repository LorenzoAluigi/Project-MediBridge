using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API_MediBridge.Models.Class
{
    public class MedicationDTO
    {
        public string MedicationName { get; set; }
        public string DailyDosage { get; set; }
        public string MedicalConditionId { get; set; }
        public string PlanId { get; set; }
        public string MedicationId { get; set; }
        public string MedicalCondition {  get; set; }
        public string TPlanDescription {  get; set; }
    }
}