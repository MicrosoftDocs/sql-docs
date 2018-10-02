---
title: "Command Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "Command Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "Command"
helpviewer_keywords: 
  - "Command element"
ms.assetid: 277598b5-9939-4d7f-8c75-06470c3fabdd
author: minewiskan
ms.author: owend
manager: craigg
---
# Command Element (ASSL)
  Defines a command that is available for use within the context of the parent element of the [Commands](../collections/commands-element-assl.md) collection.  
  
## Syntax  
  
```xml  
  
<Commands>  
   <Command>  
      <Text>...</Text>  
   </Command>  
</Commands >  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|None|  
|Default value|None|  
|Cardinality|0-n: Optional element that can occur more than once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[Commands](../collections/commands-element-assl.md)|  
|Child elements|[Text](../properties/text-element-assl.md)|  
  
## Remarks  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.Command>.  
  
## See Also  
 [Objects &#40;ASSL&#41;](objects-assl.md)  
  
  
