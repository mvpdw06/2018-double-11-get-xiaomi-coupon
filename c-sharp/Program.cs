using System;
using System.Diagnostics;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace MiCoupon
{
    class Program
    {
        static readonly string Cookie = $"Your cookie here.";
        static void Main(string[] args)
        {
            Console.WriteLine("Hi! Are you ready? Press any key to START!");
            Console.ReadLine();
            RunJob();
        }

        static void RunJob()
        {
            var targetStartTime = DateTime.Today.AddHours(19).AddMinutes(59).AddSeconds(30);
            var targetEndTime = DateTime.Today.AddHours(20).AddMinutes(1).AddSeconds(59);
            while (true)
            {
                var now = DateTime.Now;
                if (now > targetStartTime && now < targetEndTime)
                {
                    MakeTenRequest();
                }
                else
                {
                    Console.WriteLine($"{now} It's not the time to start yet.");
                }

                Thread.Sleep(1000);
            }
        }

        static void MakeTenRequest()
        {
            var sw = new Stopwatch();
            sw.Start();
            Parallel.For(0, 100, (i, loopState) =>
            {
                try
                {
                    // 250 coupon
                    //var url = $"https://hd.c.mi.com/tw/eventapi/api/raffle/drawprize?tag=tw_supersalesday_page&present_id=897";
                    // 500 coupon
                    var url = $"https://hd.c.mi.com/tw/eventapi/api/raffle/drawprize?tag=tw_supersalesday_page&present_id=898";
                    // 11 light 4174300001
                    //var url = $"https://fs.buy.mi.com/tw/seckill/do?jsonpcallback=seckill&goods_id=4174300001";
                    // 11 watch 4183200087
                    //var url = $"https://fs.buy.mi.com/tw/seckill/do?jsonpcallback=seckill&goods_id=4183200087";
                    var request = (HttpWebRequest)WebRequest.Create(url);
                    request.Headers.Add("Cache-Control", "no-cache");
                    request.Headers.Add("Origin", "https://event.mi.com");
                    request.Headers.Add("Cookie", Cookie);
                    request.Referer = "https://event.mi.com/tw/sales2018/super-sales-day";
                    //request.Host = "fs.buy.mi.com";
                    using (System.IO.Stream s = request.GetResponse().GetResponseStream())
                    {
                        using (System.IO.StreamReader sr = new System.IO.StreamReader(s))
                        {
                            var jsonResponse = sr.ReadToEnd();
                            Console.WriteLine($"Response: {jsonResponse}");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception to Fail.");
                }
            });
            sw.Stop();
            Console.WriteLine($"Cost {sw.ElapsedMilliseconds} ms.");
        }
    }
}
