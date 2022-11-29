---
description: "Schema Rowsets - Distributed Query Support in SQL Server Native Client"
title: "Distributed Query Support in Schema Rowsets | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: native-client
ms.topic: "reference"
helpviewer_keywords: 
  - "DBPROPSET_SQLSERVERSESSION property"
  - "schema rowsets [OLE DB]"
  - "distributed queries [SQL Server], SQL Server Native Client OLE DB provider"
  - "OLE DB, schema rowsets"
  - "OLE DB rowsets, schema"
  - "rowsets [OLE DB], schema"
ms.assetid: 11354bb6-be42-4d8d-854c-42dd3dc38656
author: markingmyname
ms.author: maghan
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Schema Rowsets - Distributed Query Support in SQL Server Native Client
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

> [!IMPORTANT] 
> [!INCLUDE[snac-removed-oledb-only](../../../includes/snac-removed-oledb-only.md)]

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
 [Schema Rowset Support &#40;OLE DB&#41;](../../../relational-databases/native-client/ole-db/schema-rowset-support-ole-db.md)   
 [LINKEDSERVERS Rowset &#40;OLE DB&#41;](../../../relational-databases/native-client/ole-db/schema-rowsets-linkedservers-rowset.md)  
  
  
