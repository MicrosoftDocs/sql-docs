---
title: "Discretization Methods (Data Mining) | Microsoft Docs"
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
# Discretization Methods (Data Mining)
[!INCLUDE[ssas-appliesto-sqlas](../../includes/ssas-appliesto-sqlas.md)]
  Some algorithms that are used to create data mining models in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] require specific content types in order to function correctly. For example, the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Naive Bayes algorithm cannot use continuous columns as input and cannot predict continuous values. Additionally, some columns may contain so many values that the algorithm cannot easily identify interesting patterns in the data from which to create a model.  
  
 In these cases, you can discretize the data in the columns to enable the use of the algorithms to produce a mining model. *Discretization* is the process of putting values into buckets so that there are a limited number of possible states. The buckets themselves are treated as ordered and discrete values. You can discretize both numeric and string columns.  
  
 There are several methods that you can use to discretize data. If your data mining solution uses relational data, you can control the number of buckets to use for grouping data by setting the value of the <xref:Microsoft.AnalysisServices.ScalarMiningStructureColumn.DiscretizationBucketCount%2A> property. The default number of buckets is 5.  
  
 If your data mining solution uses data from an Online Analytical Processing (OLAP) cube, the data mining algorithm automatically computes the number of buckets to generate by using the following equation, where n is the number of distinct values of data in the column:  
  
 `Number of Buckets = sqrt(n)`  
  
 If you do not want [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] to calculate the number of buckets, you can use the <xref:Microsoft.AnalysisServices.DimensionAttribute.DiscretizationBucketCount%2A> property to manually specify the number of buckets.  
  
 The following table describes the methods that you can use to discretize data in [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)].  
  
|Discretization method|Description|  
|---------------------------|-----------------|  
|**AUTOMATIC**|[!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] determines which discretization method to use.|  
|**CLUSTERS**|The algorithm divides the data into groups by sampling the training data, initializing to a number of random points, and then running several iterations of the Microsoft Clustering algorithm using the Expectation Maximization (EM) clustering method. The **CLUSTERS** method is useful because it works on any distribution curve. However, it requires more processing time than the other discretization methods.<br /><br /> This method can only be used with numeric columns.|  
|**EQUAL_AREAS**|The algorithm divides the data into groups that contain an equal number of values. This method is best used for normal distribution curves, but does not work well if the distribution includes a large number of values that occur in a narrow group in the continuous data. For example, if one-half of the items have a cost of 0, one-half the data will occur under a single point in the curve. In such a distribution, this method breaks the data up in an effort to establish equal discretization into multiple areas. This produces an inaccurate representation of the data.|  
  
## Remarks  
  
-   You can use the **EQUAL_AREAS** method to discretize strings.  
  
-   The **CLUSTERS** method uses a random sample of 1000 records to discretize data. Use the **EQUAL_AREAS** method if you do not want the algorithm to sample data.  
  
  
  
## See Also  
 [Content Types &#40;Data Mining&#41;](../../analysis-services/data-mining/content-types-data-mining.md)   
 [Content Types &#40;DMX&#41;](../../dmx/content-types-dmx.md)   
 [Data Mining Algorithms &#40;Analysis Services - Data Mining&#41;](../../analysis-services/data-mining/data-mining-algorithms-analysis-services-data-mining.md)   
 [Mining Structures &#40;Analysis Services - Data Mining&#41;](../../analysis-services/data-mining/mining-structures-analysis-services-data-mining.md)   
 [Data Types &#40;Data Mining&#41;](../../analysis-services/data-mining/data-types-data-mining.md)   
 [Mining Structure Columns](../../analysis-services/data-mining/mining-structure-columns.md)   
 [Column Distributions &#40;Data Mining&#41;](../../analysis-services/data-mining/column-distributions-data-mining.md)  
  
  
