using DailyPlanner.BL.Model;
using System;
using System.Data.Entity;
using System.Linq;


namespace DailyPlanner.CMD
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Добро пожаловать в DailyPlanner!");
            Console.WriteLine("Нажмите любую клавишу.");
            Console.ReadLine();
           

            while (true)
            {
                Console.WriteLine("A - добавить событие;");
                Console.WriteLine("B - удалить событие;");
                Console.WriteLine("С - редактировать событие;");
                Console.WriteLine("D - вывести планы на день;");
                Console.WriteLine("Q - выход.");

               
                var key = Console.ReadKey();

                Console.WriteLine();

                switch (key.Key)
                {

                    //Добавление новых событий.

                    case ConsoleKey.A:

                using (var context = new MyDbContext())
                {
                    var note = EnterNote ();

                    var day = context.Notes.ToList();
                            // Проверка на уникальность.
                            var newNote = day.FirstOrDefault(i => i.Hour == note.Hour);

                            if (newNote == null)
                            {
                                context.Notes.Add(note);

                                context.SaveChanges();


                                Console.WriteLine("Событие добавлено. Нажмите любую клавишу.");
                                Console.ReadLine();
                            }

                            else
                            {
                                Console.WriteLine("Событие в это время уже существует. Нажмите любую клавишу.");
                                Console.ReadLine();
                                break;
                            }
                    
                                      
                }
                        break;

                        //Удаление.

                    case ConsoleKey.B:
                using (var context = new MyDbContext())
                {
                            var i = ParseInt(" время (hh-00), которое хотите удалить.");

                            var note = context.Notes.Single(a => a.Hour == i);
                            
                            if (note!=null)
                            {
                                context.Entry(note).State  = EntityState.Deleted;
                                context.SaveChanges();
                                Console.WriteLine("Событие удалено. Нажмите любуюю клавишу.");
                                Console.ReadLine();
                                
                            }
                            else
                            {
                                Console.WriteLine("События в это время нет. Нажмите любуюю клавишу.");
                                Console.ReadLine();
                            }              
                }
                        break;
                        
                        //Редактирование.

                    case ConsoleKey.C:
                        using (var context = new MyDbContext())
                        {
                            var i = ParseInt(" время (hh-00), которое хотите редактировать.");
                            
                            var note = context.Notes.Single(a => a.Hour == i);
                            Console.WriteLine($"Введите новое событие в {i} часов:");
                            note.Description = Console.ReadLine();

                          
                            context.Entry(note).State = EntityState.Modified;
                            context.SaveChanges();

                       
                        }
                        
                        break;

                        //Отображение списка дел.
                       
                    case ConsoleKey.D:
                        
                        using (var context = new MyDbContext())
                        {
                            
                            var day = context.Notes.ToList();

                            //Сортировка дел в порядке возрастания времени.

                            var sortNotes = from item in day
                                              orderby item.Hour
                                              select item;

                            foreach (Note item in sortNotes)   
                            
                                Console.WriteLine ($"\t  {item.Hour} : 00 \t {item.Description}" );
                         
                            
                        }
                        Console.ReadLine();

                        break;
                        
                        // Выход из приложения.

                    case ConsoleKey.Q:
                        Environment.Exit(0);
                        break;
                }    
            }
        }

        /// <summary>
        /// Ввод описания дела.
        /// </summary>
        /// <returns></returns>
        private static Note EnterNote()
        {            
            var hour = ParseInt (" время начала события: __:00.");
            Console.WriteLine("Введите описание события.");
            var desc = Console.ReadLine();
            var note = new Note(hour, desc);
            return note;
        }
        
        private static int ParseInt(string name)
        {
            while (true)
            {
                Console.Write($"Введите {name}: ");
                if (int.TryParse(Console.ReadLine(), out int value))
                {
                    return value;
                }
                else
                {
                    Console.WriteLine($"Неверный формат поля {name}");
                }
            }
        }
    } 

}
