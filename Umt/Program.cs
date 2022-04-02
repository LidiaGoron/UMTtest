using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Umt
{
    class Program
    {
        //public static void StrongPassword(string password);

        //Limita inferioara si superioara pentru lungimea parolei
        private const int MinLength = 6;
        private const int MaxLength = 9;

        public static void StrongPasswordChecker(string password)
        {

            int changes = 0; 

            int[] ok = new int[3];


            if (password.Length > MaxLength) //Verificam daca lungimea parolei depaseste limita superioara
            {
                changes = password.Length - MaxLength;  //Numarul de schimbari va fi egal cu nr. de caractere peste limita superioara
                password = password.Substring(0, MaxLength);  //Actualizam permanent lungimea parolei

            }

            //In urmatoarea secventa de cod, se verifica daca parola introdusa contine grupuri  de caractere  identice consecutive de lungime strict egala 3
            // si totodata, se verifica daca parola contine litera mica, litera mare si cifra, folosindu-ne de vectorul "ok". Daca suma elementelor din vectorul
            // "ok" este 3, atunci parola le contine pe toate 3, iar daca suma elementelor din vector este diferita de 3, parolei ii lipsesc una sau mai multe
            // caractere impuse (litera mica/litera mare/cifra).
            char? stored = null;
            int occurrenceCounter = 0;
            int replacementsNeeded = 0;
            for (int i = 0; i < password.Length; i++)
            {
                if(stored!=password[i])
                {
                    stored = password[i];
                    occurrenceCounter = 0;
                }
                occurrenceCounter++;
                if(occurrenceCounter==2)
                {
                    replacementsNeeded++;
                    occurrenceCounter = 0;
                }

                if (char.IsLower(password[i]))
                {
                    ok[0] = 1;
                }
                else if (char.IsUpper(password[i]))
                {
                    ok[1] = 1;
                }

                else if (char.IsDigit(password[i]))
                {
                    ok[2] = 1;

                }
            }

            //Verificam daca lungimea parolei este sub limita inferioara impusa
            int minLengthAdjustment = 0;
            if (password.Length < MinLength)
            {
                minLengthAdjustment = MinLength - password.Length; //Nr. de schimari necesare va fi diferenta de caractere pana la limita inferioara
            }

            // Putem garanta ca schimbarile de simboluri se pot inlocui in replacementsNeeded. Concret, daca vom avea in parola nevoie atat de schimari de caractere
            //(litera mica/litera mare/cifra), cat si schimari de grupuri, un caracter lipsa poate fi inlocuit intr-un grup de 3 caractere indentice consecutive.
            //In cazul in care nu avem niciun grup de caractere identice consecutiv, se vor lua in calcul doar schimbarile necesare pentru simboluri.
            int missingSymbols=ok.Count(x => x == 0);
            int symbolChanges = missingSymbols - replacementsNeeded;  

            // 
            changes += Math.Max(minLengthAdjustment, symbolChanges) + replacementsNeeded;

            Console.WriteLine(changes);
        }


        static void Main(string[] args)
        {
            TestPassword();
        }

        static void TestPassword()
        {
            string password = Console.ReadLine();
            if(password == "Stop")
            {
                return;
            }
            StrongPasswordChecker(password);
            TestPassword();
        }
    }
}


