---
title: "Type Element (ClrAssemblyFile) (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "Type Element (ClrAssemblyFile)"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "TYPE"
helpviewer_keywords: 
  - "Type element"
ms.assetid: ab9e1e2c-ab06-4cd1-b007-16d738dc5604
author: minewiskan
ms.author: owend
manager: craigg
---
# Type Element (ClrAssemblyFile) (ASSL)
  Specifies the file type of one of the files that belong to a [!INCLUDE[msCoName](../../../includes/msconame-md.md)] .NET Framework assembly.  
  
## Syntax  
  
```xml  
  
<ClrAssemblyFile>  
      ...  
      <Type>...</Type>  
   ...  
</ClrAssemblyFile>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|String (enumeration)|  
|Default value|None|  
|Cardinality|1-1: Required element that occurs once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent element|[ClrAssemblyFile](../data-type/clrassemblyfile-data-type-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The value of this element is limited to one of the following strings:  
  
|Value|Description|  
|-----------|-----------------|  
|*Main*|Specified file is the main file in the assembly.|  
|*Dependent*|Specified file is a dependent file in the assembly.|  
|*Debug*|Specified file contains debugging information.|  
  
 The enumeration that corresponds to the allowed values for `Type` in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.ClrAssemblyFileType>.  
  
 The element that corresponds to the parent of `Type` in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.ClrAssemblyFile>.  
  
## See Also  
 [File Element &#40;ASSL&#41;](../objects/file-element-assl.md)   
 [Files Element &#40;ASSL&#41;](../collections/files-element-assl.md)   
 [ClrAssembly Data Type &#40;ASSL&#41;](../data-type/assembly-data-type-assl.md)   
 [Assembly Element &#40;ASSL&#41;](../objects/assembly-element-assl.md)   
 [Assemblies Element &#40;ASSL&#41;](../collections/assemblies-element-assl.md)   
 [Properties &#40;ASSL&#41;](properties-assl.md)  
  
  
