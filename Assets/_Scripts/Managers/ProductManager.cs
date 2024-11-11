using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Networking;

public class ProductManager : Manager
{
            #region Variables

    public static ProductManager instance;

            #region Variables - Managers

    private UIManager mng_UIManager;

            // End of Variables - Managers
            #endregion
            #region Variables - Components

    [Header("Components")]
    [SerializeField] private List<Transform> comp_ProductPlacements = new List<Transform>();
    [SerializeField] private ProductController[] comp_Controllers = new ProductController[0];

    [SerializeField] private TMP_Text comp_Text = null;

            // End of Variables - Components
            #endregion
            #region Variables - Constants

    private string con_URL = "https://homework.mocart.io/api/products";

            // End of Variables - Constants
            #endregion
            #region Variables - Dynamic

    private string dyn_JSON = "";

            // End of Variables - Dynamic
            #endregion

            // End of Variables
            #endregion
            #region Functions

            #region Functions - Triggered

    private void Awake() //[Trigger]
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    protected override void ManagerStart() //[Trigger]
    {
        mng_UIManager = UIManager.instance;
        
        StartCoroutine(FetchJSON());
    }

    protected override void ManagerUpdate() //[Trigger]
    {
        //
    }

    protected override void ManagerFixedUpdate() //[Trigger]
    {
        //
    }

            // End of Functions - Triggered
            #endregion
            #region Functions - Public

    public bool UpdateProduct(int _index) //[Called]
    {
        string name = mng_UIManager.GetInputFieldText(0);
        string price = mng_UIManager.GetInputFieldText(1);

        string nameOld = comp_Controllers[_index].GetProductName();
        string priceOld = comp_Controllers[_index].GetProductPrice();

        if (name == nameOld && price == priceOld)
            return false;

        comp_Controllers[_index].SetProduct(name, price);
        return true;
    }

    public string GetProductText(int _index, int _field) //[Called]
    {
        return _field == 0 ? comp_Controllers[_index].GetProductName() : comp_Controllers[_index].GetProductPrice();
    }

            // End of Functions - Public
            #endregion
            #region Functions - Private

    private IEnumerator FetchJSON() //[Start]
    {
        using (UnityWebRequest request = UnityWebRequest.Get(con_URL))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                dyn_JSON = request.downloadHandler.text;

                GetProducts();
            }
            else
            {
                Debug.LogError($"ProductManager: {request.error}");
                comp_Text.text = "Error: " + request.error;
            }
        }
    }

    private void GetProducts() //[Start - FetchJSON]
    {        
        if (string.IsNullOrEmpty(dyn_JSON))
            throw new System.Exception("ProductManager: JSON is empty");

        if (comp_ProductPlacements.Count < 1 || comp_Controllers.Length < 1)
            throw new System.Exception("ProductManager: Product placements or Product controllers are empty");
        
        ProductList productList = JsonUtility.FromJson<ProductList>(dyn_JSON);

        int minimum = Mathf.Min(productList.products.Length, comp_ProductPlacements.Count, comp_Controllers.Length);
        for (int i = 0; i < minimum; i++)
        {
            Product product = productList.products[i];

            string productIndex = product.name.Substring(8);
            Instantiate(Resources.Load<GameObject>("Products/" + productIndex), comp_ProductPlacements[i].position, quaternion.identity, comp_ProductPlacements[i]);

            comp_Controllers[i].SetProduct(product);
        }
    }

            // End of Functions - Private
            #endregion

            // End of Functions
            #endregion
}
