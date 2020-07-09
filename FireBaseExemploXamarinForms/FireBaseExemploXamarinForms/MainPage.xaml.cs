using FireBaseExemploXamarinForms.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FireBaseExemploXamarinForms
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        FireBaseService firebaseService = new FireBaseService();
        public MainPage()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {

            base.OnAppearing();
            var pokemons = await firebaseService.ObterPokemons();
            lvPokemons.ItemsSource = pokemons;
        }

        private async void BtAdicionar_Clicked(object sender, EventArgs e)
        {
            await firebaseService.AdicionarPokemon(Convert.ToInt32(entryNumero.Text), entryNome.Text);
            entryNumero.Text = string.Empty;
            entryNome.Text = string.Empty;
            await DisplayAlert("Sucesso", "Pokemon Adicionado com Sucesso", "OK");
            var pokemons = await firebaseService.ObterPokemons();
            lvPokemons.ItemsSource = pokemons;
        }

        private async void btnObter_Clicked(object sender, EventArgs e)
        {
            var pokemon = await firebaseService.ObterPokemon(Convert.ToInt32(entryNumero.Text));
            if (pokemon != null)
            {
                entryNumero.Text = pokemon.Numero.ToString();
                entryNome.Text = pokemon.Nome;
            }
            else
            {
                await DisplayAlert("Sucesso", "Pokemon não encontrado", "OK");
            }

        }

        private async void btnAtualizar_Clicked(object sender, EventArgs e)
        {
            await firebaseService.AtualizarPokemon(Convert.ToInt32(entryNumero.Text), entryNome.Text);
            entryNumero.Text = string.Empty;
            entryNome.Text = string.Empty;
            await DisplayAlert("Sucesso", "Pokemon Atualizado", "OK");
            var pokemons = await firebaseService.ObterPokemons();
            lvPokemons.ItemsSource = pokemons;
        }

        private async void btnApagar_Clicked(object sender, EventArgs e)
        {
            await firebaseService.ApagarPokemon(Convert.ToInt32(entryNumero.Text));
            await DisplayAlert("Sucesso", "Pokemon apagado com sucesso", "OK");
            var pokemons = await firebaseService.ObterPokemons();
            lvPokemons.ItemsSource = pokemons;
        }
    }
}
