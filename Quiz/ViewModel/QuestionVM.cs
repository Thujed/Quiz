using Prism.Mvvm;
using Quiz.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Quiz.ViewModel
{
    public class QuestionVM : BindableBase
    {
        private Question _question;

        public QuestionVM(Question question) {
            _question = question;
        }

        public string TimeToAnswer => _question.TimeToAnswer.ToString(@"mm\:ss");
        public string QuestionText => _question.QuestionText;
    }
}
