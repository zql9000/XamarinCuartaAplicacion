using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
//using Android.Widget;
using CuartaAplicacion.Droid.Renderers;
using CuartaAplicacion.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(MiPropiaLista), typeof(MiPropioListViewRenderer))]
namespace CuartaAplicacion.Droid.Renderers
{
    public class MiPropioListViewRenderer : ListViewRenderer
    {
        public MiPropioListViewRenderer(Context context) : base(context)
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<ListView> e)
        {
            base.OnElementChanged(e);

            this.SetBackgroundColor(Android.Graphics.Color.Aqua);
        }
    }
}