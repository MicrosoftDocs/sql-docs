---
title: "Calculation groups in Analysis Services tabular models | Microsoft Docs"
ms.date: 05/07/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: tabular-models
ms.topic: conceptual
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# Calculation groups
 
[!INCLUDE[ssas-appliesto-sqlas-aas](../../includes/ssas-appliesto-sqlas-aas.md)]


  
##  <a name="what"></a> Benefits  

Calculation groups address the issue of proliferation of measures in complex BI models often caused by common calculations like time-intelligence. Enterprise models are reused throughout large organizations, so they grow in scale and complexity. It is not uncommon for Analysis Services models to have hundreds of base measures. Each base measure often requires the same time-intelligence analysis. For example, a Sales Count may require: Sales MTD, Sales QTD, Sales YTD, Sales PY, Sales YOY%, and so on, and an Orders Count may require: Orders MTD, Orders QTD, Orders YTD, Orders PY, Orders YOY%, …

As you can see, this can easily explode the number of measures. If a model has 100 base measures and each requires 10 time-intelligence representations, the model ends up with 1,000 measures in total (100*10). This creates the following problems.

- The user experience is overwhelming because must sift through so many measures
- DAX is difficult to maintain
- Model metadata is bloated

Calculation groups address these issues. They are presented to end-users as a table with a single column. Each value in the column represents a reusable calculation that can be applied to any of the measures where it makes sense. The reusable calculations are called *calculation items*.

By reducing the number of measures, calculation groups present an uncluttered user interface to end users. They are an elegant way to manage DAX business logic. Users simply select calculation groups in the field list to view the calculations in Power BI visuals. There is no need for the end user or modeler to create separate measures.

## Examples


## DAX functions

DAX includes three functions specific to working with calculation groups.

[ISSELECTEDMEASURE](https://docs.microsoft.com/dax/isselectedmeasure-function-dax) - Used by expressions for calculation items to determine the measure that is in context is one of those specified in a list of measures.

[SELECTEDMEASURE](https://docs.microsoft.com/dax/selectedmeasure-function-dax) - Used by expressions for calculation items to reference the measure that is in context.

[SELECTEDMEASURENAME](https://docs.microsoft.com/dax/selectedmeasurename-function-dax) - Used by expressions for calculation items to determine the measure that is in context by name.


 
## See also  
\ 
  
  
