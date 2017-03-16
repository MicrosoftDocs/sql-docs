---
title: "Blocks Element (ASSL) | Microsoft Docs"
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
  - "Blocks Element"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
f1_keywords: 
  - "Blocks"
helpviewer_keywords: 
  - "Blocks element"
ms.assetid: d6fd4e6b-b5bd-43cd-9c28-48af57cf977c
caps.latest.revision: 32
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# Blocks Element (ASSL)
  Contains the collection of blocks of binary data that represent the binary contents of a [ClrAssemblyFile](../../../analysis-services/scripting/data-type/clrassemblyfile-data-type-assl.md) element.  
  
## Syntax  
  
```xml  
  
<Data xsi:type="DataBlock">  
   ...  
   <Blocks>  
      <Block>...</Block>  
   </Blocks>  
   ...  
</File>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|None|  
|Default value|None|  
|Cardinality|1-1: Required element that occurs once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[Data](../../../analysis-services/scripting/objects/data-element-assl.md) of type [DataBlock](../../../analysis-services/scripting/data-type/datablock-data-type-assl.md)|  
|Child elements|[Block](../../../analysis-services/scripting/objects/block-element-assl.md)|  
  
## Remarks  
 The element that corresponds to the parent of **Blocks** in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.ClrAssemblyFile>.  
  
## See Also  
 [Assembly Element &#40;ASSL&#41;](../../../analysis-services/scripting/objects/assembly-element-assl.md)   
 [ClrAssembly Data Type &#40;ASSL&#41;](../../../analysis-services/scripting/data-type/clrassembly-data-type-assl.md)   
 [Files Element &#40;ASSL&#41;](../../../analysis-services/scripting/collections/files-element-assl.md)   
 [File Element &#40;ASSL&#41;](../../../analysis-services/scripting/objects/file-element-assl.md)   
 [ClrAssemblyFile Data Type &#40;ASSL&#41;](../../../analysis-services/scripting/data-type/clrassemblyfile-data-type-assl.md)   
 [Data Element &#40;ASSL&#41;](../../../analysis-services/scripting/objects/data-element-assl.md)   
 [DataBlock Data Type &#40;ASSL&#41;](../../../analysis-services/scripting/data-type/datablock-data-type-assl.md)   
 [Blocks Element](../../../analysis-services/scripting/collections/blocks-element-assl.md)   
 [Block Element &#40;ASSL&#41;](../../../analysis-services/scripting/objects/block-element-assl.md)   
 [Collections &#40;ASSL&#41;](../../../analysis-services/scripting/collections/collections-assl.md)  
  
  