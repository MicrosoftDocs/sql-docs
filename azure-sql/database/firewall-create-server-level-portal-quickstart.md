---
title: Create an Azure portal server-level firewall rule
description: Learn how to create a server-level firewall rule with Azure portal
author: rohitnayakmsft
ms.author: rohitna
ms.reviewer: kendralittle, mathoma, vanto
ms.date: 07/14/2022
ms.service: sql-database
ms.subservice: security
ms.topic: quickstart
ms.custom: kr2b-contr-experiment
---
# Quickstart: Create a server-level firewall rule in Azure portal
[!INCLUDE[appliesto-sqldb](../includes/appliesto-sqldb.md)]

This quickstart describes how to create a [server-level firewall rule](firewall-configure.md) in Azure SQL Database. Firewall rules can give access to [logical SQL servers](logical-servers.md), single databases, and elastic pools and their databases. Firewall rules are also needed to connect on-premises and other Azure resources to databases. Server-level firewall rules do not apply to Azure SQL Managed Instance.

## Prerequisites

We will use the resources developed in [Create a single database using the Azure portal](single-database-create-quickstart.md) as a starting point for this tutorial.

## Sign in to Azure portal

Sign in to [Azure portal](https://portal.azure.com/).

## Create a server-level IP-based firewall rule

 Azure SQL Database creates a firewall at the server level for single and pooled databases. This firewall blocks connections from IP addresses that do not have permission. To connect to an Azure SQL database from an IP address outside of Azure, you need to create a firewall rule. You can use rules to open a firewall for a specific IP address or for a range of IP addresses. For more information about server-level and database-level firewall rules, see [Server-level and database-level IP-based firewall rules](firewall-configure.md).

> [!NOTE]
> Azure SQL Database communicates over port 1433. When you connect from within a corporate network, outbound traffic over port 1433 may not be permitted by your network firewall. This means your IT department needs to open port 1433 for you to connect to your server.
> [!IMPORTANT]
> A firewall rule of 0.0.0.0 enables all Azure services to pass through the server-level firewall rule and attempt to connect to a database through the server.

We'll use the following steps to create a server-level IP-based, firewall rule for a specific, client IP address. This enables external connectivity for that IP address through the Azure SQL Database firewall.

1. After the [database](#prerequisites) deployment completes, select **SQL databases** from the left-hand menu and then select **mySampleDatabase** on the **SQL databases** page. The overview page for your database opens. It displays the fully qualified server name (such as **mydocssampleserver.database.windows.net**) and provides options for further configuration. You can also find the firewall settings by navigating directly to your server, and selecting **Networking** under **Security**. 

2. Copy the fully qualified server name. You will use it when you connect to your server and its databases in other quickstarts. Select **Set server firewall** on the toolbar. 

   :::image type="content" source="./media/firewall-create-server-level-portal-quickstart/server-name.png" alt-text="A screenshot that shows where to copy your server name, and how to set server firewall on the toolbar." lightbox="./media/firewall-create-server-level-portal-quickstart/server-name.png":::

3. Set **Public network access** to **Selected networks** to reveal the virtual networks and firewall rules. When set to **Disabled**, virtual networks and firewall rule settings are hidden. 

   :::image type="content" source="./media/firewall-create-server-level-portal-quickstart/server-firewall-rule.png" alt-text="A screenshot that shows configuration of a server-level IP firewall rule." lightbox="./media/firewall-create-server-level-portal-quickstart/server-firewall-rule.png":::

4. Choose **Add your client IP** to add your current IP address to a new, server-level, firewall rule. This rule can open Port 1433 for a single IP address or for a range of IP addresses. You can also configure firewall settings by choosing **Add a firewall rule**. 

   > [!IMPORTANT]
   > By default, access through the Azure SQL Database firewall is disabled for all Azure services. Choose **ON** on this page to enable access for all Azure services.
   >

5. Select **Save**. Port 1433 is now open on the server and a server-level IP-based, firewall rule is created for your current IP address.

6. Close the **Networking** page.

   Open SQL Server Management Studio or another tool of your choice. Use the server admin account you created earlier to connect to the server and its databases from your IP address.

7. Save the resources from this quickstart to complete additional SQL database tutorials.

## Clean up resources

Use the following steps to delete the resources that you created during this quickstart:

1. From the left-hand menu in Azure portal, select **Resource groups** and then select **myResourceGroup**.
2. On your resource group page, select **Delete**, type **myResourceGroup** in the text box, and then select **Delete**.

## Next steps

- Learn how to [connect and query](connect-query-content-reference-guide.md) your database using your favorite tools or languages, including:
  - [Connect and query using SQL Server Management Studio](connect-query-ssms.md)
  - [Connect and query using Azure Data Studio](/sql/azure-data-studio/quickstart-sql-database?toc=/azure/sql-database/toc.json)
- Learn how to design your first database, create tables, and insert data, see one of these tutorials:
  - [Design your first single database in Azure SQL Database using SSMS](design-first-database-tutorial.md)
  - [Design a single database in Azure SQL Database and connect with C# and ADO.NET](design-first-database-csharp-tutorial.md)
