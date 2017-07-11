---
title: "ClassifiedColumnID Element (ASSL) | Microsoft Docs"
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
  - "ClassifiedColumnID Element"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
f1_keywords: 
  - "ClassifiedColumnID"
helpviewer_keywords: 
  - "ClassifiedColumnID element"
ms.assetid: c294b9c5-3ac2-4554-8ba8-d9f15d7e85c0
caps.latest.revision: 35
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# ClassifiedColumnID Element (ASSL)
  Contains the identifier (ID) of a related column that is classified by the [ScalarMiningStructureColumn](../../../analysis-services/scripting/data-type/scalarminingstructurecolumn-data-type-assl.md) element.  
  
## Syntax  
  
```xml  
  
<ClassifiedColumns>  
   <ClassifiedColumnID>...</<ClassifiedColumnID>  
</ClassifiedColumns>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|String|  
|Default value|None|  
|Cardinality|0-n: Optional element that can occur more than once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent element|[ClassifiedColumns](../../../analysis-services/scripting/collections/classifiedcolumns-element-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The element that corresponds to the parent of the **ClassifiedColumns** collection in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.ScalarMiningStructureColumn>.  
  
## See Also  
 [Content Element &#40;ASSL&#41;](../../../analysis-services/scripting/properties/content-element-assl.md)   
 [Properties &#40;ASSL&#41;](../../../analysis-services/scripting/properties/properties-assl.md)  
  
  