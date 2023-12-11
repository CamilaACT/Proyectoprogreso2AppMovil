using Proyectoprogreso2.Models;
using Proyectoprogreso2.Service;

namespace Proyectoprogreso2;

public partial class EditarUsuario : ContentPage
{
    private readonly APIService _ApiService;
    private Cliente _cliente;
    public EditarUsuario(APIService apiservice)
    {
        InitializeComponent();
        _ApiService = apiservice;
    }

    private async void OnClickLogin(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }

    private async void OnClickRegistrarse(object sender, EventArgs e)
    {
        Cliente Cli = new Cliente
        {
            IdCliente = 0,
            Nombre = Nombre.Text,
            Cedula = ID.Text,
            Apellido = Apellido.Text,
            Direccion=Direccion.Text,
            NumeroTarjeta=Int32.Parse(Tarjeta.Text),
            Login=NombreU.Text,
            Contrasenia=Contraseña.Text
        };
        await _ApiService.PutCliente(Cli);
        await Navigation.PopAsync();

        
    }
}