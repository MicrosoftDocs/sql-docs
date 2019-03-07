---
title: "Updating Data in Rowsets | Microsoft Docs"
ms.custom: ""
ms.date: "04/27/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: native-client
ms.topic: "reference"
helpviewer_keywords: 
  - "updating data [SQL Server]"
  - "rowsets [OLE DB], updating data"
  - "SQL Server Native Client OLE DB provider, rowsets"
  - "OLE DB rowsets, updating data"
  - "SQL Server Native Client OLE DB provider, data updates"
  - "data updates [SQL Server], OLE DB"
ms.assetid: 37769b1c-c480-419a-8c54-5cc420bf73db
author: MightyPen
ms.author: genemi
manager: craigg
---
# Updating Data in Rowsets
  The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client OLE DB provider updates [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data when a consumer updates a modifiable rowset that contains that data. A modifiable rowset is created when the consumer requests support for either the **IRowsetChange** or **IRowsetUpdate** interface.  
  
 All [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client OLE DB provider-modifiable rowsets use [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] cursors to support the rowset. The rowset property DBPROP_LOCKMODE alters [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] concurrency control behavior in cursors and determines the behavior of rowset row fetching and data integrity error generation in updatable rowsets.  
  
 The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client OLE DB provider supports row synchronization before or after an update.  
  
> [!NOTE]  
>  IRowChange::SetColumns is available to set the values of one or more named columns of a row object.  
  
## In This Section  
  
-   [Updating Data in SQL Server Cursors](updating-data-in-sql-server-cursors.md)  
  
-   [Resynchronizing Rows](updating-data-in-rowsets-resynchronizing-rows.md)  
  
## See Also  
 [Rowsets](rowsets.md)  
  
  
