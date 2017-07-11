---
title: "Modifying Measures | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
ms.tgt_pltfrm: ""
ms.topic: "get-started-article"
applies_to: 
  - "SQL Server 2016"
ms.assetid: 7bd48810-15ce-45ff-862b-372d08606995
caps.latest.revision: 14
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# Lesson 3-1 - Modifying Measures
You can use the **FormatString** property to define formatting settings that control how measures are displayed to users. In this task, you specify formatting properties for the currency and percentage measures in the [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] Tutorial cube.  
  
### To modify the measures of the cube  
  
1.  Switch to the **Cube Structure** tab of Cube Designer for the [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] Tutorial cube, expand the **Internet Sales** measure group in the **Measures** pane, right-click **Order Quantity**, and then click **Properties**.  
  
2.  In the Properties window, click the **Auto Hide** pushpin icon to pin the Properties window open.  
  
    It is easier to change properties for several items in the cube when the Properties window remains open.  
  
3.  In the Properties window, click the **FormatString** list, and then type **#,#**.  
  
4.  On the toolbar of the **Cube Structure** tab, click the **Show Measures Grid** icon on the left.  
  
    The grid view lets you select multiple measures at the same time.  
  
5.  Select the following measures. You can select multiple measures by clicking each while holding down the CTRL key:  
  
    -   **Unit Price**  
  
    -   **Extended Amount**  
  
    -   **Discount Amount**  
  
    -   **Product Standard Cost**  
  
    -   **Total Product Cost**  
  
    -   **Sales Amount**  
  
    -   **Tax Amt**  
  
    -   **Freight**  
  
6.  In the Properties window, in the **FormatString** list, select **Currency**.  
  
7.  In the drop-down list at the top of the Properties window (right below the title bar), select the measure **Unit Price Discount Pct**, and then select **Percent** in the **FormatString** list.  
  
8.  In the Properties window, change the **Name** property for the **Unit Price Discount Pct** measure to **Unit Price Discount Percentage**.  
  
9. In the **Measures** pane, click **Tax Amt** and change the name of this measure to **Tax Amount**.  
  
10. In the Properties window, click the **Auto Hide** icon to hide the Properties window, and then click **Show Measures Tree** on the toolbar of the **Cube Structure** tab.  
  
11. On the **File** menu, click **Save All**.  
  
## Next Task in Lesson  
[Modifying the Customer Dimension](../analysis-services/lesson-3-2-modifying-the-customer-dimension.md)  
  
## See Also  
[Define Database Dimensions](../analysis-services/multidimensional-models/define-database-dimensions.md)  
[Configure Measure Properties](../analysis-services/multidimensional-models/configure-measure-properties.md)  
  
  
  
