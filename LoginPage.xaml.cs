//using AndroidX.Core.View.Accessibility;
using CommunityToolkit.Maui.Core;
using Proyectoprogreso2.Models;
using Proyectoprogreso2.Service;

namespace Proyectoprogreso2;

public partial class LoginPage : ContentPage
{
    private readonly APIService _ApiService;
    public LoginPage(APIService apiservice)
	{
		InitializeComponent();
        _ApiService = apiservice;
    }

    private async void OnClickLogin(object sender, EventArgs e)
    {
        Cliente clie = await _ApiService.GetUsuario(NombreU.Text, Contrase�a.Text);
        if (clie != null)
        {
            IntencionCompraDTO intencion = new IntencionCompraDTO
            {
                ClienteIdCliente=clie.IdCliente,
                Fecha="11/12/2023"

            };
            IntencionCompra intencionrespuesta = await _ApiService.PostIntencionCompra(intencion);

            Preferences.Set("idusuario", clie.IdCliente);
            Preferences.Set("CodigoIntencion", intencionrespuesta.IdIntencionCompra);
            NombreU.Text="";
            Contrase�a.Text="";
            await Navigation.PushAsync(new ProductoPage(_ApiService));
        }
        else
        {
            await DisplayAlert("Lo sentimos", "Usuario o contrase�a incorrectos", "OK");

            
        }
    }

   

    private async void OnClickRegistrarse(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new RegistrarsePage(_ApiService));

    }
}