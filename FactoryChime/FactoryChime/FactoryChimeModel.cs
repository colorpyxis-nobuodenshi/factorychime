using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FactoryChime
{
    public class FactoryChimeSchedule
    {
        public string Time { get; set; }
        public string Sound { get; set; }
        public SoundPlayer SoundPlayer { get; set; }
        public string Name { get; set; }
        public int Key { get; set; }
        public bool Replayed { get; set; } = false;
    }

    public class MemoryStorage
    {
        public MemoryStorage()
        {

        }

        public static MemoryStorage Instance { get; } = new MemoryStorage();
        public SortedDictionary<string, FactoryChimeSchedule> FactoryChimeSchedules { get; } = new SortedDictionary<string, FactoryChimeSchedule>(); 

        public void Load()
        {
            var lines = System.IO.File.ReadAllLines(@"chimeschedule.txt");
            foreach(var line in lines)
            {
                if (line == "")
                    break;
                var v = line.Split(",");
                var time = v[0];
                if(!Regex.IsMatch(time, "^([01][0-9]|2[0-3]):[0-5][0-9]"))
                {
                    throw new ApplicationException("時刻の書式エラー。");
                }
                var name = v[1];
                var sound = v[2];
                var player = new SoundPlayer($@"Sound\{sound}");
                FactoryChimeSchedules.Add(time, new FactoryChimeSchedule() { Time = time, Name = name, Sound = sound, SoundPlayer = player });

            }
        }
    }
}
