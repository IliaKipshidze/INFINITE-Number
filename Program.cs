using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace INFINIT
{
    class Program
    {
        public static void calculation()
        {
            //გახსნის ფაილს სადაც გვაქვს მონაცემები და შესაბამისი ოპერაციები
            string path = "C:/Users/comp/Desktop/ილია/TSU III semester/c#/დავალებები/INFINIT/TextFile1.txt";
            var fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            var sr = new StreamReader(fs, Encoding.UTF8);

            string x1; //პირველი რიცხვი
            string x2; //მეორე რიცხვი
            string op;  //ოპერანდი

            //სანამ ფაილში მონაცემები არის, შემოგვაქვს
            while (((x1 = sr.ReadLine()) != null) && ((x2 = sr.ReadLine()) != null) && ((op = sr.ReadLine()) != null))
            {
                //იქმნება ორი infinit რიცხვი
                infinit a = new infinit(x1);
                infinit b = new infinit(x2);
                //იმის მიხედვით თუ რა ოპერანდი შემოვიდა, შეასრულებს შესაბამის მოქმედებას
                if(op == "+") Console.WriteLine($"{a} + {b} = {a + b}");
                else if(op == "-") Console.WriteLine($"{a} - {b} = {a - b}");
                else if(op == "*") Console.WriteLine($"{a} * {b} = {a * b}");
            }
        }
        static void Main(string[] args)
        {

            calculation();
            //txt ფაილში გამრავლებაზე არ ეწერა და მე ჩავამატე ბოლოში :)
            /*უფრო ლამაზი და მოსახერხებელი რომ გამოსულიყო მეთოდების დაწერის ნაცვლად გადავტვირთე ოპერატორები
            თუმცა, private-ად შემოტანილი მაქვს შესაბამისი ლოგიკის მეთოდები, რომლებშიც რეალიზებულია შეკრება, გამოკლება
            და გამრავლება. აგრეთვე, გადავტვირთე ToString() მეთოდი, რათა შესაძლებელი ყოფილიყო ობიექტის პირდაპირ 
            კონსოლში ბეჭდვა. დავამატე შეარების მეთოდიც, რომელიც უშუალოდ ჩემი შეკრების, გამოკლებისა და გამრავლების ალგორითმებზეა
            მორგებული.*/
        }
    }
}
