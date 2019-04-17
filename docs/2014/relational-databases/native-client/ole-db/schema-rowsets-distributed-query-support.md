---
title: "Distributed Query Support in Schema Rowsets | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: native-client
ms.topic: "reference"
helpviewer_keywords: 
  - "DBPROPSET_SQLSERVERSESSION property"
  - "schema rowsets [OLE DB]"
  - "distributed queries [SQL Server], SQL Server Native Client OLE DB provider"
  - "OLE DB, schema rowsets"
  - "OLE DB rowsets, schema"
  - "rowsets [OLE DB], schema"
ms.assetid: 11354bb6-be42-4d8d-854c-42dd3dc38656
author: MightyPen
ms.author: genemi
manager: craigg
---
# Distributed Query Support in Schema Rowsets
  To support [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] distributed queries, the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client OLE DB provider **IDBSchemaRowset** interface returns metadata on linked servers.  
  
 If the DBPROPSET_SQLSERVERSESSION property SSPROP_QUOTEDCATALOGNAMES is VARIANT_TRUE, a quoted identifier can be specified for the catalog name (for example "my.catalog"). When restricting schema rowset output by catalog, the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client OLE DB provider recognizes a two-part name containing the linked server and catalog name. For the schema rowsets in the table below, specifying a two-part catalog name as _linked_server_**.**_catalog_ restricts output to the applicable catalog of the named linked server.  
  
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
>  To restrict a schema rowset to all catalogs from a linked server, use the syntax *linked_server* (where the period separator is part of the name specification). This syntax is equivalent to specifying NULL for the catalog name restriction and is also used when the linked server indicates a data source that does not support catalogs.  
  
 The [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client OLE DB provider defines the schema rowset LINKEDSERVERS, returning a list of OLE DB data sources registered as linked servers.  
  
## See Also  
 [Schema Rowset Support &#40;OLE DB&#41;](schema-rowset-support-ole-db.md)   
 [LINKEDSERVERS Rowset &#40;OLE DB&#41;](schema-rowsets-linkedservers-rowset.md)  
  
  
