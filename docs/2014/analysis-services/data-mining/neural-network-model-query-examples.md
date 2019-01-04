---
title: "Neural Network Model Query Examples | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "neural network algorithms [Analysis Services]"
  - "content queries [DMX]"
  - "neural network model [Analysis Services]"
ms.assetid: 81b06183-620f-4e0c-bc10-532e6a1f0829
author: minewiskan
ms.author: owend
manager: craigg
---
# Neural Network Model Query Examples
  When you create a query against a data mining model, you can create a content query, which provides details about the patterns discovered in analysis, or a prediction query, which uses the patterns in the model to make predictions for new data. For example, a content query for a neural network model might retrieve model metadata such as the number of hidden layers. Alternatively, a prediction query might suggest classifications based on an input and optionally provide probabilities for each classification.  
  
 This section explains how to create queries for models that are based on the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Neural Network algorithm.  
  
 **Content queries**  
  
 [Getting Model Metadata by Using DMX](#bkmk_Query1)  
  
 [Retrieving Model Metadata from the Schema Rowset](#bkmk_Query2)  
  
 [Retrieving the Input Attributes for the Model](#bkmk_Query3)  
  
 [Retrieving Weights from the Hidden Layer](#bkmk_Query4)  
  
 **Prediction queries**  
  
 [Creating a Singleton Prediction](#bkmk_Query5)  
  
## Finding Information about a Neural Network Model  
 All mining models expose the content learned by the algorithm according to a standardized schema, the mining model schema rowset. This information provides details about the model and includes the basic metadata, structures discovered in analysis, and parameters that are used when processing. You can create queries against the model content by using Data Mining Extension (DMX) statements.  
  
###  <a name="bkmk_Query1"></a> Sample Query 1: Getting Model Metadata by Using DMX  
 The following query returns some basic metadata about a model that was built by using the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Neural Network algorithm. In a neural network model, the parent node of the model contains only the name of the model, the name of the database where the model is stored, and the number of child nodes. However, the marginal statistics node (NODE_TYPE = 24) provides both this basic metadata and some derived statistics about the input columns used in the model.  
  
 The following sample query is based on the mining model that you create in the [Intermediate Data Mining Tutorial](../../tutorials/lesson-5-build-models-intermediate-data-mining-tutorial.md), named `Call Center Default NN`. The model uses data from a call center to explore possible correlations between staffing and the number of calls, orders, and issues. The DMX statement retrieves data from the marginal statistics node of the neural network model. The query includes the FLATTENED keyword, because the input attribute statistics of interest are stored in a nested table, NODE_DISTRIBUTION. However, if your query provider supports hierarchical rowsets you do not need to use the FLATTENED keyword.  
  
```  
SELECT FLATTENED MODEL_CATALOG, MODEL_NAME,   
(    SELECT ATTRIBUTE_NAME, ATTRIBUTE_VALUE,  
     [SUPPORT], [PROBABILITY], VALUETYPE   
     FROM NODE_DISTRIBUTION  
) AS t  
FROM [Call Center Default NN].CONTENT  
WHERE NODE_TYPE = 24  
```  
  
> [!NOTE]  
>  You must enclose the name of the nested table columns `[SUPPORT]` and `[PROBABILITY]` in brackets to distinguish them from the reserved keywords of the same name.  
  
 Example results:  
  
|MODEL_CATALOG|MODEL_NAME|t.ATTRIBUTE_NAME|t.ATTRIBUTE_VALUE|t.SUPPORT|t.PROBABILITY|t.VALUETYPE|  
|--------------------|-----------------|-----------------------|------------------------|---------------|-------------------|-----------------|  
|Adventure Works DW Multidimensional 2012|Call Center NN|Average Time Per Issue|Missing|0|0|1|  
|Adventure Works DW Multidimensional 2012|Call Center NN|Average Time Per Issue|< 64.7094100096|11|0.407407407|5|  
  
 For a definition of what the columns in the schema rowset mean in the context of a neural network model, see [Mining Model Content for Neural Network Models &#40;Analysis Services - Data Mining&#41;](mining-model-content-for-neural-network-models-analysis-services-data-mining.md).  
  
###  <a name="bkmk_Query2"></a> Sample Query 2: Retrieving Model Metadata from the Schema Rowset  
 You can find the same information that is returned in a DMX content query by querying the data mining schema rowset. However, the schema rowset provides some additional columns. The following sample query returns the date that the model was created, the date it was modified, and the date that the model was last processed. The query also returns the predictable columns, which are not easily available from the model content, and the parameters that were used to build the model. This information can be useful for documenting the model.  
  
```  
SELECT MODEL_NAME, DATE_CREATED, LAST_PROCESSED, PREDICTION_ENTITY, MINING_PARAMETERS   
from $system.DMSCHEMA_MINING_MODELS  
WHERE MODEL_NAME = 'Call Center Default NN'  
```  
  
 Example results:  
  
|||  
|-|-|  
|MODEL_NAME|Call Center Default NN|  
|DATE_CREATED|1/10/2008 5:07:38 PM|  
|LAST_PROCESSED|1/10/2008 5:24:02 PM|  
|PREDICTION_ENTITY|Average Time Per Issue,<br /><br /> Grade Of Service,<br /><br /> Number Of Orders|  
|MINING_PARAMETERS|HOLDOUT_PERCENTAGE=30, HOLDOUT_SEED=0,<br /><br /> MAXIMUM_INPUT_ATTRIBUTES=255, MAXIMUM_OUTPUT_ATTRIBUTES=255,<br /><br /> MAXIMUM_STATES=100, SAMPLE_SIZE=10000, HIDDEN_NODE_RATIO=4|  
  
###  <a name="bkmk_Query3"></a> Sample Query 3: Retrieving the Input Attributes for the Model  
 You can retrieve the input attribute-value pairs that were used to create the model by querying the child nodes (NODE_TYPE = 20) of the input layer (NODE_TYPE = 18). The following query returns a list of input attributes from the node descriptions.  
  
```  
SELECT NODE_DESCRIPTION  
FROM [Call Center Default NN].CONTENT  
WHERE NODE_TYPE = 2  
```  
  
 Example results:  
  
|NODE_DESCRIPTION|  
|-----------------------|  
|Average Time Per Issue=64.7094100096 - 77.4002099712|  
|Day Of Week=Fri.|  
|Level 1 Operators|  
  
 Only a few representative rows from the results are shown here. However, you can see that the NODE_DESCRIPTION provides slightly different information depending on the data type of the input attribute.  
  
-   If the attribute is a discrete or discretized value, the attribute and either its value or its discretized range are returned.  
  
-   If the attribute is a continuous numeric data type, the NODE_DESCRIPTION contains only the attribute name. However, you can retrieve the nested NODE_DISTRIBUTION table to obtain the mean, or return the NODE_RULE to obtain the minimum and maximum values of the numeric range.  
  
 The following query shows how to query the nested NODE_DISTRIBUTION table to return the attributes in one column, and their values in another column. For continuous attributes, the value of the attribute is represented by its mean.  
  
```  
SELECT FLATTENED   
(SELECT ATTRIBUTE_NAME, ATTRIBUTE_VALUE  
FROM NODE_DISTRIBUTION) as t  
FROM [Call Center Default NN -- Predict Service and Orders].CONTENT  
WHERE NODE_TYPE = 21  
```  
  
 Example results:  
  
|t.ATTRIBUTE_NAME|t.ATTRIBUTE_VALUE|  
|-----------------------|------------------------|  
|Average Time Per Issue|64.7094100096 - 77.4002099712|  
|Day Of Week|Fri.|  
|Level 1 Operators|3.2962962962963|  
  
 The minimum and maximum range values are stored in the NODE_RULE column, and are represented as an XML fragment, as shown in the following example:  
  
```  
<NormContinuous field="Level 1 Operators">    
  <LinearNorm orig="2.83967303681711" norm="-1" />    
  <LinearNorm orig="3.75291955577548" norm="1" />    
</NormContinuous>    
```  
  
###  <a name="bkmk_Query4"></a> Sample Query 4: Retrieving Weights from the Hidden Layer  
 The model content of a neural network model is structured in a way that makes it easy to retrieve details about any node in the network. Moreover, the ID numbers of the nodes provide information that helps you identify relationships among the node types.  
  
 The following query demonstrates how to retrieve the coefficients that are stored under a particular node of the hidden layer. The hidden layer consists of an organizer node (NODE_TYPE = 19), which contains only metadata, and multiple child nodes (NODE_TYPE = 22), which contain the coefficients for the various combinations of attributes and values. This query returns only the coefficient nodes.  
  
```  
SELECT FLATTENED TOP 1 NODE_UNIQUE_NAME,   
(SELECT ATTRIBUTE_NAME, ATTRIBUTE_VALUE, VALUETYPE  
FROM NODE_DISTRIBUTION) as t  
FROM  [Call Center Default NN -- Predict Service and Orders].CONTENT  
WHERE NODE_TYPE = 22  
AND [PARENT_UNIQUE_NAME] = '40000000200000000' FROM [Call Center Default NN].CONTENT  
```  
  
 Example results:  
  
|NODE_UNIQUE_NAME|t.ATTRIBUTE_NAME|t.ATTRIBUTE_VALUE|t.VALUETYPE|  
|------------------------|-----------------------|------------------------|-----------------|  
|70000000200000000|6000000000000000a|-0.178616518|7|  
|70000000200000000|6000000000000000b|-0.267561918|7|  
|70000000200000000|6000000000000000c|0.11069497|7|  
|70000000200000000|6000000000000000d|0.123757712|7|  
|70000000200000000|6000000000000000e|0.294565343|7|  
|70000000200000000|6000000000000000f|0.22245318|7|  
|70000000200000000||0.188805045|7|  
  
 The partial results shown here demonstrate how the neural network model content relates the hidden node to the input nodes.  
  
-   The unique names of nodes in the hidden layer always begin with 70000000.  
  
-   The unique names of nodes in the input layer always begin with 60000000.  
  
 Thus, these results tell you that the node denoted by the ID 70000000200000000 had six different coefficients (VALUETYPE = 7) passed to it. The values of the coefficients are in the ATTRIBUTE_VALUE column. You can determine exactly which input attribute the coefficient is for by using the node ID in the ATTRIBUTE_NAME column. For example, the node ID 6000000000000000a refers to input attribute and value, `Day of Week = 'Tue.'` You can use the node ID to create a query, or you can browse to the node by using the [Microsoft Generic Content Tree Viewer](../microsoft-generic-content-tree-viewer-data-mining.md).  
  
 Similarly, if you query the NODE_DISTRIBUTION table of the nodes in the output layer (NODE_TYPE = 23), you can see the coefficients for each output value. However, in the output layer, the pointers refer back to the nodes of the hidden layer. For more information, see [Mining Model Content for Neural Network Models &#40;Analysis Services - Data Mining&#41;](mining-model-content-for-neural-network-models-analysis-services-data-mining.md).  
  
## Using a Neural Network Model to Make Predictions  
 The [!INCLUDE[msCoName](../../includes/msconame-md.md)] Neural Network algorithm supports both classification and regression. You can use prediction functions with these models to provide new data and create either singleton or batch predictions.  
  
###  <a name="bkmk_Query5"></a> Sample Query 5: Creating a Singleton Prediction  
 The easiest way to build a prediction query on a neural network model is to use the Prediction Query Builder, available on the **Mining Prediction** tab of Data Mining Designer in both [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] and [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)]. You can browse the model in the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Neural Network Viewer to filter attributes of interest and view trends, and then switch to the **Mining Prediction** tab to create a query and predict new values for those trends.  
  
 For example, you can browse the call center model to view correlations between the order volumes and other attributes. To do this, open the model in the viewer, and for **Input**, select **\<All>**.  Next, for **Output**, select **Number of Orders**. For **Value 1**, select the range that represents the most orders, and for **Value 2**, select the range that represents the fewest orders. You can then see at a glance all the attributes that the model correlates with order volume.  
  
 By browsing the results in the viewer, you find that certain days of the week have low order volumes, and that an increase in the number of operators seems to be correlated with higher sales. You could then use a prediction query on the model to test a "what if" hypothesis and ask if increasing the number of level 2 operators on a low-volume day would increase orders. To do this, create a query such as the following:  
  
```  
SELECT Predict([Call Center Default NN].[Number of Orders]) AS [Predicted Orders],  
PredictProbability([Call Center Default NN].[Number of Orders]) AS [Probability]  
FROM [Call Center Default NN]  
NATURAL PREDICTION JOIN   
(SELECT 'Tue.' AS [Day of Week],  
13 AS [Level 2 Operators]) AS t  
```  
  
 Example results:  
  
|Predicted Orders|Probability|  
|----------------------|-----------------|  
|364|0.9532...|  
  
 The predicted sales volume is higher than the current range of sales for Tuesday, and the probability of the prediction is very high. However, you might want to create multiple predictions by using a batch process to test a variety of hypotheses on the model.  
  
> [!NOTE]  
>  The Data Mining Add-Ins for Excel 2007 provide logistic regression wizards that make it easy to answer complex questions, such as how many Level Two Operators would be needed to improve service grade to a target level for a specific shift. The data mining add-ins are a free download, and include wizards that are based on the neural network and/or logistic regression algorithms. For more information, see the [Data Mining Add-ins for Office 2007](https://go.microsoft.com/fwlink/?LinkID=117790) Web site.  
  
## List of Prediction Functions  
 All [!INCLUDE[msCoName](../../includes/msconame-md.md)] algorithms support a common set of functions. There are no prediction functions that are specific to the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Neural Network algorithm; however, the algorithm supports the functions that are listed in the following table.  
  
|||  
|-|-|  
|Prediction Function|Usage|  
|[IsDescendant &#40;DMX&#41;](/sql/dmx/isdescendant-dmx)|Determines whether one node is a child of another node in the neural network graph.|  
|[PredictAdjustedProbability &#40;DMX&#41;](/sql/dmx/predictadjustedprobability-dmx)|Returns the weighted probability.|  
|[PredictHistogram &#40;DMX&#41;](/sql/dmx/predicthistogram-dmx)|Returns a table of values related to the current predicted value.|  
|[PredictVariance &#40;DMX&#41;](/sql/dmx/predictvariance-dmx)|Returns variance for the predicted value.|  
|[PredictProbability &#40;DMX&#41;](/sql/dmx/predictprobability-dmx)|Returns probability  for the predicted value.|  
|[PredictStdev &#40;DMX&#41;](/sql/dmx/predictstdev-dmx)|Returns the standard deviance for the predicted value.|  
|[PredictSupport &#40;DMX&#41;](/sql/dmx/predictsupport-dmx)|For neural network and logistic regression models, returns a single value that represents the size of the training set for the entire model.|  
  
 For the syntax of specific functions, see [Data Mining Extensions &#40;DMX&#41; Function Reference](/sql/dmx/data-mining-extensions-dmx-function-reference).  
  
## See Also  
 [Microsoft Neural Network Algorithm](microsoft-neural-network-algorithm.md)   
 [Microsoft Neural Network Algorithm Technical Reference](microsoft-neural-network-algorithm-technical-reference.md)   
 [Mining Model Content for Neural Network Models &#40;Analysis Services - Data Mining&#41;](mining-model-content-for-neural-network-models-analysis-services-data-mining.md)   
 [Lesson 5: Building Neural Network and Logistic Regression Models &#40;Intermediate Data Mining Tutorial&#41;](../../tutorials/lesson-5-build-models-intermediate-data-mining-tutorial.md)  
  
  
