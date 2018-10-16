---
title: "Attribute Profiles Tab (Mining Model Viewer) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.dm.miningmodeleditor.naivebayse.profiles.f1"
ms.assetid: 17c7e7ae-273c-4a6b-9a35-e8b9b8e65999
author: minewiskan
ms.author: owend
manager: craigg
---
# Attribute Profiles Tab (Mining Model Viewer)
  Use the **Attribute Profiles** tab to see how the distribution of input values in a Naive Bayes model state contribute to each state of the outcome attribute. The distribution of values is shown as a colored histogram, all distributions presented in a tabular format, to make it easier to compare values.  
  
 **For More Information:** [Microsoft Naive Bayes Algorithm](data-mining/microsoft-naive-bayes-algorithm.md), [Browse a Model Using the Microsoft Naive Bayes Viewer](data-mining/browse-a-model-using-the-microsoft-naive-bayes-viewer.md)  
  
## Options  
 **Refresh viewer content**  
 Reload the mining model in the viewer.  
  
 **Mining Model**  
 Choose a mining model to view, from those in the current mining structure. The mining model will open in its associated viewer.  
  
 **Viewer**  
 Choose a viewer to use to explore the selected mining model. You can choose the custom viewer provided for each mining model, or the [!INCLUDE[msCoName](../includes/msconame-md.md)] Mining Content Viewer. You can also use plug-in viewers if they are available.  
  
 **Show Legend**  
 Select this option to display a key that matches each value in **States** to one of the colors used in the distribution chart.  
  
 **Histogram bars**  
 Select how many bars to include in the histogram. If more bars exist than you choose to display, the bars of highest importance are retained, and the remaining bars are grouped together into **Other**.  
  
 **Predictable**  
 Select a predictable column from the mining model.  
  
 **Attribute Profiles**  
 The table contains the following columns:  
  
|Value|Description|  
|-----------|-----------------|  
|**Attributes**|Lists the mining model columns contained within the mining model.|  
|**States**|An optional column that describes what state the color in the corresponding row of attributes represents. Add or remove by using the **Show Legend** check box.|  
|**Population**|Displays the distribution of the attribute across the whole dataset.|  
|**Column for states of predictable attribute**|Displays a column for each state of the predictable column, with each row corresponding to an input attribute in the model.|  
  
## See Also  
 [Data Mining Algorithms &#40;Analysis Services - Data Mining&#41;](data-mining/data-mining-algorithms-analysis-services-data-mining.md)   
 [Mining Model Viewers &#40;Data Mining Model Designer&#41;](mining-model-viewers-data-mining-model-designer.md)   
 [Data Mining Model Viewers](data-mining/data-mining-model-viewers.md)  
  
  
