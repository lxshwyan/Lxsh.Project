using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Speech.Synthesis;

namespace Lxsh.Project.SpeechSynthesizer.Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            SaveFile("我是李小双");
        }

        /// <summary>
        /// 生成语音文件的方法
        /// </summary>
        /// <param name="text"></param>
        private static void SaveFile(string text)
        {
          
            using (System.Speech.Synthesis.SpeechSynthesizer speechSyn = new System.Speech.Synthesis.SpeechSynthesizer())
            {
                speechSyn.Volume = 100;
                speechSyn.Rate = 0;
                string strPath = @"F:\研发一部\5.源代码\6.组件库\Lxsh.Project\Lxsh.Project.SpeechSynthesizer.Demo\bin\Debug\1.mp3";
                speechSyn.SetOutputToWaveFile(strPath);
                   
                    speechSyn.Speak(text);
                    speechSyn.SetOutputToNull();
                   
            
            }

        }
    }
}
