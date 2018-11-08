---
title: "Modifying Default Table Names | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
ms.assetid: ddd97483-a76d-43c1-8b40-fc7cc57fb0c2
author: minewiskan
ms.author: owend
manager: craigg
---
# Modifying Default Table Names
  You can change the value of the **FriendlyName** property for objects in the data source view to make them easier to notice and use.  
  
 In the following task, you will change the friendly name of each table in the data source view by removing the "**Dim**" and "**Fact**" prefixes from these tables. This will make the cube and dimension objects (that you will define in the next lesson) easier to notice and use.  
  
> [!NOTE]  
>  You can also change the friendly names of columns, define calculated columns, and join tables or views in the data source view to make them easier to use.  
  
### To modify the default name of a table  
  
1.  In the **Tables** pane of **Data Source View Designer**, right-click the **FactInternetSales** table, and then click **Properties**.  
  
2.  If the Properties window on the right side of the Microsoft Visual Studio window is not displayed, click the **Auto Hide** button on the title bar of the Properties window so that this window remains visible.  
  
     It is easier to change the properties for each table in the data source view when the Properties window remains open. If you do not pin the window open by using the **Auto Hide** button, the window will close when you click a different object in the **Diagram** pane.  
  
3.  Change the **FriendlyName** property for the **FactInternetSales** object to *`InternetSales`*.  
  
     When you click away from the cell for the **FriendlyName** property, the change is applied. In the next lesson, you will define a measure group that is based on this fact table. The name of the fact table will be InternetSales instead of FactInternetSales because of the change you made in this lesson.  
  
4.  Click **DimProduct** in the **Tables** pane. In the Properties window, change the **FriendlyName** property to *`Product`*.  
  
5.  Change the **FriendlyName** property of each remaining table in the data source view in the same way, to remove the "**Dim**" prefix.  
  
6.  When you have finished, click the **Auto Hide** button to hide the Properties window again.  
  
7.  On the **File** menu, or on the toolbar of [!INCLUDE[ssBIDevStudio](../includes/ssbidevstudio-md.md)], click **Save All** to save the changes you have made to this point in the [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] Tutorial project. You can stop the tutorial here if you want and resume it later.  
  
## Next Lesson  
 [Lesson 2: Defining and Deploying a Cube](lesson-2-defining-and-deploying-a-cube.md)  
  
## See Also  
 [Data Source Views in Multidimensional Models](multidimensional-models/data-source-views-in-multidimensional-models.md)   
 [Change Properties in a Data Source View &#40;Analysis Services&#41;](multidimensional-models/change-properties-in-a-data-source-view-analysis-services.md)  
  
  
