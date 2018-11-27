---
title: "Testing Accuracy with Lift Charts (Basic Data Mining Tutorial) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
ms.assetid: 822d414b-4a39-473f-80c3-53476e30655a
author: minewiskan
ms.author: owend
manager: craigg
---
# Testing Accuracy with Lift Charts (Basic Data Mining Tutorial)
  On the **Mining Accuracy Chart** tab of Data Mining Designer, you can calculate how well each of your models makes predictions, and compare the results of each model directly against the results of the other models. This method of comparison is referred to as a *lift chart*. Typically, the predictive accuracy of a mining model is measured by either lift or classification accuracy. For this tutorial we will use the lift chart only.  
  
 In this topic, you will perform the following tasks:  
  
-   [Choose the Input Data](#BKMK_InputData)  
  
-   [Configure Accuracy Chart Parameters](#BKMK_Selecting)  
  
##  <a name="BKMK_InputData"></a> Choosing the Input Data  
 The first step in testing the accuracy of your mining models is to select the data source that you will use for testing. You will test how well the models perform against your testing data and then you will use them with external data.  
  
#### To select the data set  
  
1.  Switch to the **Mining Accuracy Chart** tab in Data Mining Designer in [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)] and select the **Input Selection** tab.  
  
2.  In the **Select data set to be used for Accuracy Chart** group box, select **Use mining structure test cases**. This is the testing data that you set aside when you created the mining structure.  
  
     For more information on the other options, see [Choose an Accuracy Chart Type and Set Chart Options](../../2014/analysis-services/data-mining/choose-an-accuracy-chart-type-and-set-chart-options.md).  
  
##  <a name="BKMK_Selecting"></a> Setting Accuracy Chart Parameters  
 To create an accuracy chart, you must define three things:  
  
-   Which models should you include in the accuracy chart?  
  
-   Which predictable attribute do you want to measure? Some models might have multiple targets, but each chart can measure only one outcome at a time.  
  
     To use a column as the **Predictable Column Name** in an accuracy chart, the columns must have the usage type of `Predict` or `Predict Only`. Also, the content type of the target column must be either `Discrete` or `Discretized`. In other words, you cannot measure accuracy against continuous numeric outputs using the lift chart.  
  
-   Do you want to measure the model's general accuracy, or its accuracy  in predicting a particular value (such as [Bike Buyer] = 'Yes')  
  
#### To generate the lift chart  
  
1.  On the **Input Selection** tab of Data Mining Designer, under **Select predictable mining model columns to show in the lift chart**, select the checkbox for **Synchronize Prediction Columns and Values**.  
  
2.  In the **Predictable Column Name** column, verify that **Bike Buyer** is selected for each model.  
  
3.  In the **Show** column, select each of the models.  
  
     By default, all the models in the mining structure are selected. You can decide not to include a model, but for this tutorial leave all the models selected.  
  
4.  In the **Predict Value** column, select **1**. The same value is automatically filled in for each model that has the same predictable column.  
  
5.  Select the **Lift Chart** tab.  
  
     When you click the tab, a prediction query is executed to get predictions for the test data, and the results are compared against the known values. The results are plotted on the graph.  
  
     If you specified a particular target outcome using the **Predict Value** option, the lift chart plots the results of random guesses and the results of an ideal model.  
  
    -   The random guess line shows how accurate the model would be without using any data to inform its predictions: that is, a 50-50 split between two outcomes. The lift chart helps you visualize how much better your model performs in comparison to a random guess.  
  
    -   The ideal model line represents the upper bound of accuracy. It shows you the maximum possible benefit you could achieve if your model always predicted accurately.  
  
     The mining models you created will usually fall between these two extremes. Any improvement from the random guess is considered to be *lift*.  
  
6.  Use the legend to locate the colored lines representing the Ideal Model and the Random Guess Model.  
  
     You'll notice that the `TM_Decision_Tree` model provides the greatest lift,  outperforming both the Clustering and Naive Bayes models.  
  
 For an in-depth explanation of a lift chart similar to the one created in this lesson, see [Lift Chart &#40;Analysis Services - Data Mining&#41;](../../2014/analysis-services/data-mining/lift-chart-analysis-services-data-mining.md).  
  
## Next Task in Lesson  
 [Testing a Filtered Model &#40;Basic Data Mining Tutorial&#41;](../../2014/tutorials/testing-a-filtered-model-basic-data-mining-tutorial.md)  
  
## See Also  
 [Lift Chart &#40;Analysis Services - Data Mining&#41;](../../2014/analysis-services/data-mining/lift-chart-analysis-services-data-mining.md)   
 [Lift Chart Tab &#40;Mining Accuracy Chart View&#41;](../../2014/analysis-services/lift-chart-tab-mining-accuracy-chart-view.md)  
  
  
