---
title: Connect to SQL Server with SQL Server Management Studio (SSMS)
description: Connect to SQL Server instance using SQL Server Management Studio (SSMS).
ms.prod: sql
ms.technology: ssms
ms.topic: quickstart
author: markingmyname
ms.author: maghan
ms.reviewer: sstein, mikeray
ms.custom: ""
ms.date: 12/14/2020
---

# Quickstart: Connect to SQL Server using SQL Server Management Studio (SSMS)

[!INCLUDE [sqlserver](../../includes/applies-to-version/sqlserver.md)]

Get started with SQL Server by using SQL Server Management Studio (SSMS) to connect to a SQL Server instance.

The article demonstrates how to follow the below steps:

> [!div class="checklist"]
> - Connect to a SQL Server instance

## Prerequisites

- [SQL Server Management Studio](../download-sql-server-management-studio-ssms.md) installed.
- [SQL Server instance](https://www.microsoft.com/sql-server/sql-server-downloads) installed and configured.

## Connect to a SQL Server instance

1. Start SQL Server Management Studio. The first time you run SSMS, the **Connect to Server** window opens. If it doesn't open, you can open it manually by selecting **Object Explorer** > **Connect** > **Database Engine**.

    :::image type="content" source="media/connect-data-source-ssms/connect-object-explorer.png" alt-text="Connect link in Object Explorer":::

2. The **Connect to Server** dialog box appears. Enter the following information:

    |   Setting   |   Suggested Value(s)   |   Description   |
    |--------------|-----------------------|-----------------|
    | **Server type** | Database engine | For **Server type**, select **Database Engine** (usually the default option). |
    | **Server name** | The fully qualified server name | For **Server name**, enter the name of your SQL Server (you can also use *localhost* as the server name if you're connecting locally). If you're NOT using the default instance - ***MSSQLSERVER*** - you must enter in the server name and the instance name. </br> </br> If you're unsure how to determine your SQL Server instance name, see [Additional tips and tricks for using SSMS](../tutorials/ssms-tricks.md#find-sql-server-instance-name). |
    | **Authentication** | Windows Authentication </br> </br> SQL Server Authentication | Windows Authentication is set as default. </br> </br> You can also use **SQL Server Authentication** to connect. However, if you select **SQL Server Authentication**, a username and password are required. </br> </br> For more information about authentication types, see [Connect to the server (database engine)](../f1-help/connect-to-server-database-engine.md). |
    | **Username** | Server account user ID | The user ID from the server account used to log in to the server. A login is required when using **SQL Server Authentication**. |
    | **Password** | Server account password | The password from the server account used to log in the server. A password is required when using **SQL Server Authentication**. |

    :::image type="content" source="media/connect-data-source-ssms/connect-to-sql-server-object-explorer.png" alt-text="Server name field for SQL Server":::

    You can also modify additional connection options by selecting **Options**. Examples of connection options are the database you're connecting to, the connection timeout value, and the network protocol. This article uses the default values for all the fields.

3. After you've completed all the fields, select **Connect**.

4. To verify that your SQL Server connection succeeded, expand and explore the objects within **Object Explorer** where the server name, the SQL Server version, and the username are displayed. These objects are different depending on the server type.

    :::image type="content" source="media/connect-data-source-ssms/connect-on-prem.png" alt-text="Connecting to an on-premises server":::

## Troubleshoot connectivity issues

To review troubleshooting techniques to use when you can't connect to an instance of your SQL Server Database Engine on a single server, visit [Troubleshoot connecting to the SQL Server Database Engine](../../database-engine/configure-windows/troubleshoot-connecting-to-the-sql-server-database-engine.md).

## Next steps

Advance to the next article to learn how to create...

> [!div class="nextstepaction"]
> [Create and query a SQL Server database](query-ssms-sql-server.md)