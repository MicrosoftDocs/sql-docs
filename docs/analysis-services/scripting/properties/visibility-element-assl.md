---
title: "Visibility Element (ASSL) | Microsoft Docs"
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
  - "Visibility Element"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
f1_keywords: 
  - "Visibility"
helpviewer_keywords: 
  - "Visibility element"
ms.assetid: 59372ebf-af52-4d60-bf9b-bb1644ae9865
caps.latest.revision: 33
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# Visibility Element (ASSL)
  Defines the visibility of an [Annotation](../../../analysis-services/scripting/objects/annotation-element-assl.md) element.  
  
## Syntax  
  
```xml  
  
<Annotation>  
   ...  
   <Visibility>...</Visibility>  
   ...  
</Annotation>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|String (enumeration)|  
|Default value|*None*|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent element|[Annotation](../../../analysis-services/scripting/objects/annotation-element-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The value of this element is limited to one of the strings listed in the following table.  
  
|Value|Description|  
|-----------|-----------------|  
|*SchemaRowset*|The annotation is visible in the schema rowset.|  
|*None*|The annotation is not visible.|  
  
 The enumeration that corresponds to the allowed values for **Visibility** in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.Annotation>.  
  
## See Also  
 [Properties &#40;ASSL&#41;](../../../analysis-services/scripting/properties/properties-assl.md)  
  
  