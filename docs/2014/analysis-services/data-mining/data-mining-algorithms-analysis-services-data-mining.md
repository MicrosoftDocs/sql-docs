---
title: "Data Mining Algorithms (Analysis Services - Data Mining) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "segmentation algorithms [Analysis Services]"
  - "clustering [Data Mining]"
  - "learning algorithms"
  - "data mining [Analysis Services], models"
  - "algorithms [data mining]"
  - "mining models [Analysis Services], algorithms"
  - "inductive learning"
  - "mining models [Analysis Services], creating"
  - "data mining [Analysis Services], algorithms"
  - "machine learning algorithms [Analysis Services]"
ms.assetid: ed1fc83b-b98c-437e-bf53-4ff001b92d64
author: minewiskan
ms.author: owend
manager: craigg
---
# Data Mining Algorithms (Analysis Services - Data Mining)
  A *data mining algorithm* is a set of heuristics and calculations that creates a data mining model from data. To create a model, the algorithm first analyzes the data you provide, looking for specific types of patterns or trends. The algorithm uses the results of this analysis to define the optimal parameters for creating the mining model. These parameters are then applied across the entire data set to extract actionable patterns and detailed statistics.  
  
 The mining model that an algorithm creates from your data can take various forms, including:  
  
-   A set of clusters that describe how the cases in a dataset are related.  
  
-   A decision tree that predicts an outcome, and describes how different criteria affect that outcome.  
  
-   A mathematical model that forecasts sales.  
  
-   A set of rules that describe how products are grouped together in a transaction, and the probabilities that products are purchased together.  
  
 [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] provides multiple algorithms for use in your data mining solutions. These algorithms are implementations of some of the most popular methodologies used in data mining. All of the Microsoft data mining algorithms can be customized and are fully programmable using the provided APIs, or by using the data mining components in SQL Server Integration Services.  
  
 You can also use third-party algorithms that comply with the OLE DB for Data Mining specification, or develop custom algorithms that can be registered as services and then used within the SQL Server Data Mining framework.  
  
## Choosing the Right Algorithm  
 Choosing the best algorithm to use for a specific analytical task can be a challenge. While you can use different algorithms to perform the same business task, each algorithm produces a different result, and some algorithms can produce more than one type of result. For example, you can use the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Decision Trees algorithm not only for prediction, but also as a way to reduce the number of columns in a dataset, because the decision tree can identify columns that do not affect the final mining model.  
  
### Choosing an Algorithm by Type  
 [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] includes the following algorithm types:  
  
-   **Classification algorithms** predict one or more discrete variables, based on the other attributes in the dataset.  
  
-   **Regression algorithms** predict one or more continuous variables, such as profit or loss, based on other attributes in the dataset.  
  
-   **Segmentation algorithms** divide data into groups, or clusters, of items that have similar properties.  
  
-   **Association algorithms** find correlations between different attributes in a dataset. The most common application of this kind of algorithm is for creating association rules, which can be used in a market basket analysis.  
  
-   **Sequence analysis algorithms** summarize frequent sequences or episodes in data, such as a Web path flow.  
  
 However, there is no reason that you should be limited to one algorithm in your solutions. Experienced analysts will sometimes use one algorithm to determine the most effective inputs (that is, variables), and then apply a different algorithm to predict a specific outcome based on that data. SQL Server data mining lets you build multiple models on a single mining structure, so within a single data mining solution you might use a clustering algorithm, a decision trees model, and a naïve Bayes model to get different views on your data. You might also use multiple algorithms within a single solution to perform separate tasks: for example, you could use regression to obtain financial forecasts, and use a neural network algorithm to perform an analysis of factors that influence sales.  
  
### Choosing an Algorithm by Task  
 To help you select an algorithm for use with a specific task, the following table provides suggestions for the types of tasks for which each algorithm is traditionally used.  
  
|Examples of tasks|Microsoft algorithms to use|  
|-----------------------|---------------------------------|  
|**Predicting a discrete attribute**<br /><br /> Flag the customers in a prospective buyers list as good or poor prospects.<br /><br /> Calculate the probability that a server will fail within the next 6 months.<br /><br /> Categorize patient outcomes and explore related factors.|[Microsoft Decision Trees Algorithm](microsoft-decision-trees-algorithm.md)<br /><br /> [Microsoft Naive Bayes Algorithm](microsoft-naive-bayes-algorithm.md)<br /><br /> [Microsoft Clustering Algorithm](microsoft-clustering-algorithm.md)<br /><br /> [Microsoft Neural Network Algorithm](microsoft-neural-network-algorithm.md)|  
|**Predicting a continuous attribute**<br /><br /> Forecast next year's sales.<br /><br /> Predict site visitors given past historical and seasonal trends.<br /><br /> Generate a risk score given demographics.|[Microsoft Decision Trees Algorithm](microsoft-decision-trees-algorithm.md)<br /><br /> [Microsoft Time Series Algorithm](microsoft-time-series-algorithm.md)<br /><br /> [Microsoft Linear Regression Algorithm](microsoft-linear-regression-algorithm.md)|  
|**Predicting a sequence**<br /><br /> Perform clickstream analysis of a company's Web site.<br /><br /> Analyze the factors leading to server failure.<br /><br /> Capture and analyze sequences of activities during outpatient visits, to formulate best practices around common activities.|[Microsoft Sequence Clustering Algorithm](microsoft-sequence-clustering-algorithm.md)|  
|**Finding groups of common items in transactions**<br /><br /> Use market basket analysis to determine product placement.<br /><br /> Suggest additional products to a customer for purchase.<br /><br /> Analyze survey data from visitors to an event, to find which activities or booths were correlated, to plan future activities.|[Microsoft Association Algorithm](microsoft-association-algorithm.md)<br /><br /> [Microsoft Decision Trees Algorithm](microsoft-decision-trees-algorithm.md)|  
|**Finding groups of similar items**<br /><br /> Create patient risk profiles groups based on attributes such as demographics and behaviors.<br /><br /> Analyze users by browsing and buying patterns.<br /><br /> Identify servers that have similar usage characteristics.|[Microsoft Clustering Algorithm](microsoft-clustering-algorithm.md)<br /><br /> [Microsoft Sequence Clustering Algorithm](microsoft-sequence-clustering-algorithm.md)|  
  
## Related Content  
 The following table provides links to learning resources for each of the data mining algorithms that are provided in [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)]:  
  
|||  
|-|-|  
|**Basic algorithm description**|Explains what the algorithm does and how it works, and outlines possible business scenarios where the algorithm might be useful.|  
||[Microsoft Association Algorithm](microsoft-association-algorithm.md)<br /><br /> [Microsoft Clustering Algorithm](microsoft-clustering-algorithm.md)<br /><br /> [Microsoft Decision Trees Algorithm](microsoft-decision-trees-algorithm.md)<br /><br /> [Microsoft Linear Regression Algorithm](microsoft-linear-regression-algorithm.md)<br /><br /> [Microsoft Logistic Regression Algorithm](microsoft-logistic-regression-algorithm.md)<br /><br /> [Microsoft Naive Bayes Algorithm](microsoft-naive-bayes-algorithm.md)<br /><br /> [Microsoft Neural Network Algorithm](microsoft-neural-network-algorithm.md)<br /><br /> [Microsoft Sequence Clustering Algorithm](microsoft-sequence-clustering-algorithm.md)<br /><br /> [Microsoft Time Series Algorithm](microsoft-time-series-algorithm.md)|  
|**Technical reference**|Provides technical detail about the implementation of the algorithm, with academic references as necessary. Lists the parameters that you can set to control the behavior of the algorithm and customize the results in the model. Describes data requirements and provides performance tips if possible.|  
||[Microsoft Association Algorithm Technical Reference](microsoft-association-algorithm-technical-reference.md)<br /><br /> [Microsoft Clustering Algorithm Technical Reference](microsoft-clustering-algorithm-technical-reference.md)<br /><br /> [Microsoft Decision Trees Algorithm Technical Reference](microsoft-decision-trees-algorithm-technical-reference.md)<br /><br /> [Microsoft Linear Regression Algorithm Technical Reference](microsoft-linear-regression-algorithm-technical-reference.md)<br /><br /> [Microsoft Logistic Regression Algorithm Technical Reference](microsoft-logistic-regression-algorithm-technical-reference.md)<br /><br /> [Microsoft Naive Bayes Algorithm Technical Reference](microsoft-naive-bayes-algorithm-technical-reference.md)<br /><br /> [Microsoft Neural Network Algorithm Technical Reference](microsoft-neural-network-algorithm-technical-reference.md)<br /><br /> [Microsoft Sequence Clustering Algorithm Technical Reference](microsoft-sequence-clustering-algorithm-technical-reference.md)<br /><br /> [Microsoft Time Series Algorithm Technical Reference](microsoft-time-series-algorithm-technical-reference.md)|  
|**Model content**|Explains how information is structured within each type of data mining model, and explains how to interpret the information stored in each of the nodes.|  
||[Mining Model Content for Association Models &#40;Analysis Services - Data Mining&#41;](mining-model-content-for-association-models-analysis-services-data-mining.md)<br /><br /> [Mining Model Content for Clustering Models &#40;Analysis Services - Data Mining&#41;](mining-model-content-for-clustering-models-analysis-services-data-mining.md)<br /><br /> [Mining Model Content for Decision Tree Models &#40;Analysis Services - Data Mining&#41;](mining-model-content-for-decision-tree-models-analysis-services-data-mining.md)<br /><br /> [Mining Model Content for Linear Regression Models &#40;Analysis Services - Data Mining&#41;](mining-model-content-for-linear-regression-models-analysis-services-data-mining.md)<br /><br /> [Mining Model Content for Logistic Regression Models &#40;Analysis Services - Data Mining&#41;](mining-model-content-for-logistic-regression-models.md)<br /><br /> [Mining Model Content for Naive Bayes Models &#40;Analysis Services - Data Mining&#41;](mining-model-content-for-naive-bayes-models-analysis-services-data-mining.md)<br /><br /> [Mining Model Content for Neural Network Models &#40;Analysis Services - Data Mining&#41;](mining-model-content-for-neural-network-models-analysis-services-data-mining.md)<br /><br /> [Mining Model Content for Sequence Clustering Models &#40;Analysis Services - Data Mining&#41;](mining-model-content-for-sequence-clustering-models.md)<br /><br /> [Mining Model Content for Time Series Models &#40;Analysis Services - Data Mining&#41;](mining-model-content-for-time-series-models-analysis-services-data-mining.md)|  
|**Data mining queries**|Provides multiple queries that you can use with each model type. Examples include content queries that let you learn more about the patterns in the model, and prediction queries to help you build predictions based on those patterns.|  
||[Association Model Query Examples](association-model-query-examples.md)<br /><br /> [Clustering Model Query Examples](clustering-model-query-examples.md)<br /><br /> [Decision Trees Model Query Examples](decision-trees-model-query-examples.md)<br /><br /> [Linear Regression Model Query Examples](linear-regression-model-query-examples.md)<br /><br /> [Logistic Regression Model Query Examples](logistic-regression-model-query-examples.md)<br /><br /> [Naive Bayes Model Query Examples](naive-bayes-model-query-examples.md)<br /><br /> [Neural Network Model Query Examples](neural-network-model-query-examples.md)<br /><br /> [Sequence Clustering Model Query Examples](sequence-clustering-model-query-examples.md)<br /><br /> [Time Series Model Query Examples](time-series-model-query-examples.md)|  
  
## Related Tasks  
  
|**Topic**|**Description**|  
|---------------|---------------------|  
|Determine the algorithm used by a data mining model|[Query the Parameters Used to Create a Mining Model](query-the-parameters-used-to-create-a-mining-model.md)|  
|Create a Custom Plug-In Algorithm|[Plugin Algorithms](plugin-algorithms.md)|  
|Explore a model using an algorithm-specific viewer|[Data Mining Model Viewers](data-mining-model-viewers.md)|  
|View the content of a model using a generic table format|[Browse a Model Using the Microsoft Generic Content Tree Viewer](browse-a-model-using-the-microsoft-generic-content-tree-viewer.md)|  
|Learn about how to set up your data and use algorithms to create models|[Mining Structures &#40;Analysis Services - Data Mining&#41;](mining-structures-analysis-services-data-mining.md)<br /><br /> [Mining Models &#40;Analysis Services - Data Mining&#41;](mining-models-analysis-services-data-mining.md)|  
  
## See Also  
 [Data Mining Tools](data-mining-tools.md)  
  
  
