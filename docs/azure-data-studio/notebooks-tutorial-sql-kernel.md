---
title: Notebooks with SQL Kernel in Azure Data Studio
description: This tutorial shows how you can create and run a SQL Server notebook.
author: markingmyname
ms.author: maghan
ms.reviewer: mikeray, alayu
ms.topic: tutorial
ms.prod: azure-data-studio
ms.technology: 
ms.custom: ""
ms.date: 03/30/2020
---

# Create and run a SQL Server notebook

[!INCLUDE[SQL Server 2019](../includes/applies-to-version/sqlserver2019.md)]

This tutorial demonstrates how to create and run a notebook in Azure Data Studio using SQL Server.

## Prerequisites

- [Azure Data Studio installed](download-azure-data-studio.md)
- SQL Server installed
  - [Windows](../database-engine/install-windows/install-sql-server.md)
  - [Linux](../linux/sql-server-linux-setup.md)

## Create a  notebook

The following steps show how to create a notebook file in Azure Data Studio:

1. In Azure Data Studio, connect to your SQL Server.

1. Select under the **Connections** in the **Servers** window. Then select **New Notebook**.

   ![Open notebook](media/notebook-tutorial/azure-data-studio-open-notebook.png)

1. Wait for the **Kernel** and the target context (**Attach to**) to be populated. Confirm that the **Kernel** is set to **SQL**, and set **Attach to** for your SQL Server (in this example it's *localhost*).

   ![Set Kernel and Attach to](media/notebook-tutorial/set-kernel-and-attach-to.png)

## Run a code cell

You can run each notebook cell by pressing the **Run cell** button (the round black arrow) to the left of the cell. The results are shown in the notebook after the cell finishes running.

For example:

1. Add a new code cell by selecting the **+Code** command in the toolbar.

   ![Notebook toolbar](media/notebooks-guidance/notebook-toolbar.png)

1. Copy and paste the following example into the cell and click **Run cell**. This example creates a new database.

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

   ![Run notebook cell](media/notebook-tutorial/run-notebook-cell.png)

## Save the result

If you run a script that returns a result, you can save that result in different formats using the toolbar displayed above the result.

- Save As CSV
- Save As Excel
- Save As JSON
- Save As XML

For example, the following code returns the result of [PI](../t-sql/functions/pi-transact-sql.md).

```sql
SELECT PI() AS PI;
GO
```

![Run notebook cell](media/notebook-tutorial/run-notebook-cell-2.png)

## Next steps

Learn more about notebooks:

- [How to use notebooks](notebooks-guidance.md)

- [Create and run a Python notebook](notebooks-tutorial-python-kernel.md)

- [How to manage notebooks in Azure Data Studio](notebooks-manage-sql-server.md)

- [Run a sample notebook using Spark](../big-data-cluster/notebooks-tutorial-spark.md)