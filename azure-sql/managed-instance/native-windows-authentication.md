---
title: Native Windows authentication
titleSuffix: Azure SQL Managed Instance
description: Learn about the new authentication cache modes for Azure SQL Managed Instance, which allows you to use Windows authentication with Azure SQL Managed Instance.
author: sravanisaluru
ms.author: srsaluru
ms.reviewer: mathoma, vanto, wiassaf
ms.date: 07/03/2024
ms.service: sql-managed-instance
ms.subservice: security
ms.topic: conceptual
---


# Native Windows authentication

The following **Authentication Cache Modes** are available for Azure SQL Managed Instance:

- **Windows** (New Mode): This mode allows authentication using existing Windows users within SQL Managed Instance.
- **Microsoft Entra ID** (SQL Managed Instance default): This mode allows authentication using Microsoft Entra principals using Windows user metadata.
- **Paired** (SQL Server default): The default mode for SQL Server authentication.

The **Windows** authentication cache mode is a new mode that allows users to use Windows authentication (using a Windows principal) with Azure SQL Managed Instance. This mode is available for Azure SQL Managed Instance only. The **Windows** authentication cache mode isn't available for Azure SQL Database.

The syntax `CREATE LOGIN FROM WINDOWS` and `CREATE USER FROM WINDOWS` can be used to create a login or user in SQL Managed Instance, respectively for a Windows principal in the **Windows** authentication cache mode. The Windows principal can be a Windows user or a Windows group.

In order to use the **Windows** authentication cache mode, the user environment must [Synchronize Active Directory (AD) with Microsoft Entra ID](winauth-azuread-setup.md#synchronize-ad-with-microsoft-entra-id).

## Addressing migration challenges using Windows authentication cache mode

The **Windows** authentication cache mode helps modernize authentication for application, and unblocks migration challenges to SQL Managed Instance. Here are some common scenarios where the **Windows** authentication cache mode can be used to address customer challenges:

- The complexities of setting up [Windows Authentication for Azure SQL Managed Instance using Microsoft Entra ID and Kerberos](winauth-azuread-setup.md).
- Read-only replica failovers in [Managed Instance link](managed-instance-link-feature-overview.md).
- Synchronization of [Microsoft Entra authentication for SQL Server](/sql/relational-databases/security/authentication-access/azure-ad-authentication-sql-server-overview).

### Windows authentication for Microsoft Entra principals

As long as the environment is synchronized between AD and Microsoft Entra ID, the **Windows** authentication cache mode can be used to authenticate users to SQL Managed Instance using a Windows login. The user needs a login (created from a Windows or Microsoft Entra principal that is synched) in SQL Managed Instance to authenticate using Windows authentication.

This feature is especially useful for customers who have applications that use Windows authentication and are migrating to Azure SQL Managed Instance. The **Windows** authentication cache mode allows customers to continue using Windows authentication for their applications without having to make any changes to the application code. Applications like BizTalk server, which runs `CREATE LOGIN FROM WINDOWS` and `CREATE USER FROM WINDOWS` commands, can continue to work without any changes when migrating to Azure SQL Managed Instance.

### Managed Instance link

Although Managed Instance link enables near real-time data replication between SQL Server and Azure SQL Managed Instance, the *read-only* replica in the cloud prevents creation of Microsoft Entra principals. The **Windows** authentication cache mode allows customers to use an existing Windows login to authenticate to the replica if a failover happens.

### Microsoft Entra authentication for SQL Server 2022 and later

SQL Server 2022 introduces support for Microsoft Entra authentication. Many users would like to limit the authentication modes to only utilize modern authentication, and migrate all Windows principals to Microsoft Entra ID. However, there are scenarios where Windows authentication is still required, such as application code tied to Windows principals. The **Windows** authentication cache mode allows customers to continue using Windows principals for authorization within SQL Server, while using Microsoft Entra principals that are synced for authentication.

SQL Server doesn't understand the synchronization between Active Directory and Microsoft Entra ID. Although users and groups are synchronized between AD and Microsoft Entra ID, you still had to create a login using the syntax `CREATE LOGIN FROM EXTERNAL PROVIDER` and add permissions to the login. The **Windows** authentication cache mode alleviates the need to manually migrate logins to Microsoft Entra ID.

## Authentication cache mode comparison

The authentication for SQL Managed Instance and SQL Server is based on metadata for logins. For Windows authentication logins, the metadata is created when the login is created from the `CREATE LOGIN FROM WINDOWS` command. For Microsoft Entra logins, the metadata is created when the login is created from the `CREATE LOGIN FROM EXTERNAL PROVIDER` command. For SQL authentication logins, the metadata is created when the `CREATE LOGIN WITH PASSWORD` command is executed. The authentication process is tightly coupled with the metadata stored in SQL Managed Instance or SQL Server.

Here's the flow chart that explains how the authentication cache mode works with SQL Managed Instance:

:::image type="content" source="media/authentication-cache-mode-flow.png" alt-text="Diagram of the authentication cache mode flowchart.":::

Previously, customers who synchronize users between AD and Microsoft Entra ID wouldn't be able to authenticate with a login created from a Windows principal, whether they used Windows authentication or Microsoft Entra authentication that was synced from AD. With the **Windows** authentication cache mode, customers can now authenticate with a login created from a Windows principal using Windows authentication or the synchronized Microsoft Entra principal.

For synchronized users, the authentication fails or works based on the following configurations and login type:

| Windows mode | FROM WINDOWS | FROM EXTERNAL PROVIDER |
|----------|----------|----------|
| Microsoft Entra authentication    | Works     | Fails     |
| Windows authentication    | Works     | Fails     |

| Microsoft Entra ID mode | FROM WINDOWS | FROM EXTERNAL PROVIDER |
|----------|----------|----------|
| Microsoft Entra authentication    | Fails     | Works     |
| Windows authentication    | Fails     | Works     |

| Paired mode | FROM WINDOWS | FROM EXTERNAL PROVIDER |
|----------|----------|----------|
| Microsoft Entra authentication    | Fails     | Works     |
| Windows authentication    | Works     | Fails     |

## Related content

Learn more about implementing Windows Authentication for Microsoft Entra principals on Azure SQL Managed Instance:

- [What is Windows Authentication for Microsoft Entra principals on Azure SQL Managed Instance?](winauth-azuread-overview.md)
- [How Windows Authentication for Azure SQL Managed Instance is implemented with Microsoft Entra ID and Kerberos.](winauth-implementation-aad-kerberos.md)
- [How to set up Windows Authentication for Microsoft Entra ID with the modern interactive flow.](winauth-azuread-setup-modern-interactive-flow.md)
- [How to set up Windows Authentication for Microsoft Entra ID with the incoming trust-based flow.](winauth-azuread-setup-incoming-trust-based-flow.md)
- [Configure Azure SQL Managed Instance for Windows Authentication for Microsoft Entra ID.](winauth-azuread-kerberos-managed-instance.md)
