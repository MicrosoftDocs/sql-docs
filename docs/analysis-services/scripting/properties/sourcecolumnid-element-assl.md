---
title: "SourceColumnID Element (ASSL) | Microsoft Docs"
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
  - "SourceColumnID Element"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
f1_keywords: 
  - "SourceColumnID"
helpviewer_keywords: 
  - "SourceColumnID element"
ms.assetid: 715c0be7-aa07-4dff-a909-9738224941ec
caps.latest.revision: 35
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# SourceColumnID Element (ASSL)
  Contains the identifier (ID) of the source mining structure column in the ancestor [MiningStructure](../../../analysis-services/scripting/objects/miningstructure-element-assl.md) element.  
  
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
|Parent element|[MiningModelColumn](../../../analysis-services/scripting/data-type/miningmodelcolumn-data-type-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The value of the **SourceColumnID** element matches the identifier of a mining structure column in the [Columns](../../../analysis-services/scripting/collections/columns-element-assl.md) collection of the parent **MiningStructure**.  
  
 The element that corresponds to the parent of **SourceColumnID** in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.MiningModelColumn>.  
  
## See Also  
 [Properties &#40;ASSL&#41;](../../../analysis-services/scripting/properties/properties-assl.md)  
  
  