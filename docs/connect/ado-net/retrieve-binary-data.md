---
title: "Retrieve binary data"
description: Describes how to retrieve binary data or large data structures using `CommandBehavior`.`SequentialAccess` to modify the default behavior of a `DataReader`.
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
# Retrieve binary data

[!INCLUDE[appliesto-netfx-netcore-netst-md](../../includes/appliesto-netfx-netcore-netst-md.md)]

[!INCLUDE[Driver_ADONET_Download](../../includes/driver_adonet_download.md)]

By default, the **DataReader** loads incoming data as a row as soon as an entire row of data is available. Binary large objects (BLOBs) need different treatment, however, because they can contain gigabytes of data that cannot be contained in a single row. The **Command.ExecuteReader** method has an overload that will take a <xref:System.Data.CommandBehavior> argument to modify the default behavior of the **DataReader**. You can pass <xref:System.Data.CommandBehavior.SequentialAccess> to the **ExecuteReader** method to modify the default behavior of the **DataReader** so that instead of loading rows of data, it will load data sequentially as it is received. This is ideal for loading BLOBs or other large data structures.

> [!NOTE]
> When setting the **DataReader** to use **SequentialAccess**, it is important to note the sequence in which you access the fields returned. The default behavior of the **DataReader**, which loads an entire row as soon as it is available, allows you to access the fields returned in any order until the next row is read. When using **SequentialAccess** however, you must access the fields returned by the **DataReader** in order. For example, if your query returns three columns, the third of which is a BLOB, you must return the values of the first and second fields before accessing the BLOB data in the third field. If you access the third field before the first or second fields, the first and second field values are no longer available. This is because **SequentialAccess** has modified the **DataReader** to return data in sequence and the data is not available after the **DataReader** has read past it.

When accessing the data in the BLOB field, use the **GetBytes** or **GetChars** typed accessors of the **DataReader**, which fill an array with data. You can also use **GetString** for character data; however, to conserve system resources you might not want to load an entire BLOB value into a single string variable. You can instead specify a specific buffer size of data to be returned, and a starting location for the first byte or character to be read from the returned data. **GetBytes** and **GetChars** will return a `long` value, which represents the number of bytes or characters returned. If you pass a null array to **GetBytes** or **GetChars**, the long value returned will be the total number of bytes or characters in the BLOB. You can optionally specify an index in the array as a starting position for the data being read.

## Example

The following example returns the publisher ID and logo from the [**pubs** sample database](https://github.com/Microsoft/sql-server-samples/tree/master/samples/databases/northwind-pubs). The publisher ID (`pub_id`) is a character field, and the logo is an image, which is a BLOB. Because the **logo** field is a bitmap, the example returns binary data using **GetBytes**. Notice that the publisher ID is accessed for the current row of data before the logo, because the fields must be accessed sequentially.

[!code-csharp[SqlCommand_ExecuteReader_SequentialAccess#1](~/../sqlclient/doc/samples/SqlCommand_ExecuteReader_SequentialAccess.cs#1)]

## See also

- [SQL Server binary and large-value data](./sql/sql-server-binary-large-value-data.md)
- [Microsoft ADO.NET for SQL Server](microsoft-ado-net-sql-server.md)
