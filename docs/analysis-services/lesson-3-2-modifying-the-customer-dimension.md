---
title: "Modifying the Customer Dimension | Microsoft Docs"
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
# Lesson 3-2 - Modifying the Customer Dimension
[!INCLUDE[ssas-appliesto-sqlas](../includes/ssas-appliesto-sqlas.md)]

There are many different ways that you can increase the usability and functionality of the dimensions in a cube. In the tasks in this topic, you modify the Customer dimension.  
  
## Renaming Attributes  
You can change attribute names with the **Dimension Structure** tab of Dimension Designer.  
  
#### To rename an attribute  
  
1.  Switch to **Dimension Designer** for the Customer dimension in [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)]. To do this, double-click the **Customer** dimension in the **Dimensions** node of Solution Explorer.  
  
2.  In the **Attributes** pane, right-click **English Country Region Name**, and then click **Rename**. Change the name of the attribute to **Country-Region**.  
  
3.  Change the names of the following attributes in the same manner:  
  
    -   **English Education** attribute - change to **Education**  
  
    -   **English Occupation** attribute - change to **Occupation**  
  
    -   **State Province Name** attribute - change to **State-Province**  
  
4.  On the **File** menu, click **Save All**.  
  
## Creating a Hierarchy  
You can create a new hierarchy by dragging an attribute from the **Attributes** pane to the **Hierarchies** pane.  
  
#### To create a hierarchy  
  
1.  Drag the **Country-Region** attribute from the **Attributes** pane into the **Hierarchies** pane.  
  
2.  Drag the **State-Province** attribute from the **Attributes** pane into the **<new level>** cell in the **Hierarchies** pane, underneath the **Country-Region** level.  
  
3.  Drag the **City** attribute from the **Attributes** pane into the **<new level>** cell in the **Hierarchies** pane, underneath the **State-Province** level.  
  
4.  In the **Hierarchies** pane of the **Dimension Structure** tab, right-click the title bar of the **Hierarchy** hierarchy, select **Rename**, and then type **Customer Geography**.  
  
    The name of the hierarchy is now **Customer Geography**.  
  
5.  On the **File** menu, click **Save All**.  
  
## Adding a Named Calculation  
You can add a named calculation, which is a SQL expression that is represented as a calculated column, to a table in a data source view. The expression appears and behaves as a column in the table. Named calculations let you extend the relational schema of existing tables in a data source view without modifying the table in the underlying data source. For more information, see [Define Named Calculations in a Data Source View &#40;Analysis Services&#41;](../analysis-services/multidimensional-models/define-named-calculations-in-a-data-source-view-analysis-services.md)  
  
#### To add a named calculation  
  
1.  Open the **[!INCLUDE[ssSampleDBCoShort](../includes/sssampledbcoshort-md.md)] DW 2012** data source view by double-clicking it in the **Data Source Views** folder in Solution Explorer.  
  
2.  In the **Tables** pane on the left, right-click **Customer**, and then click **New Named Calculation**.  
  
3.  In the **Create Named Calculation** dialog box, type **FullName** in the **Column name** box, and then type or copy and paste the following **CASE** statement in the **Expression** box:  
  
    ```  
    CASE  
       WHEN MiddleName IS NULL THEN  
       FirstName + ' ' + LastName  
       ELSE  
       FirstName + ' ' + MiddleName + ' ' + LastName  
    END  
    ```  
  
    The **CASE** statement concatenates the **FirstName**, **MiddleName**, and **LastName** columns into a single column that you will use in the Customer dimension as the displayed name for the **Customer** attribute.  
  
4.  Click **OK**, and then expand **Customer** in the **Tables** pane.  
  
    The **FullName** named calculation appears in the list of columns in the Customer table, with an icon that indicates that it is a named calculation.  
  
5.  On the **File** menu, click **Save All**.  
  
6.  In the **Tables** pane, right-click **Customer**, and then click **Explore Data**.  
  
7.  Review the last column in the **Explore Customer Table** view.  
  
    Notice that the **FullName** column appears in the data source view, correctly concatenating data from several columns from the underlying data source and without modifying the original data source.  
  
8.  Close the **Explore Customer Table** tab.  
  
## Using the Named Calculation for Member Names  
After you have created a named calculation in the data source view, you can use the named calculation as a property of an attribute.  
  
#### To use the named calculation for member names  
  
1.  Switch to Dimension Designer for the Customer dimension.  
  
2.  In the **Attributes** pane of the **Dimension Structure** tab, click the **Customer Key** attribute.  
  
3.  Open the Properties window and click the **Auto Hide** button on the title bar so that it stays open.  
  
4.  In the **Name** property field, type **Full Name**.  
  
5.  Click in the **NameColumn** property field at the bottom, and then click the browse (**...**) button to open the **Name Column** dialog box.  
  
6.  Select **FullName** at the bottom of the **Source column** list, and then click **OK**.  
  
7.  In the Dimensions Structure tab, drag the **Full Name** attribute from the **Attributes** pane into the **<new level>** cell in the **Hierarchies** pane, underneath the **City** level.  
  
8.  On the **File** menu, click **Save All**.  
  
## Defining Display Folders  
You can use display folders to group user and attribute hierarchies into folder structures to increase usability.  
  
#### To define display folders  
  
1.  Open the **Dimension Structure** tab for the Customer dimension.  
  
2.  In the **Attributes** pane, select the following attributes by holding down the CTRL key while clicking each of them:  
  
    -   **City**  
  
    -   **Country-Region**  
  
    -   **Postal Code**  
  
    -   **State-Province**  
  
3.  In the Properties window, click the **AttributeHierarchyDisplayFolder** property field at the top (you might need to point to it to see the full name), and then type **Location**.  
  
4.  In the **Hierarchies** pane, click **Customer Geography**, and then in the Properties window on the right, select **Location** as the value of the **DisplayFolder** property.  
  
5.  In the **Attributes** pane, select the following attributes by holding down the CTRL key while clicking each of them:  
  
    -   **Commute Distance**  
  
    -   **Education**  
  
    -   **Gender**  
  
    -   **House Owner Flag**  
  
    -   **Marital Status**  
  
    -   **Number Cars Owned**  
  
    -   **Number Children At Home**  
  
    -   **Occupation**  
  
    -   **Total Children**  
  
    -   **Yearly Income**  
  
6.  In the Properties window, click the **AttributeHierarchyDisplayFolder** property field at the top, and then type **Demographic**.  
  
7.  In the **Attributes** pane, select the following attributes by holding down the CTRL key while clicking each of them:  
  
    -   **Email Address**  
  
    -   **Phone**  
  
8.  In the Properties window, click the **AttributeHierarchyDisplayFolder** property field and type **Contacts**.  
  
9. On the **File** menu, click **Save All**.  
  
## Defining Composite KeyColumns  
The **KeyColumns** property contains the column or columns that represent the key for the attribute. In this lesson, you create a composite key for the **City** and **State-Province** attributes. Composite keys can be helpful when you need to uniquely identify an attribute. For example, when you define attribute relationships later in this tutorial, a **City** attribute must uniquely identify a **State-Province** attribute. However, there could be several cities with the same name in different states. For this reason, you will create a composite key that is composed of the **StateProvinceName** and **City** columns for the **City** attribute. For more information, see [Modify the KeyColumn Property of an Attribute](../analysis-services/multidimensional-models/attribute-properties-modify-the-keycolumn-property.md).  
  
#### To define composite KeyColumns for the City attribute  
  
1.  Open the **Dimension Structure** tab for the Customer dimension.  
  
2.  In the **Attributes** pane, click the **City** attribute.  
  
3.  In the **Properties** window, click in the **KeyColumns** field near the bottom, and then click the browse (**...**) button.  
  
4.  In the **Key Columns** dialog box, in the **Available Columns** list, select the column **StateProvinceName**, and then click the **>** button.  
  
    The **City** and **StateProvinceName** columns are now displayed in the **Key Columns** list.  
  
5.  Click **OK**.  
  
6.  To set the **NameColumn** property of the **City** attribute, click the **NameColumn** field in the Properties window, and then click the browse (**...**) button.  
  
7.  In the **Name Column** dialog box, in the **Source column** list, select **City**, and then click **OK**.  
  
8.  On the **File** menu, click **Save All**.  
  
#### To define composite KeyColumns for the State-Province attribute  
  
1.  Make sure the **Dimension Structure** tab for the Customer dimension is open.  
  
2.  In the **Attributes** pane, click the **State-Province** attribute.  
  
3.  In the **Properties** window, click in the **KeyColumns** field, and then click the browse (**...**) button.  
  
4.  In the **Key Columns** dialog box, in the **Available Columns** list, select the column **EnglishCountryRegionName**, and then click the **>** button.  
  
    The **EnglishCountryRegionName** and **StateProvinceName** columns are now displayed in the **Key Columns** list.  
  
5.  Click **OK**.  
  
6.  To set the **NameColumn** property of the **State-Province** attribute, click the **NameColumn** field in the Properties window, and then click the browse (**...**) button.  
  
7.  In the **Name Column** dialog box, in the **Source column** list, select **StateProvinceName**, and then click **OK**.  
  
8.  On the **File** menu, click **Save All**.  
  
## Defining Attribute Relationships  
If the underlying data supports it, you should define attribute relationships between attributes. Defining attribute relationships speeds up dimension, partition, and query processing. For more information, see [Define Attribute Relationships](../analysis-services/multidimensional-models/attribute-relationships-define.md) and [Attribute Relationships](../analysis-services/multidimensional-models-olap-logical-dimension-objects/attribute-relationships.md).  
  
#### To define attribute relationships  
  
1.  In the **Dimension Designer** for the Customer dimension, click the **Attribute Relationships** tab. You might need to wait.  
  
2.  In the diagram, right-click the **City** attribute, and then click **New Attribute Relationship**.  
  
3.  In the **Create Attribute Relationship** dialog box, the **Source Attribute** is **City**. Set the **Related Attribute** to **State-Province**.  
  
4.  In the **Relationship type** list, set the relationship type to **Rigid**.  
  
    The relationship type is **Rigid** because relationships between the members will not change over time. For example, it would be unusual for a city to become part of a different state or province.  
  
5.  [!INCLUDE[clickOK](../includes/clickok-md.md)]  
  
6.  In the diagram, right-click the **State-Province** attribute and then select **New Attribute Relationship**.  
  
7.  In the **Create Attribute Relationship** dialog box, the **Source Attribute** is **State-Province**. Set the **Related Attribute** to **Country-Region**.  
  
8.  In the **Relationship type** list, set the relationship type to **Rigid**.  
  
9. Click **OK**.  
  
10. On the **File** menu, click **Save All**.  
  
## Deploying Changes, Processing the Objects, and Viewing the Changes  
After you have changed attributes and hierarchies, you must deploy the changes and reprocess the related objects before you can view the changes.  
  
#### To deploy the changes, process the objects, and view the changes  
  
1.  On the **Build** menu of [!INCLUDE[ssBIDevStudio](../includes/ssbidevstudio-md.md)], click **Deploy Analysis Services Tutorial**.  
  
2.  After you receive the **Deployment Completed Successfully** message, click the **Browser** tab of Dimension Designer for the Customer dimension, and then click the Reconnect button on the left side of the toolbar of the designer.  
  
3.  Verify that **Customer Geography** is selected in the **Hierarchy** list, and then in the browser pane, expand **All**, expand **Australia**, expand **New South Wales**, and then expand **Coffs Harbour**.  
  
    The browser displays the customers in the city.  
  
4.  Switch to **Cube Designer** for the [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] Tutorial cube. To do this, double-click the **Analysis Services Tutorial** cube in the **Cubes** node of **Solution Explorer**.  
  
5.  Click the **Browser** tab, and then click the Reconnect button on the toolbar of the designer.  
  
6.  In the **Measure Group** pane, expand **Customer**.  
  
    Notice that instead of a long list of attributes, only the display folders and the attributes that do not have display folder values appear underneath Customer.  
  
7.  On the **File** menu, click **Save All**.  
  
## Next Task in Lesson  
[Modifying the Product Dimension](../analysis-services/lesson-3-3-modifying-the-product-dimension.md)  
  
## See Also  
[Dimension Attribute Properties Reference](../analysis-services/multidimensional-models/dimension-attribute-properties-reference.md)  
[Remove an Attribute from a Dimension](../analysis-services/multidimensional-models/attribute-properties-remove-an-attribute-from-a-dimension.md)  
[Rename an Attribute](../analysis-services/multidimensional-models/attribute-properties-rename-an-attribute.md)  
[Define Named Calculations in a Data Source View &#40;Analysis Services&#41;](../analysis-services/multidimensional-models/define-named-calculations-in-a-data-source-view-analysis-services.md)  
  
