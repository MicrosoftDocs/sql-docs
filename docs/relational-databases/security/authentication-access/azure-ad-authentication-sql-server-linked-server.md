---
title: "Linked server for SQL Server with Azure Active Directory authentication"
description: Learn about how to use linked server for SQL Server with Azure Active Directory authentication
author: GithubMirek
ms.author: mireks
ms.reviewer: vanto, randolphwest
ms.date: 07/25/2022
ms.service: sql
ms.subservice: security
ms.topic: conceptual
ms.custom: event-tier1-build-2022
monikerRange: ">=sql-server-ver15||>= sql-server-linux-ver16"
---

# Linked server for SQL Server with Azure Active Directory authentication

[!INCLUDE [SQL Server 2022](../../../includes/applies-to-version/sqlserver2022.md)]

[Linked servers](../../linked-servers/linked-servers-database-engine.md) can now be configured with [Azure Active Directory (Azure AD) authentication](azure-ad-authentication-sql-server-overview.md), and it supports two mechanisms for providing credentials:

- Password
- Access token

For this article, it's assumed that there are two SQL Server instances (`S1` and `S2`). Both have been configured to support Azure AD authentication, and they trust each other's SSL/TLS certificate. The examples below will be run on server `S1` to create a linked server to server `S2`.

> [!NOTE]
> The subject name of the SSL/TLS certificate used by `S2` must match the server name provided in the [`provstr`](../../system-stored-procedures/sp-addlinkedserver-transact-sql.md) attribute. This should either be the Fully Qualified Domain Name (**FQDN**) or **hostname** of `S2`.

## Prerequisites

- Fully operational Azure AD authentication for SQL Server. For more information, see [Azure Active Directory authentication for SQL Server](azure-ad-authentication-sql-server-overview.md) and [Tutorial: Set up Azure Active Directory authentication for SQL Server](azure-ad-authentication-sql-server-setup-tutorial.md).
- [SQL Server Management Studio (SSMS)](../../../ssms/download-sql-server-management-studio-ssms.md) version 18.0 or higher. Or download the latest [Azure Data Studio](../../../azure-data-studio/download-azure-data-studio.md).

## Linked server configurations for Azure AD authentication

We'll go over configuring linked servers using password authentication and using an Azure application secret or access token.

### Linked server configuration using password authentication

For password authentication, using `Authentication=ActiveDirectoryPassword` in the **Provider string** will signal the linked server to use Azure AD password authentication. A linked server login must be created to map each login on `S1` to an Azure AD login on `S2`.

1. In SSMS, connect to `S1` and expand **Server Objects** in the **Object Explorer** window.
1. Right-click **Linked Servers** and select **New Linked Server**.
1. Fill in your linked server details:
   - **Linked server**: `S2` or use the name of your linked server.
   - **Server type**: `Other data source`.
   - **Provider**: `Microsoft OLE DB Driver for SQL Server`.
   - **Product name**: leave empty.
   - **Data source**: leave empty.
   - **Provider string**: `Server=<fqdn of S2>;Authentication=ActiveDirectoryPassword`.
   - **Catalog**: leave empty.

   :::image type="content" source="media/create-linked-server-with-password-authentication.png" alt-text="Screenshot of creating linked server with password authentication":::

1. Select the **Security** tab.
1. Select **Add**.
   - **Local Login**: specify the login name used to connect to `S1`.
   - **Impersonate**: leave unchecked.
   - **Remote User**: username of the Azure AD user used to connect to S2, in the format of `user@contoso.com`.
   - **Remote Password**: password of the Azure AD user.
   - **For a login not defined in the list above, connections will**: `Not be made`
1. Select **OK**.

   :::image type="content" source="media/linked-server-add-security.png" alt-text="Screenshot of setting security for linked server":::

### Linked server configuration using access token authentication

For access token authentication, the linked server is created with `AccessToken=%s` in the **Provider string**. A linked server login is created to map each login in `S1` to an [Azure AD application](/azure/azure-sql/database/authentication-aad-service-principal), which has been granted login permissions to `S2`. The application must have a secret assigned to it, which will be used by `S1` to generate the access token. A secret can be created by navigating to the [Azure portal](https://portal.azure.com) > **Azure Active Directory** > **App registrations** > `YourApplication` > **Certificates & secrets** > **New client secret**.

:::image type="content" source="media/application-new-client-secret.png" alt-text="Screenshot of creating a new client secret for an application in the Azure portal":::

1. In SSMS, connect to `S1` and expand **Server Objects** in the **Object Explorer** window.
1. Right-click **Linked Servers** and select **New Linked Server**.
1. Fill in your linked server details:
   - **Linked server**: `S2` or use the name of your linked server.
   - **Server type**: `Other data source`.
   - **Provider**: `Microsoft OLE DB Driver for SQL Server`.
   - **Product name**: leave empty.
   - **Data source**: leave empty.
   - **Provider string**: `Server=<fqdn of S2>;AccessToken=%s`.
   - **Catalog**: leave empty.

   :::image type="content" source="media/create-linked-server-with-access-token-authentication.png" alt-text="Screenshot of creating linked server with access token authentication":::

1. Select the **Security** tab.
1. Select **Add**.
   - **Local Login**: specify the login name used to connect to `S1`.
   - **Impersonate**: leave unchecked.
   - **Remote User**: **client ID** of the Azure AD Application used to connect to S2. You can find the **Application (client) ID** in the **Overview** menu of your Azure AD Application.
   - **Remote Password**: **Secret ID** obtained from creating a **New client secret** for the application.
   - **For a login not defined in the list above, connections will**: `Not be made`
1. Select **OK**.

## See also

- [Connect SQL Server to Azure Arc](../../../sql-server/azure-arc/connect.md)
- [Azure Active Directory authentication for SQL Server](azure-ad-authentication-sql-server-overview.md)
- [Tutorial: Set up Azure Active Directory authentication for SQL Server](azure-ad-authentication-sql-server-setup-tutorial.md)