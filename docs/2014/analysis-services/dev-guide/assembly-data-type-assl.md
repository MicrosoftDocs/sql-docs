---
title: "Assembly Data Type (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
api_name: 
  - "Assembly Data Type"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
helpviewer_keywords: 
  - "Assembly data type"
ms.assetid: 0a381322-9509-4579-a754-c6cdd0a70cc9
caps.latest.revision: 16
author: "Minewiskan"
ms.author: "owend"
manager: "mblythe"
---
# Assembly Data Type (ASSL)
  Defines an abstract primitive data type that represents a [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)] assembly or a COM dynamic link library (DLL) associated with a [Server](../../../2014/analysis-services/dev-guide/server-element-assl.md) or [Database](../../../2014/analysis-services/dev-guide/database-element-assl.md) element.  
  
> [!IMPORTANT]  
>  COM assemblies might pose a security risk. Due to this risk and other considerations, COM assemblies were deprecated in [!INCLUDE[ssASversion10](../../includes/ssasversion10-md.md)]. COM assemblies might not be supported in future releases.  
  
## Syntax  
  
```xml  
  
<Assembly>  
   <Name>...</Name>  
   <ID>...</ID>  
   <Description>...</Description>  
   <CreatedTimestamp>...</CreatedTimestamp>  
   <LastSchemaUpdate>...</LastSchemaUpdate>  
   <ImpersonationInfo>...</ImpersonationInfo>  
   <Annotations>...</Annotations>  
</Assembly>  
```  
  
## Data Type Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Base data types|None|  
|Derived data types|[ClrAssembly](../../../2014/analysis-services/dev-guide/clrassembly-data-type-assl.md), [ComAssembly](../../../2014/analysis-services/dev-guide/comassembly-data-type-assl.md)|  
  
## Data Type Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|None|  
|Child elements|[Annotations](../../../2014/analysis-services/dev-guide/annotations-element-assl.md), [CreatedTimestamp](../../../2014/analysis-services/dev-guide/createdtimestamp-element-assl.md), [Description](../../../2014/analysis-services/dev-guide/description-element-assl.md), [ID](../../../2014/analysis-services/dev-guide/id-element-assl.md), [ImpersonationInfo](../../../2014/analysis-services/dev-guide/impersonationinfo-element-assl.md), [LastSchemaUpdate](../../../2014/analysis-services/dev-guide/lastschemaupdate-element-assl.md), [Name](../../../2014/analysis-services/dev-guide/name-element-assl.md)|  
|Derived elements|None|  
  
## Remarks  
 The `Assembly` data type serves as the base data type for the `ComAssembly` element, which represents COM libraries associated with the instance or database, and the `ClrAssembly` element, which represents [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)] assemblies associated with the instance or database. For more information about assemblies, see [Multidimensional Model Assemblies Management](../multidimensional-models/multidimensional-model-assemblies-management.md).  
  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.Assembly>.  
  
## See Also  
 [Server Element &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/server-element-assl.md)   
 [Database Element &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/database-element-assl.md)   
 [Analysis Services Scripting Language XML Data Types &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/analysis-services-scripting-language-xml-data-types-assl.md)  
  
  