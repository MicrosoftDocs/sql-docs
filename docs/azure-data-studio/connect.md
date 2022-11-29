---
title: "Connect to a SQL Server instance"
description: Explains the connection parameters to connect to an instance of SQL Server with Azure Data Studio. Includes details about how to secure and encrypt connections.
author: erinstellato-ms
ms.author: erinstellato
ms.reviewer: "maghan"
ms.date: 11/15/2022
ms.service: azure-data-studio
ms.topic: "how-to"
ms.custom:
---

# Connect with Azure Data Studio

This article describes how to connect to SQL Server with Azure Data Studio 1.40 and higher.

## Encrypt and Trust server certificate

Azure Data Studio 1.40 and later includes an important change to the **Encrypt** property, which is now enabled (set to **True**) by default for MSSQL provider connections, and SQL Server must be configured with TLS certificates signed by a trusted root certificate authority. In addition, the **Encrypt** and **Trust server certificate** properties have moved from the **Advanced** pane to the front of the **Connection Details** pane. Both properties have information icons to provide more detail on hover. The [best practice](../relational-databases/security/securing-sql-server.md) is to support a trusted encrypted connection to the server.

For users connecting to Azure SQL Database, no changes to existing, saved connections in Azure Data Studio are needed; Azure SQL Database supports encrypted connections and is configured with trusted certificates. 

For users connecting to on-premises SQL Server, or SQL Server in a Virtual Machine, if **Encrypt** is set to **True**, ensure that you have a certificate from a trusted certificate authority (e.g. not a self-signed certificate). Alternatively, you may choose to connect without encryption (**Encrypt** set to **False**), or to trust the server certificate (**Encrypt** set to **True** and **Trust server certificate** set to **True**).   

## New installation of Azure Data Studio 1.40 and higher

For workstations with a new installation:

1. Select **New Connection** on the **Welcome** page to open the **Connection** pane.
2. From the **Connection** pane, select  **Servers** > **New Connection**.

   Azure Data Studio shows **Connection Details**.

:::image type="content" source="connect/connect-connection-details.png" alt-text="Screenshot of Connection Details.":::

3. Set the connection details for your server.

## Connection detail description

- **Server Name:** Enter server name here. For example, localhost.
- **Authentication Type:** SQL Login
- **User name:** User name for the SQL Server
- **Password:** Password for the SQL Server
- **Encrypt:** Default value is True
- **Trust server certificate:** Default value is False
- **Database Name:** \<Default\>
- **Server Group:** \<Default\>

## Connections after upgrading to Azure Data Studio 1.40 and higher

After upgrading to Azure Data Studio 1.40 and higher, the initial launch of the application will display the following message:

:::image type="content" source="connect/connect-import-update-after-upgrade.png" alt-text="Screenshot of important update message after upgrading Azure Data Studio.":::

You should review the options selected for **Encrypt** and **Trust server certificate** for existing connections in Azure Data Studio before connecting. In some scenarios, it may be necessary to configure a signed certificate, or change the value for one or both properties.  

## More information

These changes are the result of updates at the driver level in Microsoft.Data.SqlClient. Recent releases of Microsoft.Data.SqlClient have offered increased security in the connection options. Read more in the [release notes](../connect/ado-net/introduction-microsoft-data-sqlclient-namespace.md) for Microsoft.Data.SqlClient.

If you have previously been connecting to a SQL Server that does not have encrypted connections enable and would like to enable encryption, you will be prompted to trust server certificate. You may choose to connect with the 'Trust Server Certificate' property enabled, or cancel and review client configuration to verify a valid server certificate is installed. For more information, please visit the [SQL Server documentation](../database-engine/configure-windows/enable-encrypted-connections-to-the-database-engine.md).

## Next steps

- [Quickstart: Use Azure Data Studio to connect and query SQL Server](quickstart-sql-server.md)