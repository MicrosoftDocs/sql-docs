---
title: "MDSCHEMA_INPUT_DATASOURCES Rowset | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "MDSCHEMA_INPUT_DATASOURCES"
topic_type: 
  - "apiref"
helpviewer_keywords: 
  - "MDSCHEMA_INPUT_DATASOURCES rowset"
ms.assetid: 12482fd5-16e3-4171-9cb0-76d0d4f5308e
author: minewiskan
ms.author: owend
manager: craigg
---
# MDSCHEMA_INPUT_DATASOURCES Rowset
  Describes the data sources defined within the database.  
  
## Rowset Columns  
 The `MDSCHEMA_INPUT_DATASOURCES` rowset contains the following columns.  
  
|Column name|Type indicator|Length|Description|  
|-----------------|--------------------|------------|-----------------|  
|`CATALOG_NAME`|`DBTYPE_WSTR`||The name of the catalog to which this data source belongs.|  
|`SCHEMA_NAME`|`DBTYPE_WSTR`||Not supported.|  
|`DATASOURCE_NAME`|`DBTYPE_WSTR`||The name of the data source object.|  
|`DATASOURCE_TYPE`|`DBTYPE_WSTR`||The type of the data source. Valid values include:<br /><br /> -   `Relational`<br />-   `Olap`|  
|`CREATED_ON`|`DBTYPE_DBTIMESTAMP`||The date that the data source was created.|  
|`LAST_SCHEMA_UPDATE`|`DBTYPE_DBTIMESTAMP`||The date and time that the data source was last modified.|  
|`DESCRIPTION`|`DBTYPE_WSTR`||A user-friendly description of the action.|  
|`TIMEOUT`|`DBTYPE_UI4`||The timeout of the data source.|  
  
 This schema rowset is not sorted.  
  
## Restriction Columns  
 The `MDSCHEMA_INPUT_DATASOURCES` rowset can be restricted on the columns listed in the following table.  
  
|Column name|Type indicator|Restriction State|  
|-----------------|--------------------|-----------------------|  
|`CATALOG_NAME`|`DBTYPE_WSTR`|Optional.|  
|`SCHEMA_NAME`|`DBTYPE_WSTR`|Optional.|  
|`DATASOURCE_NAME`|`DBTYPE_WSTR`|Optional.|  
|`DATASOURCE_TYPE`|`DBTYPE_WSTR`|Optional.|  
  
## See Also  
 [OLE DB for OLAP Schema Rowsets](ole-db-for-olap-schema-rowsets.md)  
  
  
