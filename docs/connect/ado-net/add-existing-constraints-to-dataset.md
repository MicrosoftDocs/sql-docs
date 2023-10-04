---
title: "Add existing constraints to a DataSet"
description: Describes how to add existing constraints to a DataSet.
author: David-Engel
ms.author: v-davidengel
ms.date: "11/30/2020"
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
dev_langs:
  - "csharp"
---
# Add existing constraints to a DataSet

[!INCLUDE[appliesto-netfx-netcore-netst-md](../../includes/appliesto-netfx-netcore-netst-md.md)]

[!INCLUDE[Driver_ADONET_Download](../../includes/driver_adonet_download.md)]

The <xref:System.Data.Common.DbDataAdapter.Fill%2A> method of the <xref:Microsoft.Data.SqlClient.SqlDataAdapter> fills a <xref:System.Data.DataSet> only with table columns and rows from a data source; though constraints are commonly set by the data source, the **Fill** method does not add this schema information to the **DataSet** by default.

To populate a **DataSet** with existing primary key constraint information from a data source, you can either call the <xref:System.Data.Common.DbDataAdapter.FillSchema%2A> method of the **DataAdapter**, or set the <xref:System.Data.Common.DataAdapter.MissingSchemaAction%2A> property of the **DataAdapter** to **AddWithKey** before calling **Fill**. This will ensure that primary key constraints in the **DataSet** reflect those at the data source.

> [!NOTE]
> Foreign key constraint information is not included and must be created explicitly.

Adding schema information to a **DataSet** before filling it with data ensures that primary key constraints are included with the <xref:System.Data.DataTable> objects in the **DataSet**. As a result, when additional calls to fill the **DataSet** are made, the primary key column information is used to match new rows from the data source with current rows in each **DataTable**, and current data in the tables is overwritten with data from the data source. Without the schema information, the new rows from the data source are appended to the **DataSet**, resulting in duplicate rows.

> [!NOTE]
> If a column in a data source is identified as auto-incrementing, the <xref:System.Data.Common.DbDataAdapter.FillSchema%2A> method, or the <xref:System.Data.Common.DbDataAdapter.Fill%2A> method with a <xref:System.Data.Common.DataAdapter.MissingSchemaAction%2A> of **AddWithKey**, creates a **DataColumn** with an **AutoIncrement** property set to `true`. However, you will need to set the **AutoIncrementStep** and **AutoIncrementSeed** values yourself.

> [!NOTE]
> Using **FillSchema** or setting the **MissingSchemaAction** to **AddWithKey** requires extra processing at the data source to determine primary key column information. This additional processing can hinder performance. If you know the primary key information at design time, we recommend that you explicitly specify the primary key column or columns in order to achieve optimal performance.

The following code example shows how to add schema information to a **DataSet** using <xref:System.Data.Common.DbDataAdapter.FillSchema%2A>:

[!code-csharp[SqlDataAdapter_FillDataSet#1](~/../sqlclient/doc/samples/SqlDataAdapter_FillDataSet.cs#1)]

The following code example shows how to add schema information to a **DataSet** using the <xref:System.Data.Common.DataAdapter.MissingSchemaAction%2A> property and the <xref:System.Data.Common.DbDataAdapter.Fill%2A> method:

[!code-csharp[SqlDataAdapter_FillDataSet#2](~/../sqlclient/doc/samples/SqlDataAdapter_FillDataSet.cs#2)]

## Handling multiple result sets

If the **DataAdapter** meets multiple result sets returned from the <xref:Microsoft.Data.SqlClient.SqlDataAdapter.SelectCommand%2A>, it will create multiple tables in the **DataSet**. The tables will be given a zero-based incremental default name of **Table** *N*, starting with **Table** instead of "Table0". The tables will be given a zero-based incremental name of **TableName** *N*, starting with **TableName** instead of "TableName0" if a table name is passed as an argument to the <xref:System.Data.Common.DbDataAdapter.FillSchema%2A> method.

## See also

- [DataAdapters and DataReaders](dataadapters-datareaders.md)
- [Retrieving and modifying data in ADO.NET](retrieving-modifying-data.md)
- [Microsoft ADO.NET for SQL Server](microsoft-ado-net-sql-server.md)
