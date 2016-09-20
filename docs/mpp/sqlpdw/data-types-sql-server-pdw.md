---
title: "Data Types (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 00cb213f-de26-448c-8099-0c42f36dd575
caps.latest.revision: 32
author: BarbKess
---
# Data Types (SQL Server PDW)
SQL Server PDW data types are a subset of the SQL Server data types. Because SQL Server PDW data is stored and operated on in SQL Server databases, data type definitions and operations are the same as SQL Server, except that SQL Server PDW supports fewer data types than SQL Server.  
  
## Contents  
  
-   [Supported Data Types](#DataTypes)  
  
-   [Data Type Conversion](#Conversion)  
  
## <a name="DataTypes"></a>Data Types  
The following tables list the supported SQL Server PDW data types and provide a link to the SQL Server documentation for each type. The [CREATE TABLE &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/create-table-sql-server-pdw.md) statement gives the syntax and describes how to define each of the SQL Server PDW data types.  
  
|Data Type|SQL Server topic|Comments|  
|-------------|--------------------|------------|  
|datetimeoffset|[datetimeoffset](http://msdn.microsoft.com/en-us/library/bb630289(v=sql11).aspx)|For additional information, see [Using Date and Time Data](http://msdn.microsoft.com/en-us/library/ms180878(v=sql11).aspx).|  
|Datetime2|[datetime2](http://msdn.microsoft.com/en-us/library/bb677335(v=sql11).aspx)||  
|datetime|[datetime](http://msdn.microsoft.com/en-us/library/ms187819(v=sql11).aspx)||  
|smalldatetime|[smalldatetime](http://msdn.microsoft.com/en-us/library/ms182418(v=sql11).aspx)||  
|date|[date](http://msdn.microsoft.com/en-us/library/bb630352(v=sql11).aspx)||  
|time|[time](http://msdn.microsoft.com/en-us/library/bb677243(v=sql11).aspx)||  
|float and real|[float and real](http://msdn.microsoft.com/en-us/library/ms173773(v=sql11).aspx)|SQL Server PDW does not support the subnormal values:<br /><br />-1.175494351E-038 < *value* < 0<br /><br />0 < *value* < 1.175494351E-038|  
|decimal|[decimal](http://msdn.microsoft.com/en-us/library/ms187746(v=sql11).aspx)||  
|money and smallmoney|[money and smallmoney](http://msdn.microsoft.com/en-us/library/ms179882(v=sql11).aspx)|When displaying data stored as type **money** or **smallmoney**, SQL Server PDW converts the data to the **decimal** type with a precision and scale of (19, 4) for **money** and (10, 4) for **smallmoney**.|  
|int, bigint, smallint, tinyint|[int, bigint, smallint, and tinyint](http://msdn.microsoft.com/en-us/library/ms187745(v=sql11).aspx)||  
|bit|[bit](http://msdn.microsoft.com/en-us/library/ms177603(v=sql11).aspx)||  
|nchar and nvarchar|[nchar and nvarchar](http://msdn.microsoft.com/en-us/library/ms186939(v=sql11).aspx)|SQL Server PDW does not support nvarchar (*max*).|  
|char and varchar|[char and varchar](http://msdn.microsoft.com/en-us/library/ms186939(v=sql11).aspx)|SQL Server PDW does not support varchar (*max*).|  
|binary and varbinary|[binary and varbinary](http://msdn.microsoft.com/en-us/library/ms188362(v=sql11).aspx)|SQL Server PDW does not support varbinary (*max*).|  
  
**Additional Data Types:** In addition to the supported data types, the data types **nvarchar(max)**, **uniqueidentifier**, **numeric**, and **sysname** are supported in the SQL Server catalog views only, and may not be used with or for user data.  
  
## <a name="Conversion"></a>Data Type Conversion  
When using the [SELECT &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/select-sql-server-pdw.md) statement, SQL Server PDW supports the SQL Server data type conversion rules for both implicit and explicit data type conversions.  
  
An *explicit* data type conversions uses the CAST or CONVERT function to perform the conversion. For more information, see [CAST and CONVERT &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/cast-and-convert-sql-server-pdw.md). An *implicit* data type conversion occurs when SQL Server PDW converts one data type to another data type without the CAST or CONVERT function.  
  
When an operator combines two expressions of different data types, the data type with the lower precedence is implicitly converted to the data type with the higher precedence. This applies to supported implicit conversions; if the conversion is not a supported implicit conversion, an error is returned.  
  
For a list of the supported implicit data type conversions in SQL Server, see For more information about data type conversions, see [Data Type Conversion (Database Engine)](http://msdn.microsoft.com/en-us/library/ms191530(v=sql11).aspx) on MSDN.  
  
### Data Type Precedence Order for Implicit Conversions  
The following list shows the precedence order for implicit conversions, from highest to lowest. For example, datetime2 has higher precedence than datetime. If a value of datetime2 is compared to a value of datetime, the value of datetime will be converted to a datetime2 before SQL Server PDW performs the comparison.  
  
**datetimeoffset**  
  
**datetime2**  
  
**datetime**  
  
**smalldatetime**  
  
**date**  
  
**time**  
  
**float**  
  
**real**  
  
**decimal**  
  
**money**  
  
**smallmoney**  
  
**bigint**  
  
**int**  
  
**smallint**  
  
**tinyint**  
  
**bit**  
  
**nvarchar**  
  
**nchar**  
  
**varchar**  
  
**char**  
  
**varbinary**  
  
**binary**  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[Data Types (Transact-SQL)](http://msdn.microsoft.com/en-us/library/ms187752.aspx(v=sql11))  
[Data Types (Database Engine)](http://msdn.microsoft.com/en-us/library/ms191530(v=sql11).aspx)  
  
