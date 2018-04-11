---
title: "DataSource Data Type (ASSL) | Microsoft Docs"
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
  - "DataSource Data Type"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
helpviewer_keywords: 
  - "DataSource data type"
ms.assetid: 05e8de8d-452d-4128-98a6-4437df227fec
caps.latest.revision: 13
author: "Minewiskan"
ms.author: "owend"
manager: "mblythe"
---
# DataSource Data Type (ASSL)
  Defines an abstract primitive data type that represents a data source in a [Database](../../../2014/analysis-services/dev-guide/database-element-assl.md) element.  
  
## Syntax  
  
```xml  
  
<DataSource>  
   <Name>...</Name>  
   <ID>...</ID>  
   <CreatedTimestamp>...</CreateTimestamp>  
   <LastSchemaUpdate>...</LastSchemaUpdate>  
   <ManagedProvider>...</ManagedProvider>  
   <ConnectionString>...</ConnectionString>  
   <ConnectionStringSecurity>...</ConnectionStringSecurity>  
   <ImpersonationInfo>...</ImpersonationInfo>  
   <Isolation>...</Isolation>  
   <MaxActiveConnections>...</MaxActiveConnections>  
   <Description>...</Description>  
   <Timeout>...</Timeout>  
   <Annotations>...</Annotations>  
   <DataSourcePermission>...</DataSourcePermission>  
</DataSource>  
```  
  
## Data Type Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Base data types|None|  
|Derived data types|[RelationalDataSource](../../../2014/analysis-services/dev-guide/relationaldatasource-data-type-assl.md), [OlapDataSource](../../../2014/analysis-services/dev-guide/olapdatasource-data-type-assl.md), [PushedDataSource](../../../2014/analysis-services/dev-guide/pusheddatasource-data-type-assl.md)|  
  
## Data Type Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|None|  
|Child elements|[Annotations](../../../2014/analysis-services/dev-guide/annotations-element-assl.md), [ConnectionString](../../../2014/analysis-services/dev-guide/connectionstring-element-assl.md), [ConnectionStringSecurity](../../../2014/analysis-services/dev-guide/connectionstringsecurity-element-assl.md), [CreatedTimestamp](../../../2014/analysis-services/dev-guide/createdtimestamp-element-assl.md), [DataSourcePermission](../../../2014/analysis-services/dev-guide/datasourcepermissions-element-assl.md), [Description](../../../2014/analysis-services/dev-guide/description-element-assl.md), [ID](../../../2014/analysis-services/dev-guide/id-element-assl.md), [ImpersonationInfo](../../../2014/analysis-services/dev-guide/impersonationinfo-element-assl.md), [Isolation](../../../2014/analysis-services/dev-guide/isolation-element-assl.md), [LastSchemaUpdate](../../../2014/analysis-services/dev-guide/lastschemaupdate-element-assl.md), [ManagedProvider](../../../2014/analysis-services/dev-guide/managedprovider-element-assl.md), [MaxActiveConnections](../../../2014/analysis-services/dev-guide/maxactiveconnections-element-assl.md), [Name](../../../2014/analysis-services/dev-guide/name-element-assl.md), [Timeout](../../../2014/analysis-services/dev-guide/timeout-element-assl.md)|  
|Derived elements|None|  
  
## Remarks  
 When defining out-of-line bindings, the `Name` element is optional. Not having to specify a `Name` element allows data sources to be defined within the binding for cubes, partitions, and so on. For data sources contained by a `Database` element, `Name` is a required element.  
  
 For more information about data sources, see [Data Sources in Multidimensional Models](../multidimensional-models/data-sources-in-multidimensional-models.md).  
  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.DataSource>.  
  
## See Also  
 [Analysis Services Scripting Language XML Data Types &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/analysis-services-scripting-language-xml-data-types-assl.md)  
  
  