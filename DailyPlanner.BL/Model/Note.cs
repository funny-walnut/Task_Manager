using System;


namespace DailyPlanner.BL.Model
{
    /// <summary>
    /// Событие.
    /// </summary>
    public class Note
    {
        /// <summary>
        /// Столбцы в БД.
        /// </summary>
        public int Id { get; set; }

        public int? Hour { get; set; }

        public string Description { get; set; }

        /// <summary>
        /// Конструктор события.
        /// </summary>
        /// <param name="hour"></param>
        /// <param name="description"></param>
        /// 
        public Note(int hour, string description)
        {
           
            if (hour < 0 || hour > 23)
            {
                throw new ArgumentException("Недопустимое значение часа.");
            }

            if (string.IsNullOrWhiteSpace(description))
            {
                throw new ArgumentNullException("Описание не может быть пустым.");
            }
            Hour = hour;
            Description = description;

        }

        public Note()
        {
        }
    }
}
