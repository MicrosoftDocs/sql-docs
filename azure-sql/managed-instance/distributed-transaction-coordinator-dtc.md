---
title: Distributed Transaction Coordinator (DTC)
titleSuffix: Azure SQL Managed Instance
description: Learn how to use Distributed Transaction Coordinator (DTC) for Azure SQL Managed Instance to run distributed transactions in a mixed environment.
author: sasapopo
ms.author: sasapopo
ms.reviewer: mathoma, danil
ms.date: 11/16/2022
ms.service: sql-managed-instance
ms.subservice: data-movement
ms.custom: ignite-2023
ms.topic: how-to
---
# Distributed Transaction Coordinator (DTC) for Azure SQL Managed Instance

[!INCLUDE[appliesto-sqlmi](../includes/appliesto-sqlmi.md)]

This article provides an overview of Distributed Transaction Coordinator (DTC) for Azure SQL Managed Instance. You can use DTC to run distributed transactions in mixed environments, including across managed instances, SQL Server instances, other relational database management systems (RDBMSs), custom applications, and other transaction participants that are hosted in any environment that can establish network connectivity to Azure.

## Scenarios

You can enable DTC for Azure SQL Managed Instance to run distributed transactions across multiple environments that can establish network connectivity to Azure. DTC for SQL Managed Instance is *managed*, which means that Azure takes care of management and maintenance, like logging, storage, DTC availability, and networking. Aside from the managed aspect, DTC for SQL Managed Instance is the same [DTC Windows service](/previous-versions/windows/desktop/ms684146(v=vs.85)) that supports traditional distributed transactions for SQL Server.

DTC for SQL Managed Instance unlocks a wide range of technologies and scenarios, including XA, .NET, T-SQL, COM+, ODBC, and JDBC.

To run distributed transactions, complete these tasks:

1. Configure DTC.
1. Enable network connectivity between participants.
1. Configure DNS settings.

> [!NOTE]
> For T-SQL or .NET distributed transactions across databases that are hosted only by managed instances, we recommend that you use [native support for distributed transactions](../database/elastic-transactions-overview.md).

## Requirements

To change DTC settings, you must have write permissions for `Microsoft.Sql/managedInstances/dtc` resource. To view DTC settings, you must have read permissions for `Microsoft.Sql/managedInstances/dtc` resource.

## Configure DTC

You can configure DTC with Azure portal, Azure PowerShell and CLI.

### [Portal](#tab/azure-portal)

To configure DTC by using the Azure portal:

1. In the [Azure portal](https://portal.azure.com), go to your managed instance.
1. In the left menu under **Settings**, select **Distributed Transaction Coordinator**.

   :::image type="content" source="media/distributed-transaction-coordinator-dtc/distributed-transaction-coordinator.png" alt-text="Screenshot that shows the highlighted menu option, the Distributed Transaction Coordinator pane for SQL Managed Instance, and the Basics tab. ":::

1. On the **Basics** tab, set **Distributed Transaction Coordinator** to **Enabled**.
1. On the **Security** tab, allow inbound or outbound transactions, and enable XA or SNA LU.
1. On the **Networking** tab, specify DTC DNS, and get information to configure external DNS and networking.

### [PowerShell](#tab/azure-powershell)

Use Azure PowerShell commandlets [Get-AzSqlInstanceDtc](/powershell/module/az.sql/get-azsqlinstancedtc) and [Set-AzSqlInstanceDtc](/powershell/module/az.sql/set-azsqlinstancedtc) to view and modify DTC configuration.

Here's an example of how you can view and modify DTC configuration.

```powershell
Get-AzSqlInstanceDtc -InstanceName "<managed_instance_name>" -ResourceGroupName "<resource_group_name>"
Set-AzSqlInstanceDtc -InstanceName "<managed_instance_name>" -ResourceGroupName "<resource_group_name>" -DtcEnabled $true
```

### [CLI](#tab/azure-cli)

Use [Azure SQL CLI to configure DTC](/cli/azure/sql/mi/dtc).

Here's an example of how you can view and modify DTC configuration (you need to modify Subscription ID, resource group name, and managed instance name).

```CLI
az sql mi dtc show --id /subscriptions/00000000-0000-0000-0000-000000000000/resourceGroups/yourResourceGroupName/providers/Microsoft.Sql/managedInstances/yourManagedInstanceName/dtc/current
az sql mi dtc update --id /subscriptions/00000000-0000-0000-0000-000000000000/resourceGroups/yourResourceGroupName/providers/Microsoft.Sql/managedInstances/yourManagedInstanceName/dtc/current --dtc-enabled true
```

---

## Network connectivity

To use DTC, all transaction participants must have a network connection to Azure. Because managed instances are always deployed to a dedicated virtual network in Azure, you must connect your external environment to the virtual network of your managed instance. In this context, *external* refers to any object or process that isn't your managed instance. If your external resource also uses a virtual network in Azure, you can use virtual network peering. Otherwise, establish connectivity by using your preferred method, such as point-to-site VPN, Azure ExpressRoute, or another network connectivity technology that meets your business needs.

Port 135 must allow both inbound and outbound communication, port range 14000-15000 must allow inbound, and 49152-65535 must allow outbound communication, in both the virtual network [network security group](/azure/virtual-network/network-security-groups-overview) for the managed instance and in any firewall that's set up in the external environment.

## DNS settings

DTC relies on a transaction participant's NetBIOS name for mutual communication. Because the NetBIOS protocol isn't supported by Azure networking and NetBIOS names can't be resolved in mixed environments, DTC for a managed instance relies on DNS name servers for host name resolution. Managed instance DTC hosts are automatically registered with the Azure DNS server. You must register external DTC hosts with a DNS server. The managed instance and external environment also must exchange DNS suffixes.

The following diagram shows name resolution across mixed environments:

:::image type="content" source="media/distributed-transaction-coordinator-dtc/dtc-mixed-environment-diagram.png" border="false" alt-text="Diagram that shows name resolution across mixed environments when you use DTC.":::

> [!NOTE]
> You don't need to configure DNS settings if you plan to use DTC only for XA transactions.

To exchange DNS suffixes:

1. In the [Azure portal](https://portal.azure.com), go to your managed instance.
1. In the left menu under **Settings**, select **Distributed Transaction Coordinator**. Select the **Networking** tab.

   :::image type="content" source="media/distributed-transaction-coordinator-dtc/dtc-network-settings.png" alt-text="Screenshot that shows the Networking tab of the DTC pane for your managed instance in the Azure portal, with New external DNS suffix highlighted.":::

1. In **DNS configuration**, select **New external DNS suffix**. Enter the DNS suffix for your external environment, such as `dnszone1.com`.
1. Copy the value for _DTC Host DNS suffix_. Then use the PowerShell command `Set-DnsClientGlobalSetting -SuffixSearchList $list` on your external environment to set the DTC Host DNS suffix. For example, if your suffix is `abc1111111.database.windows.net`, define your `$list` parameter to get the existing DNS settings. Then, append your suffix to it as shown in the following example:
  
   ```powershell
   $list = (Get-DnsClientGlobalSetting).SuffixSearchList + "abc1111111.database.windows.net"
   Set-DnsClientGlobalSetting -SuffixSearchList $list
   ```

## Test network connectivity

After you configure networking and DNS, run [Test-NetConnection (TNC)](/powershell/module/nettcpip/test-netconnection) between the DTC endpoints of your managed instance and the external DTC host.

To test the connection, first update the user-configurable values. Then, use the following PowerShell script on the external environment to identify the fully qualified domain name (FQDN) of the DTC host managed instance. Here's an example:

```powershell
# =============================================================== 
# Get DTC settings 
# =============================================================== 
# User-configurable values 
# 

$SubscriptionId = "a1a1a1a1-8372-1d28-a111-1a2a31a1a1a1" 
$RgName = "my-resource-group" 
$MIName = "my-instance-name" 

# =============================================================== 
# 

$startMoveUri = "https://management.azure.com/subscriptions/" + $SubscriptionId + "/resourceGroups/" + $RgName + "/providers/Microsoft.Sql/managedInstances/" + $MIName + "/dtc/current?api-version=2022-05-01-preview" 
Write-Host "Sign in to Azure subscription $SubscriptionID ..." 
Select-AzSubscription -SubscriptionName $SubscriptionID 
$azContext = Get-AzContext 
$azProfile = [Microsoft.Azure.Commands.Common.Authentication.Abstractions.AzureRmProfileProvider]::Instance.Profile 
$profileClient = New-Object -TypeName Microsoft.Azure.Commands.ResourceManager.Common.RMProfileClient -ArgumentList ($azProfile) 
Write-Host "Getting authentication token for REST API call ..." 
$token = $profileClient.AcquireAccessToken($azContext.Subscription.TenantId) 
$authHeader = @{'Content-Type'='application/json';'Authorization'='Bearer ' + $token.AccessToken} 


# Invoke API call to start the operation 
# 

Write-Host "Starting API call..." 
$startMoveResp = Invoke-WebRequest -Method Get -Headers $authHeader -Uri $startMoveUri 
Write-Host "Response:" $startMoveResp 

# End 
# =============================================================== 
```

The JSON output looks like the following example FQDN:

`chn000000000000.zcn111111111.database.windows.net`

Where:

- `chn000000000000` is the NetBIOS name of the managed instance DTC host.
- `zcn111111111.database.windows.net` is the DNS suffix.

Next, run a TNC to both the FQDN and the NetBIOS name of the managed instance DTC host on port 135. In the following example, the first entry verifies network connectivity. The second entry verifies that the DNS settings are correct.

```powershell
tnc chn000000000000.zcn111111111.database.windows.net -Port 135 
tnc chn000000000000 -Port 135 
```

If connectivity and DNS suffixes are configured correctly, the output **TcpTestSucceeded : True** appears.

On the managed instance side, create a SQL Agent job to run the TNC PowerShell command to test connectivity to your external host.

For example, if the FQDN for your external host is `host10.dnszone1.com`, run the following test via your SQL Agent job:

```powershell
tnc host10.dnszone1.com -Port 135 
tnc host10 -Port 135 
```

## Limitations

Consider the following limitations when you use DTC with SQL Managed Instance:

- Running distributed T-SQL transactions between SQL Managed Instance and a third-party RDBMS isn't supported. SQL Managed Instance doesn't support linked servers that have third-party RDBMSs. Conversely, running distributed T-SQL transactions between managed instances and SQL Server and other SQL Server-based products is supported.
- Host names in external environment can't be longer than 15 characters.
- Distributed transactions to Azure SQL Database aren't supported with DTC.
- For authentication, DTC supports only the *no authentication* option. Mutual authentication and incoming caller authentication options aren't available. Because DTC exchanges only synchronization messages and not user data, and because it communicates solely with the virtual network, this limitation isn't a security risk.

## Manage transactions

To view statistics of distributed transactions, see [sys.dm_tran_distributed_transaction_stats](/sql/relational-databases/system-dynamic-management-views/sys-dm-tran-distributed-transaction-stats).

You can reset the DTC log with the [sp_reset_dtc_log](/sql/relational-databases/system-stored-procedures/sp-reset-dtc-log) stored procedure.

Distributed transactions can be managed with the [sys.sp_manage_distributed_transaction](/sql/relational-databases/system-stored-procedures/sys-sp-manage-distributed-transaction) stored procedure.

## Next steps

For native managed instance distributed transaction support, see [Elastic transactions](../database/elastic-transactions-overview.md).
