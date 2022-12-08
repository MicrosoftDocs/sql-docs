---
title: Date and Time Improvements in OLE DB
description: These articles describe how OLE DB Driver for SQL Server supports new date and time data types.
author: David-Engel
ms.author: v-davidengel
ms.date: 06/14/2018
ms.service: sql
ms.subservice: connectivity
ms.topic: "reference"
helpviewer_keywords:
  - "date/time [OLE DB]"
  - "OLE DB, date/time improvements"
---
# Date and Time Improvements in OLE DB

[!INCLUDE [SQL Server](../../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

[!INCLUDE[Driver_OLEDB_Download](../../../includes/driver_oledb_download.md)]

[!INCLUDE[ssKatmai](../../../includes/sskatmai-md.md)] introduces new date and time data types. This section describes how these new types are exposed as extensions in OLE DB Driver for SQL Server. For an overview of the OLE DB Driver for SQL Server support for the new date and time data types, see [Date and Time Improvements](../features/date-and-time-improvements.md). For a sample, see [Use Enhanced Date and Time Features &#40;OLE DB&#41;](../ole-db-how-to/use-enhanced-date-and-time-features-ole-db.md).

For more general information about date and time data types, see [datetime &#40;Transact-SQL&#41;](../../../t-sql/data-types/datetime-transact-sql.md).

## In this section

[Data Type Support for OLE DB Date and Time Improvements](data-type-support-for-ole-db-date-and-time-improvements.md)  
Provides information about OLE DB (OLE DB Driver for SQL Server) types that support [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] date and time data types.

[Metadata &#40;OLE DB&#41;](metadata-parameter-and-rowset.md)  
Contains information about the DBBINDING structure, **ICommandWithParameters::GetParameterInfo**, **ICommandWithParameters::SetParameterInfo**, **IColumnsRowset::GetColumnsRowset**, and I**ColumnsInfo::GetColumnInfo**. Also provides information about updates to OLE DB schema rowsets.

[Bindings and Conversions &#40;OLE DB&#41;](conversions-ole-db.md)  
Describes the rules for conversion between server and client for both existing and new date types.

[Bulk Copy Changes for Enhanced Date and Time Types &#40;OLE DB&#41;](bulk-copy-changes-for-enhanced-date-and-time-types-ole-db.md)  
Describes date/time enhancements to support bulk copy operations.

[OLE DB API Support for Date and Time Enhancements](ole-db-api-support-for-date-and-time-enhancements.md)  
Describes the OLE DB APIs that support enhanced date/time features.

[Comparability for IRowsetFind](comparability-for-irowsetfind.md)  
Describes date/time types and **IRowsetFind**.

## See also

[OLE DB Driver for SQL Server Programming](../ole-db/oledb-driver-for-sql-server-programming.md)
