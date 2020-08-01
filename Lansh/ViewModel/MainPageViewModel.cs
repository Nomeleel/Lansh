using Lansh.Extend;
using Lansh.Model;
using Lansh.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lansh.ViewModel
{
    public class MainPageViewModel
    {
        public List<NavMenuItem> NavList { set; get; }

        public MainPageViewModel()
        {
            NavList = new List<NavMenuItem>(new[]
                {
                    new NavMenuItem()
                    {
                        Symbol = SymbolExtend.Home,
                        Label = "首页",
                        DestinationPage = typeof(Home)
                    },
                    new NavMenuItem()
                    {
                        Symbol = SymbolExtend.Find,
                        Label = "搜索",
                        DestinationPage = typeof(Search)
                    },
                    new NavMenuItem()
                    {
                        Symbol = SymbolExtend.Like,
                        Label = "春雷觉得赞",
                        DestinationPage = typeof(Play)
                    },
                    new NavMenuItem()
                    {
                        Symbol = SymbolExtend.HeartFill,
                        Label = "赞助作者",
                        //DestPage = typeof(DrillInPage)
                    },
                    new NavMenuItem()
                    {
                        Symbol = SymbolExtend.Feedback,
                        Label = "反馈",
                        DestinationPage = typeof(Feedback)
                    }
                });
        }

    }
}
