---
title: Azure portal query editor
titleSuffix: Azure SQL Database
description: Learn how to run T-SQL queries all from within the browser via the Azure portal query editor for Azure SQL Database.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: ivujic, mathoma
ms.date: 04/15/2024
ms.service: sql-database
ms.subservice: development
ms.topic: conceptual
ms.custom:
  - sqldbrb=1
  - mode-ui
  - kr2b-contr-experiment
keywords:
  - connect to sql database
  - query sql database
  - azure portal
  - portal
  - query editor
monikerRange: "=azuresql||=azuresql-db"
---
# Azure portal query editor for Azure SQL Database
[!INCLUDE [appliesto-sqldb](../includes/appliesto-sqldb.md)]

The Query editor (preview) is a tool to run T-SQL queries in the Azure portal in the browser against Azure SQL Database.

- For a quickstart on the Azure portal query editor, see [Quickstart: Use the Azure portal query editor (preview)](connect-query-portal.md).
- For more advanced object explorer capabilities and management functions, use [Azure Data Studio](/azure-data-studio/quickstart-sql-database) or [SQL Server Management Studio (SSMS)](connect-query-ssms.md).
- For more information about Microsoft Copilot in Azure SQL Database (preview), see [Copilot in Azure SQL Database (preview)](../copilot/copilot-azure-sql-overview.md). For a tutorial and examples, visit [Natural language to SQL in the Azure portal query editor (preview)](../copilot/query-editor-natural-language-to-sql-copilot.md).
- If you don't already have an Azure SQL Database created, visit [Quickstart: Create a single database](single-database-create-quickstart.md). Look for the option to use your offer to [try Azure SQL Database for free (preview)](free-offer.md).

## Query your Azure SQL Database from the Azure portal

The query editor is designed for lightweight querying and object exploration in your Azure SQL database, all from within the browser in the Azure portal. You can run T-SQL queries against your database, as well as edit data in the build-in tabular [data editor](#data-editor).

Similar to the query experience in SQL Server Management Studio, use the query editor for both simple queries or larger T-SQL queries. You can execute Data Manipulation Language (DML) and Data Definition Language (DDL) queries.

> [!TIP]
> New to Azure SQL Database? Get up to speed with in-depth free training content: [Azure SQL Fundamentals](/training/paths/azure-sql-fundamentals/).

## Connect via the query editor

There are two authentication options for query editor: SQL authentication or authentication with Microsoft Entra ID ([formerly Azure Active Directory](/entra/fundamentals/new-name)).

### Authentication to Azure SQL Database

For examples, see [Quickstart: Use the Azure portal query editor (preview) to query Azure SQL Database](connect-query-portal.md).

- To use SQL Authentication to connect to an Azure SQL database via the query editor, you must have a login in the logical server's `master` database or a contained SQL user in the desired user database. For more information, see [Logins](logins-create-manage.md).
    - Enter your username and password, then select **OK**.
- To use Microsoft Entra authentication to connect to an Azure SQL database via the query editor, your database must be configured with Microsoft Entra authentication, and you must have a [Microsoft Entra user created in the database](authentication-azure-ad-logins-tutorial.md).
    - Select **Continue as \<user@domain>**.

### Permissions required to access the query editor

Users need at least the Azure role-based access control (RBAC) permission **Read access to the server and database** to use the query editor.

## Navigate query editor

There are four main sections of the query editor:

- Navigation bar
- Object explorer
- Query window
- Copilot for Azure SQL Database

   :::image type="content" source="media/query-editor/query-editor.png" alt-text="Screenshot from the Azure portal showing red rectangles highlighting the Query editor in the main menu and the Navigation bar, Object Explorer, and Query window." lightbox="media/query-editor/query-editor.png":::

### Navigation bar

There are four tasks you can perform in the navigation bar.

- You can use **Login** to change your authentication context.
- You can use **New Query** to open a blank query window.
- You can use **Open Query** to select up to 10 `.sql` or `.txt` files from your local computer and open them in the query window.
- You can provide **Feedback** on the Azure SQL Database query editor.

### Object explorer

The object explorer allows you to view and perform tasks against your database's tables, views, and stored procedures. 

- Expand **Tables** to view the list of tables in your database. Expand the table to see the columns in the table. Use the ellipses to select the top 1,000 rows, access the [Data editor](#data-editor), or rename the table. If you rename a table, use the refresh arrow to see the changes. 
- Expand **Views** to view a list of views in your database. Expand the view to see the columns in the view. Use the ellipses to select the top 1,000 rows or rename the view. If you rename a view, use the refresh arrow to see the changes.
- Expand **Stored Procedures** to view a list of all stored procedures in your database. Expand a stored procedure to see the output of the stored procedure. Use the ellipses to view the definition of the stored procedure in the query window.

### Query window

This window allows you to type or paste a query, then run it. The results of the query are shown in the **Results** pane.

You can cancel your query. As noted under [Considerations and limitations](#considerations-and-limitations), there's a five-minute timeout period. 

The **Save query** button allows you to save the query text to your computer as a *.sql* file.

The **Export data as** button allows you to export the query results to your computer as a *.json*, *.csv*, or *.xml* file.

The query execution time, or errors, are shown in the status bar.

### Copilot for Azure SQL Database (preview)

[Copilot in Azure SQL Database](../copilot/copilot-azure-sql-overview.md) is an AI assistant designed to streamline the design, operation, optimization, and health of Azure SQL Database-driven applications. It improves productivity in the Azure portal by offering natural language to SQL conversion and self-help for database administration.

The [Natural language to SQL](../copilot/copilot-azure-sql-overview.md?view=azuresql-db&preserve-view=true#natural-language-to-sql-query) experience within the Azure portal query editor for Azure SQL Database translates natural language queries into SQL, making database interactions more intuitive.

For a tutorial and examples of natural language to SQL capabilities of the Copilot in Azure SQL Database, see [Natural language to SQL in the Azure portal query editor (preview)](../copilot/query-editor-natural-language-to-sql-copilot.md). For more information, see [Frequently asked questions about Copilot in Microsoft Azure SQL (preview)](../copilot/copilot-azure-sql-faq.yml).

## Data editor

The data editor allows you to modify data in an existing row, add a new row of data to the table, or delete a row of data. This is similar to the experience in SQL Server Management Studio (SSMS).

To access data editor, in the object explorer expand **Tables**, then select the ellipses to the right of the table name and select **Edit Data (Preview)**.

**To modify data** in an existing row, select the value you want to change, make your change, and then select **Save** at the top.

- If the column is an identity column, you can't edit that value. You'll see the error message: `Save failed: Failed to execute query. Error: Cannot update identity column *column_name*`.

**To add a new row**, select **Create New Row** and enter the values you want to add. There are certain data types you can't add or work with in this context.

- If the column is an identity column, you can't add a value in that field. You'll see the error message: `Save failed: Cannot set value in identity columns *column_name*`.
- Columns with default constraints aren't honored. The data editor won't generate the default value, it expects you to enter a value. It isn't recommended to use the data editor for tables that have default column constraints.
- Computed columns aren't calculated. You'll see the error message `Save failed: Failed to execute query. Error: The column *column_name* cannot be modified because it is either a computed column or is the result of a UNION operator.` It is not recommended to use the data editor for tables that have computed columns.

**To delete a row** of data, select the row and select **Delete Row**.

- If the row has a primary key, and that primary key has a foreign key relationship to another table, when the row is deleted, the related rows in the other table will also be deleted.

## Considerations and limitations

The following considerations and limitations apply when connecting to and querying Azure SQL Database with the Azure portal query editor.

### Query editor limitations

- If your query has multiple statements, only the results of the last statement are shown in the **Results** tab.
- The query editor doesn't support connecting to the logical server's `master` database. To connect to the `master` database, use [other tools to query your Azure SQL Database](#other-ways-to-query-your-azure-sql-database).
- The query editor can't connect to a [replica database](read-scale-out.md) with `ApplicationIntent=ReadOnly`. To connect in this way, use SSMS and specify `ApplicationIntent=ReadOnly` on the **Additional Connection Parameters** tab in connection options. For more information, see [Connect to a read-only replica](/sql/database-engine/availability-groups/windows/listeners-client-connectivity-application-failover#ConnectToSecondary).
- The query editor has a 5-minute timeout for query execution. To run longer queries, use [other tools to query your Azure SQL Database](#other-ways-to-query-your-azure-sql-database).
- The query editor only supports cylindrical projection for geography data types.
- The query editor doesn't support IntelliSense for database tables and views, but supports autocomplete for names that have already been typed. For IntelliSense support, use [other tools to query your Azure SQL Database](#other-ways-to-query-your-azure-sql-database).
- Pressing **F5** refreshes the query editor page, and any query currently in the editor isn't saved.

### Other ways to query your Azure SQL Database

In addition to the Azure portal query editor for Azure SQL Database, consider the following quickstarts for other tools:

- [Quickstart: Use Azure Data Studio to connect and query Azure SQL Database](/azure-data-studio/quickstart-sql-database)
- [Quickstart: Use SSMS to connect to and query Azure SQL Database or Azure SQL Managed Instance](connect-query-ssms.md)
- [Quickstart: Use Visual Studio Code to connect and query](connect-query-vscode.md)

## Connection considerations

- For public connections to the query editor, you need to [add your outbound IP address to the server's allowed firewall rules](firewall-create-server-level-portal-quickstart.md) to access your databases.
    - You don't need to add your IP address to the SQL server firewall rules if you have a Private Link connection set up on the server, and you connect to the server from within the private virtual network.

### Connection error troubleshooting

- If you see the error message `The X-CSRF-Signature header could not be validated`, take the following actions to resolve the issue:

  - Verify that your computer's clock is set to the right time and time zone. You can try to match your computer's time zone with Azure by searching for the time zone for your database location, such as East US.
  - If you're on a proxy network, make sure that the request header `X-CSRF-Signature` isn't being modified or dropped.

- If your database is serverless and you see the error message:
    `Database *name* on server *name.database.windows.net* is not currently available. Please retry the connection later. If the problem persists, contact customer support, and provide them the session tracing ID *ID*`
    This indicates your serverless database is currently paused. If this occurs, selecting `Continue as <user@domain>` sends a request to the database to resume. Wait approximately one minute, refresh the page, and try again.

- If you see the error message "Login failed for user `<token-identified principal>`. The server is not currently configured to accept this token." when you attempt to use AD authentication, your user does not have access to the database.

  - For more information on creating a database user from a Microsoft Entra principal, see [Configure and manage Microsoft Entra authentication with Azure SQL](authentication-aad-configure.md) and use `CREATE USER [group or user] FROM EXTERNAL PROVIDER` in the user database.

### Port 443 and 1443 connectivity

You might get one of the following errors in the query editor:

- `Your local network settings might be preventing the Query Editor from issuing queries. Please click here for instructions on how to configure your network settings.`
- `A connection to the server could not be established. This might indicate an issue with your local firewall configuration or your network proxy settings.`

These errors occur because the query editor is unable to communicate through ports 443 and 1443. You need to enable outbound HTTPS traffic on these ports. The following instructions walk you through this process, depending on your OS. Your corporate IT department might need to grant approval to open this connection on your local network.

#### Allow 443 and 1443 in Windows Defender Firewall

1. Open **Windows Defender Firewall**.
1. On the left menu, select **Advanced settings**.
1. In **Windows Defender Firewall with Advanced Security**, select **Outbound rules** on the left menu.
1. Select **New Rule** on the right menu.
1. In the **New outbound rule wizard**, follow these steps:
   1. Select **port** as the type of rule you want to create, and then select **Next**.
   1. Select **TCP**.
   1. Select **Specific remote ports**, enter `443, 1443`, and then select **Next**.
   1. Select **Allow the connection if it is secure**, select **Next**, and then select **Next** again.
   1. Keep **Domain**, **Private**, and **Public** selected.
   1. Give the rule a name, for example *Access Azure SQL query editor*, and optionally provide a description. Then select **Finish**.

#### Allow 443 and 1443 in macOS

1. On the Apple menu, open **System Preferences**.
1. Select **Security & Privacy**, and then select **Firewall**.
1. If **Firewall** is off, select **Click the lock to make changes**, and select **Turn on Firewall**.
1. Select **Firewall Options**.
1. In the **Security & Privacy** window, select **Automatically allow signed software to receive incoming connections**.

#### Allow 443 and 1443 in Linux

Run these commands to update `iptables`:

```bash
sudo iptables -A OUTPUT -p tcp --dport 443 -j ACCEPT
sudo iptables -A OUTPUT -p tcp --dport 1443 -j ACCEPT
```

#### Allow 443 and 1443 in Azure VM

When using Azure VMs, you have an [Azure network security group](/azure/virtual-network/network-security-group-how-it-works) blocking connectivity. A network security group can filter inbound and outbound network traffic to and from Azure resources in an Azure virtual network. You need to add an [outbound security rule](/azure/virtual-network/network-security-groups-overview#security-rules) to the network security group. For an example, see [Create security rules](/azure/virtual-network/tutorial-filter-network-traffic#create-security-rules).

## Next step

> [!div class="nextstepaction"]
> [Quickstart: Use the Azure portal query editor to query Azure SQL Database](connect-query-portal.md)

## Related content

- [What is Azure SQL?](../azure-sql-iaas-vs-paas-what-is-overview.md)
- [Azure SQL glossary of terms](../glossary-terms.md)
- [T-SQL differences between SQL Server and Azure SQL Database](transact-sql-tsql-differences-sql-server.md)
