using Proyectoprogreso2.Models;
using Proyectoprogreso2.Service;
using System.Collections.ObjectModel;

namespace Proyectoprogreso2;

public partial class ListaCarrito : ContentPage
{
    private readonly APIService _ApiService;
    public ListaCarrito(APIService apiservice)
	{
		InitializeComponent();
        _ApiService = apiservice;
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        int idintencioncompra = Preferences.Get("CodigoIntencion", 0);
        List<IntencionDescripcion> listaProducto = await _ApiService.GetListaDescripcionIntencion(idintencioncompra);
        var products = new ObservableCollection<IntencionDescripcion>(listaProducto);
        ListaViewCarrito.ItemsSource = products;
        var totalprecio = await _ApiService.GetPrecioTotal(idintencioncompra);
        PrecioTotalCompra.Text= totalprecio.ToString();
    }

    private async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (e.SelectedItem == null)
            return;

        IntencionDescripcion selectedIntencion = ListaViewCarrito.SelectedItem as IntencionDescripcion;

        if (selectedIntencion != null)
        {
            await Navigation.PushAsync(new DetallesDescripcionIntencion(_ApiService)
            {
                BindingContext = selectedIntencion,
            });
        }

        // Desactiva la selecci�n del elemento
        ListaViewCarrito.SelectedItem = null;
    }
    private async void ComprarClick(object sender, EventArgs e)
    { 


        await Navigation.PushAsync(new ConfirmacionCompra(_ApiService));

    }


}