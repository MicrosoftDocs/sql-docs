---
title: "Delete a Filter from a Mining Model | Microsoft Docs"
ms.date: 05/01/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: data-mining
ms.topic: conceptual
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# Delete a Filter from a Mining Model
[!INCLUDE[ssas-appliesto-sqlas](../../includes/ssas-appliesto-sqlas.md)]
  When you create a filter on a mining model, you can create models on a subset of the data in the data source view. Filters are also useful for testing the accuracy of the model on a subset of the original data.  
  
 However, you must delete the filter if you want to view the complete set of cases again. This procedure describes how to remove conditions on a filter, or delete the filter completely.  
  
### To delete a condition from a filter on a mining model  
  
1.  In [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], in Solution Explorer, click the mining structure that contains the mining model you want to filter.  
  
2.  Click the **Mining Models** tab.  
  
3.  Select the model, and right-click to open the shortcut menu.  
  
     -or-  
  
     Select the model. On the **Mining Model** menu, select **Set Model Filter**.  
  
4.  In the **Model Filter** dialog box, right-click the row in the grid that contains the condition you want to delete.  
  
5.  Select **Delete**.  
  
### To clear the filter on a mining model in the Filter Editor dialog box  
  
-   In the **Filter Editor** dialog box, right-click any row in the grid, and select **Delete All**.  
  
## Working with Model Filters Using the Properties Window  
 If you want to delete the whole filter, you do not need to open the filter editor dialog boxes. The filter conditions that you created are available in the **Filter** property of the mining model.  
  
> [!NOTE]  
>  You can view the properties of a mining model in [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], but not in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)].  
  
#### To clear the filter on a mining model in Solution Explorer  
  
1.  In Solution Explorer, click the mining model that contains the filter.  
  
2.  In the **Properties** window, right-click the filter text in the **Filter** property, and select **Select All**.  
  
3.  Press the Backspace or Delete key.  
  
## See Also  
 [Drill Through to Case Data from a Mining Model](../../analysis-services/data-mining/drill-through-to-case-data-from-a-mining-model.md)   
 [Mining Model Tasks and How-tos](../../analysis-services/data-mining/mining-model-tasks-and-how-tos.md)   
 [Filters for Mining Models &#40;Analysis Services - Data Mining&#41;](../../analysis-services/data-mining/filters-for-mining-models-analysis-services-data-mining.md)  
  
  
