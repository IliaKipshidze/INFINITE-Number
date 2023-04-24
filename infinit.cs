using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INFINIT
{
    class infinit
    {
        List<int> number = new List<int>();
        public char Sign;
        public infinit(string num)
        {
            //სტრინგს დავშლით და ავაწყობთ ინტების ლისტს
            int i = 0;
            //გავარკვევთ გადმოცემული რიცხვის ნიშანს
            if (num[0] == '-')
            {
                Sign = '-';
                i++;
            }
            else Sign ='+';
            //თუ წინ მინუსი აქვს ინტებად დაშლას დაიწყებს მეორე, ხოლო წინააღმდეგ შემთხვევაში პირველივე ელემენტიდან
            for (; i < num.Length; i++)
            {
                number.Add(int.Parse(num[i].ToString()));
            }
        }
        infinit(List<int> a, char s)
        {
            //ზოგიერთი ოპერაციისთვის კლასის შიგნით დაგვჭირდება ასეთი კონსტრუქტორიც, რომელსაც ლისტს და ნიშანს გადავცემთ
            number = a;
            Sign = s;
        }
        public static infinit operator *(infinit inf1, infinit inf2)
        {
            char s;
            //იმის მიხედვით თუ რა ნიშნები აქვთ გადაცემულ რიცხვებს, განვსაზღვრავთ შედეგის ნიშანს
            if ((inf1.Sign == '+' && inf2.Sign == '+') || (inf1.Sign == '-' && inf2.Sign == '-')) s = '+';
            else s = '-';
            infinit ret = Mult(inf1.number, inf2.number);
            ret.Sign = s;
            //თუ მინუსიანი მივიღეთ მაგრამ რიცხვი ნულია, ნიშანში ვუწერთი პლიუსს რათა შედეგის გამოტანისას "-0" არ დაგვიწეროს
            if (ret.ToString()[0] == '-' && ret.ToString()[1] == '0') ret.Sign = '+';
            return ret; 
        }
        public static infinit operator -(infinit inf1, infinit inf2_2)
        {
            //აქ მთავარი არის ნიშნის განსაზღვრა, ხოლო გამოკლებას მიმატებაზე მანიპულირებითაც შევძლებთ
            infinit inf2 = new infinit(inf2_2.number, inf2_2.Sign);
            if (inf2.Sign == '+') inf2.Sign = '-';
            else inf2.Sign = '+';
            infinit tmp = inf1 + inf2;
            return new infinit(tmp.number, tmp.Sign);
        }
        public static infinit operator +(infinit inf1, infinit inf2)
        {
            List<int> temp = new List<int>();
            if (inf1.Sign == '-') //თუ პირველი რიცხვი უარყოფითია
            {
                if (inf2.Sign == '+') // ხოლო მეორე დადებითი
                {
                    if (compare1(inf1.number, inf2.number) == 1) //და ნიშნის გარეშე პირველი მეტია
                    {
                        //პირველს გამოაკლდება მეორე და დაეწერება მინუს ნიშანი
                        temp = Sub(inf1.number, inf2.number); 
                        return new infinit(temp, '-');
                    }
                    else //ხოლო, თუ ნიშნის გარეშე მეორეა მეტი, ან ტოლია
                    {
                        //მეორეს გამოაკლდება პირველი და დაეწერება + ნიშანი
                        temp = Sub(inf2.number, inf1.number);
                        return new infinit(temp, '+');
                    }
                }
                else //თუ მეორე უარყოფითია
                {
                    //შეიკრიბება და დაეწერება მინუს ნიშანი
                    temp = Sum(inf1.number, inf2.number);
                    return new infinit(temp, '-');
                }
            }
            else //თუ პირველი რიცხვი დადებითია
            {
                if (inf2.Sign == '+') //და მეორეც დადებითი
                {
                    //შეიკრიბება და დაეწერება + ნიშანი
                    temp = Sum(inf1.number, inf2.number);
                    return new infinit(temp, '+');
                }
                else //მეორე კი უარყოფითი
                {
                    if(compare1(inf1.number, inf2.number) == -1) //და ნიშნის გარეშე მეორე მეტია პირველზე
                    {
                        //მეორეს გამოაკლდება პირველი და ნიშანი იქნება -
                        temp = Sub(inf2.number, inf1.number);
                        return new infinit(temp, '-');
                    }
                    else //პირველი მეტია ან ტოლია მეორეზე
                    {
                        //პირველს გამოაკლდება მეორე და ნიშანი იქნება +
                        temp = Sub(inf1.number, inf2.number);
                        return new infinit(temp, '+');
                    }
                }
            }
        }
        static infinit Mult(List<int> x, List<int> y)
        {
            List<int> a;
            List<int> b;
            //გავარკვიოთ რომელია და a-ში ჩავწეროთ უფრო გრძელი რიცხვი
            if (compare1(x, y) == 1)
            {
                a = new List<int>(x);
                b = new List<int>(y);
            }
            else
            {
                a = new List<int>(y);
                b = new List<int>(x);
            }
            //გამრავლების დროს ქვეშმიწერის ხაზს ქვემოთ ვიღებთ რამდენიმე რიცხვს (მატრიცის მსგავსად) რომელიც შემდეგ უნდა შევკრიბოთ
            //ამ რიცხვების შესანახად გამოვიყენოთ ლისტების ლისტი
            List<List<int>> result = new List<List<int>>();
            for (int i = b.Count - 1; i >= 0; i--) //დავიწყოთ გამრავლება ბოლოდან
            {
                //b რიცხვის ყოველი ციფრი გავამრავლოთ a-ს ყოველ ციფრზე
                List<int> temp = new List<int>(); //თითოეული მომავალში შესაკრების შესანახად
                //წანაცვლებამდე ჩავსვათ ნულები
                for (int j = 0; j < b.Count - 1 - i; j++) 
                {
                    temp.Add(0);
                }
                int c = 0; //დამახსოვრებულისთვის (რომელიც თავიდან ნულია)
                for (int j = a.Count - 1; j >= 0; j--)
                {
                    temp.Insert(0, (b[i] * a[j] + c) % 10); //მნიშვნელობა რომელიც იწერება
                    c = (b[i] * a[j] + c) / 10; //დამახსოვრებული მნიშვნელობა
                }
                if (c != 0) temp.Insert(0, c); //თუ ბოლოს კიდევ დაგვრჩება დამახსოვრებული მნიშვნელობა, ჩავამატოთ წინ
                result.Add(temp);
            }
            infinit ret = new infinit("0"); //შევქმნათ რიცხვი რომელიც თავიდან ნულის ტოლია
            //და მისი მეშვეობით შევკრიბოთ გამრავლების შედეგად მიღებული შესაკრები რიცხვები (მატრიცა)
            for(int i = 0; i < result.Count; i++)
            {
                infinit temp = new infinit(result[i], '+');
                ret = ret + temp;
            }
            return ret;
        }
        public static List<int> Sum(List<int> x, List<int> y)
        {
            List<int> a = new List<int>(x);
            List<int> b = new List<int>(y);

            List<int> ret = new List<int>();
            //ვპოულობთ არის თუ არა სიგრძეებს შორის განსხვავება, თუ ასეა მის მნიშვნელობას (რამდენით განსხვავდება)
            //ვინახავთ dif ში, და ამის მიხედვით იმას, რომლის სიგრძეც ნაკლებია, მეორეს სიგრძემდე შესავსებად წინ ჩავუმატებთ 0-ებს
            if (a.Count > b.Count)
            {
                int dif = a.Count - b.Count;
                for (int i = 0; i < dif; i++) b.Insert(0, 0);
            }
            else if (b.Count > a.Count)
            {
                int dif = b.Count - a.Count;
                for (int i = 0; i < dif; i++) a.Insert(0, 0);
            }
            int c = 0; //დამახსოვრებული მნიშვნელობის შესანახად (რომელიც თავიდან ნულია)
            for(int i = a.Count - 1; i >= 0; i--) //შეკრებას ვიწყებთ ბოლოდან
            {
                ret.Insert(0, (a[i] + b[i] + c) % 10); //ჩავამატოთ შედეგის ლისტში ის მნიშვნელობა რასაც ქვეშმიწერით შეკრების დროს ვწერთ
                if (a[i] + b[i] + c > 9) c = 1; //c-ცვლადში კი შევინახოთ დამახსოვრებული მნიშვნელობა თუ საჭიროა (1-ზე მეტი ვერ იქნება)
                else c = 0;  //წინააღმდეგ შემთხვევაში c = 0
            }
            if (c == 1) ret.Insert(0, 1); //ყველაზე წინა თანრიგების მიმატებისას თუ ისევ დაგვრჩა დამახსოვრებული მნიშვნელობა, ჩავამატოთ
            return ret;
        }
        static List<int> Sub(List<int> x, List<int> y)
        {
            //როდესაც აქ გადავცემ ლისტებს, ნიშნის გარეშე პირველი მეტი უნდა იყოს მეორეზე
            List<int> a = new List<int>(x);
            List<int> b = new List<int>(y);

            List<int> ret = new List<int>();
            //აქ კი ვამოწმებთ, პირველი სიგრძითაც მეტი ხომ არაა მეორეზე და თუ ასეა მეორეს წინ ნულებით შევავსებთ პირველის სიგრძემდე
            if (a.Count > b.Count)
            {
                int dif = a.Count - b.Count;
                for (int i = 0; i < dif; i++) b.Insert(0, 0);
            }
            for (int i = a.Count - 1; i >= 0; i--) //გამოკლებას ვიწყებთ ბოლოდან
            {
                if (a[i] < b[i]) //თუ ზედას ქვედა "არ აკლდება"
                {
                    int j = i - 1;
                    while (true) //მაშინ გადავდივართ მარცხნივ და ვეძებთ ციფრს რომლისგანაც ვისესხებთ
                    {
                        if (a[j] == 0) a[j] = 9; //როდესაც სესხებას გავაკეთებთ თუ, გამსესხებელ რიცხვსა და მსესხებელს შორის არის 0-ები, მაშინ იქ უნდა ჩავწეროთ 9
                        else
                        {
                            a[j] = a[j] - 1; //და თუ არანულოვან ციფრს მივადგებით, მისგან ვისესხებთ, რაც იმას ნიშნავს რომ, ის შემცირდება 1-ით
                            break;
                        }
                        j--;
                    }
                    ret.Insert(0, 10 + a[i] - b[i]); //სესხების გამო, a[i]-ის ნაცვლად a[i]+10 -ს დააკლდება b[i]
                }
                else
                {
                    ret.Insert(0, a[i] - b[i]); //თუ სესხება არ გვჭირდება, პირდაპირ დააკლდება
                }
            }
            while ((ret[0] == 0) && ret.Count != 1) ret.Remove(0); //თუ წინ ზედმეტი 0-ები დაგვრჩება, ამოვიღებთ
            return ret;
        }
        static int compare1(List<int> a, List<int> b)
        {
            if (a.Count > b.Count) return 1;
            else if (b.Count > a.Count) return -1;
            else
            {
                //თუ სიგრძეები ტოლია, მაშინ დაიწყება ერთნაირი თანრიგის ციფრების შედარება
                for(int i = 0; i < a.Count; i++)
                {
                    if (a[i] > b[i]) return 1;
                    else if (a[i] < b[i]) return -1;
                }
            }
            return 0;
        }
        public override string ToString()
        {
            //ჯერ განვსაზღვრავთ ნიშანს, თუ მინუსია, მაშინ დავწერთ ხოლო თუ პლიუსია - არა.
            string s = "";
            if (Sign == '-') s += "-";
            //ჩვეულებრივ, ვბეჭდავთ ლისტს
            for(int i = 0; i < number.Count; i++)
            {
                s += number[i].ToString();
            }
            return s;
        }
    }
}
