
namespace GrudNet2429805
{
    public partial class MainPage : ContentPage
    {
        int _editClientesId = 0;
        private readonly LocalDbService _dbService = new LocalDbService();

        public MainPage()
        {
            InitializeComponent();
        }

        private async void SaveButton_Clicked(object sender, EventArgs e)
        {
            if (_editClientesId == 0)
            {
                await _dbService.Create(new Clientes
                {
                    NombreCliente = nombreEntryField.Text,
                    Email = emailEntryField.Text,
                    Movil = movilEntryField.Text // Corrección aquí (== a =)
                });
            }
            else
            {
                await _dbService.Update(new Clientes
                {
                    Id = _editClientesId,
                    NombreCliente = nombreEntryField.Text,
                    Email = emailEntryField.Text,
                    Movil = movilEntryField.Text
                });
                _editClientesId = 0;
            }

            nombreEntryField.Text = string.Empty;
            emailEntryField.Text = string.Empty;
            movilEntryField.Text = string.Empty;

            ListView.ItemsSource = await _dbService.GetClientes(); // Corrección en el nombre
        }

        private async void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var clientes = (Clientes)e.Item;
            var action = await DisplayActionSheet("Action", "Cancel", null, "Edit", "Delete");
            switch (action)
            {
                case "Edit":
                    _editClientesId = clientes.Id;
                    nombreEntryField.Text = clientes.NombreCliente;
                    emailEntryField.Text = clientes.Email;
                    movilEntryField.Text = clientes.Movil;
                    break;

                case "Delete":
                    await _dbService.Delete(clientes);
                    ListView.ItemsSource = await _dbService.GetClientes(); // Corrección en el nombre
                    break;
            }
        }
    }
}