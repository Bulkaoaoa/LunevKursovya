using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace LunevRestoraunt.Entites
{
     public class AppData
    {
        public static LunevKursovayaEntities Context = new LunevKursovayaEntities();
        public static Frame MainFrame;
        public static User CurrUser;
    }
}
