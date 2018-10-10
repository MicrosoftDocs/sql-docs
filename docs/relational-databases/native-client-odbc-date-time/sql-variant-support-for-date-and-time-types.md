---
title: "sql_variant Support for Date and Time Types | Microsoft Docs"
ms.custom: ""
ms.date: "03/03/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: native-client
ms.topic: "reference"
helpviewer_keywords: 
  - "sql_variant data type"
ms.assetid: 12ff1ea6-e2cc-40e6-910c-3126974a90b3
author: MightyPen
ms.author: genemi
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sql_variant Support for Date and Time Types
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]
[!INCLUDE[SNAC_Deprecated](../../includes/snac-deprecated.md)]

  This topic describes how the **sql_variant** data type supports enhanced date and time functionality.  
  
 The column attribute SQL_CA_SS_VARIANT_TYPE is used to return the C type of a variant result column. [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] introduces an additional attribute, SQL_CA_SS_VARIANT_SQL_TYPE, which sets the SQL type of a variant result column in the implementation row descriptor (IRD). SQL_CA_SS_VARIANT_SQL_TYPE can also be used in the implementation parameter descriptor (IPD) to specify the SQL type of a SQL_SS_TIME2 or SQL_SS_TIMESTAMPOFFSET parameter that has SQL_C_BINARY C type bound with type SQL_SS_VARIANT.  
  
 The new types SQL_SS_TIME2 and SQL_SS_TIMESTAMPOFFSET can be set by SQLColAttribute. SQL_CA_SS_VARIANT_SQL_TYPE can be returned by SQLGetDescField.  
  
 For result columns, the driver will convert from the variant to date/time types. For more information, see [Conversions from SQL to C](../../relational-databases/native-client-odbc-date-time/datetime-data-type-conversions-from-sql-to-c.md). When binding to SQL_C_BINARY, the buffer length must be large enough to receive the struct that corresponds to the SQL type.  
  
 For the SQL_SS_TIME2 and SQL_SS_TIMESTAMPOFFSET parameters, the driver will convert C values to **sql_variant** values, as described in the table below. If a parameter is bound as SQL_C_BINARY and the server type is SQL_SS_VARIANT, it will be treated as a binary value unless the application has set SQL_CA_SS_VARIANT_SQL_TYPE to some other SQL type. In this case, SQL_CA_SS_VARIANT_SQL_TYPE takes precedence; that is, if SQL_CA_SS_VARIANT_SQL_TYPE is set, it overrides the default behavior of deducing the variant SQL type from the C type.  
  
|C type|Server type|Comments|  
|------------|-----------------|--------------|  
|SQL_C_CHAR|varchar|SQL_CA_SS_VARIANT_SQL_TYPE is ignored.|  
|SQL_C_WCHAR|nvarcar|SQL_CA_SS_VARIANT_SQL_TYPE is ignored.|  
|SQL_C_TINYINT|smallint|SQL_CA_SS_VARIANT_SQL_TYPE is ignored.|  
|SQL_C_STINYINT|smallint|SQL_CA_SS_VARIANT_SQL_TYPE is ignored.|  
|SQL_C_SHORT|smallint|SQL_CA_SS_VARIANT_SQL_TYPE is ignored.|  
|SQL_C_SSHORT|smallint|SQL_CA_SS_VARIANT_SQL_TYPE is ignored.|  
|SQL_C_USHORT|int|SQL_CA_SS_VARIANT_SQL_TYPE is ignored.|  
|SQL_C_LONG|int|SQL_CA_SS_VARIANT_SQL_TYPE is ignored.|  
|SQL_C_SLONG|int|SQL_CA_SS_VARIANT_SQL_TYPE is ignored.|  
|SQL_C_ULONG|bigint|SQL_CA_SS_VARIANT_SQL_TYPE is ignored.|  
|SQL_C_SBIGINT|bigint|SQL_CA_SS_VARIANT_SQL_TYPE is ignored.|  
|SQL_C_FLOAT|real|SQL_CA_SS_VARIANT_SQL_TYPE is ignored.|  
|SQL_C_DOUBLE|float|SQL_CA_SS_VARIANT_SQL_TYPE is ignored.|  
|SQL_C_BIT|bit|SQL_CA_SS_VARIANT_SQL_TYPE is ignored.|  
|SQL_C_UTINYINT|tinyint|SQL_CA_SS_VARIANT_SQL_TYPE is ignored.|  
|SQL_C_BINARY|varbinary|SQL_CA_SS_VARIANT_SQL_TYPE is not set.|  
|SQL_C_BINARY|time|SQL_CA_SS_VARIANT_SQL_TYPE = SQL_SS_TIME2<br /><br /> Scale is set to SQL_DESC_PRECISION (the *DecimalDigits* parameter of **SQLBindParameter**).|  
|SQL_C_BINARY|datetimeoffset|SQL_CA_SS_VARIANT_SQL_TYPE = SQL_SS_TIMESTAMPOFFSET<br /><br /> Scale is set to SQL_DESC_PRECISION (the *DecimalDigits* parameter of **SQLBindParameter**).|  
|SQL_C_TYPE_DATE|date|SQL_CA_SS_VARIANT_SQL_TYPE is ignored.|  
|SQL_C_TYPE_TIME|time(0)|SQL_CA_SS_VARIANT_SQL_TYPE is ignored.|  
|SQL_C_TYPE_TIMESTAMP|datetime2|Scale is set to SQL_DESC_PRECISION (the *DecimalDigits* parameter of **SQLBindParameter**).|  
|SQL_C_NUMERIC|decimal|Precision is set to SQL_DESC_PRECISION (the *ColumnSize* parameter of **SQLBindParameter**).<br /><br /> Scale set to SQL_DESC_SCALE (the *DecimalDigits* parameter of SQLBindParameter).|  
|SQL_C_SS_TIME2|time|SQL_CA_SS_VARIANT_SQL_TYPE is ignored|  
|SQL_C_SS_TIMESTAMPOFFSET|datetimeoffset|SQL_CA_SS_VARIANT_SQL_TYPE is ignored|  
  
## See Also  
 [Date and Time Improvements &#40;ODBC&#41;](../../relational-databases/native-client-odbc-date-time/date-and-time-improvements-odbc.md)  
  
  
