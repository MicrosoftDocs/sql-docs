---
title: "Updating Data in Rowsets | Microsoft Docs"
description: "Updating data in rowsets using OLE DB Driver for SQL Server"
ms.custom: ""
ms.date: "06/14/2018"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: connectivity
ms.topic: "reference"
helpviewer_keywords: 
  - "updating data [SQL Server]"
  - "rowsets [OLE DB], updating data"
  - "OLE DB Driver for SQL Server, rowsets"
  - "OLE DB rowsets, updating data"
  - "OLE DB Driver for SQL Server, data updates"
  - "data updates [SQL Server], OLE DB"
author: pmasl
ms.author: pelopes
manager: craigg
---
# Updating Data in Rowsets
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]

[!INCLUDE[Driver_OLEDB_Download](../../../includes/driver_oledb_download.md)]

  The OLE DB Driver for SQL Server updates [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] data when a consumer updates a modifiable rowset that contains that data. A modifiable rowset is created when the consumer requests support for either the **IRowsetChange** or **IRowsetUpdate** interface.  
  
 All OLE DB Driver for SQL Server-modifiable rowsets use [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] cursors to support the rowset. The rowset property DBPROP_LOCKMODE alters [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] concurrency control behavior in cursors and determines the behavior of rowset row fetching and data integrity error generation in updatable rowsets.  
  
 The OLE DB Driver for SQL Server supports row synchronization before or after an update.  
  
> [!NOTE]  
>  IRowChange::SetColumns is available to set the values of one or more named columns of a row object.  
  
## In This Section  
  
-   [Updating Data in SQL Server Cursors](../../oledb/ole-db-rowsets/updating-data-in-sql-server-cursors.md)  
  
-   [Resynchronizing Rows](../../oledb/ole-db-rowsets/updating-data-in-rowsets-resynchronizing-rows.md)  
  
## See Also  
 [Rowsets](../../oledb/ole-db-rowsets/rowsets.md)  
  
  
