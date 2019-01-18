---
title: "Distributed Query Support in Schema Rowsets | Microsoft Docs"
description: "Distributed query support in schema rowsets"
ms.custom: ""
ms.date: "06/12/2018"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: connectivity
ms.topic: "reference"
helpviewer_keywords: 
  - "DBPROPSET_SQLSERVERSESSION property"
  - "schema rowsets [OLE DB]"
  - "distributed queries [SQL Server], OLE DB Driver for SQL Server"
  - "OLE DB, schema rowsets"
  - "OLE DB rowsets, schema"
  - "rowsets [OLE DB], schema"
author: pmasl
ms.author: pelopes
manager: craigg
---
# Schema Rowsets - Distributed Query Support
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]

[!INCLUDE[Driver_OLEDB_Download](../../../includes/driver_oledb_download.md)]

  To support [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] distributed queries, the OLE DB Driver for SQL Server **IDBSchemaRowset** interface returns metadata on linked servers.  
  
 If the DBPROPSET_SQLSERVERSESSION property SSPROP_QUOTEDCATALOGNAMES is VARIANT_TRUE, a quoted identifier can be specified for the catalog name (for example "my.catalog"). When restricting schema rowset output by catalog, the OLE DB Driver for SQL Server recognizes a two-part name containing the linked server and catalog name. For the schema rowsets in the table below, specifying a two-part catalog name as _linked\_server_**.**_catalog_ restricts output to the applicable catalog of the named linked server.  
  
|Schema rowset|Catalog restriction|  
|-------------------|-------------------------|  
|DBSCHEMA_CATALOGS|CATALOG_NAME|  
|DBSCHEMA_COLUMNS|TABLE_CATALOG|  
|DBSCHEMA_PRIMARY_KEYS|TABLE_CATALOG|  
|DBSCHEMA_TABLES|TABLE_CATALOG|  
|DBSCHEMA_FOREIGN_KEYS|PK_TABLE_CATALOG FK_TABLE_CATALOG|  
|DBSCHEMA_INDEXES|TABLE_CATALOG|  
|DBSCHEMA_COLUMN_PRIVILEGES|TABLE_CATALOG|  
|DBSCHEMA_TABLE_PRIVILEGES|TABLE_CATALOG|  
  
> [!NOTE]  
>  To restrict a schema rowset to all catalogs from a linked server, use the syntax *linked_server* (where the underscore separator is part of the name specification). This syntax is equivalent to specifying NULL for the catalog name restriction and is also used when the linked server indicates a data source that does not support catalogs.  
 
 The OLE DB Driver for SQL Server defines the schema rowset LINKEDSERVERS, returning a list of OLE DB data sources registered as linked servers.  
  
## See Also  
 [Schema Rowset Support &#40;OLE DB&#41;](../../oledb/ole-db/schema-rowset-support-ole-db.md)   
 [LINKEDSERVERS Rowset &#40;OLE DB&#41;](../../oledb/ole-db/schema-rowsets-linkedservers-rowset.md)  
  
  
