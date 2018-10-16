---
title: "AggregationFunction Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "AggregationFunction Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "AggregationFunction"
helpviewer_keywords: 
  - "AggregationFunction element"
ms.assetid: 40cfc7f9-1089-45f9-be90-a29770ed9682
author: minewiskan
ms.author: owend
manager: craigg
---
# AggregationFunction Element (ASSL)
  Contains the aggregation function to use for the account type.  
  
## Syntax  
  
```xml  
  
<Account>  
   ...  
   <AggregationFunction>...</AggregationFunction>  
   ...  
</Account>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|String (enumeration)|  
|Default value|*Sum*|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[Account](../objects/account-element-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The value of this element is limited to one of the following strings:  
  
|Value|Description|  
|-----------|-----------------|  
|*Sum*|The measure is aggregated using the `Sum` function.|  
|*Count*|The measure is aggregated using the `Count` function.|  
|*Min*|The measure is aggregated using the `Min` function.|  
|*Max*|The measure is aggregated using the `Max` function.|  
|*DistinctCount*|The measure is aggregated using the `DistinctCount` function.|  
|*None*|The measure is not aggregated.|  
|*AverageOfChildren*|The measure is aggregated by returning the average of its children.|  
|*FirstChild*|The measure is aggregated by returning its first child member.|  
|*LastChild*|The measure is aggregated by returning its last child member.|  
|*FirstNonEmpty*|The measure is aggregated by returning its first nonempty member.|  
|*LastNonEmpty*|The measure is aggregated by returning its last nonempty member.|  
  
 The enumeration that corresponds to the allowed values for `AggregationFunction` in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.AggregationFunction>.  
  
## See Also  
 [Accounts Element &#40;ASSL&#41;](../collections/accounts-element-assl.md)   
 [Properties &#40;ASSL&#41;](properties-assl.md)  
  
  
