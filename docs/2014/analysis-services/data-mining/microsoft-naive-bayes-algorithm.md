---
title: "Microsoft Naive Bayes Algorithm | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "Bayesian classifiers"
  - "algorithms [data mining]"
  - "predictive modeling [Analysis Services]"
  - "classification algorithms [Analysis Services]"
  - "naive bayes algorithms [Analysis Services]"
ms.assetid: 3b53e011-3b1a-4cd1-bdc2-456768ba31b5
author: minewiskan
ms.author: owend
manager: craigg
---
# Microsoft Naive Bayes Algorithm
  The [!INCLUDE[msCoName](../../includes/msconame-md.md)] Naive Bayes algorithm is a classification algorithm based on Bayes' theorems, and provided by [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] for use in predictive modeling. The word naïve in the name Naïve Bayes derives from the fact that the algorithm uses Bayesian techniques but does not take into account dependencies that may exist.  
  
 This algorithm is less computationally intense than other [!INCLUDE[msCoName](../../includes/msconame-md.md)] algorithms, and therefore is useful for quickly generating mining models to discover relationships between input columns and predictable columns. You can use this algorithm to do initial exploration of data, and then later you can apply the results to create additional mining models with other algorithms that are more computationally intense and more accurate.  
  
## Example  
 As an ongoing promotional strategy, the marketing department for the Adventure Works Cycle company has decided to target potential customers by mailing out fliers. To reduce costs, they want to send fliers only to those customers who are likely to respond. The company stores information in a database about demographics and response to a previous mailing. They want to use this data to see how demographics such as age and location can help predict response to a promotion, by comparing potential customers to customers who have similar characteristics and who have purchased from the company in the past. Specifically, they want to see the differences between those customers who bought a bicycle and those customers who did not.  
  
 By using the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Naive Bayes algorithm, the marketing department can quickly predict an outcome for a particular customer profile, and can therefore determine which customers are most likely to respond to the fliers. By using the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Naive Bayes Viewer in [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], they can also visually investigate specifically which input columns contribute to positive responses to fliers.  
  
## How the Algorithm Works  
 The [!INCLUDE[msCoName](../../includes/msconame-md.md)] Naive Bayes algorithm calculates the probability of every state of each input column, given each possible state of the predictable column.  
  
 To understand how this works, use the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Naive Bayes Viewer in [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)] (as shown in the following graphic) to visually explore how the algorithm distributes states.  
  
 ![Naive bayes distribution of states](../media/naive-bayes.gif "Naive bayes distribution of states")  
  
 Here, the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Naive Bayes Viewer lists each input column in the dataset, and shows how the states of each column are distributed, given each state of the predictable column.  
  
 You would use this view of the model to identify the input columns that are important for differentiating between states of the predictable column.  
  
 For example, in the row for Commute Distance shown here, the distribution of input values is visibly different for buyers vs. non-buyers. What this tells you is that the input, Commute Distance = 0-1 miles, is a potential predictor.  
  
 The viewer also provides values for the distributions, so you can see that for customers who commute from one to two miles to work, the probability of them buying a bike is 0.387, and the probability that they will not buy a bike is 0.287. In this example, the algorithm uses the numeric information, derived from customer characteristics (such as commute distance), to predict whether a customer will buy a bike.  
  
 For more information about using the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Naive Bayes Viewer, see [Browse a Model Using the Microsoft Naive Bayes Viewer](browse-a-model-using-the-microsoft-naive-bayes-viewer.md).  
  
## Data Required for Naive Bayes Models  
 When you prepare data for use in training a Naive Bayes model, you should understand the requirements for the algorithm, including how much data is needed, and how the data is used.  
  
 The requirements for a Naive Bayes model are as follows:  
  
-   **A single key column** Each model must contain one numeric or text column that uniquely identifies each record. Compound keys are not allowed.  
  
-   **Input columns** In a Naive Bayes model, all columns must be either discrete or discretized columns. For information about discretizing columns, see [Discretization Methods &#40;Data Mining&#41;](discretization-methods-data-mining.md).  
  
     For a Naive Bayes model, it is also important to ensure that the input attributes are independent of each other. This is particularly important when you use the model for prediction.  
  
     The reason is that, if you use two columns of data that are already closely related, the effect would be to multiply the influence of those columns, which can obscure other factors that influence the outcome.  
  
     Conversely, the ability of the algorithm to identify correlations among variables is useful when you are exploring a model or dataset, to identify relationships among inputs.  
  
-   **At least one predictable column** The predictable attribute must contain discrete or discretized values.  
  
     The values of the predictable column can be treated as inputs. This practice can be useful when you are exploring a new dataset, to find relationships among the columns.  
  
## Viewing the Model  
 To explore the model, you can use the **Microsoft Naive Bayes Viewer**. The viewer shows you how the input attributes relate to the predictable attribute. The viewer also provides a detailed profile of each cluster, a list of the attributes that distinguish each cluster from the others, and the characteristics of the entire training data set. For more information, see [Browse a Model Using the Microsoft Naive Bayes Viewer](browse-a-model-using-the-microsoft-naive-bayes-viewer.md).  
  
 If you want to know more detail, you can browse the model in the [Microsoft Generic Content Tree Viewer &#40;Data Mining&#41;](../microsoft-generic-content-tree-viewer-data-mining.md). For more information about the type of information stored in the model, see [Mining Model Content for Naive Bayes Models &#40;Analysis Services - Data Mining&#41;](mining-model-content-for-naive-bayes-models-analysis-services-data-mining.md).  
  
## Making Predictions  
 After the model has been trained, the results are stored as a set of patterns, which you can explore or use to make predictions.  
  
 You can create queries to return predictions about how new data relates to the predictable attribute, or you can retrieve statistics that describe the correlations found by the model.  
  
 For information about how to create queries against a data mining model, see [Data Mining Queries](data-mining-queries.md). For examples of how to use queries with a Naive Bayes model, see [Naive Bayes Model Query Examples](naive-bayes-model-query-examples.md).  
  
## Remarks  
  
-   Supports the use of Predictive Model Markup Language (PMML) to create mining models.  
  
-   Supports drillthrough.  
  
-   Does not support the creation of data mining dimensions.  
  
-   Supports the use of OLAP mining models.  
  
## See Also  
 [Data Mining Algorithms &#40;Analysis Services - Data Mining&#41;](data-mining-algorithms-analysis-services-data-mining.md)   
 [Feature Selection &#40;Data Mining&#41;](feature-selection-data-mining.md)   
 [Naive Bayes Model Query Examples](naive-bayes-model-query-examples.md)   
 [Mining Model Content for Naive Bayes Models &#40;Analysis Services - Data Mining&#41;](mining-model-content-for-naive-bayes-models-analysis-services-data-mining.md)   
 [Microsoft Naive Bayes Algorithm Technical Reference](microsoft-naive-bayes-algorithm-technical-reference.md)  
  
  
