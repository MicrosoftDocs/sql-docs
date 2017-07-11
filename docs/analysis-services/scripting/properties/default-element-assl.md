---
title: "Default Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
apiname: 
  - "Default Element"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
f1_keywords: 
  - "DEFAULT"
helpviewer_keywords: 
  - "Default element"
ms.assetid: 02c1844c-51fb-44fe-aafb-001e53ad293c
caps.latest.revision: 35
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# Default Element (ASSL)
  Determines whether the [DrillThroughAction](../../../analysis-services/scripting/data-type/drillthroughaction-data-type-assl.md) is the default drillthrough action.  
  
## Syntax  
  
```xml  
  
<DrillThroughAction>  
   ...  
   <Default>...</Default  
   ...  
</ServerProperty>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|Boolean|  
|Default value|**False**|  
|Cardinality|0-1: Optional element that occurs once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[DrillThroughAction](../../../analysis-services/scripting/data-type/drillthroughaction-data-type-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The element that corresponds to the parent of **Default** in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.DrillThroughAction>.  
  
## See Also  
 [Properties &#40;ASSL&#41;](../../../analysis-services/scripting/properties/properties-assl.md)  
  
  