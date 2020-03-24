---
title: Create a SQL Server notebook
description: This tutorial shows how you can create and run a SQL Server notebook.
author: markingmyname
ms.author: maghan
ms.reviewer: mikeray, alayu
ms.topic: tutorial
ms.prod: sql
ms.technology: azure-data-studio
ms.custom: ""
ms.date: 03/27/2020
---

# Create and run a SQL Server notebook

[!INCLUDE[tsql-appliesto-ssver15-xxxx-xxxx-xxx](../includes/tsql-appliesto-ssver15-xxxx-xxxx-xxx.md)]

This tutorial demonstrates how to create and run a notebook in Azure Data Studio using SQL Server.

## Prerequisites

- [Azure Data Studio installed](download-azure-data-studio.md)
- SQL Server installed
  - [Windows](../database-engine/install-windows/install-sql-server.md)
  - [Linux](../linux/sql-server-linux-setup.md)

## New notebook

The following steps show how to create a notebook file in Azure Data Studio:

1. In Azure Data Studio, connect to your SQL Server.

2. Select under the **Connections** in the **Servers** window. Then select **New Notebook**.

   ![Open notebook](media/notebook-tutorial/azure-data-studio-open-notebook.png)

3. Wait for the **Kernel** and the target context (**Attach to**) to be populated. Confirm that the **Kernel** is set to **SQL**, and set **Attach to** for your SQL Server (in this case its *localhost*).

   ![Set Kernel and Attach to](media/notebook-tutorial/set-kernel-and-attach-to.png)

## Run a notebook cell

You can run each notebook cell by pressing the play button to the left of the cell. The results are shown in the notebook after the cell finishes running.

### Code

Add a new code cell by selecting the **+Code** command in the toolbar.

![Notebook toolbar](media/notebooks-guidance/notebook-toolbar.png)

This example uses a T-SQL script that creates a database.

   ```tsql
   -- =============================================
   -- Create database template
   -- =============================================
   USE master
   GO

   -- Drop the database if it already exists
   IF  EXISTS (
       SELECT name
           FROM sys.databases
           WHERE name = N'<Database_Name, sysname, Database_Name>'
   )
   DROP DATABASE <Database_Name, sysname, Database_Name>
   GO

   -- Create the database
   CREATE DATABASE <Database_Name, sysname, Database_Name>
   GO
   ```

   ![Run notebook cell](media/notebook-tutorial/run-notebook-cell.png)

If you run a T-SQL script that returns a result, you can save that result in different formats.

In this case, we used T-SQL to return [PI](../t-sql/functions/pi-transact-sql.md).

- Save As CSV
- Save As Excel
- Save As JSON
- Save As XML

![Run notebook cell](media/notebook-tutorial/run-notebook-cell-2.png)

### Text

Add a new text cell by selecting the **+Text** command in the toolbar.

![Notebook toolbar](media/notebooks-guidance/notebook-toolbar.png)

The cell changes to edit mode and now type markdown and you can see the preview at the same time

![Markdown cell](media/notebooks-guidance/notebook-markdown-cell.png)

Selecting outside the text cell shows the markdown text.

![Markdown text](media/notebooks-guidance/notebook-markdown-preview.png)

## Next steps

Learn more about notebooks:

- [How to use notebooks with SQL Server](notebooks-guidance.md)

- [How to manage notebooks in Azure Data Studio](notebooks-manage-sql-server.md)

- [Run a sample notebook on a SQL Server big data cluster](../big-data-cluster/notebooks-tutorial-spark.md)