---
title: "Exploring the Decision Tree Model (Basic Data Mining Tutorial) | Microsoft Docs"
ms.custom: ""
ms.date: "04/27/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
ms.assetid: 2e1472c2-3f3e-4dae-acb3-62fca374d397
author: minewiskan
ms.author: owend
manager: craigg
---
# Exploring the Decision Tree Model (Basic Data Mining Tutorial)
  The [!INCLUDE[msCoName](../includes/msconame-md.md)] Decision Trees algorithm predicts which columns influence the decision to purchase a bike based upon the remaining columns in the training set.  
  

  
##  <a name="Decision_Tree_Tab"></a> Decision Tree Tab  
 On the **Decision Tree** tab, you can view decision trees for every predictable attribute in the dataset.  
  
 In this case, the model predicts only one column, Bike Buyer, so there is only one tree to view. If there were more trees, you could use the **Tree** box to choose another tree.  
  
 As you view the `TM_Decision_Tree` model in the Decision Tree viewer, you can see the most important attributes at the left side of the chart. "Most important" means that these attributes have the greatest influence on the outcome. Attributes further down the tree (to the right of the chart) have less of an effect.  
  
 In this example, age is the single most important factor in predicting bike buying. The model groups customers by age, and then shows the next more important attribute for each age group. For example, in the group of customers aged 34 to 40, the number of cars owned is the strongest predictor after age.  
  
#### To explore the model in the Decision Tree tab  
  
1.  Select the **Mining Model Viewer** tab in **Data Mining Designer**.  
  
     By default, the designer opens to the first model that was added to the structure -- in this case, `TM_Decision_Tree`.  
  
2.  Use the magnifying glass buttons to adjust the size of the tree display.  
  
     By default, the [!INCLUDE[msCoName](../includes/msconame-md.md)] Tree Viewer shows only the first three levels of the tree. If the tree contains fewer than three levels, the viewer shows only the existing levels. You can view more levels by using the **Show Level** slider or the **Default Expansion** list.  
  
3.  Slide **Show Level** to the fourth bar.  
  
4.  Change the **Background** value to `1`.  
  
     By changing the **Background** setting, you can quickly see the number of cases in each node that have the target value of `1` for [Bike Buyer]. Remember that in this particular scenario, each case represents a customer. The value `1` indicates that the customer previously purchased a bike; the value **0** indicates that the customer has not purchased a bike. The darker the shading of the node, the higher the percentage of cases in the node that have the target value.  
  
5.  Place your cursor over the node labeled **All**. An tooltip will display the following information:  
  
    -   Total number of cases  
  
    -   Number of non bike buyer cases  
  
    -   Number of bike buyer cases  
  
    -   Number of cases with missing values for [Bike Buyer]  
  
     Alternately, place your cursor over any node in the tree to see the condition that is required to reach that node from the node that comes before it. You can also view this same information in the **Mining Legend**.  
  
6.  Click on the node for **Age >=34 and < 41**. The histogram is displayed as a thin horizontal bar across the node and represents the distribution of customers in this age range who previously did (pink) and did not (blue) purchase a bike. The Viewer shows us that customers between the ages of 34 and 40 with one or no cars are likely to purchase a bike. Taking it one step further, we find that the likelihood to purchase a bike increases if the customer is actually age 38 to 40.  
  
 Because you enabled drillthrough when you created the structure and model, you can retrieve detailed information from the model cases and mining structure, including those columns that were not included in the mining model (e.g., emailAddress, FirstName).  
  
 For more information, see [Drillthrough Queries &#40;Data Mining&#41;](../../2014/analysis-services/data-mining/drillthrough-queries-data-mining.md).  
  
#### To drill through to case data  
  
1.  Right-click a node, and select **Drill Through** then **Model Columns Only**.  
  
     The details for each training case are displayed in spreadsheet format. These details come from the vTargetMail view that you selected as the case table when building the mining structure.  
  
2.  Right-click a node, and select **Drill Through** then **Model and Structure Columns**.  
  
     The same spreadsheet displays with the structure columns appended to the end.  
  
  
###  <a name="Dependency_Network_Tab"></a> Dependency Network Tab  
 The **Dependency Network** tab displays the relationships between the attributes that contribute to the predictive ability of the mining model. The Dependency Network viewer reinforces our findings that Age and Region are important factors in predicting bike buying.  
  
##### To explore the model in the Dependency Network tab  
  
1.  Click the `Bike Buyer` node to identify its dependencies.  
  
     The center node for the dependency network, `Bike Buyer`, represents the predictable attribute in the mining model. The graph highlights any connected nodes that have an effect on the predictable attribute.  
  
2.  Adjust the **All Links** slider to identify the most influential attribute.  
  
     As you drag down the slider, attributes that have only a weak effect on the [Bike Buyer] column are removed from the graph. By adjusting the slider, you can discover that Age and Region are the greatest factors in predicting whether someone is a bike buyer.  
  
## Related Tasks  
 See these topics to explore the data using the other kinds of models.  
  
-   [Exploring the Clustering Model &#40;Basic Data Mining Tutorial&#41;](../../2014/tutorials/exploring-the-clustering-model-basic-data-mining-tutorial.md)  
  
-   [Exploring the Naive Bayes Model &#40;Basic Data Mining Tutorial&#41;](../../2014/tutorials/exploring-the-naive-bayes-model-basic-data-mining-tutorial.md)  
  
## Next Task in Lesson  
 [Exploring the Clustering Model &#40;Basic Data Mining Tutorial&#41;](../../2014/tutorials/exploring-the-clustering-model-basic-data-mining-tutorial.md)  
  
## See Also  
 [Mining Model Viewer Tasks and How-tos](../../2014/analysis-services/data-mining/mining-model-viewer-tasks-and-how-tos.md)   
 [Decision Tree Tab &#40;Mining Model Viewer&#41;](../../2014/analysis-services/decision-tree-tab-mining-model-viewer.md)   
 [Dependency Network Tab &#40;Mining Model Viewer&#41;](../../2014/analysis-services/dependency-network-tab-mining-model-viewer.md)   
 [Browse a Model Using the Microsoft Tree Viewer](../../2014/analysis-services/data-mining/browse-a-model-using-the-microsoft-tree-viewer.md)  
  
  
