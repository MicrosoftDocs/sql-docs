---
title: "Data Mining Extensions (DMX) Function Reference | Microsoft Docs"
ms.date: 06/07/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: dmx
ms.topic: conceptual
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# Data Mining Extensions (DMX) Function Reference
[!INCLUDE[ssas-appliesto-sqlas](../includes/ssas-appliesto-sqlas.md)]

  [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] supports several functions in the Data Mining Extensions (DMX) language. Functions expand the results of a prediction query to include information that further describes the prediction. Functions also provide more control over how the results of the prediction are returned. The following table provides links to resources to help you understand how to use functions in DMX.  
  
|Function|Description|  
|--------------|-----------------|  
|[General Prediction Functions &#40;DMX&#41;](../dmx/general-prediction-functions-dmx.md)|List functions that can be used with all model types, and provides links to more information about how to query specific types of mining models.|  
|[Structure and Usage of DMX Prediction Queries](../dmx/structure-and-usage-of-dmx-prediction-queries.md)|Provides an overview of how to construct a prediction query by using DMX.|  
|[BottomCount &#40;DMX&#41;](../dmx/bottomcount-dmx.md)|Returns a table that contains a specified number of bottom-most rows, in increasing order of rank based on a rank expression.|  
  
 The following table lists the functions that DMX supports.  
  
|Function|Description|  
|--------------|-----------------|  
|[BottomCount &#40;DMX&#41;](../dmx/bottomcount-dmx.md)|Returns a table that contains the last n-item rows of the table expression, in increasing order based on a rank expression.|  
|[BottomPercent &#40;DMX&#41;](../dmx/bottompercent-dmx.md)|Returns a table that contains the smallest number of bottom-most rows that meet a specified percent expression, in increasing order of rank based on a rank expression.|  
|[BottomSum &#40;DMX&#41;](../dmx/bottomsum-dmx.md)|Returns a table that contains the smallest number of bottom-most rows that meet a specified sum expression, in increasing order of rank based on a rank expression.|  
|[Cluster &#40;DMX&#41;](../dmx/cluster-dmx.md)|Returns the cluster that is most likely to contain the input case.|  
|[ClusterProbability &#40;DMX&#41;](../dmx/clusterprobability-dmx.md)|Returns the probability that the input case belongs to the cluster.|  
|[Exists &#40;DMX&#41;](../dmx/exists-dmx.md)|Returns true if the result set returned by the specified SELECT statement contains at least one row.|  
|[IsDescendant &#40;DMX&#41;](../dmx/isdescendant-dmx.md)|Indicates whether the current node descends from the specified node.|  
|[IsInNode &#40;DMX&#41;](../dmx/isinnode-dmx.md)|Indicates whether the specified node contains the case.|  
|[IsTestCase &#40;DMX&#41;](../dmx/istestcase-dmx.md)|Indicates whether a case belongs to the set of test cases.|  
|[IsTrainingCase &#40;DMX&#41;](../dmx/istrainingcase-dmx.md)|Indicates whether a case belongs to the set of training cases.|  
|[Lag &#40;DMX&#41;](../dmx/lag-dmx.md)|Returns the time slice between the date of the current case and the last date in the data.|  
|[Predict &#40;DMX&#41;](../dmx/predict-dmx.md)|Performs a prediction on a specified column.|  
|[PredictAdjustedProbability &#40;DMX&#41;](../dmx/predictadjustedprobability-dmx.md)|Returns the adjusted probability of the specified predictable column.|  
|[PredictAssociation &#40;DMX&#41;](../dmx/predictassociation-dmx.md)|Predicts associative membership in a column.|  
|[PredictCaseLikelihood &#40;DMX&#41;](../dmx/predictcaselikelihood-dmx.md)|Returns the likelihood that an input case will fit within the existing model. This function can only be used with clustering models.|  
|[PredictHistogram &#40;DMX&#41;](../dmx/predicthistogram-dmx.md)|Returns a table that represents the histogram for a specified column.|  
|[PredictNodeId &#40;DMX&#41;](../dmx/predictnodeid-dmx.md)|Returns the NodeID for a selected case.|  
|[PredictProbability &#40;DMX&#41;](../dmx/predictprobability-dmx.md)|Returns the probability of the specified column.|  
|[PredictSequence &#40;DMX&#41;](../dmx/predictsequence-dmx.md)|Predicts the next values in a sequence.|  
|[PredictStdev &#40;DMX&#41;](../dmx/predictstdev-dmx.md)|Retrieves the standard deviation value for a specified column.|  
|[PredictSupport &#40;DMX&#41;](../dmx/predictsupport-dmx.md)|Returns the support value of the column.|  
|[PredictTimeSeries &#40;DMX&#41;](../dmx/predicttimeseries-dmx.md)|Predicts the future values for a time series.|  
|[PredictVariance &#40;DMX&#41;](../dmx/predictvariance-dmx.md)|Returns the variance value of the specified column.|  
|[RangeMax &#40;DMX&#41;](../dmx/rangemax-dmx.md)|Returns the upper value of the predicted bucket that is discovered for a specified discretized column.|  
|[RangeMid &#40;DMX&#41;](../dmx/rangemid-dmx.md)|Returns the midpoint value of the predicted bucket that is discovered for a specified discretized column.|  
|[RangeMin &#40;DMX&#41;](../dmx/rangemin-dmx.md)|Returns the lower value of the predicted bucket that is discovered for a specified discretized column.|  
|[StructureColumn &#40;DMX&#41;](../dmx/structurecolumn-dmx.md)|Returns the value of the specified table mining structure column.|  
|[TopCount &#40;DMX&#41;](../dmx/topcount-dmx.md)|Returns a table that contains a specified number of topmost rows, in a decreasing order of rank based on a rank expression.|  
|[TopPercent &#40;DMX&#41;](../dmx/toppercent-dmx.md)|Returns a table that contains the smallest number of topmost rows that meet a specified percent expression, in a decreasing order of rank based on a rank expression.|  
|[TopSum &#40;DMX&#41;](../dmx/topsum-dmx.md)|Returns a table that contains the smallest number of topmost rows that meet a specified sum expression, in a decreasing order of rank based on a rank expression.|  
  
## See Also  
 [Data Mining Extensions &#40;DMX&#41; Operator Reference](../dmx/data-mining-extensions-dmx-operator-reference.md)   
 [Data Mining Extensions &#40;DMX&#41; Statement Reference](../dmx/data-mining-extensions-dmx-statements.md)   
 [Data Mining Extensions &#40;DMX&#41; Syntax Conventions](../dmx/data-mining-extensions-dmx-syntax-conventions.md)   
 [Data Mining Extensions &#40;DMX&#41; Syntax Elements](../dmx/data-mining-extensions-dmx-syntax-elements.md)   
 [General Prediction Functions &#40;DMX&#41;](../dmx/general-prediction-functions-dmx.md)   
 [Structure and Usage of DMX Prediction Queries](../dmx/structure-and-usage-of-dmx-prediction-queries.md)   
 [Understanding the DMX Select Statement](../dmx/understanding-the-dmx-select-statement.md)  
  
  
