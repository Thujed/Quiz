using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Quiz.Model
{
    public class Question : BindableBase
    {
        public Question(string questionText, TimeSpan timeToAnswer) {
            this.QuestionText = questionText;
            this.TimeToAnswer = timeToAnswer;
            timer = new Timer((o) =>
            {
                StopTimer();
                TimeToAnswer = TimeSpan.FromSeconds(TimeToAnswer.Seconds - 1);

            }, null, Timeout.Infinite, 0);
        }

        private Timer timer;

        public string QuestionText { get; }

        private TimeSpan _timeToAmswer;
        public TimeSpan TimeToAnswer {
            get => _timeToAmswer;
            set {
                SetProperty(ref _timeToAmswer, value);
            }
        }

        public void StopTimer() {
            if (TimeToAnswer.Seconds == 0) {
                timer.Change(0, Timeout.Infinite);
            }
        }

    }
}
