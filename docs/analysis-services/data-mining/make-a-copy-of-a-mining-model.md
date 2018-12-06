---
title: "Make a Copy of a Mining Model | Microsoft Docs"
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
# Make a Copy of a Mining Model
[!INCLUDE[ssas-appliesto-sqlas](../../includes/ssas-appliesto-sqlas.md)]
  Creating a copy of a mining model is useful when you want to quickly create several mining models that are based on the same data. After you copy the model, you can then edit the new copy by changing parameters or adding a filter.  
  
 For example, if you have a Customers table that is linked to a table of purchases, you could create copies to generate separate mining models for each customer demographic, filtering on attributes such as age or region.  
  
 For information about how to copy the content of the model (such as the graphical representation or the model patterns) to the Clipboard for use in other programs, see [Copy a View of a Mining Model](../../analysis-services/data-mining/copy-a-view-of-a-mining-model.md).  
  
### To create a related mining model  
  
1.  In [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], in Solution Explorer, click the mining structure that contains the mining model.  
  
2.  Click the **Mining Models** tab.  
  
3.  Select the model, and right-click to open the shortcut menu.  
  
     -or-  
  
     Select the model. On the **Mining Model** menu, select **New Mining Model**.  
  
4.  Type a name for the new mining model, and select an algorithm. [!INCLUDE[clickOK](../../includes/clickok-md.md)]  
  
### To edit the filter on the copied mining model  
  
1.  Select the mining model.  
  
2.  In the **Properties** window, click the text box for the **Filter** property, and the click the build **(...)** button.  
  
3.  Change the filter conditions.  
  
     For more information about how to use the filter editor dialog boxes, see [Apply a Filter to a Mining Model](../../analysis-services/data-mining/apply-a-filter-to-a-mining-model.md).  
  
4.  In the **Properties** window, in the **AlgorithmParameters** text box, click **Setalgorithm parameters**, and change algorithm parameters, if desired.  
  
5.  [!INCLUDE[clickOK](../../includes/clickok-md.md)]  
  
## See Also  
 [Filters for Mining Models &#40;Analysis Services - Data Mining&#41;](../../analysis-services/data-mining/filters-for-mining-models-analysis-services-data-mining.md)   
 [Mining Model Tasks and How-tos](../../analysis-services/data-mining/mining-model-tasks-and-how-tos.md)   
 [Delete a Filter from a Mining Model](../../analysis-services/data-mining/delete-a-filter-from-a-mining-model.md)  
  
  
