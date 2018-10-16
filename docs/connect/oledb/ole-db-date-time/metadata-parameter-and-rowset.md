---
title: "Parameter and Rowset Metadata | Microsoft Docs"
description: "Parameter and rowset metadata"
ms.custom: ""
ms.date: "06/14/2018"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: connectivity
ms.topic: "reference"
helpviewer_keywords: 
  - "metadata [OLE DB]"
author: pmasl
ms.author: pelopes
manager: craigg
---
# Metadata - Parameter and Rowset
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]

[!INCLUDE[Driver_OLEDB_Download](../../../includes/driver_oledb_download.md)]

  This article provides information about the following type and type members, related to the OLE DB date and time enhancements.  
  
-   DBBINDING structure  
  
-   **ICommandWithParameters::GetParameterInfo**  
  
-   **ICommandWithParameters::SetParameterInfo**  
  
-   **IColumnsRowset::GetColumnsRowset**  
  
-   **IColumnsInfo::GetColumnInfo**  
  
## ICommandWithParameters::GetParameterInfo  
 The following information is returned in the DBPARAMINFO structure through *prgParamInfo*:  
  
|Parameter Type|*wType*|*ulParamSize*|*bPrecision*|*bScale*|*dwFlags*<br /><br /> DBPARAMFLAGS_SS_ISVARIABLESCALE|  
|--------------------|-------------|-------------------|------------------|--------------|-----------------------------------------------------|  
|date|DBTYPE_DBDATE|6|10|0|Clear|  
|time|DBTYPE_DBTIME2|10|8, 10..16|0..7|Set|  
|smalldatetime|DBTYPE_DBTIMESTAMP|16|16|0|Clear|  
|datetime|DBTYPE_DBTIMESTAMP|16|23|3|Clear|  
|datetime2|DBTYPE_DBTIMESTAMP|16|19, 21..27|0..7|Set|  
|datetimeoffset|DBTYPE_DBTIMESTAMPOFFSET|20|26, 28..34|0..7|Set|  
  
 Notice that in some cases value ranges are not continuous. This is due to the addition of a decimal point when fractional precision is greater than zero.  
  
 DBPARAMFLAGS_SS_ISVARIABLESCALE is only valid when connected to a [!INCLUDE[ssKatmai](../../../includes/sskatmai-md.md)] (or later) server. DBPARAMFLAGS_SS_ISVARIABLESCALE is never set when connected to down-level servers.  
  
## ICommandWithParameters::SetParameterInfo and Implied Parameter Types  
 The information provided in the DBPARAMBINDINFO structure must conform to the following:  
  
|*pwszDataSourceType*<br /><br /> (provider specific)|*pwszDataSourceType*<br /><br /> (OLE DB generic)|*ulParamSize*|*bScale*|  
|----------------------------------------------------|-------------------------------------------------|-------------------|--------------|  
||DBTYPE_DATE|6|Ignored|  
|date|DBTYPE_DBDATE|6|Ignored|  
||DBTYPE_DBTIME|10|Ignored|  
|time|DBTYPE_DBTIME2|10|0..7|  
|smalldatetime||16|Ignored|  
|datetime||16|Ignored|  
|datetime2 or DBTYPE_DBTIMESTAMP|DBTYPE_DBTIMESTAMP|16|0..7|  
|datetimeoffset|DBTYPE_DBTIMESTAMPOFFSET|20|0..7|  
  
 The *bPrecision* parameter is ignored.  
  
 "DBPARAMFLAGS_SS_ISVARIABLESCALE" is ignored when sending data to the server. Applications can force the use of legacy tabular-data stream (TDS) types by using the provider-specific type names "**datetime**" and "**smalldatetime**". When connected to [!INCLUDE[ssKatmai](../../../includes/sskatmai-md.md)] (or later) servers, "**datetime2**" format will be used and an implicit server conversion will occur, if necessary, when the type name is "**datetime2**" or "DBTYPE_DBTIMESTAMP". *bScale* is ignored if the provider specific type names "**datetime**" or "**smalldatetime**" are used. Otherwise, applications must ensure that *bScale* is set correctly. Applications upgraded from MDAC and OLE DB Driver for SQL Server from [!INCLUDE[ssVersion2005](../../../includes/ssversion2005-md.md)] that use "DBTYPE_DBTIMESTAMP" will fail if they do not set *bScale* correctly. When connected to server instances earlier than [!INCLUDE[ssKatmai](../../../includes/sskatmai-md.md)], a *bScale* value other than 0 or 3 with "DBTYPE_DBTIMESTAMP" is an error and E_FAIL will be returned.  
  
 When ICommandWithParameters::SetParameterInfo is not called, the provider implies the server type from the binding type as specified in IAccessor::CreateAccessor as follows:  
  
|Binding Type|*pwszDataSourceType*<br /><br /> (provider specific)|  
|------------------|----------------------------------------------------|  
|DBTYPE_DATE|datetime2(0)|  
|DBTYPE_DBDATE|date|  
|DBTYPE_DBTIME|time(0)|  
|DBTYPE_DBTIME2|time(7)|  
|DBTYPE_DBTIMESTAMP|datetime2(7)|  
|DBTYPE_DBTIMESTAMPOFFSET|datetimeoffset(7)|  
  
## IColumnsRowset::GetColumnsRowset  
 **IColumnsRowset::GetColumnsRowset** returns the following columns:  
  
|Column Type|DBCOLUMN_TYPE|DBCOLUM_COLUMNSIZE|DBCOLUMN_PRECISION|DBCOLUMN_SCALE, DBCOLUMN_DATETIMEPRECISION|DBCOLUMN_FLAGS, DBCOLUMNFLAGS_SS_ISVARIABLESCALE|  
|-----------------|--------------------|-------------------------|-------------------------|--------------------------------------------------|---------------------------------------------------------|  
|date|DBTYPE_DBDATE|6|10|0|Clear|  
|time|DBTYPE_DBTIME2|10|8, 10..16|0..7|Set|  
|smalldatetime|DBTYPE_DBTIMESTAMP|16|16|0|Clear|  
|datetime|DBTYPE_DBTIMESTAMP|16|23|3|Clear|  
|datetime2|DBTYPE_DBTIMESTAMP|16|19, 21..27|0..7|Set|  
|datetimeoffset|DBTYPE_DBTIMESTAMPOFFSET|20|26, 28..34|0..7|Set|  
  
 In DBCOLUMN_FLAGS, DBCOLUMNFLAGS_ISFIXEDLENGTH is always true for date/time types and the following flags are always false:  
  
-   DBCOLUMNFLAGS_CACHEDEFERRED  
  
-   DBCOLUMNFLAGS_ISBOOKMARK  
  
-   DBCOLUMNFLAGS_ISCHAPTER  
  
-   DBCOLUMNFLAGS_ISLONG  
  
-   DBCOLUMNFLAGS_ISROWID  
  
-   DBCOLUMNFLAGS_ISROWVER  
  
-   DBCOLUMNFLAGS_MAYDEFER  
  
 The remaining flags (DBCOLUMNFLAGS_ISNULLABLE, DBCOLUMNFLAGS_MAYBENULL, DBCOLUMNFLAGS_WRITE, and DBCOLUMNFLAGS_WRITEUNKNOWN) can be set, depending on how the column is defined and the actual query.  
  
 A new flag DBCOLUMNFLAGS_SS_ISVARIABLESCALE is provided in DBCOLUMN_FLAGS to allow an application to determine the server type of columns, where DBCOLUMN_TYPE is DBTYPE_DBTIMESTAMP. DBCOLUMN_SCALE or DBCOLUMN_DATETIMEPRECISION must also be used to identify the server type.  
  
 DBCOLUMNFLAGS_SS_ISVARIABLESCALE is only valid when connected to a [!INCLUDE[ssKatmai](../../../includes/sskatmai-md.md)] (or later) server. DBCOLUMNFLAGS_SS_ISVARIABLESCALE is undefined when connected to down-level servers.  
  
## IColumnsInfo::GetColumnInfo  
 DBCOLUMNINFO structure returns the following information:  
  
|Parameter Type|*wType*|*ulColumnSize*|*bPrecision*|*bScale*|*dwFlags*<br /><br /> DBPARAMFLAGS_SS_ISVARIABLESCALE|  
|--------------------|-------------|--------------------|------------------|--------------|-----------------------------------------------------|  
|date|DBTYPE_DBDATE|6|10|0|Clear|  
|time(1..7)|DBTYPE_DBTIME2|10|8, 10..16|0..7|Set|  
|smalldatetime|DBTYPE_DBTIMESTAMP|16|16|0|Clear|  
|datetime|DBTYPE_DBTIMESTAMP|16|23|3|Clear|  
|datetime2|DBTYPE_DBTIMESTAMP|16|19, 21..27|0..7|Set|  
|datetimeoffset|DBTYPE_DBTIMESTAMPOFFSET|20|26, 28..34|0..7|Set|  
  
 In *dwFlags*, DBCOLUMNFLAGS_ISFIXEDLENGTH is always true for date/time types and the following flags are always false:  
  
-   DBCOLUMNFLAGS_CACHEDEFERRED  
  
-   DBCOLUMNFLAGS_ISBOOKMARK  
  
-   DBCOLUMNFLAGS_ISCHAPTER  
  
-   DBCOLUMNFLAGS_ISLONG  
  
-   DBCOLUMNFLAGS_ISROWID  
  
-   DBCOLUMNFLAGS_ISROWVER, MAYDEFER  
  
 The remaining flags (DBCOLUMNFLAGS_ISNULLABLE, DBCOLUMNFLAGS_MAYBENULL, DBCOLUMNFLAGS_WRITE, and DBCOLUMNFLAGS_WRITEUNKNOWN) can be set.  
  
 A new flag DBCOLUMNFLAGS_SS_ISVARIABLESCALE is provided in *dwFlags* to allow an application to determine the server type of columns, where *wType* is DBTYPE_DBTIMESTAMP. *bScale* must also be used to identify the server type.  
  
## See Also  
 [Data Type Support for OLE DB Date and Time Improvements](../../oledb/ole-db-date-time/data-type-support-for-ole-db-date-and-time-improvements.md)  
  
  
