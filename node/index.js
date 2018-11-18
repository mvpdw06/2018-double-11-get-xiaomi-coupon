const request = require('request');

const yourCookie = `enter your cookie here.`;

var options = {
  url: 'https://hd.c.mi.com/tw/eventapi/api/raffle/drawprize?tag=tw_supersalesday_page&present_id=897',
  headers: {
    'Cache-Control': 'no-cache',
    'Origin': 'https://event.mi.com',
    'Referer': 'https://event.mi.com/tw/sales2018/super-sales-day',
    'Cookie': yourCookie
  }
};

const fireRequest = () => {
  request(options, (err, res, body) => {
    console.log(body);
  });
};

let runTimes = 10;
while (runTimes !== 0) {
  fireRequest();
  runTimes--;
}