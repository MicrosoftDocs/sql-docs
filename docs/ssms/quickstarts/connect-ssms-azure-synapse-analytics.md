---
title: Connect to a dedicated SQL pool in Azure Synapse Analytics with SQL Server Management Studio (SSMS)
description: Connect to a dedicated SQL pool in Azure Synapse Analytics using SQL Server Management Studio (SSMS).
ms.prod: sql
ms.technology: ssms
ms.topic: quickstart
author: markingmyname
ms.author: maghan
ms.reviewer: sstein, mikeray
ms.custom: ""
ms.date: 12/14/2020
---

# Quickstart: Connect to a dedicated SQL pool in Azure Synapse Analytics using SQL Server Management Studio (SSMS)

[!INCLUDE [asa](../../includes/applies-to-version/asa.md)]

This quickstart explains how to use SQL Server Management Studio (SSMS) to connect to Azure Synapse Analytics.

## Prerequisites

To complete this article, you need SQL Server Management Studio and access to a data source.

- Install [SQL Server Management Studio](../download-sql-server-management-studio-ssms.md).
- [Azure Synapse Analytics](https://azure.microsoft.com/services/synapse-analytics/?&ef_id=CjwKCAiA17P9BRB2EiwAMvwNyDW39uBCUDMPmL693_KrmcNAV2uiMHZDeNJ-615AoxdIPBNqXwBDMhoCkL8QAvD_BwE:G:s&OCID=AID2100131_SEM_CjwKCAiA17P9BRB2EiwAMvwNyDW39uBCUDMPmL693_KrmcNAV2uiMHZDeNJ-615AoxdIPBNqXwBDMhoCkL8QAvD_BwE:G:s&gclid=CjwKCAiA17P9BRB2EiwAMvwNyDW39uBCUDMPmL693_KrmcNAV2uiMHZDeNJ-615AoxdIPBNqXwBDMhoCkL8QAvD_BwE)

## Connect to a dedicated SQL pool in Azure Synapse Analytics

[!INCLUDE[ssms-connect-azure-ad](../../includes/ssms-connect-azure-ad.md)]

1. Start SQL Server Management Studio. The first time you run SSMS, the **Connect to Server** window opens. If it doesn't open, you can open it manually by selecting **Object Explorer** > **Connect** > **Database Engine**.

    :::image type="content" source="media/connect-data-source-ssms/connect-object-explorer.png" alt-text="Connect link in Object Explorer":::

2. In the **Connect to Server** window, follow the list below:

    |   Setting   |   Suggested Value(s)   |   Description   |
    |-------------|------------------------|-----------------|
    | **Server type** | Database engine | For **Server type**, select **Database Engine** (usually the default option). |
    | **Server name** | The fully qualified server name | For **Server name**, enter the name of your Azure Synapse Analytics server name. |
    | **Authentication** | SQL Server Authentication | Use **SQL Server Authentication** for Azure Synapse Analytics to connect. </br> </br> The **Windows Authentication** method isn't supported for Azure SQL. For more information, see [Azure SQL authentication](/azure/sql-database/sql-database-security-overview#access-management). |
    | **Username** | Server account user ID | The user ID from the server account used to create the server. |
    | **Password** | Server account password | The password from the server account used to create the server. |

    :::image type="content" source="media/connect-data-source-ssms/connect-to-azure-synapse-analytics-object-explorer.png" alt-text="Server name field for Azure Synapse Analytics":::

    You can also modify additional connection options by selecting **Options**. Examples of connection options are the database you're connecting to, the connection timeout value, and the network protocol. This article uses the default values for all the options.

3. After selecting **Connect** when you first try to connect to your database with SSMS, a prompt appears to configure the firewall. Once you sign in, fill in your Azure account login information and continue to set the firewall rule. Then select **OK**. This prompt is a one time action. Once you configure the firewall, either with SSMS or in the Azure portal, before connecting with SSMS, the firewall prompt doesn't appear.

    :::image type="content" source="media/connect-data-source-ssms/azure-sql-firewall-sign-in-3.png" alt-text="Azure SQL New Firewall Rule":::

4. After you've completed all the fields, select **Connect**.

5. To verify that your Azure Synapse Analytics connection succeeded, expand and explore the objects within **Object Explorer** where the server name, the SQL Server version, and the username are displayed. These objects are different depending on the server type.

    :::image type="content" source="media/connect-data-source-ssms/connect-azure-synapse-analytics.png" alt-text="Connecting to an Azure Synapse Analytics database":::

## Troubleshoot connectivity issues

You can encounter connection problems caused by reconfiguration, firewall settings, a connection timeout, incorrect login information or failure to apply best practices and design guidelines during the application design process. For more information on how to troubleshoot connection problems, visit [Troubleshooting connectivity issues](https://docs.microsoft.com/azure/azure-sql/database/troubleshoot-common-errors-issues).

You can prevent, troubleshoot, diagnose, and mitigate connection and transient errors that you encounter when interacting with Azure Synapse Analytics. For more information, visit [Troubleshoot transient connection errors](https://docs.microsoft.com/azure/azure-sql/database/troubleshoot-common-connectivity-issues).

## Next steps

Advance to the next article to learn how to create.

> [!div class="nextstepaction"]
> [Create and query a dedicated SQL pool in Azure Synapse Analytics](query-ssms-azure-synapse-analytics.md)