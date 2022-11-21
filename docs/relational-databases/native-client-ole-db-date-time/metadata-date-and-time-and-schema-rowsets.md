---
description: "Metadata - Date and Time and Schema Rowsets in SQL Server Native Client"
title: "Date and Time and Schema Rowsets"
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: 

ms.topic: "reference"
helpviewer_keywords: 
  - "date/time [OLE DB], schema rowsets"
ms.assetid: 8c35e86f-0597-4ef4-b2b8-f643e53ed4c2
author: markingmyname
ms.author: maghan
ms.custom: seo-dt-2019
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Metadata - Date and Time and Schema Rowsets in SQL Server Native Client
[!INCLUDE[SQL Server Azure SQL Database Synapse Analytics PDW ](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  This topic provides information about COLUMNS rowset and PROCEDURE_PARAMETERS rowset. This information relates to the OLE DB date and time enhancements introduced in [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)].  
  
## COLUMNS Rowset  
 The following column values are returned for date/time types:  
  
|Column Type|DATA_TYPE|COLUMN_FLAGS, DBCOLUMFLAGS_SS_ISVARIABLESCALE|DATETIME_PRECISION|  
|-----------------|----------------|------------------------------------------------------|-------------------------|  
|date|DBTYPE_DBDATE|Clear|0|  
|time|DBTYPE_DBTIME2|Set|0..7|  
|smalldatetime|DBTYPE_DBTIMESTAMP|Clear|0|  
|datetime|DBTYPE_DBTIMESTAMP|Clear|3|  
|datetime2|DBTYPE_DBTIMESTAMP|Set|0..7|  
|datetimeoffset|DBTYPE_DBTIMESTAMPOFFSET|Set|0..7|  
  
 In COLUMN_FLAGS, DBCOLUMNFLAGS_ISFIXEDLENGTH is always true for date/time types and the following flags are always false:  
  
-   DBCOLUMNFLAGS_CACHEDEFERRED  
  
-   DBCOLUMNFLAGS_ISBOOKMARK  
  
-   DBCOLUMNFLAGS_ISCHAPTER  
  
-   DBCOLUMNFLAGS_ISLONG  
  
-   DBCOLUMNFLAGS_ISROWID  
  
-   DBCOLUMNFLAGS_ISROWVER  
  
-   DBCOLUMNFLAGS_MAYDEFER  
  
 The remaining flags (DBCOLUMNFLAGS_ISNULLABLE, DBCOLUMNFLAGS_MAYBENULL, DBCOLUMNFLAGS_WRITE, and DBCOLUMNFLAGS_WRITEUNKNOWN) might be set, depending on how the column is defined.  
  
 A new flag, DBCOLUMNFLAGS_SS_ISVARIABLESCALE, is provided in COLUMN_FLAGS to allow an application to determine the server type of columns where DATA_TYPE is DBTYPE_DBTIMESTAMP. DATETIME_PRECISION must also be used to identify the server type.  
  
 DBCOLUMNFLAGS_SS_ISVARIABLESCALE is only valid when connected to a [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] or later server. DBCOLUMNFLAGS_SS_ISFIXEDSCALE is undefined when connected to down-level servers.  
  
## PROCEDURE_PARAMETERS Rowset  
 DATA_TYPE contains the same values as the COLUMNS schema rowset and TYPE_NAME contains the server type.  
  
 A new column, SS_DATETIME_PRECISION, has been added to return the precision of the type as in the DATETIME_PRECISION column, similar to the COLUMNS rowset.  
  
## PROVIDER_TYPES Rowset  
 The following rows are returned for date/time types:  
  
|Type -><br /><br /> Column|date|time|smalldatetime|datetime|datetime2|datetimeoffset|  
|--------------------------|----------|----------|-------------------|--------------|---------------|--------------------|  
|TYPE_NAME|date|time|smalldatetime|datetime|datetime2|datetimeoffset|  
|DATA_TYPE|DBTYPE_DBDATE|DBTYPE_DBTIME2|DBTYPE_DBTIMESTAMP|DBTYPE_DBTIMESTAMP|DBTYPE_DBTIMESTAMP|DBTYPE_DBTIMESTAMPOFFSET|  
|COLUMN_SIZE|10|16|16|23|27|34|  
|LITERAL_PREFIX|'|'|'|'|'|'|  
|LITERAL_SUFFIX|'|'|'|'|'|'|  
|CREATE_PARAMS|NULL|scale|NULL|NULL|scale|scale|  
|IS_NULLABLE|VARIANT_TRUE|VARIANT_TRUE|VARIANT_TRUE|VARIANT_TRUE|VARIANT_TRUE|VARIANT_TRUE|  
|CASE_SENSITIVE|VARIANT_FALSE|VARIANT_FALSE|VARIANT_FALSE|VARIANT_FALSE|VARIANT_FALSE|VARIANT_FALSE|  
|SEARCHABLE|DB_SEARCHABLE|DB_SEARCHABLE|DB_SEARCHABLE|DB_SEARCHABLE|DB_SEARCHABLE|DB_SEARCHABLE|  
|UNSIGNED_ATTRIBUTE|NULL|NULL|NULL|NULL|NULL|NULL|  
|FIXED_PREC_SCALE|VARIANT_FALSE|VARIANT_FALSE|VARIANT_FALSE|VARIANT_FALSE|VARIANT_FALSE|VARIANT_FALSE|  
|AUTO_UNIQUE_VALUE|VARIANT_FALSE|VARIANT_FALSE|VARIANT_FALSE|VARIANT_FALSE|VARIANT_FALSE|VARIANT_FALSE|  
|LOCAL_TYPE_NAME|date|time|smalldatetime|datetime|datetime2|datetimeoffset|  
|MINIMUM_SCALE|NULL|0|NULL|NULL|0|0|  
|MAXIMUM_SCALE|NULL|7|NULL|NULL|7|7|  
|GUID|NULL|NULL|NULL|NULL|NULL|NULL|  
|TYPELIB|NULL|NULL|NULL|NULL|NULL|NULL|  
|VERSION|NULL|NULL|NULL|NULL|NULL|NULL|  
|IS_LONG|VARIANT_FALSE|VARIANT_FALSE|VARIANT_FALSE|VARIANT_FALSE|VARIANT_FALSE|VARIANT_FALSE|  
|BEST_MATCH|VARIANT_TRUE|VARIANT_TRUE|VARIANT_TRUE|VARIANT_TRUE unless one of the following is true:<br /><br /> Is client connected to a down-level server.<br /><br /> The data type compatibility connection property specifies a compatibility level that equals 80.|VARIANT_TRUE unless one of the following is true:<br /><br /> Is client connected to a down-level server.<br /><br /> The data type compatibility connection property specifies a compatibility level that equals 80.|VARIANT_TRUE|  
|IS_FIXEDLENGTH|VARIANT_TRUE|VARIANT_TRUE|VARIANT_TRUE|VARIANT_TRUE|VARIANT_TRUE|VARIANT_TRUE|  
  
 OLE DB only defines MINIMUM_SCALE and MAXIMUM_SCALE for numeric and decimal types, so [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client's use of these columns for time, datetime2 and datetimeoffset is non-standard.  
  
## See Also  
 [Metadata &#40;OLE DB&#41;](./data-type-support-for-ole-db-date-and-time-improvements.md)  
  
