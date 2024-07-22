---
title: Network Access Controls
titleSuffix: Azure SQL Database & Azure Synapse Analytics
description: Overview of how to manage and control network access for Azure SQL Database and Azure Synapse Analytics.
author: rohitnayakmsft
ms.author: rohitna
ms.reviewer: wiassaf, vanto, mathoma
ms.date: 05/01/2024
ms.service: sql-database
ms.subservice: security
ms.topic: conceptual
ms.custom: sqldbrb=3
---

# Azure SQL Database and Azure Synapse Analytics network access controls

[!INCLUDE[appliesto-sqldb-asa](../includes/appliesto-sqldb-asa-formerly-sqldw.md)]

When you create a logical server from the [Azure portal](single-database-create-quickstart.md) for Azure SQL Database and Azure Synapse Analytics, the result is a public endpoint in the format, *yourservername.database.windows.net*.

You can use the following network access controls to selectively allow access to a database via **the public endpoint**:

- **IP based firewall rules**: Use this feature to explicitly allow connections from a specific IP address. For example, from on-premises machines or a range of IP addresses by specifying the start and end IP address.

- **Allow Azure services and resources to access this server**: When enabled, other resources within the Azure boundary can access SQL Database. For example, an Azure Virtual Machine can access the SQL Database resources.

You can also allow **private access** to the database from [virtual networks](/azure/virtual-network/virtual-networks-overview) via:

- **Virtual network firewall rules**: Use this feature to allow traffic from a specific virtual network within the Azure boundary.

- **Private Link**: Use this feature to create a private endpoint for the [logical server in Azure](logical-servers.md) within a specific virtual network.

> [!IMPORTANT]
> This article does *not* apply to **SQL Managed Instance**. For more information about the networking configuration, see [connecting to Azure SQL Managed Instance](../managed-instance/connect-application-instance.md) .

## IP firewall rules

IP based firewall is a feature of the logical server in Azure that prevents all access to your server until you explicitly [add IP addresses](firewall-create-server-level-portal-quickstart.md) for the client machines.

## Allow Azure services

By default, during creation of a new logical server [from the Azure portal](single-database-create-quickstart.md), **Allow Azure services and resources to access this server** is unchecked and not enabled. This setting appears when connectivity is allowed via public endpoint.

You can also change this setting via the **Networking** setting after the logical server is created as follows:
  
![Screenshot of manage server firewall][2]

When **Allow Azure services and resources to access this server** is enabled, your server allows communications from all resources inside the Azure boundary, **regardless of whether they are part of your subscription**. In many cases, enabling the setting is more permissive than what most customers want. You might want to uncheck this setting and replace it with more restrictive IP firewall rules or use one the options for private access.

> [!IMPORTANT]
> Checking *Allow Azure services and resources to access this server* adds an IP based firewall rule with start and end IP address of 0.0.0.0

However, doing so affects the following features that run on virtual machines in Azure that aren't part of your virtual network and hence connect to the database via an Azure IP address:

### Import Export Service

Import Export Service doesn't work when **Allow Azure services and resources to access this server** isn't enabled. However you can work around the problem [by manually running SqlPackage from an Azure VM or performing the export](./database-import-export-azure-services-off.md) directly in your code by using the DACFx API.

### Data Sync

To use the Data sync feature with **Allow Azure services and resources to access this server** not enabled, you need to create individual firewall rule entries to [add IP addresses](firewall-create-server-level-portal-quickstart.md) from the **Sql service tag** for the region hosting the **Hub** database. Add these server-level firewall rules to the servers hosting both **Hub** and **Member** databases (which might be in different regions).

Use the following PowerShell script to generate IP addresses corresponding to the SQL service tag for West US region.

```powershell
PS C:\>  $serviceTags = Get-AzNetworkServiceTag -Location eastus2
PS C:\>  $sql = $serviceTags.Values | Where-Object { $_.Name -eq "Sql.WestUS" }
PS C:\> $sql.Properties.AddressPrefixes.Count
70
PS C:\> $sql.Properties.AddressPrefixes
13.86.216.0/25
13.86.216.128/26
13.86.216.192/27
13.86.217.0/25
13.86.217.128/26
13.86.217.192/27
```

> [!TIP]
> Get-AzNetworkServiceTag returns the global range for SQL Service Tag despite specifying the Location parameter. Be sure to filter it to the region that hosts the Hub database used by your sync group

The output of the PowerShell script is in Classless Inter-Domain Routing (CIDR) notation. This needs to be converted to a format of Start and End IP address using [Get-IPrangeStartEnd.ps1](https://www.sqltechnet.com/2020/12/powershell-set-azure-sql-firewall-for.html) like this:

```powershell
PS C:\> Get-IPrangeStartEnd -ip 52.229.17.93 -cidr 26
start        end
-----        ---
52.229.17.64 52.229.17.127
```

You can use the following PowerShell script to convert all the IP addresses from CIDR to Start and End IP address format.

```powershell
PS C:\>foreach( $i in $sql.Properties.AddressPrefixes) {$ip,$cidr= $i.split('/') ; Get-IPrangeStartEnd -ip $ip -cidr $cidr;}
start          end
-----          ---
13.86.216.0    13.86.216.127
13.86.216.128  13.86.216.191
13.86.216.192  13.86.216.223
```

You can now add these as distinct firewall rules and then disable the setting **Allow Azure services and resources to access this server**.

## Sql Service Tag

[Service tags](/azure/virtual-network/service-tags-overview) can be used in security rules and routes from clients to SQL Database. Service tags can be used in network security groups, Azure Firewall, and user-defined routes by specifying them in the source or destination field of a security rule. 
The **Sql** service tag consists of all IP addresses that are being used by SQL Database. The tag is further segmented by regions. For example **Sql.WestUS** lists all the IP addresses used by SQL Database in West US.

The **Sql** service tag consists of IP addresses that are required to establish connectivity to SQL Database as documented in [Gateway IP addresses](connectivity-architecture.md#gateway-ip-addresses). Additionally, a service tag will also be associated with any outbound traffic from SQL Database used in features such as:

- [Auditing](auditing-overview.md)
- [Vulnerability assessment](/azure/defender-for-cloud/sql-azure-vulnerability-assessment-overview)
- [Import/Export service](database-import-export-azure-services-off.md)
- [OPENROWSET](/sql/t-sql/functions/openrowset-transact-sql)
- [Bulk Insert](/sql/t-sql/statements/bulk-insert-transact-sql)
- [sp_invoke_external_rest_endpoint](/sql/relational-databases/system-stored-procedures/sp-invoke-external-rest-endpoint-transact-sql)
- [Ledger](/sql/relational-databases/security/ledger/ledger-digest-management) 
- [Azure SQL transparent data encryption with customer-managed key](transparent-data-encryption-byok-configure.md)

## SqlManagement Service Tag

SqlManagement service tag is used for control plane operations against SQL Database.

## Virtual network firewall rules

[Virtual network firewall rules](vnet-service-endpoint-rule-overview.md) are easier alternatives to establish and manage access from a specific subnet that contains your VMs.

## Private Link

Private Link allows you to connect to a server via a **private endpoint**. A [private endpoint](private-endpoint-overview.md) is a private IP address within a specific [virtual network](/azure/virtual-network/virtual-networks-overview) and subnet.

## Related content

- For a quickstart on creating a server-level IP firewall rule, see [Create a database in SQL Database](single-database-create-quickstart.md).

- For a quickstart on creating a server-level virtual network firewall rule, see [Virtual Network service endpoints and rules for Azure SQL Database](vnet-service-endpoint-rule-overview.md).

- For help with connecting to a database in SQL Database from open source or partner applications, see [Client quickstart code samples to SQL Database](/previous-versions/azure/ee336282(v=azure.100)).

- For information on other ports that you might need to open, see the **SQL Database: Outside vs inside** section of [Ports beyond 1433 for ADO.NET 4.5 and SQL Database](adonet-v12-develop-direct-route-ports.md)

- For an overview of Azure SQL Database Connectivity, see [Azure SQL Connectivity Architecture](connectivity-architecture.md)

- For an overview of Azure SQL Database security, see [Securing your database](security-overview.md)

<!--Image references-->
[1]: media/quickstart-create-single-database/new-server2.png
[2]: media/quickstart-create-single-database/manage-server-firewall.png
