---
title: "Protocols for MSSQLSERVER Properties (Advanced Tab)"
description: Learn about the benefits and requirements of the Extended Protection for Authentication for the SQL Server Database Engine. See how to enable and configure it.
author: rwestMSFT
ms.author: randolphwest
ms.date: 12/15/2025
ms.service: sql
ms.subservice: tools-other
ms.topic: ui-reference
ms.collection:
  - data-tools
monikerRange: ">=sql-server-2017"
---

# Protocols for MSSQLSERVER Properties (Advanced tab)

[!INCLUDE [SQL Server Windows Only](../../includes/applies-to-version/sql-windows-only.md)]

Use the **Advanced** tab on the **Protocols for MSSQLSERVER Properties** dialog box to configure **Extended Protection for Authentication** for the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE [ssDE](../../includes/ssde-md.md)]. **Extended Protection** is a feature of the network components implemented by Windows. [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] is more secure when connections are made using **Extended Protection**. Some benefits of **Extended Protection** require **Force Encryption** to be selected on the **Flags** tab.

> [!IMPORTANT]  
> Windows doesn't enable **Extended Protection** by default. For information about how to enable **Extended Protection**, see the following:
>
> - [Windows Extended Protection \<extendedProtection\>](/iis/configuration/system.webserver/security/authentication/windowsauthentication/extendedprotection/)
> - [Extended Protection for Authentication Overview](/dotnet/framework/wcf/feature-details/extended-protection-for-authentication-overview)

For more information about how to configure other [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] services, see [Manage the Database Engine services](../../database-engine/configure-windows/manage-the-database-engine-services.md). For a complete description of Extended Protection, see [Connect to the database engine with Extended Protection](../../database-engine/configure-windows/connect-to-the-database-engine-using-extended-protection.md).

## Options

### Extended Protection

There are three possible values:

- **Off**: Means **Extended Protection** is disabled. The instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] accepts connections from any client regardless of whether the client is protected or not. **Off** is compatible with older and unpatched operating systems, but is less secure. Only use this setting when you know that the client operating systems don't support extended protection.

- **Allowed**: Means **Extended Protection** is required for connections from operating systems that support **Extended Protection**. Connections from unprotected client applications that are running on protected client operating systems are rejected. **Extended Protection** is ignored for connections from unprotected operating systems. This setting is more secure than **Off**, but isn't the most secure setting. Use this setting in mixed environments, where some operating systems or applications support **Extended Protection** and some don't.

- **Required**: Means that for a connection to be accepted, it must come from a protected application on a protected operating system. This setting is the most secure of the three options. But connections from operating systems that don't support **Extended Protection** won't be able to connect to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].

### Accepted NTLM SPNs

An instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] can be identified by more than one NTLM service principal name (SPN). You list the SPNs as a series of strings separated by semicolons. For example, the value `MSSQLSvc/HostName1.Contoso.com;MSSQLSvc/HostName2.Contoso.com`, indicates that clients attempting to connect to SPNs named `MSSQLSvc/HOST1.Contoso.com` or `MSSQLSvc/HOST2.Contoso.com` are allowed. The variable has a maximum length of 2048 characters.

## Related content

- [Extended protection for authentication with Reporting Services](../../reporting-services/security/extended-protection-for-authentication-with-reporting-services.md)
