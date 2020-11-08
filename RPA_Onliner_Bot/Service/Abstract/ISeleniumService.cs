using System.Collections.Generic;
using RPA_Onliner_Bot.DataFile;

namespace RPA_Onliner_Bot.Service.Abstract
{
    public interface ISeleniumService
    {
        List<MicrowaveData> GetData();
    }
}
