---
title: Connect to an Azure SQL database with SQL Server Management Studio (SSMS)
description: Connect to an Azure SQL database using SQL Server Management Studio (SSMS).
ms.prod: sql
ms.technology: ssms
ms.topic: quickstart
author: markingmyname
ms.author: maghan
ms.reviewer: sstein, mikeray
ms.custom: ""
ms.date: 12/14/2020
---

# Quickstart: Connect to an Azure SQL Database or Azure SQL Managed Instance using SQL Server Management Studio (SSMS)

[!INCLUDE [asdb](../../includes/applies-to-version/asdb.md)]

Get started with SQL Server by using SQL Server Management Studio (SSMS) to connect to an Azure SQL database.

The article demonstrates how to follow the below steps:

> [!div class="checklist"]
> - Connect to an Azure SQL database

## Prerequisites

- [SQL Server Management Studio](../download-sql-server-management-studio-ssms.md) installed.
- [Azure SQL Database](https://azure.microsoft.com/free/sql-database/search/?&ef_id=CjwKCAiA17P9BRB2EiwAMvwNyDTqtKIHvBKK_qCTAnj3san_fx4KFjrSR8c58InygXQX5m_G71ZoMhoCzSMQAvD_BwE:G:s&OCID=AID2100131_SEM_CjwKCAiA17P9BRB2EiwAMvwNyDTqtKIHvBKK_qCTAnj3san_fx4KFjrSR8c58InygXQX5m_G71ZoMhoCzSMQAvD_BwE:G:s&gclid=CjwKCAiA17P9BRB2EiwAMvwNyDTqtKIHvBKK_qCTAnj3san_fx4KFjrSR8c58InygXQX5m_G71ZoMhoCzSMQAvD_BwE) or [Azure SQL Managed Instance](https://azure.microsoft.com/services/azure-sql/.sql-managed-instance/)

## Connect to an Azure SQL Database or Azure SQL Managed Instance

[!INCLUDE[ssms-connect-azure-ad](../../includes/ssms-connect-azure-ad.md)]

1. Start SQL Server Management Studio. The first time you run SSMS, the **Connect to Server** window opens. If it doesn't open, you can open it manually by selecting **Object Explorer** > **Connect** > **Database Engine**.

    :::image type="content" source="media/connect-data-source-ssms/connect-object-explorer.png" alt-text="Connect link in Object Explorer":::

2. The **Connect to Server** dialog box appears. Enter the following information:

    |   Setting   |   Suggested Value(s)   |   Description   |
    |-------------|------------------------|-----------------|
    | **Server type** | Database engine | For **Server type**, select **Database Engine** (usually the default option). |
    | **Server name** | The fully qualified server name | For **Server name**, enter the name of your *Azure SQL Database* or *Azure Managed Instance* name. |
    | **Authentication** | SQL Server Authentication | Use **SQL Server Authentication** for Azure SQL to connect. </br> </br> The **Windows Authentication** method isn't supported for Azure SQL. For more information, see [Azure SQL authentication](/azure/sql-database/sql-database-security-overview#access-management). |
    | **Username** | Server account user ID | The user ID from the server account used to create the server. |
    | **Password** | Server account password | The password from the server account used to create the server. |

    You can also modify additional connection options by selecting **Options**. Examples of connection options are the database you're connecting to, the connection timeout value, and the network protocol. This article uses the default values for all the options.

    :::image type="content" source="media/connect-data-source-ssms/connect-to-azure-sql-object-explorer.png" alt-text="Server name field for Azure SQL":::

    You can also modify additional connection options by selecting **Options**. Examples of connection options are the database you're connecting to, the connection timeout value, and the network protocol. This article uses the default values for all the options.

3. After selecting **Connect** when you first try to connect to your database with SSMS, a prompt appears to configure the firewall. Once you sign in, fill in your Azure account login information and continue to set the firewall rule. Then select **OK**. This prompt is a one time action. Once you configure the firewall, either with SSMS or in the Azure portal, before connecting with SSMS, the firewall prompt doesn't appear.

    :::image type="content" source="media/connect-data-source-ssms/azure-sql-firewall-sign-in-3.png" alt-text="Azure SQL New Firewall Rule":::

4. To verify that your Azure SQL Database or Azure Managed Instance connection succeeded, expand and explore the objects within **Object Explorer** where the server name, the SQL Server version, and the username are displayed. These objects are different depending on the server type.

    :::image type="content" source="media/connect-data-source-ssms/connect-azure-sql.png" alt-text="Connecting to a SQL Azure DB":::

## Troubleshoot connectivity issues

You can encounter connection problems caused by reconfiguration, firewall settings, a connection timeout, incorrect login information or failure to apply best practices and design guidelines during the application design process. For more information on how to troubleshoot connection problems, visit [Troubleshooting connectivity issues](https://docs.microsoft.com/azure/azure-sql/database/troubleshoot-common-errors-issues).

You can prevent, troubleshoot, diagnose, and mitigate connection and transient errors that you encounter when interacting with Azure SQL Database or Azure SQL Managed Instance. For more information, visit [Troubleshoot transient connection errors](https://docs.microsoft.com/azure/azure-sql/database/troubleshoot-common-connectivity-issues).

## Next steps

Advance to the next article to learn how to create.

> [!div class="nextstepaction"]
> [Create and query an Azure SQL Database](query-ssms-sql-server.md)