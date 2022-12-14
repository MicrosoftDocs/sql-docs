---
description: "Table-Valued Parameter Descriptor Fields"
title: "Table-Valued Parameter Descriptor Fields | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: native-client
ms.topic: "reference"
helpviewer_keywords: 
  - "table-valued parameters (ODBC), descriptor fields"
ms.assetid: 4e009eff-c156-4d63-abcf-082ddd304de2
author: markingmyname
ms.author: maghan
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Table-Valued Parameter Descriptor Fields
[!INCLUDE[SQL Server Azure SQL Database Synapse Analytics PDW ](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  Support for table-valued parameters includes new [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]-specific fields in ODBC application parameter descriptors (APDs) and implementation parameter descriptors (IPDs).  
  
## Remarks  
  
|Name|Location|Type|Description|  
|----------|--------------|----------|-----------------|  
|SQL_CA_SS_TYPE_NAME|IPD|SQLTCHAR*|The server type name of the table-valued parameter.<br /><br /> When a table-valued parameter type name is specified on a call to SQLBindParameter, it must always be specified as a Unicode value, even in applications that are built as ANSI applications. The value used for the parameter *StrLen_or_IndPtr* should be either SQL_NTS or the string length of the name multiplied by sizeof(WCHAR).<br /><br /> When a table-valued parameter type name is specified via SQLSetDescField, it can be specified by using a literal that conforms to the way the application is built. The ODBC Driver Manager will perform any required Unicode conversion.|  
|SQL_CA_SS_TYPE_CATALOG_NAME (read only)|IPD|SQLTCHAR*|The catalog where the type is defined.|  
|SQL_CA_SS_TYPE_SCHEMA_NAME|IPD|SQLTCHAR*|The schema where the type is defined.|  
  
 Applications must not set SQL_CA_SS_TYPE_CATALOG_NAME for table-valued parameters. Doing so will return a SQL_ERROR and log a diagnostic record with SQLSTATE = HY091 and the message "Invalid descriptor field identifier".  
  
 The following statement attributes and descriptor header fields apply to table-valued parameters when the parameter focus is set to a table-valued parameter:  
  
|Name|Location|Type|Description|  
|----------|--------------|----------|-----------------|  
|SQL_ATTR_PARAMSET_SIZE<br /><br /> (This is equivalent to SQL_DESC_ARRAY_SIZE in the APD.)|APD|SQLUINTEGER|The array size of the buffer arrays for a table-valued parameter. This is the maximum number of rows the buffers will accommodate or the size of the buffers in rows; the table-valued parameter value itself might have more or fewer rows than the buffers can hold. Default is 1.<br /><br /> Note: If SQL_SOPT_SS_PARAM_FOCUS is set to its default value of 0, SQL_ATTR_PARAMSET_SIZE refers to the statement and specifies the number of parameter sets. If SQL_SOPT_SS_PARAM_FOCUS is set to the ordinal of a table-valued parameter, it refers to the table-valued parameter and specifies the number of rows per parameter set for the table-valued parameter.|  
|SQL_ATTR_PARAM _BIND_TYPE|APD|SQLINTEGER|The default is SQL_PARAM_BIND_BY_COLUMN.<br /><br /> To select row-wise binding, this field is set to the length of the structure or an instance of a buffer that will be bound to a set of table-valued parameter rows. This length must include space for all of the bound columns and any padding of the structure or buffer. This ensures that when the address of a bound column is incremented with the specified length, the result will point to the beginning of the same column in the next row. When using the **sizeof** operator in ANSI C, this behavior is guaranteed.|  
|SQL_ATTR_PARAM_BIND_OFFSET_PTR|APD|SQLINTEGER*|The default is a null pointer.<br /><br /> If this field is non-null, the driver dereferences the pointer, adds the dereferenced value to each of the deferred fields in the descriptor record (SQL_DESC_DATA_PTR, SQL_DESC_INDICATOR_PTR, and SQL_DESC_OCTET_LENGTH_PTR), and uses the new pointer values to access data values.|  
  
 These fields are only valid with table-valued parameters, and are ignored for other data types.  
  
 SQL_CA_SS_TYPE_NAME is optional for stored procedure calls. It must be specified for SQL statements that are not procedure calls to enable the server to determine the type of the table-valued parameter.  
  
 If the type name is reqired and the table type for the table-valued parameter is defined in a different schema than the stored procedure, SQL_CA_SS_TYPE_SCHEMA_NAME must be specified in the implementation parameter descriptor (IPD). If not, the server will not be able to determine the type of the table-valued parameter. This will result in an error when you call SQLExecute or SQLExecDirect. The error will have SQLSTATE= 07006 and the message "Restricted data type attribute violation".  
  
 Table-valued parameter columns can use either row-wise or column-wise binding. The default is column-wise binding. Row-wise binding can be specified by setting SQL_ATTR_PARAM_BIND_TYPE and SQL_ATTR_ PARAM_BIND_OFFSET_PTR. This is analogous to row-wise binding of columns and parameters.  
  
 SQL_CA_SS_TYPE_CATALOG_NAME and SQL_CA_SS_TYPE_SCHEMA_NAME can also be used to retrieve the catalog and schema associated with CLR user-defined type parameters. These are alternatives to the existing type specific catalog schema attributes for these types.  
  
## See Also  
 [Table-Valued Parameters &#40;ODBC&#41;](../../relational-databases/native-client-odbc-table-valued-parameters/table-valued-parameters-odbc.md)  
  
  
