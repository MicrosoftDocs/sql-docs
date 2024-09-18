---
title: "Deploy availability group with HPE Serviceguard"
description: Use HPE Serviceguard as the cluster manager to achieve high availability with an availability group on SQL Server on Linux
author: amitkh-msft
ms.author: amitkh
ms.reviewer: vanto, randolphwest
ms.date: 03/05/2024
ms.service: sql
ms.subservice: linux
ms.topic: tutorial
ms.custom:
  - intro-deployment
  - linux-related-content
---
# Tutorial: Set up a three node Always On availability group with HPE Serviceguard for Linux

[!INCLUDE [SQL Server - Linux](../includes/applies-to-version/sql-linux.md)]

This tutorial explains how to configure SQL Server availability groups with HPE Serviceguard for Linux, running on on-premises virtual machines (VMs) or in Azure-based Virtual Machines.

Refer to [HPE Serviceguard Clusters](https://h20195.www2.hpe.com/v2/GetPDF.aspx/c04154488.pdf) for an overview of the HPE Serviceguard clusters.

> [!NOTE]  
>  
> Microsoft supports data movement, the availability group, and the SQL Server components. Contact HPE for support related to the documentation of HPE Serviceguard cluster and quorum management.

This tutorial consists of the following tasks:

> [!div class="checklist"]
> - Install SQL Server on all the three VMs that will be part of the availability group
> - Install HPE Serviceguard on the VMs
> - Create the HPE Serviceguard cluster
> - Create the load balancer in the Azure portal
> - Create the availability group and add a sample database to the availability group
> - Deploy the SQL Server workload on the availability group through Serviceguard cluster manager
> - Perform an automatic failover and join the node back to cluster

## Prerequisites

- In Azure, create three Linux-based VMs (Virtual Machines). To create Linux-based virtual machines in Azure, see [Quickstart: Create Linux virtual machine in Azure portal](/azure/virtual-machines/linux/quick-create-portal). When deploying the VMs, make sure to use HPE Serviceguard supported Linux distributions. You could also deploy the VMs locally in an on-premises environment if you prefer.

  For an example of a supported distribution, see [HPE Serviceguard for Linux](https://h20195.www2.hpe.com/v2/gethtml.aspx?docname=c04154488). Check with HPE for information about support for public cloud environments.

  The instructions in this tutorial are validated against HPE Serviceguard for Linux. A trial edition is available for download from [HPE](https://www.hpe.com/us/en/resources/servers/serviceguard-linux-trial.html).

- SQL Server database files on logical volume mount (LVM) for all three virtual machines. See [Quick start guide for Serviceguard Linux (HPE)](https://support.hpe.com/hpesc/public/docDisplay?docId=a00112895en_us&page=GUID-1E75E8C6-C674-48D1-B30D-DED738431FDD.html)

- Ensure that you have a OpenJDK Java runtime installed on the VMs. The IBM Java SDK isn't supported.

## Install SQL Server

On all the three VMs, follow one of the below steps based on the Linux distribution that you choose for this tutorial, to install SQL Server and tools.

### Red Hat Enterprise Linux (RHEL)

- [Install SQL Server on Red Hat](quickstart-install-connect-red-hat.md#install)
- [Tools](quickstart-install-connect-red-hat.md#tools)

### SUSE Linux Enterprise Server (SLES)

- [Install SQL Server on SLES](quickstart-install-connect-suse.md#install)
- [Tools](quickstart-install-connect-suse.md#tools)

After you complete this step, you should have SQL Server service and tools installed on all three VMs that will participate in the availability group.

## Install HPE Serviceguard on the VMs

In this step, install HPE Serviceguard for Linux on all three VMs. The following table describes the role each server plays in the cluster.

| Number of VMs | HPE Serviceguard role | Microsoft SQL Server availability group replica role |
| --- | --- | --- |
| 1 | HPE Serviceguard cluster nodes | Primary replica |
| 1 or more | HPE Serviceguard cluster node | Secondary replica |
| 1 | HPE Serviceguard quorum server | Configuration only replica |

> [!NOTE]  
> Refer to this video from HPE, which describes [how to install and configure an HPE Serviceguard cluster via the UI](https://support.hpe.com/hpesc/public/videoDisplay?videoId=vtc00040206en_us).

To install Serviceguard, use the `cminstaller` method. Specific instructions are available in the following links:

- [Install Serviceguard for Linux on two nodes](https://support.hpe.com/hpesc/public/docDisplay?docId=a00112895en_us&page=GUID-1E75E8C6-C674-48D1-B30D-DED738431FDD.html). Refer to the section **Install_serviceguard_using_cminstaller**.
- [Install Serviceguard quorum server on the third node](https://support.hpe.com/hpesc/public/docDisplay?docId=a00112895en_us&page=GUID-5C321932-E5F7-4D05-8A55-33E56BE7AFB0.html). Refer to the section **Install_QS_from_the_ISO**.

After you complete the installation of the HPE Serviceguard cluster, you can enable cluster management portal on TCP port 5522 on the primary replica node. The following steps add a rule to the firewall to allow 5522. The following command is for a Red Hat Enterprise Linux (RHEL). you need to run similar commands for other distributions:

```bash
sudo firewall-cmd --zone=public --add-port=5522/tcp --permanent
sudo firewall-cmd --reload
```

## Create HPE Serviceguard cluster

Follow theses instructions to configure and create the HPE Serviceguard cluster. In this step, you will also configure the quorum server.

1. [Configure the Serviceguard quorum server on the third node](https://support.hpe.com/hpesc/public/docDisplay?docId=a00112895en_us&page=GUID-C371758D-AF60-4FA9-B541-E1198650162A.html). Refer to the **Configure_QS** section.
1. [Configure and create Serviceguard cluster on the other two nodes](https://support.hpe.com/hpesc/public/docDisplay?docId=a00112895en_us&page=GUID-43419A91-E430-42F3-BFE4-29CC7FAA64D0.html). Refer to the **Configure_and_create_Cluster** section.

> [!NOTE]  
> You can bypass manual installation of your HPE Serviceguard cluster and quorum, by adding the [HPE Serviceguard for Linux (SGLX) extension](https://techcommunity.microsoft.com/t5/sql-server-blog/hpe-sglx-the-new-azure-vm-extension-for-sql-server-on-linux/ba-p/3723764) from the Azure VM marketplace, when you create your VM.



## Create the availability group and add a sample database

In this step, create an availability group with two (or more) synchronous replicas and a configuration only replica, which provides data protection and might also provide high availability. The following diagram represents this architecture:

:::image type="content" source="media/sql-server-linux-availability-group-ha/2-configuration-only.png" alt-text="Screenshot showing how the primary replica synchronizes user data and configuration data with the secondary replica. The primary replica only synchronizes configuration data with the configuration only replica. The configuration only replica doesn't have user data replicas.":::

1. Synchronous replication of user data to the secondary replica. It also includes availability group configuration metadata.

1. Synchronous replication of availability group configuration metadata. It doesn't include user data.

For more information, see [Two synchronous replicas and a configuration only replica](sql-server-linux-availability-group-ha.md).

To create the availability group, follow these steps:

1. [Enable availability groups and restart mssql-server](#enable-availability-groups-and-restart-mssql-server) on all the VMs including the Configuration only replica.
1. [Enable an `AlwaysOn_health` event session (optional)](#enable-an-alwayson_health-event-session-optional)
1. [Create a certificate on the primary VM](#create-a-certificate-on-the-primary-vm)
1. [Create the certificate on secondary servers](#create-the-certificate-on-secondary-servers)
1. [Create the database mirroring endpoints on the replicas](#create-the-database-mirroring-endpoints-on-the-replicas)
1. [Create availability group](#create-availability-group)
1. [Join the secondary replicas](#join-the-secondary-replicas)
1. [Add a database to the availability group](#add-a-database-to-the-availability-group)

### Enable availability groups and restart mssql-server

Enable availability groups on all the nodes that host a SQL Server instance. Then restart mssql-server. Run the following script on all three nodes:

```bash
sudo /opt/mssql/bin/mssql-conf
set hadr.hadrenabled 1 sudo systemctl restart mssql-server
```

### Enable an `AlwaysOn_health` event session (optional)

Optionally enable Always On availability groups extended events to help with root-cause diagnosis when you troubleshoot an availability group. Run the following command on each instance of SQL Server:

```sql
ALTER EVENT SESSION AlwaysOn_health ON SERVER WITH (STARTUP_STATE=ON);
GO
```

### Create a certificate on the primary VM

The following Transact-SQL script creates a master key and a certificate. It then backs up the certificate and secures the file with a private key. Update the script with strong passwords. Connect to the primary SQL Server instance and run the following Transact-SQL script:

```sql
CREATE MASTER KEY ENCRYPTION BY PASSWORD = '<Master_Key_Password>';

CREATE CERTIFICATE dbm_certificate WITH SUBJECT = 'dbm';

BACKUP CERTIFICATE dbm_certificate TO FILE = '/var/opt/mssql/data/dbm_certificate.cer'
WITH PRIVATE KEY
    ( FILE = '/var/opt/mssql/data/dbm_certificate.pvk',
      ENCRYPTION BY PASSWORD = '<Private_Key_Password>' );
```

At this point, the primary SQL Server replica has a certificate at `/var/opt/mssql/data/dbm_certificate.cer` and a private key at `var/opt/mssql/data/dbm_certificate.pvk`. Copy these two files to the same location on all servers that will host availability replicas. Use the `mssql` user, or give permission to the `mssql` user to access these files.

For example, on the source server, the following command copies the files to the target machine. Replace the `node2` values with the name of the host running the secondary SQL Server instance. Copy the certificate on the configuration only replica as well and run the below commands on that node as well.

```bash
cd /var/opt/mssql/data
scp dbm_certificate.* root@<node2>:/var/opt/mssql/data/
```

Now on the secondary VMs running the secondary instance and the configuration only replica of SQL Server, run the below commands so that the `mssql` user can own the copied certificate:

```bash
cd /var/opt/mssql/data
chown mssql:mssql dbm_certificate.*
```

### Create the certificate on secondary servers

The following Transact-SQL script creates a master key and a certificate from the backup that you created on the primary SQL Server replica. Update the script with strong passwords. The decryption password is the same password that you used to create the .pvk file in a previous step. To create the certificate, run the following script on all secondary servers except the configuration-only replica:

```sql
CREATE MASTER KEY ENCRYPTION BY PASSWORD =
'<Master_Key_Password>';

CREATE CERTIFICATE dbm_certificate FROM FILE =
'/var/opt/mssql/data/dbm_certificate.cer'
WITH PRIVATE KEY ( FILE = '/var/opt/mssql/data/dbm_certificate.pvk',
DECRYPTION BY PASSWORD = '<Private_Key_Password>' );
```

### Create the database mirroring endpoints on the replicas

On the primary and the secondary replicas, run the below commands to create the database mirroring endpoints:

```sql
CREATE ENDPOINT [hadr_endpoint] AS TCP (LISTENER_PORT = 5022)
    FOR DATABASE_MIRRORING
        (
        ROLE = WITNESS,
        AUTHENTICATION = CERTIFICATE dbm_certificate,
        ENCRYPTION = REQUIRED ALGORITHM AES
        );

ALTER ENDPOINT [hadr_endpoint] STATE = STARTED;
```

> [!NOTE]  
> 5022 is the standard port used for the database mirroring endpoint, but you can change it to any available port.

On the configuration-only replica create the database mirroring endpoint using the below command, note for the value for the Role here is set to `WITNESS`, which is what it needs to be for the configuration-only replica.

```sql
CREATE ENDPOINT [hadr_endpoint] AS TCP (LISTENER_PORT = 5022)
    FOR DATABASE_MIRRORING (
        ROLE = WITNESS,
        AUTHENTICATION = CERTIFICATE dbm_certificate,
        ENCRYPTION = REQUIRED ALGORITHM AES
        );

ALTER ENDPOINT [hadr_endpoint] STATE = STARTED;
```

### Create availability group

On the primary replica instance, run the following commands. These commands create an availability group named `ag1`, which has an EXTERNAL `cluster_type` and grants create database permission to the availability group.

Before you run the following scripts, replace the `<node1>`, `<node2>`, and `<node3>` (configuration-only replica) placeholders with the name of the VMs that you created in previous steps.

```sql
CREATE AVAILABILITY GROUP [ag1]
    WITH (CLUSTER_TYPE = EXTERNAL)
    FOR REPLICA ON
    N'<node1>' WITH (
        ENDPOINT_URL = N'tcp://<node1>:<5022>',
        AVAILABILITY_MODE = SYNCHRONOUS_COMMIT,
        FAILOVER_MODE = EXTERNAL,
        SEEDING_MODE = AUTOMATIC
        ),

    N'<node2>' WITH (
        ENDPOINT_URL = N'tcp://<node2>:\<5022>',
        AVAILABILITY_MODE = SYNCHRONOUS_COMMIT,
        FAILOVER_MODE = EXTERNAL,
        SEEDING_MODE = AUTOMATIC
        ),
    
    N'<node3>' WITH (
        ENDPOINT_URL = N'tcp://<node3>:<5022>',
        AVAILABILITY_MODE = CONFIGURATION_ONLY
        );
          
ALTER AVAILABILITY GROUP [ag1] GRANT CREATE ANY DATABASE;
```

### Join the Secondary replicas

Run the following commands on all the secondary replicas. These commands join the secondary replicas to the `ag1` availability group with the primary replica, and provide create database access to the `ag1` availability group.

```sql
ALTER AVAILABILITY GROUP [ag1]
JOIN WITH (CLUSTER_TYPE = EXTERNAL);
GO
ALTER AVAILABILITY GROUP [ag1]
GRANT CREATE ANY DATABASE;
GO
```

### Add a database to the availability group

Connect to the primary replica and run the following T-SQL commands to:

1. Create a sample database named `db1`, which will be added to the availability group.
1. Set the recovery model of the database to full. All databases in an availability group require full recovery model.
1. Back up the database. A database requires at least one full backup before you can add it to an availability group.

```sql
-- creates a database named db1
CREATE DATABASE [db1];
GO

-- set the database in full recovery model
ALTER DATABASE [db1] SET RECOVERY FULL; 
GO

-- backs up the database to disk
BACKUP DATABASE [db1]
TO DISK = N'/var/opt/mssql/data/db1.bak';
GO

-- adds the database db1 to the AG
ALTER AVAILABILITY GROUP [ag1] ADD DATABASE [db1];
GO
```

After successfully completing the previous steps, you can see an `ag1` availability group created and the three VMs are added as replica with one primary replica, one secondary replica, and one configuration-only replica. `ag1` contains one database.

## Deploy the SQL Server availability group workload (HPE Cluster Manager)

In HPE Serviceguard, deploy the SQL Server workload on availability group through Serviceguard cluster manager UI.

Deploy the availability group workload and enable high availability (HA), disaster recovery (DR) via Serviceguard cluster using the [Serviceguard manager graphical user interface](https://support.hpe.com/hpesc/public/docDisplay?docId=a00112895en_us&page=GUID-BD13B685-12ED-4BA0-83CD-181B312F6138.html). Refer to the section **Protecting Microsoft SQL Server on Linux for Always On Availability Groups**.

### Create the load balancer in the Azure portal

For Deployments in Azure Cloud, HPE Serviceguard for Linux requires a load balancer to enable client connections with the primary replica, to substitute traditional IP addresses.

1. In the Azure portal, open the resource group that contains the Serviceguard cluster nodes or virtual machines.
1. In the resource group, select **Add**.
1. Search for "load balancer" and then, in the search results, select the **Load Balancer** that is published by Microsoft.
1. On the **Load Balancer** pane, select **Create**.
1. Configure the load balancer as follows:

   | Setting | Value |
   | --- | --- |
   | **Name** | The load balancer name. For example, `SQLAvailabilityGroupLB`. |
   | **Type** | Internal |
   | **SKU** | Basic or Standard |
   | **Virtual network** | VNet used for the VM replicas |
   | **Subnet** | Subnet in which SQL Server instances are hosted |
   | **IP Address Assignment** | Static |
   | **Private IP address** | Create a private IP within subnet |
   | **Subscription** | Choose the concerned subscription |
   | **Resource Group** | Choose the concerned resource group |
   | **Location** | Select same location as SQL nodes |

### Configure the backend pool

The backend pool is the addresses of the two instances on which the Serviceguard cluster is configured.

1. In your resource group, select the load balancer that you created.
1. Navigate to **Settings > Backend pools**, and select **Add** to create a backend address pool.
1. On **Add backend pool**, under **Name**, type a name for the backend pool.
1. Under **Associated to**, select **Virtual machine**.
1. Select the virtual machine in the environment, and associate the appropriate IP address to each selection.
1. Select **Add**.

#### Create a probe

The probe defines how Azure verifies which of the Serviceguard cluster node is primary replica. Azure probes the service based on the IP address on a port that you define when you create the probe.

1. On the **Load balancer settings** pane, select **Health probes**.

1. On the **Health probes** pane, select **Add**.

1. Use the following values to configure the probe:

   | Setting | Value |
   | --- | --- |
   | **Name** | Name representing the probe. For example, `SQLAGPrimaryReplicaProbe`. |
   | **Protocol** | TCP |
   | **Port** | You can use any available port. For example, 59999. |
   | **Interval** | 5 |
   | **Unhealthy threshold** | 2 |

1. Select **OK**.

1. Sign in to all your virtual machines, and open the probe port using the following commands:

   ```bash
   sudo firewall-cmd --zone=public --add-port=59999/tcp --permanent
   sudo firewall-cmd --reload
   ```

Azure creates the probe and then uses it to test the Serviceguard node on which the primary replica instance of the availability group is running. Remember the port configured (59999), which is required to [deploy the AG in the Serviceguard cluster](#deploy-the-sql-server-availability-group-workload-hpe-cluster-manager).

### Set the load balancing rules

The load balancing rules configure how the load balancer routes traffic to the Serviceguard node, which is the primary replica in the cluster. For this load balancer, enable the direct server return, because only one of the Serviceguard cluster nodes can be a primary replica at a time.

1. On the **Load balancer settings** pane, select **Load balancing rules**.
1. On the **Load balancing rules** pane, select **Add**.
1. Configure the load balancing rule using the following settings:

   | Setting | Value |
   | --- | --- |
   | **Name** | Name representing the load balancing rules. For example, `SQLAGPrimaryReplicaListener`. |
   | **Protocol** | TCP |
   | **Port** | 1433 |
   | **Backend port** | 1433. This value is ignored because this rule uses Floating IP. |
   | **Probe** | Use the name of the probe that you created for this load balancer. |
   | **Session persistence** | None |
   | **Idle timeout (minutes)** | 4 |
   | **Floating IP** | Enabled |

1. Select **OK**.

1. Azure configures the load balancing rule. Now the load balancer is configured to route traffic to the Serviceguard node that is the primary replica instance in the cluster.

Take note of the load balancer's frontend IP address "LbReadWriteIP", which is required to [deploy the AG in the Serviceguard cluster](#deploy-the-sql-server-availability-group-workload-hpe-cluster-manager).

At this point, the resource group has a load balancer that connects to all Serviceguard nodes. The load balancer also contains an IP address for the clients to connect to the primary replica instance in the cluster, so that any machine that is a primary replica can respond to requests for the availability group.

## Perform automatic failover and join the node back to cluster

For the automatic failover test, you can bring down the primary replica (power off), which replicates the sudden unavailability of the primary node. The expected behavior is:

1. The cluster manager promotes one of the secondary replicas in the availability group to primary.
1. The failed primary replica automatically joins the cluster after it is back up. The cluster manager promotes it to secondary replica.

For HPE Serviceguard, refer to the section [**Testing the setup for failover readiness**](https://support.hpe.com/hpesc/public/docDisplay?docId=a00112895en_us&page=GUID-1119C5B1-3DEE-473D-8684-A7816BE12B7D.html)

## Related content

- [Availability Groups on Linux](sql-server-linux-availability-group-overview.md)
