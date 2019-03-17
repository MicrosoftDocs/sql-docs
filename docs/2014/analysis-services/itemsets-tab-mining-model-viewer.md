---
title: "Itemsets Tab (Mining Model Viewer) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.dm.miningmodeleditor.associationrules.itemsets.f1"
ms.assetid: 95b2b805-b142-4064-9c80-4b1b3fe2fe63
author: minewiskan
ms.author: owend
manager: craigg
---
# Itemsets Tab (Mining Model Viewer)
  You can use the **Itemsets** pane to view the frequent itemsets that an association rules mining model contains. Because an association model can contain many itemsets, controls are provided in the viewer to help you filter the itemsets that are displayed in the viewer.  
  
 **For More Information:** [Microsoft Association Algorithm](data-mining/microsoft-association-algorithm.md), [Browse a Model Using the Microsoft Association Rules Viewer](data-mining/browse-a-model-using-the-microsoft-association-rules-viewer.md)  
  
## Options  
 **Refresh viewer content**  
 Reload the mining model in the viewer.  
  
 **Mining Model**  
 Choose a mining model to view that is contained in the current mining structure. The mining model will open in its associated viewer.  
  
 **Viewer**  
 Choose a viewer to use to view the selected mining model. You can use either the custom viewer for association models, or the [!INCLUDE[msCoName](../includes/msconame-md.md)] Generic Content Tree viewer. You can also use plug-in viewers if available.  
  
 **Minimum support**  
 Change this value to set the minimum support that an itemset must contain to appear in the viewer. The default value that is displayed when you first open the model is calculated by the model, but you can change it to see more or fewer itemsets.  
  
 **Minimum itemset size**  
 Change this value to set the minimum number of items that must be included in an itemset before the itemset can be displayed in the viewer.  
  
 **Filter Itemset**  
 Type a string value to filter the number of itemsets that appear in the viewer.  
  
 You can also type .NET regular expressions as filter criteria. For example, the following expression returns all itemsets that contain 'Road Bottle Cage':  
  
 `\bRoad\b.\bBottle\b.\bCage\b.*`  
  
 Note that you might need to refresh the view to see the filter criteria apply. You can also toggle the **Show long name** option on and off to refresh the list.  
  
 By default the filter criteria applies to the full name of the attribute-value combination; therefore, if you are viewing the attribute name only, it might not be obvious that the filter criteria has applied correctly. Use the **Show** dropdown list to select **Show attribute name and value**, and verify that the list of itemsets is filtered correctly.  
  
 **Show**  
 Adjust how the itemset is displayed in the viewer. You can select one of the three following options:  
  
-   Show attribute name and value  
  
-   Show attribute value only  
  
-   Show attribute name only  
  
 Note that this option does not requery the model; it only filters the attributes or values that are displayed.  
  
 **Show long name**  
 Select this option to display the full name of the itemset as it appears in the mining model content.  
  
 **Maximum rows**  
 Limit the number of itemsets that are displayed in the viewer. By default, itemsets are ordered by support in descending order, so lowering this value restricts the list to the most common itemsets.  
  
 **Support**  
 Displays the support for each itemset.  
  
 **Size**  
 Displays the number of items that exist in each itemset.  
  
 **Itemset**  
 Displays the description of each itemset. By default, itemsets are presented as a comma-delimited list of attributes and their values. You can change the way they are displayed by using the **Show** option.  
  
## See Also  
 [Data Mining Algorithms &#40;Analysis Services - Data Mining&#41;](data-mining/data-mining-algorithms-analysis-services-data-mining.md)   
 [Mining Model Viewers &#40;Data Mining Model Designer&#41;](mining-model-viewers-data-mining-model-designer.md)   
 [Data Mining Model Viewers](data-mining/data-mining-model-viewers.md)  
  
  
