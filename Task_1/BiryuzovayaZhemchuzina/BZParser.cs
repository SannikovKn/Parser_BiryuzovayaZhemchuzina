
using System;
using System.Linq;
using System.Collections.Generic;
using AngleSharp.Html.Dom;
using Task_3.Interfaces;

namespace Task_4.BiryuzovayaZhemchuzina
{
    internal class BZParser : IParser<string[]>
    {
        public string[] Parse(IHtmlDocument document)
        {
            var dataList = new List<string>();

            var items = document.QuerySelectorAll("tr").Select(i => i).Skip(1);
            int y = 0;
            foreach (var item in items)
            {
                y++;
                string flat_id = string.Concat(item.OuterHtml.Substring(item.OuterHtml.IndexOf("flats/"), 10).Where(i => char.IsNumber(i)));
                string flat_num = "105/1";
                string building = item.QuerySelectorAll("td")[8].TextContent;
                string floor = item.QuerySelectorAll("td")[4].TextContent;
                string area = item.QuerySelectorAll("td")[1].TextContent;
                string rooms = item.QuerySelectorAll("td")[0].TextContent[0].ToString();
                if (rooms[0] == 'С')
                    rooms = "0";

                string price = item.QuerySelectorAll("td")[2].TextContent;
                if (price != null )
                    price = price.Replace(" руб.", "").Replace(" ", "").Replace("\t","").Replace("\n", "");

                string status = item.QuerySelectorAll("td")[3].TextContent.Replace("\t", "").Replace("\n", "");
                if (status == "Бронировать")
                    status = "В продаже";

                string date = DateTime.Now.ToString("yyyy-MM-dd");
                
                string result = flat_id + "|" + flat_num + "|" + building + "|" + floor + "|" + area + "|" + rooms + "|" + price + "|" + SpriceCalculation(price, area) + "|" + status + "|" + date;
                dataList.Add(result);
            }

            return dataList.ToArray();
        }

        private double SpriceCalculation(string price, string area)
        {
            double sprice = 0;

            if (price.Length > 5 && area.Length > 0)
            {
                sprice = Math.Round(Convert.ToInt32(price) / Convert.ToDouble(area.Replace(".", ",")), 2);
            }

            return sprice;
        }
    }
}
