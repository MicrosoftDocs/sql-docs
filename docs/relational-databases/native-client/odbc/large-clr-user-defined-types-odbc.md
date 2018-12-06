---
title: "Large CLR User-Defined Types (ODBC) | Microsoft Docs"
ms.custom: ""
ms.date: "03/17/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: native-client
ms.topic: "reference"
helpviewer_keywords: 
  - "ODBC, large user-defined types"
  - "large user-defined types [ODBC]"
ms.assetid: ddce337e-bb6e-4a30-b7cc-4969bb1520a9
author: MightyPen
ms.author: genemi
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Large CLR User-Defined Types (ODBC)
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]
[!INCLUDE[SNAC_Deprecated](../../../includes/snac-deprecated.md)]

  This topic discusses the changes to ODBC in SQL Server Native Client to support large common language runtime (CLR) user-defined types (UDTs).  
  
 For a sample showing ODBC support for large CLR UDTs, see [Support for Large UDTs](../../../relational-databases/native-client-odbc-how-to/support-for-large-udts.md).  
  
 For more information about support for large CLR UDTs in SQL Server Native Client, see [Large CLR User-Defined Types](../../../relational-databases/native-client/features/large-clr-user-defined-types.md).  
  
## Data Format  
 SQL Server Native Client uses SQL_SS_LENGTH_UNLIMITED to denote that the size of a column is greater than 8,000 bytes for large object (LOB) types. Beginning with SQL Server 2008, the same value is used for CLR UDTs when their size is greater than 8,000 bytes.  
  
 UDT values are represented as byte arrays. Conversions to and from hex strings are supported. Literal values are represented as hex strings with a prefix of "0x".  
  
 The following table shows data type mapping in parameters and result sets:  
  
|SQL Server data type|SQL data type|Value|  
|--------------------------|-------------------|-----------|  
|CLR UDT|SQL_SS_UDT|-151 (sqlncli.h)|  
  
 The following table discusses the corresponding structure and ODBC C type. Essentially, CLR UDT is a **varbinary** type with additional metadata.  
  
|SQL data type|Memory layout|C data type|Value (sqlext.h)|  
|-------------------|-------------------|-----------------|------------------------|  
|SQL_SS_UDT|SQLCHAR *(unsigned char \*)|SQL_C_BINARY|SQL_BINARY (-2)|  
  
## Descriptor Fields for Parameters  
 Information returned in the IPD fields is as follows:  
  
|Descriptor field|SQL_SS_UDT<br /><br /> (length less than or equal to 8,000 bytes)|SQL_SS_UDT<br /><br /> (length greater than 8,000 bytes)|  
|----------------------|-------------------------------------------------------------------|----------------------------------------------------------|  
|SQL_DESC_CASE_SENSITIVE|SQL_FALSE|SQL_FALSE|  
|SQL_DESC_CONCISE_TYPE|SQL_SS_UDT|SQL_SS_UDT|  
|SQL_DESC_DATETIME_INTERVAL_CODE|0|0|  
|SQL_DESC_DATETIME_INTERVAL_PRECISION|*n*|SQL_SS_LENGTH_UNLIMITED (0)|  
|SQL_DESC_FIXED_PREC_SCALE|SQL_FALSE|SQL_FALSE|  
|SQL_DESC_LENGTH|*n*|SQL_SS_LENGTH_UNLIMITED (0)|  
|SQL_DESC_LOCAL_TYPE_NAME|"udt"|"udt"|  
|SQL_DESC_OCTET_LENGTH|*n*|SQL_SS_LENGTH_UNLIMITED (0)|  
|SQL_DESC_PRECISION|*n*|SQL_SS_LENGTH_UNLIMITED (0)|  
|SQL_DESC_SCALE|0|0|  
|SQL_DESC_TYPE|SQL_SS_UDT|SQL_SS_UDT|  
|SQL_DESC_TYPE_NAME|"udt"|"udt"|  
|SQL_DESC_UNSIGNED|SQL_TRUE|SQL_TRUE|  
|SQL_CA_SS_UDT_CATALOG_NAME|The name of the catalog that contains the UDT.|The name of the catalog that contains the UDT.|  
|SQL_CA_SS_UDT_SCHEMA_NAME|The name of the schema that contains the UDT.|The name of the schema the contains the UDT.|  
|SQL_CA_SS_UDT_TYPE_NAME|The name of the UDT.|The name of the UDT.|  
|SQL_CA_SS_UDT_ASSEMBLY_TYPE_NAME|The fully-qualified name of the UDT.|The fully-qualified name of the UDT.|  
  
 For UDT parameters, SQL_CA_SS_UDT_TYPE_NAME must always be set via **SQLSetDescField**. SQL_CA_SS_UDT_CATALOG_NAME and SQL_CA_SS_UDT_SCHEMA_NAME are optional.  
  
 If the UDT is defined in the same database with a different schema than the table, SQL_CA_SS_UDT_SCHEMA_NAME must be set.  
  
 If the UDT is defined in a different database than the table, SQL_CA_SS_UDT_CATALOG_NAME and SQL_CA_SS_UDT_SCHEMA_NAME must be set.  
  
 If there are any errors or omissions in the settings for SQL_CA_SS_UDT_TYPE_NAME, SQL_CA_SS_UDT_CATALOG_NAME, or SQL_CA_SS_UDT_SCHEMA_NAME, a diagnostic record is generated with SQLSTATE HY000 and server-specific message text.  
  
## Descriptor Fields for Results  
 Information returned in the IRD fields is as follows:  
  
|Descriptor field|SQL_SS_UDT<br /><br /> (length less than or equal to 8,000 bytes)|SQL_SS_UDT<br /><br /> (length greater than 8,000 bytes)|  
|----------------------|-------------------------------------------------------------------|----------------------------------------------------------|  
|SQL_DESC_AUTO_UNIQUE_VALUE|SQL_FALSE|SQL_FALSE|  
|SQL_DESC_CASE_SENSITIVE|SQL_FALSE|SQL_FALSE|  
|SQL_DESC_CONCISE_TYPE|SQL_SS_UDT|SQL_SS_UDT|  
|SQL_DESC_DATETIME_INTERVAL_CODE|0|0|  
|SQL_DESC_DATETIME_INTERVAL_PRECISION|*n*|SQL_SS_LENGTH_UNLIMITED (0)|  
|SQL_DESC_DISPLAY_SIZE|2*n*|SQL_SS_LENGTH_UNLIMITED (0)|  
|SQL_DESC_FIXED_PREC_SCALE|SQL_FALSE|SQL_FALSE|  
|SQL_DESC_LENGTH|*n*|SQL_SS_LENGTH_UNLIMITED (0)|  
|SQL_DESC_LITERAL_PREFIX|"0x"|"0x"|  
|SQL_DESC_LITERAL_SUFFIX|""|""|  
|SQL_DESC_LOCAL_TYPE_NAME|"udt"|"udt"|  
|SQL_DESC_OCTET_LENGTH|*n*|SQL_SS_LENGTH_UNLIMITED (0)|  
|SQL_DESC_PRECISION|*n*|SQL_SS_LENGTH_UNLIMITED (0)|  
|SQL_DESC_SCALE|0|0|  
|SQL_DESC_SEARCHABLE|SQL_PRED_NONE|SQL_PRED_NONE|  
|SQL_DESC_TYPE|SQL_SS_UDT|SQL_SS_UDT|  
|SQL_DESC_TYPE_NAME|"udt"|"udt"|  
|SQL_DESC_UNSIGNED|SQL_TRUE|SQL_TRUE|  
|SQL_CA_SS_UDT_CATALOG_NAME|The name of the catalog that contains the UDT.|The name of the catalog that contains the UDT.|  
|SQL_CA_SS_UDT_SCHEMA_NAME|The name of the schema that contains the UDT.|The name of the schema that contains the UDT.|  
|SQL_CA_SS_UDT_TYPE_NAME|The name of the UDT.|The name of the UDT.|  
|SQL_CA_SS_UDT_ASSEMBLY_TYPE_NAME|The fully-qualified name of the UDT.|The fully-qualified name of the UDT.|  
  
## Column Metadata Returned by SQLColumns and SQLProcedureColumns (Catalog Metadata)  
 The following column values are returned for UDTs:  
  
|Column name|SQL_SS_UDT<br /><br /> (length less than or equal to 8,000 bytes)|SQL_SS_UDT<br /><br /> (length greater than 8,000 bytes)|  
|-----------------|-------------------------------------------------------------------|----------------------------------------------------------|  
|DATA_TYPE|SQL_SS_UDT|SQL_SS_UDT|  
|TYPE_NAME|The name of the UDT.|The name of the UDT.|  
|COLUMN_SIZE|*n*|SQL_SS_LENGTH_UNLIMITED (0)|  
|BUFFER_LENGTH|*n*|SQL_SS_LENGTH_UNLIMITED (0)|  
|DECIMAL_DIGITS|NULL|NULL|  
|SQL_DATA_TYPE|SQL_SS_UDT|SQL_SS_UDT|  
|SQL_DATETIME_SUB|NULL|NULL|  
|CHAR_OCTET_LENGTH|*n*|SQL_SS_LENGTH_UNLIMITED (0)|  
|SS_UDT_CATALOG_NAME|The name of the catalog that contains the UDT.|The name of the catalog that contains the UDT.|  
|SS_UDT_SCHEMA_NAME|The name of the schema that contains the UDT.|The name of the schema that contains the UDT.|  
|SS_UDT_ASSEMBLY_TYPE_NAME|The fully-qualified name of the UDT.|The fully-qualified name of the UDT.|  
  
 The last three columns are driver-specific columns. They are added after any ODBC-defined columns, but before any existing driver-specific columns of the result set of SQLColumns or SQLProcedureColumns.  
  
 No rows are returned by SQLGetTypeInfo, for individual UDTs or for the generic type "udt".  
  
## Bindings and Conversions  
 The supported conversions from SQL to C datatypes are as follows:  
  
|Conversion to and from:|SQL_SS_UDT|  
|-----------------------------|------------------|  
|SQL_C_WCHAR|Supported *|  
|SQL_C_BINARY|Supported|  
|SQL_C_CHAR|Supported *|  
  
 \* Binary data is converted to a hex string.  
  
 The supported conversions from C to SQL datatypes are as follows:  
  
|Conversion to and from:|SQL_SS_UDT|  
|-----------------------------|------------------|  
|SQL_C_WCHAR|Supported *|  
|SQL_C_BINARY|Supported|  
|SQL_C_CHAR|Supported *|  
  
 \* Hex string to binary data conversion occurs.  
  
## SQL_VARIANT Support for UDTs  
 UDTs are not supported in SQL_VARIANT columns.  
  
## BCP Support for UDTs  
 UDTs values can be imported and exported only as character or binary values.  
  
## Downlevel Client Behavior for UDTs  
 UDTs are subject to type mapping with down-level clients, as follows:  
  
|Server version|SQL_SS_UDT<br /><br /> (length less than or equal to 8,000 bytes)|SQL_SS_UDT<br /><br /> (length greater than 8,000 bytes)|  
|--------------------|-------------------------------------------------------------------|----------------------------------------------------------|  
|SQL Server 2005|**UDT**|**varbinary(max)**|  
|SQL Server 2008 and later|**UDT**|**UDT**|  
  
## ODBC Functions Supporting Large CLR UDTs  
 This section discusses changes to SQL Server Native Client ODBC functions to support large CLR UDTs.  
  
### SQLBindCol  
 UDT result column values are converted from SQL to C datatypes as described in the "Bindings and Conversions" section, earlier in this topic.  
  
### SQLBindParameter  
 The values required for UDTs are as follows:  
  
|SQL data type|*Parametertype*|*ColumnSizePtr*|*DecimalDigitsPtr*|  
|-------------------|---------------------|---------------------|------------------------|  
|SQL_SS_UDT<br /><br /> (length less than or equal to 8,000 bytes)|SQL_SS_UDT|*n*|0|  
|SQL_SS_UDT<br /><br /> (length greater than 8,000 bytes)|SQL_SS_UDT|SQL_SS_LENGTH_UNLIMITED (0)|0|  
  
### SQLColAttribute  
 The values returned for UDTs are as described in the "Descriptor Fields for Results" section, earlier in this topic.  
  
### SQLColumns  
 The values returned for UDTs are as described in the "Column Metadata Returned by SQLColumns and SQLProcedureColumns (Catalog Metadata)" section, earlier in this topic.  
  
### SQLDescribeCol  
 The values returned for UDTs are as follows:  
  
|SQL data type|*DataTypePtr*|*ColumnSizePtr*|*DecimalDigitsPtr*|  
|-------------------|-------------------|---------------------|------------------------|  
|SQL_SS_UDT<br /><br /> (length less than or equal to 8,000 bytes)|SQL_SS_UDT|*n*|0|  
|SQL_SS_UDT<br /><br /> (length greater than 8,000 bytes)|SQL_SS_UDT|SQL_SS_LENGTH_UNLIMITED (0)|0|  
  
### SQLDescribeParam  
 The values returned for UDTs are as follows:  
  
|SQL data type|*DataTypePtr*|*ColumnSizePtr*|*DecimalDigitsPtr*|  
|-------------------|-------------------|---------------------|------------------------|  
|SQL_SS_UDT<br /><br /> (length less than or equal to 8,000 bytes)|SQL_SS_UDT|*n*|0|  
|SQL_SS_UDT<br /><br /> (length greater than 8,000 bytes)|SQL_SS_UDT|SQL_SS_LENGTH_UNLIMITED (0)|0|  
  
### SQLFetch  
 UDT result column values are converted from SQL to C datatypes as described in the "Bindings and Conversions" section, earlier in this topic.  
  
### SQLFetchScroll  
 UDT result column values are converted from SQL to C datatypes as described in the "Bindings and Conversions" section, earlier in this topic.  
  
### SQLGetData  
 UDT result column values are converted from SQL to C datatypes as described in the "Bindings and Conversions" section, earlier in this topic.  
  
### SQLGetDescField  
 Descriptor fields available with the new types are described in the "Descriptor Fields for Parameters" and "Descriptor Fields for Results" sections, earlier in this topic.  
  
### SQLGetDescRec  
 The values returned for UDTs are as follows:  
  
|SQL data type|Type|SubType|Length|Precision|Scale|  
|-------------------|----------|-------------|------------|---------------|-----------|  
|SQL_SS_UDT<br /><br /> (length less than or equal to 8,000 bytes)|SQL_SS_UDT|0|*n*|n|0|  
|SQL_SS_UDT<br /><br /> (length greater than 8,000 bytes)|SQL_SS_UDT|0|SQL_SS_LENGTH_UNLIMITED (0)|SQL_SS_LENGTH_UNLIMITED (0)|0|  
  
### SQLGetTypeInfo  
 The values returned for UDTs are as described in the "Metadata Returned by SQLColumns and SQLProcedureColumns (Catalog Metadata)" section, earlier in this topic.  
  
### SQLProcedureColumns  
 The values returned for UDTs are as described in the "Metadata Returned by SQLColumns and SQLProcedureColumns (Catalog Metadata)" section, earlier in this topic.  
  
### SQLPutData  
 UDT parameter values are converted from C to SQL datatypes as described in the "Bindings and Conversions" section, earlier in this topic.  
  
### SQLSetDescField  
 Descriptor field available with the new types are described in the "Descriptor Fields for Parameters" and "Descriptor Fields for Results" sections, earlier in this topic.  
  
### SQLSetDescRec  
 The values allowed for UDTs are as follows:  
  
|SQL data type|Type|SubType|Length|Precision|Scale|  
|-------------------|----------|-------------|------------|---------------|-----------|  
|SQL_SS_UDT<br /><br /> (length less than or equal to 8,000 bytes)|SQL_SS_UDT|0|*n*|*n*|0|  
|SQL_SS_UDT<br /><br /> (length greater than 8,000 bytes)|SQL_SS_UDT|0|SQL_SS_LENGTH_UNLIMITED (0)|SQL_SS_LENGTH_UNLIMITED (0)|0|  
  
### SQLSpecialColumns  
 The values returned for the columns DATA_TYPE, TYPE_NAME, COLUMN_SIZE, BUFFER_LENGTH, and DECIMAL_DIGTS UDTs are as described in the "Metadata Returned by SQLColumns and SQLProcedureColumns (Catalog Metadata)" section, earlier in this topic.  
  
## See Also  
 [Large CLR User-Defined Types](../../../relational-databases/native-client/features/large-clr-user-defined-types.md)  
  
  
