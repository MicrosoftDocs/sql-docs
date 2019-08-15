---
title: "SQL Server Data Types and ADO.NET"
ms.date: "08/15/2019"
ms.assetid: 81b43550-23e8-43bb-b460-7eb8ac825c33
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.topic: conceptual
author: MightyPen
ms.author: genemi
---
# SQL Server Data Types and ADO.NET

![Download-DownArrow-Circled](../../ssdt/media/download.png)[Download ADO.NET](../sql-connection-libraries.md#anchor-20-drivers-relational-access)

SQL Server and the .NET Framework are based on different type systems, which can result in potential data loss. To preserve data integrity, the .NET Framework Data Provider for SQL Server (<xref:System.Data.SqlClient>) provides typed accessor methods for working with SQL Server data. You can use the enumerations in the <xref:System.Data.SqlDbType> classes to specify <xref:System.Data.SqlClient.SqlParameter> data types.  
  
  
 SQL Server 2008 introduces new data types that are designed to meet business needs to work with date and time, structured, semi-structured, and unstructured data. These are documented in SQL Server 2008 Books Online.  
  
 The SQL Server data types that are available for use in your application depends on the version of SQL Server that you are using. For more information, see the relevant version of SQL Server Books Online in the following table.  
  
 **SQL Server Books Online**  
  
1. [Data Types (Database Engine)](https://go.microsoft.com/fwlink/?LinkID=107468)  
  
## In This Section  
 [SqlTypes and the DataSet](../../connect/ado-net/sqltypes-and-the-dataset.md)  
 Describes type support for `SqlTypes` in the `DataSet`.  
  
 [Handling Null Values](../../connect/ado-net/handling-null-values.md)  
 Demonstrates how to work with null values and three-valued logic.  
  
 [Comparing GUID and uniqueidentifier Values](../../connect/ado-net/comparing-guid-and-uniqueidentifier-values.md)  
 Demonstrates how to work with GUID and uniqueidentifier values in SQL Server and the .NET Framework.  
  
 [Date and Time Data](../../connect/ado-net/date-and-time-data.md)  
 Describes how to use the new date and time data types introduced in SQL Server 2008.  
  
 [Large UDTs](../../connect/ado-net/large-udts.md)  
 Demonstrates how to retrieve data from large value UDTs introduced in SQL Server 2008.  
  
 [XML Data in SQL Server](../../connect/ado-net/xml-data-in-sql-server.md)  
 Describes how to work with XML data retrieved from SQL Server.  
  
## Reference  
 <xref:System.Data.DataSet>  
 Describes the `DataSet` class and all of its members.  
  
 <xref:System.Data.SqlTypes>  
 Describes the `SqlTypes` namespace and all of its members.  
  
 <xref:System.Data.SqlDbType>  
 Describes the `SqlDbType` enumeration and all of its members.  
  
 <xref:System.Data.DbType>  
 Describes the `DbType` enumeration and all of its members.  
  
## See also

- [Table-Valued Parameters](../../connect/ado-net/table-valued-parameters.md)
- [SQL Server Binary and Large-Value Data](../../connect/ado-net/sql-server-binary-and-large-value-data.md)
- [ADO.NET Managed Providers and DataSet Developer Center](https://go.microsoft.com/fwlink/?LinkId=217917)
