---
title: "Type Element (ClrAssemblyFile) (ASSL) | Microsoft Docs"
ms.date: 5/8/2018
ms.prod: sql
ms.custom: assl
ms.reviewer: owend
ms.technology: analysis-services
ms.topic: reference
author: minewiskan
ms.author: owend
manager: kfile
---
# Type Element (ClrAssemblyFile) (ASSL)
[!INCLUDE[ssas-appliesto-sqlas](../../../includes/ssas-appliesto-sqlas.md)]
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
|Parent element|[ClrAssemblyFile](../../../analysis-services/scripting/data-type/clrassemblyfile-data-type-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The value of this element is limited to one of the following strings:  
  
|Value|Description|  
|-----------|-----------------|  
|*Main*|Specified file is the main file in the assembly.|  
|*Dependent*|Specified file is a dependent file in the assembly.|  
|*Debug*|Specified file contains debugging information.|  
  
 The enumeration that corresponds to the allowed values for **Type** in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.ClrAssemblyFileType>.  
  
 The element that corresponds to the parent of **Type** in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.ClrAssemblyFile>.  
  
## See Also  
 [File Element &#40;ASSL&#41;](../../../analysis-services/scripting/objects/file-element-assl.md)   
 [Files Element &#40;ASSL&#41;](../../../analysis-services/scripting/collections/files-element-assl.md)   
 [ClrAssembly Data Type &#40;ASSL&#41;](../../../analysis-services/scripting/data-type/clrassembly-data-type-assl.md)   
 [Assembly Element &#40;ASSL&#41;](../../../analysis-services/scripting/objects/assembly-element-assl.md)   
 [Assemblies Element &#40;ASSL&#41;](../../../analysis-services/scripting/collections/assemblies-element-assl.md)   
 [Properties &#40;ASSL&#41;](../../../analysis-services/scripting/properties/properties-assl.md)  
  
  
