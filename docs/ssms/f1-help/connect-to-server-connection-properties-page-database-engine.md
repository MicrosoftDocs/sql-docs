---
title: Connect to Server (Connection Properties) - Database Engine
description: Learn how to use the connect to the server (Connection Properties page) in the database engine to manage your SQL Server connections and settings.
author: erinstellato-ms
ms.author: erinstellato
ms.reviewer: maghan, randolphwest
ms.date: 07/12/2024
ms.service: sql
ms.subservice: ssms
ms.topic: ui-reference
f1_keywords:
  - "sql13.swb.connecttoce.connectionproperties.f1"
  - "sql13.swb.connecttosqlserver.connectionproperties.f1"
---

# Connect to server (Connection Properties page) - Database Engine

[!INCLUDE [SQL Server Azure SQL Database Synapse Analytics PDW](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

Use this tab to view or specify options when you connect to an instance of [!INCLUDE [msCoName](../../includes/msconame-md.md)] [!INCLUDE [ssDEnoversion](../../includes/ssdenoversion-md.md)] or when you register a [!INCLUDE [ssDE](../../includes/ssde-md.md)] in **Registered Servers**. **Connect** and **Options>>** only appear in this dialog box when you connect to an instance of the [!INCLUDE [ssDE](../../includes/ssde-md.md)]. **Test** and **Save** only appear in this dialog box when registering [!INCLUDE [ssDE](../../includes/ssde-md.md)]. Access this tab by selecting **Options>>** on the **Login** tab.

[!INCLUDE [entra-id-hard-coded](../../includes/entra-id-hard-coded.md)]

## Connect to database

Select a database to connect to from the list. If you select **\<default>**, you connect to the default database for the server. If you select **\<Browse server>**, you can browse the server for the database to which to connect.

When you connect to an instance of the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Database Engine through [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], you must use [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Authentication and specify a database in the **Connect to Server** dialog box on the **Connection Properties** tab. Ensure that you select the **Encrypt connection** checkbox.

By default, [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] connects to `master`. When you connect to [!INCLUDE [ssSDS](../../includes/sssds-md.md)], if you specify a user database, you only see that database and its objects in Object Explorer. If you connect to `master`, you can see all databases. For more information, see [What is Azure SQL Database?](/azure/sql-database/sql-database-technical-overview).

### Network protocol

Select a protocol from the list. The available client protocols are configured using the Client Network Configuration in Computer Management.

### Network packet size

Enter the size of the network packets to be sent. The default is 4,096 bytes.

### Connection time-out

Enter the number of seconds to wait for a connection to be established before timing out. The default value is 30 seconds.

### Execution time-out

Enter the time in seconds to wait before the execution of a query is completed on the server. The default value is zero seconds, which indicates there's no time-out.

### Encrypt connection

Forces encryption of the connection. When enabled, [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] uses TLS encryption for all the data sent between the client and server. For more information, see [SQL Server and client encryption summary](../../database-engine/configure-windows/sql-server-and-client-encryption-summary.md).

The **Encrypt connection** property appears on the Connection Properties for SSMS 19.x and earlier versions.

### Trust server certificate

When enabled, with **Encrypt connection** also enabled or if the server is configured to force encryption, [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] doesn't validate the server certificate on the client machine when you enable encryption on the network connection between client and server.

The **Trust server certificate** property appears on the Connection Properties page for SSMS 19.x and earlier versions.

### Use custom color

Select to specify the background color for the status bar in a [!INCLUDE [ssDE](../../includes/ssde-md.md)] Query Editor window. To specify the color, choose **Select**. In the **Color** dialog box, select a predefined color from the **Basic Colors** grid or select **Define Custom Colors** to define and use a custom color.

- When you specify a color for a server entry in the **Object Explorer** pane, that color is used when you open a Query Editor window. To open a Query Editor window, right-click the server entry and select **New Query**. Alternatively, when the **Object Explorer** pane is active and focused on this server, you can select **New Query** on the toolbar.

- When you specify a color for a server entry in the **Registered Servers** pane, that color is used when you open a Query Editor window. To open a Query Editor window, right-click the server entry and select **New Query**. Alternatively, when the **Registered Server** pane is active and focused on this server, you can select **New Query** on the toolbar.

- On the **File** menu, when you select **New** > **Database Engine Query**, the color that you specify in the **Connect to Server** dialog box applies to that Query Editor window.

### Reset All

Replace all manually entered connection property values with their defaults.

### Connect

Attempt a connection using the listed values.

### Options

Select to hide the **Connection Properties** and **[Additional Connection Parameters](connect-to-server-additional-connection-parameters-page-database-engine.md)** tabs and view the **Login** tab.

### Test

When you register [!INCLUDE [ssDE](../../includes/ssde-md.md)] in **Registered Servers**, select to test the connection.

### Save

Saves the settings in **Registered Servers**.

## Related content

- [Quickstart: Connect and query a SQL Server instance using SQL Server Management Studio (SSMS)](../quickstarts/ssms-connect-query-sql-server.md)
- [Quickstart: Connect and query a SQL Server instance on an Azure Virtual Machine using SQL Server Management Studio (SSMS)](../quickstarts/ssms-connect-query-sql-server-azure-vm.md)
- [Quickstart: Connect and query an Azure SQL Database or an Azure SQL Managed Instance using SQL Server Management Studio (SSMS)](../quickstarts/ssms-connect-query-azure-sql.md)
- [Quickstart: Connect and query a dedicated SQL pool (formerly SQL DW) in Azure Synapse Analytics with SQL Server Management Studio (SSMS)](../quickstarts/ssms-connect-query-azure-synapse-analytics.md)
- [Quickstart: Use the Azure portal query editor to query Azure SQL Database](/azure/azure-sql/database/connect-query-portal)
