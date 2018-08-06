---
title: "Microsoft Association Algorithm | Microsoft Docs"
ms.date: 05/08/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: data-mining
ms.topic: conceptual
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# Microsoft Association Algorithm
[!INCLUDE[ssas-appliesto-sqlas](../../includes/ssas-appliesto-sqlas.md)]
  The [!INCLUDE[msCoName](../../includes/msconame-md.md)] Association algorithm is an algorithm that is often used for recommendation engines. A recommendation engine recommends items to customers based on items they have already bought, or in which they have indicated an interest. The [!INCLUDE[msCoName](../../includes/msconame-md.md)] Association algorithm is also useful for market basket analysis.   
  
 Association models are built on datasets that contain identifiers both for individual cases and for the items that the cases contain. A group of items in a case is called an *itemset*. An association model consists of a series of itemsets and the rules that describe how those items are grouped together within the cases. The rules that the algorithm identifies can be used to predict a customer's likely future purchases, based on the items that already exist in the customer's shopping cart. The following diagram shows a series of rules in an itemset.  
  
 ![A set of rules for an association model](../../analysis-services/data-mining/media/association.gif "A set of rules for an association model")  
  
 As the diagram illustrates, the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Association algorithm can potentially find many rules within a dataset. The algorithm uses two parameters, support and probability, to describe the itemsets and rules that it generates. For example, if X and Y represent two items that could be in a shopping cart, the support parameter is the number of cases in the dataset that contain the combination of items, X and Y. By using the support parameter in combination with the user-defined parameters, *MINIMUM_SUPPORT* and *MAXIMUM_SUPPORT,* the algorithm controls the number of itemsets that are generated. The probability parameter, also named *confidence*, represents the fraction of cases in the dataset that contain X and that also contain Y. By using the probability parameter in combination with the *MINIMUM_PROBABILITY* parameter, the algorithm controls the number of rules that are generated.  
  
## Example  
 The [!INCLUDE[ssSampleDBCoShort](../../includes/sssampledbcoshort-md.md)] Cycle company is redesigning the functionality of its Web site. The goal of the redesign is to increase sell-through of products. Because the company records each sale in a transactional database, they can use the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Association algorithm to identify sets of products that tend to be purchased together. They can then predict additional items that a customer might be interested in, based on items that are already in the customer's shopping basket.  
  
## How the Algorithm Works  
 The [!INCLUDE[msCoName](../../includes/msconame-md.md)] Association algorithm traverses a dataset to find items that appear together in a case. The algorithm then groups into itemsets any associated items that appear, at a minimum, in the number of cases that are specified by the *MINIMUM_SUPPORT* parameter. For example, an itemset could be "Mountain 200=Existing, Sport 100=Existing", and could have a support of 710. The algorithm then generates rules from the itemsets. These rules are used to predict the presence of an item in the database, based on the presence of other specific items that the algorithm identifies as important. For example, a rule could be "if Touring 1000=existing and Road bottle cage=existing, then Water bottle=existing", and could have a probability of 0.812. In this example, the algorithm identifies that the presence in the basket of the Touring 1000 tire and the water bottle cage predicts that a water bottle would also likely be in the basket.  
  
 For a more detailed explanation of the algorithm, together with a list of parameters for customizing the behavior of the algorithm and controlling the results in the mining model, see [Microsoft Association Algorithm Technical Reference](../../analysis-services/data-mining/microsoft-association-algorithm-technical-reference.md).  
  
## Data Required for Association Models  
 When you prepare data for use in an association rules model, you should understand the requirements for the particular algorithm, including how much data is needed, and how the data is used.  
  
 The requirements for an association rules model are as follows:  
  
-   **A single key column** Each model must contain one numeric or text column that uniquely identifies each record. compound keys not permitted.  
  
-   **A single predictable column** An association model can have only one predictable column. Typically it is the key column of the nested table, such as the filed that lists the products that were purchased. The values must be discrete or discretized.  
  
-   **Input columns** . The input columns must be discrete. The input data for an association model often is contained in two tables. For example, one table might contain customer information while another table contains customer purchases. You can input this data into the model by using a nested table. For more information about nested tables, see [Nested Tables &#40;Analysis Services - Data Mining&#41;](../../analysis-services/data-mining/nested-tables-analysis-services-data-mining.md).  
  
 For more detailed information about the content types and data types supported for association models, see the Requirements section of [Microsoft Association Algorithm Technical Reference](../../analysis-services/data-mining/microsoft-association-algorithm-technical-reference.md).  
  
## Viewing an Association Model  
 To explore the model, you can use the **Microsoft Association Viewer**. When you view an association model, [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] presents the correlations from different angles so that you can better understand the relationships and rules that were found in the data. The **Itemset** pane in the viewer provides a detailed breakdown of the most common combinations, or itemsets. The **Rules** pane presents a list of rules that have been generalized from the data, adds calculations of probability, and ranks the rules by relative importance. the dependency network viewer lets you visually explore how individual different items are connected. For more information, see [Browse a Model Using the Microsoft Cluster Viewer](../../analysis-services/data-mining/browse-a-model-using-the-microsoft-cluster-viewer.md).  
  
 If you want to find out more detail about any of the itemsets and rules, you can browse the model in the [Microsoft Generic Content Tree Viewer](../../analysis-services/data-mining/browse-a-model-using-the-microsoft-generic-content-tree-viewer.md). The content stored for the model includes the support for each itemset, a score for each rule, and other statistics. For more information, see [Mining Model Content for Association Models &#40;Analysis Services - Data Mining&#41;](../../analysis-services/data-mining/mining-model-content-for-association-models-analysis-services-data-mining.md).  
  
## Creating Predictions  
 After the model has been processed, you can use the rules and itemsets to make predictions. In an association model, a prediction tells you what item is likely to occur given the presence of the specified item, and the prediction can include such information as the probability, the support, or the importance. For examples of how to create queries against an association model, see [Association Model Query Examples](../../analysis-services/data-mining/association-model-query-examples.md).  
  
 For general information about how to create a query against a data mining model, see [Data Mining Queries](../../analysis-services/data-mining/data-mining-queries.md).  
  
## Performance  
 The process of creating itemsets and counting correlations can be time-consuming. Although the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Association Rules algorithm uses optimization techniques to save space and make processing faster, you should know that performance issues can occur under conditions such as the following:  
  
-   Data set is large with many individual items.  
  
-   Minimum itemset size is set too low.  
  
 To minimize processing time and reduce the complexity of the itemsets, you might try grouping related items by categories before you analyze the data.  
  
## Remarks  
  
-   Does not support the use of Predictive Model Markup Language (PMML) to create mining models.  
  
-   Supports drillthrough.  
  
-   Supports the use of OLAP mining models.  
  
-   Supports the creation of data mining dimensions.  
  
## See Also  
 [Data Mining Algorithms &#40;Analysis Services - Data Mining&#41;](../../analysis-services/data-mining/data-mining-algorithms-analysis-services-data-mining.md)   
 [Browse a Model Using the Microsoft Association Rules Viewer](../../analysis-services/data-mining/browse-a-model-using-the-microsoft-association-rules-viewer.md)   
 [Mining Model Content for Association Models &#40;Analysis Services - Data Mining&#41;](../../analysis-services/data-mining/mining-model-content-for-association-models-analysis-services-data-mining.md)   
 [Microsoft Association Algorithm Technical Reference](../../analysis-services/data-mining/microsoft-association-algorithm-technical-reference.md)   
 [Association Model Query Examples](../../analysis-services/data-mining/association-model-query-examples.md)  
  
  
