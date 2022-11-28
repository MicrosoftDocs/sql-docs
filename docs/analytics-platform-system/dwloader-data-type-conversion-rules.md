---
title: Dwloader data type conversion rules
description: This topic describes the input data formats and implicit data type conversions that dwloader Command-Line Loader supports when it loads data into Parallel Data Warehouse (PDW).
author: charlesfeddersen
ms.author: charlesf
ms.reviewer: martinle
ms.date: 04/17/2018
ms.service: sql
ms.subservice: data-warehouse
ms.topic: conceptual
ms.custom: seo-dt-2019
---

# Data type conversion rules for dwloader - Parallel Data Warehouse
This topic describes the input data formats and implicit data type conversions that [dwloader Command-Line Loader](dwloader.md) supports when it loads data into PDW. The implicit data conversions occur when the input data does not match the data type in the SQL Server PDW target table. Use this information when designing your loading process to ensure your data will load successfully into SQL Server PDW.  
   
  
## <a name="InsertBinaryTypes"></a>Inserting Literals into Binary Types  
The following table defines the accepted literal types, format, and conversion rules for loading a literal value into a SQL Server PDW column of type **binary** (*n*) or **varbinary**(*n*).  
  
|Input Data Type|Input Data Examples|Conversion to binary or varbinary Data Type|  
|-------------------|-----------------------|-----------------------------------------------|  
|Binary literal|[0x]*hexidecimal_string*<br /><br />Example: 12Ef or 0x12Ef|The 0x prefix is optional.<br /><br />The data source length cannot exceed the number of bytes specified for the data type.<br /><br />If the data source length is less than size of the **binary** data type, the data is padded to the right with zeros to reach the data type size.|  
  
## <a name="InsertDateTimeTypes"></a>Inserting Literals into Date and Time Types  
Date and time literals are represented by using string literals in specific formats, enclosed in single quotation marks. The following tables define the allowed literal types, format, and conversion rules for loading a date or time literal into a column of type **datetime**, **smalldatetime**, **date**, **time**, **datetimeoffset**, or **datetime2**. The tables define the default format for the given data type. Other formats that can be specified are defined in the section [Datetime Formats](#DateFormats). Date and time literals cannot include leading or trailing spaces. **date**, **smalldatetime**, and null values cannot be loaded in fixed width mode.  
  
### datetime Data Type  
The following table defines the default format and rules for loading literal values into a column of type **datetime**. An empty string ('') is converted to the default value '1900-01-01 12:00:00.000'. Strings that contain only blanks ('    ') generate an error.  
  
|Input Data Type|Input Data Examples|Conversion to datetime Data Type|  
|-------------------|-----------------------|------------------------------------|  
|String literal in **datetime** format|'yyyy-MM-dd hh:mm:ss[.fff]'<br /><br />Example: '2007-05-08 12:35:29.123'|Missing fractional digits are set to 0 when the value is inserted. For example, the literal '2007-05-08 12:35' is inserted as '2007-05-08 12:35:00.000'.|  
|String literal in **smalldatetime** format|'yyyy-MM-dd hh:mm'<br /><br />Example: '2007-05-08 12:35'|Seconds and remaining fractional digits are set to 0 when the value is inserted.|  
|String literal in **date** format|'yyyy-MM-dd'<br /><br />Example: '2007-05-08'|Time values (hour, minutes, seconds, and fractions) are set to 12:00:00.000 when the value is inserted.|  
|String literal in **datetime2** format|'yyyy-MM-dd hh:mm:ss.fffffff'<br /><br />Example: '2007-05-08 12:35:29.1234567'|The source data cannot exceed three fractional digits. For example, the literal '2007-05-08 12:35:29.123' will be inserted, but the value '2007-05-8 12:35:29.1234567' generates an error.|  
  
### smalldatetime Data Type  
The following table defines the default format and rules for loading literal values into a column of type **smalldatetime**. An empty string ('') is converted to the default value '1900-01-01 12:00'. Strings that contain only blanks ('    ') generate an error.  
  
|Input Data Type|Input Data Examples|Conversion to smalldatetime Data Type|  
|-------------------|-----------------------|-----------------------------------------|  
|String literal in **smalldatetime** format|'yyyy-MM-dd hh:mm' or 'yyyy-MM-dd hh:mm:ss'<br /><br />Example: '2007-05-08 12:00' or '2007-05-08 12:00:15'|The source data must have values for year, month, date, hour and minute. Seconds are optional and, if present, must be set to the value 00. Any other value generates an error.<br /><br />Seconds are optional. When loading into a smalldatetime column, dwloader will round up seconds and fractional seconds. For example, 1999-01-05 20:10:35.123 will load as 01-05 20:11.|  
|String literal in **date** format|'yyyy-MM-dd'<br /><br />Example: '2007-05-08'|Time values (hour, minutes, seconds, and fractions) are set to 0 when the value is inserted.|  
  
### date Data Type  
The following table defines the default format and rules for loading literal values into a column of type **date**. An empty string ('') is converted to the default value '1900-01-01'. Strings that contain only blanks ('    ') generate an error.  
  
|Input Data Type|Input Data Examples|Conversion to date Data Type|  
|-------------------|-----------------------|--------------------------------|  
|String literal in **date** format|'yyyy-MM-dd'<br /><br />Example: '2007-05-08'||  
  
### time Data Type  
The following table defines the default format and rules for loading literal values into a column of type **time**. An empty string ('') is converted to the default value '00:00:00.0000'. Strings that contain only blanks ('    ') generate an error.  
  
|Input Data Type|Input Data Examples|Conversion to time Data Type|  
|-------------------|-----------------------|--------------------------------|  
|String literal in **time** format|'hh:mm:ss.fffffff'<br /><br />Example: '12:35:29.1234567'|If the data source has a smaller or equal precision (number of fractional digits) than the precision of the **time** data type, the data is padded to the right with zeros. For example, a literal value '12:35:29.123' is inserted as '12:35:29.1230000'.|  
  
### datetimeoffset Data Type  
The following table defines the default format and rules for loading literal values into a column of type **datetimeoffset** (*n*). The default format is 'yyyy-MM-dd hh:mm:ss.fffffff {+|-}hh:mm'. An empty string ('') is converted to the default value '1900-01-01 12:00:00.0000000 +00:00'. Strings that contain only blanks ('    ') generate an error. The number of fractional digits depends on the column definition. For example, a column defined as **datetimeoffset** (2) will have two fractional digits.  
  
|Input Data Type|Input Data Examples|Conversion to datetimeoffset Data Type|  
|-------------------|-----------------------|------------------------------------------|  
|String literal in **datetime** format|'yyyy-MM-dd hh:mm:ss[.fff]'<br /><br />Example: '2007-05-08 12:35:29.123'|Missing fractional digits and offset values are set to 0 when the value is inserted. For example, the literal '2007-05-08 12:35:29.123' is inserted as '2007-05-08 12:35:29.1230000 +00:00'.|  
|String literal in **smalldatetime** format|'yyyy-MM-dd hh:mm'<br /><br />Example: '2007-05-08 12:35'|Seconds, remaining fractional digits and offset values are set to 0 when the value is inserted.|  
|String literal in **date** format|'yyyy-MM-dd'<br /><br />Example: '2007-05-08'|Time values (hour, minutes, seconds, and fractions) are set to 0 when the value is inserted. For example, the literal '2007-05-08' is inserted as '2007-05-08 00:00:00.0000000 +00:00'.|  
|String literal in **datetime2** format|'yyyy-MM-dd hh:mm:ss.fffffff'<br /><br />Example: '2007-05-08 12:35:29.1234567'|The source data cannot exceed the specified number of fractional seconds in the datetimeoffset column. If the data source has a smaller or equal number of fractional seconds, the data is padded to the right with zeros. For example, if the data type is datetimeoffset (5), the literal value '2007-05-08 12:35:29.123 +12:15' is inserted as '12:35:29.12300 +12:15'.|  
|String literal in **datetimeoffset** format|'yyyy-MM-dd hh:mm:ss.fffffff {+&#124;-} hh:mm'<br /><br />Example: '2007-05-08 12:35:29.1234567 +12:15'|The source data cannot exceed the specified number of fractional seconds in the datetimeoffset column. If the data source has a smaller or equal number of fractional seconds, the data is padded to the right with zeros. For example, if the data type is datetimeoffset (5), the literal value '2007-05-08 12:35:29.123 +12:15' is inserted as '12:35:29.12300 +12:15'.|  
  
### datetime2 Data Type  
The following table defines the default format and rules for loading literal values into a column of type **datetime2** (*n*). The default format is 'yyyy-MM-dd hh:mm:ss.fffffff'. An empty string ('') is converted to the default value '1900-01-01 12:00:00'. Strings that contain only blanks ('    ') generate an error. The number of fractional digits depends on the column definition. For example, a column defined as **datetime2** (2) will have two fractional digits.  
  
|Input Data Type|Input Data Examples|Conversion to datetime2 Data Type|  
|-------------------|-----------------------|-------------------------------------|  
|String literal in **datetime** format|'yyyy-MM-dd hh:mm:ss[.fff]'<br /><br />Example: '2007-05-08 12:35:29.123'|Fractional seconds are optional and are set to 0 when the value is inserted.|  
|String literal in **smalldatetime** format|'yyyy-MM-dd hh:mm'<br /><br />Example: '2007-05-08 12'|Optional seconds and remaining fractional digits are set to 0 when the value is inserted.|  
|String literal in **date** format|'yyyy-MM-dd'<br /><br />Example: '2007-05-08'|Time values (hour, minutes, seconds, and fractions) are set to 0 when the value is inserted. For example, the literal '2007-05-08' is inserted as '2007-05-08 12:00:00.0000000'.|  
|String literal in **datetime2** format|'yyyy-MM-dd hh:mm:ss:fffffff'<br /><br />Example: '2007-05-08 12:35:29.1234567'|If the data source contains data and time components that are less than or equal to the value specified in **datetime2**(*n*), the data is inserted; otherwise an error is generated.|  
  
### <a name="DateFormats"></a>Datetime Formats  
Dwloader supports the following data formats for the input data that it is loading into SQL Server PDW. More details are listed after the table.  
  
|datetime|smalldatetime|date|datetime2|datetimeoffset|  
|------------|-----------------|--------|-------------|------------------|  
|[M[M]]M-[d]d-[yy]yy HH:mm:ss[.fff]|[M[M]]M-[d]d-[yy]yy HH:mm[:00]|[M[M]]M-[d]d-[yy]yy|[M[M]]M-[d]d-[yy]yy HH:mm:ss[.fffffff]|[M[M]]M-[d]d-[yy]yy HH:mm:ss[.fffffff] zzz|  
|[M[M]]M-[d]d-[yy]yy hh:mm:ss[.fff][tt]|[M[M]]M-[d]d-[yy]yy hh:mm[:00][tt]||[M[M]]M-[d]d-[yy]yy hh:mm:ss[.fffffff][tt]|[M[M]]M-[d]d-[yy]yy hh:mm:ss[.fffffff][tt] zzz|  
|[M[M]]M-[yy]yy-[d]d HH:mm:ss[.fff]|[M[M]]M-[yy]yy-[d]d HH:mm[:00]|[M[M]]M-[yy]yy-[d]d|[M[M]]M-[yy]yy-[d]d HH:mm:ss[.fffffff]|[M[M]]M-[yy]yy-[d]d HH:mm:ss[.fffffff] zzz|  
|[M[M]]M-[yy]yy-[d]d hh:mm:ss[.fff][tt]|[M[M]]M-[yy]yy-[d]d hh:mm[:00][tt]||[M[M]]M-[yy]yy-[d]d hh:mm:ss[.fffffff][tt]|[M[M]]M-[yy]yy-[d]d hh:mm:ss[.fffffff][tt] zzz|  
|[yy]yy-[M[M]]M-[d]d HH:mm:ss[.fff]|[yy]yy-[M[M]]M-[d]d HH:mm[:00]|[yy]yy-[M[M]]M-[d]d|[yy]yy-[M[M]]M-[d]d HH:mm:ss[.fffffff]|[yy]yy-[M[M]]M-[d]d HH:mm:ss[.fffffff]  zzz|  
|[yy]yy-[M[M]]M-[d]d hh:mm:ss[.fff][tt]|[yy]yy-[M[M]]M-[d]d hh:mm[:00][tt]||[yy]yy-[M[M]]M-[d]d hh:mm:ss[.fffffff][tt]|[yy]yy-[M[M]]M-[d]d hh:mm:ss[.fffffff][tt] zzz|  
|[yy]yy-[d]d-[M[M]]M HH:mm:ss[.fff]|[yy]yy-[d]d-[M[M]]M HH:mm[:00]|[yy]yy-[d]d-[M[M]]M|[yy]yy-[d]d-[M[M]]M HH:mm:ss[.fffffff]|[yy]yy-[d]d-[M[M]]M HH:mm:ss[.fffffff]  zzz|  
|[yy]yy-[d]d-[M[M]]M hh:mm:ss[.fff][tt]|[yy]yy-[d]d-[M[M]]M hh:mm[:00][tt]||[yy]yy-[d]d-[M[M]]M hh:mm:ss[.fffffff][tt]|[yy]yy-[d]d-[M[M]]M hh:mm:ss[.fffffff][tt] zzz|  
|[d]d-[M[M]]M-[yy]yy HH:mm:ss[.fff]|[d]d-[M[M]]M-[yy]yy HH:mm[:00]|[d]d-[M[M]]M-[yy]yy|[d]d-[M[M]]M-[yy]yy HH:mm:ss[.fffffff]|[d]d-[M[M]]M-[yy]yy HH:mm:ss[.fffffff] zzz|  
|[d]d-[M[M]]M-[yy]yy hh:mm:ss[.fff][tt]|[d]d-[M[M]]M-[yy]yy hh:mm[:00][tt]||[d]d-[M[M]]M-[yy]yy hh:mm:ss[.fffffff][tt]|[d]d-[M[M]]M-[yy]yy hh:mm:ss[.fffffff][tt] zzz|  
|[d]d-[yy]yy-[M[M]]M HH:mm:ss[.fff]|[d]d-[yy]yy-[M[M]]M HH:mm[:00]|[d]d-[yy]yy-[M[M]]M|[d]d-[yy]yy-[M[M]]M HH:mm:ss[.fffffff]|[d]d-[yy]yy-[M[M]]M HH:mm:ss[.fffffff]  zzz|  
|[d]d-[yy]yy-[M[M]]M hh:mm:ss[.fff][tt]|[d]d-[yy]yy-[M[M]]M hh:mm[:00][tt]||[d]d-[yy]yy-[M[M]]M hh:mm:ss[.fffffff][tt]|[d]d-[yy]yy-[M[M]]M hh:mm:ss[.fffffff][tt] zzz|  
  
Details:  
  
-   To separate month, day and year values, you can use ' - ', ' / ', or ' . '. For simplicity, the table uses only the ' - ' separator.  
  
-   To specify the month as text use three or more characters. Months with 1 or 2 characters will be interpreted as a number.  
  
-   To separate time values, use the ' : ' symbol.  
  
-   Letters enclosed in square brackets are optional.  
  
-   The letters 'tt' designate [AM|PM|am|pm]. AM is the default. When 'tt' is specified, the hour value (hh) must be in the range of 0 to 12.  
  
-   The letters 'zzz' designate the time zone offset for the system's current time zone in the format {+|-}HH:ss].  
  
## <a name="InsertNumerictypes"></a>Inserting Literals into Numeric Types  
The following tables define the default format and conversion rules for loading a literal value into a SQL Server PDW column that uses a numeric type.  
  
### bit Data Type  
The following table defines the default format and rules for loading literal values into a column of type **bit**. An empty string ('') or a string that contain only blanks ('    ') is converted to 0.  
  
|Input Data Type|Input Data Examples|Conversion to bit Data Type|  
|-------------------|-----------------------|-------------------------------|  
|String literal in **integer** format|'ffffffffff'<br /><br />Example: '1' or '321'|An integer value formatted as a string literal cannot contain a negative value. For example, the value '-123' generates an error.<br /><br />A value larger than 1 is converted to 1. For example, the value '123' is converted to 1.|  
|String literal|'TRUE' or 'FALSE'<br /><br />Example: 'true'|The value 'TRUE' is converted to 1; the value 'FALSE' is converted to 0.|  
|Integer literal|fffffffn<br /><br />Example: 1 or 321|A value larger than 1 or less than 0 is converted to 1. For example, the values 123 and -123 are converted to 1.|  
|Decimal literal|fffnn.fffn<br /><br />Example: 1234.5678|A value larger than 1 or less than 0 is converted to 1. For example, the values 123.45 and -123.45 are converted to 1.|  
  
### decimal Data Type  
The following table defines the rules for loading literal values into a column of type **decimal** (*p,s*). Data conversion rules are the same as for SQL Server. For more information, see [Data Type Conversion (Database Engine)](/previous-versions/sql/sql-server-2008-r2/ms191530(v=sql.105)) on MSDN.  
  
|Input Data Type|Input Data Examples|  
|-------------------|-----------------------|  
|Integer literal|321312313123|  
|Decimal literal|123344.34455|  
  
### float and real Data Types  
The following table defines rules for loading literal values into a column of type **float** or **real**. Data conversion rules are the same as for SQL Server. For more information, see [Data Type Conversion (Database Engine)](../t-sql/data-types/data-type-conversion-database-engine.md) on MSDN.  
  
|Input Data Type|Input Data Examples|  
|-------------------|-----------------------|  
|Integer literal|321312313123|  
|Decimal literal|123344.34455|  
|Floating point literal|3.12323E+14|  
  
### int, bigint, tinyint, smallint Data Types  
The following table defines the rules for loading literal values into a column of type **int**, **bigint**, **tinyint**, or **smallint**. The data source cannot exceed the range allowed for the given data type. For example, the range for **tinyint** is 0 to 255 and the range for **int** is -2,147,483,648 to 2,147,483,647.  
  
|Input Data Type|Input Data Examples|Conversion to Integer Data Types|  
|-------------------|-----------------------|------------------------------------|  
|Integer literal|321312313123||  
|Decimal literal|123344.34455|The values to the right of the decimal point are truncated.|  
  
### money and smallmoney Data Types  
Money literal values are represented as a string of numbers with an optional decimal point and an optional currency symbol as a prefix. The data source cannot exceed the range allowed for the given data type. For example, the range for **smallmoney** is -214,748.3648 to 214,748.3647 and the range for **money** is -922,337,203,685,477.5808 to 922,337,203,685,477.5807. The following table defines the rules for loading literal values into a column of type **money** or **smallmoney**.  
  
|Input Data Type|Input Data Examples|Conversion To money or smallmoney Data Type|  
|-------------------|-----------------------|-----------------------------------------------|  
|Integer literal|321312|Missing digits after the decimal point are set to 0 when the value is inserted. For example, the literal 12345 is inserted as 12345.0000|  
|Decimal literal|123344.34455|If the number of digits after the decimal point exceed 4, the value is rounded up to the nearest value. For example, the value 123344.34455 is inserted as 123344.3446.|  
|Money literal|$123456.7890|The currency symbol is not inserted with the value.<br /><br />If the number of digits after the decimal point exceed 4, the value is rounded up to the nearest value.|  
  
## <a name="InsertStringTypes"></a>Inserting Literals into String Types  
The following tables define the default format and conversion rules for loading a literal value into a SQL Server PDW column that uses a string type.  
  
### char, varchar, nchar and nvarchar Data Types  
The following table defines the default format and rules for loading literal values into a column of type **char**, **varchar**, **nchar** and **nvarchar**. The data source length cannot exceed the size specified for the data type. If the data source length is less than size of the **char** or **nchar** data type, the data is padded to the right with blank spaces to reach the data type size.  
  
|Input Data Type|Input Data Examples|Conversion to Character Data Types|  
|---------------|-------------------|----------------------------------|  
|String literal|Format: 'character string'<br /><br />Example: 'abc'| NA |  
|Unicode string literal|Format: N'character string'<br /><br />Example: N'abc'| NA |  
|Integer literal|Format: ffffffffffn<br /><br />Example: 321312313123| NA |  
|Decimal literal|Format: ffffff.fffffff<br /><br />Example: 12344.34455| NA |  
|Money literal|Format: $ffffff.fffnn<br /><br />Example: $123456.99|The optional currency symbol is not inserted with the value. To insert the currency symbol, insert the value as a string literal. This will match the format of the loader, which treats every literal as a string literal.<br /><br />Commas are not allowed.<br /><br />If the number of digits after the decimal point exceed 2, the value is rounded up to the nearest value. For example, the value 123.946789 is inserted as 123.95.<br /><br />Only the default style 0 (no commas and 2 digits after the decimal point) is allowed when using the CONVERT function to insert money literals.|  
  
### General Remarks  
**dwloader** performs the same implicit conversions that SMP SQL Server performs, but does not support all of the implicit conversions that SMP SQL Server supports.  
 
<!-- MISSING LINKS 
## See Also  
[Grant Permissions to Load Data &#40;SQL Server PDW&#41;](../sqlpdw/grant-permissions-to-load-data-sql-server-pdw.md)  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  

-->
