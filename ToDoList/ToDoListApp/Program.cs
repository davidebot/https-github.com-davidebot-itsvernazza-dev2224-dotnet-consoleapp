﻿using System.Diagnostics;
using ToDoListApp.Models;

namespace ToDoListApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // lista delle attività
            List<Attivita> todoList = new List<Attivita>();

            #region inserimento
            // variabile per gestire se l'utente vuole inserire nuove attività
            bool elencoTerminato = false;

            // ciclo per gestire se l'utente vuole inserire nuove attività
            while (elencoTerminato == false)
            {
                // input del nome dell'attività
                Console.WriteLine("Inserisci un nome per l'attività");
                string? nome = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(nome)) // controlla che il nome non sia null, vuoto o contenga soltanto spazi
                {
                    throw new ArgumentNullException(nameof(nome),
                        "Il nome dell'attività non è valido.");
                }

                // input della durata stimata dell'attività
                Console.WriteLine("Inserisci la durata stimata dell'attività (in minuti)");
                string? durata = Console.ReadLine();
                if (Int32.TryParse(durata, out int durataConvertita) == false || durataConvertita <= 0) // controlla che la durata sia un numero valido e maggiore di zero
                {
                    throw new InvalidCastException("La durata inserita non è valida.");
                }

                // crea l'istanza della nuova attività che intende inserire l'utente
                Attivita nuovaAttivita = new Attivita()
                {
                    Nome = nome.Trim(),
                    Durata = durataConvertita,
                };

                // alternativa al .Any usato successivamente
                //foreach (Attivita attivita in todoList)
                //{
                //    if (attivita.Nome == nuovaAttivita.Nome)
                //    {
                //        throw new InvalidOperationException($"L'attività con nome {nuovaAttivita.Nome} è già presente");
                //    }
                //    else
                //    {
                //        todoList.Add(nuovaAttivita);
                //    }
                //}

                // alternativa compatta al .Any usato successivamente
                //if (todoList.Any(attivita => attivita.Nome.ToLower().Trim() == nuovaAttivita.Nome.ToLower().Trim()))

                // controlla se ALMENO UN elemento nella lista delle attività contiene il nome della nuova attività (in minuscolo, senza spazi)
                if (todoList
                    .Any(attivita =>
                    {
                        return attivita.Nome.ToLower().Trim() == nuovaAttivita.Nome.ToLower().Trim();
                    })
                )
                {
                    // eccezione generata se il nome dell'attività è già presente
                    throw new InvalidOperationException($"L'attività con nome {nuovaAttivita.Nome} è già presente");
                }
                else
                {
                    // aggiunge l'attività alla lista
                    todoList.Add(nuovaAttivita);
                }

                // mostra a video l'elenco di tutte le attività
                for (int i = 0; i < todoList.Count; i++)
                {
                    var numElenco = i + 1;
                    var attivita = todoList[i];
                    Console.WriteLine($"{numElenco}. Attività: {attivita.Nome}. Durata stimata: {attivita.GetDurata()}");
                }

                // alternativa al ciclo for precedente
                //int progressivoAttivita = 0;
                //foreach (var attivita in todoList) 
                //{
                //    progressivoAttivita++;
                //    Console.WriteLine($"{progressivoAttivita}. Attività: {attivita.Nome}. Durata stimata: {attivita.GetDurata()}");
                //}

                // variabile per gestire se l'utente ha digitato correttamente il carattere 'S' o 'N'
                bool vuoleInserireValido = false;

                // ciclo per verificare se l'utente ha digitato correttamente il carattere 'S' o 'N'
                do
                {
                    try
                    {
                        // input se l'utente vuole inserire una nuova attività
                        Console.WriteLine("Vuoi inserire una nuova attività? (digita S o N)");
                        string? vuoleInserire = Console.ReadLine();

                        // controlla che il valore inserito sia valido, altrimenti genera un'eccezione
                        if (vuoleInserire != "S" && vuoleInserire != "N")
                        {
                            throw new ArgumentException("Digita 'S' o 'N'");
                        }
                        else
                        {
                            vuoleInserireValido = true;
                        }

                        // se l'utente non vuole inserire una nuova attività, termina il ciclo per l'inserimento di nuove attività (quello più esterno)
                        if (vuoleInserire != "S")
                        {
                            elencoTerminato = true;
                        }
                    }
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine(ex.Message);
                        vuoleInserireValido = false;
                    }
                } while (vuoleInserireValido == false);

            }
            #endregion inserimento

            #region cancellazione
            // variabile per gestire se l'utente vuole cancellare un'attività
            bool cancellazioneTerminata = false;

            // ciclo per gestire se l'utente vuole cancellare un'attività
            while (cancellazioneTerminata == false)
            {
                // variabile per gestire se l'utente ha digitato correttamente il carattere 'S' o 'N'
                bool vuoleCancellareValido = false;
                // ciclo per verificare se l'utente ha digitato correttamente il carattere 'S' o 'N'
                do
                {
                    try
                    {
                        // input se l'utente vuole cancellare un'attività
                        Console.WriteLine("Vuoi cancellare un'attività? (digita S o N)");
                        string? vuoleCancellare = Console.ReadLine();

                        // controlla che il valore inserito sia valido, altrimenti genera un'eccezione
                        if (vuoleCancellare != "S" && vuoleCancellare != "N")
                        {
                            throw new ArgumentException("Digita 'S' o 'N'");
                        }
                        else
                        {
                            vuoleCancellareValido = true;
                        }

                        // se l'utente non vuole cancellare un'attività, termina il ciclo per la cancellazione di attività (quello più esterno)
                        if (vuoleCancellare != "S")
                        {
                            cancellazioneTerminata = true;
                        }

                        // se l'utente vuole cancellare un'attività
                        if (cancellazioneTerminata == false)
                        {
                            // mostra a video l'elenco di tutte le attività
                            Console.WriteLine("Elenco delle attività esistenti:");
                            for (int i = 0; i < todoList.Count; i++)
                            {
                                var numElenco = i + 1;
                                var attivita = todoList[i];
                                Console.WriteLine($"{numElenco}. Attività: {attivita.Nome}. Durata stimata: {attivita.GetDurata()}");
                            }

                            // input del nome dell'attività da cancellare
                            Console.WriteLine("Digita il nome dell'attività che vorresti cancellare:");
                            string? nomeAttivitaDaCancellare = Console.ReadLine();
                            if (string.IsNullOrWhiteSpace(nomeAttivitaDaCancellare)) // controlla che il nome non sia null, vuoto o contenga soltanto spazi
                            {
                                throw new ArgumentNullException(nameof(nomeAttivitaDaCancellare),
                                    "Il nome dell'attività non è valido.");
                            }

                            // controlla che il nome sia presente nell'elenco
                            if (todoList.Any(x => x.Nome == nomeAttivitaDaCancellare) == false)
                            {
                                throw new ArgumentException(nameof(nomeAttivitaDaCancellare),
                                    "Il nome dell'attività non esiste.");
                            }

                            /*
                            // metodo 1
                            var listaAttivitaFiltrata = todoList.Where(attivita => attivita.Nome == nomeAttivitaDaCancellare);
                            var attivitaDaCancellare1 = listaAttivitaFiltrata.First();
                            todoList.Remove(attivitaDaCancellare1);

                            // metodo 2
                            Attivita? attivitaDaCancellare2 = null;
                            foreach (var attivita in todoList)
                            {
                                if (attivita.Nome == nomeAttivitaDaCancellare)
                                {
                                    attivitaDaCancellare2 = attivita;
                                }
                            }
                            if (attivitaDaCancellare2 != null)
                            {
                                todoList.Remove(attivitaDaCancellare2);
                            }

                            // metodo 3
                            List<Attivita> elencoSenzaAttivitaDaCancellare = new List<Attivita>();
                            foreach (var attivita in todoList)
                            {
                                if (attivita.Nome != nomeAttivitaDaCancellare)
                                {
                                    elencoSenzaAttivitaDaCancellare.Add(attivita);
                                }
                            }
                            todoList = elencoSenzaAttivitaDaCancellare;

                            // metodo 4 
                            var attivitaDaEliminare = todoList.Where(attivita => attivita.Nome == nomeAttivitaDaCancellare).First();
                            int indiceAttivitaDaCancellare1 = todoList.IndexOf(attivitaDaEliminare);
                            todoList.RemoveAt(indiceAttivitaDaCancellare1);

                            // metodo 5
                            int indiceAttivitaDaCancellare2 = -1;
                            for (int i = 0; i < todoList.Count; i++)
                            {
                                if (todoList[i].Nome == nomeAttivitaDaCancellare)
                                {
                                    indiceAttivitaDaCancellare2 = i;
                                }
                            } 
                            todoList.RemoveAt(indiceAttivitaDaCancellare2);

                            // metodo 6 (sconsigliato)
                            //for (int i = 0;i < todoList.Count;i++)
                            //{
                            //    var attivita = todoList[i];
                            //    if (attivita.Nome == nomeAttivitaDaCancellare)
                            //    {
                            //        todoList.Remove(attivita);
                            //    }
                            //}
                            */

                            // metodo 7 (anche con più nomi uguali) <-- CONSIGLIATO
                            // crea una nuova lista di attività filtrata per nome diverso da quello digitato dall'utente e la sostituisce
                            todoList = todoList.Where(attivita => attivita.Nome != nomeAttivitaDaCancellare).ToList();

                            // metodo 7 esteso
                            //var listaFiltrata = todoList.Where(attivita => attivita.Nome != nomeAttivitaDaCancellare).ToList();
                            //todoList = listaFiltrata;

                            // mostra a video l'elenco di tutte le attività
                            Console.WriteLine("Elenco delle attività aggiornato:");
                            for (int i = 0; i < todoList.Count; i++)
                            {
                                var numElenco = i + 1;
                                var attivita = todoList[i];
                                Console.WriteLine($"{numElenco}. Attività: {attivita.Nome}. Durata stimata: {attivita.GetDurata()}");
                            }
                        }
                    }
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine(ex.Message);
                        vuoleCancellareValido = false;
                    }
                } while (vuoleCancellareValido == false);
            }
            #endregion

            Console.ReadLine();
        }
    }
}