using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CuartaAplicacion.CustomControls
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MiControlPersonalizado : ContentView
	{
		public MiControlPersonalizado ()
		{
			InitializeComponent ();

            BindingContext = this;
		}

        public static readonly BindableProperty MiTextoProperty = BindableProperty.Create(
                  propertyName: "MiTexto",
                  returnType: typeof(string),
                  declaringType: typeof(MiControlPersonalizado),
                  defaultValue: string.Empty,
                  defaultBindingMode: BindingMode.TwoWay
                  //propertyChanged: HandleValuePropertyChanged
            );

        public string MiTexto {
            get
            {
                return (string)base.GetValue(MiTextoProperty);
            }
            set
            {
                base.SetValue(MiTextoProperty, value);
            }
        }
    }
}