---
title: "getFunctionColumns Method (SQLServerDatabaseMetaData)"
description: "getFunctionColumns Method (SQLServerDatabaseMetaData)"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
---
# getFunctionColumns Method (SQLServerDatabaseMetaData)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Retrieves a description of the specified catalog's system- or user-function parameters and return type.  
  
## Syntax  
  
```  
  
public ResultSet getFunctionColumns(java.lang.String catalog,  
                       java.lang.String schemaPattern,  
                       java.lang.String functionNamePattern  
                       java.lang.String columnNamePattern)  
```  
  
#### Parameters  
 *catalog*  
  
 A **String** that contains the catalog name. If it is an empty string "", the result includes the functions without a catalog. If it is **null**, the catalog name is not used for search.  
  
 *schemaPattern*  
  
 A **String** that contains the schema name pattern. If it is an empty string "", the result includes the functions without a schema. If it is **null**, the schema name is not used for search.  
  
 *functionNamePattern*  
  
 A **String** that contains the name of a function.  
  
 *columnNamePattern*  
  
 A **String** that contains the name of a parameter.  
  
## Return Value  
 A [SQLServerResultSet](../../../connect/jdbc/reference/sqlserverresultset-class.md) object.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This getFunctionColumns method is specified by the getFunctionColumns method in the java.sql.DatabaseMetaData interface.  
  
 This method returns only the functions and parameters matching the specified schema, function name, and parameter name within the specified catalog.  
  
 Each row in the result set includes the following columns for a parameter description, a column description, or a return type:  
  
|Name|Type|Description|  
|----------|----------|-----------------|  
|FUNCTION_CAT|**String**|The name of the database in which the function resides.|  
|FUNCTION_SCHEM|**String**|The schema for the function.|  
|FUNCTION_NAME|**String**|The name of the function.|  
|COLUMN_NAME|**String**|The name of a parameter or column.|  
|COLUMN_TYPE|**short**|**The type of the column. It can be one of the following values:**<br /><br /> functionColumnUnknown (0): Unknown type.<br /><br /> functionColumnIn (1): Input parameter.<br /><br /> functionColumnInOut (2): Input/Output parameter.<br /><br /> functionColumnOut (3): Output parameter.<br /><br /> functionReturn (4): Function return value.<br /><br /> functionColumnResult (5): A parameter or column is a column in the result set.|  
|DATA_TYPE|**smallint**|The SQL data type value from Java.sql.Types.|  
|TYPE_NAME|**String**|The name of the data type.|  
|PRECISION|**int**|The total number of significant digits.|  
|LENGTH|**int**|The length of the data in bytes.|  
|SCALE|**short**|The number of digits to the right of the decimal point.|  
|RADIX|**short**|The base for numeric types.|  
|NULLABLE|**short**|Indicates if the parameter or return value can contain a **null** value.<br /><br /> **It can be one of the following values:**<br /><br /> functionNoNulls (0): NULL value is not allowed.<br /><br /> functionNullable (1): NULL value is allowed.<br /><br /> functionNullableUnknown (2): Unknown.|  
|REMARKS|**String**|The comments about a column or a parameter.|  
|COLUMN_DEF|**String**|The default value of the column.<br /><br /> **Note:** This information is available with [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] and is JDBC driver-specific.|  
|SQL_DATA_TYPE|**smallint**|This column is the same as the **DATA_TYPE** column, except for the **datetime** and ISO **interval** data types.<br /><br /> **Note:** This information is available with [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] and is JDBC driver-specific.|  
|SQL_DATETIME_SUB|**smallint**|The **datetime** ISO **interval** subcode if the value of **SQL_DATA_TYPE** is **SQL_DATETIME** or **SQL_INTERVAL**. For data types other than **datetime** and ISO **interval**, this column is NULL.<br /><br /> **Note:** This information is available with [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] and is JDBC driver-specific.|  
|CHAR_OCTET_LENGTH|**int**|The maximum length of binary and character based parameters or columns. For other data types, it is NULL.|  
|ORDINAL_POSITION|**int**|For input and output parameters, it represents the position starting from 1.<br /><br /> For result set columns, it is the position of the column in the result set starting from 1.<br /><br /> For return value, it is 0.|  
|IS_NULLABLE|**String**|Determines the nullability of a parameter or column.<br /><br /> It can be one of the following values:<br /><br /> **YES**: The parameter or column can include NULL values.<br /><br /> **NO**: The parameter or column can not include NULL values.<br /><br /> Empty string (""): Unknown.|  
|SS_TYPE_CATALOG_NAME|**String**|The name of the catalog that contains the user-defined type (UDT).|  
|SS_TYPE_SCHEMA_NAME|**String**|The name of the schema that contains the user-defined type (UDT).|  
|SS_UDT_CATALOG_NAME|**String**|The fully-qualified name user-defined type (UDT).|  
|SS_UDT_SCHEMA_NAME|**String**|The name of the catalog where an XML schema collection name is defined. If the catalog name cannot be found, this variable contains an empty string.|  
|SS_UDT_ASSEMBLY_TYPE_NAME|**String**|The name of the schema where an XML schema collection name is defined. If the schema name cannot be found, this is an empty string.|  
|SS_XML_SCHEMACOLLECTION_CATALOG_NAME|**String**|The name of an XML schema collection. If the name cannot be found, this is an empty string.|  
|SS_XML_SCHEMACOLLECTION_SCHEMA_NAME|**String**|The name of the catalog that contains the user-defined type (UDT).|  
|SS_XML_SCHEMACOLLECTION_NAME|**String**|The name of the schema that contains the user-defined type (UDT).|  
|SS_DATA_TYPE|**tinyint**|The [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] data type that is used by extended stored procedures.<br /><br /> **Note** For more information about the data types returned by [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)], see "Data Types (Transact-SQL)" in [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Books Online.|  
  
## See Also  
 [SQLServerDatabaseMetaData Members](../../../connect/jdbc/reference/sqlserverdatabasemetadata-members.md)   
 [SQLServerDatabaseMetaData Class](../../../connect/jdbc/reference/sqlserverdatabasemetadata-class.md)  
  
  
