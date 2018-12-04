---
title: "Parameter and Result Metadata | Microsoft Docs"
ms.custom: ""
ms.date: "03/04/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: native-client
ms.topic: "reference"
helpviewer_keywords: 
  - "metadata [ODBC]"
ms.assetid: 1518e6e5-a6a8-4489-b779-064c5624df53
author: MightyPen
ms.author: genemi
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Metadata - Parameter and Result
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]
[!INCLUDE[SNAC_Deprecated](../../includes/snac-deprecated.md)]

  This topic describes what is returned in the implementation parameter descriptor (IPD) and implementation row descriptor (IRD) fields for date and time data types.  
  
## Information Returned in IPD Fields  
 The following information is returned in the IPD fields:  
  
|Parameter type|date|time|smalldatetime|datetime|datetime2|datetimeoffset|  
|--------------------|----------|----------|-------------------|--------------|---------------|--------------------|  
|SQL_DESC_CASE_SENSITIVE|SQL_FALSE|SQL_FALSE|SQL_FALSE|SQL_FALSE|SQL_FALSE|SQL_FALSE|  
|SQL_DESC_CONCISE_TYPE|SQL_TYPE_DATE|SQL_SS_TIME2|SQL_TYPE_TIMESTAMP|SQL_TYPE_TIMESTAMP|SQL_TYPE_TIMESTAMP|SQL_SS_TIMESTAMPOFFSET|  
|SQL_DESC_DATETIME_INTERVAL_CODE|SQL_CODE_DATE|0|SQL_CODE_TIMESTAMP|SQL_CODE_TIMESTAMP|SQL_CODE_TIMESTAMP|0|  
|SQL_DESC_DATETIME_INTERVAL_PRECISION|10|8,10..16|16|23|19, 21..27|26, 28..34|  
|SQL_DESC_FIXED_PREC_SCALE|SQL_FALSE|SQL_FALSE|SQL_FALSE|SQL_FALSE|SQL_FALSE|SQL_FALSE|  
|SQL_DESC_LENGTH|10|8,10..16|16|23|19, 21..27|26, 28..34|  
|SQL_DESC_OCTET_LENGTH|6|12|4|8|16|20|  
|SQL_DESC_PRECISION|0|0..7|0|3|0..7|0..7|  
|SQL_DESC_SCALE|0|0..7|0|3|0..7|0..7|  
|SQL_DESC_TYPE|SQL_TYPE_DATE|SQL_SS_TYPE_TIME2|SQL_DATETIME|SQL_DATETIME|SQL_DATETIME|SQL_SS_TIMESTAMPOFFSET|  
|SQL_DESC_TYPE_NAME|**date**|**time**|**smalldatetime** in IRD, **datetime2** in IPD|**datetime** in IRD, **datetime2** in IPD|**datetime2**|datetimeoffset|  
|SQL_CA_SS_VARIANT_TYPE|SQL_C_TYPE_DATE|SQL_C_TYPE_BINARY|SQL_C_TYPE_TIMESTAMP|SQL_C_TYPE_TIMESTAMP|SQL_C_TYPE_TIMESTAMP|SQL_C_TYPE_BINARY|  
|SQL_CA_SS_VARIANT_SQL_TYPE|SQL_TYPE_DATE|SQL_SS_TIME2|SQL_TYPE_TIMESTAMP|SQL_TYPE_TIMESTAMP|SQL_TYPE_TIMESTAMP|SQL_SS_TIMESTAMPOFFSET|  
|SQL_CA_SS_SERVER_TYPE|N/A|N/A|SQL_SS_TYPE_SMALLDATETIME|SQL_SS_TYPE_DATETIME|SQL_SS_TYPE_DEFAULT|N/A|  
  
 Sometimes there are discontinuities in value ranges. For example, 9 is missing in 8,10..16. This is due to the addition of a decimal point when fractional precision is greater than zero.  
  
 **datetime2** is returned as the typename for **smalldatetime** and **datetime** because the driver uses this as a common type for transmitting all **SQL_TYPE_TIMESTAMP** values to the server.  
  
 SQL_CA_SS_VARIANT_SQL_TYPE is a new descriptor field. This field was added to the IRD and IPD to enable applications to specify the value type associated with **sqlvariant** (SQL_SSVARIANT) columns and parameters  
  
 SQL_CA_SS_SERVER_TYPE is a new IPD-only field to enable applications to control how values for parameters bound as SQL_TYPE_TYPETIMESTAMP (or as SQL_SS_VARIANT with a C type of SQL_C_TYPE_TIMESTAMP) are sent to the server. If SQL_DESC_CONCISE_TYPE is SQL_TYPE_TIMESTAMP (or is SQL_SS_VARIANT and the C type is SQL_C_TYPE_TIMESTAMP) when SQLExecute or SQLExecDirect is called, the value of SQL_CA_SS_SERVER_TYPE determines the tabular data stream (TDS) type of the parameter value, as follows:  
  
|Value of SQL_CA_SS_SERVER_TYPE|Valid values for SQL_DESC_PRECISION|Valid values for SQL_DESC_LENGTH|TDS type|  
|----------------------------------------|-------------------------------------------|----------------------------------------|--------------|  
|SQL_SS_TYPE_DEFAULT|0..7|19, 21..27|**datetime2**|  
|SQL_SS_TYPE_SMALLDATETIME|0|19|**smalldatetime**|  
|SQL_SS_TYPE_DATETIME|3|23|**datetime**|  
  
 The default setting of SQL_CA_SS_SERVER_TYPE is SQL_SS_TYPE_DEFAULT. The settings of SQL_DESC_PRECISION and SQL_DESC_LENGTH are validated with the setting of SQL_CA_SS_SERVER_TYPE as described in the table above. If this validation fails, SQL_ERROR is returned and a diagnostic record is logged with SQLState 07006 and the message "Restricted data type attribute violation". This error is also returned if SQL_CA_SS_SERVER_TYPE is set to a value other than SQL_SS_TYPE DEFAULT and DESC_CONCISE_TYPE is not SQL_TYPE_TIMESTAMP. These validations are performed when descriptor consistency validation occurs, for example:  
  
-   When SQL_DESC_DATA_PTR is changed.  
  
-   At prepare or execute time (when SQLExecute, SQLExecDirect, SQLSetPos, or SQLBulkOperations is called).  
  
-   When an application forces a non-deferred prepare by calling SQLPrepare with deferred prepare disabled, or by calling SQLNumResultCols, SQLDescribeCol, or SQLDescribeParam for a statement that is prepared but not executed.  
  
 When SQL_CA_SS_SERVER_TYPE is set by a call to SQLSetDescField, its value must be SQL_SS_TYPE_DEFAULT, SQL_SS_TYPE_SMALLDATETIME, or SQL_SS_TYPE_DATETIME. If this is not the case, SQL_ERROR is returned and a diagnostic record is logged with SQLState HY092 and the message "Invalid attribute/option identifier".  
  
 The SQL_CA_SS_SERVER_TYPE attribute can be used by applications that depend on functionality supported by **datetime** and **smalldatetime**, but not **datetime2**. For example, **datetime2** requires the use of the **dateadd** and **datediif** functions, whereas **datetime** and **smalldatetime** also allow arithmetic operators. Most applications will not need to use this attribute and its use should be avoided.  
  
## Information Returned in IRD Fields  
 The following information is returned in the IRD fields:  
  
|Column Type|date|time|smalldatetime|datetime|datetime2|datetimeoffset|  
|-----------------|----------|----------|-------------------|--------------|---------------|--------------------|  
|SQL_DESC_AUTO_UNIQUE_VALUE|SQL_FALSE|SQL_FALSE|SQL_FALSE|SQL_FALSE|SQL_FALSE|SQL_FALSE|  
|SQL_DESC_CASE_SENSITIVE|SQL_FALSE|SQL_FALSE|SQL_FALSE|SQL_FALSE|SQL_FALSE|SQL_FALSE|  
|SQL_DESC_CONCISE_TYPE|SQL_TYPE_DATE|SQL_SS_TIME2|SQL_TYPE_TIMESTAMP|SQL_TYPE_TIMESTAMP|SQL_TYPE_TIMESTAMP|SQL_SS_TIMESTAMPOFFSET|  
|SQL_DESC_DATETIME_INTERVAL_CODE|SQL_CODE_DATE|0|SQL_CODE_TIMESTAMP|SQL_CODE_TIMESTAMP|SQL_CODE_TIMESTAMP|0|  
|SQL_DESC_DATETIME_INTERVAL_PRECISION|10|8,10..16|16|23|19, 21..27|26, 28..34|  
|SQL_DESC_DISPLAY_SIZE|10|8,10..16|16|23|19, 21..27|26, 28..34|  
|SQL_DESC_FIXED_PREC_SCALE|SQL_FALSE|SQL_FALSE|SQL_FALSE|SQL_FALSE|SQL_FALSE|SQL_FALSE|  
|SQL_DESC_LENGTH|10|8,10..16|16|2|19, 21..27|26, 28..34|  
|SQL_DESC_LITERAL_PREFIX|'|'|'|'|'|'|  
|SQL_DESC_LITERAL_SUFFIX|'|'|'|'|'|'|  
|SQL_DESC_LOCAL_TYPE_NAME|**date**|**time**|**smalldatetime**|**datetime**|**datetime2**|datetimeoffset|  
|SQL_DESC_OCTET_LENGTH|6|12|4|8|16|20|  
|SQL_DESC_PRECISION|0|0..7|0|3|0..7|0..7|  
|SQL_DESC_SCALE|0|0..7|0|3|0..7|0..7|  
|SQL_DESC_SEARCHABLE|SQL_PRED_SEARCHABLE|SQL_PRED_SEARCHABLE|SQL_PRED_SEARCHABLE|SQL_PRED_SEARCHABLE|SQL_PRED_SEARCHABLE|SQL_PRED_SEARCHABLE|  
|SQL_DESC_TYPE|SQL_DATETIME|SQL_SS_TIME2|SQL_DATETIME|SQL_DATETIME|SQL_DATETIME|SQL_SS_TIMESTAMPOFFSET|  
|SQL_DESC_TYPE_NAME|**date**|**time**|**smalldatetime**|**datetime**|**datetime2**|datetimeoffset|  
|SQL_DESC_UNSIGNED|SQL_TRUE|SQL_TRUE|SQL_TRUE|SQL_TRUE|SQL_TRUE|SQL_TRUE|  
  
## See Also  
 [Metadata &#40;ODBC&#41;](https://msdn.microsoft.com/library/99133efc-b1f2-46e9-8203-d90c324a8e4c)  
  
  
