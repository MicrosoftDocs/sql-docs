---
title: "Filter an Itemset in an Association Rules Model | Microsoft Docs"
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
# Filter an Itemset in an Association Rules Model
[!INCLUDE[ssas-appliesto-sqlas](../../includes/ssas-appliesto-sqlas.md)]
  In [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)], you can filter the itemsets that are displayed in the **Itemsets** tab of the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Association Rules Viewer.  
  
### To filter an itemset  
  
1.  On the **Mining Model Viewer** tab of Data Mining Designer in [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], click the **Itemsets** tab of the **Association Rules Viewer**.  
  
2.  Enter a rule condition in the **Filter itemset** box. For example, a rule condition might be "Touring-1000 = existing"  
  
3.  Click **Enter**.  
  
 The itemsets are now filtered to display only those itemsets that contain the selected items. The box is not case-sensitive. Filters are stored in memory so that you can select an old filter from the list.  
  
## See Also  
 [Mining Model Viewer Tasks and How-tos](../../analysis-services/data-mining/mining-model-viewer-tasks-and-how-tos.md)  
  
  
