---
title: Configure a single-subnet Always On availability group (PowerShell & Azure CLI)
description: "Use either PowerShell or the Azure CLI to create the Windows Server Failover Cluster (WSFC), the Always On availability group listener, and the internal load balancer on a SQL Server VM in Azure."
author: AbdullahMSFT
ms.author: amamun
ms.reviewer: vanto
ms.date: 01/23/2026
ms.service: azure-vm-sql-server
ms.subservice: hadr
ms.topic: how-to
ai-usage: ai-assisted
ms.custom:
  - devx-track-azurecli
  - devx-track-azurepowershell
tags: azure-resource-manager
---

# Use PowerShell or Azure CLI to configure a single subnet Always On availability group for SQL Server on Azure VMs 
[!INCLUDE[appliesto-sqlvm](../../includes/appliesto-sqlvm.md)]

[!INCLUDE[tip-for-multi-subnet-ag](../../includes/virtual-machines-ag-listener-multi-subnet.md)]

This article describes how to use [PowerShell](/powershell/scripting/install/installing-powershell) or the [Azure CLI](/cli/azure/sql/vm) to deploy a Windows Server Failover Cluster (WSFC), add SQL Server on Azure Virtual Machines (VMs) to the WSFC, and create the internal load balancer and listener for an Always On availability group (AG) within a single subnet. 

Deployment of the AG is still done manually through SQL Server Management Studio (SSMS) or Transact-SQL (T-SQL). 

While this article uses PowerShell and the Azure CLI to configure the AG environment, it's also possible to do so from the [Azure portal](availability-group-azure-portal-configure.md), using [Azure Quickstart Templates](availability-group-quickstart-template-configure.md), or [Manually](availability-group-manually-configure-tutorial-single-subnet.md) as well. 

> [!NOTE]
> It's now possible to lift and shift your AG solution to SQL Server on Azure VMs using Azure Migrate. See [Migrate availability group](../../migration-guides/virtual-machines/sql-server-availability-group-to-sql-on-azure-vm.md) to learn more. 

## Prerequisites

To configure an Always On availability group, you must have the following prerequisites: 

- An [Azure subscription](https://azure.microsoft.com/pricing/purchase-options/azure-account?cid=msft_learn).
- A resource group with a domain controller. 
- One or more domain-joined [VMs in Azure running SQL Server 2016 (or later) Enterprise edition](./create-sql-vm-portal.md) in the *same* availability set or *different* availability zones that have been [registered with the SQL IaaS Agent extension](sql-agent-extension-manually-register-single-vm.md).  
- The latest version of [PowerShell](/powershell/scripting/install/installing-powershell) or the [Azure CLI](/cli/azure/install-azure-cli). 
- Two available (not used by any entity) IP addresses. One IP address is for the internal load balancer. The other IP address is for the AG listener within the same subnet as the AG. If you're using an existing load balancer, you only need one available IP address for the AG listener.
- Windows Server Core isn't a supported operating system for the PowerShell commands referenced in this article as there's a dependency on [RSAT](/windows-server/remote/remote-server-administration-tools), which isn't included in Core installations of Windows.

## Permissions

You need the following account permissions to configure the AG by using the Azure CLI: 

- An existing domain user account that has **Create Computer Object** permission in the domain. For example, a domain admin account typically has sufficient permission (for example: account@domain.com). _The domain user account should also be part of the local administrator group on each VM to create the WSFC._
- The domain user account that controls SQL Server. 
 
## Create a storage account 

The WSFC needs a storage account to act as the cloud witness. You can use any existing storage account, or you can create a new storage account. If you want to use an existing storage account, skip ahead to the next section. 

# [Azure CLI](#tab/azure-cli)

Use the [az storage account create](/cli/azure/storage/account#az-storage-account-create) Azure CLI command to create a storage account for the cluster cloud witness.

```azurecli-interactive
# Create the storage account
# example: az storage account create -n 'cloudwitness' -g SQLVM-RG -l 'West US' `
#  --sku Standard_LRS --kind StorageV2 --access-tier Hot --https-only true

az storage account create -n <name> -g <resource group name> -l <region> `
  --sku Standard_LRS --kind StorageV2 --access-tier Hot --https-only true
```

> [!TIP]
> You might see the error `az sql: 'vm' is not in the 'az sql' command group` if you're using an outdated version of the Azure CLI. Download the [latest version of Azure CLI](/cli/azure/install-azure-cli-windows) to get past this error.


# [PowerShell](#tab/azure-powershell)

Use the [New-AzStorageAccount](/powershell/module/az.storage/new-azstorageaccount) PowerShell command to create a storage account for the cluster cloud witness.

```powershell-interactive
# Create the storage account
# example: New-AzStorageAccount -ResourceGroupName SQLVM-RG -Name cloudwitness `
#    -SkuName Standard_LRS -Location West US -Kind StorageV2 `
#    -AccessTier Hot -EnableHttpsTrafficOnly $true

New-AzStorageAccount -ResourceGroupName <resource group name> -Name <name> `
    -SkuName Standard_LRS -Location <region> -Kind StorageV2 `
    -AccessTier Hot -EnableHttpsTrafficOnly $true
```

**Key flags:** `-Kind StorageV2` specifies the storage version; `-EnableHttpsTrafficOnly` enforces secure connections.

---

**Verification:** The command returns `ProvisioningState: Succeeded` and the storage account appears in the resource group.

## Define cluster metadata

The SQL VM group manages the metadata of the WSFC service that hosts the AG. WSFC metadata includes the Active Directory domain, cluster accounts, storage accounts to be used as the cloud witness, and SQL Server version. Define the metadata for WSFC so that when the first SQL Server VM is added, the WSFC is created as defined.

# [Azure CLI](#tab/azure-cli)

Use the [az sql vm group create](/cli/azure/sql/vm/group#az-sql-vm-group-create) Azure CLI command to define the metadata for the WSFC.

**Key parameters:**

| Parameter | Example value / Default |
|-----------|------------------------|
| `-n` | Cluster name |
| `-l` | `eastus` (region) |
| `-g` | Resource group name |
| `--image-offer` | `SQL2017-WS2016`, `sql2019-ws2019`, `sql2022-ws2022`, `sql2025-ws2025` |
| `--image-sku` | `Enterprise` |
| `--domain-fqdn` | `domain.com` |
| `--operator-acc` | `vmadmin@domain.com` |
| `--bootstrap-acc` | `vmadmin@domain.com` |
| `--service-acc` | `sqlservice@domain.com` |
| `--storage-account` | `https://cloudwitness.blob.core.windows.net/` |
| `--cluster-subnet-type` | `SingleSubnet` |

The following code snippet defines the WSFC metadata:

```azurecli-interactive
# Define the cluster metadata

az sql vm group create -n <cluster name> -l <region ex:eastus> -g <resource group name> `
  --image-offer <SQL2016-WS2016 or SQL2017-WS2016> --image-sku Enterprise --domain-fqdn <FQDN ex: domain.com> `
  --operator-acc <domain account ex: testop@domain.com> --bootstrap-acc <domain account ex:bootacc@domain.com> `
  --service-acc <service account ex: testservice@domain.com> `
  --sa-key '<PublicKey>' `
  --storage-account '<ex:https://cloudwitness.blob.core.windows.net/>'
  --cluster-subnet-type 'SingleSubnet'
```

# [PowerShell](#tab/azure-powershell)

Use the [New-AzSqlVMGroup](/powershell/module/az.sqlvirtualmachine/new-azsqlvmgroup) PowerShell command to define the metadata for the WSFC.

**Key parameters:**

| Parameter | Example value / Default |
|-----------|------------------------|
| `-Name` | Cluster name |
| `-Location` | `West US` (region) |
| `-ResourceGroupName` | Resource group name |
| `-Offer` | `sql2019-ws2019`, `SQL2017-WS2016`, `sql2022-ws2022`, `sql2025-ws2025` |
| `-Sku` | `Enterprise` |
| `-DomainFqdn` | `domain.com` |
| `-ClusterOperatorAccount` | `vmadmin@domain.com` |
| `-ClusterBootstrapAccount` | `vmadmin@domain.com` |
| `-SqlServiceAccount` | `sqlservice@domain.com` |
| `-StorageAccountUrl` | `https://cloudwitness.blob.core.windows.net/` |
| `-StorageAccountPrimaryKey` | Secure string (public key) |
| `-ClusterSubnetType` | `SingleSubnet` |

```powershell-interactive
# Convert storage account key to secure string
$storageAccountPrimaryKey = ConvertTo-SecureString -String "<PublicKey>" -AsPlainText -Force
```

**Key flag:** `-AsPlainText -Force` converts the plain text key into a secure string for the cluster witness.

```powershell-interactive
# Create the SQL VM Group with cluster metadata
$group = New-AzSqlVMGroup -Name <name> -Location <region> `
  -ResourceGroupName <resource group name> -Offer <SQL201?-WS201?> `
  -Sku Enterprise -DomainFqdn <FQDN> `
  -ClusterOperatorAccount <domain account> `
  -ClusterBootstrapAccount <domain account> `
  -SqlServiceAccount <service account> `
  -StorageAccountUrl '<ex:StorageAccountUrl>' `
  -StorageAccountPrimaryKey $storageAccountPrimaryKey `
  -ClusterSubnetType 'SingleSubnet'
```

**Key flags:** `-ClusterSubnetType 'SingleSubnet'` specifies single-subnet topology; `-StorageAccountUrl` sets cloud witness location.

---

## Add VMs to the cluster

Adding the first SQL Server VM to the WSFC (WSFC created by `az sql vm group create`) creates the physical WSFC. The [az sql vm add-to-group](/cli/azure/sql/vm#az-sql-vm-add-to-group) command creates the WSFC with the name previously given, installs the WSFC role on the SQL Server VMs, and adds those VMs to the WSFC. Subsequent uses of the `az sql vm add-to-group` command add more SQL Server VMs to the newly created WSFC. 


# [Azure CLI](#tab/azure-cli)

Use the [az sql vm add-to-group](/cli/azure/sql/vm#az-sql-vm-add-to-group) Azure CLI command to add SQL Server VMs to the cluster.

```azurecli-interactive
# Add SQL Server VMs to WSFC
# example: az sql vm add-to-group -n SQLVM1 -g SQLVM-RG --sqlvm-group Cluster `
#  -b <password> -p <password> -s <password>
# example: az sql vm add-to-group -n SQLVM2 -g SQLVM-RG --sqlvm-group Cluster `
#  -b <password> -p <password> -s <password>

az sql vm add-to-group -n <VM1 Name> -g <Resource Group Name> --sqlvm-group <cluster name> `
  -b <bootstrap account password> -p <operator account password> -s <service account password>
az sql vm add-to-group -n <VM2 Name> -g <Resource Group Name> --sqlvm-group <cluster name> `
  -b <bootstrap account password> -p <operator account password> -s <service account password>
```

```azurecli-interactive
# Verify VM registration
az sql vm show -n <VM1 Name> -g <Resource Group Name>
```

**Expected output:** The command returns `"provisioningState": "Succeeded"` and `"sqlVirtualMachineGroupResourceId"` populated with the cluster group resource ID.

# [PowerShell](#tab/azure-powershell)

Use the [Update-AzSqlVM](/powershell/module/az.sqlvirtualmachine/update-azsqlvm) PowerShell command to add SQL Server VMs to the cluster.

```powershell-interactive
# Add first SQL Server VM to WSFC (creates the physical WSFC)
$sqlvm1 = Update-AzSqlVM -ResourceGroupName <Resource Group Name> -Name <VM1 Name> `
   -SqlVirtualMachineGroupResourceId $group.Id `
   -WsfcDomainCredentialsClusterBootstrapAccountPassword <bootstrap account password> `
   -WsfcDomainCredentialsClusterOperatorAccountPassword <operator account password> `
   -WsfcDomainCredentialsSqlServiceAccountPassword <service account password>
```

**Key flag:** `-SqlVirtualMachineGroupResourceId $group.Id` links the VM to the cluster group; first VM creates the WSFC.

```powershell-interactive
# Add second SQL Server VM to existing WSFC
$sqlvm2 = Update-AzSqlVM -ResourceGroupName <Resource Group Name> -Name <VM2 Name> `
   -SqlVirtualMachineGroupResourceId $group.Id `
   -WsfcDomainCredentialsClusterBootstrapAccountPassword <bootstrap account password> `
   -WsfcDomainCredentialsClusterOperatorAccountPassword <operator account password> `
   -WsfcDomainCredentialsSqlServiceAccountPassword <service account password>
```

**Key flags:** Three `-WsfcDomainCredentials*` parameters provide passwords for cluster accounts (bootstrap, operator, SQL service).

```powershell-interactive
# Verify VM registration
Get-AzSqlVM -ResourceGroupName <Resource Group Name> -Name <VM1 Name>
```

**Expected output:** The command returns the SQL VM resource showing `ProvisioningState: Succeeded` and `SqlVirtualMachineGroupResourceId` populated with the cluster group ID.

---

**Verification:** The command returns `ProvisioningState: Succeeded` for each VM, and the WSFC is created on the first VM addition.

## Configure quorum

Although the disk witness is the most resilient quorum option, it requires an Azure shared disk, which imposes some limitations to the AG. As such, the cloud witness is the recommended quorum solution for WSFCs hosting AGs for SQL Server on Azure VMs. 

If you have an even number of votes in the WSFC, configure the [quorum solution](hadr-cluster-quorum-configure-how-to.md) that best suits your business needs. For more information, see [Quorum with SQL Server VMs](hadr-windows-server-failover-cluster-overview.md#quorum). 

## Validate cluster 

For a WSFC to be supported by Microsoft, it must pass cluster validation. Connect to the VM using your preferred method, such as [Bastion](/azure/bastion/bastion-connect-vm-rdp-windows), and validate that your WSFC passes validation before proceeding further. Failure to do so leaves your WSFC in an unsupported state. 

You can validate the WSFC using Failover Cluster Manager (FCM) or the following PowerShell command:

   ```powershell
   Test-Cluster –Node ("<node1>","<node2>") –Include "Inventory", "Network", "System Configuration"
   ```

**Verification:** Cluster validation completes with no failures; warnings are acceptable but review them for potential issues.

## Create availability group

Manually create the AG as you normally would, by using [SQL Server Management Studio](/sql/database-engine/availability-groups/windows/use-the-availability-group-wizard-sql-server-management-studio), [PowerShell](/sql/database-engine/availability-groups/windows/create-an-availability-group-sql-server-powershell), or [Transact-SQL](/sql/database-engine/availability-groups/windows/create-an-availability-group-transact-sql). 

>[!IMPORTANT]
> Do *not* create a listener at this time because the listener registration is done in a later section.  

## Create internal load balancer

[!INCLUDE [sql-ag-use-dnn-listener](../../includes/sql-ag-use-dnn-listener.md)]

The AG listener requires an internal instance of Azure Load Balancer. The internal load balancer provides a "floating" IP address for the AG listener that allows for faster failover and reconnection. The internal load balancer should be in the same virtual network as the SQL Server VM instances. 

[!INCLUDE [sql-vm-basic-load-balancer-retired](../../includes/sql-vm-basic-load-balancer-retired.md)]

The following code snippet creates the internal load balancer:

# [Azure CLI](#tab/azure-cli)

Use the [az network lb create](/cli/azure/network/lb#az-network-lb-create) Azure CLI command to create the internal load balancer.

```azurecli-interactive
# Create the internal load balancer
# example: az network lb create --name sqlILB -g SQLVM-RG --sku Standard `
# --vnet-name SQLVMvNet --subnet default

az network lb create --name sqlILB -g <resource group name> --sku Standard `
  --vnet-name <VNet Name> --subnet <subnet name>
```

# [PowerShell](#tab/azure-powershell)

Use the [New-AzLoadBalancer](/powershell/module/az.network/new-azloadbalancer) PowerShell command to create the internal load balancer.

```powershell-interactive
# Create the internal load balancer
# example: New-AzLoadBalancer -name sqlILB -ResourceGroupName SQLVM-RG  `
#  -Sku Standard -Location West US

$intLb = New-AzLoadBalancer -name <load balancer name> -ResourceGroupName <resource group name> `
   -Sku Standard -Location SouthCentralUS
```

**Key flag:** `-Sku Standard` creates a Standard Load Balancer (required for availability zones and advanced features).

---

**Verification:** The load balancer is created with `ProvisioningState: Succeeded` and appears in the virtual network's resource group.

## Create listener

After you manually create the AG, you can create the listener.

The *subnet resource ID* is the value of `/subnets/<subnetname>` appended to the resource ID of the virtual network resource. To identify the subnet resource ID:
   1. Go to your resource group in the [Azure portal](https://portal.azure.com). 
   1. Select the virtual network resource. 
   1. Select **Properties** in the **Settings** pane. 
   1. Identify the resource ID for the virtual network and append `/subnets/<subnetname>` to the end of the virtual network resource ID to create the subnet resource ID. For example:
      - Your virtual network resource ID is:
        `/subscriptions/a1a1-1a11a/resourceGroups/SQLVM-RG/providers/Microsoft.Network/virtualNetworks/SQLVMvNet`
      - Your subnet name is: `default`
      - Therefore, your subnet resource ID is:
        `/subscriptions/a1a1-1a11a/resourceGroups/SQLVM-RG/providers/Microsoft.Network/virtualNetworks/SQLVMvNet/subnets/default`


The following code snippet creates the availability group listener:

# [Azure CLI](#tab/azure-cli)

Use the [az sql vm group ag-listener create](/cli/azure/sql/vm/group/ag-listener#az-sql-vm-group-ag-listener-create) Azure CLI command to create the AG listener.

**Key parameters:**

| Parameter | Example value / Default |
|-----------|------------------------|
| `--ag-name` | `SQLAG` |
| `--group-name` | Cluster name from metadata |
| `--ip-address` | `10.0.0.27` (AG listener IP address, available IP in subnet) |
| `--load-balancer` | Load balancer name |
| `--probe-port` | `59999` (default) |
| `--subnet` | Subnet resource ID (see below) |
| `--sqlvms` | `sqlvm1 sqlvm2` |

```azurecli-interactive
# Create the AG listener
# example: az sql vm group ag-listener create -n AGListener -g SQLVM-RG `
#  --ag-name SQLAG --group-name Cluster --ip-address 10.0.0.27 `
#  --load-balancer sqlilb --probe-port 59999  `
#  --subnet /subscriptions/a1a1-1a11a/resourceGroups/SQLVM-RG/providers/Microsoft.Network/virtualNetworks/SQLVMvNet/subnets/default `
#  --sqlvms sqlvm1 sqlvm2

az sql vm group ag-listener create -n <listener name> -g <resource group name> `
  --ag-name <availability group name> --group-name <cluster name> --ip-address <ag listener IP address> `
  --load-balancer <lbname> --probe-port <Load Balancer probe port, default 59999>  `
  --subnet <subnet resource id> `
  --sqlvms <names of SQL VM's hosting AG replicas, ex: sqlvm1 sqlvm2>
```

# [PowerShell](#tab/azure-powershell)

Use the [New-AzAvailabilityGroupListener](/powershell/module/az.sqlvirtualmachine/new-azavailabilitygrouplistener) and [New-AzSqlVirtualMachineAgReplicaObject](/powershell/module/az.sqlvirtualmachine/new-azsqlvirtualmachineagreplicaobject) PowerShell commands to create the AG listener.

**Key parameters:**

| Parameter | Example value / Default |
|-----------|------------------------|
| `-ResourceGroupName` | Resource group name |
| `-SqlVMGroupName` | Cluster name |
| `-Name` | Listener name |
| `-AvailabilityGroupName` | AG name |
| `-IpAddress` | `192.168.15.201` (AG listener IP address, available IP in subnet) |
| `-LoadBalancerResourceId` | `$intLb.id` (from load balancer creation) |
| `-SubnetId` | `$SubnetResource.Id` (subnet resource ID) |
| `-ProbePort` | `59999` (default) |
| `-AvailabilityGroupConfigurationReplica` | `$AgReplica1, $AgReplica2` (replica objects) |
| `-SqlVirtualMachineId` | `$sqlvm1.id,$sqlvm2.id` (VM resource IDs) |

```powershell-interactive
# Define replica objects for primary and secondary roles
$AgReplica1 = New-AzSqlVirtualMachineAgReplicaObject -Commit 'SYNCHRONOUS_COMMIT' `
   -Failover 'AUTOMATIC' -ReadableSecondary 'NO' -Role 'PRIMARY' `
   -SqlVirtualMachineInstanceId $sqlvm1.id
$AgReplica2 = New-AzSqlVirtualMachineAgReplicaObject -Commit 'SYNCHRONOUS_COMMIT' `
   -Failover 'AUTOMATIC' -ReadableSecondary 'NO' -Role 'SECONDARY' `
   -SqlVirtualMachineInstanceId $sqlvm2.id
```

**Key flags:** `-Commit 'SYNCHRONOUS_COMMIT'` ensures data consistency; `-Failover 'AUTOMATIC'` enables automatic failover.

```powershell-interactive
# Get subnet resource for listener placement
$SubnetResource = Get-AzVirtualNetworkSubnetConfig -Name <Subnet Name> `
   -VirtualNetwork (Get-AzVirtualNetwork -Name <virtual network name> `
   -ResourceGroupName <resource group name>)
```

**Key command:** Retrieves subnet ID where the listener IP will be registered.

```powershell-interactive
# Create the availability group listener
New-AzAvailabilityGroupListener -ResourceGroupName <resource group name> `
   -SqlVMGroupName <Cluster Name> `
   -Name <Listener Name> `
   -AvailabilityGroupName <Availability Group Name> `
   -IpAddress <IP Address> `
   -LoadBalancerResourceId $intLb.id `
   -SubnetId $SubnetResource.Id `
   -ProbePort 59999 `
   -AvailabilityGroupConfigurationReplica $AgReplica1, $AgReplica2 `
   -SqlVirtualMachineId $sqlvm1.id,$sqlvm2.id
```

**Key flags:** `-LoadBalancerResourceId` links to internal load balancer; `-ProbePort 59999` sets health probe port for failover detection.

---


## Configure probe port

[!INCLUDE [virtual-machines-port-exclusion](../../includes/virtual-machines-port-exclusion.md)]

**Verification:** The port exclusion is added successfully.

## Add a replica

There's an added layer of complexity when you're deploying an AG to SQL Server VMs hosted in Azure. The resource provider and the virtual machine group now manage the resources. As such, when you're adding or removing replicas in the AG, there's an additional step of updating the AG listener (AG listener registered through SQL IaaS Agent extension) metadata with information about the SQL Server VMs. When you're modifying the number of replicas in the AG, you must also use the [az sql vm group ag-listener update](/cli/azure/sql/vm/group/ag-listener#az-sql-vm-group-ag-listener-update) command to update the AG listener metadata with the metadata of the SQL Server VMs. 

To add a new replica to the AG:

1. Add the SQL Server VM to the WSFC group. Replace `<password>` with a valid password.

   ```azurecli-interactive

   # Add the SQL Server VM to the WSFC group
   # example: az sql vm add-to-group -n SQLVM3 -g SQLVM-RG --sqlvm-group Cluster `
   # -b <password> -p <password> -s <password>

   az sql vm add-to-group -n <VM3 Name> -g <Resource Group Name> --sqlvm-group <cluster name> `
   -b <bootstrap account password> -p <operator account password> -s <service account password>
   ```

1. Use SSMS to add the SQL Server instance as a replica within the AG.
1. Add the SQL Server VM metadata to the AG listener:

   ```azurecli-interactive
   # Update the listener metadata with the new VM
   # example: az sql vm group ag-listener update -n AGListener `
   # -g sqlvm-rg --group-name Cluster --sqlvms sqlvm1 sqlvm2 sqlvm3

   az sql vm group ag-listener update -n <Listener> `
   -g <RG name> --group-name <cluster name> --sqlvms <SQL VMs, along with new SQL VM>
   ```

## Remove a replica

There's an added layer of complexity when you're deploying an AG to SQL Server VMs hosted in Azure. The resource provider and the virtual machine group now manage the resources. As such, when you're adding or removing replicas in the AG, there's an additional step of updating the AG listener (AG listener registered through SQL IaaS Agent extension) metadata with information about the SQL Server VMs. When you're modifying the number of replicas in the AG, you must also use the [az sql vm group ag-listener update](/cli/azure/sql/vm/group/ag-listener#az-sql-vm-group-ag-listener-update) command to update the AG listener metadata with the metadata of the SQL Server VMs. 

To remove a replica from the AG:

1. Remove the replica from the AG by using SSMS. 
1. Remove the SQL Server VM metadata from the AG listener:
   ```azurecli-interactive
   # Update the listener metadata by removing the VM from the SQLVMs list
   # example: az sql vm group ag-listener update -n AGListener `
   # -g sqlvm-rg --group-name Cluster --sqlvms sqlvm1 sqlvm2

   az sql vm group ag-listener update -n <Listener> `
   -g <RG name> --group-name <cluster name> --sqlvms <SQL VMs that remain>
   ```
1. Remove the SQL Server VM from the WSFC:
   ```azurecli-interactive
   # Remove the SQL VM from the WSFC
   # example: az sql vm remove-from-group --name SQLVM3 --resource-group SQLVM-RG

   az sql vm remove-from-group --name <SQL VM name> --resource-group <RG name> 
   ```

## Remove listener
If you later need to remove the AG listener configured with the Azure CLI, you must go through the SQL IaaS Agent extension. Because the AG listener is registered through the SQL IaaS Agent extension, just deleting the AG listener via SSMS is insufficient. 

The best method is to delete the AG listener through the SQL IaaS Agent extension by using the following code snippet in the Azure CLI. Doing so removes the AG listener metadata from the SQL IaaS Agent extension. The deletion also physically removes the AG listener from the AG. 

# [Azure CLI](#tab/azure-cli)

Use the [az sql vm group ag-listener delete](/cli/azure/sql/vm/group/ag-listener#az-sql-vm-group-ag-listener-delete) Azure CLI command to remove the AG listener.

```azurecli-interactive
# Remove the AG listener
# example: az sql vm group ag-listener delete --group-name Cluster --name AGListener --resource-group SQLVM-RG

az sql vm group ag-listener delete --group-name <cluster name> --name <listener name > --resource-group <resource group name>
```

# [PowerShell](#tab/azure-powershell)

Use the [Remove-AzAvailabilityGroupListener](/powershell/module/az.sqlvirtualmachine/remove-azavailabilitygrouplistener) PowerShell command to remove the AG listener.

```powershell-interactive
# Remove the AG listener
# example: Remove-AzAvailabilityGroupListener -Name AGListener `
#   -ResourceGroupName SQLVM-RG -SqlVMGroupName Cluster

Remove-AzAvailabilityGroupListener -Name <Listener> `
   -ResourceGroupName <Resource Group Name> -SqlVMGroupName <cluster name>
```

---

## Remove cluster

Remove all of the nodes from the WSFC to destroy the WSFC, and then remove the WSFC metadata from the SQL IaaS Agent extension. You can do so by using the Azure CLI or PowerShell. 

> [!NOTE]
> If the SQL Server VMs removed are the only VMs in the WSFC, the WSFC is destroyed. If there are any other VMs in the WSFC apart from the SQL Server VMs that were removed, the other VMs aren't removed and the WSFC isn't destroyed.

# [Azure CLI](#tab/azure-cli)

Use the [az sql vm remove-from-group](/cli/azure/sql/vm#az-sql-vm-remove-from-group) Azure CLI command to remove SQL Server VMs from the WSFC.

First, remove all of the SQL Server VMs from the WSFC: 

```azurecli-interactive
# Remove the VM from the WSFC metadata
# example: az sql vm remove-from-group --name SQLVM2 --resource-group SQLVM-RG

az sql vm remove-from-group --name <VM1 name>  --resource-group <resource group name>
az sql vm remove-from-group --name <VM2 name>  --resource-group <resource group name>
```

Next, remove the WSFC metadata from the SQL IaaS Agent extension: 

```azurecli-interactive
# Remove the cluster from the SQL VM RP metadata
# example: az sql vm group delete --name Cluster --resource-group SQLVM-RG

az sql vm group delete --name <cluster name> --resource-group <resource group name>
```


# [PowerShell](#tab/azure-powershell)

Use the [Update-AzSqlVM](/powershell/module/az.sqlvirtualmachine/update-azsqlvm) PowerShell command to remove SQL Server VMs from the WSFC.

First, remove all of the SQL Server VMs from the WSFC: 

```powershell-interactive
# Remove the SQL VM from the WSFC
# example: 
# Update-AzSqlVM -ResourceGroupName SQLVM-RG -Name $sqlvm1 `
#     -SqlVirtualMachineGroupResourceId '' 
# Update-AzSqlVM -ResourceGroupName SQLVM-RG -Name $sqlvm2 `
#     -SqlVirtualMachineGroupResourceId '' 

Update-AzSqlVM -ResourceGroupName <resource group name> -Name <VM1 Name> `
   -SqlVirtualMachineGroupResourceId '' 
Update-AzSqlVM -ResourceGroupName <resource group name> -Name <VM2 Name> `
   -SqlVirtualMachineGroupResourceId '' 
```

Next, remove the WSFC metadata from the SQL IaaS Agent extension: 

```powershell-interactive
# Remove the cluster metadata
# example: Remove-AzSqlVMGroup -ResourceGroupName "SQLVM-RG" -Name "Cluster"

Remove-AzSqlVMGroup -ResourceGroupName "<resource group name>" -Name "<cluster name>"
```

---

## Next steps

Once the AG is deployed, consider optimizing the [HADR settings for SQL Server on Azure VMs](hadr-cluster-best-practices.md). 


To learn more, see:

- [Windows Server Failover Cluster with SQL Server on Azure VMs](hadr-windows-server-failover-cluster-overview.md)
- [Always On availability groups with SQL Server on Azure VMs](availability-group-overview.md)
- [Always On availability groups overview](/sql/database-engine/availability-groups/windows/overview-of-always-on-availability-groups-sql-server)
