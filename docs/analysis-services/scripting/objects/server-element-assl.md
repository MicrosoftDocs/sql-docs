---
title: "Server Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
apiname: 
  - "Server Element"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
f1_keywords: 
  - "server"
helpviewer_keywords: 
  - "Server element"
ms.assetid: 92ca67f6-817e-4a75-9244-8f8bcf412190
caps.latest.revision: 38
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
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
|Child elements|[Name](../../../analysis-services/scripting/properties/name-element-assl.md), [ID](../../../analysis-services/scripting/properties/id-element-assl.md), [CreatedTimestamp](../../../analysis-services/scripting/properties/createdtimestamp-element-assl.md), [LastSchemaUpdate](../../../analysis-services/scripting/properties/lastschemaupdate-element-assl.md), [Description](../../../analysis-services/scripting/properties/description-element-assl.md), [Annotations](../../../analysis-services/scripting/collections/annotations-element-assl.md), [ProductName](../../../analysis-services/scripting/properties/productname-element-assl.md), [Edition](../../../analysis-services/scripting/properties/edition-element-assl.md), [EditionId](../../../analysis-services/xmla/xml-elements-properties/editionid-element.md), [Version](../../../analysis-services/scripting/properties/version-element-assl.md), [ServerMode](../../../analysis-services/xmla/xml-elements-properties/editionid-element.md), [ProductLevel](../../../analysis-services/xmla/xml-elements-properties/productlabel-element.md), [Databases](../../../analysis-services/scripting/collections/databases-element-assl.md), [Assemblies](../../../analysis-services/scripting/collections/assemblies-element-assl.md), [Traces](../../../analysis-services/scripting/collections/traces-element-assl.md), [Roles](../../../analysis-services/scripting/collections/roles-element-assl.md), [ServerProperties](../../../analysis-services/scripting/collections/serverproperties-element-assl.md)>|  
  
## Remarks  
 The **Server** element represents an instance of [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)], and serves as the topmost node in the Analysis Services Scripting Language (ASSL) node hierarchy.  
  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.Server>.  
  
## See Also  
 [Objects &#40;ASSL&#41;](../../../analysis-services/scripting/objects/objects-assl.md)  
  
  