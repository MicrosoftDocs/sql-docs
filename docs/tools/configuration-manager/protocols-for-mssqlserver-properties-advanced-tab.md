---
title: "Protocols for MSSQLSERVER Properties (Advanced Tab)"
description: Learn about the benefits and requirements of the Extended Protection for Authentication for the SQL Server Database Engine. See how to enable and configure it.
ms.custom: seo-lt-2019
ms.date: 01/22/2021
ms.service: sql
ms.reviewer: ""
ms.subservice: tools-other
ms.topic: conceptual
author: markingmyname
ms.author: maghan
monikerRange: ">=sql-server-2016"
---
# Protocols for MSSQLSERVER Properties (Advanced Tab)

[!INCLUDE [SQL Server Windows Only](../../includes/applies-to-version/sql-windows-only.md)]

Use the **Advanced** tab on the **Protocols for MSSQLSERVER Properties** dialog box to configure **Extended Protection for Authentication** for the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssDE](../../includes/ssde-md.md)]. **Extended Protection** is a feature of the network components implemented by the operating system. **Extended Protection** is available in Windows 7 and Windows Server 2008 R2, and is included in service packs for older operating systems. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is more secure when connections are made using **Extended Protection**. Some benefits of **Extended Protection** require **Force Encryption** to be selected on the **Flags** tab.

> [!IMPORTANT]  
> Windows does not enable **Extended Protection** by default. For information about how to enable **Extended Protection**, see the following:
> - [Windows Extended Protection \<extendedProtection\>](/iis/configuration/system.webserver/security/authentication/windowsauthentication/extendedprotection/)
> - [Extended Protection for Authentication Overview](/dotnet/framework/wcf/feature-details/extended-protection-for-authentication-overview)

For more information about how to configure other [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] services, see [Manage the Database Engine Services](../../database-engine/configure-windows/manage-the-database-engine-services.md). For a complete description of Extended Protection, see [Connect to the Database Engine Using Extended Protection](../../database-engine/configure-windows/connect-to-the-database-engine-using-extended-protection.md).

**Extended Protection** is fully supported by the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client beginning with [!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)]. Support for **Extended Protection** for other [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] client providers is not currently supported.

## Options

### Extended Protection

There are three possible values:  

- **Off**: Means **Extended Protection** is disabled. The instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] will accept connections from any client regardless of whether the client is protected or not. **Off** is compatible with older and unpatched operating systems, but is less secure. Only use this setting when you know that the client operating systems do not support extended protection.

- **Allowed**: Means **Extended Protection** is required for connections from operating systems that support **Extended Protection**. Connections from unprotected client applications that are running on protected client operating systems are rejected. **Extended Protection** is ignored for connections from unprotected operating systems. This setting is more secure than **Off**, but is not the most secure setting. Use this setting in mixed environments, where some operating systems or applications support **Extended Protection** and some do not.

- **Required**: Means that for a connection to be accepted, it must come from a protected application on a protected operating system. This setting is the most secure of the three options. But connections from operating systems that do not support **Extended Protection** will not be able to connect to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].

### Accepted NTLM SPNs

An instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] can be identified by more than one NTLM service principal name (SPN). You list the SPNs as a series of strings separated by semicolons. For example, the value **MSSQLSvc/HostName1.Contoso.com;MSSQLSvc/HostName2.Contoso.com**, indicates that clients attempting to connect to SPNs named **MSSQLSvc/HOST1.Contoso.com** or **MSSQLSvc/HOST2.Contoso.com** are allowed. The variable has a maximum length of 2048 characters.

## See Also

[Extended Protection for Authentication with Reporting Services](../../reporting-services/security/extended-protection-for-authentication-with-reporting-services.md)