[System.Serializable]
public class ProductList
{
    public Product[] products;
}

[System.Serializable]
public class Product
{
    public string name;
    public string description;
    public float price;
}
