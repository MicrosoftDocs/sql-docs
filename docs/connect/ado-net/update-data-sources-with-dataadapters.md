---
title: "Update data sources with DataAdapters"
description: Learn how the Update method of DataAdapter resolves changes from a DataSet back to the data source in ADO.NET applications.
author: David-Engel
ms.author: v-davidengel
ms.reviewer: v-chmalh
ms.date: "11/30/2020"
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
dev_langs:
  - "csharp"
---
# Update data sources with DataAdapters

[!INCLUDE[appliesto-netfx-netcore-netst-md](../../includes/appliesto-netfx-netcore-netst-md.md)]

[!INCLUDE[Driver_ADONET_Download](../../includes/driver_adonet_download.md)]

The `Update` method of the <xref:System.Data.Common.DataAdapter> is called to resolve changes from a <xref:System.Data.DataSet> back to the data source. The `Update` method, like the `Fill` method, takes as arguments an instance of a `DataSet`, and an optional <xref:System.Data.DataTable> object or `DataTable` name. The `DataSet` instance is the `DataSet` that contains the changes that have been made, and the `DataTable` identifies the table from which to retrieve the changes. If no `DataTable` is specified, the first `DataTable` in the `DataSet` is used.

When you call the `Update` method, the `DataAdapter` analyzes the changes that have been made and executes the appropriate command (INSERT, UPDATE, or DELETE). When the `DataAdapter` encounters a change to a <xref:System.Data.DataRow>, it uses the <xref:Microsoft.Data.SqlClient.SqlDataAdapter.InsertCommand%2A>, <xref:Microsoft.Data.SqlClient.SqlDataAdapter.UpdateCommand%2A>, or <xref:Microsoft.Data.SqlClient.SqlDataAdapter.DeleteCommand%2A> to process the change.

These properties allow you to maximize the performance of your ADO.NET application by specifying command syntax at design time and, where possible, through the use of stored procedures. You must explicitly set the commands before calling `Update`. If `Update` is called and the appropriate command does not exist for a particular update (for example, no `DeleteCommand` for deleted rows), an exception is thrown.

> [!IMPORTANT]
> If you are using SQL Server stored procedures to edit or delete data using a `DataAdapter`, make sure that you do not use `SET NOCOUNT ON` in the stored procedure definition. This causes the rows affected count returned to be zero, which the `DataAdapter` interprets as a concurrency conflict. In this event, a <xref:System.Data.DBConcurrencyException> will be thrown.

Command parameters can be used to specify input and output values for an SQL statement or stored procedure for each modified row in a `DataSet`. For more information, see [DataAdapter parameters](dataadapter-parameters.md).

> [!NOTE]
> It is important to understand the difference between deleting a row in a <xref:System.Data.DataTable> and removing the row. When you call the `Remove` or `RemoveAt` method, the row is removed immediately. Any corresponding rows in the back end data source **will not be affected** if you then pass the `DataTable` or `DataSet` to a `DataAdapter` and call `Update`. When you use the `Delete` method, the row remains in the `DataTable` and is marked for deletion. If you then pass the `DataTable` or `DataSet` to a `DataAdapter` and call `Update`, the corresponding row in the back end data source **is deleted**.

If your `DataTable` maps to or is generated from a single database table, you can take advantage of the <xref:System.Data.Common.DbCommandBuilder> object to automatically generate the `DeleteCommand`, `InsertCommand`, and `UpdateCommand` objects for the `DataAdapter`. For more information, see [Generating commands with CommandBuilders](generate-commands-with-commandbuilders.md).

## Use UpdatedRowSource to map values to a DataSet

You can control how the values returned from the data source are mapped back to the `DataTable` following a call to the <xref:System.Data.Common.DbDataAdapter.Update%2A> method of a `DataAdapter`, by using the <xref:Microsoft.Data.SqlClient.SqlCommand.UpdatedRowSource%2A> property of a <xref:Microsoft.Data.SqlClient.SqlCommand> object. By setting the `UpdatedRowSource` property to one of the <xref:System.Data.UpdateRowSource> enumeration values, you can control whether output parameters returned by the `DataAdapter` commands are ignored or applied to the changed row in the `DataSet`. You can also specify whether the first returned row (if it exists) is applied to the changed row in the `DataTable`.

The following table describes the different values of the `UpdateRowSource` enumeration and how they affect the behavior of a command used with a `DataAdapter`.

|UpdatedRowSource Enumeration|Description|
|----------------------------------|-----------------|
|<xref:System.Data.UpdateRowSource.Both>|Both the output parameters and the first row of a returned result set may be mapped to the changed row in the `DataSet`.|
|<xref:System.Data.UpdateRowSource.FirstReturnedRecord>|Only the data in the first row of a returned result set may be mapped to the changed row in the `DataSet`.|
|<xref:System.Data.UpdateRowSource.None>|Any output parameters or rows of a returned result set are ignored.|
|<xref:System.Data.UpdateRowSource.OutputParameters>|Only output parameters may be mapped to the changed row in the `DataSet`.|

The `Update` method resolves your changes back to the data source; however other clients may have modified data at the data source since the last time you filled the `DataSet`. To refresh your `DataSet` with current data, use the `DataAdapter` and `Fill` method. New rows will be added to the table, and updated information will be incorporated into existing rows.

The `Fill` method determines whether a new row will be added or an existing row will be updated by examining the primary key values of the rows in the `DataSet` and the rows returned by the `SelectCommand`. If the `Fill` method encounters a primary key value for a row in the `DataSet` that matches a primary key value from a row in the results returned by the `SelectCommand`, it updates the existing row with the information from the row returned by the `SelectCommand` and sets the <xref:System.Data.DataRow.RowState%2A> of the existing row to `Unchanged`. If a row returned by the `SelectCommand` has a primary key value that does not match any of the primary key values of the rows in the `DataSet`, the `Fill` method adds a new row with a `RowState` of `Unchanged`.

> [!NOTE]
> If the `SelectCommand` returns the results of an **OUTER JOIN**, the `DataAdapter` will not set a `PrimaryKey` value for the resulting `DataTable`. You must define the `PrimaryKey` yourself to ensure that duplicate rows are resolved correctly.

To handle exceptions that may occur when calling the `Update` method, you can use the `RowUpdated` event to respond to row update errors as they occur (see [Handle DataAdapter events](handle-dataadapter-events.md)), or you can set <xref:System.Data.Common.DataAdapter.ContinueUpdateOnError%2A> to `true` before calling `Update`, and respond to the error information stored in the `RowError` property of a particular row when the update is complete.

> [!NOTE]
> Calling `AcceptChanges` on the `DataSet`, `DataTable`, or `DataRow` will cause all `Original` values for a `DataRow` to be overwritten with the `Current` values for the `DataRow`. If the field values that identify the row as unique have been modified, after calling `AcceptChanges` the `Original` values will no longer match the values in the data source. `AcceptChanges` is called automatically for each row during a call to the `Update` method of a `DataAdapter`. You can preserve the original values during a call to the Update method by first setting the `AcceptChangesDuringUpdate` property of the `DataAdapter` to false, or by creating an event handler for the `RowUpdated` event and setting the <xref:System.Data.Common.RowUpdatedEventArgs.Status%2A> to <xref:System.Data.UpdateStatus.SkipCurrentRow>. For more information, see [Handle DataAdapter Events](handle-dataadapter-events.md).

The following examples demonstrate how to perform updates to modified rows by explicitly setting the `UpdateCommand` of a `DataAdapter` and calling its `Update` method.

> [!NOTE]
> The parameter specified in the `WHERE clause` of the `UPDATE statement` is set to use the `Original` value of the `SourceColumn`. This is important, because the `Current` value may have been modified and may not match the value in the data source. The `Original` value is the value that was used to populate the `DataTable` from the data source.

[!code-csharp[SqlDataAdapter_Update#1](~/../sqlclient/doc/samples/SqlDataAdapter_Update.cs#1)]

## AutoIncrement columns

If the tables from your data source have auto-incrementing columns, you can fill the columns in your `DataSet` either by returning the auto-increment value as an output parameter of a stored procedure and mapping that to a column in a table, by returning the auto-increment value in the first row of a result set returned by a stored procedure or SQL statement, or by using the `RowUpdated` event of the `DataAdapter` to execute an additional SELECT statement. For more information and an example, see [Retrieve identity or autonumber values](retrieve-identity-or-autonumber-values.md).

## Ordering of inserts, updates, and deletes

In many circumstances, the order in which changes made through the `DataSet` are sent to the data source is important. For example, if a primary key value for an existing row is updated, and a new row has been added with the new primary key value as a foreign key, it is important to process the update before the insert.

You can use the `Select` method of the `DataTable` to return a `DataRow` array that only references rows with a particular `RowState`. You can then pass the returned `DataRow` array to the `Update` method of the `DataAdapter` to process the modified rows. By specifying a subset of rows to be updated, you can control the order in which inserts, updates, and deletes are processed.

## Example

For example, the following code ensures that the deleted rows of the table are processed first, then the updated rows, and then the inserted rows.

[!code-csharp[SqlDataAdapter_Update#2](~/../sqlclient/doc/samples/SqlDataAdapter_Update.cs#2)]

## Use a DataAdapter to retrieve and update data

You can use a DataAdapter to retrieve and update the data.

- The sample uses `DataAdapter.AcceptChangesDuringFill` to clone the data in the database. If the property is set as **false**, **AcceptChanges** is not called when filling the table, and the newly added rows are treated as inserted rows. So, the sample uses these rows to insert the new rows into the database.

- The samples uses `DataAdapter.TableMappings` to define the mapping between the source table and DataTable.

- The sample uses `DataAdapter.FillLoadOption` to determine how the adapter fills the **DataTable** from the **DbDataReader**. When you create a DataTable, you can only write the data from database to the current version or the original version by setting the property as the **LoadOption.Upsert** or the **LoadOption.PreserveChanges**.

- The sample will also update the table by using `DbDataAdapter.UpdateBatchSize` to perform batch operations.

Before you compile and run the sample, you need to create the sample database:

```sql
USE [master]
GO

CREATE DATABASE [MySchool]

GO

USE [MySchool]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Course]([CourseID] [nvarchar](10) NOT NULL,
[Year] [smallint] NOT NULL,
[Title] [nvarchar](100) NOT NULL,
[Credits] [int] NOT NULL,
[DepartmentID] [int] NOT NULL,
 CONSTRAINT [PK_Course] PRIMARY KEY CLUSTERED
(
[CourseID] ASC,
[Year] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY]

GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Department]([DepartmentID] [int] IDENTITY(1,1) NOT NULL,
[Name] [nvarchar](50) NOT NULL,
[Budget] [money] NOT NULL,
[StartDate] [datetime] NOT NULL,
[Administrator] [int] NULL,
 CONSTRAINT [PK_Department] PRIMARY KEY CLUSTERED
(
[DepartmentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY]

GO

INSERT [dbo].[Course] ([CourseID], [Year], [Title], [Credits], [DepartmentID]) VALUES (N'C1045', 2012, N'Calculus', 4, 7)
INSERT [dbo].[Course] ([CourseID], [Year], [Title], [Credits], [DepartmentID]) VALUES (N'C1061', 2012, N'Physics', 4, 1)
INSERT [dbo].[Course] ([CourseID], [Year], [Title], [Credits], [DepartmentID]) VALUES (N'C2021', 2012, N'Composition', 3, 2)
INSERT [dbo].[Course] ([CourseID], [Year], [Title], [Credits], [DepartmentID]) VALUES (N'C2042', 2012, N'Literature', 4, 2)

SET IDENTITY_INSERT [dbo].[Department] ON

INSERT [dbo].[Department] ([DepartmentID], [Name], [Budget], [StartDate], [Administrator]) VALUES (1, N'Engineering', 350000.0000, CAST(0x0000999C00000000 AS DateTime), 2)
INSERT [dbo].[Department] ([DepartmentID], [Name], [Budget], [StartDate], [Administrator]) VALUES (2, N'English', 120000.0000, CAST(0x0000999C00000000 AS DateTime), 6)
INSERT [dbo].[Department] ([DepartmentID], [Name], [Budget], [StartDate], [Administrator]) VALUES (4, N'Economics', 200000.0000, CAST(0x0000999C00000000 AS DateTime), 4)
INSERT [dbo].[Department] ([DepartmentID], [Name], [Budget], [StartDate], [Administrator]) VALUES (7, N'Mathematics', 250024.0000, CAST(0x0000999C00000000 AS DateTime), 3)
SET IDENTITY_INSERT [dbo].[Department] OFF

ALTER TABLE [dbo].[Course]  WITH CHECK ADD  CONSTRAINT [FK_Course_Department] FOREIGN KEY([DepartmentID])
REFERENCES [dbo].[Department] ([DepartmentID])
GO
ALTER TABLE [dbo].[Course] CHECK CONSTRAINT [FK_Course_Department]
GO
```

[!code-csharp[SqlDataAdapter_Properties#1](~/../sqlclient/doc/samples/SqlDataAdapter_Properties.cs#1)]

## See also

- [DataAdapters and DataReaders](dataadapters-datareaders.md)
- [Retrieve identity or autonumber values](retrieve-identity-or-autonumber-values.md)
- [Microsoft ADO.NET for SQL Server](microsoft-ado-net-sql-server.md)
