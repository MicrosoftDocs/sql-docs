---
title: Configure an availability group for SQL Server on Ubuntu virtual machines in Azure - Linux virtual machines
description: Learn about setting up an availability group in SQL Server on Ubuntu virtual machines in Azure.
author: aravindmahadevan-ms
ms.author: armaha
ms.reviewer: amitkh-msft, randolphwest
ms.date: 11/29/2023
ms.service: azure-vm-sql-server
ms.subservice: hadr
ms.custom: devx-track-azurecli, linux-related-content
ms.topic: tutorial
---

# Tutorial: Configure availability groups for SQL Server on Ubuntu virtual machines in Azure

In this tutorial, you'll learn how to:

> [!div class="checklist"]
>  
> - Create virtual machines, place them in availability set
> - Enable high availability (HA)
> - Create a Pacemaker cluster
> - Configure a fencing agent by creating a STONITH device
> - Install SQL Server and mssql-tools on Ubuntu
> - Configure SQL Server Always On availability group
> - Configure availability group (AG) resources in the Pacemaker cluster
> - Test a failover and the fencing agent

[!INCLUDE [bias-sensitive-term-t](../../../docs/includes/bias-sensitive-term-t.md)]

This tutorial uses the Azure CLI to deploy resources in Azure.

If you don't have an Azure subscription, create a [free account](https://azure.microsoft.com/free/?WT.mc_id=A261C142F) before you begin.

[!INCLUDE [azure-cli-prepare-your-environment.md](~/../azure-sql/reusable-content/azure-cli/azure-cli-prepare-your-environment.md)]

- This article requires version 2.0.30 or later of the Azure CLI. If using Azure Cloud Shell, the latest version is already installed.

## Create a resource group

If you have more than one subscription, [set the subscription](/cli/azure/manage-azure-subscriptions-azure-cli) that you want deploy these resources to.

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

1. Create a named subnet with a preassigned IP address range. Replace these values in the following command:

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

## Create Ubuntu VMs inside the availability set

1. Get a list of virtual machine images that offer Ubuntu based OS in Azure.

   ```azurecli-interactive
    az vm image list --all --offer "sql2022-ubuntupro2004"
   ```

   You should see the following results when you search for the BYOS images:

   ```json
   [
     {
       "architecture": "x64",
       "offer": "sql2022-ubuntupro2004",
       "publisher": "MicrosoftSQLServer",
       "sku": "enterprise_upro",
       "urn": "MicrosoftSQLServer:sql2022-ubuntupro2004:enterprise_upro:16.0.221108",
       "version": "16.0.221108"
     },
     {
       "architecture": "x64",
       "offer": "sql2022-ubuntupro2004",
       "publisher": "MicrosoftSQLServer",
       "sku": "enterprise_upro",
       "urn": "MicrosoftSQLServer:sql2022-ubuntupro2004:enterprise_upro:16.0.230207",
       "version": "16.0.230207"
     },
     {
       "architecture": "x64",
       "offer": "sql2022-ubuntupro2004",
       "publisher": "MicrosoftSQLServer",
       "sku": "enterprise_upro",
       "urn": "MicrosoftSQLServer:sql2022-ubuntupro2004:enterprise_upro:16.0.230808",
       "version": "16.0.230808"
     },
     {
       "architecture": "x64",
       "offer": "sql2022-ubuntupro2004",
       "publisher": "MicrosoftSQLServer",
       "sku": "sqldev_upro",
       "urn": "MicrosoftSQLServer:sql2022-ubuntupro2004:sqldev_upro:16.0.221108",
       "version": "16.0.221108"
     },
     {
       "architecture": "x64",
       "offer": "sql2022-ubuntupro2004",
       "publisher": "MicrosoftSQLServer",
       "sku": "sqldev_upro",
       "urn": "MicrosoftSQLServer:sql2022-ubuntupro2004:sqldev_upro:16.0.230207",
       "version": "16.0.230207"
     },
     {
       "architecture": "x64",
       "offer": "sql2022-ubuntupro2004",
       "publisher": "MicrosoftSQLServer",
       "sku": "sqldev_upro",
       "urn": "MicrosoftSQLServer:sql2022-ubuntupro2004:sqldev_upro:16.0.230808",
       "version": "16.0.230808"
     },
     {
       "architecture": "x64",
       "offer": "sql2022-ubuntupro2004",
       "publisher": "MicrosoftSQLServer",
       "sku": "standard_upro",
       "urn": "MicrosoftSQLServer:sql2022-ubuntupro2004:standard_upro:16.0.221108",
       "version": "16.0.221108"
     },
     {
       "architecture": "x64",
       "offer": "sql2022-ubuntupro2004",
       "publisher": "MicrosoftSQLServer",
       "sku": "standard_upro",
       "urn": "MicrosoftSQLServer:sql2022-ubuntupro2004:standard_upro:16.0.230207",
       "version": "16.0.230207"
     },
     {
       "architecture": "x64",
       "offer": "sql2022-ubuntupro2004",
       "publisher": "MicrosoftSQLServer",
       "sku": "standard_upro",
       "urn": "MicrosoftSQLServer:sql2022-ubuntupro2004:standard_upro:16.0.230808",
       "version": "16.0.230808"
     },
     {
       "architecture": "x64",
       "offer": "sql2022-ubuntupro2004",
       "publisher": "MicrosoftSQLServer",
       "sku": "web_upro",
       "urn": "MicrosoftSQLServer:sql2022-ubuntupro2004:web_upro:16.0.221108",
       "version": "16.0.221108"
     },
     {
       "architecture": "x64",
       "offer": "sql2022-ubuntupro2004",
       "publisher": "MicrosoftSQLServer",
       "sku": "web_upro",
       "urn": "MicrosoftSQLServer:sql2022-ubuntupro2004:web_upro:16.0.230207",
       "version": "16.0.230207"
     },
     {
       "architecture": "x64",
       "offer": "sql2022-ubuntupro2004",
       "publisher": "MicrosoftSQLServer",
       "sku": "web_upro",
       "urn": "MicrosoftSQLServer:sql2022-ubuntupro2004:web_upro:16.0.230808",
       "version": "16.0.230808"
     }
   ]
   ```

   This tutorial uses `Ubuntu 20.04`.

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
          --image "Canonical:0001-com-ubuntu-server-jammy:20_04-lts-gen2:latest" \
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

```json
{
  "fqdns": "",
  "id": "/subscriptions/<subscriptionId>/resourceGroups/<resourceGroupName>/providers/Microsoft.Compute/virtualMachines/ubuntu1",
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
[<username>@ubuntu1 ~]$
```

Type `exit` to leave the SSH session.

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

In the following command, the `<username>` account can be the same account you configured for each node when creating the VM. You can also use the `root` account, but this option isn't recommended in a production environment.

```bash
sudo ssh-copy-id <username>@ubuntu1
sudo ssh-copy-id <username>@ubuntu2
sudo ssh-copy-id <username>@ubuntu3
```

### Verify passwordless access from each node

To confirm that the SSH public key was copied to each node, use the `ssh` command from each node. If you copied the keys correctly, you aren't prompted for a password, and the connection is successful.

In this example, we're connecting to the second and third nodes from the first VM (`ubuntu1`). Once again the `<username>` account can be the same account you configured for each node when creating the VM.

```bash
ssh <username>@ubuntu2
ssh <username>@ubuntu3
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
> - `ubuntu1`: 10.0.0.85
> - `ubuntu2`: 10.0.0.86
> - `ubuntu3`: 10.0.0.87

## Enable High availability

Use **ssh** to connect to each of the 3 VMs, and once connected, run the following commands to enable high availability.

```bash
sudo /opt/mssql/bin/mssql-conf set hadr.hadrenabled 1
sudo systemctl restart mssql-server
```

### Install and configure Pacemaker cluster

To get started with configuring Pacemaker cluster, you need to install the required packages and resource agents.
Run the below commands on each of your VMs:

```bash
sudo apt-get install -y pacemaker pacemaker-cli-utils crmsh resource-agents fence-agents csync2 python3-azure
```

Now proceed to create authentication key on primary server:

```bash
sudo corosync-keygen
```

The authkey is generated in `/etc/corosync/authkey` location. Copy the authkey to secondary servers in this location: `/etc/corosync/authkey`

```bash
sudo scp /etc/corosync/authkey username@ubuntu2:~
sudo scp /etc/corosync/authkey username@ubuntu3:~
```

Move the authkey from home directory to `/etc/corosync`.

```bash
sudo mv authkey /etc/corosync/authkey
```

Proceed to create the cluster by using following commands:

```bash
cd /etc/corosync/
sudo vi corosync.conf
```

Edit the Corosync file to depict contents as follows:

```output
totem {
    version: 2
    secauth: off
    cluster_name: demo
    transport: udpu
}

nodelist {
    node {
        ring0_addr: 10.0.0.85
        name: ubuntu1
        nodeid: 1
    }
    node {
        ring0_addr: 10.0.0.86
        name: ubuntu2
        nodeid: 2
    }
    node {
        ring0_addr: 10.0.0.87
        name: ubuntu3
        nodeid: 3
    }
}

quorum {
    provider: corosync_votequorum
    two_node: 0
}

qb {
    ipc_type: native
}

logging {
    fileline: on
    to_stderr: on
    to_logfile: yes
    logfile: /var/log/corosync/corosync.log
    to_syslog: no
    debug: off
}
```

Copy the `corosync.conf` file to other nodes to `/etc/corosync/corosync.conf`:

```bash
sudo scp /etc/corosync/corosync.conf username@ubuntu2:~
sudo scp /etc/corosync/corosync.conf username@ubuntu3:~
sudo mv corosync.conf /etc/corosync/
```

Restart Pacemaker and Corosync, and confirm the status:

```bash
sudo systemctl restart pacemaker corosync
sudo crm status
```

The output looks similar to the following example:

```output
Cluster Summary:
  * Stack: corosync
  * Current DC: ubuntu1 (version 2.0.3-4b1f869f0f) - partition with quorum
  * Last updated: Wed Nov 29 07:01:32 2023
  * Last change:  Sun Nov 26 17:00:26 2023 by hacluster via crmd on ubuntu1
  * 3 nodes configured
  * 0 resource instances configured

Node List:
  * Online: [ ubuntu1 ubuntu2 ubuntu3 ]

Full List of Resources:
  * No resources
```

## Configure fencing agent

Configure fencing on the cluster. *Fencing* is the isolation of failed node in a cluster. It restarts the failed node, letting it go down, reset, and come back up, rejoining the cluster.

In order to configure fencing, perform the following actions:

1. Register a new application in Microsoft Entra ID and create a secret
1. Create a custom role from json file in powershell/CLI
1. Assign the role and application to the VMs in cluster
1. Set the fencing agent properties

### Register a new application in Microsoft Entra ID and create a secret

1. Go to Microsoft Entra ID in the portal and make a note of the Tenant ID.
1. Select **App Registrations** on the left hand side menu and then select **New Registration**.
1. Enter a **Name** and then select **Accounts in this organization directory only**.
1. For **Application Type**, select **Web**, enter `http://localhost` as a sign-on URL, then select **Register**.
1. Select **Certificates and secrets** on the left hand side menu, then select **New client secret**.
1. Enter a description and select an expiry period.
1. Make a note of the value of the secret, it's used as the following password and the secret ID, it's used as the following username.
1. Select "Overview" and make a note of the Application ID. It's used as the following login.

Create a JSON file called `fence-agent-role.json` and add the following (adding your subscription ID):

```json
{
  "Name": "Linux Fence Agent Role-ap-server-01-fence-agent",
  "Id": null,
  "IsCustom": true,
  "Description": "Allows to power-off and start virtual machines",
  "Actions": [
    "Microsoft.Compute/*/read",
    "Microsoft.Compute/virtualMachines/powerOff/action",
    "Microsoft.Compute/virtualMachines/start/action"
  ],
  "NotActions": [],
  "AssignableScopes": [
    "/subscriptions/XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX"
  ]
}
```

### Create a custom role from JSON file in PowerShell/CLI

```bash
az role definition create --role-definition fence-agent-role.json
```

### Assign the role and application to the VMs in cluster

1. For each of the VMs in the cluster, select **Access Control (IAM)** from the side menu.
1. Select **Add a role assignment** (use the classic experience).
1. Select the role created previously.
1. In the Select list, enter the name of the application created earlier.

Now we can create the fencing agent resource using previous values and your subscription ID:

```bash
  sudo crm configure primitive fence-vm stonith:fence_azure_arm \
params \
action=reboot \
resourceGroup="resourcegroupname" \
resourceGroup="$resourceGroup" \
username="$secretId" \
login="$applicationId" \
passwd="$password" \
tenantId="$tenantId" \
subscriptionId="$subscriptionId" \
pcmk_reboot_timeout=900 \
power_timeout=60 \
op monitor \
interval=3600 \
timeout=120
```

### Set the fencing agent properties

Run the following commands to set the fencing agent properties:

```bash
sudo crm configure property cluster-recheck-interval=2min
sudo crm configure property start-failure-is-fatal=true
sudo crm configure property stonith-timeout=900
sudo crm configure property concurrent-fencing=true
sudo crm configure property stonith-enabled=true
```

And confirm the cluster status:

```bash
  sudo crm status
```

The output looks similar to the following example:

```output
Cluster Summary:
  * Stack: corosync
  * Current DC: ubuntu1 (version 2.0.3-4b1f869f0f) - partition with quorum
  * Last updated: Wed Nov 29 07:01:32 2023
  * Last change:  Sun Nov 26 17:00:26 2023 by root via cibadmin on ubuntu1
  * 3 nodes configured
  * 1 resource instances configured

Node List:
  * Online: [ ubuntu1 ubuntu2 ubuntu3 ]

Full List of Resources:
  * fence-vm     (stonith:fence_azure_arm):                        Started ubuntu1
```

## Install SQL Server and mssql-tools

The following commands are used to install SQL Server:

1. Import the public repository GPG keys:

   ```bash
   curl https://packages.microsoft.com/keys/microsoft.asc | sudo tee /etc/apt/trusted.gpg.d/microsoft.asc
   ```

1. Register the Ubuntu repository:

   ```bash
   sudo add-apt-repository "$(wget -qO- https://packages.microsoft.com/config/ubuntu/20.04/mssql-server-2022.list)"
   ```

1. Run the following commands to install SQL Server:

   ```bash
   sudo apt-get update
   sudo apt-get install -y mssql-server
   ```

1. After the package installation finishes, run `mssql-conf setup` and follow the prompts to set the SA password and choose your edition. As a reminder, the following editions are freely licensed: Evaluation, Developer, and Express.

   ```bash
   sudo /opt/mssql/bin/mssql-conf setup
   ```

1. Once the configuration is done, verify that the service is running:

   ```bash
   systemctl status mssql-server --no-pager
   ```

1. Install the SQL Server command-line tools

To create a database, you need to connect with a tool that can run Transact-SQL statements on SQL Server. The following steps install the SQL Server command-line tools: [**sqlcmd**](/sql/tools/sqlcmd/sqlcmd-utility) and [**bcp**](/sql/tools/bcp-utility).

[!INCLUDE [odbc-ubuntu](includes/odbc-ubuntu.md)]

### Install SQL Server high availability agent

Run the following command on **all nodes** to install the high availability agent package for SQL Server:

```bash
sudo apt-get install mssql-server-ha
```

## Configure an availability group

Use the following steps to configure a SQL Server Always On availability group for your VMs. For more information, see [Configure SQL Server Always On availability groups for high availability on Linux](/sql/linux/sql-server-linux-availability-group-configure-ha).

### Enable availability groups and restart SQL Server

Enable availability groups on each node that hosts a SQL Server instance. Then restart the `mssql-server` service. Run the following commands on each node:

```bash
sudo /opt/mssql/bin/mssql-conf set hadr.hadrenabled 1
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
- Replace the `ubuntu1`, `ubuntu2`, and `ubuntu3` values with the names of the SQL Server instances that host the replicas.

```sql
CREATE AVAILABILITY
GROUP [ag1]
WITH (
        DB_FAILOVER = ON,
        CLUSTER_TYPE = EXTERNAL
        )
FOR REPLICA
    ON N'ubuntu1'
WITH (
        ENDPOINT_URL = N'tcp://ubuntu1:5022',
        AVAILABILITY_MODE = SYNCHRONOUS_COMMIT,
        FAILOVER_MODE = EXTERNAL,
        SEEDING_MODE = AUTOMATIC
        ),
    N'ubuntu2'
WITH (
        ENDPOINT_URL = N'tcp://ubuntu2:5022',
        AVAILABILITY_MODE = SYNCHRONOUS_COMMIT,
        FAILOVER_MODE = EXTERNAL,
        SEEDING_MODE = AUTOMATIC
        ),
    N'ubuntu3'
WITH (
        ENDPOINT_URL = N'tcp://ubuntu3:5022',
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

   :::image type="content" source="media/ubuntu-high-availability-fencing-tutorial/availability-group-joined.png" alt-text="Screenshot shows the primary and secondary availability replicas.":::

### Add a database to the availability group

This section follows the article for [adding a database to an availability group](/sql/linux/sql-server-linux-availability-group-configure-ha#add-a-database-to-the-availability-group).

The following Transact-SQL commands are used in this step. Run these commands on the primary replica:

```sql
CREATE DATABASE [db1]; -- creates a database named db1
GO

ALTER DATABASE [db1] SET RECOVERY FULL; -- set the database in full recovery mode
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

## Create availability group resources in Pacemaker cluster

To create the availability group resource in Pacemaker, Run the following commands:

```bash
sudo crm

configure

primitive ag1_cluster \
ocf:mssql:ag \
params ag_name="ag1" \
meta failure-timeout=60s \
op start timeout=60s \
op stop timeout=60s \
op promote timeout=60s \
op demote timeout=10s \
op monitor timeout=60s interval=10s \
op monitor timeout=60s on-fail=demote interval=11s role="Master" \
op monitor timeout=60s interval=12s role="Slave" \
op notify timeout=60s

ms ms-ag1 ag1_cluster \
meta master-max="1" master-node-max="1" clone-max="3" \
clone-node-max="1" notify="true"

commit
```

This above command creates the ag1_cluster resource, that is, the availability group resource. Then it creates ms-ag1 resource (primary/secondary resource in Pacemaker, then adding the AG resource to it. This ensures that AG resource runs on all three nodes in cluster but only one of those nodes is primary.)

To view the AG group resource, and to check status of cluster:

```bash
sudo crm resource status ms-ag1
sudo crm status
```

The output looks similar to the following example:

```output
resource ms-ag1 is running on: ubuntu1 Master
resource ms-ag1 is running on: ubuntu3
resource ms-ag1 is running on: ubuntu2
```

The output looks similar to the following example. To add colocation and promotion constraints, see [Tutorial: Configure an availability group listener on Linux virtual machines](high-availability-listener-tutorial.md).

```output
Cluster Summary:
  * Stack: corosync
  * Current DC: ubuntu1 (version 2.0.3-4b1f869f0f) - partition with quorum
  * Last updated: Wed Nov 29 07:01:32 2023
  * Last change:  Sun Nov 26 17:00:26 2023 by root via cibadmin on ubuntu1
  * 3 nodes configured
  * 4 resource instances configured

Node List:
  * Online: [ ubuntu1 ubuntu2 ubuntu3 ]

Full List of Resources:
  * Clone Set: ms-ag1 [ag1_cluster] (promotable):
  * Masters: [ ubuntu1 ]
  * Slaves : [ ubuntu2 ubuntu3 ]
  * fence-vm     (stonith:fence_azure_arm):                        Started ubuntu1
```

Run the following command to create a group resource, so that the colocation and promotion constraints applied to the listener and load balancer don't have to be applied individually.

```bash
sudo crm configure group virtualip-group azure-load-balancer virtualip
```

The output of `crm status` will look similar to the following example:

```output
Cluster Summary:
  * Stack: corosync
  * Current DC: ubuntu1 (version 2.0.3-4b1f869f0f) - partition with quorum
  * Last updated: Wed Nov 29 07:01:32 2023
  * Last change:  Sun Nov 26 17:00:26 2023 by root via cibadmin on ubuntu1
  * 3 nodes configured
  * 6 resource instances configured

Node List:
  * Online: [ ubuntu1 ubuntu2 ubuntu3 ]

Full List of Resources:
  * Clone Set: ms-ag1 [ag1_cluster] (promotable):
    * Masters: [ ubuntu1 ]
    * Slaves : [ ubuntu2 ubuntu3 ]
  * Resource Group:  virtual ip-group:
    * azure-load-balancer  (ocf  :: heartbeat:azure-lb):           Started ubuntu1     
    * virtualip     (ocf :: heartbeat: IPaddr2):                   Started ubuntu1
  * fence-vm     (stonith:fence_azure_arm):                        Started ubuntu1
```

## Next step

> [!div class="nextstepaction"]
> [Tutorial: Configure an availability group listener on Linux virtual machines](high-availability-listener-tutorial.md)
