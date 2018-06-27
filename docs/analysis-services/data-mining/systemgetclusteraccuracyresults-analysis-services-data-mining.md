---
title: "SystemGetClusterAccuracyResults (Analysis Services - Data Mining) | Microsoft Docs"
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
# SystemGetClusterAccuracyResults (Analysis Services - Data Mining)
[!INCLUDE[ssas-appliesto-sqlas](../../includes/ssas-appliesto-sqlas.md)]
  Returns cross-validation accuracy metrics for a mining structure and related clustering models.  
  
 This stored procedure returns metrics for the entire data set as a single partition. To partition the dataset into cross-sections and return metrics for each partition, use [SystemGetClusterCrossValidationResults &#40;Analysis Services - Data Mining&#41;](../../analysis-services/data-mining/systemgetclustercrossvalidationresults-analysis-services-data-mining.md).  
  
> [!NOTE]  
>  This stored procedure works only for clustering models. For non-clustering models, use [SystemGetAccuracyResults &#40;Analysis Services - Data Mining&#41;](../../analysis-services/data-mining/systemgetaccuracyresults-analysis-services-data-mining.md).  
  
## Syntax  
  
```  
  
SystemGetClusterAccuracyResults(  
<mining structure>   
[,<mining model list>]  
,<data set>  
,<test list>])  
```  
  
## Arguments  
 *mining structure*  
 Name of a mining structure in the current database.  
  
 (Required)  
  
 *mining model list*  
 Comma-separated list of models to validate.  
  
 The default is **null**, meaning that all applicable models are used. When the default is used, non-clustering models are automatically excluded from the list of candidates for processing.  
  
 (Optional)  
  
 *data set*  
 An integer value that indicates which partition in the mining structure is to be used for testing. The value is derived from a bitmask that represents the sum of the following values, where any single value is optional:  
  
|||  
|-|-|  
|Training cases|0x0001|  
|Test cases|0x0002|  
|Model filter|0x0004|  
  
 For a complete list of possible values, see the Remarks section of this topic.  
  
 (Required)  
  
 *test list*  
 A string that specifies testing options. This parameter is reserved for future use.  
  
 (optional)  
  
## Return Type  
 A table that contains scores for each individual partition and aggregates for all models.  
  
 The following table lists the columns returned by **SystemGetClusterAccuracyResults**. To learn more about how to interpret the information returned by the stored procedure, see [Measures in the Cross-Validation Report](../../analysis-services/data-mining/measures-in-the-cross-validation-report.md).  
  
|Column Name|Description|  
|-----------------|-----------------|  
|ModelName|The name of the model that was tested. **All** indicates that the result is an aggregate for all models.|  
|AttributeName|Not applicable to clustering models.|  
|AttributeState|Not applicable to clustering models.|  
|PartitionIndex|A number that indicates the partition.<br /><br /> For this stored procedure, the number is always 0.|  
|PartitionCases|An integer that indicates how many cases have been tested.|  
|Test|The type of test that was performed.|  
|Measure|The name of the measure returned by the test. Measures for each model depend on the model type, and the type of the predictable value.<br /><br /> For a list of measures returned for each predictable type, see [Measures in the Cross-Validation Report](../../analysis-services/data-mining/measures-in-the-cross-validation-report.md).<br /><br /> For a definition of each measure, see [Cross-Validation &#40;Analysis Services - Data Mining&#41;](../../analysis-services/data-mining/cross-validation-analysis-services-data-mining.md).|  
|Value|A probability score that indicates the cluster case likelihood.|  
  
## Remarks  
 The following table provides examples of the values that you can use to specify the data in the mining structure that is used for cross-validation. If you want to use test cases for cross-validation, the mining structure must already contain a testing data set. For information about how to define a testing data set when you create a mining structure, see [Training and Testing Data Sets](../../analysis-services/data-mining/training-and-testing-data-sets.md).  
  
|Integer Value|Description|  
|-------------------|-----------------|  
|1|Only training cases are used.|  
|2|Only test cases are used.|  
|3|Both the training cases and testing cases are used.|  
|4|Invalid combination.|  
|5|Only training cases are used, and the model filter is applied.|  
|6|Only test cases are used, and the model filter is applied.|  
|7|Both the training and testing cases are used, and the model filter is applied.|  
  
 For more information about the scenarios in which you would use cross-validation, see [Testing and Validation &#40;Data Mining&#41;](../../analysis-services/data-mining/testing-and-validation-data-mining.md).  
  
## Examples  
 This example returns accuracy measures for two clustering models, named `Cluster 1` and `Cluster 2`, that are  associated with the vTargetMail mining structure. The code on line four indicates that the results should be based on the testing cases alone, without using any filters that might be associated with each model.  
  
```  
CALL SystemGetClusterAccuracyResults (  
[vTargetMail],  
[Cluster 1], [Cluster 2],  
2  
)  
```  
  
 Sample Results:  
  
|ModelName|AttributeName|AttributeState|PartitionIndex|PartitionSize|Test|Measure|Value|  
|---------------|-------------------|--------------------|--------------------|-------------------|----------|-------------|-----------|  
|Cluster 1|||0|5545|Clustering|Case Likelihood|0.796514342249313|  
|Cluster 2|||0|5545|Clustering|Case Likelihood|0.732122471228572|  
  
## Requirements  
 Cross-validation is available only in [!INCLUDE[ssEnterprise](../../includes/ssenterprise-md.md)] beginning in [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)].  
  
## See Also  
 [SystemGetCrossValidationResults &#40;Analysis Services - Data Mining&#41;](../../analysis-services/data-mining/systemgetcrossvalidationresults-analysis-services-data-mining.md)   
 [SystemGetAccuracyResults &#40;Analysis Services - Data Mining&#41;](../../analysis-services/data-mining/systemgetaccuracyresults-analysis-services-data-mining.md)   
 [SystemGetClusterCrossValidationResults &#40;Analysis Services - Data Mining&#41;](../../analysis-services/data-mining/systemgetclustercrossvalidationresults-analysis-services-data-mining.md)   
 [SystemClusterGetAccuracyResults](../../analysis-services/data-mining/systemgetclusteraccuracyresults-analysis-services-data-mining.md)  
  
  
