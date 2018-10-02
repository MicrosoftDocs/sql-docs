---
title: "SourceColumnID Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "SourceColumnID Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "SourceColumnID"
helpviewer_keywords: 
  - "SourceColumnID element"
ms.assetid: 715c0be7-aa07-4dff-a909-9738224941ec
author: minewiskan
ms.author: owend
manager: craigg
---
# SourceColumnID Element (ASSL)
  Contains the identifier (ID) of the source mining structure column in the ancestor [MiningStructure](../objects/miningstructure-element-assl.md) element.  
  
## Syntax  
  
```xml  
  
<MiningModelColumn>  
   ...  
   <SourceColumnID>...</SourceColumnID>  
   ...  
</MiningModelColumn>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|String|  
|Default value|None|  
|Cardinality|1-1: Required element that occurs once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent element|[MiningModelColumn](../data-type/miningmodelcolumn-data-type-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The value of the `SourceColumnID` element matches the identifier of a mining structure column in the [Columns](../collections/columns-element-assl.md) collection of the parent `MiningStructure`.  
  
 The element that corresponds to the parent of `SourceColumnID` in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.MiningModelColumn>.  
  
## See Also  
 [Properties &#40;ASSL&#41;](properties-assl.md)  
  
  
