---
title: "OLE DB API Support for Date and Time Enhancements | Microsoft Docs"
ms.custom: ""
ms.date: "04/27/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
ms.topic: "reference"
helpviewer_keywords: 
  - "OLE DB, date/time improvements"
ms.assetid: e65c9253-bd99-4dc3-9cb8-7613f754c966
author: MightyPen
ms.author: genemi
manager: craigg
---
# OLE DB API Support for Date and Time Enhancements
  The following OLE DB APIs support enhanced date/time features.  
  
|Function|Description|  
|--------------|-----------------|  
|IAccessor::CreateAccessor|A flag is added in the DBBINDING structure to enable applications to discriminate between `datetime`, `datetime2`, and `smalldatetime` values. For more information, see [Parameter and Rowset Metadata](metadata-parameter-and-rowset.md).|  
|IBCPSession::BCPColFmt|For more information, see [Bulk Copy Changes for Enhanced Date and Time Types &#40;OLE DB and ODBC&#41;](../native-client-odbc-date-time/bulk-copy-changes-for-enhanced-date-and-time-types-ole-db-and-odbc.md).|  
|ICommandWithParameters::GetParameterInfo|For more information, see[Parameter and Rowset Metadata](metadata-parameter-and-rowset.md).|  
|ICommandWithParameters::SetParameterinfo|For more information, see[Parameter and Rowset Metadata](metadata-parameter-and-rowset.md).|  
|IColumnsRowset::GetColumnsRowset|For more information, see[Parameter and Rowset Metadata](metadata-parameter-and-rowset.md).|  
|IColumnsInfo::GetColumnInfo|For more information, see[Parameter and Rowset Metadata](metadata-parameter-and-rowset.md).|  
|IDBSchemaRowset::GetRowset|For details of the affected schema rowsets, see[Date and Time and Schema Rowsets](../native-client-ole-db-rowsets/rowsets.md).|  
|IRowsetFastLoad|This interface supports the new date/time types, but there is no change to its interface.|  
|ITableDefinition::CreateTable|For more information, see [Data Type Support for OLE DB Date and Time Improvements](data-type-support-for-ole-db-date-and-time-improvements.md).|  
  
## See Also  
 [Date and Time Improvements &#40;OLE DB&#41;](date-and-time-improvements-ole-db.md)  
  
  
