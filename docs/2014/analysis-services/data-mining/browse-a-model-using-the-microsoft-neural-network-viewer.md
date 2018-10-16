---
title: "Browse a Model Using the Microsoft Neural Network Viewer | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "mining model content, viewing"
  - "classification mining model [Analysis Services]"
  - "Microsoft Neural Network Viewer"
  - "regression algorithms [Analysis Services]"
  - "Neural Network Viewer [Analysis Services]"
  - "neural network model [Analysis Services]"
ms.assetid: 2343d746-c4f4-499b-9d3c-17d63310a9a3
author: minewiskan
ms.author: owend
manager: craigg
---
# Browse a Model Using the Microsoft Neural Network Viewer
  The [!INCLUDE[msCoName](../../includes/msconame-md.md)] Neural Network Viewer in [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] displays mining models that are built with the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Neural Network algorithm. The [!INCLUDE[msCoName](../../includes/msconame-md.md)] Neural Network algorithm creates classification and regression mining models that can analyze multiple inputs and outputs, and is very useful for open-ended analyses and exploration. For more information about this algorithm, see [Microsoft Neural Network Algorithm](microsoft-neural-network-algorithm.md).  
  
 When you explore a model using the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Neural Network Viewer, you typically pick some target attribute and state, and then use the viewer to see how input attributes affect the outcome  
  
 For example, suppose you know these facts about a class of potential customers:  
  
-   Middle aged (40 to 50 years old).  
  
-   Owns a home.  
  
-   Has two children who still live at home.  
  
 How can you correlate these attributes with the likelihood that the customer will make a purchase?  
  
 By building a neural network model using purchasing behavior as the target outcome, you can explore multiple combinations on customer attributes, such as high income, and discover which combination of attributes is most likely to influence purchasing behavior. For example, you might discover that the determining factor is the distance that they commute to work.  
  
 If you need to more view detailed information, such as the equations that represent each pattern that was discovered, you can switch views and use the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Generic Content Tree viewer. For more information, see [Browse a Model Using the Microsoft Generic Content Tree Viewer](browse-a-model-using-the-microsoft-generic-content-tree-viewer.md) or [Microsoft Generic Content Tree Viewer &#40;Data Mining&#41;](../microsoft-generic-content-tree-viewer-data-mining.md).  
  
##  <a name="BKMK_ViewerTabs"></a> Viewer Tabs  
 When you browse a mining model in [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)], the model is displayed on the **Mining Model Viewer** tab of Data Mining Designer in the appropriate viewer for the model. The [!INCLUDE[msCoName](../../includes/msconame-md.md)] Neural Network Viewer provides the following tabs for use in exploring neural network mining models:  
  
-   [Inputs](#BKMK_Inputs)  
  
-   [Outputs](#BKMK_Outputs)  
  
-   [Variables](#BKMK_Characteristics)  
  
###  <a name="BKMK_Inputs"></a> Inputs  
 Use the **Inputs** tab to choose the attributes and values that the model used as inputs. By default, the viewer opens with all attributes included. In this default view, the model chooses which attribute values are the most important to display.  
  
 To select an input attribute, click inside the **Attribute** column of the **Input** grid, and select an attribute from the drop-down list. (Only attributes that are included in the model are included in the list.)  
  
 The first distinct value appears under the **Value** column. Clicking the default value reveals a list that contains all the possible states of the associated attribute. You can select the state that you want to investigate. You can select as many attributes as you want.  
  
 [Back to Top](#BKMK_ViewerTabs)  
  
###  <a name="BKMK_Outputs"></a> Outputs  
 Use the **Outputs** tab to choose the outcome attribute to investigate. You can choose any two outcome states to compare, assuming the columns were defined as predictable attributes when the model was created.  
  
 Use the **OutputAttribute** list to select an attribute. You can then select two states that are associated with the attribute from the **Value 1** and **Value 2** lists. These two states of the output attribute will be compared in the **Variables** pane.  
  
 [Back to Top](#BKMK_ViewerTabs)  
  
###  <a name="BKMK_Characteristics"></a> Variables  
 The grid in the **Variables** tab contains the following columns: **Attribute**, **Value**, **Favors [value 1]**, and **Favors [value 2]**. By default, the columns are sorted by the strength of **Favors [value 1]**. Clicking a column heading changes the sort order to the selected column.  
  
 A bar to the right of the attribute shows which state of the output attribute the specified input attribute state favors. The size of the bar shows how strongly the output state favors the input state.  
  
 [Back to Top](#BKMK_ViewerTabs)  
  
## See Also  
 [Microsoft Neural Network Algorithm](microsoft-neural-network-algorithm.md)   
 [Mining Model Viewer Tasks and How-tos](mining-model-viewer-tasks-and-how-tos.md)   
 [Mining Model Viewer Tasks and How-tos](mining-model-viewer-tasks-and-how-tos.md)   
 [Data Mining Tools](data-mining-tools.md)   
 [Data Mining Model Viewers](data-mining-model-viewers.md)  
  
  
