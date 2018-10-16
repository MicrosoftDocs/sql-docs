---
title: "Adding Attributes to Dimensions | Microsoft Docs"
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
# Lesson 2-3 - Adding Attributes to Dimensions
[!INCLUDE[ssas-appliesto-sqlas](../includes/ssas-appliesto-sqlas.md)]

Now that you have defined dimensions, you can populate them with attributes that represent each data element in the dimension. Attributes are commonly based on fields from a data source view. When adding attributes to a dimension, you can include fields from any table in the data source view.  
  
In this task, you will use Dimension Designer to add attributes to the Customer and Product dimensions. The Customer dimension will include attributes based on fields from both the Customer and Geography tables.  
  
## Adding Attributes to the Customer Dimension  
  
#### To add attributes  
  
1.  Open Dimension Designer for the Customer dimension. To do this, double-click the **Customer** dimension in the **Dimensions** node of Solution Explorer.  
  
2.  In the **Attributes** pane, notice the Customer Key and Geography Key attributes that were created by the Cube Wizard.  
  
3.  On the toolbar of the **Dimension Structure** tab, make sure the Zoom icon to view the tables in the **Data Source View** pane is set at 100 percent.  
  
4.  Drag the following columns from the **Customer** table in the **Data Source View** pane to the **Attributes** pane:  
  
    -   **BirthDate**  
  
    -   **MaritalStatus**  
  
    -   **Gender**  
  
    -   **EmailAddress**  
  
    -   **YearlyIncome**  
  
    -   **TotalChildren**  
  
    -   **NumberChildrenAtHome**  
  
    -   **EnglishEducation**  
  
    -   **EnglishOccupation**  
  
    -   **HouseOwnerFlag**  
  
    -   **NumberCarsOwned**  
  
    -   **Phone**  
  
    -   **DateFirstPurchase**  
  
    -   **CommuteDistance**  
  
5.  Drag the following columns from the **Geography** table in the **Data Source View** pane to the **Attributes** pane:  
  
    -   **City**  
  
    -   **StateProvinceName**  
  
    -   **EnglishCountryRegionName**  
  
    -   **PostalCode**  
  
6.  On the File menu, click **Save All**.  
  
## Adding Attributes to the Product Dimension  
  
#### To add attributes  
  
1.  Open Dimension Designer for the Product dimension. Double-click the **Product** dimension in Solution Explorer.  
  
2.  In the **Attributes** pane, notice the Product Key attribute that was created by the Cube Wizard.  
  
3.  On the toolbar of the **Dimension Structure** tab, make sure the Zoom icon to view the tables in the **Data Source View** pane is set at 100 percent.  
  
4.  Drag the following columns from the **Product** table in the **Data Source View** pane to the **Attributes** pane:  
  
    -   **StandardCost**  
  
    -   **Color**  
  
    -   **SafetyStockLevel**  
  
    -   **ReorderPoint**  
  
    -   **ListPrice**  
  
    -   **Size**  
  
    -   **SizeRange**  
  
    -   **Weight**  
  
    -   **DaysToManufacture**  
  
    -   **ProductLine**  
  
    -   **DealerPrice**  
  
    -   **Class**  
  
    -   **Style**  
  
    -   **ModelName**  
  
    -   **StartDate**  
  
    -   **EndDate**  
  
    -   **Status**  
  
5.  On the File menu, click **Save All**.  
  
## Next Task in Lesson  
[Reviewing Cube and Dimension Properties](../analysis-services/lesson-2-4-reviewing-cube-and-dimension-properties.md)  
  
## See Also  
[Dimension Attribute Properties Reference](../analysis-services/multidimensional-models/dimension-attribute-properties-reference.md)  
  
  
  
