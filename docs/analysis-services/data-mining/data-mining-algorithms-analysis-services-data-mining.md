---
title: "Data Mining Algorithms (Analysis Services - Data Mining) | Microsoft Docs"
ms.date: 05/01/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: data-mining
ms.topic: conceptual
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# Data Mining Algorithms (Analysis Services - Data Mining)
[!INCLUDE[ssas-appliesto-sqlas](../../includes/ssas-appliesto-sqlas.md)]
  An *algorithm* in data mining (or machine learning) is a set of heuristics and calculations that creates a model from data. To create a model, the algorithm first analyzes the data you provide, looking for specific types of patterns or trends. The algorithm uses the results of this analysis over many iterations to find the optimal parameters for creating the mining model. These parameters are then applied across the entire data set to extract actionable patterns and detailed statistics.  
  
 The mining model that an algorithm creates from your data can take various forms, including:  
  
-   A set of clusters that describe how the cases in a dataset are related.  
  
-   A decision tree that predicts an outcome, and describes how different criteria affect that outcome.  
  
-   A mathematical model that forecasts sales.  
  
-   A set of rules that describe how products are grouped together in a transaction, and the probabilities that products are purchased together.  
  
 The algorithms provided in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Data Mining are the most popular, well-researched methods of deriving patterns from data. To take one example, K-means clustering is one  of the oldest clustering algorithms and is available widely in many different tools and with many different implementations and options. However, the particular implementation of K-means clustering used in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Data Mining was developed by Microsoft Research and then optimized for performance with [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)]. All of the Microsoft data mining algorithms can be extensively customized and are fully programmable, using the provided APIs. You can also automate the creation, training, and retraining of models by using the data mining components in [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)].  
  
 You can also use third-party algorithms that comply with the OLE DB for Data Mining specification, or develop custom algorithms that can be registered as services and then used within the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Data Mining framework.  
  
## Choosing the Right Algorithm  
 Choosing the best algorithm to use for a specific analytical task can be a challenge. While you can use different algorithms to perform the same business task, each algorithm produces a different result, and some algorithms can produce more than one type of result. For example, you can use the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Decision Trees algorithm not only for prediction, but also as a way to reduce the number of columns in a dataset, because the decision tree can identify columns that do not affect the final mining model.  
  
### Choosing an Algorithm by Type  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Data Mining includes the following algorithm types:  
  
-   **Classification algorithms** predict one or more discrete variables, based on the other attributes in the dataset.  
  
-   **Regression algorithms** predict one or more continuous numeric variables, such as profit or loss, based on other attributes in the dataset.  
  
-   **Segmentation algorithms** divide data into groups, or clusters, of items that have similar properties.  
  
-   **Association algorithms** find correlations between different attributes in a dataset. The most common application of this kind of algorithm is for creating association rules, which can be used in a market basket analysis.  
  
-   **Sequence analysis algorithms** summarize frequent sequences or episodes in data, such as a series of clicks in a web site, or a series of log events preceding machine maintenance.  
  
 However, there is no reason that you should be limited to one algorithm in your solutions. Experienced analysts will sometimes use one algorithm to determine the most effective inputs (that is, variables), and then apply a different algorithm to predict a specific outcome based on that data. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Data Mining lets you build multiple models on a single mining structure, so within a single data mining solution you could use a clustering algorithm, a decision trees model, and a Naïve Bayes model to get different views on your data. You might also use multiple algorithms within a single solution to perform separate tasks: for example, you could use regression to obtain financial forecasts, and use a neural network algorithm to perform an analysis of factors that influence forecasts.  
  
### Choosing an Algorithm by Task  
 To help you select an algorithm for use with a specific task, the following table provides suggestions for the types of tasks for which each algorithm is traditionally used.  
  
|Examples of tasks|Microsoft algorithms to use|  
|-----------------------|---------------------------------|  
|**Predicting a discrete attribute:**<br /><br /> Flag the customers in a prospective buyers list as good or poor prospects.<br /><br /> Calculate the probability that a server will fail within the next 6 months.<br /><br /> Categorize patient outcomes and explore related factors.|[Microsoft Decision Trees Algorithm](../../analysis-services/data-mining/microsoft-decision-trees-algorithm.md)<br /><br /> [Microsoft Naive Bayes Algorithm](../../analysis-services/data-mining/microsoft-naive-bayes-algorithm.md)<br /><br /> [Microsoft Clustering Algorithm](../../analysis-services/data-mining/microsoft-clustering-algorithm.md)<br /><br /> [Microsoft Neural Network Algorithm](../../analysis-services/data-mining/microsoft-neural-network-algorithm.md)|  
|**Predicting a continuous attribute:**<br /><br /> Forecast next year's sales.<br /><br /> Predict site visitors given past historical and seasonal trends.<br /><br /> Generate a risk score given demographics.|[Microsoft Decision Trees Algorithm](../../analysis-services/data-mining/microsoft-decision-trees-algorithm.md)<br /><br /> [Microsoft Time Series Algorithm](../../analysis-services/data-mining/microsoft-time-series-algorithm.md)<br /><br /> [Microsoft Linear Regression Algorithm](../../analysis-services/data-mining/microsoft-linear-regression-algorithm.md)|  
|**Predicting a sequence:**<br /><br /> Perform clickstream analysis of a company's Web site.<br /><br /> Analyze the factors leading to server failure.<br /><br /> Capture and analyze sequences of activities during outpatient visits, to formulate best practices around common activities.|[Microsoft Sequence Clustering Algorithm](../../analysis-services/data-mining/microsoft-sequence-clustering-algorithm.md)|  
|**Finding groups of common items in transactions:**<br /><br /> Use market basket analysis to determine product placement.<br /><br /> Suggest additional products to a customer for purchase.<br /><br /> Analyze survey data from visitors to an event, to find which activities or booths were correlated, to plan future activities.|[Microsoft Association Algorithm](../../analysis-services/data-mining/microsoft-association-algorithm.md)<br /><br /> [Microsoft Decision Trees Algorithm](../../analysis-services/data-mining/microsoft-decision-trees-algorithm.md)|  
|**Finding groups of similar items:**<br /><br /> Create patient risk profiles groups based on attributes such as demographics and behaviors.<br /><br /> Analyze users by browsing and buying patterns.<br /><br /> Identify servers that have similar usage characteristics.|[Microsoft Clustering Algorithm](../../analysis-services/data-mining/microsoft-clustering-algorithm.md)<br /><br /> [Microsoft Sequence Clustering Algorithm](../../analysis-services/data-mining/microsoft-sequence-clustering-algorithm.md)|  
  
## Related Content  
 The following table provides links to learning resources for each of the data mining algorithms that are provided in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Data Mining:  
  
|||  
|-|-|  
|**Basic algorithm description**|Explains what the algorithm does and how it works, and outlines possible business scenarios where the algorithm might be useful.|  
||[Microsoft Association Algorithm](../../analysis-services/data-mining/microsoft-association-algorithm.md)<br /><br /> [Microsoft Clustering Algorithm](../../analysis-services/data-mining/microsoft-clustering-algorithm.md)<br /><br /> [Microsoft Decision Trees Algorithm](../../analysis-services/data-mining/microsoft-decision-trees-algorithm.md)<br /><br /> [Microsoft Linear Regression Algorithm](../../analysis-services/data-mining/microsoft-linear-regression-algorithm.md)<br /><br /> [Microsoft Logistic Regression Algorithm](../../analysis-services/data-mining/microsoft-logistic-regression-algorithm.md)<br /><br /> [Microsoft Naive Bayes Algorithm](../../analysis-services/data-mining/microsoft-naive-bayes-algorithm.md)<br /><br /> [Microsoft Neural Network Algorithm](../../analysis-services/data-mining/microsoft-neural-network-algorithm.md)<br /><br /> [Microsoft Sequence Clustering Algorithm](../../analysis-services/data-mining/microsoft-sequence-clustering-algorithm.md)<br /><br /> [Microsoft Time Series Algorithm](../../analysis-services/data-mining/microsoft-time-series-algorithm.md)|  
|**Technical reference**|Provides technical detail about the implementation of the algorithm, with academic references as necessary. Lists the parameters that you can set to control the behavior of the algorithm and customize the results in the model. Describes data requirements and provides performance tips if possible.|  
||[Microsoft Association Algorithm Technical Reference](../../analysis-services/data-mining/microsoft-association-algorithm-technical-reference.md)<br /><br /> [Microsoft Clustering Algorithm Technical Reference](../../analysis-services/data-mining/microsoft-clustering-algorithm-technical-reference.md)<br /><br /> [Microsoft Decision Trees Algorithm Technical Reference](../../analysis-services/data-mining/microsoft-decision-trees-algorithm-technical-reference.md)<br /><br /> [Microsoft Linear Regression Algorithm Technical Reference](../../analysis-services/data-mining/microsoft-linear-regression-algorithm-technical-reference.md)<br /><br /> [Microsoft Logistic Regression Algorithm Technical Reference](../../analysis-services/data-mining/microsoft-logistic-regression-algorithm-technical-reference.md)<br /><br /> [Microsoft Naive Bayes Algorithm Technical Reference](../../analysis-services/data-mining/microsoft-naive-bayes-algorithm-technical-reference.md)<br /><br /> [Microsoft Neural Network Algorithm Technical Reference](../../analysis-services/data-mining/microsoft-neural-network-algorithm-technical-reference.md)<br /><br /> [Microsoft Sequence Clustering Algorithm Technical Reference](../../analysis-services/data-mining/microsoft-sequence-clustering-algorithm-technical-reference.md)<br /><br /> [Microsoft Time Series Algorithm Technical Reference](../../analysis-services/data-mining/microsoft-time-series-algorithm-technical-reference.md)|  
|**Model content**|Explains how information is structured within each type of data mining model, and explains how to interpret the information stored in each of the nodes.|  
||[Mining Model Content for Association Models &#40;Analysis Services - Data Mining&#41;](../../analysis-services/data-mining/mining-model-content-for-association-models-analysis-services-data-mining.md)<br /><br /> [Mining Model Content for Clustering Models &#40;Analysis Services - Data Mining&#41;](../../analysis-services/data-mining/mining-model-content-for-clustering-models-analysis-services-data-mining.md)<br /><br /> [Mining Model Content for Decision Tree Models &#40;Analysis Services - Data Mining&#41;](../../analysis-services/data-mining/mining-model-content-for-decision-tree-models-analysis-services-data-mining.md)<br /><br /> [Mining Model Content for Linear Regression Models &#40;Analysis Services - Data Mining&#41;](../../analysis-services/data-mining/mining-model-content-for-linear-regression-models-analysis-services-data-mining.md)<br /><br /> [Mining Model Content for Logistic Regression Models &#40;Analysis Services - Data Mining&#41;](../../analysis-services/data-mining/mining-model-content-for-logistic-regression-models.md)<br /><br /> [Mining Model Content for Naive Bayes Models &#40;Analysis Services - Data Mining&#41;](../../analysis-services/data-mining/mining-model-content-for-naive-bayes-models-analysis-services-data-mining.md)<br /><br /> [Mining Model Content for Neural Network Models &#40;Analysis Services - Data Mining&#41;](../../analysis-services/data-mining/mining-model-content-for-neural-network-models-analysis-services-data-mining.md)<br /><br /> [Mining Model Content for Sequence Clustering Models &#40;Analysis Services - Data Mining&#41;](../../analysis-services/data-mining/mining-model-content-for-sequence-clustering-models.md)<br /><br /> [Mining Model Content for Time Series Models &#40;Analysis Services - Data Mining&#41;](../../analysis-services/data-mining/mining-model-content-for-time-series-models-analysis-services-data-mining.md)|  
|**Data mining queries**|Provides multiple queries that you can use with each model type. Examples include content queries that let you learn more about the patterns in the model, and prediction queries to help you build predictions based on those patterns.|  
||[Association Model Query Examples](../../analysis-services/data-mining/association-model-query-examples.md)<br /><br /> [Clustering Model Query Examples](../../analysis-services/data-mining/clustering-model-query-examples.md)<br /><br /> [Decision Trees Model Query Examples](../../analysis-services/data-mining/decision-trees-model-query-examples.md)<br /><br /> [Linear Regression Model Query Examples](../../analysis-services/data-mining/linear-regression-model-query-examples.md)<br /><br /> [Logistic Regression Model Query Examples](../../analysis-services/data-mining/logistic-regression-model-query-examples.md)<br /><br /> [Naive Bayes Model Query Examples](../../analysis-services/data-mining/naive-bayes-model-query-examples.md)<br /><br /> [Neural Network Model Query Examples](../../analysis-services/data-mining/neural-network-model-query-examples.md)<br /><br /> [Sequence Clustering Model Query Examples](../../analysis-services/data-mining/sequence-clustering-model-query-examples.md)<br /><br /> [Time Series Model Query Examples](../../analysis-services/data-mining/time-series-model-query-examples.md)|  
  
## Related Tasks  
  
|**Topic**|**Description**|  
|---------------|---------------------|  
|Determine the algorithm used by a data mining model|[Query the Parameters Used to Create a Mining Model](../../analysis-services/data-mining/query-the-parameters-used-to-create-a-mining-model.md)|  
|Create a Custom Plug-In Algorithm|[Plugin Algorithms](../../analysis-services/data-mining/plugin-algorithms.md)|  
|Explore a model using an algorithm-specific viewer|[Data Mining Model Viewers](../../analysis-services/data-mining/data-mining-model-viewers.md)|  
|View the content of a model using a generic table format|[Browse a Model Using the Microsoft Generic Content Tree Viewer](../../analysis-services/data-mining/browse-a-model-using-the-microsoft-generic-content-tree-viewer.md)|  
|Learn about how to set up your data and use algorithms to create models|[Mining Structures &#40;Analysis Services - Data Mining&#41;](../../analysis-services/data-mining/mining-structures-analysis-services-data-mining.md)<br /><br /> [Mining Models &#40;Analysis Services - Data Mining&#41;](../../analysis-services/data-mining/mining-models-analysis-services-data-mining.md)|  
  
## See Also  
 [Data Mining Tools](../../analysis-services/data-mining/data-mining-tools.md)  
  
  
