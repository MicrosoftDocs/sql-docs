---
title: "Deploy availability group with HPE Serviceguard - SQL Server on Linux"
description: Use HPE Serviceguard as the cluster manager to achieve high availability with an availability group on SQL Server on Linux
ms.date: 04/11/2022
ms.service: sql
ms.subservice: linux
ms.topic: tutorial
author: amitkh-msft
ms.author: amitkh
ms.reviewer: vanto, randolphwest
ms.custom:
  - intro-deployment
---

# Tutorial: Set up a three node Always On availability group with HPE Serviceguard for Linux

This tutorial explains how to configure SQL Server Always On availability group with HPE Serviceguard for Linux running on on-premises Virtual Machines (VMs) or in Azure based Virtual Machines.

Refer to [HPE Serviceguard Clusters](https://h20195.www2.hpe.com/v2/GetPDF.aspx/c04154488.pdf) for an overview of the HPE Serviceguard clusters.

> [!NOTE]
>  
> Microsoft supports data movement, the availability group, and the SQL Server components. Contact HPE for support related to the documentation of HPE Serviceguard cluster and quorum management.

This tutorial consists of the following tasks:

> [!div class="checklist"]
>  
> - Install SQL Server on all the three VMs that will be part of the availability group
> - Install HPE Serviceguard on the VMs
> - Create the HPE Serviceguard cluster
> - Create the availability group and add a sample database to the availability group
> - Deploy the SQL Server workload on the availability group through Serviceguard cluster manager
> - Perform an automatic failover and join the node back to cluster

## Prerequisites

- In Azure, create three Linux-based VMs (Virtual Machines). To create Linux-based virtual machines in Azure, see [Quickstart: Create Linux virtual machine in Azure portal](/azure/virtual-machines/linux/quick-create-portal). When deploying the VMs, make sure to use HPE Serviceguard supported Linux distributions. You could also deploy the VMs locally in an on-premises environment if you prefer.

  For an example of a supported distribution, see [HPE Serviceguard for Linux](https://h20195.www2.hpe.com/v2/gethtml.aspx?docname=c04154488). Check with HPE for information about support for public cloud environments.

  The instructions in this tutorial are validated against HPE Serviceguard for Linux. A trial edition is available for download from [HPE](https://www.hpe.com/us/en/resources/servers/serviceguard-linux-trial.html).

- SQL Server database files on logical volume mount (LVM) for all three virtual machines. See [Quick start guide for Serviceguard Linux (HPE)](https://support.hpe.com/hpesc/public/docDisplay?docId=a00107699en_us)

- Ensure that you have a OpenJDK Java runtime installed on the VMs. The IBM Java SDK is not supported.

## Install SQL Server

On all the three VMs, follow one of the below steps based on the Linux distribution that you choose for this tutorial, to install SQL Server and tools.

### RHEL

- [Install SQL Server on Red Hat](quickstart-install-connect-red-hat.md#install)
- [Tools](quickstart-install-connect-red-hat.md#tools)

### SLES

- [Install SQL Server on SLES](quickstart-install-connect-suse.md#install)
- [Tools](quickstart-install-connect-suse.md#tools)

After you complete this step, you should have SQL Server service and tools installed on all three VMs that will participate in the availability group.

## Install HPE Serviceguard on the VMs

In this step, install HPE Serviceguard for Linux on all three VMs. The following table describes the role each server plays in the cluster.

|Number of VMs | HPE Serviceguard role | Microsoft SQL Server availability group replica role|
|--------------|-----------------|------------|
|1 | HPE Serviceguard cluster nodes | Primary replica |
|1 or more | HPE Serviceguard cluster node | Secondary replica |
|1 | HPE Serviceguard quorum server | Configuration only replica |

To install Serviceguard, use the `cminstaller` method. Specific instructions are available in the links below:

Serviceguard cluster and Serviceguard Quorum server

* [Install Serviceguard for Linux on two nodes](https://support.hpe.com/hpesc/public/docDisplay?docId=a00107699en_us#Install_serviceguard_using_cminstaller). Refer to the section **Install_serviceguard_using_cminstaller**.
* [Install Serviceguard quorum server on the third node](https://support.hpe.com/hpesc/public/docDisplay?docId=a00107699en_us#Install_QS_from_the_ISO). Refer to the section **Install_QS_from_the_ISO**.

After you complete the installation of the HPE Serviceguard cluster, you can enable cluster management portal on TCP port 5522 on the primary replica node. The steps below add a rule to the firewall to allow 5522, the command below is for a RHEL distribution, you need to run similar commands for other distributions:

```console
sudo firewall-cmd --zone=public --add-port=5522/tcp --permanent

sudo firewall-cmd --reload 
```

## Create HPE Serviceguard cluster

Follow the instructions below to configure and create the HPE Serviceguard cluster. In this step we will also be configuring the quorum server.

1. [Configure the Serviceguard quorum server on the third node](https://support.hpe.com/hpesc/public/docDisplay?docId=a00107699en_us#Configure_QS). Refer to the **Configure_QS** section.
2. [Configure and create Serviceguard cluster on the other two nodes](https://support.hpe.com/hpesc/public/docDisplay?docId=a00107699en_us#Configure_and_create_cluster). Refer to the **Configure_and_create_Cluster** section.

## Create the availability group and add a sample database

In this step, create an availability group with two (or more) synchronous replicas and a configuration only replica, which provides data protection and may also provide high availability. The following diagram represents this architecture:

:::image type="content" source="media/sql-server-linux-availability-group-ha/2-configuration-only.png" alt-text="Primary replica synchronizes user data and configuration data with the secondary replica. The primary replica only synchronizes configuration data with the configuration only replica. The configuration only replica does not have user data replicas.":::

1. Synchronous replication of user data to the secondary replica. It also includes availability group configuration metadata.

2. Synchronous replication of availability group configuration metadata. It does not include user data.

For more details, refer to [Two synchronous replicas and a configuration only replica](sql-server-linux-availability-group-ha.md).

To create the availability group, follow these steps:

1. [Enable Always On availability groups and restart mssql-server](#enable-always-on-availability-groups-and-restart-mssql-server) on all the VMs including the Configuration only replica.
2. [Enable an `AlwaysOn_health` event session - (Optional)](#enable-an-alwayson_health-event-session---optional)
3. [Create a certificate on the primary VM](#create-a-certificate-on-the-primary-vm)
4. [Create the certificate on secondary servers](#create-the-certificate-on-secondary-servers)
5. [Create the database mirroring endpoints on the replicas](#create-the-database-mirroring-endpoints-on-the-replicas)
6. [Create availability group](#create-availability-group)
7. [Join the secondary replicas](#join-the-secondary-replicas)
8. [Add a database to the availability group created above](#add-a-database-to-the-availability-group-created-above)

### Enable Always On availability groups and restart mssql-server

Enable Always On feature on all the nodes that hosts a SQL Server instance. Then restart mssql-server. Run the following script on all three nodes:

```console
sudo /opt/mssql/bin/mssql-conf
set hadr.hadrenabled 1 sudo systemctl restart mssql-serve
```

### Enable an `AlwaysOn_health` event session - (Optional)

Optionally enable Always On availability groups extended events to help with root-cause diagnosis when you troubleshoot an availability group. Run the following command on each instance of SQL Server:

```tsql
ALTER EVENT SESSION AlwaysOn_health ON SERVER WITH (STARTUP_STATE=ON);
GO
```

### Create a certificate on the primary VM

The following Transact-SQL script creates a master key and a certificate. It then backs up the certificate and secures the file with a private key. Update the script with strong passwords. Connect to the primary SQL Server instance and run the following Transact-SQL script:

```tsql
CREATE MASTER KEY ENCRYPTION BY PASSWORD = '<Master_Key_Password';

CREATE CERTIFICATE dbm_certificate WITH SUBJECT = 'dbm';

BACKUP CERTIFICATE dbm_certificate TO FILE = '/var/opt/mssql/data/dbm_certificate.cer'
WITH PRIVATE KEY 
    ( FILE = '/var/opt/mssql/data/dbm_certificate.pvk',
      ENCRYPTION BY PASSWORD = '<Private_Key_Password>' );

```

At this point, the primary SQL Server replica has a certificate at `/var/opt/mssql/data/dbm_certificate.cer` and a private key at `var/opt/mssql/data/dbm_certificate.pvk`. Copy these two files to the same location on all servers that will host availability replicas. Use the mssql user, or give permission to the mssql user to access these files.

For example, on the source server, the following command copies the files to the target machine. Replace the `'node2'` values with the name of the host running the secondary SQL Server instance. Copy the certificate on the configuration only replica as well and run the below commands on that node as well.

```console
cd /var/opt/mssql/data
scp dbm_certificate.* root@<node2>:/var/opt/mssql/data/
```

Now on the secondary VMs running the secondary instance and the configuration only replica of SQL Server run the below commands so mssql user can own the copied certificate:

```console
cd /var/opt/mssql/data

chown mssql:mssql dbm_certificate.*
```

### Create the certificate on secondary servers

The following Transact-SQL script creates a master key and a certificate from the backup that you created on the primary SQL Server replica. Update the script with strong passwords. The decryption password is the same password that you used to create the .pvk file in a previous step. To create the certificate, run the following script on all secondary servers except the configuration-only replica:

```tsql
CREATE MASTER KEY ENCRYPTION BY PASSWORD =
'<Master_Key_Password>';

CREATE CERTIFICATE dbm_certificate FROM FILE =
'/var/opt/mssql/data/dbm_certificate.cer'
WITH PRIVATE KEY ( FILE = '/var/opt/mssql/data/dbm_certificate.pvk',
DECRYPTION BY PASSWORD = '<Private_Key_Password>' );
```

### Create the database mirroring endpoints on the replicas

On the primary and the secondary replica run the below commands to create the database mirroring endpoints:

```tsql
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

On the configuration-only replica create the database mirroring endpoint using the below command, note for the value for the Role here is set to `WITNESS` which is what it needs to be for the configuration-only replica.

```tsql
CREATE ENDPOINT [hadr_endpoint] AS TCP (LISTENER_PORT = 5022)
    FOR DATABASE_MIRRORING (
        ROLE = WITNESS,
        AUTHENTICATION = CERTIFICATE dbm_certificate,
        ENCRYPTION = REQUIRED ALGORITHM AES
        );

ALTER ENDPOINT [hadr_endpoint] STATE = STARTED;
```

### Create availability group

On the primary replica instance, run the commands below. These commands create an availability group named **ag1** which has an **External** `cluster_type` and grants create database permission to the availability group.

Before you run the following scripts, replace the `<node1>`, `<node2>`, and `<node3>` (configuration-only replica) placeholders with the name of the VMs that you created in previous steps.

```tsql
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

Run the commands below on all the secondary replicas. These commands join the secondary replicas to the "ag1" availability group with the primary replica, and provide create database access to the ag1 availability group.

```tsql
ALTER AVAILABILITY GROUP [ag1] JOIN WITH (CLUSTER_TYPE = EXTERNAL);
GO
ALTER AVAILABILITY GROUP [ag1] GRANT CREATE ANY DATABASE;
GO
```

### Add a database to the availability group created above

Connect to the primary replica and run the below T-SQL commands
to:

1. Create a sample database named **db1** which will be added to the availability group.
2. Set the recovery model of the database to full. All databases in an availability group require full recovery model.
3. Back up the database. A database requires at least one full backup before you can add it to an availability group.

```tsql
CREATE DATABASE [db1]; -- creates a database named db1
GO
ALTER DATABASE [db1] SET RECOVERY FULL; -- set the database in full recovery mode
GO
BACKUP DATABASE [db1] -- backs up the database to disk TO DISK = N'/var/opt/mssql/data/db1.bak';
GO
ALTER AVAILABILITY GROUP [ag1] ADD DATABASE [db1]; -- adds the database db1 to the AG
GO
```

After successfully completing the previous steps, you can see an **ag1** availability group created and the three VMs are added as replica with one primary replica, one secondary replica, and 1 configuration-only replica. **ag1** contains one database.

## Deploy the SQL Server availability group workload (HPE Cluster Manager)

In HPE Serviceguard, deploy the SQL Server workload on availability group through Serviceguard cluster manager UI.
   
Deploy the availability group workload and enable high availability (HA), disaster recovery (DR) via Serviceguard cluster using the [Serviceguard manager graphical user interface](https://support.hpe.com/hpesc/public/docDisplay?docId=a00107699en_us#Protect_your_alwayson_availability_group). Refer to the section **Protecting Microsoft SQL Server on Linux for Always On availability groups**.

## Perform automatic failover and join the node back to cluster

For the automatic failover test, you can go ahead and bring down the primary replica (power off) this will replicate the sudden unavailability of the primary node. The expected behavior is:

1. The cluster manager promotes one of the secondary replicas in the availability group to primary.
2. The failed primary replica automatically joins the cluster after it is back up. The cluster manager promotes it to secondary replica.

For HPE Serviceguard, refer to the section [**Testing the setup for failover readiness**](https://support.hpe.com/hpesc/public/docDisplay?docId=a00107699en_us#Test_the_setup_preparedness)

## Next steps

- Learn more about Always On [Availability Groups on Linux](sql-server-linux-availability-group-overview.md).
