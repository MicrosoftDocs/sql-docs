---
title: Create an Azure Portal server-level firewall rule
description: Learn how to create a server-level firewall rule with Azure Portal
services: sql-database
ms.service: sql-database
ms.subservice: security
ms.custom: sqldbrb=1, mode-ui
ms.devlang: 
ms.topic: quickstart
author: rohitnayakmsft
ms.author: rohitna
ms.reviewer: kendralittle, mathoma, vanto
ms.date: 06/06/2022
ms.custom: kr2b-contr-experiment
---
# Quickstart: Create a server-level firewall rule in Azure portal
[!INCLUDE[appliesto-sqldb](../includes/appliesto-sqldb.md)]

This quick-start tutorial describes how to create a [server-level firewall rule](firewall-configure.md) in Azure SQL Database, using Azure portal. Firewall rules enable you to connect to [logical SQL servers](logical-servers.md), single databases, and elastic pools and their databases. They are also needed to connect on-premises and other Azure resources to the database. Server-level firewall rules do not apply to Azure SQL Managed Instance.

## Prerequisites

We will use the resources developed in [Create a single database using the Azure portal](single-database-create-quickstart.md) as a starting point.

## Sign in to the Azure portal

Sign in to the [Azure portal](https://portal.azure.com/).

## Create a server-level IP firewall rule

 SQL Database creates a firewall at the  server level for single and pooled databases. This firewall prevents client applications from connecting to a server or any of its databases. To connect from an IP address outside Azure, you need to create a firewall rule. You are able to open the firewall for a specific IP address or a range of IP addresses. For more information about server-level and database-level IP firewall rules, see [Server-level and database-level IP firewall rules](firewall-configure.md).

> [!NOTE]
> Azure SQL Database communicates over port 1433. When you connect from within a corporate network, outbound traffic over port 1433 may not be permitted by your network firewall. This means your IT department needs to open port 1433 for you to connect to your server.
> [!IMPORTANT]
> A firewall rule of 0.0.0.0 enables all Azure services to pass through the server-level firewall rule. Connections to databases is through the configured server.

Follow these steps to create a server-level, IP firewall rule for a specific, client IP address. This will enable external connectivity for that IP address through the Azure SQL Database firewall.

1. After the [database](#prerequisites) deployment completes, select **SQL databases** from the left-hand menu and then select **mySampleDatabase** on the **SQL databases** page. The overview page for your database opens. It displays the fully qualified server name (such as **mynewserver-20170824.database.windows.net**) and provides options for further configuration.

2. Copy the fully qualified server name. You will use it when you connect to your server and its databases in other quickstarts.

   :::image type="content" source="./media/firewall-create-server-level-portal-quickstart/server-name.png" alt-text="A screenshot that shows where to copy your server name.":::

3. Select **Set server firewall** on the toolbar. The **Firewall settings** page for the server opens.

   :::image type="content" source="./media/firewall-create-server-level-portal-quickstart/server-firewall-rule.png" alt-text="A screenshot that shows configuration of a server-level IP firewall rule.":::

4. Choose **Add client IP** on the toolbar to add your current IP address to a new server-level IP firewall rule. A server-level IP firewall rule can open port 1433 for a single IP address or for a range of IP addresses.

   > [!IMPORTANT]
   > By default, access through the Azure SQL Database firewall is disabled for all Azure services. Choose **ON** on this page if you want to enable access for all Azure services.
   >

5. Select **Save**. A server-level IP firewall rule is created for your current IP address opening port 1433 on the server.

6. Close the **Firewall settings** page.

Using SQL Server Management Studio or another tool of your choice, you can now connect to the server and its databases from this IP address. to do this, use the server admin account you created earlier.

## Clean up resources

Save these resources if you want to go to [Next steps](#next-steps) and learn how to connect and query your database using a number of different methods. If, however, you want to delete the resources that you created in this quickstart, use the following steps.

1. From the left-hand menu in Azure portal, select **Resource groups** and then select **myResourceGroup**.
2. On your resource group page, select **Delete**, type **myResourceGroup** in the text box, and then select **Delete**.

## Next steps

- Now that you have a database, you can [connect and query](connect-query-content-reference-guide.md) it using one of your favorite tools or languages, including:
  - [Connect and query using SQL Server Management Studio](connect-query-ssms.md)
  - [Connect and query using Azure Data Studio](/sql/azure-data-studio/quickstart-sql-database?toc=/azure/sql-database/toc.json)
- To learn how to design your first database, create tables, and insert data, see one of these tutorials:
  - [Design your first single database in Azure SQL Database using SSMS](design-first-database-tutorial.md)
  - [Design a single database in Azure SQL Database and connect with C# and ADO.NET](design-first-database-csharp-tutorial.md)
