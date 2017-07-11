---
title: "Annotations Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/04/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
apiname: 
  - "Annotations Element"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
f1_keywords: 
  - "Annotations"
helpviewer_keywords: 
  - "Annotations element"
ms.assetid: b2236075-6a48-470d-8182-b0de112e258a
caps.latest.revision: 38
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# Annotations Element (ASSL)
  Contains the collection of [Annotation](../../../analysis-services/scripting/objects/annotation-element-assl.md) elements associated with the parent element.  
  
## Syntax  
  
```xml  
  
<Account>  
<!-- or another object in the Analysis Services Scripting Language -->  
   ...  
   <Annotations>  
      <Annotation>...</Annotation>  
   </Annotations>  
   ...  
</Account>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|None|  
|Default value|None|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|Most objects in the Analysis Services Scripting Language|  
|Child elements|[Annotation](../../../analysis-services/scripting/objects/annotation-element-assl.md)|  
  
## Remarks  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.AnnotationCollection>.  
  
  