// ImportXMLTool.Core/XmlReaderHelper.cs
using System.Data;
using System.Linq;
using System.Xml.Linq;

public static class XmlReaderHelper
{
    public static DataTable ReadToDataTable(string filePath)
    {
        XDocument doc = XDocument.Load(filePath);
        var grouped = doc.Descendants().GroupBy(x => x.Name.LocalName);
        var nodeName = grouped.OrderByDescending(g => g.Count()).First().Key;
        var items = doc.Descendants(nodeName);

        var dt = new DataTable(nodeName);
        var first = items.FirstOrDefault();
        if (first != null)
        {
            foreach (var el in first.Elements())
                dt.Columns.Add(el.Name.LocalName);
        }

        foreach (var item in items)
        {
            var row = dt.NewRow();
            foreach (var el in item.Elements())
                row[el.Name.LocalName] = (string)el.Value;
            dt.Rows.Add(row);
        }
        return dt;
    }
}
