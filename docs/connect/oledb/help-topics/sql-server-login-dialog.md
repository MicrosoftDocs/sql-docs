---
title: "SQL Server Login Dialog Box (OLE DB)"
description: When you attempt to connect without specifying enough information, the OLE DB Driver for SQL Server prompts you with the SQL Server Login dialog box.
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: vanto, randolphwest, davidengel, sunilbs, vbeiranvand
ms.date: 08/26/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: concept-article
ms.custom:
  - sfi-image-nochange
  - ignite-2025
---
# SQL Server Login dialog box (OLE DB)

[!INCLUDE [Driver_OLEDB_Download](../../../includes/driver_oledb_download.md)]

When you attempt to connect without specifying enough information, the OLE DB driver displays the **SQL Server Login** dialog box.

> [!NOTE]  
> SQL Server Login Dialog prompting behavior is controlled by the `DBPROP_INIT_PROMPT` initialization property. For more information, see:
>
> - [Initialization and authorization properties](../ole-db-data-source-objects/initialization-and-authorization-properties.md)
> - [OLE DB Programmer's Guide](/previous-versions/windows/desktop/ms714342(v=vs.85))

:::image type="content" source="../media/sql-server-login-dialog.png" alt-text="Screenshot of SQL Server Login Dialog Box.":::

## Options

### Server

The name of an instance of SQL Server on your network. Select a server\instance name from the list, or type the `<server>\<instance>` name in the **Server** box. Optionally, you can create a server alias on the client computer using **SQL Server Configuration Manager**, and type that name in the **Server** box.

You can enter `(local)` when you're using the same computer as SQL Server. You can then connect to a local instance of SQL Server, even when running a non-networked version of SQL Server.

For more information about server names for different types of networks, see [SQL Server installation guide](../../../database-engine/install-windows/install-sql-server.md).

### Authentication Mode

You can select the following authentication options from the dropdown list:

| Value | Description |
| --- | --- |
| **Windows Authentication** | Authenticate to SQL Server using the currently logged-in user's Windows account credentials. |
| **SQL Server Authentication** | Authenticate using SQL username and password. |
| **Active Directory - Integrated** | Integrated Windows authentication through Microsoft Entra ID. This mode is used for Windows authentication in Active Directory environments federated with Microsoft Entra ID. |
| **Active Directory - Password** | [DEPRECATED] Username and password authentication with a Microsoft Entra identity. |
| **Active Directory - Universal with MFA support** | Interactive authentication with a Microsoft Entra identity. This mode supports Microsoft Entra multifactor authentication. |
| **Active Directory - Service Principal** | Authentication with a Microsoft Entra service principal. **Login ID** should be set to the application (client) ID. **Password** should be set to the application (client) secret. |

### Server SPN

If you use a trusted connection, you can specify a service principal name (SPN) for the server.

### Login ID

Specifies the login ID to use for the connection. The Login ID text box is only enabled if **Authentication Mode** is set to **SQL Server Authentication**, **Active Directory - Password**, **Active Directory - Universal with MFA support**, or **Active Directory - Service Principal**.

### Password

Specifies the password used for the connection. The password text box is only enabled if **Authentication Mode** is set to **SQL Server Authentication**, **Active Directory - Password**, or **Active Directory - Service Principal**.

### Options

Displays or hides the **Options** group. The **Options** button is enabled if **Server** has a value.

### Change Password

When checked, enables **New Password** and **Confirm New Password** text boxes.

### New Password

Specifies the new password.

### Confirm New Password

Specifies the new password a second time, for confirmation.

### Database

Select or type the default database to use on the connection. This setting overrides the default database specified for the login on the server. If no database is specified, the connection uses the default database specified for the login on the server.

### Mirror Server

Specifies the name of the failover partner of the database to be mirrored.

### Mirror SPN

Optionally, you can specify an SPN for the mirror server. The SPN for the mirror server is used for mutual authentication between client and server.

### Language

Specifies the national language to use for SQL Server system messages. The computer running SQL Server must have the language installed. This setting overrides the default language specified for the login on the server. If no language is specified, the connection uses the default language specified for the login on the server.

### Application Name

Specifies the application name to be stored in the `program_name` column in the row for this connection in `sys.sysprocesses`.

### Workstation ID

Specifies the workstation ID to be stored in the `hostname` column in the row for this connection in `sys.sysprocesses`.

### Connection encryption

When **Mandatory** or **Strict**, data that is passed through the connection will be encrypted. The **Strict** option additionally encrypts the PRELOGIN packets. This option is only available for versions 19.x.x.

### Use strong encryption for data

When checked, data that is passed through the connection will be encrypted. This option is only available for versions 18.x.x.

### Trust server certificate

When unchecked, the server's certificate will be validated. Server's certificate must have the correct hostname of the server and issued by a trusted certificate authority.

### Server certificate

Specifies the path to a certificate file to match against the SQL Server TLS/SSL certificate. This option can only be used when **Strict** encryption is enabled.

Enter the full path to the certificate file in the text box labeled **Server certificate**, or select the **...** button to browse for the certificate file. This option is available only in versions 19.2 and later.

### Host name in certificate

The host name to use when validating the SQL Server TLS/SSL certificate. If you don't set this option, the driver uses the server name on the connection URL as the host name to validate the SQL Server TLS/SSL certificate. This option is available only in versions 19 and later.

> [!NOTE]  
> When using **Windows Authentication** or **SQL Server Authentication** modes, **Trust server certificate** is considered only when the **Use strong encryption for data** option is enabled.

## Related content

- [Use Microsoft Entra ID](../features/using-azure-active-directory.md)
- [Universal Data Link (UDL) configuration](data-link-pages.md)
