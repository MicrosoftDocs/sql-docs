---
title: "Rules Tab (Mining Model Viewer) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.dm.miningmodeleditor.associationrules.rules.f1"
ms.assetid: 705d5492-b58f-45d9-94d7-ed57b7025823
author: minewiskan
ms.author: owend
manager: craigg
---
# Rules Tab (Mining Model Viewer)
  Use the **Rules** pane in an association model to view the rules that the algorithm extracted from the data. Rules describe how items are related to each other, and can be used to create recommendations.  
  
 You can use the options in the viewer to filter the number of rules that are displayed in the viewer.  
  
> [!WARNING]  
>  By default, only the rules that are above the probability threshold defined in **Minimum probability** are displayed in the viewer. You cannot make this value any smaller by using the viewer, because the probability threshold for rule output is determined when the model is created. For more information, see [Microsoft Association Algorithm Technical Reference](data-mining/microsoft-association-algorithm-technical-reference.md).  
  
 **For More Information:** [Microsoft Association Algorithm](data-mining/microsoft-association-algorithm.md), [Browse a Model Using the Microsoft Association Rules Viewer](data-mining/browse-a-model-using-the-microsoft-association-rules-viewer.md)  
  
## Options  
 **Refresh viewer content**  
 Reload the mining model in the viewer.  
  
 **Mining Model**  
 Choose a mining model to view that is contained in the current mining structure. The mining model will open in its associated viewer.  
  
 **Viewer**  
 Choose a viewer to use to view the selected mining model. You can use the custom viewer for each mining model, or the **Microsoft Generic Content Tree Viewer**. You can also use plug-in viewers if available.  
  
 **Minimum probability**  
 Change this value to set the minimum probability required for a rule to be displayed in the viewer. Increasing the value for probability will reduce the number of rules that are displayed.  
  
 **Minimum importance**  
 Change this value to set the minimum importance required for a rule to be displayed in the viewer. Increasing the value for importance will reduce the number of rules that are displayed.  
  
 **Filter Rule**  
 Type a string value to filter the number of rules that appear in the viewer.  
  
 You can also type .NET regular expressions as filter criteria. For example, the following expression returns all rules that contain 'Road Bottle Cage' on the left side of the rule:  
  
 `\bRoad\b.\bBottle\b.\bCage\b.*->.*`  
  
 Note that you might need to refresh the view to see the filter criteria apply. You can also toggle the **Show long name** option on and off to refresh the list.  
  
 By default the filter criteria applies to the full name of the attribute-value combination; therefore, if you are viewing the attribute name only, it might not be obvious that the filter criteria has applied correctly. Use the **Show** dropdown list to select **Show attribute name and value**, and verify that the list of itemsets is filtered correctly.  
  
 **Show**  
 Adjust the way that the rule is displayed in the viewer. You can select one of the three following options:  
  
-   Show the attribute name and value  
  
-   Show the attribute value only  
  
-   Show the attribute name only  
  
 **Show long name**  
 Show the full name of the rule as it appears in the mining model content.  
  
 **Maximum rows**  
 Limit the number of rules that are displayed in the viewer.  
  
 **Probability**  
 This column in the chart displays the probability for each rule.  
  
 You can click the column heading to sort by probability.  
  
 **Importance**  
 This column in the chart displays the importance for each rule.  
  
 You can click the column heading to sort by importance.  
  
 **Rule**  
 This column in the chart displays the text description for each rule, according to the format specified by using the options, **Show** and **Show long name**.  
  
 You can click the column heading to sort by the text of the rule.  
  
## See Also  
 [Data Mining Algorithms &#40;Analysis Services - Data Mining&#41;](data-mining/data-mining-algorithms-analysis-services-data-mining.md)   
 [Mining Model Viewers &#40;Data Mining Model Designer&#41;](mining-model-viewers-data-mining-model-designer.md)   
 [Data Mining Model Viewers](data-mining/data-mining-model-viewers.md)  
  
  
