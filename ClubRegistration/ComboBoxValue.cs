using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubRegistration
{
    internal class ComboBoxValue
    {
        private string[] Gender = { "Male", "Female", "Other" };
        private string[] Program = {
                                      "BS in Computer Engineering",
                                      "BS in Computer Science",
                                      "BS in Information Technology",
                                      "BS in Information System"
                                    };

    
        private string selectedGender;
        private string selectedProgram;

     
        public void SetGender(string gender)
        {
            if (Gender.Contains(gender))
            {
                selectedGender = gender;
            }
            else
            {
                Console.WriteLine("Invalid gender selected");
            }
        }

   
        public void SetProgram(string program)
        {
            if (Program.Contains(program))
            {
                selectedProgram = program;
            }
            else
            {
                Console.WriteLine("Invalid program selected");
            }
        }

     
        public string GetGender()
        {
            return selectedGender ?? "Not set";
        }

     
        public string GetProgram()
        {
            return selectedProgram ?? "Not set";
        }

    
        public string[] GetGenderOptions()
        {
            return Gender;
        }

      
        public string[] GetProgramOptions()
        {
            return Program;
        }
    }
}