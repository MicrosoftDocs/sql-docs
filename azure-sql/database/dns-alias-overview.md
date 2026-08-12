---
title: DNS alias
titleSuffix: Azure SQL Database
description: Your applications can connect to an alias for the name of the server for Azure SQL Database. Meanwhile, you can change the SQL Database the alias points to anytime, to facilitate testing and so on.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: mathoma, vanto
ms.date: 05/29/2026
ms.service: azure-sql-database
ms.subservice: security
ms.topic: concept-article
---
# DNS alias for Azure SQL Database

[!INCLUDE[appliesto-sqldb](../includes/appliesto-sqldb.md)]

Azure SQL Database has a Domain Name System (DNS) server. PowerShell and REST APIs accept [calls to create and manage DNS aliases](#anchor-powershell-code-62x) for your [logical SQL server](logical-servers.md) name.

Use a *DNS alias* in place of the server name. Client programs can use the alias in their connection strings. The DNS alias provides a translation layer that can redirect your client programs to different servers. This layer spares you the difficulties of having to find and edit all the clients and their connection strings.

Common uses for a DNS alias include the following cases:

- Create an easy-to-remember name for a server.
- During initial development, your alias can refer to a test server. When the application goes live, you can modify the alias to refer to the production server. The transition from test to production doesn't require any modification to the clients that connect to the server.
- Suppose the only database in your application is moved to another server. You can modify the alias without having to modify the configurations of several clients.
- During a regional outage you use [geo-restore](recovery-using-backups.md#geo-restore) to recover your database in a different server and region. You can modify your existing alias to point to the new server so that the existing client application can re-connect to it.

For information on DNS aliases for standalone dedicated SQL pools in Azure Synapse Analytics, see [DNS alias for standalone dedicated SQL pools (formerly SQL DW)](/azure/synapse-analytics/sql-data-warehouse/dns-alias-overview).

## Domain Name System (DNS) of the Internet

The Internet relies on the DNS. The DNS translates your friendly names into the name of your server.

## Scenarios with one DNS alias

Suppose you need to switch your system to a new server. In the past, you needed to find and update every connection string in every client program. But now, if the connection strings use a DNS alias, you only need to update an alias property.

The DNS alias feature of Azure SQL Database can help in the following scenarios:

### Test to production

When you start developing the client programs, use a DNS alias in their connection strings. Properties of the alias point to a test version of your server.

Later, when the new system goes live in production, update the properties of the alias to point to the production server. You don't need to change the client connection strings.

### Cross-region support

A disaster recovery might shift your server to a different geographic region. For a system that uses a DNS alias, you can avoid the need to find and update all the connection strings for all clients. Instead, update an alias to refer to the new server that now hosts your Azure SQL Database.

## Properties of a DNS alias

The following properties apply to each DNS alias for your server:

- *Unique name:* Each alias name you create is unique across all servers, just as server names are.
- *Server is required:* You can't create a DNS alias unless it references exactly one server, and the server must already exist. An updated alias must always reference exactly one existing server.
  - When you drop a server, the Azure system also drops all DNS aliases that refer to the server.
- *Not bound to any region:* DNS aliases aren't bound to a region. You can update any DNS alias to refer to a server that resides in any geographic region.
- *Permissions:* To manage a DNS alias, you must have *Server Contributor* permissions, or higher. For more information, see [Get started with Azure role-based access control in the Azure portal](/azure/role-based-access-control/overview).

## Manage your DNS aliases

Use REST APIs or PowerShell cmdlets to programmatically manage your DNS aliases.

### Use REST APIs to manage Azure SQL Database DNS aliases

For more information on the REST APIs, see [Azure SQL Database REST API](/rest/api/sql/server-dns-aliases).

<a name="anchor-powershell-code-62x"></a>
<a name="use-powershell-manage-azure-sql-database-dns-aliases"></a>

### Use PowerShell to manage Azure SQL Database DNS aliases

PowerShell cmdlets are available that call the REST APIs. For PowerShell samples, see: [PowerShell for DNS Alias to Azure SQL Database](dns-alias-powershell-create.md).

The cmdlets used in the code example are the following:

- [New-AzSqlServerDnsAlias](/powershell/module/az.Sql/New-azSqlServerDnsAlias): Creates a new DNS alias in the Azure SQL Database service system. The alias refers to server 1.
- [Get-AzSqlServerDnsAlias](/powershell/module/az.Sql/Get-azSqlServerDnsAlias): Gets and lists all the DNS aliases that are assigned to server 1.
- [Set-AzSqlServerDnsAlias](/powershell/module/az.Sql/Set-azSqlServerDnsAlias): Modifies the server name that the alias is configured to refer to, from server 1 to server 2.
- [Remove-AzSqlServerDnsAlias](/powershell/module/az.Sql/Remove-azSqlServerDnsAlias): Removes the DNS alias from server 2, by using the name of the alias.

[!INCLUDE [updated-for-az](../includes/updated-for-az.md)]

> [!IMPORTANT]
> The PowerShell Azure Resource Manager (AzureRM) module was deprecated on February 29, 2024. All future development should use the Az.Sql module. Users are advised to migrate from AzureRM to the Az PowerShell module to ensure continued support and updates. The AzureRM module is no longer maintained or supported. The arguments for the commands in the Az PowerShell module and in the AzureRM modules are substantially identical. For more about their compatibility, see [Introducing the new Az PowerShell module](/powershell/azure/new-azureps-module-az).

## Limitations

Currently, a DNS alias has the following limitations:

- *Delay of up to 2 minutes:* It takes up to 2 minutes for a DNS alias to be updated or removed.
  - Regardless of any brief delay, the alias immediately stops referring client connections to the legacy server.
- *DNS lookup:* For now, the only authoritative way to check what server a given DNS alias refers to is by performing a [DNS lookup](/windows-server/administration/windows-commands/nslookup).
- DNS alias is subject to [naming restrictions](/azure/azure-resource-manager/management/resource-name-rules).

## Related content

- [Overview of business continuity with Azure SQL Database](business-continuity-high-availability-disaster-recover-hadr-overview.md), including disaster recovery.
- [Server DNS Aliases API](/rest/api/sql/server-dns-aliases)
- [PowerShell for DNS Alias to Azure SQL Database](dns-alias-powershell-create.md)
- [Geo-restore for Azure SQL Database](recovery-using-backups.md#geo-restore)
