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

Calculation groups are supported for tabular models at the 1470 and higher compatibility level. Models at the 1470 compatibility level cannot be deployed to SQL Server 2017 or earlier, or downgraded to a lower compatibility level. 1470 tabular models are supported on SQL Server Analysis Services 2019 and later only.

Calculation groups appear to users as a table with a single column.

 
## How they work 

### Sideways recursion

## Examples


## DAX functions

DAX includes three functions specific to working with calculation groups.

[ISSELECTEDMEASURE](https://docs.microsoft.com/dax/isselectedmeasure-function-dax) - Used by expressions for calculation items to determine the measure that is in context is one of those specified in a list of measures.

[SELECTEDMEASURE](https://docs.microsoft.com/dax/selectedmeasure-function-dax) - Used by expressions for calculation items to reference the measure that is in context.

[SELECTEDMEASURENAME](https://docs.microsoft.com/dax/selectedmeasurename-function-dax) - Used by expressions for calculation items to determine the measure that is in context by name.

## Data Management View (DMV) queries

By using [Data Management Views](../instances/use-dynamic-management-views-dmvs-to-monitor-analysis-services.md), you can query schema rowsets that return information about calculation groups in your model. The following schema rowsets have been introduced for calculation groups: 

- TMSCHEMA_CALCULATION_GROUPS   
- TMSCHEMA_CALCULATION_ITEMS   
 
## See also  

  
  
