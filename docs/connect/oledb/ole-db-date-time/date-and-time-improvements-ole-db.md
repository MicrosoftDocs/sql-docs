---
title: "Date and Time Improvements (OLE DB) | Microsoft Docs"
description: "Date and time improvements (OLE DB)"
ms.custom: ""
ms.date: "06/14/2018"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: connectivity
ms.topic: "reference"
helpviewer_keywords: 
  - "date/time [OLE DB]"
  - "OLE DB, date/time improvements"
author: pmasl
ms.author: pelopes
manager: craigg
---
# Date and Time Improvements (OLE DB)
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]

[!INCLUDE[Driver_OLEDB_Download](../../../includes/driver_oledb_download.md)]

  [!INCLUDE[ssKatmai](../../../includes/sskatmai-md.md)] introduces new date and time data types. This section describes how these new types are exposed as extensions in OLE DB Driver for SQL Server. For an overview of the OLE DB Driver for SQL Server support for the new date and time data types, see [Date and Time Improvements](../../oledb/features/date-and-time-improvements.md). For a sample, see [Use Enhanced Date and Time Features &#40;OLE DB&#41;](../../oledb/ole-db-how-to/use-enhanced-date-and-time-features-ole-db.md).  
  
 For more general information about date and time data types, see [datetime &#40;Transact-SQL&#41;](../../../t-sql/data-types/datetime-transact-sql.md).  
  
## In This Section  
 [Data Type Support for OLE DB Date and Time Improvements](../../oledb/ole-db-date-time/data-type-support-for-ole-db-date-and-time-improvements.md)  
 Provides information about OLE DB (OLE DB Driver for SQL Server) types that support [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] date and time data types.  
  
 [Metadata &#40;OLE DB&#41;](../../oledb/ole-db-date-time/metadata-parameter-and-rowset.md)  
 Contains information about the DBBINDING structure, **ICommandWithParameters::GetParameterInfo**, **ICommandWithParameters::SetParameterInfo**, **IColumnsRowset::GetColumnsRowset**, and I**ColumnsInfo::GetColumnInfo**. Also provides information about updates to OLE DB schema rowsets.  
  
 [Bindings and Conversions &#40;OLE DB&#41;](../../oledb/ole-db-date-time/conversions-ole-db.md)  
 Describes the rules for conversion between server and client for both existing and new date types.  
  
 [Bulk Copy Changes for Enhanced Date and Time Types &#40;OLE DB&#41;](../../oledb/ole-db-date-time/bulk-copy-changes-for-enhanced-date-and-time-types-ole-db.md)  
 Describes date/time enhancements to support bulk copy operations.  
  
 [OLE DB API Support for Date and Time Enhancements](../../oledb/ole-db-date-time/ole-db-api-support-for-date-and-time-enhancements.md)  
 Describes the OLE DB APIs that support enhanced date/time features.  
  
 [Comparability for IRowsetFind](../../oledb/ole-db-date-time/comparability-for-irowsetfind.md)  
 Describes date/time types and **IRowsetFind**.  
 
  
## See Also  
 [OLE DB Driver for SQL Server Programming](../../oledb/ole-db/oledb-driver-for-sql-server-programming.md)  
  
  
