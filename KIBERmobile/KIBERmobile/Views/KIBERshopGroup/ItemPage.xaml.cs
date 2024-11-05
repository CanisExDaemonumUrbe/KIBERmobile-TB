using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KIBERmobile.ViewModels;

namespace KIBERmobile.Views.KIBERshopGroup;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class ItemPage : ContentPage
{
    private readonly ItemVM _vm;

    public ItemPage(int itemId)
    {
        BindingContext = _vm = new ItemVM(itemId);
        InitializeComponent();
    }
}