using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mirutrading.Application.Core.Weixin
{
    public class UnifiedOrder
    {
        public string appid = "";
        public string mch_id = "";
        public string device_info = "";
        public string nonce_str = "";
        public string sign = "";
        public string body = "";
        public string attach = "";
        public string out_trade_no = "";
        public int total_fee = 0;
        public string spbill_create_ip = "";
        //yyyyMMddHHmmss
        public string time_start = "";
        public string time_expire = "";
        public string goods_tag = "";
        public string notify_url = "";
        //JSAPI、NATIVE、APP
        public string trade_type = "";
        public string openid = "";
        //只在 trade_type 为 NATIVE时需要填写。
        public string product_id = "";
    }
}
