---
title: "DataAdapter, DataTable, and DataColumn mappings"
description: Describes how to set up DataTableMappings and ColumnMappings for a DataAdapter.
author: David-Engel
ms.author: v-davidengel
ms.date: "11/30/2020"
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
dev_langs:
  - "csharp"
---
# DataAdapter, DataTable, and DataColumn mappings

[!INCLUDE[appliesto-netfx-netcore-netst-md](../../includes/appliesto-netfx-netcore-netst-md.md)]

[!INCLUDE[Driver_ADONET_Download](../../includes/driver_adonet_download.md)]

A <xref:Microsoft.Data.SqlClient.SqlDataAdapter> contains a collection of zero or more <xref:System.Data.Common.DataTableMapping> objects in its <xref:System.Data.Common.DataAdapter.TableMappings%2A> property. A **DataTableMapping** provides a main mapping between the data returned from a query against a data source, and a <xref:System.Data.DataTable>. The **DataTableMapping** name can be passed in place of the **DataTable** name to the <xref:System.Data.Common.DbDataAdapter.Fill%2A> method of the **DataAdapter**. The following example creates a **DataTableMapping** named **AuthorsMapping** for the **Authors** table.

```csharp
workAdapter.TableMappings.Add("AuthorsMapping", "Authors");
```

A **DataTableMapping** enables you to use column names in a **DataTable** that are different from those in the database. The **DataAdapter** uses the mapping to match the columns when the table is updated.

> [!NOTE]
> If you do not specify a **TableName** or a **DataTableMapping** name when calling the **Fill** or **Update** method of the **DataAdapter**, the **DataAdapter** looks for a **DataTableMapping** named "Table". The **TableName** of the **DataTable** is "Table" if that **DataTableMapping** does not exist. You can specify a default **DataTableMapping** by creating a **DataTableMapping** with the name of "Table".

The following code example creates a **DataTableMapping** (from the <xref:System.Data.Common> namespace) and makes it the default mapping for the specified **DataAdapter** by naming it "Table". The example then maps the columns from the first table in the query result (the **Customers** table of the **Northwind** database) to a set of more user-friendly names in the **Northwind Customers** table in the <xref:System.Data.DataSet>. For columns that are not mapped, the name of the column from the data source is used.

[!code-csharp[SqlDataAdapter_TableMappings#1](~/../sqlclient/doc/samples/SqlDataAdapter_TableMappings.cs#1)]

In more advanced situations, you may decide that you want the same **DataAdapter** to support loading different tables with different mappings. To do this, add additional **DataTableMapping** objects.

When the **Fill** method is passed an instance of a **DataSet** and a **DataTableMapping** name, if a mapping with that name exists it is used; otherwise, a **DataTable** with that name is used.

The following examples create a **DataTableMapping** with a name of **Customers** and a **DataTable** name of **BizTalkSchema**. The example then maps the rows returned by the SELECT statement to the **BizTalkSchema** **DataTable**.

[!code-csharp[SqlDataAdapter_TableMappings#2](~/../sqlclient/doc/samples/SqlDataAdapter_TableMappings.cs#2)]

> [!NOTE]
> If a source column name is not supplied for a column mapping, default names will be automatically generated. The column mapping is given an incremental default name of **SourceColumn** *N,* starting with **SourceColumn1** if no source column is supplied for a column mapping.

> [!NOTE]
> If a source table name is not supplied for a table mapping, default names will be automatically generated. The table mapping is given an incremental default name of **SourceTable** *N*, starting with **SourceTable1** if no source table name is supplied for a table mapping.

> [!NOTE]
> We recommend that you avoid the naming convention of **SourceColumn** *N* for a column mapping, or **SourceTable** *N* for a table mapping, because the name you supply may conflict with an existing default column mapping name in the **ColumnMappingCollection** or table mapping name in the **DataTableMappingCollection**. If the supplied name already exists, an exception will be thrown.

## Handle multiple result sets

If your **SelectCommand** returns multiple tables, **Fill** automatically generates table names with incremental values for the tables in the **DataSet**, starting with the specified table name and continuing on in the form **TableName** *N*, starting with **TableName1**. You can use table mappings to map the automatically generated table name to a name you want specified for the table in the **DataSet**. For example, for a **SelectCommand** that returns two tables, **Customers** and **Orders**, issue the following call to **Fill**.

```csharp
adapter.Fill(customersDataSet, "Customers");
```

Two tables are created in the **DataSet**: **Customers** and **Customers1**. You can use table mappings to ensure that the second table is named **Orders** instead of **Customers1**. To do this, map the source table of **Customers1** to the **DataSet** table **Orders**, as shown in the following example.

[!code-csharp[SqlDataAdapter_TableMappings#3](~/../sqlclient/doc/samples/SqlDataAdapter_TableMappings.cs#3)]

## See also

- [DataAdapters and DataReaders](dataadapters-datareaders.md)
- [Retrieving and modifying data in ADO.NET](retrieving-modifying-data.md)
- [Microsoft ADO.NET for SQL Server](microsoft-ado-net-sql-server.md)
