using System.Xml.Serialization;

namespace DK_Hardware.Entity
{
    [XmlRoot(ElementName = "ProductsResponse")]
    public class Entity_Banner
    {
        [XmlElement(ElementName = "product")]
        public List<Product> Product { get; set; }

        [XmlAttribute(AttributeName = "xsi")]
        public string Xsi { get; set; }

        [XmlAttribute(AttributeName = "xsd")]
        public string Xsd { get; set; }

        [XmlText]
        public string Text { get; set; }
    }


    [XmlRoot(ElementName = "product")]
    public class Product
    {

        [XmlElement(ElementName = "itemCode")]
        public string ItemCode { get; set; }

        [XmlElement(ElementName = "name")]
        public string Name { get; set; }

        [XmlElement(ElementName = "manufacturer")]
        public string Manufacturer { get; set; }

        [XmlElement(ElementName = "upc")]
        public double Upc { get; set; }

        [XmlElement(ElementName = "price")]
        public double Price { get; set; }

        [XmlElement(ElementName = "brand")]
        public string Brand { get; set; }
    }

}

