---
title: "Block Element (ASSL) | Microsoft Docs"
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
  - "Block Element"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
f1_keywords: 
  - "Block"
helpviewer_keywords: 
  - "Block element"
ms.assetid: f45c32ae-e4e0-465a-9564-9ccfb10a858f
caps.latest.revision: 37
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# Block Element (ASSL)
  Contains all or a portion of the binary contents of a [ClrAssemblyFile](../../../analysis-services/scripting/data-type/clrassemblyfile-data-type-assl.md) element.  
  
## Syntax  
  
```xml  
  
<Blocks>  
   <Block>...</Block>  
</Blocks>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|Base64Binary|  
|Default value|None|  
|Cardinality|1-n: Required element that can occur more than once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[Blocks](../../../analysis-services/scripting/collections/blocks-element-assl.md)|  
|Child elements|None|  
  
## See Also  
 [Assembly Element &#40;ASSL&#41;](../../../analysis-services/scripting/objects/assembly-element-assl.md)   
 [ClrAssembly Data Type &#40;ASSL&#41;](../../../analysis-services/scripting/data-type/clrassembly-data-type-assl.md)   
 [Files Element &#40;ASSL&#41;](../../../analysis-services/scripting/collections/files-element-assl.md)   
 [File Element &#40;ASSL&#41;](../../../analysis-services/scripting/objects/file-element-assl.md)   
 [ClrAssemblyFile Data Type &#40;ASSL&#41;](../../../analysis-services/scripting/data-type/clrassemblyfile-data-type-assl.md)   
 [Data Element &#40;ASSL&#41;](../../../analysis-services/scripting/objects/data-element-assl.md)   
 [DataBlock Data Type &#40;ASSL&#41;](../../../analysis-services/scripting/data-type/datablock-data-type-assl.md)   
 [Objects &#40;ASSL&#41;](../../../analysis-services/scripting/objects/objects-assl.md)  
  
  