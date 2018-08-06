---
title: "SystemGetCrossValidationResults (Analysis Services - Data Mining) | Microsoft Docs"
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
# SystemGetCrossValidationResults (Analysis Services - Data Mining)
[!INCLUDE[ssas-appliesto-sqlas](../../includes/ssas-appliesto-sqlas.md)]
  Partitions the mining structure into the specified number of cross-sections, trains a model for each partition, and then returns accuracy metrics for each partition.  
  
> [!NOTE]  
>  This stored procedure cannot be used to cross-validate clustering models, or models that are built by using the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Time Series algorithm or the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Sequence Clustering algorithm. To cross-validate clustering models, you can use the separate stored procedure, [SystemGetClusterCrossValidationResults &#40;Analysis Services - Data Mining&#41;](../../analysis-services/data-mining/systemgetclustercrossvalidationresults-analysis-services-data-mining.md).  
  
## Syntax  
  
```  
  
SystemGetCrossValidationResults(  
<mining structure>  
[, <mining model list>]  
,<fold count>  
,<max cases>  
,<target attribute>  
[,<target state>]  
[,<target threshold>]  
[,<test list>])  
```  
  
## Arguments  
 *mining structure*  
 Name of a mining structure in the current database.  
  
 (required)  
  
 *mining model list*  
 Comma-separated list of mining models to validate.  
  
 If a model name contains any characters that are not valid in the name of an identifier, the name must be enclosed in brackets.  
  
 If a list of mining models is not specified, cross-validation is performed against all models that are associated with the specified structure and that contain a predictable attribute.  
  
> [!NOTE]  
>  To cross-validate clustering models, you must use a separate stored procedure, [SystemGetClusterCrossValidationResults &#40;Analysis Services - Data Mining&#41;](../../analysis-services/data-mining/systemgetclustercrossvalidationresults-analysis-services-data-mining.md).  
  
 (optional)  
  
 *fold count*  
 Integer that specifies the number of partitions into which to separate the data set. The minimum value is 2. The maximum number of folds is **maximum integer** or the number of cases, whichever is lower.  
  
 Each partition will contain roughly this number of cases: *max cases*/*fold count*.  
  
 There is no default value.  
  
> [!NOTE]  
>  The number of folds greatly affects the time that is required to perform cross-validation. If you select a number that is too high, the query might run for a very long time, and in some cases the server can become unresponsive or time out.  
  
 (required)  
  
 *max cases*  
 Integer that specifies the maximum number of cases that can be tested across all folds.  
  
 A value of 0 indicates that all the cases in the data source will be used.  
  
 If you specify a value that is greater than the actual number of cases in the data set, all cases in the data source will be used.  
  
 There is no default value.  
  
 (required)  
  
 *target attribute*  
 String that contains the name of the predictable attribute. A predictable attribute can be a column, nested table column, or nested table key column of a mining model.  
  
> [!NOTE]  
>  The existence of the target attribute is validated only at run time.  
  
 (required)  
  
 *target state*  
 Formula that specifies the value to predict. If a target value is specified, metrics are collected for the specified value only.  
  
 If a value is not specified or is **null**, the metrics are computed for the most probable state for each prediction.  
  
 The default is **null**.  
  
 An error is raised during validation if the specified value is not valid for the specified attribute, or if the formula is not the correct type for the specified attribute.  
  
 (optional)  
  
 *target*  *threshold*  
 **Double** greater than 0 and less than 1. Indicates the minimum probability score that must be obtained for the prediction of the specified target state to be counted as correct.  
  
 A prediction that has a probability less than or equal to this value is considered incorrect.  
  
 If no value is specified or is **null**, the most probable state is used, regardless of its probability score.  
  
 The default is **null**.  
  
> [!NOTE]  
>  [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] will not raise an error if you set *state threshold* to 0.0, but you should never use this value. In effect, a threshold of 0.0 means that predictions with a 0 percent probability are counted as correct.  
  
 (optional)  
  
 *test list*  
 A string that specifies testing options.  
  
 **Note** This parameter is reserved for future use.  
  
 (optional)  
  
## Return Type  
 The rowset that is returned contains scores for each partition in each model.  
  
 The following table describes the columns in the rowset.  
  
|Column Name|Description|  
|-----------------|-----------------|  
|ModelName|The name of the model that was tested.|  
|AttributeName|The name of the predictable column.|  
|AttributeState|A specified target value in the predictable column. If this value is **null**, the most probable prediction was used.<br /><br /> If this column contains a value, the accuracy of the model is assessed against this value only.|  
|PartitionIndex|An 1-based index that identifies to which partition the results apply.|  
|PartitionSize|An integer that indicates how many cases were included in each partition.|  
|Test|Category of the test that was performed. For a description of the categories and the tests that are included in each category, see [Measures in the Cross-Validation Report](../../analysis-services/data-mining/measures-in-the-cross-validation-report.md).|  
|Measure|The name of the measure returned by the test. Measures for each model depend on the type of the predictable value. For a definition of each measure, see [Cross-Validation &#40;Analysis Services - Data Mining&#41;](../../analysis-services/data-mining/cross-validation-analysis-services-data-mining.md).<br /><br /> For a list of measures returned for each predictable type, see [Measures in the Cross-Validation Report](../../analysis-services/data-mining/measures-in-the-cross-validation-report.md).|  
|Value|The value of the specified test measure.|  
  
## Remarks  
 To return accuracy metrics for the complete data set, use [SystemGetAccuracyResults &#40;Analysis Services - Data Mining&#41;](../../analysis-services/data-mining/systemgetaccuracyresults-analysis-services-data-mining.md).  
  
 If the mining model has already been partitioned into folds, you can bypass processing and return only the results of cross-validation by using [SystemGetAccuracyResults &#40;Analysis Services - Data Mining&#41;](../../analysis-services/data-mining/systemgetaccuracyresults-analysis-services-data-mining.md).  
  
## Examples  
 The following example demonstrates how to partition a mining structure for cross-validation into two folds, and then test two mining models that are associated with the mining structure, `[v Target Mail]`.  
  
 Line three of the code lists the mining models that you want to test. If you do not specify the list, all non-clustering models associated with the structure are used. Line four of the code specifies the number of partitions. Because no value is specified for *max cases*, all cases in the mining structure are used and distributed evenly across the partitions.  
  
 Line five specifies the predictable attribute, Bike Buyer, and line six specifies the value to predict, 1 (meaning "yes, will buy").  
  
 The NULL value in line seven indicates that there is no minimum probability bar that must be met. Therefore, the first prediction that has a non-zero probability will be used in assessing accuracy.  
  
```  
CALL SystemGetCrossValidationResults(  
[v Target Mail],  
[Target Mail DT], [Target Mail NB],  
2,  
'Bike Buyer',  
1,  
NULL  
)  
```  
  
 Sample results:  
  
|ModelName|AttributeName|AttributeState|PartitionIndex|PartitionSize|Test|Measure|Value|  
|---------------|-------------------|--------------------|--------------------|-------------------|----------|-------------|-----------|  
|Target Mail DT|Bike Buyer|1|1|500|Classification|True Positive|144|  
|Target Mail DT|Bike Buyer|1|1|500|Classification|False Positive|105|  
|Target Mail DT|Bike Buyer|1|1|500|Classification|True Negative|186|  
|Target Mail DT|Bike Buyer|1|1|500|Classification|False Negative|65|  
|Target Mail DT|Bike Buyer|1|1|500|Likelihood|Log Score|-0.619042807138345|  
|Target Mail DT|Bike Buyer|1|1|500|Likelihood|Lift|0.0740963734002671|  
|Target Mail DT|Bike Buyer|1|1|500|Likelihood|Root Mean Square Error|0.346946279977653|  
|Target Mail DT|Bike Buyer|1|2|500|Classification|True Positive|162|  
|Target Mail DT|Bike Buyer|1|2|500|Classification|False Positive|86|  
|Target Mail DT|Bike Buyer|1|2|500|Classification|True Negative|165|  
|Target Mail DT|Bike Buyer|1|2|500|Classification|False Negative|87|  
|Target Mail DT|Bike Buyer|1|2|500|Likelihood|Log Score|-0.654117781086519|  
|Target Mail DT|Bike Buyer|1|2|500|Likelihood|Lift|0.038997399132084|  
|Target Mail DT|Bike Buyer|1|2|500|Likelihood|Root Mean Square Error|0.342721344892651|  
  
## Requirements  
 Cross-validation is available only in [!INCLUDE[ssEnterprise](../../includes/ssenterprise-md.md)] beginning with [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)].  
  
## See Also  
 [SystemGetCrossValidationResults](../../analysis-services/data-mining/systemgetcrossvalidationresults-analysis-services-data-mining.md)   
 [SystemGetAccuracyResults &#40;Analysis Services - Data Mining&#41;](../../analysis-services/data-mining/systemgetaccuracyresults-analysis-services-data-mining.md)   
 [SystemGetClusterCrossValidationResults &#40;Analysis Services - Data Mining&#41;](../../analysis-services/data-mining/systemgetclustercrossvalidationresults-analysis-services-data-mining.md)   
 [SystemGetClusterAccuracyResults &#40;Analysis Services - Data Mining&#41;](../../analysis-services/data-mining/systemgetclusteraccuracyresults-analysis-services-data-mining.md)  
  
  
