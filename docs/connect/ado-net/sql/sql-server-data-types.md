---
title: "SQL Server data types and ADO.NET"
description: "Describes how to work with SQL Server data types and how they interact with .NET data types."
ms.date: "08/15/2019"
ms.assetid: 81b43550-23e8-43bb-b460-7eb8ac825c33
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.topic: conceptual
author: David-Engel
ms.author: v-daenge
ms.reviewer: v-kaywon
---
# SQL Server data types and ADO.NET

[!INCLUDE[Driver_ADONET_Download](../../../includes/driver_adonet_download.md)]

SQL Server and the .NET are based on different type systems, which can result in potential data loss. To preserve data integrity, the Microsoft SqlClient Data Provider for SQL Server (<xref:Microsoft.Data.SqlClient>) provides typed accessor methods for working with SQL Server data. You can use the enumerations in the <xref:System.Data.SqlDbType> classes to specify <xref:Microsoft.Data.SqlClient.SqlParameter> data types.  
  
SQL Server 2008 introduces new data types that are designed to meet business needs to work with date and time, structured, semi-structured, and unstructured data. These are documented in SQL Server 2008 Books Online.  
  
The SQL Server data types that are available for use in your application depends on the version of SQL Server that you are using. For more information, see [Data Types (Database Engine)](https://go.microsoft.com/fwlink/?LinkID=107468) from SQL Server Books Online.
  
## In this section  
[SqlTypes and the DataSet](sqltypes-dataset.md)  
Describes type support for `SqlTypes` in the `DataSet`.  
  
[Handling null values](handle-null-values.md)  
Demonstrates how to work with null values and three-valued logic.  
  
[Comparing GUID and uniqueidentifier values](compare-guid-uniqueidentifier-values.md)  
Demonstrates how to work with GUID and uniqueidentifier values in SQL Server and .NET.  
  
[Date and time data](date-time-data.md)  
Describes how to use the new date and time data types introduced in SQL Server 2008.  
  
[Large UDTs](large-udts.md)  
Demonstrates how to retrieve data from large value UDTs introduced in SQL Server 2008.  
  
[XML data in SQL Server](xml-data-sql-server.md)  
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
  
## Next steps
- [Table-valued parameters](table-valued-parameters.md)
- [SQL Server binary and large-value data](sql-server-binary-large-value-data.md)
