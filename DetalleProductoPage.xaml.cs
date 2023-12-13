using CommunityToolkit.Maui.Core;
using Proyectoprogreso2.Models;
using Proyectoprogreso2.Service;
using System.Collections.ObjectModel;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Proyectoprogreso2;

public partial class DetalleProductoPage : ContentPage
{
    private ProductoColorTalla _producto;
    private readonly APIService _ApiService;
    public List<string> Numeros { get; set; }
    private int cantidadMaximaStock;
    public DetalleProductoPage(APIService apiservice)
    {
        InitializeComponent();
        _ApiService = apiservice;
    }


    protected override void OnAppearing()
    {
        base.OnAppearing();
        _producto = BindingContext as ProductoColorTalla;
        Nombre.Text = _producto.Producto.nombre;
        Cantidad.Text = _producto.stock.ToString();
        cantidadMaximaStock=_producto.stock;
        Descripcion.Text = _producto.Producto.descripcion;
        Color.Text = _producto.ColorProducto.nombre;
        CargarNumerosEnPicker();
    }

    private async void OnClickSalir(object sender, EventArgs e)
    {

        await Navigation.PopAsync();

    }

    private async void OnClickAgregarAlCarrito(object sender, EventArgs e)
    {

        int usuarioid = Preferences.Get("idusuario", 0);
        if (usuarioid==0)
        {
            Navigation.PushAsync(new LoginPage(_ApiService));
        }
        else
        {
           
            //toma el indice del picker y suma uno para saber la cantidad que mando el usuario
            var cantidadSeleccionada = CantidadPicker.SelectedIndex;
            var cantidad = cantidadSeleccionada+1;

            if (cantidad==0)
            {
                await DisplayAlert("Esperaaa", "Agrega algo al carrito primero :)", "OK");
            }
            else
            {


                int idintencioncompra = Preferences.Get("CodigoIntencion", 0);



                IntencionDescripcionDTO intencionCompra = new IntencionDescripcionDTO
                {
                    Cantidad = cantidad,
                    ProductoColorTallaIdProductoColorTalla = _producto.idProductoColorTalla,
                    IntencionCompraIdIntencionCompra = idintencioncompra,
                };

                IntencionDescripcion intenciondescipcion = await _ApiService.PostIntencionDescripcion(intencionCompra);
                await DisplayAlert("Yeiii", "Agregaste el producto a tu carrito con éxito", "OK");
                await Navigation.PopAsync();

                //var toast = CommunityToolkit.Maui.Alerts.Toast.Make("Usuario o contraseña incorrecta", ToastDuration.Short, 14);

                //await toast.Show();
            }
        }
    }

    private void CargarNumerosEnPicker()
    {
        // Crear una lista de números
        Numeros = new List<string>();
        for (int i = 1; i<=cantidadMaximaStock; i++)
        {
            Numeros.Add(i.ToString());
        }

        // Asignar la lista al Picker
        CantidadPicker.ItemsSource = Numeros;
    }
}