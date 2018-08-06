---
title: "Find a Specific Node in a Dependency Network | Microsoft Docs"
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
# Find a Specific Node in a Dependency Network
[!INCLUDE[ssas-appliesto-sqlas](../../includes/ssas-appliesto-sqlas.md)]
  A dependency network in a [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] mining model can contain many nodes, making it difficult to locate the data you are interested in. To solve this problem, you can use the **Find Node** dialog box on the **Dependency Network** tab of Data Mining Designer to search for a specific node.  
  
### To find a specific node in a dependency network  
  
1.  On the **Mining Model Viewer** tab of **Data Mining Designer** in [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], click **Find Node** on the toolbar of the **Dependency Network** tab of the mining model viewer.  
  
     The **Find Node** dialog box opens.  
  
2.  In the **Node name contains** box, enter part of the name of the node for which you want to search.  
  
     The list of nodes is filtered to display only those nodes that contain part of the search path.  
  
3.  Select the correct node from the list, and then click **OK**.  
  
## See Also  
 [Mining Model Viewer Tasks and How-tos](../../analysis-services/data-mining/mining-model-viewer-tasks-and-how-tos.md)  
  
  
