---
title: "Server Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
api_name: 
  - "Server Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "server"
helpviewer_keywords: 
  - "Server element"
ms.assetid: 92ca67f6-817e-4a75-9244-8f8bcf412190
caps.latest.revision: 38
author: "Minewiskan"
ms.author: "owend"
manager: "mblythe"
---
# Server Element (ASSL)
  Describes an instance of [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)].  
  
## Syntax  
  
```xml  
  
<Server>  
   <!—These elements are common to each major object -->  
   <Name>...</Name>  
   <ID>...</ID>  
   <CreatedTimestamp>...</CreatedTimestamp>  
   <LastSchemaUpdate>...</LastSchemaUpdate>  
   <Description>...</Description>  
   <Annotations>...</Annotations>  
   <!-- server elements or properties -->  
   <ProductName>...</ProductName>  
   <Edition>...</Edition>  
   <EditionId>...</Edition>  
   <Version>...</Version>  
   <ServerMode>...</ServerMode>  
   <ProductLevel>...</Databases>  
   <Databases>...</Databases>  
   <Assemblies>...</Assemblies>  
   <Traces>...</Traces>  
   <Roles>...</Roles>  
   <ServerProperties>...</ServerProperties>  
</Server>  
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
|Parent elements|None|  
|Child elements|[Name](../../../2014/analysis-services/dev-guide/name-element-assl.md), [ID](../../../2014/analysis-services/dev-guide/id-element-assl.md), [CreatedTimestamp](../../../2014/analysis-services/dev-guide/createdtimestamp-element-assl.md), [LastSchemaUpdate](../../../2014/analysis-services/dev-guide/lastschemaupdate-element-assl.md), [Description](../../../2014/analysis-services/dev-guide/description-element-assl.md), [Annotations](../../../2014/analysis-services/dev-guide/annotations-element-assl.md), [ProductName](../../../2014/analysis-services/dev-guide/productname-element-assl.md), [Edition](../../../2014/analysis-services/dev-guide/edition-element-assl.md), [EditionId](../../../2014/analysis-services/dev-guide/editionid-element.md), [Version](../../../2014/analysis-services/dev-guide/version-element-assl.md), [ServerMode](../../../2014/analysis-services/dev-guide/editionid-element.md), [ProductLevel](../../../2014/analysis-services/dev-guide/productlabel-element.md), [Databases](../../../2014/analysis-services/dev-guide/databases-element-assl.md), [Assemblies](../../../2014/analysis-services/dev-guide/assemblies-element-assl.md), [Traces](../../../2014/analysis-services/dev-guide/traces-element-assl.md), [Roles](../../../2014/analysis-services/dev-guide/roles-element-assl.md), [ServerProperties](../../../2014/analysis-services/dev-guide/serverproperties-element-assl.md)>|  
  
## Remarks  
 The `Server` element represents an instance of [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)], and serves as the topmost node in the Analysis Services Scripting Language (ASSL) node hierarchy.  
  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.Server>.  
  
## See Also  
 [Objects &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/objects-assl.md)  
  
  