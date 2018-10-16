---
title: "Files Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/09/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "Files Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "Files"
helpviewer_keywords: 
  - "Files element"
ms.assetid: 8a1327cb-1f60-42a7-b8ef-213d45a63e55
author: minewiskan
ms.author: owend
manager: craigg
---
# Files Element (ASSL)
  Contains the collection of [File](../objects/file-element-assl.md) elements that make up a [ClrAssembly](../data-type/assembly-data-type-assl.md) element.  
  
## Syntax  
  
```xml  
  
<Assembly xsi:type="ClrAssembly">  
   ...  
   <Files>  
      <File xsi:type="ClrAssemblyFile">...</File>  
   </Files>  
   ...  
</Assembly>  
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
|Parent elements|[Assembly](../objects/assembly-element-assl.md) of type [ClrAssembly](../data-type/assembly-data-type-assl.md)|  
|Child elements|[File](../objects/file-element-assl.md) of type [ClrAssemblyFile](../data-type/clrassemblyfile-data-type-assl.md)|  
  
## Remarks  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.ClrAssemblyFileCollection>.  
  
## See Also  
 [Server Element &#40;ASSL&#41;](../objects/server-element-assl.md)   
 [Database Element &#40;ASSL&#41;](../objects/database-element-assl.md)   
 [Assemblies Element &#40;ASSL&#41;](assemblies-element-assl.md)   
 [Data Element &#40;ASSL&#41;](../objects/data-element-assl.md)   
 [DataBlock Data Type &#40;ASSL&#41;](../data-type/datablock-data-type-assl.md)   
 [Blocks Element &#40;ASSL&#41;](blocks-element-assl.md)   
 [Block Element &#40;ASSL&#41;](../objects/block-element-assl.md)   
 [ComAssembly Data Type &#40;ASSL&#41;](../data-type/comassembly-data-type-assl.md)   
 [Collections &#40;ASSL&#41;](collections-assl.md)  
  
  
