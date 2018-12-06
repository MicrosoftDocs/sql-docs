---
title: "Browse a Model Using the Microsoft Association Rules Viewer | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "itemsets [Analysis Services]"
  - "mining models [Analysis Services], associations"
  - "mining model content, viewing"
  - "rules [Data Mining]"
  - "Association Rules Viewer [Analysis Services]"
  - "market basket analysis [Analysis Services]"
  - "associations [Analysis Services]"
  - "Microsoft Association Rules Viewer"
  - "dependencies [Analysis Services]"
ms.assetid: 538fc01b-8eb1-467a-9b66-3cd57cf7489f
author: minewiskan
ms.author: owend
manager: craigg
---
# Browse a Model Using the Microsoft Association Rules Viewer
  The [!INCLUDE[msCoName](../../includes/msconame-md.md)] Association Rules Viewer in [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] displays mining models that are built with the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Association algorithm. The [!INCLUDE[msCoName](../../includes/msconame-md.md)] Association algorithm is an association algorithm for use in creating data mining models that you can use for market basket analysis. For more information about this algorithm, see [Microsoft Association Algorithm](microsoft-association-algorithm.md).  
  
 Following are the primary reasons for using the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Association algorithm:  
  
-   To find itemsets that describe items that are typically found together in a transaction.  
  
-   To discover rules that predict the presence of other items in a transaction based on existing items.  
  
> [!NOTE]  
>  To view detailed information about the equations used in the model and the patterns that were discovered, use the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Generic Content Tree viewer. For more information, see [Browse a Model Using the Microsoft Generic Content Tree Viewer](browse-a-model-using-the-microsoft-generic-content-tree-viewer.md) or [Microsoft Generic Content Tree Viewer &#40;Data Mining&#41;](../microsoft-generic-content-tree-viewer-data-mining.md).  
  
 For a walkthrough of how to create, explore, and use an association mining model, see [Lesson 3: Building a Market Basket Scenario &#40;Intermediate Data Mining Tutorial&#41;](../../tutorials/lesson-3-building-a-market-basket-scenario-intermediate-data-mining-tutorial.md).  
  
##  <a name="BKMK_ViewerTabs"></a> Viewer Tabs  
 When you browse a mining model in [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)], the model is displayed on the **Mining Model Viewer** tab of Data Mining Designer in the appropriate viewer for the model. The [!INCLUDE[msCoName](../../includes/msconame-md.md)] Association Rules Viewer includes the following tabs:  
  
-   [Itemsets](#BKMK_Itemsets)  
  
-   [Rules](#BKMK_Rules)  
  
-   [Dependency Net](#BKMK_Dependency)  
  
 Each tab contains the **Show long name** check box, which you can use to show or hide the table from which the itemset originates in the rule or itemset.  
  
###  <a name="BKMK_Itemsets"></a> Itemsets  
 The **Itemsets** tab displays the list of itemsets that the model identified as frequently found together. The tab displays a grid with the following columns: **Support**, **Size**, and **Itemset**. For more information about support, see [Microsoft Association Algorithm](microsoft-association-algorithm.md). The **Size** column displays the number of items in the itemset. The **Itemset** column displays the actual itemset that the model discovered. You can control the format of the itemset by using the **Show** list, which you can set to the following options:  
  
-   **Show attribute name and value**  
  
-   **Show attribute value only**  
  
-   **Show attribute name only**  
  
 You can filter the number of itemsets that are displayed in the tab by using **Minimum support** and **Minimum itemset size**. You can limit the number of displayed itemsets even more by using **Filter Itemset** and entering an itemset characteristic that must exist. For example, if you type "Water Bottle = existing", you can limit the itemsets to only those that contain a water bottle. The **Filter Itemset** option also displays a list of the filters that you have used previously.  
  
 You can sort the rows in the grid by clicking a column heading.  
  
 [Back to Top](#BKMK_ViewerTabs)  
  
###  <a name="BKMK_Rules"></a> Rules  
 The **Rules** tab displays the rules that the association algorithm discovered. The **Rules** tab includes a grid that contains the following columns: **Probability**, **Importance**, and **Rule**. The probability describes how likely the result of a rule is to occur. The importance is designed to measure the usefulness of a rule. Although the probability that a rule will occur may be high, the usefulness of the rule may in itself be unimportant. The importance column addresses this. For example, if every itemset contains a specific state of an attribute, a rule that predicts state is trivial, even though the probability is very high. The greater the importance, the more important the rule is.  
  
 You can use **Minimum probability** and **Minimum importance** to filter the rules, similar to the filtering you can do on the **Itemsets** tab. You can also use **Filter Rule** to filter a rule based on the states of the attributes that it contains.  
  
 You can sort the rows in the grid by clicking a column heading.  
  
 [Back to Top](#BKMK_ViewerTabs)  
  
###  <a name="BKMK_Dependency"></a> Dependency Net  
 The **Dependency Net** tab includes a dependency network viewer. Each node in the viewer represents an item, such as "state = WA". The arrow between nodes represents the association between items. The direction of the arrow dictates the association between the items according to the rules that the algorithm discovered. For example, if the viewer contains three items, A, B, and C, and C is predicted by A and B, if you select node C, two arrows point toward node C - A to C and B to C.  
  
 The slider at the left of the viewer acts as a filter that is tied to the probability of the rules. Lowering the slider shows only the strongest links.  
  
 [Back to Top](#BKMK_ViewerTabs)  
  
## See Also  
 [Microsoft Association Algorithm](microsoft-association-algorithm.md)   
 [Mining Model Viewer Tasks and How-tos](mining-model-viewer-tasks-and-how-tos.md)   
 [Mining Model Viewer Tasks and How-tos](mining-model-viewer-tasks-and-how-tos.md)   
 [Data Mining Tools](data-mining-tools.md)   
 [Data Mining Model Viewers](data-mining-model-viewers.md)  
  
  
