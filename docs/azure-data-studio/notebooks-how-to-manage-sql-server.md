---
title: Manage a SQL Server notebook
description: Learn how to manage notebooks in Azure Data Studio. This includes opening notebooks, saving them, and changing your big data cluster connection.
author: markingmyname
ms.author: maghan
ms.reviewer: achatter, alayu, mikeray
ms.metadata: seo-lt-2019
ms.topic: conceptual
ms.prod: sql
ms.technology: azure-data-studio
ms.custom: ""
ms.date: 12/13/2019
---

# How to manage notebooks in Azure Data Studio

[!INCLUDE[tsql-appliesto-ssver15-xxxx-xxxx-xxx](../includes/tsql-appliesto-ssver15-xxxx-xxxx-xxx.md)]

This article shows you how to open and save notebook files in Azure Data Studio with SQL Server. It also demonstrates how to change your connection to your SQL Server.

## Open a notebook

There are several ways to open the **Open Notebook** dialog. You can use the File menu, the Dashboard, and the Command Palette. The following sections describe each method.

### File menu

Select **File Open** from the File menu Ctrl+O (in Windows) and Cmd+O (in Mac).

![Open the Open File dialog by selecting File Open](./media/notebooks-how-to-manage-sql-server/open-file-1.png)

### Dashboard

Click **Open Notebook** in the dashboard to open the File Open dialog.

![Open the Open File dialog by selecting Open Notebook in the dashboard](./media/notebooks-how-to-manage-sql-server/open-file-2.png) 

### Command Palette

Use command **File: Open** from command palette by typing Ctrl+Shift+P (in Windows) and Cmd+Shift+P (in Mac).

![Open the Open File dialog by entering File:Open in the command palette](./media/notebooks-how-to-manage-sql-server/open-file-3.png)

## Save a notebook

There is currently one way to save a notebook. You must select **Save** from the notebook toolbar.

![Save File by clicking Save in the notebook toolbar](./media/notebooks-how-to-manage-sql-server/save-file-1.png)

> [!NOTE]
> The following methods currently do not save changes to notebooks:
>
> - **File Save**, **File Save As...** and **File Save All** commands from the File menu.
> - **File: Save** commands entered in the command palette.

## Change the SQL Server

To change the SQL Server big data cluster for a notebook:

1. Select the **Attach to** menu from the notebook toolbar amd then select **Change Connection**.

   ![Click the Attach to menu in the notebook toolbar](./media/notebooks-how-to-manage-sql-server/select-attach-to-1.png)

2. Now you can either select a recent connection server or enter new connection details to connect.

   ![Select a server from the Attach to menu](./media/notebooks-how-to-manage-sql-server/select-attach-to-2.png)

## Next steps

For more information about notebooks in Azure Data Studio, see [How to use notebooks in SQL Server 2019](notebooks-guidance.md).
