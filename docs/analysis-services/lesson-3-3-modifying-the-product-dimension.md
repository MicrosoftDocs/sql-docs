---
title: "Modifying the Product Dimension | Microsoft Docs"
ms.date: 05/08/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: multidimensional-models
ms.topic: tutorial
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# Lesson 3-3 - Modifying the Product Dimension
[!INCLUDE[ssas-appliesto-sqlas](../includes/ssas-appliesto-sqlas.md)]

In the tasks in this topic, you use a named calculation to provide more descriptive names for the product lines, define a hierarchy in the Product dimension, and specify the (All) member name for the hierarchy. You also group attributes into display folders.  
  
## Adding a Named Calculation  
You can add a named calculation to a table in a data source view. In the following task, you create a named calculation that displays the full product line name.  
  
#### To add a named calculation  
  
1.  To open the **Adventure Works DW 2012** data source view, double-click **Adventure Works DW 2012** in the **Data Source Views** folder in Solution Explorer.  
  
2.  In the bottom of the diagram pane, right-click the **Product** table header, and then click **New Named Calculation**.  
  
3.  In the **Create Named Calculation** dialog box, type **ProductLineName** in the **Column name** box.  
  
4.  In the **Expression** box, type or copy and paste the following **CASE** statement:  
  
    ```  
    CASE ProductLine  
       WHEN 'M' THEN 'Mountain'  
       WHEN 'R' THEN 'Road'  
       WHEN 'S' THEN 'Accessory'  
       WHEN 'T' THEN 'Touring'  
       ELSE 'Components'  
    END  
    ```  
  
    This **CASE** statement creates user-friendly names for each product line in the cube.  
  
5.  Click **OK** to create the **ProductLineName** named calculation. You might need to wait.  
  
6.  On the **File** menu, click **Save All**.  
  
## Modifying the NameColumn Property of an Attribute  
  
#### To modify the NameColumn property value of an attribute  
  
1.  Switch to Dimension Designer for the Product dimension. To do this, double-click the **Product** dimension in the **Dimensions** node of Solution Explorer.  
  
2.  In the **Attributes** pane of the **Dimension Structure** tab, select **Product Line**.  
  
3.  In the Properties window on the right side of the screen, click the **NameColumn** property field at the bottom of the window, and then click the browse (**...**) button to open the **Name Column** dialog box. (You might need to click the **Properties** tab on the right side of the screen to open the Properties window.  
  
4.  Select **ProductLineName** at the bottom of the **Source column** list, and then click **OK**.  
  
    The NameColumn field now contains the text, **Product.ProductLineName (WChar)**. The members of the **Product Line** attribute hierarchy now display the full name of the product line instead of an abbreviated product line name.  
  
5.  In the **Attributes** pane of the **Dimension Structure** tab, select **Product Key**.  
  
6.  In the Properties window, click the **NameColumn** property field, and then click the ellipsis browse (**...**) button to open the **Name Column** dialog box.  
  
7.  Select **EnglishProductName** in the **Source column** list, and then click **OK**.  
  
    The NameColumn field now contains the text, **Product.EnglishProductName (WChar)**.  
  
8.  In the Properties window, scroll up, click the **Name** property field, and then type **Product Name**.  
  
## Creating a Hierarchy  
  
#### To create a hierarchy  
  
1.  Drag the **Product Line** attribute from the **Attributes** pane into the **Hierarchies** pane.  
  
2.  Drag the **Model Name** attribute from the **Attributes** pane into the **<new level>** cell in the **Hierarchies** pane, underneath the **Product Line** level.  
  
3.  Drag the **Product Name** attribute from the **Attributes** pane into the **<new level>** cell in the **Hierarchies** pane, underneath the **Model Name** level. (You renamed Product Key to Product Name in the previous section.)  
  
4.  In the **Hierarchies** pane of the **Dimension Structure** tab, right-click the title bar of the **Hierarchy** hierarchy, click **Rename**, and then type **Product Model Lines**.  
  
    The name of the hierarchy is now **Product Model Lines**.  
  
5.  On the **File** menu, click **Save All**.  
  
## Specifying Folder Names and All Member Names  
  
#### To specify the folder and member names  
  
1.  In the **Attributes** pane, select the following attributes by holding down the CTRL key while clicking each of them:  
  
    -   **Class**  
  
    -   **Color**  
  
    -   **Days To Manufacture**  
  
    -   **Reorder Point**  
  
    -   **Safety Stock Level**  
  
    -   **Size**  
  
    -   **Size Range**  
  
    -   **Style**  
  
    -   **Weight**  
  
2.  In the **AttributeHierarchyDisplayFolder** property field in the Properties window, type **Stocking**.  
  
    You have now grouped these attributes into a single display folder.  
  
3.  In the **Attributes** pane, select the following attributes:  
  
    -   **Dealer Price**  
  
    -   **List Price**  
  
    -   **Standard Cost**  
  
4.  In the **AttributeHierarchyDisplayFolder** property cell in the Properties window, type **Financial**.  
  
    You have now grouped these attributes into a second display folder.  
  
5.  In the **Attributes** pane, select the following attributes:  
  
    -   **End Date**  
  
    -   **Start Date**  
  
    -   **Status**  
  
6.  In the **AttributeHierarchyDisplayFolder** property cell in the Properties window, type **History**.  
  
    You have now grouped these attributes into a third display folder.  
  
7.  Select the **Product Model Lines** hierarchy in the **Hierarchies** pane, and then change the **AllMemberName** property in the Properties window to **All Products**.  
  
8.  Click an open area of the **Hierarchies** pane, and then change the **AttributeAllMemberName** property at the top of the Properties window to **All Products**.  
  
    Clicking an open area lets you modify properties of the Product dimension itself. You could also click **Product** at the top of the attributes list in the **Attributes** pane.  
  
9. On the **File** menu, click **Save All**.  
  
## Defining Attribute Relationships  
If the underlying data supports it, you should define attribute relationships between attributes. Defining attribute relationships speeds up dimension, partition, and query processing. For more information, see [Define Attribute Relationships](../analysis-services/multidimensional-models/attribute-relationships-define.md) and [Attribute Relationships](../analysis-services/multidimensional-models-olap-logical-dimension-objects/attribute-relationships.md).  
  
#### To define attribute relationships  
  
1.  In the **Dimension Designer** for the Product dimension, click the **Attribute Relationships** tab.  
  
2.  In the diagram, right-click the **Model Name** attribute, and then click **New Attribute Relationship**.  
  
3.  In the **Create Attribute Relationship** dialog box, the **Source Attribute** is **Model Name**. Set the **Related Attribute** to **Product Line**.  
  
    In the **Relationship type** list, leave the relationship type set to **Flexible** because relationships between the members might change over time. For example, a product model might eventually be moved to a different product line.  
  
4.  Click **OK**.  
  
5.  On the **File** menu, click **Save All**.  
  
## Reviewing Product Dimension Changes  
  
#### To review the Product dimension changes  
  
1.  On the **Build** menu of [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)], click **Deploy Analysis Services Tutorial**.  
  
2.  After you have received the **Deployment Completed Successfully** message, click the **Browser** tab of **Dimension Designer** for the **Product** dimension, and then click the Reconnect button on the toolbar of the designer.  
  
3.  Verify that **Product Model Lines** is selected in the **Hierarchy** list, and then expand **All Products**.  
  
    Notice that the name of the **All** member appears as **All Products**. This is because you changed the **AllMemberName** property for the hierarchy to **All Products** earlier in the lesson. Also, the members of the **Product Line** level now have user-friendly names, instead of single-letter abbreviations.  
  
## Next Task in Lesson  
[Modifying the Date Dimension](../analysis-services/lesson-3-4-modifying-the-date-dimension.md)  
  
## See Also  
[Define Named Calculations in a Data Source View &#40;Analysis Services&#41;](../analysis-services/multidimensional-models/define-named-calculations-in-a-data-source-view-analysis-services.md)  
[Create User-Defined Hierarchies](../analysis-services/multidimensional-models/user-defined-hierarchies-create.md)  
[Configure the &#40;All&#41; Level for Attribute Hierarchies](../analysis-services/multidimensional-models/database-dimensions-configure-the-all-level-for-attribute-hierarchies.md)  
  
