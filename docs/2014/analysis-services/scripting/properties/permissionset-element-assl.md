---
title: "PermissionSet Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "PermissionSet Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "PermissionSet"
helpviewer_keywords: 
  - "PermissionSet element"
ms.assetid: da5a9175-48e4-4b5e-a780-3e0077939974
author: minewiskan
ms.author: owend
manager: craigg
---
# PermissionSet Element (ASSL)
  Identifies the permission set associated with a [!INCLUDE[msCoName](../../../includes/msconame-md.md)] .NET Framework assembly.  
  
## Syntax  
  
```xml  
  
<ClrAssembly>  
   ...  
   <PermissionSet>...</PermissionSet>  
  
</ClrAssembly>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|String (enumeration)|  
|Default value|*Safe*|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent element|[ClrAssembly](../data-type/assembly-data-type-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The value of this element is limited to one of the strings listed in the following table.  
  
|Value|Description|  
|-----------|-----------------|  
|*Safe*|Only internal computation and local data access is allowed. *Safe* is the most restrictive permission set. Code executed by an assembly with *Safe* permissions cannot access external system resources such as files, the network, environment variables, or the registry.|  
|*ExternalAccess*|*Safe*, with the additional ability to access external system resources such as files, networks, environmental variables, and the registry.|  
|*Unrestricted*|Unrestricted allows assemblies unrestricted access to resources, both within and outside [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)]. Code executing from within an *Unrestricted* assembly can call unmanaged code.|  
  
 The enumeration that corresponds to the allowed values for `PermissionSet` in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.PermissionSet>.  
  
## See Also  
 [ComAssembly Data Type &#40;ASSL&#41;](../data-type/comassembly-data-type-assl.md)   
 [Assemblies Element &#40;ASSL&#41;](../collections/assemblies-element-assl.md)   
 [Database Element &#40;ASSL&#41;](../objects/database-element-assl.md)   
 [Server Element &#40;ASSL&#41;](../objects/server-element-assl.md)   
 [Properties &#40;ASSL&#41;](properties-assl.md)  
  
  
