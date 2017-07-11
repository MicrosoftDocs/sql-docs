---
title: "DataBlock Data Type (ASSL) | Microsoft Docs"
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
  - "DataBlock Data Type"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
f1_keywords: 
  - "DataBlock"
helpviewer_keywords: 
  - "DataBlock data type"
ms.assetid: 4192b388-613a-472b-881c-f9c02215aa81
caps.latest.revision: 33
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# DataBlock Data Type (ASSL)
  Defines a primitive data type that represents a collection of data blocks used to store the binary contents of a [ClrAssemblyFile](../../../analysis-services/scripting/data-type/clrassemblyfile-data-type-assl.md) element.  
  
## Syntax  
  
```xml  
  
<DataBlock>  
   <Blocks>...</Blocks>  
</DataBlock>  
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
|Child elements|[Blocks](../../../analysis-services/scripting/collections/blocks-element-assl.md)|  
|Derived elements|[Data](../../../analysis-services/scripting/objects/data-element-assl.md) element of [ClrAssemblyFile](../../../analysis-services/scripting/data-type/clrassemblyfile-data-type-assl.md) type ([Files](../../../analysis-services/scripting/collections/files-element-assl.md) collection of [ClrAssembly](../../../analysis-services/scripting/data-type/clrassembly-data-type-assl.md) type)|  
  
## See Also  
 [Assembly Element &#40;ASSL&#41;](../../../analysis-services/scripting/objects/assembly-element-assl.md)   
 [File Element &#40;ASSL&#41;](../../../analysis-services/scripting/objects/file-element-assl.md)   
 [Block Element &#40;ASSL&#41;](../../../analysis-services/scripting/objects/block-element-assl.md)   
 [Analysis Services Scripting Language XML Data Types &#40;ASSL&#41;](../../../analysis-services/scripting/data-type/analysis-services-scripting-language-xml-data-types-assl.md)  
  
  