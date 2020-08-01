using Lansh.Extend;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace Lansh.Model
{
    public class NavMenuItem
    {
        #region private attributes
        private string label;
        private SymbolExtend symbol;
        private Type destinationPage;
        private object arguments;
        #endregion

        #region attributes public method
        public string Label
        {
            get
            {
                return label;
            }
            set
            {
                label = value;
            }
        }
        public SymbolExtend Symbol
        {
            get
            {
                return symbol;
            }
            set
            {
                symbol = value;
            }
        }
        public char SymbolAsChar
        {
            get
            {
                return (char)this.Symbol;
            }
        }
        public Type DestinationPage
        {
            get
            {
                return destinationPage;
            }
            set
            {
                destinationPage = value;
            }
        }
        public object Arguments
        {
            get
            {
                return arguments;
            }
            set
            {
                arguments = value;
            }
        }
        #endregion

    }
}
