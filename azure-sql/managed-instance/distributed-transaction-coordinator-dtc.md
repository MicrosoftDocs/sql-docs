---
title: Distributed Transaction Coordinator (DTC) (preview)
titleSuffix: Azure SQL Managed Instance
description: Learn about the Distributed Transaction Coordinator (DTC) for Azure SQL Managed Instance (preview) to run distributed transactions in mixed environments such as across managed instances, SQL Servers, other relational database management systems (RDBMSs), custom applications and other transaction participants hosted in any environment that can establish network connectivity to Azure. 
author: sasapopo
ms.author: sasapopo
ms.reviewer: mathoma, danil
ms.date: 11/16/2022
ms.service: sql-managed-instance
ms.subservice: data-movement
ms.topic: how-to
---
# Distributed Transaction Coordinator (DTC) for Azure SQL Managed Instance (preview)
[!INCLUDE[appliesto-sqlmi](../includes/appliesto-sqlmi.md)]

This article provides an overview of the Distributed Transaction Coordinator (DTC) for Azure SQL Managed Instance, which allows you to run distributed transactions in mixed environments such as across managed instances, SQL Servers, other relational database management systems (RDBMSs), custom applications and other transaction participants hosted in any environment that can establish network connectivity to Azure. 

Distributed Transaction Coordinator (DTC) for Azure SQL Managed Instance is currently in preview. 

> [!NOTE]
> DTC for Azure SQL Managed Instance is part of the November 2022 feature wave. To learn more about the timelines for feature wave roll out, see [November 2022 feature wave](https://aka.ms/sqlmi-fwnov2022). 

## Overview 

Distributed Transaction Coordinator (DTC) for Azure SQL Managed Instance allows you to run distributed transactions across a number of environments that can establish network connectivity to Azure. DTC for managed instance is **managed**, which means that Azure takes care of management and maintenance, such as logging, storage, DTC availability, networking, etc. However, aside from the managed aspect, it's still the same [DTC windows service](/previous-versions/windows/desktop/ms684146(v=vs.85)) that supports traditional distributed transactions for SQL Server. 

DTC for managed instance unlocks a wide range of technologies and scenarios, such as XA, .NET, T-SQL, COM+, ODBC, and JDBC. 

To run distributed transactions, you need to: 

- Configure DTC
- Enable network connectivity between participants
- Configure DNS settings

> [!NOTE]
> For T-SQL or .NET distributed transactions across databases hosted only by managed instances, [native support for distributed transactions](../database/elastic-transactions-overview.md) is the recommended approach. 

## Requirements 

To configure DTC, you need to have the write RBAC permission on `Microsoft.Sql/managedInstances/dtc resource`. To view DTC settings, you only need the read RBAC permission on `Microsoft.Sql/managedInstances/dtc resource`. 

## Configure DTC

To configure DTC by using the Azure portal, follow these steps: 

1. Go to your managed instance in the [Azure portal](https://portal.azure.com). 
1. Select **Distributed Transaction Coordinator** under **Settings**. 

   :::image type="content" source="media/distributed-transaction-coordinator-dtc/distributed-transaction-coordinator.png" alt-text="Screenshot of the Azure portal, the Distributed Transaction Coordinator page for SQL Managed Instance, basics tab. Distributed Transaction Coordinator is highlighted under Settings. ":::

1. Enable DTC on the **Basics** tab. 
1. Allow inbound or outbound transactions, as well as enable XA or SNA LU on the **Security** tab. 
1. Specify DTC DNS, and get information to configure external DNS and networking on the **Networking** tab. 


## Network connectivity 

There must be a network connection between all transaction participants. Since managed instances are always deployed to a dedicated Azure VNet, you need to connect your external environment to the VNet of your managed instance - in this context, external refers to anything that's not your managed instance. If your external resource is also using an Azure VNet, you can use VNet peering. Otherwise establish connectivity using your preferred method, such as point-to-site VPN, ExpressRoute, or another network connectivity technology that meets your business needs. 

Ports 135 and 1024-65535 need to allow both inbound and outbound communication - this applies to both the Azure VNet [network security group (NSG)](/azure/virtual-network/network-security-groups-overview) for the managed instance, and any firewall on the external environment. 

## DNS settings 

DTC relies on the NetBIOS name for mutual communication. Since the NetBIOS protocol isn't supported by Azure networking, and NetBIOS names can't be resolved in mixed environments, DTC for managed instance relies on DNS name servers for host name resolution. As such, managed instance DTC hosts are automatically registered with the Azure DNS Server, and you need to register external DTC hosts with some DNS server. Additionally, the managed instance and external environment need to exchange DNS suffixes.

The following diagram shows name resolution across mixed environments: 

:::image type="content" source="media/distributed-transaction-coordinator-dtc/dtc-mixed-environment-diagram.png" alt-text="Diagram that shows name resolution across mixed environments when using DTC.":::

> [!NOTE]
> You don't need to configure DNS settings if you plan to use DTC only for XA transactions. 

To exchange DNS suffixes, follow these steps: 

1. Go to your managed instance in the [Azure portal](https://portal.azure.com). 
1. Select **Distributed Transaction Coordinator** under **Settings** and then open the **Networking** tab. 

   :::image type="content" source="media/distributed-transaction-coordinator-dtc/dtc-network-settings.png" alt-text="Screenshot of the Azure portal, networking tab of the DTC page for your managed instance, and +New external DNS suffix highlighted.":::

1. Select **+New external DNS suffix**, and then provide the DNS suffix for your external environment, such as `dnszone1.com`.
1. Copy the _DTC Host DNS suffix_ value and then use the PowerShell command `Set-DnsClientGlobalSetting -SuffixSearchList $list` on your external environment to set the DTC Host DNS suffix. For example, if your suffix is abc1111111.database.windows.net as in the previous sample screenshot, then define your $list parameter to get the existing DNS settings, and append your suffix to it like the following sample: 
  
   ```powershell
   $list = (Get-DnsClientGlobalSetting).SuffixSearchList + "abc1111111.database.windows.net"
   Set-DnsClientGlobalSetting -SuffixSearchList $list
   ```

## Test network connectivity

After networking and DNS are properly configured, you should be able to run [Test-NetConnection (TNC)](/powershell/module/nettcpip/test-netconnection) between the DTC endpoints of your managed instance and the external DTC host. 

First, update the user configurable values and then use the following PowerShell script on the external environment to identify the FQDN of the DTC host managed instance: 

```powershell
# =============================================================== 
# Get DTC settings 
# =============================================================== 
# User configurable values 
# 

$SubscriptionId = "a1a1a1a1-8372-1d28-a111-1a2a31a1a1a1" 
$RgName = "my-resource-group" 
$MIName = "my-instance-name" 

# =============================================================== 
# 

$startMoveUri = "https://management.azure.com/subscriptions/" + $SubscriptionId + "/resourceGroups/" + $RgName + "/providers/Microsoft.Sql/managedInstances/" + $MIName + "/dtc/current?api-version=2022-05-01-preview" 
Write-Host "Login to Azure subscription $SubscriptionID ..." 
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

The JSON output will look something like the following FQDN: `chn000000000000.zcn111111111.database.windows.net` where: 

- `chn000000000000` is the NetBIOS name of the managed instance DTC host
- `zcn111111111.database.windows.net` is the DNS suffix

Next, run a TNC to both the FQDN and the NetBIOS name of the managed instance DTC host on port 135. 

The first entry verifies network connectivity, and the second entry verifies the DNS settings are correct: 

```
tnc chn000000000000.zcn111111111.database.windows.net -Port 135 
tnc chn000000000000 -Port 135 
```

You'll see **TcpTestSucceeded : True** as a result if connectivity and DNS suffixes are configured correctly. 

Likewise, on the managed instance side, create a SQL Agent job to run the TNC PowerShell command to test connectivity to your external host. 

For example, if the FQDN for your external host is `host10.dnszone1.com`, then run the following test via your SQL Agent job: 

```
tnc host10.dnszone1.com -Port 135 
tnc host10 -Port 135 
```

## Limitations 

Consider the following limitations when using DTC with SQL Managed Instance: 

- Running distributed T-SQL transactions between SQL Managed Instance and third-party RDBMSs isn't supported, as SQL Managed Instance doesn't support linked servers with third-party RDBMSs. Conversely, running distributed T-SQL transactions between managed instances and SQL Server, and other SQL Server-based products is supported. 
- Host names in external environment can't be longer than 15 characters. 
- Distributed transactions to Azure SQL Database aren't supported with DTC. 
- For authentication, DTC only supports the **No authentication** option. Mutual authentication and Incoming Caller authentication options aren't available. Since DTC only exchanges synchronization messages rather than user data, and communicates solely with the VNet, this isn't a security risk. 



## Next steps

For native managed instance distributed transaction support, review [Elastic transactions](../database/elastic-transactions-overview.md). 

