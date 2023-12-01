using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DictionaryManagement_Models.IntDBModels
{
    public class TagLibrarySheetWithSirTagsDTO
    {
        public string MesParamCode { get; set; }
        public string MesParamNameТМ { get; set; }
        public string MesParamNameTI { get; set; }
        public MesParamDTO? MesParamDTOFK { get; set; }

        [NotMapped]        
        public string MesParamIdToString
        {
            get
            {
                return MesParamDTOFK == null ? "" : MesParamDTOFK.ToStringId;
            }
            set
            {
                MesParamIdToString = value;
            }
        }

        [NotMapped]
        public string MesParamCodeNameToString
        {
            get
            {
                string retVar = "НЕ НАЙДЕН";
                if(MesParamDTOFK != null)
                {
                    retVar = MesParamDTOFK.Code + " " + MesParamDTOFK.Name;
                }
                else
                {
                    if (MesParamCode == "MesParamCode")
                    {
                        retVar = "";
                    }
                }
                return retVar;
            }
            set
            {
                MesParamIdToString = value;
            }
        }

    }
}
