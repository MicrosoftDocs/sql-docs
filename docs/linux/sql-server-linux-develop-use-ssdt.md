---
title: Develop and Deploy SQL Server Databases for Linux
description: SQL Server Data Tools with Visual Studio is a powerful development and database lifecycle management environment for SQL Server on Linux.
author: rwestMSFT
ms.author: randolphwest
ms.date: 07/03/2025
ms.service: sql
ms.subservice: linux
ms.topic: install-set-up-deploy
ms.custom:
  - linux-related-content
  - sfi-image-nochange
---

# Use Visual Studio to create databases for SQL Server on Linux

[!INCLUDE [SQL Server - Linux](../includes/applies-to-version/sql-linux.md)]

SQL Server Data Tools (SSDT) turns Visual Studio into a powerful development and database lifecycle management (DLM) environment for SQL Server on Linux. You can develop, build, test, and publish your database from a source-controlled project, just as you develop your application code.

## Install Visual Studio and SQL Server Data Tools

1. If you haven't already installed Visual Studio on your Windows machine, [Download and Install Visual Studio](https://visualstudio.microsoft.com/downloads/). If you don't have a Visual Studio license, Visual Studio Community edition is a free, fully featured IDE for students, open-source, and individual developers.

1. During the Visual Studio installation, select **Custom** for the **Choose the type of installation** option. Select **Next**.

1. Select **Microsoft SQL Server Data Tools**, **Git for Windows**, and **GitHub Extension for Visual Studio** from the feature selection list.

   :::image type="content" source="media/sql-server-linux-develop-use-ssdt/ssdt-setup.png" alt-text="Screenshot of SSDT setup." lightbox="media/sql-server-linux-develop-use-ssdt/ssdt-setup.png":::

1. Continue and finish the installation of Visual Studio. It can take a few minutes.

## Get the latest version of SQL Server Data Tools

SQL Server on Linux is supported by SSDT version 17.0 or later.

- [Install SQL Server Data Tools (SSDT) for Visual Studio](../ssdt/download-sql-server-data-tools-ssdt.md)

## Create a new database project in source control

1. Launch Visual Studio.

1. Select **Team Explorer** on the **View** menu.

1. Select **New** in the **Local Git Repository** section on the **Connect** page.

   :::image type="content" source="media/sql-server-linux-develop-use-ssdt/git-repository.png" alt-text="Screenshot of the Local Git Repository section with the New option called out.":::

1. Select **Create**. After the local Git repository is created, double-click **SSDTRepo**.

1. Select **New** in the **Solutions** section. Select **SQL Server** under the **Other Languages** node in the **New Project** dialog.

   :::image type="content" source="media/sql-server-linux-develop-use-ssdt/new-project.png" alt-text="Screenshot of the Solutions section with the New option and SQL Server option called out." lightbox="media/sql-server-linux-develop-use-ssdt/new-project.png":::

1. Enter `TutorialDB` as the name, and then select **OK** to create a new database project.

## Create a new table in the database project

1. Select **Solution Explorer** on the **View** menu.

1. Open the database project menu by right-clicking on **TutorialDB** in Solution Explorer.

1. Select **Table** under **Add**.

   :::image type="content" source="media/sql-server-linux-develop-use-ssdt/create-table.png" alt-text="Screenshot showing how to create a new table using Add > Table." lightbox="media/sql-server-linux-develop-use-ssdt/create-table.png":::

1. In the table designer, add the **Name** and **Location** columns. Set both columns to the **nvarchar(50)** data type, as shown in the image. SSDT generates the `CREATE TABLE` script as you add the columns in the designer.

   :::image type="content" source="media/sql-server-linux-develop-use-ssdt/add-columns.png" alt-text="Screenshot of the table designer with the Name and Location values called out." lightbox="media/sql-server-linux-develop-use-ssdt/add-columns.png":::

1. Save the **Table1.sql** file.

## Build and validate the database

1. Open the database project menu on **TutorialDB** and select **Build**. SSDT compiles the `.sql` source files in your project and builds a data-tier application package (DACPAC) file. You can use this file to publish a database to your SQL Server instance on Linux.

   :::image type="content" source="media/sql-server-linux-develop-use-ssdt/build.png" alt-text="Screenshot showing the TutorialDB with the Build option called out." lightbox="media/sql-server-linux-develop-use-ssdt/build.png":::

1. Check for a build success message in the **Output** window in Visual Studio.

## Publish the database to a SQL Server instance on Linux

1. Open the database project menu on **TutorialDB** and select **Publish**.

1. Select **Edit** to select your SQL Server instance on Linux.

   :::image type="content" source="media/sql-server-linux-develop-use-ssdt/publish-dialog.png" alt-text="Screenshot showing the Publish option with the Edit option called out." lightbox="media/sql-server-linux-develop-use-ssdt/publish-dialog.png":::

1. In the connection dialog, enter the IP address or host name, user name, and password for your SQL Server instance on Linux.

   :::image type="content" source="media/sql-server-linux-develop-use-ssdt/connection-dialog.png" alt-text="Screenshot showing the connection dialog." lightbox="media/sql-server-linux-develop-use-ssdt/connection-dialog.png":::

1. Select **Publish** in the publish dialog.

1. Check the publish status in the **Data Tools Operations** window.

1. Select **View Results** or **View Script** to see details of the database publish result on your SQL Server on Linux.

   :::image type="content" source="media/sql-server-linux-develop-use-ssdt/publish-result.png" alt-text="Screenshot showing the publish result with View Script and View Results called out." lightbox="media/sql-server-linux-develop-use-ssdt/publish-result.png":::

You've successfully created a new database on a SQL Server instance on Linux and learned the basics of developing a database with a source-controlled database project.

## Related content

- [Tutorial: Write Transact-SQL statements](../t-sql/tutorial-writing-transact-sql-statements.md)
- [Download and Install Visual Studio](https://visualstudio.microsoft.com/downloads)
- [Install SQL Server Data Tools (SSDT) for Visual Studio](../ssdt/download-sql-server-data-tools-ssdt.md)
- [Archived SQL Server Data Tools content](/previous-versions/sql/sql-server-data-tools/hh272686(v=vs.103))
- [Transact-SQL reference (Database Engine)](../t-sql/language-reference.md)
