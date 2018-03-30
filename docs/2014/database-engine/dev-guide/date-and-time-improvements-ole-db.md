---
title: "Date and Time Improvements (OLE DB) | Microsoft Docs"
ms.custom: ""
ms.date: "03/08/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
helpviewer_keywords: 
  - "date/time [OLE DB]"
  - "OLE DB, date/time improvements"
ms.assetid: 71614aaf-0fa4-4fe0-b522-68e2e0b66f43
caps.latest.revision: 25
author: "JennieHubbard"
ms.author: "jhubbard"
manager: "jhubbard"
---
# Date and Time Improvements (OLE DB)
  [!INCLUDE[ssKatmai](../../../includes/sskatmai-md.md)] introduces new date and time data types. This section describes how these new types are exposed as extensions in [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client. For an overview of the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client support for the new date and time data types, see [Date and Time Improvements](../../../2014/database-engine/dev-guide/date-and-time-improvements.md). For a sample, see [Use Enhanced Date and Time Features &#40;OLE DB&#41;](../../../2014/database-engine/dev-guide/use-enhanced-date-and-time-features-ole-db.md).  
  
 For more general information about date and time data types, see [datetime &#40;Transact-SQL&#41;](../Topic/datetime%20\(Transact-SQL\).md).  
  
## In This Section  
 [Data Type Support for OLE DB Date and Time Improvements](../../../2014/database-engine/dev-guide/data-type-support-for-ole-db-date-and-time-improvements.md)  
 Provides information about OLE DB ([!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client) types that support [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] date and time data types.  
  
 [Metadata &#40;OLE DB&#41;](../../../2014/database-engine/dev-guide/metadata-ole-db.md)  
 Contains information about the DBBINDING structure, `ICommandWithParameters::GetParameterInfo`, `ICommandWithParameters::SetParameterInfo`, `IColumnsRowset::GetColumnsRowset`, and I`ColumnsInfo::GetColumnInfo`. Also provides information about updates to OLE DB schema rowsets.  
  
 [Bindings and Conversions &#40;OLE DB&#41;](../../../2014/database-engine/dev-guide/bindings-and-conversions-ole-db.md)  
 Describes the rules for conversion between server and client for both existing and new date types.  
  
 [Bulk Copy Changes for Enhanced Date and Time Types &#40;OLE DB and ODBC&#41;](../../../2014/database-engine/dev-guide/bulk-copy-changes-for-enhanced-date-and-time-types-ole-db-and-odbc.md)  
 Describes date/time enhancements to support bulk copy operations.  
  
 [OLE DB API Support for Date and Time Enhancements](../../../2014/database-engine/dev-guide/ole-db-api-support-for-date-and-time-enhancements.md)  
 Describes the OLE DB APIs that support enhanced date/time features.  
  
 [Comparability for IRowsetFind](../../../2014/database-engine/dev-guide/comparability-for-irowsetfind.md)  
 Describes date/time types and `IRowsetFind`.  
  
 [New Date and Time Features with Previous SQL Server Versions &#40;OLE DB&#41;](../../../2014/database-engine/dev-guide/new-date-and-time-features-with-previous-sql-server-versions-ole-db.md)  
 Describes the expected behavior when a client application that uses enhanced date and time features communicates with an older version of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)], and when a client compiled with an older version of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client sends commands to a server that supports enhanced date and time features.  
  
## See Also  
 [SQL Server Native Client &#40;OLE DB&#41;](../../../2014/database-engine/dev-guide/sql-server-native-client-ole-db.md)  
  
  