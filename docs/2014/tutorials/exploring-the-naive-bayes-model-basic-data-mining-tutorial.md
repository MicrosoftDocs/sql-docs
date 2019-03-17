---
title: "Exploring the Naive Bayes Model (Basic Data Mining Tutorial) | Microsoft Docs"
ms.custom: ""
ms.date: "04/27/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: analysis-services
ms.topic: conceptual
ms.assetid: b06708d5-4477-4a51-bf8d-0b1e3c1f9ebb
author: minewiskan
ms.author: owend
manager: kfile
---
# Exploring the Naive Bayes Model (Basic Data Mining Tutorial)
  The [!INCLUDE[msCoName](../includes/msconame-md.md)] Naive Bayes algorithm provides several methods for displaying the interaction between bike buying and the input attributes.  
  
 The [!INCLUDE[msCoName](../includes/msconame-md.md)] Naive Bayes Viewer provides the following tabs for use in exploring Naive Bayes mining models:  
  
 
  
##  <a name="DependencyNetwork"></a> Dependency Network  
 The **Dependency Network** tab works in the same way as the **Dependency Network** tab for the [!INCLUDE[msCoName](../includes/msconame-md.md)] Tree Viewer. Each node in the viewer represents an attribute, and the lines between nodes represent relationships. In the viewer, you can see all the attributes that affect the state of the predictable attribute, Bike Buyer.  
  
#### To explore the model in the Dependency Network tab  
  
1.  Use the **Mining Model** list at the top of the **Mining Model Viewer** tab to switch to the `TM_NaiveBayes` model.  
  
2.  Use the **Viewer** list to switch to **Microsoft Naive Bayes Viewer**.  
  
3.  Click the `Bike Buyer` node to identify its dependencies.  
  
     The pink shading indicates that all of the attributes have an effect on bike buying.  
  
4.  Adjust the slider to identify the most influential attribute.  
  
     As you lower the slider, only the attributes that have the greatest effect on the [Bike Buyer] column remain. By adjusting the slider, you can discover that a few of the most influential attributes are: number of cars owned, commute distance, and total number of children.  
 
  
##  <a name="AttributeProfiles"></a> Attribute Profiles  
 The **Attribute Profiles** tab describes how different states of the input attributes affect the outcome of the predictable attribute.  
  
#### To explore the model in the Attribute Profiles tab  
  
1.  In the **Predictable** box, verify that `Bike Buyer` is selected.  
  
2.  If the **Mining Legend** is blocking display of the **Attribute profiles**, move it out of the way.  
  
3.  In the **Histogram** bars box, select **5**.  
  
     In our model, 5 is the maximum number of states for any one variable.  
  
     The attributes that affect the state of this predictable attribute are listed together with the values of each state of the input attributes and their distributions in each state of the predictable attribute.  
  
4.  In the **Attributes** column, find **Number Cars Owned**.  Notice the differences in the histograms for bike buyers (column labeled 1) and non-buyers (column labeled 0). A person with zero or one car is much more likely to buy a bike.  
  
5.  Double-click the **Number Cars Owned** cell in the bike buyer (column labeled 1) column.  
  
     The **Mining Legend** displays a more detailed view.  
  
  
##  <a name="AttributeCharacteristics"></a> Attribute Characteristics  
 With the **Attribute Characteristics** tab, you can select an attribute and value to see how frequently values for other attributes appear in the selected value cases.  
  
#### To explore the model in the Attribute Characteristics tab  
  
1.  In the **Attribute** list, verify that `Bike Buyer` is selected.  
  
2.  Set the **Value** to **1**.  
  
     In the viewer, you will see that customers who have no children at home, short commutes, and live in the North America region are more likely to buy a bike.  
  
  
##  <a name="AttributeDiscrimination"></a> Attribute Discrimination  
 With the **Attribute Discrimination** tab, you can investigate the relationship between two discrete values of bike buying and other attribute values. Because the `TM_NaiveBayes` model has only two states, 1 and 0, you do not have to make any changes to the viewer.  
  
 In the viewer, you can see that people who do not own cars tend to buy bicycles, and people who own two cars tend not to buy bicycles.  
  
## Related Tasks  
 See the following topics to explore the other mining models.  
  
-   [Exploring the Decision Tree Model &#40;Basic Data Mining Tutorial&#41;](../../2014/tutorials/exploring-the-decision-tree-model-basic-data-mining-tutorial.md)  
  
-   [Exploring the Clustering Model &#40;Basic Data Mining Tutorial&#41;](../../2014/tutorials/exploring-the-clustering-model-basic-data-mining-tutorial.md)  
  
## Next Lesson  
 [Lesson 5: Testing Models &#40;Basic Data Mining Tutorial&#41;](../../2014/tutorials/lesson-5-testing-models-basic-data-mining-tutorial.md)  
  
## Previous Task in Lesson  
 [Exploring the Clustering Model &#40;Basic Data Mining Tutorial&#41;](../../2014/tutorials/exploring-the-clustering-model-basic-data-mining-tutorial.md)  
  
## See Also  
 [Browse a Model Using the Microsoft Naive Bayes Viewer](../../2014/analysis-services/data-mining/browse-a-model-using-the-microsoft-naive-bayes-viewer.md)   
 [Attribute Discrimination Tab &#40;Mining Model Viewer&#41;](../../2014/analysis-services/attribute-discrimination-tab-mining-model-viewer.md)   
 [Attribute Profiles Tab &#40;Mining Model Viewer&#41;](../../2014/analysis-services/attribute-profiles-tab-mining-model-viewer.md)   
 [Attribute Characteristics Tab &#40;Mining Model Viewer&#41;](../../2014/analysis-services/attribute-characteristics-tab-mining-model-viewer.md)   
 [Dependency Network Tab &#40;Mining Model Viewer&#41;](../../2014/analysis-services/dependency-network-tab-mining-model-viewer.md)  
  
  
