---
title: "SystemGetClusterCrossValidationResults (Analysis Services - Data Mining) | Microsoft Docs"
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
# SystemGetClusterCrossValidationResults (Analysis Services - Data Mining)
[!INCLUDE[ssas-appliesto-sqlas](../../includes/ssas-appliesto-sqlas.md)]
  Partitions the mining structure into the specified number of cross-sections, trains a model for each partition, and then returns accuracy metrics for each partition.  
  
 **Note** This stored procedure can be used only with a mining structure that contains at least one clustering model. To cross-validate non-clustering models, you must use [SystemGetCrossValidationResults &#40;Analysis Services - Data Mining&#41;](../../analysis-services/data-mining/systemgetcrossvalidationresults-analysis-services-data-mining.md).  
  
## Syntax  
  
```  
  
SystemGetClusterCrossValidationResults(  
<structure name>,   
[,<mining model list>]  
,<fold count>}  
,<max cases>  
<test list>])  
```  
  
## Arguments  
 *mining structure*  
 Name of a mining structure in the current database.  
  
 (required)  
  
 *mining model list*  
 Comma-separated list of mining models to validate.  
  
 If a list of mining models is not specified, cross-validation is performed against all clustering models that are associated with the specified structure.  
  
> [!NOTE]  
>  To cross-validate models that are not clustering models, you must use a separate stored procedure, [SystemGetCrossValidationResults &#40;Analysis Services - Data Mining&#41;](../../analysis-services/data-mining/systemgetcrossvalidationresults-analysis-services-data-mining.md).  
  
 (optional)  
  
 *fold count*  
 Integer that specifies the number of partitions into which to separate the data set. The minimum value is 2. The maximum number of folds is **maximum integer** or the number of cases, whichever is lower.  
  
 Each partition will contain roughly this number of cases: *max cases*/*fold count*.  
  
 There is no default value.  
  
> [!NOTE]  
>  The number of folds greatly affects the time required to perform cross-validation. If you select a number that is too high, the query may run for a very long time, and in some cases the server may become unresponsive or time out.  
  
 (required)  
  
 *max cases*  
 Integer that specifies the maximum number of cases that can be tested.  
  
 A value of 0 indicates that all the cases in the data source will be used.  
  
 If you specify a number that is higher than the actual number of cases in the data set, all cases in the data source will be used.  
  
 (required)  
  
 *test list*  
 A string that specifies testing options.  
  
 **Note** This parameter is reserved for future use.  
  
 (optional)  
  
## Return Type  
 The Return Type table contains scores for each individual partition and aggregates for all models.  
  
 The following table describes the columns returned.  
  
|Column Name|Description|  
|-----------------|-----------------|  
|ModelName|The name of the model that was tested.|  
|AttributeName|The name of the predictable column. For cluster models, always **null**.|  
|AttributeState|A specified target value in the predictable column. For cluster models, always **null.**|  
|PartitionIndex|An 1-based index that identifies which partition the results apply to.|  
|PartitionSize|An integer that indicates how many cases were included in each partition.|  
|Test|The type of test that was performed.|  
|Measure|The name of the measure returned by the test. Measures for each model depend on the type of the predictable value. For a definition of each measure, see [Cross-Validation &#40;Analysis Services - Data Mining&#41;](../../analysis-services/data-mining/cross-validation-analysis-services-data-mining.md).<br /><br /> For a list of measures returned for each predictable type, see [Measures in the Cross-Validation Report](../../analysis-services/data-mining/measures-in-the-cross-validation-report.md).|  
|Value|The value of the specified test measure.|  
  
## Remarks  
 To return accuracy metrics for the entire data set, use [SystemGetClusterAccuracyResults &#40;Analysis Services - Data Mining&#41;](../../analysis-services/data-mining/systemgetclusteraccuracyresults-analysis-services-data-mining.md).  
  
 Also, if the mining model has already been partitioned into folds, you can bypass processing and return only the results of cross-validation by using [SystemGetClusterAccuracyResults &#40;Analysis Services - Data Mining&#41;](../../analysis-services/data-mining/systemgetclusteraccuracyresults-analysis-services-data-mining.md).  
  
## Examples  
 The following example demonstrates how to partition a mining structure into three folds, and then test two clustering models that are associated with the mining structure.  
  
 Line three of the code lists the specific mining models that you want to test. If you do not specify the list, all clustering models associated with the structure are used.  
  
 Line four of the code specifies the number of folds, and line five specifies the maximum number of cases to use.  
  
 Because these are clustering models, you do not need to specify a predictable attribute or value.  
  
```  
CALL SystemGetClusterCrossValidationResults(  
[v Target Mail],  
[Cluster 1], [Cluster 2],  
3,  
10000  
)  
```  
  
 Sample results:  
  
|ModelName|AttributeName|AttributeState|PartitionIndex|PartitionSize|Test|Measure|Value|  
|---------------|-------------------|--------------------|--------------------|-------------------|----------|-------------|-----------|  
|Cluster 1|||1|3025|Clustering|Case Likelihood|0.930524511864121|  
|Cluster 1|||2|3025|Clustering|Case Likelihood|0.919184178430778|  
|Cluster 1|||3|3024|Clustering|Case Likelihood|0.929651120490248|  
|Cluster 2|||1|1289|Clustering|Case Likelihood|0.922789726933607|  
|Cluster 2|||2|1288|Clustering|Case Likelihood|0.934865535691068|  
|Cluster 2|||3|1288|Clustering|Case Likelihood|0.924724595688798|  
  
## Requirements  
 Cross-validation is available only in [!INCLUDE[ssEnterprise](../../includes/ssenterprise-md.md)] beginning in [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)].  
  
## See Also  
 [SystemGetCrossValidationResults &#40;Analysis Services - Data Mining&#41;](../../analysis-services/data-mining/systemgetcrossvalidationresults-analysis-services-data-mining.md)   
 [SystemGetAccuracyResults &#40;Analysis Services - Data Mining&#41;](../../analysis-services/data-mining/systemgetaccuracyresults-analysis-services-data-mining.md)   
 [SystemGetClusterCrossValidationResults](../../analysis-services/data-mining/systemgetclustercrossvalidationresults-analysis-services-data-mining.md)   
 [SystemGetClusterAccuracyResults &#40;Analysis Services - Data Mining&#41;](../../analysis-services/data-mining/systemgetclusteraccuracyresults-analysis-services-data-mining.md)  
  
  
