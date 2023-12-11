using Proyectoprogreso2.Models;
using Proyectoprogreso2.Service;
using System.Collections.ObjectModel;

namespace Proyectoprogreso2;

public partial class ProductoPage : ContentPage
{

    private readonly APIService _ApiService;
    public ProductoPage(APIService apiservice)
    {
        InitializeComponent();
        _ApiService = apiservice;

    }

    private async void OnCarritoClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ListaCarrito(_ApiService));
    }


    private async void Btn_Buscar(object sender, EventArgs e)
    {


        ProductoColorTalla p = await _ApiService.GetProducto(Int32.Parse(ID.Text));
        await Navigation.PushAsync(new DetalleProductoPage(_ApiService){
            BindingContext = p,
        });
    }


   
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        List<ProductoColorTalla> listaProducto = await _ApiService.GetProductos();
        var products = new ObservableCollection<ProductoColorTalla>(listaProducto);
        ListaProducto.ItemsSource = products;
    }
    private async void OnClickShowDetails(object sender, SelectedItemChangedEventArgs e)
    {
     
        ProductoColorTalla producto = e.SelectedItem as ProductoColorTalla;
        await Navigation.PushAsync(new DetalleProductoPage(_ApiService)
        {
            BindingContext = producto,
        });
    }

    private async void OnUsuarioClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MenuUsuario(_ApiService));
    }


}