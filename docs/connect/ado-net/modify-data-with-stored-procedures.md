---
title: "Modify data with stored procedures"
description: Describes how to use stored procedure input parameters and output parameters to insert a row in a database, returning a new identity value.
author: David-Engel
ms.author: v-davidengel
ms.reviewer: v-chmalh
ms.date: "12/04/2020"
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
dev_langs:
  - "csharp"
---
# Modify data with stored procedures

[!INCLUDE[appliesto-netfx-netcore-netst-md](../../includes/appliesto-netfx-netcore-netst-md.md)]

[!INCLUDE[Driver_ADONET_Download](../../includes/driver_adonet_download.md)]

Stored procedures can accept data as input parameters and can return data as output parameters, result sets, or return values. The sample below illustrates how Microsoft SqlClient Data Provider for SQL Server sends and receives input parameters, output parameters, and return values. The example inserts a new record into a table where the primary key column is an identity column.

> [!NOTE]
> If you are using stored procedures to edit or delete data using a <xref:Microsoft.Data.SqlClient.SqlDataAdapter>, make sure that you do not use **SET NOCOUNT ON** in the stored procedure definition. This causes the rows affected count returned to be zero, which the `DataAdapter` interprets as a concurrency conflict. In this event, a <xref:System.Data.DBConcurrencyException> will be thrown.

## Example

The sample uses the following stored procedure to insert a new category into the **Northwind** **Categories** table. The stored procedure takes the value in the **CategoryName** column as an input parameter and uses the **SCOPE_IDENTITY()** function to retrieve the new value of the identity field, **CategoryID**, and return it in an output parameter. The RETURN statement uses the **\@\@ROWCOUNT** function to return the number of rows inserted.

```sql
CREATE PROCEDURE dbo.InsertCategory  
  @CategoryName nvarchar(15),  
  @Identity int OUT  
AS  
INSERT INTO Categories (CategoryName) VALUES(@CategoryName)  
SET @Identity = SCOPE_IDENTITY()  
RETURN @@ROWCOUNT  
```  

The following code example uses the `InsertCategory` stored procedure shown above as the source for the <xref:Microsoft.Data.SqlClient.SqlDataAdapter.InsertCommand%2A> of the <xref:Microsoft.Data.SqlClient.SqlDataAdapter>. The `@Identity` output parameter will be reflected in the <xref:System.Data.DataSet> after the record has been inserted into the database when the `Update` method of the <xref:Microsoft.Data.SqlClient.SqlDataAdapter> is called. The code also retrieves the return value.

[!code-csharp[DataWorks SqlClient.SprocIdentityReturn#1](~/../sqlclient/doc/samples/SqlDataAdapter_SPIdentityReturn.cs#1)]

## See also

- [Retrieving and modifying data in ADO.NET](retrieving-modifying-data.md)
- [DataAdapters and DataReaders](dataadapters-datareaders.md)
- [Executing a command](execute-command.md)
- [Microsoft ADO.NET for SQL Server](microsoft-ado-net-sql-server.md)
