---
title: "Browse a Model Using the Microsoft Naive Bayes Viewer | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "discrimination [Analysis Services]"
  - "naive bayes model [Analysis Services]"
  - "Bayesian classifiers"
  - "mining model content, viewing"
  - "predictive modeling [Analysis Services]"
  - "Naive Bayes Viewer [Analysis Services]"
  - "data mining [Analysis Services], predictive modeling"
  - "Microsoft Naive Bayes Viewer"
  - "histograms [Analysis Services]"
  - "mining models [Analysis Services], predictive modeling"
  - "dependencies [Analysis Services]"
ms.assetid: 19743095-63c1-4486-8c1d-2efc143243be
author: minewiskan
ms.author: owend
manager: craigg
---
# Browse a Model Using the Microsoft Naive Bayes Viewer
  The [!INCLUDE[msCoName](../../includes/msconame-md.md)] Naive Bayes Viewer in [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] displays mining models that are built with the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Naive Bayes algorithm. The [!INCLUDE[msCoName](../../includes/msconame-md.md)] Naive Bayes algorithm is a classification algorithm that is highly adaptable to predictive modeling tasks. For more information about this algorithm, see [Microsoft Naive Bayes Algorithm](microsoft-naive-bayes-algorithm.md).  
  
 Because one of the main purposes of a naive Bayes model is to provide a way to quickly explore the data in a dataset, the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Naive Bayes Viewer provides several methods for displaying the interaction between predictable attributes and input attributes.  
  
> [!NOTE]  
>  If you want to view detailed information about the equations used in the model and the patterns that were discovered, you can switch to the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Generic Content Tree viewer. For more information, see [Browse a Model Using the Microsoft Generic Content Tree Viewer](browse-a-model-using-the-microsoft-generic-content-tree-viewer.md) or [Microsoft Generic Content Tree Viewer &#40;Data Mining&#41;](../microsoft-generic-content-tree-viewer-data-mining.md).  
  
##  <a name="BKMK_ViewerTabs"></a> Viewer Tabs  
 When you browse a mining model in [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)], the model is displayed on the **Mining Model Viewer** tab of Data Mining Designer in the appropriate viewer for the model. The [!INCLUDE[msCoName](../../includes/msconame-md.md)] Naive Bayes Viewer provides the following tabs for exploring data:  
  
-   [Dependency Network](#BKMK_Dependency)  
  
-   [Attribute Profiles](#BKMK_Profiles)  
  
-   [Attribute Characteristics](#BKMK_Characteristics)  
  
-   [Attribute Discrimination](#BKMK_Discrimination)  
  
##  <a name="BKMK_Dependency"></a> Dependency Network  
 The **Dependency Network** tab displays the dependencies between the input attributes and the predictable attributes in a model. The slider at the left of the viewer acts as a filter that is tied to the strengths of the dependencies. Lowering the slider shows only the strongest links.  
  
 When you select a node, the viewer highlights the dependencies that are specific to the node. For example, if you choose a predictable node, the viewer also highlights each node that helps predict the predictable node.  
  
 The legend at the bottom of the viewer links color codes to the type of dependency in the graph. For example, when you select a predictable node, the predictable node is shaded turquoise, and the nodes that predict the selected node are shaded orange.  
  
 [Back to Top](#BKMK_ViewerTabs)  
  
##  <a name="BKMK_Profiles"></a> Attribute Profiles  
 The **Attribute Profiles** tab displays histograms in a grid. You can use this grid to compare the predictable attribute that you select in the **Predictable** box to all other attributes that are in the model. Each column in the tab represents a state of the predictable attribute. If the predictable attribute has many states, you can change the number of states that appear in the histogram by adjusting the **Histogram bars**. If the number you choose is less than the total number of states in the attribute, the states are listed in order of support, with the remaining states collected into a single gray bucket.  
  
 To display a Mining Legend that relates the colors of the histogram to the states of an attribute, click the **Show Legend** check box. The Mining Legend also displays the distribution of cases for each attribute-value pair that you select.  
  
 To copy the contents of the grid to the Clipboard as an HTML table, right-click the **Attribute Profiles** tab and select **Copy**.  
  
 [Back to Top](#BKMK_ViewerTabs)  
  
##  <a name="BKMK_Characteristics"></a> Attribute Characteristics  
 To use the **Attribute Characteristics** tab, select a predictable attribute from the **Attribute** list and select a state of the selected attribute from the **Value** list. When you set these variables, the **Attribute Characteristics** tab displays the states of the attributes that are associated with the selected case of the selected attribute. The attributes are sorted by importance.  
  
 [Back to Top](#BKMK_ViewerTabs)  
  
##  <a name="BKMK_Discrimination"></a> Attribute Discrimination  
 To use the **Attribute Discrimination** tab, select a predictable attribute and two of its states from the **Attribute**, **Value 1**, and **Value 2** lists. The grid on the **Attribute Discrimination** tab then displays the following information in columns:  
  
 **Attribute**  
 Lists other attributes in the dataset that contain a state that highly favors one state of the predictable attribute.  
  
 **Values**  
 Shows the value of the attribute in the **Attribute** column.  
  
 **Favors \<value 1>**  
 Shows a colored bar that indicates how strongly the attribute value favors the predictable attribute value shown in **Value 1**.  
  
 **Favors \<value 2>**  
 Shows a colored bar that indicates how strongly the attribute value favors the predictable attribute value shown in **Value 2**.  
  
 [Back to Top](#BKMK_ViewerTabs)  
  
## See Also  
 [Microsoft Naive Bayes Algorithm](microsoft-naive-bayes-algorithm.md)   
 [Mining Model Viewer Tasks and How-tos](mining-model-viewer-tasks-and-how-tos.md)   
 [Data Mining Tools](data-mining-tools.md)   
 [Data Mining Model Viewers](data-mining-model-viewers.md)  
  
  
