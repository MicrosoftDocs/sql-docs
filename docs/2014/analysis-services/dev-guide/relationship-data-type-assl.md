---
title: "Relationship Data Type (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
ms.assetid: 73d7c48d-d8e0-4119-849d-b5f912d449e4
caps.latest.revision: 4
author: "Minewiskan"
ms.author: "owend"
manager: "mblythe"
---
# Relationship Data Type (ASSL)
  Defines a primitive data type that represents a relationship in a dimension.  
  
## Syntax  
  
```xml  
  
<Relationship>  
   <ID>...</ID>  
   <Visible>...</Visible>  
   <FromRelationshipEnd>...</FromRelationshipEnd>  
   <ToRelationshipEnd>...</ToRelationshipEnd>  
</Relationship>  
```  
  
## Data Type Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Base data types|None|  
|Derived data types|None|  
  
## Data Type Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|None|  
|Child elements|[ID](../../../2014/analysis-services/dev-guide/id-element-assl.md), [Visible](../../../2014/analysis-services/dev-guide/visible-element-assl.md), [FromRelationshipEnd](../../../2014/analysis-services/dev-guide/relationshipend-data-type-assl.md), [ToRelationshipEnd](../../../2014/analysis-services/dev-guide/relationshipend-data-type-assl.md)|  
|Derived elements||  
  
## Remarks  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.Relationship>.  
  
## See Also  
 [Analysis Services Scripting Language XML Data Types &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/analysis-services-scripting-language-xml-data-types-assl.md)  
  
  