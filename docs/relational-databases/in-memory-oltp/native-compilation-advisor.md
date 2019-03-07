---
title: "Native Compilation Advisor | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: in-memory-oltp
ms.topic: conceptual
f1_keywords: 
  - "sql13.swb.nativecompilationwizard.f1"
  - "swb.nativecompilationwizard.f1"
ms.assetid: d3898a47-2985-4a08-bc70-fd8331a01b7b
author: MightyPen
ms.author: genemi
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Native Compilation Advisor
[!INCLUDE[appliesto-ss-asdb-xxxx-xxx-md](../../includes/appliesto-ss-asdb-xxxx-xxx-md.md)]

  Transaction Performance Analysis reports tells you which interpreted stored procedures in your database will benefit if ported to use native compilation. For details see [Determining if a Table or Stored Procedure Should Be Ported to In-Memory OLTP](../../relational-databases/in-memory-oltp/determining-if-a-table-or-stored-procedure-should-be-ported-to-in-memory-oltp.md).  
  
 After you identify a stored procedure that you would like to port to use native compilation, you can use the Native Compilation Advisor (NCA) to help you migrate the interpreted stored procedure to native compilation. For more information about natively compiled stored procedures, see [Natively Compiled Stored Procedures](../../relational-databases/in-memory-oltp/natively-compiled-stored-procedures.md).  
  
 In a given interpreted stored procedure, the NCA allows you to identify all the features that are not supported in native modules. The NCA provides documentation links to work-arounds or solutions.  
  
 For information about migration methodologies, see [In-Memory OLTP - Common Workload Patterns and Migration Considerations](https://msdn.microsoft.com/library/dn673538.aspx).  
  
## Walkthrough Using the Native Compilation Advisor  
 In **Object Explorer**, right click the stored procedure you want to convert, and select **Native Compilation Advisor**. This will display the welcome page for the **Stored Procedure Native Compilation Advisor**. Click **Next** to continue.  
  
### Stored Procedure Validation  
 This page will report if the stored procedure uses any constructs that are not compatible with native compilation. You can click **Next** to see details. If there are constructs that are not compatible with native compilation, you can click **Next** to see details.  
  
### Stored Procedure Validation Result  
 If there are constructs that are not compatible with native compilation, the **Stored Procedure Validation Result** page will display details. You can generate a report (click **Generate Report**), exit the **Native Compilation Advisor**, and update your code so that it is compatible with native compilation.  
  
## Code Sample  
 The following sample shows an interpreted stored procedure and the *equivalent* stored procedure for native compilation. The sample assumes a directory called c:\data.  
  
> [!NOTE]  
>  As usual, the **FILEGROUP** element, and the **USE** mydatabase statement, apply to Microsoft SQL Server, but do not apply to Azure SQL Database.  
  
```sql  
CREATE DATABASE Demo  
ON  
PRIMARY(NAME = [Demo_data],  
FILENAME = 'C:\DATA\Demo_data.mdf', size=500MB)  
, FILEGROUP [Demo_fg] CONTAINS MEMORY_OPTIMIZED_DATA(  
NAME = [Demo_dir],  
FILENAME = 'C:\DATA\Demo_dir')  
LOG ON (name = [Demo_log], Filename='C:\DATA\Demo_log.ldf', size=500MB)  
COLLATE Latin1_General_100_BIN2;  
go  
  
USE Demo;  
go  
  
CREATE TABLE [dbo].[SalesOrders]  
(  
     [order_id] [int] NOT NULL,  
     [order_date] [datetime] NOT NULL,  
     [order_status] [tinyint] NOT NULL  
     CONSTRAINT [PK_SalesOrders] PRIMARY KEY NONCLUSTERED HASH   
(  
     [order_id]  
) WITH ( BUCKET_COUNT = 2097152)  
) WITH ( MEMORY_OPTIMIZED = ON )  
go  
  
-- Interpreted.  
CREATE PROCEDURE [dbo].[InsertOrder] @id INT, @date DATETIME2, @status TINYINT  
AS   
BEGIN   
  INSERT dbo.SalesOrders VALUES (@id, @date, @status);  
END  
go  
  
-- Natively Compiled.  
CREATE PROCEDURE [dbo].[InsertOrderXTP]  
      @id INT, @date DATETIME2, @status TINYINT  
  WITH NATIVE_COMPILATION, SCHEMABINDING, EXECUTE AS OWNER  
AS   
BEGIN ATOMIC WITH   
     (TRANSACTION ISOLATION LEVEL = SNAPSHOT, LANGUAGE = N'us_english'  
     )  
  INSERT dbo.SalesOrders VALUES (@id, @date, @status);  
END  
go  
  
SELECT * from SalesOrders;  
go  
  
EXECUTE dbo.InsertOrder @id= 10, @date = '1956-01-01 12:00:00', @status = 1;  
EXECUTE dbo.InsertOrderXTP @id= 11, @date = '1956-01-01 12:01:00', @status = 2;  
  
SELECT * from SalesOrders;  
```  
  
## See Also  
 [Migrating to In-Memory OLTP](../../relational-databases/in-memory-oltp/migrating-to-in-memory-oltp.md)   
 [Requirements for Using Memory-Optimized Tables](../../relational-databases/in-memory-oltp/requirements-for-using-memory-optimized-tables.md)  
  
  
