---
title: "Change Tracking Functions (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "08/08/2016"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "functions [SQL Server], change tracking"
  - "change tracking [SQL Server], functions"
ms.assetid: 04eb53c4-8b69-414e-9696-185d227fea35
author: "rothja"
ms.author: "jroth"
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Change Tracking Functions (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

  Change tracking records insert, update, and delete activity applied to tracked tables, supplying the details of the changes in an easily consumed relational format. The following functions return information about the changes.  
  
|Function|Description|  
|--------------|-----------------|  
|[CHANGETABLE (CHANGES)](../../relational-databases/system-functions/changetable-transact-sql.md)|Returns tracking information for all changes to a table that have occurred since a specified version.|  
|[CHANGETABLE (VERSION)](../../relational-databases/system-functions/changetable-transact-sql.md)|Returns the latest change tracking information for a specified row.|  
|[CHANGE_TRACKING_MIN_VALID_VERSION()](../../relational-databases/system-functions/change-tracking-min-valid-version-transact-sql.md)|Returns the minimum version that is valid for use in obtaining change tracking information from the specified table when you are using the [CHANGETABLE](../../relational-databases/system-functions/changetable-transact-sql.md) function.|  
|[CHANGE_TRACKING_CURRENT_VERSION](../../relational-databases/system-functions/change-tracking-current-version-transact-sql.md)|Obtains a version that is associated with the last committed transaction. You can use this version the next time you enumerate changes by using CHANGETABLE.|  
|[CHANGE_TRACKING_IS_COLUMN_IN_MASK](../../relational-databases/system-functions/change-tracking-is-column-in-mask-transact-sql.md)|Interprets the SYS_CHANGE_COLUMNS value that is returned by the CHANGETABLE(CHANGES ...) function.|  
|[WITH CHANGE_TRACKING_CONTEXT](../../relational-databases/system-functions/with-change-tracking-context-transact-sql.md)|Enables the specification of a change context, such as an originator ID, when an application changes data.|  
  
## See Also  
 [Track Data Changes &#40;SQL Server&#41;](../../relational-databases/track-changes/track-data-changes-sql-server.md)  
  
  
