---
title: "DataSource Data Type (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
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
author: minewiskan
ms.author: owend
manager: craigg
---
# DataSource Data Type (ASSL)
  Defines an abstract primitive data type that represents a data source in a [Database](../objects/database-element-assl.md) element.  
  
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
|Derived data types|[RelationalDataSource](datasource-data-type-assl.md), [OlapDataSource](olapdatasource-data-type-assl.md), [PushedDataSource](pusheddatasource-data-type-assl.md)|  
  
## Data Type Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|None|  
|Child elements|[Annotations](../collections/annotations-element-assl.md), [ConnectionString](../properties/connectionstring-element-assl.md), [ConnectionStringSecurity](../properties/connectionstringsecurity-element-assl.md), [CreatedTimestamp](../properties/createdtimestamp-element-assl.md), [DataSourcePermission](../collections/datasourcepermissions-element-assl.md), [Description](../properties/description-element-assl.md), [ID](../properties/id-element-assl.md), [ImpersonationInfo](../properties/impersonationinfo-element-assl.md), [Isolation](../properties/isolation-element-assl.md), [LastSchemaUpdate](../properties/lastschemaupdate-element-assl.md), [ManagedProvider](../properties/managedprovider-element-assl.md), [MaxActiveConnections](../properties/maxactiveconnections-element-assl.md), [Name](../properties/name-element-assl.md), [Timeout](../properties/timeout-element-assl.md)|  
|Derived elements|None|  
  
## Remarks  
 When defining out-of-line bindings, the `Name` element is optional. Not having to specify a `Name` element allows data sources to be defined within the binding for cubes, partitions, and so on. For data sources contained by a `Database` element, `Name` is a required element.  
  
 For more information about data sources, see [Data Sources in Multidimensional Models](../../multidimensional-models/data-sources-in-multidimensional-models.md).  
  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.DataSource>.  
  
## See Also  
 [Analysis Services Scripting Language XML Data Types &#40;ASSL&#41;](analysis-services-scripting-language-xml-data-types-assl.md)  
  
  
