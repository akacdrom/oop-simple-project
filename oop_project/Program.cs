using System;
using System.IO;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace oop_project
{
    class Program
    {       
        abstract class movies
        {
            
            protected string keyword;
            protected int yearselection;
            public int counter = 0;
            public int error_counter = 0;
            public int search_year;
            private int year;
            public int Year 
                {
                    get {return year;}
                    set {if(value<1700){System.Console.WriteLine("year is too low"); return;} else {year = value;}}
                }   
            private string line; 
            public string name,type,actor,genre,imdb;
            public abstract void Counter();
            
            public Stopwatch sw = new Stopwatch();
            public Stopwatch wt = new Stopwatch();

            public void takeFilter()
            {
                
                sw.Restart();
                System.IO.StreamReader file = new System.IO.StreamReader("movies.txt");
                while((line = file.ReadLine()) != null)
                    
                {
                    
                    if (line.Contains(keyword))
                    {
                        var parts = line.Split(';').ToList();

                        
                        if (yearselection == 1)
                        {
                            try
                            {
                                if (int.Parse(parts[5]) >= search_year)
                                {
                                    read();
                                }
                            }
                            catch (System.FormatException)
                            {
                                error_counter++;
                            }
                            
                        }
                        else if (yearselection == 2)
                        {
                            
                            try
                            {
                                if (int.Parse(parts[5]) <= search_year)
                                {
                                    read();
                                }
                            }
                            catch (System.FormatException)
                            {
                                error_counter++;
                            }
                            
                        }
                        
                        else
                        {
                            read();
                        }
                    }
                    
                }  
                
                file.Close();
                sw.Stop();
                System.Console.WriteLine(); 
            }

            public void read()
            {
                
                var parts = line.Split(';').ToList();   
                System.Console.WriteLine($"NAME: {parts[0]} \n TYPE: {parts[1]} \n ACTOR: {parts[2]} \n YEAR: {parts[5]} \n GENRE: {parts[6]} \n IMDB-PAGE: {parts[10]}");
                counter++;                  
            }
            public virtual void Timer() 
            {
                Console.WriteLine("Nothing measured.");
            }
        }

        class Readff : movies
        {
            public override void Timer() 
            {
                Console.WriteLine("File readed and written to Console in "+sw.ElapsedMilliseconds+" ms.");
            }
            public override void Counter()
            {
                System.Console.WriteLine($"There were {counter} movies.\nAnd {error_counter} movies couldn't showed cause of syntax error");
            }
            public void readkeyword()
            {
                
                System.Console.Write("Enter the key word: "); keyword = Console.ReadLine();
                System.Console.Write("1-After the date: ");
                System.Console.WriteLine("\n2-Before the date: ");
                System.Console.WriteLine("3-All the movies"); yearselection = int.Parse(Console.ReadLine());
                if (yearselection != 3)
                {
                    System.Console.Write("Enter the date: "); search_year = int.Parse(Console.ReadLine());  
                }           
            }

           
        }
        class Writetf : movies
        {
            public override void Timer() 
            {
                Console.WriteLine("File readed and written to Console in "+sw.ElapsedMilliseconds+" ms.");
                Console.WriteLine("File written in "+wt.ElapsedMilliseconds+" ms.");
            }
            public override void Counter()
            {
                System.Console.WriteLine("New Movie added right now we have {0} movies.",counter);
            }
            public void write()
            {
                System.Console.Write("Name: "); name=Console.ReadLine();
                System.Console.Write("Type: "); type=Console.ReadLine();
                System.Console.Write("Actor: "); actor=Console.ReadLine();
                System.Console.Write("Year: "); Year=int.Parse(Console.ReadLine());
                System.Console.Write("Genre: "); genre=Console.ReadLine();
                System.Console.Write("Imdb: "); imdb=Console.ReadLine();

                
                wt.Restart();

                using(StreamWriter file = new StreamWriter("movies.txt",true))
                    {
                        file.WriteLine($"{name};{type};{actor}; ; ;{Year};{genre}; ; ; ;{imdb};");
                    }

                wt.Stop();
            keyword = "";
            }
        }

        static void Main(string[] args)
        {
            Console.Clear();

            Readff readff = new Readff();
            readff.readkeyword();
            readff.takeFilter();
            readff.Counter();
            readff.Timer();
            Console.ReadLine();

            Writetf writetf = new Writetf();
            writetf.write();
            writetf.takeFilter();
            writetf.Counter();
            writetf.Timer();
            Console.ReadLine();
            
        }
    }
}