---
title: "Design your first relational database C#"
description: "Learn to design your first relational database in Azure SQL Database with C# using ADO.NET."
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: mathoma
ms.date: 06/25/2024
ms.service: azure-sql-database
ms.subservice: development
ms.topic: tutorial
ms.custom:
  - sqldbrb=1
  - devx-track-csharp
---
# Tutorial: Design a relational database in Azure SQL Database C&#x23; and ADO.NET
[!INCLUDE [appliesto-sqldb](../includes/appliesto-sqldb.md)]

Azure SQL Database is a relational database-as-a-service (DBaaS) in the Microsoft Cloud (Azure). In this tutorial, you learn how to use the Azure portal and ADO.NET with Visual Studio to:

> [!div class="checklist"]
>
> * Create a database using the Azure portal
> * Set up a server-level IP firewall rule using the Azure portal
> * Connect to the database with ADO.NET and Visual Studio
> * Create tables with ADO.NET
> * Insert, update, and delete data with ADO.NET
> * Query data ADO.NET

> [!TIP]
> This free Learn module shows you how to [Develop and configure an ASP.NET application that queries an Azure SQL Database](/training/modules/develop-app-that-queries-azure-sql/), including the creation of a simple database.

## Prerequisites

- An installation of [Visual Studio 2019](https://www.visualstudio.com/downloads/) or later.
- If you don't have an Azure subscription, [create a free account](https://azure.microsoft.com/free/) before you begin.
- If you don't already have an Azure SQL Database created, visit [Quickstart: Create a single database](single-database-create-quickstart.md). Look for the option to use your offer to [try Azure SQL Database for free (preview)](free-offer.md).

## Sign in to the Azure portal

Sign in to the [Azure portal](https://portal.azure.com/).

## Create a server-level IP firewall rule

SQL Database creates an IP firewall at the server-level. This firewall prevents external applications and tools from connecting to the server and any databases on the server unless a firewall rule allows their IP through the firewall. To enable external connectivity to your database, you must first add an IP firewall rule for your IP address (or IP address range). Follow these steps to create a [server-level IP firewall rule](firewall-configure.md).

> [!IMPORTANT]
> SQL Database communicates over port 1433. If you are trying to connect to this service from within a corporate network, outbound traffic over port 1433 might not be allowed by your network's firewall. If so, you cannot connect to your database unless your administrator opens port 1433.

1. After the deployment is complete, select **SQL databases** from the left-hand menu and then select *yourDatabase* on the **SQL databases** page. The overview page for your database opens, showing you the fully qualified **Server name** (such as *yourserver.database.windows.net*) and provides options for further configuration.

1. Copy this fully qualified server name for use to connect to your server and databases from SQL Server Management Studio.

   :::image type="content" source="media/design-first-database-csharp-tutorial/server-name.png" alt-text="Screenshot of the Azure portal, database overview page, with the server name highlighted." lightbox="media/design-first-database-csharp-tutorial/server-name.png":::

1. In the Azure portal, navigate to the logical SQL server for your Azure SQL Database. The easiest way is to select the **Server name** value on the SQL database page.

1. In the resource menu, under **Settings**, select **Networking**.

1. Choose the **Public Access** tab, and then select **Selected networks** under **Public network access**.

   :::image type="content" source="media/design-first-database-csharp-tutorial/server-firewall-rule.png" alt-text="Screenshot of the Azure portal, Networking page, showing that public network access is enabled." lightbox="media/design-first-database-csharp-tutorial/server-firewall-rule.png":::

1. Scroll down to the **Firewall rules** section.

   :::image type="content" source="media/design-first-database-csharp-tutorial/firewall-ip-address.png" alt-text="Screenshot of the Azure portal, Networking page, Firewall rules section." lightbox="media/design-first-database-csharp-tutorial/firewall-ip-address.png":::

1. Select **Add your client IPv4 address** to add your current IP address to a new IP firewall rule. An IP firewall rule can open port 1433 for a single IP address or a range of IP addresses.

1. Select **Save**. A server-level IP firewall rule is created for your current IP address opening port 1433 on the server.

1. Select **OK** and then close the **Firewall settings** page.

   Your IP address can now pass through the IP firewall. You can now connect to your database using SQL Server Management Studio or another tool of your choice. Be sure to use the server admin account you created previously.

> [!IMPORTANT]
> By default, access through the SQL Database IP firewall is enabled for all Azure services. Select **OFF** on this page to disable access for all Azure services.

[!INCLUDE [sql-database-csharp-adonet-create-query-2](../includes/sql-database-csharp-adonet-create-query-2.md)]

> [!TIP]
> To learn more about writing SQL queries, visit [Tutorial: Write Transact-SQL statements](/sql/t-sql/tutorial-writing-transact-sql-statements).

## Related content

- [Try Azure SQL Database for free (preview)](free-offer.md)
- [What's new in Azure SQL Database?](doc-changes-updates-release-notes-whats-new.md)
- [Configure and manage content reference - Azure SQL Database](how-to-content-reference-guide.md)
- [Plan and manage costs for Azure SQL Database](cost-management.md)

## Next step

Advance to the next tutorial to learn about data migration.

> [!div class="nextstepaction"]
> [Migrate SQL Server to Azure SQL Database offline using DMS](/azure/dms/tutorial-sql-server-to-azure-sql)
