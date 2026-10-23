---
title: Migrate the Oracle HR Schema to SQL Server on Linux
description: Learn how to convert a sample Oracle schema to SQL Server on Linux.
author: nilabjaball
ms.author: niball
ms.reviewer: vanto
ms.date: 06/03/2025
ms.service: sql
ms.subservice: ssma
ms.topic: upgrade-and-migration-article
ms.collection:
  - sql-migration-content
ms.custom:
  - intro-migration
  - linux-related-content
  - sfi-image-nochange
---

# Migrate an Oracle schema to SQL Server 2017 on Linux by using the SQL Server Migration Assistant

Learn how to use SQL Server Migration Assistant (SSMA) for Oracle to convert the Oracle sample HR schema to [SQL Server on Linux](../../linux/sql-server-linux-overview.md).

In this article, you complete the following steps:

> [!div class="checklist"]
> - Download and install SSMA on Windows
> - Create an SSMA project to manage the migration
> - Connect to Oracle
> - Run a migration report
> - Convert the sample HR schema
> - Migrate the data

## Prerequisites

- An instance of Oracle 12c (12.2.0.1.0) with the HR schema installed
- A working instance of SQL Server on Linux

> [!NOTE]  
> The same steps can be used to target SQL Server on Windows, but you must select **Windows** in the **Migrate To** project setting.

## Download and install SSMA for Oracle

There are several editions of SQL Server Migration Assistant available, depending on your source database. Download the current version of [SQL Server Migration Assistant for Oracle](https://aka.ms/ssmafororacle) and install it by using the instructions found on the download page.

> [!NOTE]  
> At this time, the **SSMA for Oracle Extension Pack** isn't supported on Linux, but it's not necessary for this tutorial.

## Create and set up a new SSMA project

1. Open SSMA for Oracle and choose **New Project** from the **File** menu.

1. Give the project a name.

1. In the **Migrate To** field, choose **SQL Server 2017 (Linux) - Preview**.

## Enable the HR schema

SSMA for Oracle doesn't use the Oracle sample schemas by default. To enable the HR schema, use the following steps:

1. In SSMA, select the **Tools** menu.

1. Select **Default Project Settings**, and then choose **Loading System Objects**.

1. Make sure **HR** is checked, and choose **OK**.

## Connect SSMA to Oracle

1. On the toolbar, select **Connect to Oracle**.

1. Fill in the **Server name**, **Server port**, **Oracle SID**, **User name**, and **Password** fields.

   :::image type="content" source="media/sql-server-linux-convert-from-oracle/ConnectToOracle.png" alt-text="Screenshot that shows how to connect to Oracle." lightbox="media/sql-server-linux-convert-from-oracle/ConnectToOracle.png":::

1. Select **Connect**. In a few moments, SSMA for Oracle connects to your database and reads its metadata.

## Generate a migration report

1. In the **Oracle Metadata Explorer**, expand your server's node.

1. Expand **Schemas**, right-click **HR**, and select **Create Report**.

   :::image type="content" source="media/sql-server-linux-convert-from-oracle/CreateReport.png" alt-text="Screenshot that shows how to create a report." lightbox="media/sql-server-linux-convert-from-oracle/CreateReport.png":::

1. A new browser window opens with a report that lists all of the warnings and errors associated with the conversion.

   :::image type="content" source="media/sql-server-linux-convert-from-oracle/SSMAReport.png" alt-text="Screenshot that shows an example report." lightbox="media/sql-server-linux-convert-from-oracle/SSMAReport.png":::

   > [!NOTE]  
   > You don't need to do anything with the list of warnings and errors for this tutorial. If you perform these steps for your own Oracle database, you should review the report and address any important conversion problems.

## Connect to SQL Server

Choose **Connect to SQL Server** and enter the appropriate connection information. If you use a database name that doesn't already exist, SSMA for Oracle creates it for you.

:::image type="content" source="media/sql-server-linux-convert-from-oracle/ConnectToSQLServer.png" alt-text="Screenshot that shows the Connect to SQL Server dialog." lightbox="media/sql-server-linux-convert-from-oracle/ConnectToSQLServer.png":::

## Convert Schema

Right-click **HR** in **Oracle Metadata Explorer**, and select **Convert Schema**.

:::image type="content" source="media/sql-server-linux-convert-from-oracle/ConvertSchema.png" alt-text="Screenshot that shows how to select Convert Schema." lightbox="media/sql-server-linux-convert-from-oracle/ConvertSchema.png":::

## Synchronize your database

1. After the conversion finishes, use the **SQL Server Metadata Explorer** to go to the database you created in the previous step.

1. Right-click your database, select **Synchronize with Database**, and then select **OK**.

   :::image type="content" source="media/sql-server-linux-convert-from-oracle/SynchronizeWithDatabase.png" alt-text="Screenshot that shows how to choose the Synchronize with Database option." lightbox="media/sql-server-linux-convert-from-oracle/SynchronizeWithDatabase.png":::

## Migrate data

The final step is to migrate your data.

1. In the **Oracle Metadata Explorer**, right-click **HR**, and select **Migrate Data**.

1. The data migration step requires that you reenter your Oracle and SQL Server credentials.

1. When you're finished, review the data migration report, which should look similar to the following screenshot:

   :::image type="content" source="media/sql-server-linux-convert-from-oracle/DataMigrationReport.png" alt-text="Screenshot that shows a data migration report." lightbox="media/sql-server-linux-convert-from-oracle/DataMigrationReport.png":::

## Conclusion

For a more complex Oracle schema, the conversion process involves more time, testing, and possible changes to client applications. The purpose of this tutorial is to show how you can use SSMA for Oracle as a part of your overall migration process.

In this tutorial, you learned how to:

> [!div class="checklist"]
> - Install SSMA on Windows.
> - Create a new SSMA project.
> - Assess and run a migration from Oracle.

## Next step

> [!div class="nextstepaction"]
> [SQL Server Migration Assistant](../sql-server-migration-assistant.md)
