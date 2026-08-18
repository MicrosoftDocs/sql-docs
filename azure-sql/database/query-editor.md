---
title: Azure Portal Query Editor
titleSuffix: Azure SQL Database
description: Learn how to run T-SQL queries all from within the browser via the Azure portal query editor for Azure SQL Database.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: ivujic, mathoma
ms.date: 03/11/2026
ms.service: azure-sql-database
ms.subservice: development
ms.topic: concept-article
ms.collection:
  - ce-skilling-ai-copilot
ms.custom:
  - sqldbrb=1
  - mode-ui
  - kr2b-contr-experiment
  - sfi-image-nochange
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

- For a quickstart on the Azure portal query editor, see [Quickstart: Use the Azure portal query editor](connect-query-portal.md).
- For more advanced object explorer capabilities and management functions, use [SQL Server Management Studio (SSMS)](connect-query-ssms.md) or the [MSSQL extension for Visual Studio Code](/sql/tools/visual-studio-code-extensions/mssql/mssql-extension-visual-studio-code).
- If you don't already have an Azure SQL Database created, visit [Quickstart: Create a single database](single-database-create-quickstart.md). Look for the option to use your offer to [Deploy Azure SQL Database for free](free-offer.md).

## Query your Azure SQL Database from the Azure portal

The query editor is designed for lightweight querying and object exploration in your Azure SQL database, all from within the browser in the Azure portal. 

Similar to the query experience in SQL Server Management Studio, use the query editor for both simple queries or larger T-SQL queries. You can execute Data Manipulation Language (DML) and Data Definition Language (DDL) queries.

> [!TIP]
> New to Azure SQL Database? Get up to speed with in-depth free training content: [Azure SQL Fundamentals](/training/paths/azure-sql-fundamentals/).

## Connect via the query editor

You can authenticate to the query editor by using SQL authentication or Microsoft Entra authentication.

### Authentication to Azure SQL Database

For examples, see [Quickstart: Use the Azure portal query editor](connect-query-portal.md).

- To use **SQL authentication** to connect to an Azure SQL database via the query editor, you must have a login in the logical server's `master` database or a contained SQL user in the desired user database. For more information, see [Logins](logins-create-manage.md).
    - Enter your username and password, and then select **OK**.
- To use **Microsoft Entra authentication** to connect to an Azure SQL database via the query editor, your database must be configured with Microsoft Entra authentication, and you must have a [Microsoft Entra user created in the database](authentication-azure-ad-logins-tutorial.md).
    - Select **Continue as \<user@domain>**.

<a id="object-explorer"></a>

## Navigate query editor

Once connected to the query editor experience, you can use the Explorer to view database objects or the Query window to execute T-SQL queries. 

   :::image type="content" source="media/query-editor/query-editor.png" alt-text="Screenshot from the Azure portal showing red rectangles highlighting the Query editor in the main menu and the navigation bar, Explorer, tool bar, and query window." lightbox="media/query-editor/query-editor.png":::

Helpful tips:

- The toolbar contains **Templates** for new T-SQL objects.
- The **Open in** dropdown list lets you launch a connection to this database in [SQL Server Management Studio (SSMS)](connect-query-ssms.md?view=azuresql-db&preserve-view=true) or [Visual Studio Code and the MSSQL extension](connect-query-vscode.md?view=azuresql-db&preserve-view=true). This action launches the **Connection Dialogue** in Visual Studio Code.
- To use the [older version of the Azure portal SQL query editor](query-editor-classic.md), select **Classic experience**.

### Query window

This Query window lets you type or paste a query and run it. The **Results** pane shows the query results.

- There's a five-minute query timeout.  
- Use the **Download as** buttons to export the query results to your computer as a *.csv*, *.json*, or *.xlsx* file.
- The status bar shows the query execution time or any errors.
- You can copy or filter the result sets in the **Results** toolbar.

## Considerations and limitations

The following considerations and limitations apply when connecting to and querying Azure SQL Database by using the Azure portal query editor.

### Query editor limitations

- If your query has multiple statements, the **Results** tab shows only the results of the last statement.
- The query editor doesn't support connecting to the logical server's `master` database. To connect to the `master` database, use [SQL Server Management Studio (SSMS)](connect-query-ssms.md) or the [MSSQL extension for Visual Studio Code](/sql/tools/visual-studio-code-extensions/mssql/mssql-extension-visual-studio-code).
- The query editor can't connect to a [replica database](read-scale-out.md) by using `ApplicationIntent=ReadOnly`. To connect in this way, use SSMS and specify `ApplicationIntent=ReadOnly` on the **Additional Connection Parameters** tab in connection options. For more information, see [Connect to a read-only replica](/sql/database-engine/availability-groups/windows/listeners-client-connectivity-application-failover#ConnectToSecondary).
- The query editor has a five-minute timeout for query execution. To run longer queries, use [SQL Server Management Studio (SSMS)](connect-query-ssms.md) or the [MSSQL extension for Visual Studio Code](/sql/tools/visual-studio-code-extensions/mssql/mssql-extension-visual-studio-code).
- The query editor only supports cylindrical projection for geography data types.
- The query editor doesn't support IntelliSense for columns, but it does support tables and views. For full IntelliSense support, use [SQL Server Management Studio (SSMS)](connect-query-ssms.md) or the [MSSQL extension for Visual Studio Code](/sql/tools/visual-studio-code-extensions/mssql/mssql-extension-visual-studio-code).
- While in same browser session, you can freely navigate through the Azure portal and your queries will be persisted. If you refresh the page (by pressing **F5**) or close your browser, you will lose your queries.

## Connection considerations

- For public connections to the query editor, you need to [add your outbound IP address to the server's allowed firewall rules](firewall-create-server-level-portal-quickstart.md) to access your databases.
    - You don't need to add your IP address to the SQL server firewall rules if you have a Private Link connection set up on the server, and you connect to the server from within the private virtual network.
    - For troubleshooting, see [Connection error troubleshooting](query-editor.md#connection-considerations).
    - For more information about public network access, TLS version settings, and connection policy, see [Azure SQL connectivity settings](connectivity-settings.md?view=azuresql-db&preserve-view=true).  

### Connection error troubleshooting

- If you see the error message `The X-CSRF-Signature header could not be validated`, take the following actions to resolve the issue:

  - Verify that your computer's clock is set to the right time and time zone. You can try to match your computer's time zone with Azure by searching for the time zone for your database location, such as East US.
  - If you're on a proxy network, make sure that the request header `X-CSRF-Signature` isn't being modified or dropped.

- If your database is serverless and you see the error message:
    `Database *name* on server *name.database.windows.net* is not currently available. Please retry the connection later. If the problem persists, contact customer support, and provide them the session tracing ID *ID*`
        This error message indicates your serverless database is currently paused. If this error message appears, selecting `Continue as <user@domain>` sends a request to the database to resume. Wait approximately one minute, refresh the page, and try again.

- If you see the error message "Login failed for user `<token-identified principal>`. The server is not currently configured to accept this token." when you attempt to use AD authentication, your user doesn't have access to the database.

  - For more information on creating a database user from a Microsoft Entra principal, see [Configure and manage Microsoft Entra authentication with Azure SQL](authentication-aad-configure.md) and use `CREATE USER [group or user] FROM EXTERNAL PROVIDER` in the user database.

### Port 443 connectivity

Starting in March 2026, the Azure SQL query editor uses only TCP port 443.

You might get one of the following errors in the query editor:

- `Your local network settings might be preventing the Query Editor from issuing queries. Please click here for instructions on how to configure your network settings.`
- `A connection to the server could not be established. This might indicate an issue with your local firewall configuration or your network proxy settings.`

These errors occur because the query editor needs to communicate through port 443 but can't. You need to enable outbound HTTPS traffic on this port. The following instructions walk you through this process, depending on your OS. Your corporate IT department might need to grant approval to open this connection on your local network.

#### Allow 443 in Windows Defender Firewall

1. Open **Windows Defender Firewall**.
1. On the left menu, select **Advanced settings**.
1. In **Windows Defender Firewall with Advanced Security**, select **Outbound rules** on the left menu.
1. Select **New Rule** on the right menu.
1. In the **New outbound rule wizard**, follow these steps:
   1. Select **port** as the type of rule you want to create, and then select **Next**.
   1. Select **TCP**.
   1. Select **Specific remote ports**, enter `443`, and then select **Next**.
   1. Select **Allow the connection if it is secure**, select **Next**, and then select **Next** again.
   1. Keep **Domain**, **Private**, and **Public** selected.
   1. Give the rule a name, for example *Access Azure SQL query editor*, and optionally provide a description. Then select **Finish**.

#### Allow 443 in macOS

1. On the Apple menu, open **System Preferences**.
1. Select **Security & Privacy**, and then select **Firewall**.
1. If **Firewall** is off, select **Click the lock to make changes**, and select **Turn on Firewall**.
1. Select **Firewall Options**.
1. In the **Security & Privacy** window, select **Automatically allow signed software to receive incoming connections**.

#### Allow 443 in Linux

Run these commands to update `iptables`:

```bash
sudo iptables -A OUTPUT -p tcp --dport 443 -j ACCEPT
```

#### Allow 443 in Azure VM

When you use Azure VMs, an [Azure network security group](/azure/virtual-network/network-security-group-how-it-works) blocks connectivity. A network security group can filter inbound and outbound network traffic to and from Azure resources in an Azure virtual network. You need to add an [outbound security rule](/azure/virtual-network/network-security-groups-overview#security-rules) to the network security group. For an example, see [Create security rules](/azure/virtual-network/tutorial-filter-network-traffic#create-security-rules).

## Next step

> [!div class="nextstepaction"]
> [Quickstart: Use the Azure portal query editor](connect-query-portal.md)

## Related content

- [What is Azure SQL?](../azure-sql-iaas-vs-paas-what-is-overview.md)
- [Azure SQL glossary of terms](../glossary-terms.md)
- [T-SQL differences between SQL Server and Azure SQL Database](transact-sql-tsql-differences-sql-server.md)
