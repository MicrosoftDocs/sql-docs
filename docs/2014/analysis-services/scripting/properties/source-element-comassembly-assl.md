---
title: "Source Element (ComAssembly) (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "Source Element (ComAssembly)"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "Source"
helpviewer_keywords: 
  - "Source element"
ms.assetid: 5c9209e8-ace6-4688-a64d-4987a7648ab9
author: minewiskan
ms.author: owend
manager: craigg
---
# Source Element (ComAssembly) (ASSL)
  Contains the file name or programmatic identifier (ProgID) for a Component Object Model (COM) component.  
  
## Syntax  
  
```xml  
  
<ComAssembly>  
   ...  
   <Source>...</Source>  
   ...  
</ComAssembly>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|String|  
|Default value|None|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent element|[ComAssembly](../data-type/assembly-data-type-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The element that corresponds to the parent of `Source` in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.ComAssembly>.  
  
## See Also  
 [Assembly Element &#40;ASSL&#41;](../objects/assembly-element-assl.md)   
 [ClrAssembly Data Type &#40;ASSL&#41;](../data-type/clrassembly-data-type-assl.md)   
 [Assemblies Element &#40;ASSL&#41;](../collections/assemblies-element-assl.md)   
 [Database Element &#40;ASSL&#41;](../objects/database-element-assl.md)   
 [Server Element &#40;ASSL&#41;](../objects/server-element-assl.md)   
 [Properties &#40;ASSL&#41;](properties-assl.md)  
  
  
