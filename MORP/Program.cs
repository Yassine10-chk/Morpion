using System;
using System.Security.Policy;
using System.Text.RegularExpressions;


namespace Morpion

{

    class Program

    {

        public static int[,] grille = new int[3, 3]; // matrice pour stocker les coups joués


        // Fonction permettant l'affichage du Morpion

        public static void AfficherMorpion(int j, int k)

        {

            for (var p = 0; p < grille.GetLength(0); p++)

            {

                Console.Write("\n|====|====|====|\n");

                Console.Write("|");

                for (var i = 0; i < grille.GetLength(1); i++)

                {

                    if (grille[p, i] == 10)

                        Console.Write("    ");

                    else if (grille[p, i] == 1)

                        Console.Write(" X ");

                    else

                        Console.Write(" O ");



                    Console.Write("|");

                }

            }

            Console.Write("\n|====|====|====|\n");

        }


        // Fonction permettant de changer la grille

        public static bool AJouer(int j, int k, int joueur)

        {

            if (j >= 0 && j < 3 && k >= 0 && k < 3) // Vérifie que l'indice est dans les limites

            {

                if (grille[j, k] == 10) // Vérifie que la case est libre (10 = case vide)

                {

                    grille[j, k] = joueur; // Place le symbole du joueur

                    return true; // Ajout réussi

                }

            }

            return false; // Coup non valide

        }


        // Fonction permettant de vérifier si un joueur a gagné

        public static bool Gagner(int l, int c, int joueur)

        {

            // Vérification de la ligne

            if (grille[l, 0] == joueur && grille[l, 1] == joueur && grille[l, 2] == joueur) return true;


            // Vérification de la colonne

            if (grille[0, c] == joueur && grille[1, c] == joueur && grille[2, c] == joueur) return true;


            // Vérification de la diagonale principale

            if (grille[0, 0] == joueur && grille[1, 1] == joueur && grille[2, 2] == joueur) return true;


            // Vérification de la diagonale secondaire

            if (grille[0, 2] == joueur && grille[1, 1] == joueur && grille[2, 0] == joueur) return true;


            return false;

        }


        // Programme principal

        static void Main(string[] args)

        {

            //--- Déclarations et initialisations --

            int LigneDébut = Console.CursorTop; // Position de départ

            int ColonneDébut = Console.CursorLeft;


            int essais = 0; // compteur d'essais

            int joueur = 1; // 1 pour le premier joueur, 2 pour le second

            int l, c; // Numéro de ligne et de colonne

            bool gagner = false; // Permet de vérifier si un joueur a gagné

            bool bonnePosition; // Vérifie si la position souhaitée est disponible


            //--- Initialisation de la grille ---

            for (int j = 0; j < grille.GetLength(0); j++)

                for (int k = 0; k < grille.GetLength(1); k++)

                    grille[j, k] = 10;


            while (!gagner && essais < 9)

            {

                AfficherMorpion(0, 0);


                // Demander au joueur de jouer

                do

                {

                    try

                    {

                        Console.WriteLine($"C'est au tour du joueur {joueur} :");

                        Console.Write("Ligne = ");

                        l = int.Parse(Console.ReadLine()) - 1;

                        Console.Write("Colonne = ");

                        c = int.Parse(Console.ReadLine()) - 1;


                        bonnePosition = AJouer(l, c, joueur);

                        if (!bonnePosition)

                            Console.WriteLine("Case déjà occupée ou hors limite. Réessayez.");

                    }

                    catch (Exception)

                    {

                        Console.WriteLine("Entrée invalide. Réessayez.");

                        bonnePosition = false;

                    }

                } while (!bonnePosition);


                // Vérifier si le joueur a gagné

                gagner = Gagner(1, 2, joueur);


                // Changer de joueur si personne n'a gagné

                if (!gagner)

                {

                    joueur = (joueur == 1) ? 2 : 1;

                    essais++;

                }

            }


            // Fin de la partie

            AfficherMorpion(0, 0);

            if (gagner)

                Console.WriteLine($"Félicitations, le joueur {joueur} a gagné !");

            else

                Console.WriteLine("Match nul !");


            Console.ReadKey();

        }

    }

}