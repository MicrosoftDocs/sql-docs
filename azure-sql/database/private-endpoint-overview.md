---
title: Azure Private Link
titleSuffix: Azure SQL Database & Azure Synapse Analytics
description: Overview of private endpoint feature.
author: rohitnayakmsft
ms.author: rohitna
ms.reviewer: wiassaf, vanto, mathoma, randolphwest
ms.date: 04/05/2024
ms.service: sql-database
ms.subservice: security
ms.topic: overview
ms.custom:
  - sqldbrb=1
  - fasttrack-edit
---

# Azure Private Link for Azure SQL Database and Azure Synapse Analytics

[!INCLUDE [appliesto-sqldb-asa](../includes/appliesto-sqldb-asa-formerly-sqldw.md)]

Private Link allows you to connect to various PaaS services in Azure via a **private endpoint**. For a list of PaaS services that support Private Link functionality, go to the [Private Link Documentation](/azure/private-link/index) page. A private endpoint is a private IP address within a specific [VNet](/azure/virtual-network/virtual-networks-overview) and subnet.

> [!IMPORTANT]  
> This article applies to both Azure SQL Database and [dedicated SQL pool (formerly SQL DW)](/azure/synapse-analytics/sql-data-warehouse/sql-data-warehouse-overview-what-is) in Azure Synapse Analytics. These settings apply to all SQL Database and dedicated SQL pool (formerly SQL DW) databases associated with the server. For simplicity, the term 'database' refers to both databases in Azure SQL Database and Azure Synapse Analytics. Likewise, any references to 'server' is referring to the [logical server](logical-servers.md) that hosts Azure SQL Database and dedicated SQL pool (formerly SQL DW) in Azure Synapse Analytics. This article does *not* apply to Azure SQL Managed Instance or dedicated SQL pools in Azure Synapse Analytics workspaces.

## How to set up Private Link

### Creation process

Private Endpoints can be created using the Azure portal, PowerShell, or the Azure CLI:

- [The portal](/azure/private-link/create-private-endpoint-portal)
- [PowerShell](/azure/private-link/create-private-endpoint-powershell)
- [CLI](/azure/private-link/create-private-endpoint-cli)

### Approval process

Once the network admin creates the Private Endpoint (PE), the SQL admin can manage the Private Endpoint Connection (PEC) to SQL Database.

1. Navigate to the server resource in the [Azure portal](https://portal.azure.com).

1. Navigate to the private endpoint approval page:
    - In Azure SQL Database, under **Security** in the resource menu, select **Networking**. Select the **Private access** tab.
    - In Synapse workspace, under **Security** in the resource menu, select **Private endpoint connections**.

1. The page shows the following:

   - A list of all Private Endpoint Connections (PECs)
   - Private endpoints (PE) created

   :::image type="content" source="media/private-endpoint/pec-list-before.png" alt-text="Screenshot that shows how to locate the list of private endpoint connections for the server resource." lightbox="media/private-endpoint/pec-list-before.png" border="false":::

1. If there are no private endpoints, create one using the **Create a private endpoint** button. Otherwise, choose an individual PEC from the list by selecting it.

   :::image type="content" source="media/private-endpoint/pec-select.png" alt-text="Screenshot that shows how to select a private endpoint connection in the Azure portal." lightbox="media/private-endpoint/pec-select.png":::

1. The SQL admin can choose to approve or reject a PEC and optionally add a short text response.

   :::image type="content" source="media/private-endpoint/pec-approve.png" alt-text="Screenshot that shows how to approve a PEC in the Azure portal." lightbox="media/private-endpoint/pec-approve.png":::

1. After approval or rejection, the list will reflect the appropriate state along with the response text.

   :::image type="content" source="media/private-endpoint/pec-list-after.png" alt-text="Screenshot that shows the PEC in the Approved state after approval by the admin." lightbox="media/private-endpoint/pec-list-after.png":::

1. Finally, select the private endpoint name

   :::image type="content" source="media/private-endpoint/pec-select.png" alt-text="Screenshot showing PEC details with the endpoint name." lightbox="media/private-endpoint/pec-select.png":::

   This takes you to the **Private endpoint** overview page. Select the **Network interfaces** link to get the network interface details for the private endpoint connection.

   :::image type="content" source="media/private-endpoint/pec-nic-click.png" alt-text="Screenshot that shows the NIC details for the private endpoint connection." lightbox="media/private-endpoint/pec-nic-click.png":::

   The **Network interface** page shows the private IP address for the private endpoint connection.

   :::image type="content" source="media/private-endpoint/pec-ip-display.png" alt-text="Screenshot that shows the Private IP address for the private endpoint connection." lightbox="media/private-endpoint/pec-ip-display.png":::

> [!IMPORTANT]  
> When you add a private endpoint connection, public routing to your logical server isn't blocked by default. In the **Firewall and virtual networks** pane, the setting **Deny public network access** isn't selected by default. To disable public network access, ensure that you select **Deny public network access**.

## Disable public access to your logical server

In Azure SQL Database logical SQL server, assume you want to disable all public access to your logical server and allow connections only from your virtual network.

First, ensure that your private endpoint connections are enabled and configured. Then, to disable public access to your logical server:

1. Go to the **Networking** page of your logical server.
1. Select the **Deny public network access** checkbox.

   :::image type="content" source="media/private-endpoint/pec-deny-public-access.png" alt-text="Screenshot that shows how to disable public network access for the private endpoint connection." lightbox="media/private-endpoint/pec-deny-public-access.png":::

## Test connectivity to SQL Database from an Azure VM in same virtual network

For this scenario, assume you've created an Azure Virtual Machine (VM) running a recent version of Windows in the same virtual network as the private endpoint.

1. [Start a Remote Desktop (RDP) session and connect to the virtual machine](/azure/virtual-machines/windows/connect-logon#connect-to-the-virtual-machine).

1. You can then do some basic connectivity checks to ensure that the VM is connecting to SQL Database via the private endpoint using the following tools:

   - Telnet
   - PsPing
   - Nmap
   - [SQL Server Management Studio (SSMS)](https://aka.ms/ssms)

### Check connectivity using Telnet

[Telnet Client](/previous-versions/windows/it-pro/windows-server-2008-R2-and-2008/cc754293%28v%3dws.10%29) is a Windows feature that can be used to test connectivity. Depending on the version of the Windows OS, you might need to enable this feature explicitly.

Open a Command Prompt window after you have installed Telnet. Run the **Telnet** command and specify the IP address and private endpoint of the database in SQL Database.

```console
telnet 10.9.0.4 1433
```

When Telnet connects successfully, it outputs a blank screen at the command window, as shown in the following image:

:::image type="content" source="media/private-endpoint/telnet-result.png" alt-text="Diagram of the Telnet window with blank screen." lightbox="media/private-endpoint/telnet-result.png":::

Use PowerShell command to check the connectivity:

```powershell
Test-NetConnection -computer myserver.database.windows.net -port 1433
```

### Check Connectivity using PsPing

**[PsPing](/sysinternals/downloads/PsPing)** can be used as follows to check that the private endpoint is listening for connections on port 1433.

Run **PsPing** as follows by providing the FQDN for logical SQL server and port 1433:

```console
PsPing.exe mysqldbsrvr.database.windows.net:1433
```

This is an example of the expected output:

```output
TCP connect to 10.9.0.4:1433:
5 iterations (warmup 1) ping test:
Connecting to 10.9.0.4:1433 (warmup): from 10.6.0.4:49953: 2.83ms
Connecting to 10.9.0.4:1433: from 10.6.0.4:49954: 1.26ms
Connecting to 10.9.0.4:1433: from 10.6.0.4:49955: 1.98ms
Connecting to 10.9.0.4:1433: from 10.6.0.4:49956: 1.43ms
Connecting to 10.9.0.4:1433: from 10.6.0.4:49958: 2.28ms
```

The output show that **PsPing** could ping the private IP address associated with the private endpoint.

### Check connectivity using Nmap

Nmap (Network Mapper) is a free and open-source tool used for network discovery and security auditing. For more information and the download link, visit https://Nmap.org. You can use this tool to ensure that the private endpoint is listening for connections on port 1433.

Run **Nmap** as follows by providing the address range of the subnet that hosts the private endpoint.

```console
Nmap -n -sP 10.9.0.0/24
```

This is an example of the expected output:

```output
Nmap scan report for 10.9.0.4
Host is up (0.00s latency).
Nmap done: 256 IP addresses (1 host up) scanned in 207.00 seconds
```

The result shows that one IP address is up; which corresponds to the IP address for the private endpoint.

### Check connectivity using SQL Server Management Studio (SSMS)

> [!NOTE]  
> Use the **Fully Qualified Domain Name (FQDN)** of the server in connection strings for your clients (`<server>.database.windows.net`). Any login attempts made directly to the IP address or using the private link FQDN (`<server>.privatelink.database.windows.net`) shall fail. This behavior is by design, since private endpoint routes traffic to the SQL Gateway in the region and the correct FQDN needs to be specified for logins to succeed.

Follow the steps here to use [SSMS to connect to the SQL Database](connect-query-ssms.md). After you connect to the SQL Database using SSMS, the following query shall reflect client_net_address that matches the private IP address of the Azure VM you're connecting from:

```sql
SELECT client_net_address
FROM sys.dm_exec_connections
WHERE session_id = @@SPID;
```

## Use Redirect connection policy with private endpoints

We recommend that customers use the private link with the **Redirect connection policy** for reduced latency and improved throughput. For connections to use this mode, clients need to meet the following pre-requisites:

- Allow **inbound** communication to the VNET hosting the private endpoint to port range 1433 to 65535.

- Allow **outbound** communication from the VNET hosting the client to port range 1433 to 65535.

- Use the **latest version of drivers that have redirect support built in.** Redirect support is included in ODBC, OLEDB, NET SqlClient Data Provider, Core .NET SqlClient Data Provider, and JDBC (version 9.4 or above) drivers. Connections originating from all other drivers are proxied.

After meeting the pre-requisite, clients need to explcitly [choose **Redirect** connection policy](connectivity-architecture.md#connection-policy).

If it isn't feasible to modify the firewall settings to allow outbound access on the 1433-65535 port range, an alternative solution is to change the connection policy to **Proxy**.

Existing private endpoints using **Default** connection policy shall be proxied i.e. they will use the Proxy connection policy with port 1433. The reason for doing this is is to avoid any disruption to client's traffic from reaching Sql Database due to requisite port ranges for redirection not being open.


## On-premises connectivity over private peering

When customers connect to the public endpoint from on-premises machines, their IP address needs to be added to the IP-based firewall using a [Server-level firewall rule](firewall-create-server-level-portal-quickstart.md). While this model works well for allowing access to individual machines for dev or test workloads, it's difficult to manage in a production environment.

With Private Link, customers can enable cross-premises access to the private endpoint using [ExpressRoute](/azure/expressroute/expressroute-introduction), private peering, or VPN tunneling. Customers can then disable all access via the public endpoint and not use the IP-based firewall to allow any IP addresses.

## Use cases of Private Link for Azure SQL Database

Clients can connect to the Private endpoint from the same virtual network, peered virtual network in same region, or via virtual network to virtual network connection across regions. Additionally, clients can connect from on-premises using ExpressRoute, private peering, or VPN tunneling. The following simplified diagram shows the common use cases.

:::image type="content" source="media/private-endpoint/pe-connect-overview.png" alt-text="Diagram of connectivity option." lightbox="media/private-endpoint/pe-connect-overview.png":::

In addition, services that aren't running directly in the virtual network but are integrated with it (for example, App Service web apps or Functions) can also achieve private connectivity to the database. For more information on this specific use case, see the [Web app with private connectivity to Azure SQL database](/azure/architecture/example-scenario/private-web-app/private-web-app) architecture scenario.

## Connect from an Azure VM in Peered Virtual Network

Configure [virtual network peering](/azure/virtual-network/tutorial-connect-virtual-networks-powershell) to establish connectivity to the SQL Database from an Azure VM in a peered virtual network.

## Connect from an Azure VM in virtual network to virtual network environment

Configure [virtual network to virtual network VPN gateway connection](/azure/vpn-gateway/vpn-gateway-howto-vnet-vnet-resource-manager-portal) to establish connectivity to a database in SQL Database from an Azure VM in a different region or subscription.

## Connect from an on-premises environment over VPN

To establish connectivity from an on-premises environment to the database in SQL Database, choose and implement one of the options:

- [Point-to-Site connection](/azure/vpn-gateway/vpn-gateway-howto-point-to-site-rm-ps)
- [Site-to-Site VPN connection](/azure/vpn-gateway/vpn-gateway-create-site-to-site-rm-powershell)
- [ExpressRoute circuit](/azure/expressroute/expressroute-howto-linkvnet-portal-resource-manager)

Consider [DNS configuration scenarios](/azure/private-link/private-endpoint-dns#dns-configuration-scenarios) as well, as the FQDN of the service can resolve to the public IP address.

## Connect from Azure Synapse Analytics to Azure Storage using PolyBase and the COPY statement

PolyBase and the COPY statement are commonly used to load data into Azure Synapse Analytics from Azure Storage accounts. If the Azure Storage account, which you're loading data from, limits access only to a set of virtual network subnets via Private Endpoints, Service Endpoints, or IP-based firewalls, the connectivity from PolyBase and the COPY statement to the account will break. For enabling both import and export scenarios with Azure Synapse Analytics connecting to Azure Storage that's secured to a virtual network, follow the steps provided [here](vnet-service-endpoint-rule-overview.md#impact-of-using-virtual-network-service-endpoints-with-azure-storage).

## Data exfiltration prevention

Data exfiltration in Azure SQL Database is when a user, such as a database admin is able extract data from one system and move it another location or system outside the organization. For example, the user moves the data to a storage account owned by a third party.

Consider a scenario with a user running SQL Server Management Studio (SSMS) inside an Azure virtual machine connecting to a database in SQL Database. This database is in the West US data center. The following example shows how to limit access with public endpoints on SQL Database using network access controls.

1. Disable all Azure service traffic to SQL Database via the public endpoint by setting Allow Azure Services to **OFF**. Ensure no IP addresses are allowed in the server and database level firewall rules. For more information, see [Azure SQL Database and Azure Synapse Analytics network access controls](network-access-controls-overview.md).
1. Only allow traffic to the database in SQL Database using the Private IP address of the VM. For more information, see the articles on [Service Endpoint](vnet-service-endpoint-rule-overview.md) and [virtual network firewall rules](firewall-configure.md).
1. On the Azure VM, narrow down the scope of outgoing connection by using [Network Security Groups (NSGs)](/azure/virtual-network/manage-network-security-group) and Service Tags as follows.
   - Specify an NSG rule to allow traffic for Service Tag = SQL.WestUs - only allowing connection to SQL Database in West US.
   - Specify an NSG rule (with a **higher priority**) to deny traffic for Service Tag = SQL - denying connections to SQL Database in all regions.

At the end of this setup, the Azure VM can connect only to a database in SQL Database in the West US region. However, the connectivity isn't restricted to a single database in SQL Database. The VM can still connect to any database in the West US region, including the databases that aren't part of the subscription. While we've reduced the scope of data exfiltration in the above scenario to a specific region, we haven't eliminated it altogether.

With Private Link, customers can now set up network access controls like NSGs to restrict access to the private endpoint. Individual Azure PaaS resources are then mapped to specific private endpoints. A malicious insider can only access the mapped PaaS resource (for example a database in SQL Database) and no other resource.

## Related content

- [An overview of Azure SQL Database and SQL Managed Instance security capabilities](security-overview.md)
- [Azure SQL Database and Azure Synapse Analytics connectivity architecture](connectivity-architecture.md)
- [Web app with private connectivity to Azure SQL database](/azure/architecture/example-scenario/private-web-app/private-web-app)
