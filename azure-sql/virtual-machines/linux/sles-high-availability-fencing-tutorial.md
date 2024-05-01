---
title: Configure availability groups for SQL Server on SLES virtual machines in Azure - Linux virtual machines
description: Learn about setting up high availability in an SLES cluster environment and set up STONITH
author: aravindmahadevan-ms
ms.author: armaha
ms.reviewer: amitkh-msft, randolphwest
ms.date: 11/29/2023
ms.service: virtual-machines-sql
ms.subservice: hadr
ms.topic: tutorial
ms.custom: devx-track-azurecli, linux-related-content
---
# Tutorial: Configure availability groups for SQL Server on SLES virtual machines in Azure

[!INCLUDE [appliesto-sqlvm](../../includes/appliesto-sqlvm.md)]

> [!NOTE]  
> We use [!INCLUDE [sssql22-md](../../../docs/includes/sssql22-md.md)] with SUSE Linux Enterprise Server (SLES) v15 in this tutorial, but it's possible to use [!INCLUDE [sssql19-md](../../../docs/includes/sssql19-md.md)] with SLES v12 or SLES v15, to configure high availability.

In this tutorial, you learn how to:

> [!div class="checklist"]
> - Create a new resource group, availability set, and Linux virtual machines (VMs)
> - Enable high availability (HA)
> - Create a Pacemaker cluster
> - Configure a fencing agent by creating a STONITH device
> - Install SQL Server and mssql-tools on SLES
> - Configure SQL Server Always On availability group
> - Configure availability group (AG) resources in the Pacemaker cluster
> - Test a failover and the fencing agent

This tutorial uses the Azure CLI to deploy resources in Azure.

If you don't have an Azure subscription, create a [free account](https://azure.microsoft.com/free/?WT.mc_id=A261C142F) before you begin.

[!INCLUDE [azure-cli-prepare-your-environment.md](~/../azure-sql/reusable-content/azure-cli/azure-cli-prepare-your-environment.md)]

- This article requires version 2.0.30 or later of the Azure CLI. If using Azure Cloud Shell, the latest version is already installed.

## Create a resource group

If you've more than one subscription, [set the subscription](/cli/azure/manage-azure-subscriptions-azure-cli) that you want deploy these resources to.

Use the following command to create a resource group `<resourceGroupName>` in a region. Replace `<resourceGroupName>` with a name of your choosing. This tutorial uses `East US 2`. For more information, see the following [Quickstart](/azure/application-gateway/quick-create-cli).

```azurecli-interactive
az group create --name <resourceGroupName> --location eastus2
```

## Create an availability set

The next step is to create an availability set. Run the following command in Azure Cloud Shell, and replace `<resourceGroupName>` with your resource group name. Choose a name for `<availabilitySetName>`.

```azurecli-interactive
az vm availability-set create \
    --resource-group <resourceGroupName> \
    --name <availabilitySetName> \
    --platform-fault-domain-count 2 \
    --platform-update-domain-count 2
```

You should get the following results once the command completes:

```output
{
  "id": "/subscriptions/<subscriptionId>/resourceGroups/<resourceGroupName>/providers/Microsoft.Compute/availabilitySets/<availabilitySetName>",
  "location": "eastus2",
  "name": "<availabilitySetName>",
  "platformFaultDomainCount": 2,
  "platformUpdateDomainCount": 2,
  "proximityPlacementGroup": null,
  "resourceGroup": "<resourceGroupName>",
  "sku": {
    "capacity": null,
    "name": "Aligned",
    "tier": null
  },
  "statuses": null,
  "tags": {},
  "type": "Microsoft.Compute/availabilitySets",
  "virtualMachines": []
}
```

## Create a virtual network and subnet

1. Create a named subnet with a pre-assigned IP address range. Replace these values in the following command:

   - `<resourceGroupName>`
   - `<vNetName>`
   - `<subnetName>`

   ```azurecli-interactive
   az network vnet create \
       --resource-group <resourceGroupName> \
       --name <vNetName> \
       --address-prefix 10.1.0.0/16 \
       --subnet-name <subnetName> \
       --subnet-prefix 10.1.1.0/24
   ```

   The previous command creates a VNet and a subnet containing a custom IP range.

## Create SLES VMs inside the availability set

1. Get a list of virtual machine images that offer SLES v15 SP4 with BYOS (bring your own subscription). You can also use the SUSE Enterprise Linux 15 SP4 + Patching VM (`sles-15-sp4-basic`).

   ```azurecli-interactive
   az vm image list --all --offer "sles-15-sp3-byos"
   # if you want to search the basic offers you could search using the command below
   az vm image list --all --offer "sles-15-sp3-basic"
   ```

   You should see the following results when you search for the BYOS images:

   ```output
   [
      {
         "offer": "sles-15-sp3-byos",
         "publisher": "SUSE",
         "sku": "gen1",
         "urn": "SUSE:sles-15-sp3-byos:gen1:2022.05.05",
         "version": "2022.05.05"
      },
      {
         "offer": "sles-15-sp3-byos",
         "publisher": "SUSE",
         "sku": "gen1",
         "urn": "SUSE:sles-15-sp3-byos:gen1:2022.07.19",
         "version": "2022.07.19"
      },
      {
         "offer": "sles-15-sp3-byos",
         "publisher": "SUSE",
         "sku": "gen1",
         "urn": "SUSE:sles-15-sp3-byos:gen1:2022.11.10",
         "version": "2022.11.10"
      },
      {
         "offer": "sles-15-sp3-byos",
         "publisher": "SUSE",
         "sku": "gen2",
         "urn": "SUSE:sles-15-sp3-byos:gen2:2022.05.05",
         "version": "2022.05.05"
      },
      {
         "offer": "sles-15-sp3-byos",
         "publisher": "SUSE",
         "sku": "gen2",
         "urn": "SUSE:sles-15-sp3-byos:gen2:2022.07.19",
         "version": "2022.07.19"
      },
      {
         "offer": "sles-15-sp3-byos",
         "publisher": "SUSE",
         "sku": "gen2",
         "urn": "SUSE:sles-15-sp3-byos:gen2:2022.11.10",
         "version": "2022.11.10"
      }
   ]
   ```

   This tutorial uses `SUSE:sles-15-sp3-byos:gen1:2022.11.10`.

   > [!IMPORTANT]  
   > Machine names must be less than 15 characters in length to set up an availability group. Usernames can't contain upper case characters, and passwords must have between 12 and 72 characters.

1. Create three VMs in the availability set. Replace these values in the following command:

   - `<resourceGroupName>`
   - `<VM-basename>`
   - `<availabilitySetName>`
   - `<VM-Size>` - An example would be "Standard_D16s_v3"
   - `<username>`
   - `<adminPassword>`
   - `<vNetName>`
   - `<subnetName>`

   ```azurecli-interactive
   for i in `seq 1 3`; do
       az vm create \
          --resource-group <resourceGroupName> \
          --name <VM-basename>$i \
          --availability-set <availabilitySetName> \
          --size "<VM-Size>" \
          --os-disk-size-gb 128 \
          --image "SUSE:sles-15-sp3-byos:gen1:2022.11.10" \
          --admin-username "<username>" \
          --admin-password "<adminPassword>" \
          --authentication-type all \
          --generate-ssh-keys \
          --vnet-name "<vNetName>" \
          --subnet "<subnetName>" \
          --public-ip-sku Standard \
          --public-ip-address ""
       done
   ```

The previous command creates the VMs using the previously defined VNet. For more information on the different configurations, see the [az vm create](/cli/azure/vm) article.

The command also includes the `--os-disk-size-gb` parameter to create a custom OS drive size of 128 GB. If you increase this size later, expand appropriate folder volumes to accommodate your installation, [configure the Logical Volume Manager (LVM)](/previous-versions/azure/virtual-machines/linux/configure-lvm).

You should get results similar to the following once the command completes for each VM:

```output
{
  "fqdns": "",
  "id": "/subscriptions/<subscriptionId>/resourceGroups/<resourceGroupName>/providers/Microsoft.Compute/virtualMachines/sles1",
  "location": "westus",
  "macAddress": "<Some MAC address>",
  "powerState": "VM running",
  "privateIpAddress": "<IP1>",
  "resourceGroup": "<resourceGroupName>",
  "zones": ""
}
```

### Test connection to the created VMs

Connect to each of the VMs using the following command in Azure Cloud Shell. If you're unable to find your VM IPs, follow this [Quickstart on Azure Cloud Shell](/azure/cloud-shell/quickstart#ssh-into-your-linux-vm).

```azurecli-interactive
ssh <username>@<publicIPAddress>
```

If the connection is successful, you should see the following output representing the Linux terminal:

```output
[<username>@sles1 ~]$
```

Type `exit` to leave the SSH session.

## Register with SUSEConnect and install high availability packages

In order to complete this tutorial, your VMs must be registered with SUSEConnect to receive updates and support. You can then install the High Availability Extension module, or *pattern*, which is a set of packages that enables HA.

It's easier to open an SSH session on each of the VMs (nodes) simultaneously, as the same commands must be run on each VM throughout the article.

If you're copying and pasting multiple `sudo` commands and are prompted for a password, the additional commands won't run. Run each command separately.

Connect to each VM node to run the following steps.

### Register the VM with SUSEConnect

To register your VM node with SUSEConnect, replace these values in the following command, on all the nodes:

- `<subscriptionEmailAddress>`
- `<registrationCode>`

```bash
sudo SUSEConnect
    --url=https://scc.suse.com
    -e <subscriptionEmailAddress> \
    -r <registrationCode>
```

### Install High Availability Extension

To install the High Availability Extension, run the following command on all the nodes:

```bash
sudo SUSEConnect -p sle-ha/15.3/x86_64 -r <registration code for Partner Subscription for High Availability Extension>
```

## Configure passwordless SSH access between nodes

Passwordless SSH access allows your VMs to communicate with each other using SSH public keys. You must configure SSH keys on each node, and copy those keys to each node.

### Generate new SSH keys

The required SSH key size is 4,096 bits. On each VM, change to the `/root/.ssh` folder, and run the following command:

```bash
ssh-keygen -t rsa -b 4096
```

During this step, you might be prompted to overwrite an existing SSH file. You must agree to this prompt. You don't need to enter a passphrase.

### Copy the public SSH keys

On each VM, you must copy the public key from the node you just created, using the `ssh-copy-id` command. If you want to specify the target directory on the target VM, you can use the `-i` parameter.

In the following command, the `<username>` account can be the same account you configured for each node when creating the VM. You can also use the `root` account, but this isn't recommended in a production environment.

```bash
sudo ssh-copy-id <username>@sles1
sudo ssh-copy-id <username>@sles2
sudo ssh-copy-id <username>@sles3
```

### Verify passwordless access from each node

To confirm that the SSH public key was copied to each node, use the `ssh` command from each node. If you copied the keys correctly, you won't be prompted for a password, and the connection will be successful.

In this example, we are connecting to the second and third nodes from the first VM (`sles1`). Once again the `<username>` account can be the same account you configured for each node when creating the VM

```bash
ssh <username>@sles2
ssh <username>@sles3
```

Repeat this process from all three nodes, so that each node can communicate with the others without requiring passwords.

### Configure name resolution

You can configure name resolution using either DNS, or by manually editing the `etc/hosts` file on each node.

For more information about DNS and Active Directory, see [Join SQL Server on a Linux host to an Active Directory domain](/sql/linux/sql-server-linux-active-directory-join-domain).

> [!IMPORTANT]  
> We recommend that you use your **private IP** address in the previous example. Using the public IP address in this configuration will cause the setup to fail, and would expose your VM to external networks.
>  
> The VMs and their IP address used in this example are listed as follows:
>  
> - `sles1`: 10.0.0.85
> - `sles2`: 10.0.0.86
> - `sles3`: 10.0.0.87

## Configure the cluster

For this tutorial, your first VM (`sles1`) is **node 1**, your second VM (`sles2`) is **node 2**, and your third VM (`sles3`) is **node 3**. For more information on cluster installation, see [Set up Pacemaker on SUSE Linux Enterprise Server in Azure](/azure/sap/workloads/high-availability-guide-suse-pacemaker#use-an-azure-fence-agent-1).

### Cluster installation

1. Run the following command to install the `ha-cluster-bootstrap` package on node 1, and then restart the node. In this example, it's the `sles1` VM.

   ```bash
   sudo zypper install ha-cluster-bootstrap
   ```

   After the node is restarted, run the following command to deploy the cluster:

   ```bash
   sudo crm cluster init --name sqlcluster
   ```

   You'll see a similar output to the following example:

   ```output
   Do you want to continue anyway (y/n)? y
     Generating SSH key for root
     The user 'hacluster' will have the login shell configuration changed to /bin/bash
   Continue (y/n)? y
     Generating SSH key for hacluster
     Configuring csync2
     Generating csync2 shared key (this may take a while)...done
     csync2 checking files...done
     Detected cloud platform: microsoft-azure

   Configure Corosync (unicast):
     This will configure the cluster messaging layer.  You will need
     to specify a network address over which to communicate (default
     is eth0's network, but you can use the network address of any
     active interface).

     Address for ring0 [10.0.0.85]
     Port for ring0 [5405]

   Configure SBD:
     If you have shared storage, for example a SAN or iSCSI target,
     you can use it avoid split-brain scenarios by configuring SBD.
     This requires a 1 MB partition, accessible to all nodes in the
     cluster.  The device path must be persistent and consistent
     across all nodes in the cluster, so /dev/disk/by-id/* devices
     are a good choice.  Note that all data on the partition you
     specify here will be destroyed.

   Do you wish to use SBD (y/n)? n
   WARNING: Not configuring SBD - STONITH will be disabled.
     Hawk cluster interface is now running. To see cluster status, open:
       https://10.0.0.85:7630/
     Log in with username 'hacluster', password 'linux'
   WARNING: You should change the hacluster password to something more secure!
     Waiting for cluster..............done
     Loading initial cluster configuration

   Configure Administration IP Address:
     Optionally configure an administration virtual IP
     address. The purpose of this IP address is to
     provide a single IP that can be used to interact
     with the cluster, rather than using the IP address
     of any specific cluster node.

   Do you wish to configure a virtual IP address (y/n)? y
     Virtual IP []10.0.0.89
     Configuring virtual IP (10.0.0.89)....done

   Configure Qdevice/Qnetd:
     QDevice participates in quorum decisions. With the assistance of
     a third-party arbitrator Qnetd, it provides votes so that a cluster
     is able to sustain more node failures than standard quorum rules
     allow. It is recommended for clusters with an even number of nodes
     and highly recommended for 2 node clusters.

   Do you want to configure QDevice (y/n)? n
   Done (log saved to /var/log/crmsh/ha-cluster-bootstrap.log)
   ```

1. Check the status of the cluster on **node 1** using the following command:

    ```bash
    sudo crm status
    ```

   Your output should include the following text if it was successful:

   ```output
   1 node configured
   1 resource instance configured
   ```

1. On **all nodes**, change the password for `hacluster` to something more secure using the following command. You must also change your `root` user password:

   ```bash
   sudo passwd hacluster
   ```

   ```bash
   sudo passwd root
   ```

1. Run the following command on **node 2** and **node 3** to first install the `crmsh` package:

   ```bash
   sudo zypper install crmsh
   ```

   Now, run the command to join the cluster:

   ```bash
   sudo crm cluster join
   ```

   Here are some of the interactions to expect:

   ```output
   Join This Node to Cluster:
   You will be asked for the IP address of an existing node, from which
   configuration will be copied.  If you have not already configured
   passwordless ssh between nodes, you will be prompted for the root
   password of the existing node.

     IP address or hostname of existing node (e.g.: 192.168.1.1) []10.0.0.85
     Configuring SSH passwordless with root@10.0.0.85
     root@10.0.0.85's password:
     Configuring SSH passwordless with hacluster@10.0.0.85
     Configuring csync2...done
   Merging known_hosts
   WARNING: scp to sles2 failed (Exited with error code 1, Error output: The authenticity of host 'sles2 (10.1.1.5)' can't be established.
   ECDSA key fingerprint is SHA256:UI0iyfL5N6X1ZahxntrScxyiamtzsDZ9Ftmeg8rSBFI.
   Are you sure you want to continue connecting (yes/no/[fingerprint])?
   lost connection
   ), known_hosts update may be incomplete
   Probing for new partitions...done
     Address for ring0 [10.0.0.86]

   Hawk cluster interface is now running. To see cluster status, open:
       https://10.0.0.86:7630/
     Log in with username 'hacluster', password 'linux'
   WARNING: You should change the hacluster password to something more secure!
   Waiting for cluster.....done
   Reloading cluster configuration...done
     Done (log saved to /var/log/crmsh/ha-cluster-bootstrap.log)
   ```

1. Once you've joined all machines to the cluster, check your resource to see if all VMs are online:

   ```bash
   sudo crm status
   ```

   You should see the following output:

   ```output
   Stack: corosync
    Current DC: sles1 (version 2.0.5+20201202.ba59be712-150300.4.30.3-2.0.5+20201202.ba59be712) - partition with quorum
    Last updated: Mon Mar  6 18:01:17 2023
    Last change:  Mon Mar  6 17:10:09 2023 by root via cibadmin on sles1

   3 nodes configured
   1 resource instance configured

   Online: [ sles1 sles2 sles3 ]

   Full list of resources:

    admin-ip       (ocf::heartbeat:IPaddr2):       Started sles1
   ```

1. Install the cluster resource component. Run the following command on **all nodes**.

   ```bash
   sudo zypper in socat
   ```

1. Install the `azure-lb` component. Run the following command on **all nodes**.

   ```bash
   sudo zypper in resource-agents
   ```

1. Configure the operating system. Go through the following steps on **all nodes**.

   1. Edit the configuration file:

      ```bash
      sudo vi /etc/systemd/system.conf
      ```

   1. Change the `DefaultTasksMax` value to `4096`:

      ```ini
      #DefaultTasksMax=512
      DefaultTasksMax=4096
      ```

   1. Save and exit the **vi** editor.

   1. To activate this setting, run the following command:

      ```bash
      sudo systemctl daemon-reload
      ```

   1. Test if the change was successful:

      ```bash
      sudo systemctl --no-pager show | grep DefaultTasksMax
      ```

1. Reduce the size of the dirty cache. Go through the following steps on **all nodes**.

   1. Edit the system control configuration file:

      ```bash
      sudo vi /etc/sysctl.conf
      ```

   1. Add the following two lines to the file:

      ```ini
      vm.dirty_bytes = 629145600
      vm.dirty_background_bytes = 314572800
      ```

   1. Save and exit the **vi** editor.

1. Install the Azure Python SDK on **all nodes** with the following commands:

   ```bash
   sudo zypper install fence-agents
   # Install the Azure Python SDK on SLES 15 or later:
   # You might need to activate the public cloud extension first. In this example, the SUSEConnect command is for SLES 15 SP1
   SUSEConnect -p sle-module-public-cloud/15.1/x86_64
   sudo zypper install python3-azure-mgmt-compute
   sudo zypper install python3-azure-identity
   ```

## Configure fencing agent

A STONITH device provides a fencing agent. The below instructions are modified for this tutorial. For more information, see [Create an Azure fence agent STONITH device](/azure/virtual-machines/workloads/sap/high-availability-guide-suse-pacemaker#create-an-azure-fence-agent-stonith-device).

Check the version of the Azure fence agent to ensure that it's updated. Use the following command:

```bash
sudo zypper info resource-agents
```

You should see a similar output to the below example.

```output
Information for package resource-agents:
----------------------------------------
Repository     : SLE-Product-HA15-SP3-Updates
Name           : resource-agents
Version        : 4.8.0+git30.d0077df0-150300.8.37.1
Arch           : x86_64
Vendor         : SUSE LLC <https://www.suse.com/>
Support Level  : Level 3
Installed Size : 2.5 MiB
Installed      : Yes (automatically)
Status         : up-to-date
Source package : resource-agents-4.8.0+git30.d0077df0-150300.8.37.1.src
Upstream URL   : http://linux-ha.org/
Summary        : HA Reusable Cluster Resource Scripts
Description    : A set of scripts to interface with several services
                 to operate in a High Availability environment for both
                 Pacemaker and rgmanager service managers.
```

### <a id="register-new-application-in-azure-active-directory"></a> Register new application in Microsoft Entra ID

To register a new application in Microsoft Entra ID ([formerly Azure Active Directory](/entra/fundamentals/new-name)), follow these steps:

1. Go to <https://portal.azure.com>.
1. Open the [Microsoft Entra ID Properties pane](https://portal.azure.com/#blade/Microsoft_AAD_IAM/ActiveDirectoryMenuBlade/Properties) and write down the `Tenant ID`.
1. Select **[App registrations](https://portal.azure.com/#blade/Microsoft_AAD_RegisteredApps/ApplicationsListBlade)**.
1. Select **New registration**.
1. Enter a **Name** like `<resourceGroupName>-app`. For **supported account types** select **Accounts in this organizational directory only (Microsoft only - Single tenant)**.
1. Select *Web* for **Redirect URI**, and enter a URL (for example, http://localhost) and select **Add**. The sign-on URL can be any valid URL. Once done, select **Register**.
1. Choose **Certificates and secrets** for your new App registration, then select **New client secret**.
1. Enter a description for a new key (client secret), and then select **Add**.
1. Write down the value of the secret. It's used as the password for the *service principal*.
1. Select **Overview**. Write down the Application ID. It's used as the username (login ID in the steps below) of the service principal.

### Create custom role for the fence agent

Follow the tutorial to [Create an Azure custom role using Azure CLI](/azure/role-based-access-control/tutorial-custom-role-cli#create-a-custom-role).

Your JSON file should look similar to the following example.

- Replace `<username>` with a name of your choice. This is to avoid any duplication when creating this role definition.
- Replace `<subscriptionId>` with your Azure Subscription ID.

```json
{
  "Name": "Linux Fence Agent Role-<username>",
  "Id": null,
  "IsCustom": true,
  "Description": "Allows to power-off and start virtual machines",
  "Actions": [
    "Microsoft.Compute/*/read",
    "Microsoft.Compute/virtualMachines/powerOff/action",
    "Microsoft.Compute/virtualMachines/start/action"
  ],
  "NotActions": [
  ],
  "AssignableScopes": [
    "/subscriptions/<subscriptionId>"
  ]
}
```

To add the role, run the following command:

- Replace `<filename>` with the name of the file.
- If you're executing the command from a path other than the folder that the file is saved to, include the folder path of the file in the command.

```bash
az role definition create --role-definition "<filename>.json"
```

You should see the following output:

```output
{
  "assignableScopes": [
    "/subscriptions/<subscriptionId>"
  ],
  "description": "Allows to power-off and start virtual machines",
  "id": "/subscriptions/<subscriptionId>/providers/Microsoft.Authorization/roleDefinitions/<roleNameId>",
  "name": "<roleNameId>",
  "permissions": [
    {
      "actions": [
        "Microsoft.Compute/*/read",
        "Microsoft.Compute/virtualMachines/powerOff/action",
        "Microsoft.Compute/virtualMachines/start/action"
      ],
      "dataActions": [],
      "notActions": [],
      "notDataActions": []
    }
  ],
  "roleName": "Linux Fence Agent Role-<username>",
  "roleType": "CustomRole",
  "type": "Microsoft.Authorization/roleDefinitions"
}
```

### Assign the custom role to the service principal

Assign the custom role `Linux Fence Agent Role-<username>` that was created in the last step, to the service principal. Repeat these steps for **all nodes**.

> [!WARNING]  
> Don't use the Owner role from here on.

1. Go to <https://portal.azure.com>
1. Open the [All resources pane](https://portal.azure.com/#pane/HubsExtension/BrowseAll)
1. Select the virtual machine of the first cluster node
1. Select **Access control (IAM)**
1. Select **Add role assignments**
1. Select the role `Linux Fence Agent Role-<username>` from the **Role** list
1. Leave **Assign access to** as the default `Users, group, or service principal`.
1. In the **Select** list, enter the name of the application you created previously, for example `<resourceGroupName>-app`.
1. Select **Save**.

### Create the STONITH devices

1. Run the following commands on **node 1**:

   - Replace the `<ApplicationID>` with the ID value from your application registration.
   - Replace the `<servicePrincipalPassword>` with the value from the client secret.
   - Replace the `<resourceGroupName>` with the resource group from your subscription used for this tutorial.
   - Replace the `<tenantID>` and the `<subscriptionId>` from your Azure Subscription.

1. Run `crm configure` to open the **crm** prompt:

   ```bash
   sudo crm configure
   ```

1. In the **crm** prompt, run the following command to configure the resource properties, which creates the resource called `rsc_st_azure` as shown in the following example:

   ```bash
   primitive rsc_st_azure stonith:fence_azure_arm params subscriptionId="subscriptionID" resourceGroup="ResourceGroup_Name" tenantId="TenantID" login="ApplicationID" passwd="servicePrincipalPassword" pcmk_monitor_retries=4 pcmk_action_limit=3 power_timeout=240 pcmk_reboot_timeout=900 pcmk_host_map="sles1:sles1;sles2:sles2;sles3:sles3" op monitor interval=3600 timeout=120
   commit
   quit
   ```

1. Run the following commands to configure the fencing agent:

   ```bash
   sudo crm configure property stonith-timeout=900
   sudo crm configure property stonith-enabled=true
   sudo crm configure property concurrent-fencing=true
   ```

1. Check the status of your cluster to see that STONITH has been enabled:

   ```bash
   sudo crm status
   ```

   You should see output similar to the following text:

   ```output
   Stack: corosync
    Current DC: sles1 (version 2.0.5+20201202.ba59be712-150300.4.30.3-2.0.5+20201202.ba59be712) - partition with quorum
    Last updated: Mon Mar  6 18:20:17 2023
    Last change:  Mon Mar  6 18:10:09 2023 by root via cibadmin on sles1

   3 nodes configured
   2 resource instances configured

   Online: [ sles1 sles2 sles3 ]

   Full list of resources:

   admin-ip       (ocf::heartbeat:IPaddr2):       Started sles1
   rsc_st_azure   (stonith:fence_azure_arm):      Started sles2
   ```

## Install SQL Server and mssql-tools

Use the below section to install SQL Server and **mssql-tools**. For more information, see [Install SQL Server on SUSE Linux Enterprise Server](/sql/linux/quickstart-install-connect-suse).

Perform these steps on **all nodes** in this section.

### Install SQL Server on the VMs

The following commands are used to install SQL Server:

1. Download the Microsoft SQL Server 2019 SLES repository configuration file:

   ```bash
   sudo zypper addrepo -fc https://packages.microsoft.com/config/sles/15/mssql-server-2022.repo
   ```

1. Refresh your repositories.

   ```bash
   sudo zypper --gpg-auto-import-keys refresh
   ```

   To ensure that the Microsoft package signing key is installed on your system, use the following command to import the key:

   ```bash
   sudo rpm --import https://packages.microsoft.com/keys/microsoft.asc
   ```

1. Run the following commands to install SQL Server:

   ```bash
   sudo zypper install -y mssql-server
   ```

1. After the package installation finishes, run `mssql-conf setup` and follow the prompts to set the SA password and choose your edition.

   ```bash
   sudo /opt/mssql/bin/mssql-conf setup
   ```

   > [!NOTE]  
   > Make sure to specify a strong password for the SA account (Minimum length 8 characters, including uppercase and lowercase letters, base 10 digits and/or non-alphanumeric symbols).

1. Once the configuration is done, verify that the service is running:

   ```bash
   systemctl status mssql-server
   ```

### Install SQL Server command-line tools

The following steps install the SQL Server command-line tools, namely [sqlcmd](/sql/tools/sqlcmd-utility) and [bcp](/sql/tools/bcp-utility).

1. Add the Microsoft SQL Server repository to Zypper.

   ```bash
   sudo zypper addrepo -fc https://packages.microsoft.com/config/sles/15/prod.repo
   ```

1. Refresh your repositories.

   ```bash
   sudo zypper --gpg-auto-import-keys refresh
   ```

1. Install **mssql-tools** with the `unixODBC` developer package. For more information, see [Install the Microsoft ODBC driver for SQL Server (Linux)](/sql/connect/odbc/linux-mac/installing-the-microsoft-odbc-driver-for-sql-server).

   ```bash
   sudo zypper install -y mssql-tools unixODBC-devel
   ```

For convenience, you can add `/opt/mssql-tools/bin/` to your `PATH` environment variable. This enables you to run the tools without specifying the full path. Run the following commands to modify the PATH for both login sessions and interactive/non-login sessions:

```bash
echo 'export PATH="$PATH:/opt/mssql-tools/bin"' >> ~/.bash_profile
echo 'export PATH="$PATH:/opt/mssql-tools/bin"' >> ~/.bashrc
source ~/.bashrc
```

### Install SQL Server high availability agent

Run the following command on **all nodes** to install the high availability agent package for SQL Server:

```bash
sudo zypper install mssql-server-ha
```

### Open ports for high availability services

1. You can open the following firewall ports on all nodes for SQL Server and HA services: 1433, 2224, 3121, 5022, 5405, 21064.

   ```bash
   sudo firewall-cmd --zone=public --add-port=1433/tcp --add-port=2224/tcp --add-port=3121/tcp --add-port=5022/tcp --add-port=5405/tcp --add-port=21064 --permanent
   sudo firewall-cmd --reload
   ```

## Configure an availability group

Use the following steps to configure a SQL Server Always On availability group for your VMs. For more information, see [Configure SQL Server Always On availability groups for high availability on Linux](/sql/linux/sql-server-linux-availability-group-configure-ha)

### Enable availability groups and restart SQL Server

Enable availability groups on each node that hosts a SQL Server instance. Then restart the `mssql-server` service. Run the following commands on each node:

```bash
sudo /opt/mssql/bin/mssql-conf set hadr.hadrenabled 1
```

```bash
sudo systemctl restart mssql-server
```

### Create a certificate

[!INCLUDE [msconame-md](../../../docs/includes/msconame-md.md)] doesn't support Active Directory authentication to the AG endpoint. Therefore, you must use a certificate for AG endpoint encryption.

1. Connect to **all nodes** using SQL Server Management Studio (SSMS) or **sqlcmd**. Run the following commands to enable an AlwaysOn_health session and create a master key:

   > [!IMPORTANT]  
   > If you're connecting remotely to your SQL Server instance, you'll need to have port 1433 open on your firewall. You'll also need to allow inbound connections to port 1433 in your NSG for each VM. For more information, see [Create a security rule](/azure/virtual-network/manage-network-security-group#create-a-security-rule) for creating an inbound security rule.

   - Replace the `<MasterKeyPassword>` with your own password.

   ```sql
   ALTER EVENT SESSION AlwaysOn_health ON SERVER
       WITH (STARTUP_STATE = ON);
   GO

   CREATE MASTER KEY ENCRYPTION BY PASSWORD = '<MasterKeyPassword>';
   GO
   ```

1. Connect to the primary replica using SSMS or **sqlcmd**. The below commands create a certificate at `/var/opt/mssql/data/dbm_certificate.cer` and a private key at `var/opt/mssql/data/dbm_certificate.pvk` on your primary SQL Server replica:

   - Replace the `<PrivateKeyPassword>` with your own password.

   ```sql
   CREATE CERTIFICATE dbm_certificate
       WITH SUBJECT = 'dbm';
   GO

   BACKUP CERTIFICATE dbm_certificate TO FILE = '/var/opt/mssql/data/dbm_certificate.cer'
   WITH PRIVATE KEY (
           FILE = '/var/opt/mssql/data/dbm_certificate.pvk',
           ENCRYPTION BY PASSWORD = '<PrivateKeyPassword>'
           );
   GO
   ```

Exit the **sqlcmd** session by running the `exit` command, and return back to your SSH session.

### Copy the certificate to the secondary replicas and create the certificates on the server

1. Copy the two files that were created to the same location on all servers that will host availability replicas.

   On the primary server, run the following `scp` command to copy the certificate to the target servers:

   - Replace `<username>` and `sles2` with the user name and target VM name that you're using.
   - Run this command for all secondary replicas.

   > [!NOTE]  
   > You don't have to run `sudo -i`, which gives you the root environment. You can run the `sudo` command in front of each command instead.

   ```bash
   # The below command allows you to run commands in the root environment
   sudo -i
   ```

   ```bash
   scp /var/opt/mssql/data/dbm_certificate.* <username>@sles2:/home/<username>
   ```

1. On the target server, run the following command:

   - Replace `<username>` with your user name.
   - The `mv` command moves the files or directory from one place to another.
   - The `chown` command is used to change the owner and group of files, directories, or links.
   - Run these commands for all secondary replicas.

   ```bash
   sudo -i
   mv /home/<username>/dbm_certificate.* /var/opt/mssql/data/
   cd /var/opt/mssql/data
   chown mssql:mssql dbm_certificate.*
   ```

1. The following Transact-SQL script creates a certificate from the backup that you created on the primary SQL Server replica. Update the script with strong passwords. The decryption password is the same password that you used to create the .pvk file in the previous step. To create the certificate, run the following script using **sqlcmd** or SSMS on all secondary servers:

   ```sql
   CREATE CERTIFICATE dbm_certificate
       FROM FILE = '/var/opt/mssql/data/dbm_certificate.cer'
       WITH PRIVATE KEY (
       FILE = '/var/opt/mssql/data/dbm_certificate.pvk',
       DECRYPTION BY PASSWORD = '<PrivateKeyPassword>'
   );
   GO
   ```

### Create the database mirroring endpoints on all replicas

Run the following script on all SQL Server instances using **sqlcmd** or SSMS:

```sql
CREATE ENDPOINT [Hadr_endpoint]
   AS TCP (LISTENER_PORT = 5022)
   FOR DATABASE_MIRRORING (
   ROLE = ALL,
   AUTHENTICATION = CERTIFICATE dbm_certificate,
ENCRYPTION = REQUIRED ALGORITHM AES
);
GO

ALTER ENDPOINT [Hadr_endpoint] STATE = STARTED;
GO
```

### Create the availability group

Connect to the SQL Server instance that hosts the primary replica using **sqlcmd** or SSMS. Run the following command to create the availability group:

- Replace `ag1` with your desired AG name.
- Replace the `sles1`, `sles2`, and `sles3` values with the names of the SQL Server instances that host the replicas.

```sql
CREATE AVAILABILITY
GROUP [ag1]
WITH (
        DB_FAILOVER = ON,
        CLUSTER_TYPE = EXTERNAL
        )
FOR REPLICA
    ON N'sles1'
WITH (
        ENDPOINT_URL = N'tcp://sles1:5022',
        AVAILABILITY_MODE = SYNCHRONOUS_COMMIT,
        FAILOVER_MODE = EXTERNAL,
        SEEDING_MODE = AUTOMATIC
        ),
    N'sles2'
WITH (
        ENDPOINT_URL = N'tcp://sles2:5022',
        AVAILABILITY_MODE = SYNCHRONOUS_COMMIT,
        FAILOVER_MODE = EXTERNAL,
        SEEDING_MODE = AUTOMATIC
        ),
    N'sles3'
WITH (
        ENDPOINT_URL = N'tcp://sles3:5022',
        AVAILABILITY_MODE = SYNCHRONOUS_COMMIT,
        FAILOVER_MODE = EXTERNAL,
        SEEDING_MODE = AUTOMATIC
        );
GO

ALTER AVAILABILITY GROUP [ag1]
GRANT CREATE ANY DATABASE;
GO
```

### Create a SQL Server login for Pacemaker

On all SQL Server instances, create a SQL Server login for Pacemaker. The following Transact-SQL creates a login.

- Replace `<password>` with your own complex password.

```sql
USE [master]
GO

CREATE LOGIN [pacemakerLogin]
    WITH PASSWORD = N'<password>';
GO

ALTER SERVER ROLE [sysadmin]
    ADD MEMBER [pacemakerLogin];
GO
```

On all SQL Server instances, save the credentials used for the SQL Server login.

1. Create the file:

   ```bash
   sudo vi /var/opt/mssql/secrets/passwd
   ```

1. Add the following two lines to the file:

   ```bash
   pacemakerLogin
   <password>
   ```

   To exit the **vi** editor, first hit the **Esc** key, and then enter the command `:wq` to write the file and quit.

1. Make the file only readable by root:

   ```bash
   sudo chown root:root /var/opt/mssql/secrets/passwd
   sudo chmod 400 /var/opt/mssql/secrets/passwd
   ```

### Join secondary replicas to the availability group

1. On your secondary replicas, run the following commands to join them to the AG:

   ```sql
   ALTER AVAILABILITY GROUP [ag1] JOIN WITH (CLUSTER_TYPE = EXTERNAL);
   GO

   ALTER AVAILABILITY GROUP [ag1] GRANT CREATE ANY DATABASE;
   GO
   ```

1. Run the following Transact-SQL script on the primary replica and each secondary replica:

   ```sql
   GRANT ALTER, CONTROL, VIEW DEFINITION
       ON AVAILABILITY GROUP::ag1 TO pacemakerLogin;
   GO

   GRANT VIEW SERVER STATE TO pacemakerLogin;
   GO
   ```

1. Once the secondary replicas are joined, you can see them in SSMS Object Explorer by expanding the **Always On High Availability** node:

   :::image type="content" source="media/sles-high-availability-fencing-tutorial/availability-group-joined.png" alt-text="Screenshot shows the primary and secondary availability replicas.":::

### Add a database to the availability group

This section follows the article for [adding a database to an availability group](/sql/linux/sql-server-linux-availability-group-configure-ha#add-a-database-to-the-availability-group).

The following Transact-SQL commands are used in this step. Run these commands on the primary replica:

```sql
CREATE DATABASE [db1]; -- creates a database named db1
GO

ALTER DATABASE [db1] SET RECOVERY FULL; -- set the database in full recovery model
GO

BACKUP DATABASE [db1] -- backs up the database to disk
    TO DISK = N'/var/opt/mssql/data/db1.bak';
GO

ALTER AVAILABILITY GROUP [ag1] ADD DATABASE [db1]; -- adds the database db1 to the AG
GO
```

### Verify that the database is created on the secondary servers

On each secondary SQL Server replica, run the following query to see if the db1 database was created and is in a SYNCHRONIZED state:

```sql
SELECT * FROM sys.databases
WHERE name = 'db1';
GO

SELECT DB_NAME(database_id) AS 'database',
    synchronization_state_desc
FROM sys.dm_hadr_database_replica_states;
GO
```

If the `synchronization_state_desc` lists SYNCHRONIZED for `db1`, this means the replicas are synchronized. The secondaries are showing `db1` in the primary replica.

## Create availability group resources in the Pacemaker cluster

[!INCLUDE [bias-sensitive-term-t](../../../docs/includes/bias-sensitive-term-t.md)]

This article references the guide to [create the availability group resources in a Pacemaker cluster](/sql/linux/sql-server-linux-create-availability-group#create-the-availability-group-resources-in-the-pacemaker-cluster-external-only).

### Enable Pacemaker

Enable Pacemaker so that it automatically starts.

Run the following command on **all nodes** in the cluster.

```bash
sudo systemctl enable pacemaker
```

### Create the AG cluster resource

1. Run `crm configure` to open the **crm** prompt:

   ```bash
   sudo crm configure
   ```

1. In the **crm** prompt, run the following command to configure the resource properties. The following commands create the resource `ag_cluster` in the availability group `ag1`.

   ```bash
   primitive ag_cluster ocf:mssql:ag params ag_name="ag1" meta failure-timeout=60s op start timeout=60s op stop timeout=60s op promote timeout=60s op demote timeout=10s op monitor timeout=60s interval=10s op monitor timeout=60s interval=11s role="Master" op monitor timeout=60s interval=12s role="Slave" op notify timeout=60s ms ms-ag_cluster ag_cluster meta master-max="1" master-node-max="1" clone-max="3" clone-node-max="1" notify="true"
   commit
   quit
   ```

   > [!TIP]  
   > Type `quit` to exit from the **crm** prompt.

1. Set the co-location constraint for the virtual IP, to run on the same node as the primary node:

   ```bash
   sudo crm configure
   colocation vip_on_master inf: admin-ip ms-ag_cluster: Master
   commit
   quit
   ```

1. Add the ordering constraint, to prevent the IP address from temporarily pointing to the node with the pre-failover secondary. Run the following command to create ordering constraint:

   ```bash
   sudo crm configure
   order ag_first inf: ms-ag_cluster:promote admin-ip:start
   commit
   quit
   ```

1. Check the status of the cluster using the command:

   ```bash
   sudo crm status
   ```

   The output should be similar to the following example:

   ```output
   Cluster Summary:
     * Stack: corosync
     * Current DC: sles1 (version 2.0.5+20201202.ba59be712-150300.4.30.3-2.0.5+20201202.ba59be712) - partition with quorum
     * Last updated: Mon Mar  6 18:38:17 2023
     * Last change:  Mon Mar  6 18:38:09 2023 by root via cibadmin on sles1
     * 3 nodes configured
     * 5 resource instances configured

   Node List:
     * Online: [ sles1 sles2 sles3 ]

   Full List of Resources:
     * admin-ip    (ocf::heartbeat:IPaddr2):                Started sles1
     * rsc_st_azure        (stonith:fence_azure_arm):       Started sles2
     * Clone Set: ms-ag_cluster [ag_cluster] (promotable):
       * Masters: [ sles1 ]
       * Slaves: [ sles2 sles3 ]
   ```

1. Run the following command to review the constraints:

   ```bash
   sudo crm configure show
   ```

   The output should be similar to the following example:

   ```output
   node 1: sles1
   node 2: sles2
   node 3: sles3
   primitive admin-ip IPaddr2 \
           params ip=10.0.0.93 \
           op monitor interval=10 timeout=20
   primitive ag_cluster ocf:mssql:ag \
           params ag_name=ag1 \
           meta failure-timeout=60s \
           op start timeout=60s interval=0 \
           op stop timeout=60s interval=0 \
           op promote timeout=60s interval=0 \
           op demote timeout=10s interval=0 \
           op monitor timeout=60s interval=10s \
           op monitor timeout=60s interval=11s role=Master \
           op monitor timeout=60s interval=12s role=Slave \
           op notify timeout=60s interval=0
   primitive rsc_st_azure stonith:fence_azure_arm \
           params subscriptionId=xxxxxxx resourceGroup=amvindomain tenantId=xxxxxxx login=xxxxxxx passwd="******" cmk_monitor_retries=4 pcmk_action_limit=3 power_timeout=240 pcmk_reboot_timeout=900 pcmk_host_map="sles1:sles1;les2:sles2;sles3:sles3" \
           op monitor interval=3600 timeout=120
   ms ms-ag_cluster ag_cluster \
           meta master-max=1 master-node-max=1 clone-max=3 clone-node-max=1 notify=true
   order ag_first Mandatory: ms-ag_cluster:promote admin-ip:start
   colocation vip_on_master inf: admin-ip ms-ag_cluster:Master
   property cib-bootstrap-options: \
           have-watchdog=false \
           dc-version="2.0.5+20201202.ba59be712-150300.4.30.3-2.0.5+20201202.ba59be712" \
           cluster-infrastructure=corosync \
           cluster-name=sqlcluster \
           stonith-enabled=true \
           concurrent-fencing=true \
           stonith-timeout=900
   rsc_defaults rsc-options: \
           resource-stickiness=1 \
           migration-threshold=3
   op_defaults op-options: \
           timeout=600 \
           record-pending=true
   ```

## Test failover

To ensure that the configuration has succeeded so far, test a failover. For more information, see [Always On availability group failover on Linux](/sql/linux/sql-server-linux-availability-group-failover-ha).

1. Run the following command to manually fail over the primary replica to `sles2`. Replace `sles2` with the value of your server name.

   ```bash
   sudo crm resource move ag_cluster sles2
   ```

   The output should be similar to the following example:

   ```output
   INFO: Move constraint created for ms-ag_cluster to sles2
   INFO: Use `crm resource clear ms-ag_cluster` to remove this constraint
   ```

1. Check the status of the cluster:

   ```bash
   sudo crm status
   ```

   The output should be similar to the following example:

   ```output
   Cluster Summary:
     * Stack: corosync
     * Current DC: sles1 (version 2.0.5+20201202.ba59be712-150300.4.30.3-2.0.5+20201202.ba59be712) - partition with quorum
     * Last updated: Mon Mar  6 18:40:02 2023
     * Last change:  Mon Mar  6 18:39:53 2023 by root via crm_resource on sles1
     * 3 nodes configured
     * 5 resource instances configured

   Node List:
     * Online: [ sles1 sles2 sles3 ]

   Full List of Resources:
     * admin-ip    (ocf::heartbeat:IPaddr2):                Stopped
     * rsc_st_azure        (stonith:fence_azure_arm):       Started sles2
     * Clone Set: ms-ag_cluster [ag_cluster] (promotable):
       * Slaves: [ sles1 sles2 sles3 ]
   ```

1. After some time, the `sles2` VM is now the primary, and the other two VMs are secondaries. Run `sudo crm status` once again, and review the output, which is similar to the following example:

   ```output
   Cluster Summary:
     * Stack: corosync
     * Current DC: sles1 (version 2.0.5+20201202.ba59be712-150300.4.30.3-2.0.5+20201202.ba59be712) - partition with quorum
     * Last updated: Tue Mar  6 22:00:44 2023
     * Last change:  Mon Mar  6 18:42:59 2023 by root via cibadmin on sles1
     * 3 nodes configured
     * 5 resource instances configured

   Node List:
     * Online: [ sles1 sles2 sles3 ]

   Full List of Resources:
     * admin-ip    (ocf::heartbeat:IPaddr2):                Started sles2
     * rsc_st_azure        (stonith:fence_azure_arm):       Started sles2
     * Clone Set: ms-ag_cluster [ag_cluster] (promotable):
       * Masters: [ sles2 ]
       * Slaves: [ sles1 sles3 ]
   ```

1. Check your constraints again, using `crm config show`. Observe that another constraint was added because of the manual failover.

1. Remove the constraint with ID `cli-prefer-ag_cluster`, using the following command:

   ```bash
   crm configure
   delete cli-prefer-ms-ag_cluster
   commit
   ```

## Test fencing

You can test STONITH by running the following command. Try running the below command from `sles1` for `sles3`.

```bash
sudo crm node fence sles3
```

## Next step

> [!div class="nextstepaction"]
> [Tutorial: Configure an availability group listener on Linux virtual machines](high-availability-listener-tutorial.md)
