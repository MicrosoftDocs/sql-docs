---
title: "Data Type Usage | Microsoft Docs"
ms.custom: ""
ms.date: "03/16/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: native-client
ms.topic: "reference"
helpviewer_keywords: 
  - "ODBC data types"
  - "ODBC data types, about ODBC data types"
  - "SQL Server Native Client ODBC driver, result sets"
  - "ODBC applications, result sets"
  - "data types [ODBC]"
  - "SQL Server Native Client ODBC driver, data types"
  - "data types [ODBC], about data types"
ms.assetid: 4f19b0d6-94ac-4a98-a121-57d38787864c
author: MightyPen
ms.author: genemi
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Data Type Usage
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]
[!INCLUDE[SNAC_Deprecated](../../includes/snac-deprecated.md)]

  The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client ODBC driver and [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] impose the following use of data types.  
  
|Data type|Limitation|  
|---------------|----------------|  
|Date literals|Date literals, when stored in a SQL_TYPE_TIMESTAMP column ([!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data types of **datetime** or **smalldatetime**), have a time value of 12:00:00.000 A.M.|  
|**money** and **smallmoney**|Only the integer parts of the **money** and **smallmoney** data types are significant. If the decimal part of SQL **money** data is truncated during data type conversion, the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client ODBC driver returns a warning, not an error.|  
|SQL_BINARY (nullable)|When connected to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] version 6.0 and earlier, if a SQL_BINARY column is nullable, the data that is stored in the data source is not padded with zeroes. When data from such a column is retrieved, the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client ODBC driver pads it with zeroes on the right. However, data that is created in operations performed by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], such as concatenation, does not have such padding.<br /><br /> Also, when data is placed in such a column in an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] 6.0 or earlier, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] truncates the data on the right if it is too long to fit into the column.<br /><br /> Note: The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client ODBC driver does support connecting to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] 6.5 and earlier.|  
|SQL_CHAR (truncation)|When connected to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] 6.0 and earlier, and data is placed into a SQL_CHAR column, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] truncates it on the right without warning if the data is too long to fit into the column.<br /><br /> Note: The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client ODBC driver does support connecting to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] 6.5 and earlier.|  
|SQL_CHAR (nullable)|When connected to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] 6.0 and earlier, if a SQL_CHAR column is nullable, the data that is stored in the data source is not padded with blanks. When data from such a column is retrieved, the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client ODBC driver pads it with blanks on the right. However, data that is created in operations performed by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], such as concatenation, does not have such padding.<br /><br /> Note: The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client ODBC driver does support connecting to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] 6.5 and earlier.|  
|SQL_LONGVARBINARY, SQL_LONGVARCHAR, SQL_WLONGVARCHAR|Updates of columns with SQL_LONGVARBINARY, SQL_LONGVARCHAR, or SQL_WLONGVARCHAR data types (using a WHERE clause) that affect multiple rows are fully supported when connected to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] 6.*x* and later. When connected to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] 4.2*x*, an S1000 error, "Partial insert/update. The insert/update of a text or image column(s) did not succeed" is returned if the update affects more than one row.<br /><br /> Note: The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client ODBC driver does support connecting to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] 6.5 and earlier.|  
|String function parameters|*string_exp* parameters to the string functions must be of data type SQL_CHAR or SQL_VARCHAR. SQL_LONG_VARCHAR data types are not supported in the string functions. The *count* parameter must be less than or equal to 8,000 because the SQL_CHAR and SQL_VARCHAR data types are limited to a maximum length of 8,000 characters.|  
|Time literals|Time literals, when stored in a SQL_TIMESTAMP column ([!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data types of **datetime** or **smalldatetime**), have a date value of January 1, 1900.|  
|**timestamp**|Only a NULL value can be manually inserted into a **timestamp** column. However, because **timestamp**columns are automatically updated by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], a NULL value is overwritten.|  
|**tinyint**|The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] **tinyint** data type is unsigned. A **tinyint** column is bound to a variable of data type SQL_C_UTINYINT by default.|  
|Alias data types|When connected to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] 4.2*x*, the ODBC driver adds NULL to a column definition that does not explicitly declare a column's nullability. Therefore, the nullability that is stored in the definition of an alias data type is ignored.<br /><br /> When connected to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] 4.2*x*, columns with an alias data type that has a base data type of **char** or **binary** and for which no nullability is declared are created as data type **varchar** or **varbinary**. [SQLColAttribute](../../relational-databases/native-client-odbc-api/sqlcolattribute.md), [SQLColumns](../../relational-databases/native-client-odbc-api/sqlcolumns.md), and [SQLDescribeCol](../../relational-databases/native-client-odbc-api/sqldescribecol.md) return SQL_VARCHAR or SQL_VARBINARY as the data type for these columns. Data that is retrieved from these columns is not padded.<br /><br /> Note: The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client ODBC driver does support connecting to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] 6.5 and earlier.|  
|LONG data types|*data-at-execution* parameters are restricted for both the SQL_LONGVARBINARY and the SQL_LONGVARCHAR data types.|  
|Large value types|The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client ODBC driver will expose **varchar(max)**, **varbinary(max)**, and **nvarchar(max)** types as SQL_VARCHAR, SQL_VARBINARY and SQL_WVARCHAR (respectively) in APIs that accept or return ODBC SQL data types.|  
|User-defined type (UDT)|UDT columns are mapped as SQL_SS_UDT. If a UDT column is mapped explicitly to another type in the SQL statement using the ToString() or ToXMLString() methods of the UDT, or via the CAST/CONVERT functions, the type of the column in the result set will reflect the actual type to which the column was converted.<br /><br /> The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client ODBC driver can only bind to a UDT column as binary. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] only supports conversion between the SQL_SS_UDT and SQL_C_BINARY data types.|  
|XML|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] will automatically convert XML to Unicode text. The XML type is mapped as SQL_SS_XML.|  
  
## See Also  
 [Processing Results &#40;ODBC&#41;](../../relational-databases/native-client-odbc-results/processing-results-odbc.md)  
  
  
