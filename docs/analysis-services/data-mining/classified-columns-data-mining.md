---
title: "Classified Columns (Data Mining) | Microsoft Docs"
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
# Classified Columns (Data Mining)
[!INCLUDE[ssas-appliesto-sqlas](../../includes/ssas-appliesto-sqlas.md)]
  When you define a classified column, you create a relationship between the current column and another column in the mining structure. The data in the mining structure column that you designate as the classified column contains categorical information that describes the values in another column in the mining structure.  
  
 For example, suppose you have two columns with numerical data: one column, [Yearly Purchases], contains the total yearly purchases per customer for a specific calendar year, and the other column, [Standard Deviations], contains the standard deviations for those values. In this case you could designate the [Yearly Purchases] column as the classified column, and the model would be able to use this relationship in analysis.  
  
> [!NOTE]  
>  The algorithms provided in [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] do not support the use of classified columns; this feature is provided for use in creating custom algorithms.  
  
## Defining a Classified Column  
 The data type of a classified column must be either **Long** or **Double**.  
  
 The following list describes the content types that [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] supports for classified columns.  
  
 **PROBABILITY**  
 The value in the column is the probability of the associated value, and is a number between 0 and 1.  
  
 **VARIANCE**  
 The value in the column is the variance of the associated value.  
  
 **STDEV**  
 The value in the column is the standard deviation of the associated value.  
  
 **PROBABILITY_VARIANCE**  
 The value in the column is the variance of the probability for the associated value.  
  
 **PROBABILITY_STDEV**  
 The value in the column is the standard deviation of the probability for the associated value.  
  
 **SUPPORT**  
 The value in the column is the weight, or case replication factor, of the associated value.  
  
## See Also  
 [Content Types &#40;Data Mining&#41;](../../analysis-services/data-mining/content-types-data-mining.md)   
 [Mining Structures &#40;Analysis Services - Data Mining&#41;](../../analysis-services/data-mining/mining-structures-analysis-services-data-mining.md)   
 [Data Types &#40;Data Mining&#41;](../../analysis-services/data-mining/data-types-data-mining.md)  
  
  
