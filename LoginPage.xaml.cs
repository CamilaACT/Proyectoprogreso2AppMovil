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
        Cliente clie = await _ApiService.GetUsuario(NombreU.Text, Contraseña.Text);
        if (clie != null)
        {
            IntencionCompraDTO intencion = new IntencionCompraDTO
            {
                ClienteIdCliente=clie.IdCliente,
                Fecha="HOY"

            };
            IntencionCompra intencionrespuesta = await _ApiService.PostIntencionCompra(intencion);

            Preferences.Set("idusuario", clie.IdCliente);
            Preferences.Set("CodigoIntencion", intencionrespuesta.IdIntencionCompra);
            await Navigation.PushAsync(new ProductoPage(_ApiService));
        }
        else
        {
            var toast = CommunityToolkit.Maui.Alerts.Toast.Make("Usuario o contraseña incorrecta", ToastDuration.Short, 14);

            await toast.Show();
        }
    }

   

    private async void OnClickRegistrarse(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new RegistrarsePage(_ApiService));

    }
}