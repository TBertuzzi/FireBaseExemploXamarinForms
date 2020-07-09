using Firebase.Database;
using Firebase.Database.Query;
using FireBaseExemploXamarinForms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireBaseExemploXamarinForms.Services
{
    public class FireBaseService
    {
        FirebaseClient firebase = new FirebaseClient("URL DO SEU BANCO");

        public async Task<List<Pokemon>> ObterPokemons()
        {

            return (await firebase
              .Child("Pokemons")
              .OnceAsync<Pokemon>()).Select(item => new Pokemon
              {
                  Nome = item.Object.Nome,
                  Numero = item.Object.Numero
              }).ToList();
        }

        public async Task AdicionarPokemon(int numero, string nome)
        {

            await firebase
              .Child("Pokemons")
              .PostAsync(new Pokemon() { Numero = numero, Nome = nome });
        }

        public async Task<Pokemon> ObterPokemon(int numero)
        {
            var allPersons = await ObterPokemons();
            await firebase
              .Child("Pokemons")
              .OnceAsync<Pokemon>();
            return allPersons.Where(a => a.Numero == numero).FirstOrDefault();
        }

        public async Task AtualizarPokemon(int numero, string nome)
        {
            var toUpdatePerson = (await firebase
              .Child("Pokemons")
              .OnceAsync<Pokemon>()).Where(a => a.Object.Numero == numero).FirstOrDefault();

            await firebase
              .Child("Pokemons")
              .Child(toUpdatePerson.Key)
              .PutAsync(new Pokemon() { Numero = numero, Nome = nome });
        }

        public async Task ApagarPokemon(int personId)
        {
            var toDeletePerson = (await firebase
              .Child("Pokemons")
              .OnceAsync<Pokemon>()).Where(a => a.Object.Numero == personId).FirstOrDefault();
            await firebase.Child("Pokemons").Child(toDeletePerson.Key).DeleteAsync();

        }
    }
}
