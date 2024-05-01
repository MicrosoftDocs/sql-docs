---
title: "Connect with SQL Server Management Studio"
description: Explains the connection parameters to connect to an instance of SQL Server with SQL Server Management Studio. Includes details about how to secure and encrypt connections.
author: erinstellato-ms
ms.author: erinstellato
ms.reviewer: maghan, randolphwest
ms.date: 02/29/2024
ms.service: sql
ms.subservice: ssms
ms.topic: ui-reference
---
# Connect with SQL Server Management Studio

This article describes how to connect to the [!INCLUDE [ssdenoversion-md](../../includes/ssdenoversion-md.md)] with SQL Server Management Studio 20 and later versions, for the following products and services:

- [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)]
- [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)]
- [!INCLUDE [ssazuremi-md](../../includes/ssazuremi-md.md)]
- [!INCLUDE [ssazuresynapse-md](../../includes/ssazuresynapse-md.md)]

## Encryption changes

SQL Server Management Studio (SSMS) 20 includes an important security improvement related to connection encryption, and it includes many changes.

- The **Encrypt connection** property has been renamed to **Encryption**.
- The value of *True* (**Encrypt connection** option checked, or enabled) now maps to *Mandatory* for **Encryption**.
- The value of *False* (**Encrypt connection** option unchecked, or disabled) now maps to *Optional* for **Encryption**.
- The **Encryption** property is now set to *Mandatory* by default.
- The **Encryption** property includes a new option, *Strict (SQL Server 2022 and Azure SQL)*, which isn't available in previous versions of SSMS.

In addition, the **Encryption** and **Trust server certificate** properties have moved to the **Login** page under **Connection Security**. A new option, **Host name in certificate**, has also been added. The [best practice](../../relational-databases/security/securing-sql-server.md) is to support a trusted, encrypted connection to the server.

To encrypt connections, the [!INCLUDE [ssdenoversion-md](../../includes/ssdenoversion-md.md)] must be configured with a TLS certificate signed by a trusted root certificate authority.

For users connecting to Azure SQL Database and Azure SQL Managed Instance, existing connections in the most recently used (MRU) list should be updated to use *Strict (SQL Server 2022 and Azure SQL)* encryption.  Any new connection to Azure SQL Database and Azure SQL Managed Instance should use *Strict (SQL Server 2022 and Azure SQL)* encryption. Azure SQL Database and Azure SQL Managed Instance support encrypted connections and are configured with trusted certificates.

With *Strict (SQL Server 2022 and Azure SQL)* encryption selected, the **Trust server certificate** option is unavailable.

With *Mandatory* encryption selected, and **Trust server certificate** enabled (checked), the **Host name in certificate** option is unavailable.

For users connecting to an on-premises SQL Server or SQL Server in a Virtual Machine, the [!INCLUDE [ssde-md](../../includes/ssde-md.md)] must be configured to support encrypted connections. For complete instructions, see [Configure SQL Server Database Engine for encrypting connections](../../database-engine/configure-windows/configure-sql-server-encryption.md).

If the [!INCLUDE [ssde-md](../../includes/ssde-md.md)] has either **Force Encryption** or **Force Strict Encryption** enabled, ensure that you have a certificate from a trusted certificate authority. For more information, see [Certificate requirements for SQL Server](../../database-engine/configure-windows/certificate-requirements.md). If you don't have a certificate from a trusted certificate authority and you try to connect, you see the error:

:::image type="content" source="media/ssms-connect/ssms-connection-error-certificate-chain.png" alt-text="Screenshot of error message when connecting to a SQL Server that doesn't have a certificate from a trusted certificate authority.":::

The text from the screenshot shows an error similar to the following example:

> **Cannot connect to MyServer**
>  
> A connection was successfully established with the server, but then an error occurred during the login process.
> (provider: SSL Provider, error: 0 - > The certificate chain was issued by an authority that is not trusted.)
> (Microsoft SQL Server, Error: -2146893019)
> For help, click: `https://docs.microsoft.com/sql/relational-databases/errors-events/mssqlserver--2146893019-database-engine-error`
>  
> The certificate chain was issued by an authority that is not trusted

For more information, see ["The certificate received from the remote server was issued by an untrusted certificate authority" error when you connect to SQL Server](/troubleshoot/sql/database-engine/connect/error-message-when-you-connect).

## New connections in SQL Server Management Studio 20 and later versions

For workstations with a new installation of SSMS, or when adding a new connection:

1. Select **Connect** in **Object Explorer**, or **File > New > Query with Current Connection**.
1. Within the **Connect to Server** dialog, set the connection details for your server.

## Connection detail description

The following table describes the connection details.

| Setting | Description |
| --- | --- |
| **Server name** | Enter the server name here. For example, `MyServer` or `MyServer\MyInstance` or `localhost`. |
| **Authentication** | Select the type of authentication to use. |
| **User name** | Enter the user name with which to authenticate. The user name is automatically populated if *Windows Authentication* is selected for the **Authentication** type. When you use Microsoft Entra MFA, the user name can be left blank. |
| **Password** | Enter the password for the user (if available). The password field is unavailable for specific **Authentication** types. |
| ***Encryption** <sup>1</sup> | Select the encryption level for the connection. The default value is *Mandatory*. |
| **Trust server certificate** | Check this option to bypass server certificate validation. The default value is *False* (unchecked), which promotes better security using trusted certificates. |
| **Host name in the certificate** | The value provided in this option is used to specify a different, but expected, CN or SAN in the server certificate for the server to which SSMS is connecting. This option can be left blank, so that certificate validation ensures that the Common Name (CN) or Subject Alternate Name (SAN) in the certificate matches the server name to which you're connecting. This parameter can be populated when the server name doesn't match the CN or SAN, for example, when using DNS aliases. For more information, see [Encryption and certificate validation in Microsoft.Data.SqlClient](../../connect/ado-net/encryption-and-certificate-validation.md). |

<sup>1</sup> The value selected by the user represents the desired and minimal level of encryption. SSMS negotiates with the SQL engine (via the driver) to determine the encryption used, and the connection can be established with a different (yet more secure) level of encryption. For example, if you select *Optional* for the **Encryption** option, and the server has **Force Encryption** enabled and a trusted certificate, then the connection is encrypted.

## Connections after upgrading to SQL Server Management Studio 20

During the initial launch of SQL Server Management Studio 20, you're prompted to import settings from an earlier version of SSMS. If you select **Import from SSMS 19** or **Import from SSMS 18**, the list of most recently used connections is imported from the selected version of SSMS.

:::image type="content" source="media/ssms-connect/ssms-import-settings.png" alt-text="Screenshot of message asking to import settings.":::

For workstations that imported settings from SSMS 18 or SSMS 19:

1. Select **Connect** in **Object Explorer**, or **File > New > Query with Current Connection**.
1. Within the **Connect to Server** dialog, select the server to which you want to connect.
1. Review the options selected for **Encryption** and **Trust server certificate** for the existing connection. In some scenarios, it might be necessary to configure a signed certificate or change the value for one or both properties.
1. If you previously connected with **Encryption** set to *Mandatory*, and the SQL Server doesn't have encrypted connections enabled or doesn't have a signed certificate, you're prompted to enable the **Trust server certificate** option:

:::image type="content" source="media/ssms-connect/ssms-prompt-trust-server-certificate.png" alt-text="Screenshot of prompt asking to enable **Trust Server Certificate** for the connection.":::

1. If you select yes, **Trust Server Certificate** is enabled for the connection. You can also enable **Trust Server Certificate** for all connections imported from older versions of SSMS.
1. If you select no, **Trust Server Certificate** isn't enabled, and you aren't able to connect. Review the configuration to verify that a valid server certificate is installed.

For more information, see [Configure SQL Server Database Engine for encrypting connections](../../database-engine/configure-windows/configure-sql-server-encryption.md).

## Remarks

These changes are the result of updates at the driver level in Microsoft.Data.SqlClient. Recent releases of Microsoft.Data.SqlClient offer increased security in the connection options. For more information, see [Introduction to Microsoft.Data.SqlClient namespace](../../connect/ado-net/introduction-microsoft-data-sqlclient-namespace.md).

## Related content

- [Quickstart: Connect and query a SQL Server instance using SQL Server Management Studio (SSMS)](ssms-connect-query-sql-server.md)
- [Quickstart: Connect and query a SQL Server instance on an Azure Virtual Machine using SQL Server Management Studio (SSMS)](ssms-connect-query-sql-server-azure-vm.md)
- [Quickstart: Connect and query an Azure SQL Database or an Azure SQL Managed Instance using SQL Server Management Studio (SSMS)](ssms-connect-query-azure-sql.md)
- [Quickstart: Connect and query a dedicated SQL pool (formerly SQL DW) in Azure Synapse Analytics with SQL Server Management Studio (SSMS)](ssms-connect-query-azure-synapse-analytics.md)
