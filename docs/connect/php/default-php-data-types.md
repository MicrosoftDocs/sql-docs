---
title: "Default PHP data types"
description: "This topic lists all the default PHP Data types with their corresponding SQL Server data types when using the Microsoft SQLSRV Driver for PHP for SQL Server"
author: David-Engel
ms.author: v-davidengel
ms.date: "08/10/2020"
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords:
  - "default data types"
  - "converting data types"
---
# Default PHP Data Types
[!INCLUDE[Driver_PHP_Download](../../includes/driver_php_download.md)]

When retrieving data from the server, the [!INCLUDE[ssDriverPHP](../../includes/ssdriverphp_md.md)] converts data to a default PHP data type if no PHP data type has been specified by the user.  
  
When data is returned using the PDO_SQLSRV driver, the data type will either be int or string.  
  
The remainder of this topic discusses default data types using the SQLSRV driver.  
  
The following table lists the SQL Server data type (the data type being retrieved from the server), the default PHP data type (the data type to which data is converted), and the default encoding for streams and strings. For details about how to specify data types when retrieving data from the server, see [How to: Specify PHP Data Types](../../connect/php/how-to-specify-php-data-types.md).  
  
|SQL Server Type|Default PHP Type|Default Encoding|  
|-------------------|--------------------|--------------------|  
|bigint|String|8-bit character<sup>1</sup>|  
|binary|Stream<sup>2</sup>|Binary<sup>3</sup>|  
|bit|Integer|8-bit character<sup>1</sup>|  
|char|String|8-bit character<sup>1</sup>|  
|date<sup>4</sup>|Datetime|Not applicable|  
|datetime<sup>4</sup>|Datetime|Not applicable|  
|datetime2<sup>4</sup>|Datetime|Not applicable|  
|datetimeoffset<sup>4</sup>|Datetime|Not applicable|  
|decimal|String|8-bit character<sup>1</sup>|  
|float|Float|8-bit character<sup>1</sup>|  
|geography|STREAM|Binary<sup>3</sup>|  
|geometry|STREAM|Binary<sup>3</sup>|  
|image<sup>5</sup>|Stream<sup>2</sup>|Binary<sup>3</sup>|  
|int|Integer|8-bit character<sup>1</sup>|  
|money|String|8-bit character<sup>1</sup>|  
|nchar|String|8-bit character<sup>1</sup>|  
|numeric|String|8-bit character<sup>1</sup>|  
|nvarchar|String|8-bit character<sup>1</sup>|  
|nvarchar(MAX)|Stream<sup>2</sup>|8-bit character<sup>1</sup>|  
|ntext<sup>6</sup>|Stream<sup>2</sup>|8-bit character<sup>1</sup>|  
|real|Float|8-bit character<sup>1</sup>|  
|smalldatetime|Datetime|8-bit character<sup>1</sup>|  
|smallint|Integer|8-bit character<sup>1</sup>|  
|smallmoney|String|8-bit character<sup>1</sup>|  
|sql_variant<sup>7</sup>|String|8-bit character<sup>1</sup>|  
|text<sup>8</sup>|Stream<sup>2</sup>|8-bit character<sup>1</sup>|  
|time<sup>4</sup>|Datetime|Not applicable|  
|timestamp|String|8-bit character<sup>1</sup>|  
|tinyint|Integer|8-bit character<sup>1</sup>|  
|UDT|Stream<sup>2</sup>|Binary<sup>3</sup>|  
|uniqueidentifier|String<sup>9</sup>|8-bit character<sup>1</sup>|  
|varbinary|Stream<sup>2</sup>|Binary<sup>3</sup>|  
|varbinary(MAX)|Stream<sup>2</sup>|Binary<sup>3</sup>|  
|varchar|String|8-bit character<sup>1</sup>|  
|varchar(MAX)|Stream<sup>2</sup>|8-bit character<sup>1</sup>|
|xml|Stream<sup>2</sup>|8-bit character<sup>1</sup>|  
  

1.  Data is returned in 8-bit characters as specified in the code page of the Windows locale set on the system. Any multi-byte characters or characters that do not map into this code page are substituted with a single-byte question mark (?) character.  
  
2.  If [sqlsrv_fetch_array](../../connect/php/sqlsrv-fetch-array.md) or [sqlsrv_fetch_object](../../connect/php/sqlsrv-fetch-object.md) is used to retrieve data that has a default PHP type of Stream, the data is returned as a string with the same encoding as the stream. For example, if a SQL Server binary type is retrieved by using **sqlsrv_fetch_array**, the default return type is a binary string.  
  
3.  Data is returned as a raw byte stream from the server without performing encoding or translation.  

4.  Date and time types can be retrieved as strings. For more information, see [How to: Retrieve Date and Time Type as Strings Using the SQLSRV Driver](../../connect/php/how-to-retrieve-date-and-time-type-as-strings-using-the-sqlsrv-driver.md).  

5.  This is a legacy type that maps to the varbinary(max) type.

6. This is a legacy type that maps to the nvarchar(max) type.

7.  sql_variant is not supported in bidirectional or output parameters.

8.  This is a legacy type that maps to the varchar(max) type.  
  
9.  UNIQUEIDENTIFIERs are GUIDs represented by the following regular expression:  
  
    [0-9a-fA-F]{8}-[0-9a-fA-F]{4}-[0-9a-fA-f]{4}-[0-9a-fA-f]{4}-[0-9a-fA-F]{12}  
 
 
## Other New SQL Server 2008 Data Types and Features  
Data types that are new in SQL Server 2008 and that exist outside of columns (such as table-valued parameters) are not supported in the [!INCLUDE[ssDriverPHP](../../includes/ssdriverphp_md.md)]. The following table summarizes the PHP support for new SQL Server 2008 features.  
  
|Feature|PHP Support|  
|-----------|---------------|  
|Table-valued parameter|No|  
|Sparse columns|Partial|  
|Null-bit compression|Yes|  
|Large CLR user-defined types (UDTs)|Yes|  
|Service principal name|No|  
|MERGE|Yes|  
|FILESTREAM|Partial|  
  
Partial type support means that you cannot programmatically query for the type of the column.  
  
## See Also  
[Constants &#40;Microsoft Drivers for PHP for SQL Server&#41;](../../connect/php/constants-microsoft-drivers-for-php-for-sql-server.md)

[Converting Data Types](../../connect/php/converting-data-types.md)

[PHP Types](https://php.net/manual/en/language.types.php)

[Data Types (Transact-SQL)](../../t-sql/data-types/data-types-transact-sql.md)

[sqlsrv_field_metadata](../../connect/php/sqlsrv-field-metadata.md)  
  
