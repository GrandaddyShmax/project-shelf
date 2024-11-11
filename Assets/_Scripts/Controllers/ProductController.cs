using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ProductController : Controller
{
            #region Variables

            #region Variables - Components

    [Header("Product Controller")]
    [SerializeField] private TMP_Text comp_Name;
    [SerializeField] private TMP_Text comp_Price;

            // End of Variables - Components
            #endregion

            // End of Variables
            #endregion
            #region Functions

            #region Functions - Triggered
    
    protected override void ControllerUpdate() //[Trigger]
    {
        //
    }

    protected override void ControllerFixedUpdate() //[Trigger]
    {
        //
    }

            // End of Functions - Triggered
            #endregion
            #region Functions - Public

    public void SetProduct(Product product) // [Called]
    {
        comp_Name.text = product.name;
        comp_Price.text = product.price.ToString() + "$";
    }

    public void SetProduct(string name, string price) // [Called]
    {
        comp_Name.text = name;
        comp_Price.text = price + "$";
    }

    public string GetProductName() => comp_Name.text;

    public string GetProductPrice() => comp_Price.text.Substring(0, comp_Price.text.Length - 1);

            // End of Functions - Public
            #endregion

            // End of Functions
            #endregion
}
