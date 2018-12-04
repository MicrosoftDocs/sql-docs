---
title: "Exploring the Call Center Model (Intermediate Data Mining Tutorial) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
ms.assetid: 9095212c-9068-4dd8-85ce-17a467adeabb
author: minewiskan
ms.author: owend
manager: craigg
---
# Exploring the Call Center Model (Intermediate Data Mining Tutorial)
  Now that you have built the exploratory model, you can use it to learn more about your data by using the following tools provided in [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)].  
  
-   [Microsoft Neural Network Viewer](#bkmk_NNviewer) **:** This viewer is available in the **Mining Model Viewer** tab of Data Mining Designer, and is designed to help you experiment with interactions in the data.  
  
-   [Microsoft Generic Content Tree Viewer](#bkmk_genviewer) **:** This standard viewer provides in-depth detail about the patterns and statistics discovered by the algorithm when it generated the model.  
  
##  <a name="bkmk_NNviewer"></a> Microsoft Neural Network Viewer  
 The viewer has three panes - **Input**, **Output**, and **Variables**.  
  
 By using the **Output** pane, you can select different values for the predictable attribute, or dependent variable. If your model contains multiple predictable attributes, you can select the attribute from the **Output Attribute** list.  
  
 The **Variables** pane compares the two outcomes that you chose in terms of contributing attributes, or variables. The colored bars visually represent how strongly the variable affects the target outcomes. You can also view lift scores for the variables. A lift score is calculated differently depending on which mining model type you are using, but generally tells you the improvement in the model when using this attribute for prediction.  
  
 The **Input** pane lets you add influencers to the model to try out various what-if scenarios.  
  
### Using the Output Pane  
 In this initial model, you are interested in seeing how various factors affect the grade of service. To do this, you can select Service Grade from the list of output attributes, and then compare different levels of service by selecting ranges from the dropdown lists for **Value 1** and **Value 2**.  
  
##### To compare lowest and highest service grades  
  
1.  For **Value 1**, select the range with the lowest values. For example, the range 0-0-0.7 represents the lowest abandon rates, and therefore the best level of service.  
  
    > [!NOTE]  
    >  The exact values in this range may vary depending on how you configured the model.  
  
2.  For **Value 2**, select the range with the highest values. For example, the range with the value >=0.12 represents the highest abandon rates, and therefore the worst service grade. In other words, 12% of the customers who phoned during this shift hung up before speaking to a representative.  
  
     The contents of the **Variables** pane are updated to compare attributes that contribute to the outcome values. Therefore, the left column shows you the attributes that are associated with the best grade of service, and the right column shows you the attributes associated with the worst grade of service.  
  
### Using the Variables Pane  
 In this model, it appears that `Average Time Per Issue` is an important factor. This variable indicates the average time that it takes for a call to be answered, regardless of call type.  
  
##### To view and copy probability and lift scores for an attribute  
  
1.  In the **Variables** pane, pause the mouse over the colored bar in the first row.  
  
     This colored bar shows you how strongly `Average Time Per Issue` contributes toward the service grade. The tooltip shows an overall score, probabilities, and lift scores for each combination of a variable and a target outcome.  
  
2.  In the **Variables** pane, right-click any colored bar and select **Copy**.  
  
3.  In an Excel worksheet, right-click any cell and select **Paste**.  
  
     The report is pasted as an HTML table, and shows only the scores for each bar.  
  
4.  In a different Excel worksheet, right-click any cell and select **Paste Special**.  
  
     The report is pasted as text format, and includes the related statistics described in the next section.  
  
### Using the Input Pane  
 Suppose that you are interested in looking at the effect of a particular factor, such as the shift, or number of operators. You can select a particular variable by using the **Input** pane, and the **Variables** pane is automatically updated to compare the two previously selected groups given the specified variable.  
  
##### To review the effect on service grade by changing input attributes  
  
1.  In the **Input** pane, for **attribute**, select Shift.  
  
2.  For **Value**, select **AM**.  
  
     The **Variables** pane updates to show the impact on the model when the shift is **AM**. All other selections remain the same - you are still comparing the lowest and highest service grades.  
  
3.  For **Value**, select **PM1**.  
  
     The **Variables** pane updates to show the impact on the model when the shift changes.  
  
4.  In the **Input** pane, click the next blank row under **Attribute**, and select Calls. For **Value**, select the range that indicates the greatest number of calls.  
  
     A new input condition is added to the list. The **Variables** pane updates to show the impact on the model for a particular shift when the call volume is highest.  
  
5.  Continue to change the values for Shift and Calls to find any interesting correlations between shift, call volume, and service grade.  
  
    > [!NOTE]  
    >  To clear the **Input** pane so that you can use different attributes, click **Refresh viewer content**.  
  
### Interpreting the Statistics Provided in the Viewer  
 Longer waiting times are a strong predictor of a high abandon rate, meaning a poor service grade. This may seem an obvious conclusion; however, the mining model provides you with some additional statistical data to help you interpret these trends.  
  
-   **Score**: Value that indicates the overall importance of this variable for discriminating between outcomes. The higher the score, the stronger the effect the variable has on the outcome.  
  
-   **Probability of value 1**: Percentage that represents the probability of this value for this outcome.  
  
-   **Probability of value 2**: Percentage that represents the probability of this value for this outcome.  
  
-   **Lift for Value 1** and **Lift for Value 2**: Scores that represents the impact of using this particular variable for predicting the Value 1 and Value 2 outcomes. The higher the score, the better the variable is at predicting the outcomes.  
  
 The following table contains some example values for the top influencers. For example, the **Probability of value 1** is 60.6% and **Probability of value 2** is 8.30%, meaning that when the Average Time Per Issue was in the range of 44-70 minutes, 60.6% of cases were in the shift with the highest service grades (Value 1), and 8.30% of cases were in the shift with the worse service grades (Value 2).  
  
 From this information, you can draw some conclusions. Shorter call response time (the range of 44-70) strongly influences better service grade (the range 0.00-0.07). The score (92.35) tells you that this variable is very important.  
  
 However, as you look down the list of contributing factors, you see some other factors with effects that are more subtle and more difficult to interpret. For example, shift appears to influence service, but the lift scores and the relative probabilities indicate that shift is not a major factor.  
  
|Attribute|Value|Favors \< 0.07|Favors >= 0.12|  
|---------------|-----------|--------------------|----------------------|  
|Average Time Per Issue|89.087 - 120.000||Score:  100<br /><br /> Probability of Value1: 4.45 %<br /><br /> Probability of Value2: 51.94 %<br /><br /> Lift for Value1: 0.19<br /><br /> Lift for Value2: 1.94|  
|Average Time Per Issue|44.000 - 70.597|Score: 92.35<br /><br /> Probability of Value1: 60.06 %<br /><br /> Probability of Value2: 8.30 %<br /><br /> Lift for Value1: 2.61<br /><br /> Lift for Value2: 0.31||  
  
 [Back to Top](#bkmk_NNviewer)  
  
##  <a name="bkmk_genviewer"></a> Microsoft Generic Content Tree Viewer  
 This viewer can be used to view even more detailed information created by the algorithm when the model is processed. The **MicrosoftGeneric Content Tree Viewer** represents the mining model as a series of nodes, wherein each node represents learned knowledge about the training data. This viewer can be used with all models, but the contents of the nodes are different depending in the model type.  
  
 For neural network models or logistic regression models, you might find the `marginal statistics node` particularly useful. This node contains derived statistics about the distribution of values in your data. This information can be useful if you want to get a summary of the data without having to write many T-SQL queries. The chart of binning values in the previous topic was derived from the marginal statistics node.  
  
#### To obtain a summary of data values from the mining model  
  
1.  In Data Mining Designer, in the **Mining Model Viewer** tab, select \<mining model name>.  
  
2.  From the **Viewer** list, select **Microsoft Generic Content Tree Viewer**.  
  
     The view of the mining model refreshes to show a node hierarchy in the left-hand pane and an HTML table in the right-hand pane.  
  
3.  In the **Node Caption** pane, click the node that has the name 10000000000000000.  
  
     The topmost node in any model is always the model root node. In a neural network or logistic regression model, the node immediately under that is the marginal statistics node.  
  
4.  In the **Node Details** pane, scroll down until you find the row, NODE_DISTRIBUTION.  
  
5.  Scroll down through the NODE_DISTRIBUTION table to view the distribution of values as calculated by the neural network algorithm.  
  
 To use this data in a report, you could select and then copy the information for specific rows, or you can use the following Data Mining Extensions (DMX) query to extract the complete contents of the node.  
  
```  
SELECT *   
FROM [Call Center EQ4].CONTENT  
WHERE NODE_NAME = '10000000000000000'  
```  
  
 You can also use the node hierarchy and the details in the NODE_DISTRIBUTION table to traverse individual paths in the neural network and view statistics from the hidden layer. For more information, see [Neural Network Model Query Examples](../../2014/analysis-services/data-mining/neural-network-model-query-examples.md).  
  
 [Back to Top](#bkmk_NNviewer)  
  
## Next Task in Lesson  
 [Adding a Logistic Regression Model to the Call Center Structure &#40;Intermediate Data Mining Tutorial&#41;](../../2014/tutorials/add-logistic-regression-model-to-call-center-intermediate-data-mining.md)  
  
## See Also  
 [Mining Model Content for Neural Network Models &#40;Analysis Services - Data Mining&#41;](../../2014/analysis-services/data-mining/mining-model-content-for-neural-network-models-analysis-services-data-mining.md)   
 [Neural Network Model Query Examples](../../2014/analysis-services/data-mining/neural-network-model-query-examples.md)   
 [Microsoft Neural Network Algorithm Technical Reference](../../2014/analysis-services/data-mining/microsoft-neural-network-algorithm-technical-reference.md)   
 [Change the Discretization of a Column in a Mining Model](../../2014/analysis-services/data-mining/change-the-discretization-of-a-column-in-a-mining-model.md)  
  
  
