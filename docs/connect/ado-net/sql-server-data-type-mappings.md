---
title: "SQL Server data type mappings"
description: Learn about mapping between the different type systems for SQL Server and the .NET Framework. This article summarizes how the systems interact in the Microsoft SqlClient Data Provider for SQL Server.
author: David-Engel
ms.author: v-davidengel
ms.reviewer: v-chmalh
ms.date: "11/13/2020"
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
---
# SQL Server data type mappings

[!INCLUDE[appliesto-netfx-netcore-netst-md](../../includes/appliesto-netfx-netcore-netst-md.md)]

[!INCLUDE[Driver_ADONET_Download](../../includes/driver_adonet_download.md)]

SQL Server and the .NET Framework are based on different type systems. For example, the .NET Framework <xref:System.Decimal> structure has a maximum scale of 28, whereas the SQL Server decimal and numeric data types have a maximum scale of 38. To maintain data integrity when reading and writing data, the  <xref:Microsoft.Data.SqlClient.SqlDataReader> exposes SQL Server–specific typed accessor methods that return objects of <xref:System.Data.SqlTypes> as well as accessor methods that return .NET Framework types. Both SQL Server types and .NET Framework types are also represented by enumerations in the <xref:System.Data.DbType> and <xref:System.Data.SqlDbType> classes, which you can use when specifying <xref:Microsoft.Data.SqlClient.SqlParameter> data types.

The following table shows the inferred .NET Framework type, the <xref:System.Data.DbType> and <xref:System.Data.SqlDbType> enumerations, and the accessor methods for the <xref:Microsoft.Data.SqlClient.SqlDataReader>.

|SQL Server Database Engine type|.NET Framework type|SqlDbType enumeration|SqlDataReader SqlTypes typed accessor|DbType enumeration|SqlDataReader DbType typed accessor|  
|-------------------------------------|-------------------------|---------------------------|-------------------------------------------|------------------------|-----------------------------------------|  
|bigint|Int64|<xref:System.Data.SqlDbType.BigInt>|<xref:Microsoft.Data.SqlClient.SqlDataReader.GetSqlInt64%2A>|<xref:System.Data.DbType.Int64>|<xref:Microsoft.Data.SqlClient.SqlDataReader.GetInt64%2A>|  
|binary|Byte[]|<xref:System.Data.SqlDbType.VarBinary>|<xref:Microsoft.Data.SqlClient.SqlDataReader.GetSqlBinary%2A>|<xref:System.Data.DbType.Binary>|<xref:Microsoft.Data.SqlClient.SqlDataReader.GetBytes%2A>|  
|bit|Boolean|<xref:System.Data.SqlDbType.Bit>|<xref:Microsoft.Data.SqlClient.SqlDataReader.GetSqlBoolean%2A>|<xref:System.Data.DbType.Boolean>|<xref:Microsoft.Data.SqlClient.SqlDataReader.GetBoolean%2A>|  
|char|String<br /><br /> Char[]|<xref:System.Data.SqlDbType.Char>|<xref:Microsoft.Data.SqlClient.SqlDataReader.GetSqlString%2A>|<xref:System.Data.DbType.AnsiStringFixedLength>,<br /><br /> <xref:System.Data.DbType.String>|<xref:Microsoft.Data.SqlClient.SqlDataReader.GetString%2A><br /><br /> <xref:Microsoft.Data.SqlClient.SqlDataReader.GetChars%2A>|  
|date <sup>1</sup><br /><br /> (SQL Server 2008 and later)|DateTime|<xref:System.Data.SqlDbType.Date> <sup>1</sup>|<xref:Microsoft.Data.SqlClient.SqlDataReader.GetSqlDateTime%2A>|<xref:System.Data.DbType.Date> <sup>1</sup>|<xref:Microsoft.Data.SqlClient.SqlDataReader.GetDateTime%2A>|  
|datetime|DateTime|<xref:System.Data.SqlDbType.DateTime>|<xref:Microsoft.Data.SqlClient.SqlDataReader.GetSqlDateTime%2A>|<xref:System.Data.DbType.DateTime>|<xref:Microsoft.Data.SqlClient.SqlDataReader.GetDateTime%2A>|  
|datetime2<br /><br /> (SQL Server 2008 and later)|DateTime|<xref:System.Data.SqlDbType.DateTime2>|None|<xref:System.Data.DbType.DateTime2>|<xref:Microsoft.Data.SqlClient.SqlDataReader.GetDateTime%2A>|  
|datetimeoffset<br /><br /> (SQL Server 2008 and later)|DateTimeOffset|<xref:System.Data.SqlDbType.DateTimeOffset>|none|<xref:System.Data.DbType.DateTimeOffset>|<xref:Microsoft.Data.SqlClient.SqlDataReader.GetDateTimeOffset%2A>|  
|decimal|Decimal|<xref:System.Data.SqlDbType.Decimal>|<xref:Microsoft.Data.SqlClient.SqlDataReader.GetSqlDecimal%2A>|<xref:System.Data.DbType.Decimal>|<xref:Microsoft.Data.SqlClient.SqlDataReader.GetDecimal%2A>|  
|FILESTREAM attribute (varbinary(max))|Byte[]|<xref:System.Data.SqlDbType.VarBinary>|<xref:Microsoft.Data.SqlClient.SqlDataReader.GetSqlBytes%2A>|<xref:System.Data.DbType.Binary>|<xref:Microsoft.Data.SqlClient.SqlDataReader.GetBytes%2A>|  
|float|Double|<xref:System.Data.SqlDbType.Float>|<xref:Microsoft.Data.SqlClient.SqlDataReader.GetSqlDouble%2A>|<xref:System.Data.DbType.Double>|<xref:Microsoft.Data.SqlClient.SqlDataReader.GetDouble%2A>|  
|image|Byte[]|<xref:System.Data.SqlDbType.Binary>|<xref:Microsoft.Data.SqlClient.SqlDataReader.GetSqlBinary%2A>|<xref:System.Data.DbType.Binary>|<xref:Microsoft.Data.SqlClient.SqlDataReader.GetBytes%2A>|  
|int|Int32|<xref:System.Data.SqlDbType.Int>|<xref:Microsoft.Data.SqlClient.SqlDataReader.GetSqlInt32%2A>|<xref:System.Data.DbType.Int32>|<xref:Microsoft.Data.SqlClient.SqlDataReader.GetInt32%2A>|  
|money|Decimal|<xref:System.Data.SqlDbType.Money>|<xref:Microsoft.Data.SqlClient.SqlDataReader.GetSqlMoney%2A>|<xref:System.Data.DbType.Decimal>|<xref:Microsoft.Data.SqlClient.SqlDataReader.GetDecimal%2A>|  
|nchar|String<br /><br /> Char[]|<xref:System.Data.SqlDbType.NChar>|<xref:Microsoft.Data.SqlClient.SqlDataReader.GetSqlString%2A>|<xref:System.Data.DbType.StringFixedLength>|<xref:Microsoft.Data.SqlClient.SqlDataReader.GetString%2A><br /><br /> <xref:Microsoft.Data.SqlClient.SqlDataReader.GetChars%2A>|  
|ntext|String<br /><br /> Char[]|<xref:System.Data.SqlDbType.NText>|<xref:Microsoft.Data.SqlClient.SqlDataReader.GetSqlString%2A>|<xref:System.Data.DbType.String>|<xref:Microsoft.Data.SqlClient.SqlDataReader.GetString%2A><br /><br /> <xref:Microsoft.Data.SqlClient.SqlDataReader.GetChars%2A>|  
|numeric|Decimal|<xref:System.Data.SqlDbType.Decimal>|<xref:Microsoft.Data.SqlClient.SqlDataReader.GetSqlDecimal%2A>|<xref:System.Data.DbType.Decimal>|<xref:Microsoft.Data.SqlClient.SqlDataReader.GetDecimal%2A>|  
|nvarchar|String<br /><br /> Char[]|<xref:System.Data.SqlDbType.NVarChar>|<xref:Microsoft.Data.SqlClient.SqlDataReader.GetSqlString%2A>|<xref:System.Data.DbType.String>|<xref:Microsoft.Data.SqlClient.SqlDataReader.GetString%2A><br /><br /> <xref:Microsoft.Data.SqlClient.SqlDataReader.GetChars%2A>|  
|real|Single|<xref:System.Data.SqlDbType.Real>|<xref:Microsoft.Data.SqlClient.SqlDataReader.GetSqlSingle%2A>|<xref:System.Data.DbType.Single>|<xref:Microsoft.Data.SqlClient.SqlDataReader.GetFloat%2A>|  
|rowversion|Byte[]|<xref:System.Data.SqlDbType.Timestamp>|<xref:Microsoft.Data.SqlClient.SqlDataReader.GetSqlBinary%2A>|<xref:System.Data.DbType.Binary>|<xref:Microsoft.Data.SqlClient.SqlDataReader.GetBytes%2A>|  
|smalldatetime|DateTime|<xref:System.Data.SqlDbType.DateTime>|<xref:Microsoft.Data.SqlClient.SqlDataReader.GetSqlDateTime%2A>|<xref:System.Data.DbType.DateTime>|<xref:Microsoft.Data.SqlClient.SqlDataReader.GetDateTime%2A>|  
|smallint|Int16|<xref:System.Data.SqlDbType.SmallInt>|<xref:Microsoft.Data.SqlClient.SqlDataReader.GetSqlInt16%2A>|<xref:System.Data.DbType.Int16>|<xref:Microsoft.Data.SqlClient.SqlDataReader.GetInt16%2A>|  
|smallmoney|Decimal|<xref:System.Data.SqlDbType.SmallMoney>|<xref:Microsoft.Data.SqlClient.SqlDataReader.GetSqlMoney%2A>|<xref:System.Data.DbType.Decimal>|<xref:Microsoft.Data.SqlClient.SqlDataReader.GetDecimal%2A>|  
|sql_variant|Object <sup>2</sup>|<xref:System.Data.SqlDbType.Variant>|<xref:Microsoft.Data.SqlClient.SqlDataReader.GetSqlValue%2A> <sup>2</sup>|<xref:System.Data.DbType.Object>|<xref:Microsoft.Data.SqlClient.SqlDataReader.GetValue%2A> <sup>2</sup>|  
|text|String<br /><br /> Char[]|<xref:System.Data.SqlDbType.Text>|<xref:Microsoft.Data.SqlClient.SqlDataReader.GetSqlString%2A>|<xref:System.Data.DbType.String>|<xref:Microsoft.Data.SqlClient.SqlDataReader.GetString%2A><br /><br /> <xref:Microsoft.Data.SqlClient.SqlDataReader.GetChars%2A>|  
|time<br /><br /> (SQL Server 2008 and later)|TimeSpan|<xref:System.Data.SqlDbType.Time>|none|<xref:System.Data.DbType.Time>|<xref:Microsoft.Data.SqlClient.SqlDataReader.GetDateTime%2A>|  
|timestamp|Byte[]|<xref:System.Data.SqlDbType.Timestamp>|<xref:Microsoft.Data.SqlClient.SqlDataReader.GetSqlBinary%2A>|<xref:System.Data.DbType.Binary>|<xref:Microsoft.Data.SqlClient.SqlDataReader.GetBytes%2A>|  
|tinyint|Byte|<xref:System.Data.SqlDbType.TinyInt>|<xref:Microsoft.Data.SqlClient.SqlDataReader.GetSqlByte%2A>|<xref:System.Data.DbType.Byte>|<xref:Microsoft.Data.SqlClient.SqlDataReader.GetByte%2A>|  
|uniqueidentifier|Guid|<xref:System.Data.SqlDbType.UniqueIdentifier>|<xref:Microsoft.Data.SqlClient.SqlDataReader.GetSqlGuid%2A>|<xref:System.Data.DbType.Guid>|<xref:Microsoft.Data.SqlClient.SqlDataReader.GetGuid%2A>|  
|varbinary|Byte[]|<xref:System.Data.SqlDbType.VarBinary>|<xref:Microsoft.Data.SqlClient.SqlDataReader.GetSqlBinary%2A>|<xref:System.Data.DbType.Binary>|<xref:Microsoft.Data.SqlClient.SqlDataReader.GetBytes%2A>|  
|varchar|String<br /><br /> Char[]|<xref:System.Data.SqlDbType.VarChar>|<xref:Microsoft.Data.SqlClient.SqlDataReader.GetSqlString%2A>|<xref:System.Data.DbType.AnsiString>, <xref:System.Data.DbType.String>|<xref:Microsoft.Data.SqlClient.SqlDataReader.GetString%2A><br /><br /> <xref:Microsoft.Data.SqlClient.SqlDataReader.GetChars%2A>|  
|xml|Xml|<xref:System.Data.SqlDbType.Xml>|<xref:Microsoft.Data.SqlClient.SqlDataReader.GetSqlXml%2A>|<xref:System.Data.DbType.Xml>|none|

<sup>1</sup> You cannot set the `DbType` property of a `SqlParameter` to `SqlDbType.Date`.  
<sup>2</sup> Use a specific typed accessor if you know the underlying type of the `sql_variant`.

## SQL Server documentation

For more information about SQL Server data types, see [Data types (Transact-SQL)](../../t-sql/data-types/data-types-transact-sql.md).

## See also

- [SQL Server data types and ADO.NET](./sql/sql-server-data-types.md)
- [SQL Server binary and large-value data](./sql/sql-server-binary-large-value-data.md)
- [Configuring parameters](configure-parameters.md)
- [Data type mappings in ADO.NET](data-type-mappings-ado-net.md)