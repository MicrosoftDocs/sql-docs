---
title: "Copy a View of a Mining Model | Microsoft Docs"
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
# Copy a View of a Mining Model
[!INCLUDE[ssas-appliesto-sqlas](../../includes/ssas-appliesto-sqlas.md)]
  The **Mining Model Viewer** tab of Data Mining Designer in [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)] uses a separate viewer for each type of mining model. Several of the viewers have components from which you can copy the contents to the Clipboard, and from there paste the contents into a document or into image manipulation software. The following components make this functionality available:  
  
-   Cluster Diagram in the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Cluster Viewer and the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Sequence Cluster Viewer  
  
-   Decision Tree in the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Tree Viewer and the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Time Series Viewer  
  
-   State Transitions in the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Sequence Cluster Viewer  
  
-   Dependency Network in the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Association Rules Viewer, the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Naive Bayes Viewer, and the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Tree Viewer  
  
-   Mining model content, from the Node Details pane of the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Generic Content Tree Viewer  
  
 You can copy the complete representation of the mining model, or just the part that is visible in the viewer.  
  
> [!WARNING]  
>  When you copy a model using the viewer, it does not create a new model object. To create a new model, you must use either the wizard, or the Data Mining Designer,. For more information, see [Make a Copy of a Mining Model](../../analysis-services/data-mining/make-a-copy-of-a-mining-model.md).  
  
### To copy the complete model to the Clipboard  
  
1.  From the **Mining Model** list on the **Mining Model Viewer** tab, select the mining model that you want to view.  
  
2.  Select the appropriate tab, such as the **Dependency Network** tab, and then click **Copy Entire Graph** on the toolbar of that tab.  
  
### To copy the visible piece of the model to the Clipboard  
  
1.  From the **Mining Model** list on the **Mining Model Viewer** tab, select the mining model that you want to view.  
  
2.  Select the appropriate tab, such as the **Dependency Network** tab, and then zoom in or out to view the model at the level that you want.  
  
3.  Click **Copy Graph View** on the toolbar of the selected tab.  
  
### To copy the mining model content to the Clipboard  
  
1.  From the **Mining Model** list on the **Mining Model Viewer** tab, select the mining model that you want to view.  
  
2.  From the **Viewer** drop-down list, select **Microsoft Generic Content Tree Viewer**.  
  
3.  In the **Node Caption (Unique ID)** pane, click a node.  
  
4.  Right-click the **Node Details** pane and then select **Select All**.  
  
5.  Right-click the **Node Details** pane again and select **Copy**.  
  
## See Also  
 [Mining Model Viewer Tasks and How-tos](../../analysis-services/data-mining/mining-model-viewer-tasks-and-how-tos.md)  
  
  
