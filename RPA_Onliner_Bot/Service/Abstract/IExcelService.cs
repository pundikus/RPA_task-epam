using System.Collections.Generic;
using RPA_Onliner_Bot.DataFile;

namespace RPA_Onliner_Bot.Service.Abstract
{
    public interface IExcelService
    {
        bool WriteData(List<MicrowaveData> list);
    }
}
