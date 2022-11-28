---
title: Notebooks with SQL Kernel in Azure Data Studio
description: This tutorial shows how you can create and run a SQL Server notebook.
author: markingmyname
ms.author: maghan
ms.reviewer: mikeray, alayu
ms.date: 07/01/2020
ms.service: azure-data-studio
ms.topic: how-to
---

# Create and run a SQL Server notebook

[!INCLUDE[SQL Server 2019](../../includes/applies-to-version/sqlserver2019.md)]

This tutorial demonstrates how to create and run a notebook in Azure Data Studio using SQL Server.

## Prerequisites

- [Azure Data Studio installed](../download-azure-data-studio.md)
- SQL Server installed
  - [Windows](../../database-engine/install-windows/install-sql-server.md)
  - [Linux](../../linux/sql-server-linux-setup.md)

## Create a  notebook

The following steps show how to create a notebook file in Azure Data Studio:

1. In Azure Data Studio, connect to your SQL Server.

2. Select under the **Connections** in the **Servers** window. Then select **New Notebook**.

3. Wait for the **Kernel** and the target context (**Attach to**) to be populated. Confirm that the **Kernel** is set to **SQL**, and set **Attach to** for your SQL Server (in this example it's *localhost*).

   ![Set Kernel and Attach to](media/notebooks-sql-kernel/set-kernel-and-attach-to.png)

You can save the notebook using the **Save** or **Save as...** command from the **File** menu.

To open a notebook, you can use the **Open file...** command in the **File** menu, select **Open file** on the **Welcome** page, or use the **File: Open** command from the command palette.

## Change the SQL connection

To change the SQL connection for a notebook:

1. Select the **Attach to** menu from the notebook toolbar and then select **Change Connection**.

   ![Select the Attach to menu in the notebook toolbar](./media/notebooks-sql-kernel/select-attach-to-1.png)

2. Now you can either select a recent connection server or enter new connection details to connect.

## Run a code cell

You can create cells containing SQL code that you can run in place by clicking the **Run cell** button (the round black arrow) to the left of the cell. The results are shown in the notebook after the cell finishes running.

For example:

1. Add a new code cell by selecting the **+Code** command in the toolbar.

   ![Notebook toolbar](media/notebooks-guidance/notebook-toolbar.png)

2. Copy and paste the following example into the cell and click **Run cell**. This example creates a new database.

   ```sql
   USE master
   GO

   -- Drop the database if it already exists
   IF  EXISTS (
           SELECT name
           FROM sys.databases
           WHERE name = N'TestNotebookDB'
      )
   DROP DATABASE TestNotebookDB
   GO

   -- Create the database
   CREATE DATABASE TestNotebookDB
   GO
   ```

   ![Run cell](media/notebooks-sql-kernel/run-notebook-cell.png)

## Save the result

If you run a script that returns a result, you can save that result in different formats using the toolbar displayed above the result.

- Save As CSV
- Save As Excel
- Save As JSON
- Save As XML

For example, the following code returns the result of [PI](../../t-sql/functions/pi-transact-sql.md).

```sql
SELECT PI() AS PI;
GO
```

![Save the result](media/notebooks-sql-kernel/run-notebook-cell-2.png)

## Next steps

Learn more about notebooks:

- [How to use notebooks in Azure Data Studio](./notebooks-guidance.md)
- [Create and run a Python notebook](././notebooks-python-kernel.md)
- [Run a sample notebook using Spark](../../big-data-cluster/notebooks-tutorial-spark.md)