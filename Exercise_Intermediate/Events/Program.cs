namespace Events
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //1. The Basic Alarm (Define and Invoke)
            AlarmClock myAlarmClock = new AlarmClock();
            myAlarmClock.AlarmRaised += MyAlarmClock_OnAlarmRaised;
            myAlarmClock.StartAlarm();

            //2. The Video Encoder (Custom EventArgs)
            VideoEncoder encoder = new VideoEncoder();
            encoder.VideoEncoded += Encoder_OnVideoEncoder;
            encoder.EncodeVideo("Train Your Dragon");
            encoder.EncodeVideo("The Matrix");

            //3.The Newsletter Service(Unsubscribe)
            NewsLetter myNewsLetter = new NewsLetter();
            Console.WriteLine("--- Subscribing ---");
            myNewsLetter.NewsLetterRaised += MyNewsLetter_NewsLetterRaised;
            myNewsLetter.PrintMessage("Welcome to our service!");
            myNewsLetter.NewsLetterRaised -= MyNewsLetter_NewsLetterRaised;
            myNewsLetter.PrintMessage("This message won't print out");

            //4. Temperature Monitor (Update UI Logic)
            Thermostat myThermostat = new Thermostat();
            Display myDisplay = new Display(myThermostat);
            myThermostat.UpdateTemperature(43);
            myThermostat.UpdateTemperature(43);
            myThermostat.UpdateTemperature(35);
            myThermostat.UpdateTemperature(60);

            //5. The Battle System (Decouple Logic)
            Player myPlayer = new Player();
            ScoreTracker myScoreTracker = new ScoreTracker(myPlayer);
            myPlayer.DefeatEnemy(50);
            myPlayer.DefeatEnemy(20);
            myPlayer.DefeatEnemy(30);
            myPlayer.DefeatEnemy(40);


        }

        private static void MyNewsLetter_NewsLetterRaised(object? sender, NewsLetterEventArgs e)
        {
            Console.WriteLine($"[{DateTime.Now}]Printing message... {e.Messages} which is receiving at {e.DateTime} ");
            System.Threading.Thread.Sleep(1000);

        }

        private static void Encoder_OnVideoEncoder(object? sender, VideoEncoderEventArgs e)
        {
            Console.WriteLine($"{e.Title} movie had been encoded at {e.CompletionTime}");
        }

        private static void MyAlarmClock_OnAlarmRaised(object? sender, EventArgs e)
        {
            Console.WriteLine("Wake Up!");
        }
    }

    class AlarmClock
    {
        public event EventHandler AlarmRaised;

        public void StartAlarm()
        {
            AlarmRaised?.Invoke(this, EventArgs.Empty);
        }
    }

    public class VideoEncoderEventArgs : EventArgs
    {
        public string Title {  get; init; } = string.Empty;
        public DateTime CompletionTime { get; init; }
    }

    class VideoEncoder
    {
        public event EventHandler<VideoEncoderEventArgs> VideoEncoded;

        public void EncodeVideo(string movieTitle)
        {
            Console.WriteLine($"Encoding video: {movieTitle}...");
            System.Threading.Thread.Sleep(1000);
            
            VideoEncoded?.Invoke(this, new VideoEncoderEventArgs
            { 
                Title = movieTitle,
                CompletionTime = DateTime.Now,
            });
        }
    }

    public class NewsLetterEventArgs : EventArgs
    {
        public string Messages { get; init; }
        public DateTime DateTime { get; init; }
    }
    
    class NewsLetter
    {
        public event EventHandler<NewsLetterEventArgs> NewsLetterRaised;
        public void PrintMessage(string message)
        {
            Console.WriteLine($"Receiving message at {DateTime.Now}");
            NewsLetterRaised?.Invoke(this, new NewsLetterEventArgs
            {
                Messages = message,
                DateTime = DateTime.Now,
            });
        }
    }

    public class TemperatureChangedEventArgs: EventArgs
    {
        public int Temperature { get; set; }
    }
    class Thermostat
    {
        public int CurrentTemperature { get; set; }

        public event EventHandler<TemperatureChangedEventArgs> TemperatureChanged;

        public void UpdateTemperature(int temp)
        {
            if (CurrentTemperature == temp) return;
            CurrentTemperature = temp;
            TemperatureChanged?.Invoke(this, new TemperatureChangedEventArgs
            {
                Temperature = temp,
            });
        }

    }

    class Display
    {
        public Display(Thermostat thermostat)
        {
            thermostat.TemperatureChanged += Thermostat_onTemperatureChanged;
        }

        private void Thermostat_onTemperatureChanged(object? sender, TemperatureChangedEventArgs e)
        {
            Console.WriteLine($"UI Update: The screen now shows {e.Temperature}°C");
        }
    }

    public class EnemyDefeatedEventArgs : EventArgs
    {
        public int OverallScore { get; set; }
    }
    class Player
    {
        public int Score {  set; get; }
        public event EventHandler<EnemyDefeatedEventArgs> EnemyDefeated;

        public void DefeatEnemy(int score)
        {
            this.Score += score;
            EnemyDefeated?.Invoke(this, new EnemyDefeatedEventArgs
            {
                OverallScore = this.Score,
            });
        }
    }

    class ScoreTracker
    {
        public ScoreTracker(Player player)
        {
            player.EnemyDefeated += Player_EnemyDefeated;
        }

        private void Player_EnemyDefeated(object? sender, EnemyDefeatedEventArgs e)
        {
            Console.WriteLine($"Score Update: current score is {e.OverallScore}");
        }
    }
}
