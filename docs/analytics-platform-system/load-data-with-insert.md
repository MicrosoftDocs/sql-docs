---
title: "Load Data With INSERT (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 6e951b0e-e95b-4fd1-b5f3-c65607aee0d8
caps.latest.revision: 21
author: BarbKess
---
# Load Data With INSERT (SQL Server PDW)
You can use the SQL INSERT statement to load data into a SQL Server PDW distributed or replicated table. For more information about INSERT, see [INSERT &#40;SQL Server PDW&#41;](../sqlpdw/insert-sql-server-pdw.md). For replicated tables and all non-distribution columns in a distributed table, SQL Server PDW uses SQL Server to implicitly convert the data values specified in the statement to the data type of the destination column. For more information about SQL Server data conversion rules, see [Data Type Conversion (Database Engine)](http://msdn.microsoft.com/en-us/library/ms191530(v=sql11).aspx) on MSDN. However, for distribution columns, SQL Server PDW supports only a subset of implicit conversions that SQL Server supports. Therefore, when you use the INSERT statement to load data into a distribution column, the source data must be specified in one of the formats defined in the following tables.  
  
## Contents  
  
-   [Inserting Literals into Binary Types](#InsertingLiteralsBinary)  
  
-   [Inserting Literals into Date and Time Types](#InsertingLiteralsDateTime)  
  
-   [Inserting Literals into Numeric Types](#InsertLiteralsNumeric)  
  
-   [Inserting Literals into String Types](#InsertLiteralsString)  
  
## <a name="InsertingLiteralsBinary"></a>Inserting Literals into Binary Types  
The following table defines the accepted literal types, format, and conversion rules for inserting a literal value into a distribution column of type **binary** (*n*) or **varbinary**(*n*).  
  
|Literal Type|Format|Conversion Rules|  
|----------------|----------|--------------------|  
|Binary literal|0x*hexidecimal_string*<br /><br />Example: 0x12Ef|Binary literals must be prefixed with 0x.<br /><br />The data source length cannot exceed the number of bytes specified for the data type.<br /><br />If the data source length is less than size of the **binary** data type, the data is padded to the right with zeros to reach the data type size.|  
  
## <a name="InsertingLiteralsDateTime"></a>Inserting Literals into Date and Time Types  
Date and time literals are represented by using character values in specific formats, enclosed in single quotation marks. The following tables define the allowed literal types, format, and conversion rules for inserting a date or time literal into a SQL Server PDW distribution column of type **datetime**, **smalldatetime**, **date**, **time**, **datetimeoffset**, or **datetime2**.  
  
### datetime Data Type  
The following table defines the accepted formats and rules for inserting literal values into a distribution column of type **datetime**. Any empty string ('') is converted to the default value '1900-01-01 12:00:00.000'. Strings that contain only blanks ('    ') generate an error.  
  
|Literal Type|Format|Conversion Rules|  
|----------------|----------|--------------------|  
|String literal in **datetime** format|'YYYY-MM-DD hh:mm:ss[.nnn]'<br /><br />Example: '2007-05-08 12:35:29.123'|Missing fractional digits are set to 0 when the value is inserted. For example, the literal '2007-05-08 12:35' is inserted as '2007-05-08 12:35:00.000'.|  
|String literal in **smalldatetime** format|'YYYY-MM-DD hh:mm'<br /><br />Example: '2007-05-08 12:35'|Seconds and remaining fractional digits are set to 0 when the value is inserted.|  
|String literal in **date** format|'YYYY-MM-DD'<br /><br />Example: '2007-05-08'|Time values (hour, minutes, seconds, and fractions) are set to 12:00:00.000 when the value is inserted.|  
|String literal in **datetime2** format|'YYYY-MM-DD hh:mm:ss.nnnnnnn'<br /><br />Example: '2007-05-08 12:35:29.1234567'|The source data cannot exceed three fractional digits. For example, the literal '2007-05-08 12:35:29.123' will be inserted, but the value '2007-05-08 12:35:29.1234567' generates an error.|  
  
### smalldatetime Data Type  
The following table defines the accepted formats and rules for inserting literal values into a distribution column of type **smalldatetime**. Any empty string ('') is converted to the default value '1900-01-01 12:00'. Strings that contain only blanks ('    ') generate an error.  
  
|Literal Type|Format|Conversion Rules|  
|----------------|----------|--------------------|  
|String literal in **smalldatetime** format|'YYYY-MM-DD hh:mm' or 'YYYY-MM-DD hh:mm:00'<br /><br />Example: '2007-05-08 12:00' or '2007-05-08 12:00:00'|The source data must have values for year, month, date, hour and minute. Seconds are optional and, if present, must be set to the value 00. Any other value generates an error.|  
|String literal in **date** format|'YYYY-MM-DD'<br /><br />Example: '2007-05-08'|Time values (hour, minutes, seconds, and fractions) are set to 0 when the value is inserted.|  
  
### date Data Type  
The following table defines the accepted formats and rules for inserting literal values into a distribution column of type **date**. Any empty string ('') is converted to the default value '1900-01-01'. Strings that contain only blanks ('    ') generate an error.  
  
|Literal Type|Format|Conversion Rules|  
|----------------|----------|--------------------|  
|String literal in **date** format|'YYYY-MM-DD'<br /><br />Example: '2007-05-08'|This is the only accepted format.|  
  
### time Data Type  
The following table defines the accepted formats and rules for inserting literal values into a distribution column of type **time**. Any empty string ('') is converted to the default value '00:00:00.0000'. Strings that contain only blanks ('    ') generate an error.  
  
|Literal Type|Format|Conversion Rules|  
|----------------|----------|--------------------|  
|String literal in **time** format|'hh:mm:ss.nnnnnnn'<br /><br />Example: '12:35:29.1234567'|If the data source has a smaller or equal precision (number of fractional digits) than the precision of the **time** data type, the data is padded to the right with zeros. For example, a literal value '12:35:29.123' is inserted as '12:35:29.1230000'.<br /><br />A value that has a larger precision than the target data type is rejected.|  
  
### datetimeoffset Data Type  
The following table defines the accepted formats and rules for inserting literal values into a distribution column of type **datetimeoffset** (*n*). The default format is 'YYYY-MM-DD hh:mm:ss.nnnnnnn {+|-}hh:mm'. An empty string ('') is converted to the default value '1900-01-01 12:00:00.0000000 +00:00'. Strings that contain only blanks ('    ') generate an error. The number of fractional digits depends on the column definition. For example, a column defined as **datetimeoffset** (2) will have two fractional digits.  
  
|Literal Type|Format|Conversion Rules|  
|----------------|----------|--------------------|  
|String literal in **datetime** format|'YYYY-MM-DD hh:mm:ss[.nnn]'<br /><br />Example: '2007-05-08 12:35:29.123'|Missing fractional digits and offset values are set to 0 when the value is inserted. For example, the literal '2007-05-08 12:35:29.123' is inserted as '2007-05-08 12:35:29.1230000 +00:00'.|  
|String literal in **smalldatetime** format|'YYYY-MM-DD hh:mm'<br /><br />Example: '2007-05-08 12:35'|Seconds, remaining fractional digits and offset values are set to 0 when the value is inserted.|  
|String literal in **date** format|'YYYY-MM-DD'<br /><br />Example: '2007-05-08'|Time values (hour, minutes, seconds, and fractions) are set to 0 when the value is inserted. For example, the literal '2007-05-08' is inserted as '2007-05-08 00:00:00.0000000 +00:00'.|  
|String literal in **datetime2** format|'YYYY-MM-DD hh:mm:ss.nnnnnnn'<br /><br />Example: '2007-05-08 12:35:29.1234567'|The source data cannot exceed the specified number of fractional seconds in the datetimeoffset column. If the data source has a smaller or equal number of fractional seconds, the data is padded to the right with zeros. For example, if the data type is datetimeoffset (5), the literal value '2007-05-08 12:35:29.123 +12:15' is inserted as '12:35:29.12300 +12:15'.|  
|String literal in **datetimeoffset** format|'YYYY-MM-DD hh:mm:ss.nnnnnnn {+&#124;-} hh:mm'<br /><br />Example: '2007-05-08 12:35:29.1234567 +12:15'|The source data cannot exceed the specified number of fractional seconds in the datetimeoffset column. If the data source has a smaller or equal number of fractional seconds, the data is padded to the right with zeros. For example, if the data type is datetimeoffset (5), the literal value '2007-05-08 12:35:29.123 +12:15' is inserted as '12:35:29.12300 +12:15'.|  
  
### datetime2 Data Type  
The following table defines the accepted formats and rules for inserting literal values into a distribution column of type **datetime2** (*n*). The default format is 'YYYY-MM-DD hh:mm:ss.nnnnnnn'. An empty string ('') is converted to the default value '1900-01-01 12:00:00'. Strings that contain only blanks ('    ') generate an error. The number of fractional digits depends on the column definition. For example, a column defined as **datetime2** (2) will have two fractional digits.  
  
|Literal Type|Format|Conversion Rules|  
|----------------|----------|--------------------|  
|String literal in **datetime** format|'YYYY-MM-DD hh:mm:ss[.nnn]'<br /><br />Example: '2007-05-08 12:35:29.123'|Fractional seconds are optional and are set to 0 when the value is inserted.<br /><br />A value that has more fractional digits than the target data type is rejected.|  
|String literal in **smalldatetime** format|'YYYY-MM-DD hh:mm'<br /><br />Example: '2007-05-08 12'|Optional seconds and remaining fractional digits are set to 0 when the value is inserted.|  
|String literal in **date** format|'YYYY-MM-DD'<br /><br />Example: '2007-05-08'|Time values (hour, minutes, seconds, and fractions) are set to 0 when the value is inserted. For example, the literal '2007-05-08' is inserted as '2007-05-08 12:00:00.0000000'.|  
|String literal in **datetime2** format|'YYYY-MM-DD hh:mm:ss:nnnnnnn'<br /><br />Example: '2007-05-08 12:35:29.1234567'|If the data source contains data and time components that are less than or equal to the value specified in **datetime2**(*n*), the data is inserted; otherwise an error is generated.|  
  
## <a name="InsertLiteralsNumeric"></a>Inserting Literals into Numeric Types  
The following tables define the accepted formats and conversion rules for inserting a literal value into a SQL Server PDW distribution column that uses a numeric type.  
  
### bit Data Type  
The following table defines the accepted formats and rules for inserting literal values into a distribution column of type **bit**. An empty string ('') or a string that contain only blanks ('    ') is converted to 0.  
  
|Literal Type|Format|Conversion Rules|  
|----------------|----------|--------------------|  
|String literal in **integer** format|'nnnnnnnnnn'<br /><br />Example: '1' or '321'|An integer value formatted as a string literal cannot contain a negative value. For example, the value '-123' generates an error.<br /><br />A value larger than 1 is converted to 1. For example, the value '123' is converted to 1.|  
|String literal|'TRUE' or 'FALSE'<br /><br />Example: 'true'|The value 'TRUE' is converted to 1; the value 'FALSE' is converted to 0.|  
|Integer literal|nnnnnnnn<br /><br />Example: 1 or 321|A value larger than 1 or less than 0 is converted to 1. For example, the values 123 and -123 are converted to 1.|  
|Decimal literal|nnnnn.nnnn<br /><br />Example: 1234.5678|A value larger than 1 or less than 0 is converted to 1. For example, the values 123.45 and -123.45 are converted to 1.|  
  
### decimal Data Type  
The following table defines the accepted formats and rules for inserting literal values into a distribution column of type **decimal** (*p,s*). The data conversion rules are the same as for SQL Server. For more information, see [Data Type Conversion (Database Engine)](http://msdn.microsoft.com/en-us/library/ms191530(v=sql11).aspx) on MSDN.  
  
|Literal Type|Format|  
|----------------|----------|  
|String literal in **integer** format|'nnnnnnnnnnnn'<br /><br />Example: '321312313123'|  
|String literal in **decimal** format|'nnnnnn.nnnnn'<br /><br />Example: '123344.34455'|  
|Integer literal|nnnnnnnnnnnn<br /><br />Example: 321312313123|  
|Decimal literal|nnnnnn.nnnnn<br /><br />Example: '123344.34455'|  
  
### float and real Data Types  
The following table defines the accepted formats and rules for inserting literal values into a distribution column of type **float** or **real**. Data conversion rules are the same as for SQL Server. For more information, see [Data Type Conversion (Database Engine)](http://msdn.microsoft.com/en-us/library/ms191530(v=sql11).aspx) on MSDN.  
  
|Literal Type|Format|  
|----------------|----------|  
|String literal in **integer** format|'nnnnnnnnnnnn'<br /><br />Example: '321312313123'|  
|String literal in **decimal** format|'nnnnnn.nnnnn'<br /><br />Example: '123344.34455'|  
|String literal in **floating point** format|'n.nnnnnE+nn'<br /><br />Example: '3.12323E+14'|  
|Integer literal|nnnnnnnnnnnn<br /><br />Example: 321312313123|  
|Decimal literal|nnnnnn.nnnnn<br /><br />Example: 123344.34455|  
|Floating point literal|n.nnnnnE+nn<br /><br />Example: 3.12323E+14|  
  
### int, bigint, tinyint, smallint Data Types  
The following table defines the accepted formats and rules for inserting literal values into a distribution column of type **int**, **bigint**, **tinyint**, or **smallint**. The data source cannot exceed the range allowed for the given data type. For example, the range for **tinyint** is 0 to 255 and the range for **int** is -2,147,483,648 to 2,147,483,647.  
  
||||  
|-|-|-|  
|Literal Type|Format|Conversion Rules|  
|String literal in **integer** format|'nnnnnnnnnnnnnn'<br /><br />Example: '321312313123'||  
|Integer literal|nnnnnnnnnnnnnn<br /><br />Example: 321312313123||  
|Decimal literal|nnnnnn.nnnnn<br /><br />Example: 123344.34455|The values to the right of the decimal point are truncated.|  
  
### money and smallmoney Data Types  
Money literal values are represented as numbers with an optional decimal point and currency symbol as a prefix. The data source cannot exceed the range allowed for the given data type. For example, the range for **smallmoney** is -214,748.3648 to 214,748.3647 and the range for **money** is -922,337,203,685,477.5808 to 922,337,203,685,477.5807. The following table defines the accepted formats and rules for inserting literal values into a distribution column of type **money** or **smallmoney**.  
  
|Literal Type|Format|Conversion Rules|  
|----------------|----------|--------------------|  
|String literal in **integer** format|'nnnnnnnn'<br /><br />Example: '123433'|Missing digits after the decimal point are set to 0 when the value is inserted. For example, the literal '12345' is inserted as 12345.0000.|  
|String literal in **decimal** format|'nnnnnn.nnnnn'<br /><br />Example: '123344.34455'|If the number of digits after the decimal point exceed 4, the value is rounded up to the nearest value. For example, the value '123344.34455' is inserted as 123344.3446.|  
|String literal in **money** format|'$nnnnnn.nnnn'<br /><br />Example: '$123456.7890'|The optional currency symbol is not inserted with the value.<br /><br />If the number of digits after the decimal point exceed 4, the value is rounded up to the nearest value.|  
|Integer literal|nnnnnnnn<br /><br />Example: 123433|Missing digits after the decimal point are set to 0 when the value is inserted. For example, the literal 12345 is inserted as 12345.0000.|  
|Decimal literal|nnnnnn.nnnnn<br /><br />Example: 123344.34455|If the number of digits after the decimal point exceed 4, the value is rounded up to the nearest value. For example, the value 123344.34455 is inserted as 123344.3446.|  
|Money literal|$nnnnnn.nnnn<br /><br />Example: $123456.7890|The optional currency symbol is not inserted with the value.<br /><br />If the number of digits after the decimal point exceed 4, the value is rounded up to the nearest value.|  
  
## <a name="InsertLiteralsString"></a>Inserting Literals into String Types  
The following tables define the accepted formats and conversion rules for inserting a literal value into a SQL Server PDW column that uses a string type.  
  
### char, varchar, nchar and nvarchar Data Types  
The following table defines the accepted formats and rules for inserting literal values into a distribution column of type **char**, **varchar**, **nchar** and **nvarchar**. The data source length cannot exceed the size specified for the data type. If the data source length is less than size of the **char** or **nchar** data type, the data is padded to the right with blank spaces to reach the data type size.  
  
|Literal Type|Format|Conversion Rules|  
|----------------|----------|--------------------|  
|String literal|Format: 'character string'<br /><br />Example: 'abc'||  
|Unicode string literal|Format: N'character string'<br /><br />Example: N'abc'||  
|Integer literal|Format: nnnnnnnnnnn<br /><br />Example: 321312313123||  
|Decimal literal|Format: nnnnnn.nnnnnnn<br /><br />Example: 12344.34455||  
|Money literal|Format: $nnnnnn.nnnnn<br /><br />Example: $123456.99|The currency symbol is not inserted with the value. To insert the currency symbol, insert the value as a string literal. This will match the format of the **dwloader** tool, which treats every literal as a string literal.<br /><br />Commas are not allowed.<br /><br />If the number of digits after the decimal point exceed 2, the value is rounded up to the nearest value. For example, the value 123.946789 is inserted as 123.95.<br /><br />Only the default style 0 (no commas and 2 digits after the decimal point) is allowed when using the CONVERT function to insert money literals.|  
||||  
  
## See Also  
[Grant Permissions to Load Data &#40;SQL Server PDW&#41;](../sqlpdw/grant-permissions-to-load-data-sql-server-pdw.md)  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[Distributed and Replicated Tables &#40;SQL Server PDW&#41;](../sqlpdw/distributed-and-replicated-tables-sql-server-pdw.md)  
[INSERT &#40;SQL Server PDW&#41;](../sqlpdw/insert-sql-server-pdw.md)  
  
