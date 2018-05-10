---
title: "Manually Edit a Prediction Query | Microsoft Docs"
ms.date: 05/08/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: data-mining
ms.topic: conceptual
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# Manually Edit a Prediction Query
[!INCLUDE[ssas-appliesto-sqlas](../../includes/ssas-appliesto-sqlas.md)]
  After you have designed a query by using the Prediction Query Builder, you can modify the query by switching to Query Text view on the **Mining Model Prediction** tab of Data Mining Designer. A text editor appears at the bottom of the screen to display the query that the query builder created.  
  
 Switching to Query Text view is useful for making additions to the query. For example, you can add a WHERE clause or ORDER BY clause.  
  
 Use the grid in the Prediction Query Builder to insert the names of objects and columns and set up the syntax for individual prediction functions, and then switch to manual editing mode to change parameter values.  
  
> [!NOTE]  
>  If you switch back to **Design** view from **Query Text** view, any changes that you made in **Query Text** view will be lost.  
  
### Modify a query  
  
1.  On the **Mining Model Prediction** tab in Data Mining Designer in [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], click **SQL**.  
  
     The grid at the bottom of the screen is replaced by a text editor that contains the query. You can type changes to the query in this editor.  
  
2.  To run the query, on the **Mining Model** menu, select **Result**, or click the button to switch to query results.  
  
    > [!NOTE]  
    >  If the query that you have created is invalid, the Results window does not display an error and does not display any results. Click the **Design** button, or select **Design** or **Query** from the **Mining Model** menu to correct the problem and run the query again.  
  
## See Also  
 [Data Mining Queries](../../analysis-services/data-mining/data-mining-queries.md)   
 [Prediction Query Builder &#40;Data Mining&#41;](http://msdn.microsoft.com/library/12900d49-db88-48bb-a5f4-0a9a172bc126)   
 [Lesson 6: Creating and Working with Predictions &#40;Basic Data Mining Tutorial&#41;](http://msdn.microsoft.com/library/b213cb58-2c40-4c89-b08b-d3c36a4afad3)  
  
  
