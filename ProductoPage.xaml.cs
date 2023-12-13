using Proyectoprogreso2.Models;
using Proyectoprogreso2.Service;
using System.Collections.ObjectModel;
using static Java.Util.Jar.Attributes;

namespace Proyectoprogreso2;

public partial class ProductoPage : ContentPage
{

    private readonly APIService _ApiService;
    public ProductoPage(APIService apiservice)
    {
        InitializeComponent();
        _ApiService = apiservice;
        int usuarioid = Preferences.Get("idusuario", 0);
        if (usuarioid==0)
        {
            Preferences.Set("idusuario", 0);
            Preferences.Set("CodigoIntencion", 0);
         
        }
     
          

    }

    private async void OnCarritoClicked(object sender, EventArgs e)
    {
        int usuarioid = Preferences.Get("idusuario", 0);
        if (usuarioid==0)
        {
            Navigation.PushAsync(new LoginPage(_ApiService));
        }
        else
        {

            await Navigation.PushAsync(new ListaCarrito(_ApiService));
        }
       
    }


    
        private async void Btn_Buscar(object sender, EventArgs e)
        {
            // Verificar si el Entry "Name" está vacío
            if (string.IsNullOrWhiteSpace(ID.Text))
            {
                // Entry está vacío, puedes mostrar un mensaje o simplemente salir del método
                DisplayAlert("Advertencia", "Por favor, ingrese un ID válido.", "OK");
                return;
            }

            // Continuar con la búsqueda ya que el Entry "Name" contiene algún valor
            ProductoColorTalla p = await _ApiService.GetProducto(Int32.Parse(ID.Text));

            // Verificar si se encontró un producto antes de navegar a la página de detalles
            if (p != null )
            {
                await Navigation.PushAsync(new DetalleProductoPage(_ApiService)
                {
                    BindingContext = p,
                });
            }
            else
            {
                // Mostrar un mensaje si no se encontró ningún producto con el ID proporcionado
                DisplayAlert("Advertencia", "No se encontró ningún producto con el ID proporcionado.", "OK");
            }
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
        int usuarioid = Preferences.Get("idusuario", 0);
        if (usuarioid==0)
        {
            Navigation.PushAsync(new LoginPage(_ApiService));
        }
        else {

            await Navigation.PushAsync(new MenuUsuario(_ApiService));
        }
        
    }


}