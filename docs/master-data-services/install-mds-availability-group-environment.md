---
title: High Availability and Disaster Recovery
description: Install and configure SQL Master Data Services on an Always On Availability group to improve high availability and disaster recovery of backend data.
author: meetdeepak
ms.author: dkhare
ms.reviewer: randolphwest
ms.date: 08/13/2026
ms.service: sql
ms.subservice: master-data-services
ms.topic: concept-article
ms.custom:
  - build-2025
  - sfi-image-nochange
---

# High availability and disaster recovery for Master Data Services

[!INCLUDE [SQL Server Windows Only - ASDBMI](../includes/applies-to-version/sql-windows-only-asdbmi.md)]

[!INCLUDE [support-notice](includes/support-notice.md)]

## Introduction

This article describes a solution for Master Data Services (MDS) hosted on an Always On availability group (AG) configuration. The article describes how to install and configure SQL Server 2016 Master Data Services (MDS) on a SQL Server 2016 AG.

The main purpose of this solution is to improve high availability and disaster recovery of MDS backend data hosted on a SQL Server database.

To implement the solution, you need to complete the following tasks covered in this article.

1. [Install and set up Windows Server Failover Cluster (WSFC)](#windows-server-failover-cluster-wsfc).
1. [Set up an availability group](#sql-server-availability-group).
1. [Configure MDS to run on a WSFC node](#configure-mds-to-run-on-a-wsfc-node).

These sections briefly introduce the technologies, followed by instructions. For detailed information about the technologies, review the documents linked to in each section.

The solution described in this article is built on top of an AG, in which each database has multiple synchronous or asynchronous replicas. Only one replica accepts the transaction (accepts user requests). This is the primary replica.

Each replica has its own storage, so there's no centralized shared storage in this solution. When there's a software failure or a hardware failure affecting the primary replica, the primary replica can be failed over to a synchronous or asynchronous replica either automatically or manually based on the configuration and situations. This guarantees high availability of the database with minimum interruption to the users.

Asynchronous replicas are usually hosted on a data center that is remote from the primary replica data center. In case of disaster scenarios, the primary replica can be failed over to another data center. This configuration guarantees disaster recovery of the database.

For demonstration purposes, the solution described in this article uses the following versions of software. Other versions should work the same with potentially minor differences.

- Windows Server 2012 R2 with Server Failover cluster
- SQL Server 2016 with the Master Data Services feature

Also, the solution uses two VMs, `MDS-HA1` and `MDS-HA2`, to host two replicas. As long as it's supported by an AG, MDS doesn't limit how many replicas you can use.

This article assumes that you have basic knowledge about Windows Server, Windows Server Failover Cluster, AGs, and SQL Server MDS.

## What isn't covered

This document doesn't cover the following topics:

- How to make IIS, the web server hosting the Master Data Services UI, highly available and recoverable after a disaster. MDS doesn't impose any particular requirement on IIS, so the standard techniques to make IIS highly available and load balancing can work here as well.

- How to use a SQL Server Always On failover cluster instance (FCI) to support high availability (HA) on the MDS backend. SQL Server failover clustering is a different HA solution and is officially supported by SQL Server, and it does work with MDS.

- How to use a hybrid solution of an FCI and an AG to support HA on the MDS backend. The hybrid solution does work with MDS.

## Design consideration

Figure 1 shows a typical configuration used mostly in an AG. In the primary data center, there are two replicas with a synchronous commit relationship, and both replicas have the VOTE privilege. This configuration mainly improves HA in case the primary replica fails.

In the disaster recovery data center, there's a secondary replica with an asynchronous commit relationship with the primary. This data center is usually in a Geo Region different from the primary data center. The secondary replica doesn't have VOTE privilege.

Use this configuration to achieve recovery in case the primary data center is in a disaster, such as a fire, earthquake, or other catastrophic event. The configuration achieves both HA and disaster recovery with relatively low cost.

:::image type="content" source="media/install-mds-availability-group-environment/figure-1-typical-config.png" alt-text="Screenshot of a typical configuration for an AG.":::

Figure 1. A Typical AG Configuration

If you don't need to consider disaster recovery, you don't need to have a replica in a second data center. If you need to improve HA, then you could have more synchronous replicas in the same primary data center.

Consider your scenarios and requirements, and choose how many asynchronous and synchronous replicas you need, and which data center you should put them in.

## Windows Server failover cluster (WSFC)

This section covers the following tasks:

1. [Install Windows Failover Cluster feature](#install-failover-cluster-feature).
1. [Create a Windows Server Failover Cluster](#create-a-windows-server-failover-cluster).

As shown in Figure 1 in the previous section, the solution described in this article includes Windows Server Failover Cluster (WSFC). You need to set up WSFC because AGs depend on WSFC for failure detection and failover.

WSFC is a feature that improves the high availability of applications and services. It consists of a group of independent Windows Server instances with Microsoft Failover Cluster Service running on those instances. The Windows Server instances (or *nodes*) are connected so that they can communicate with each other, and failure detection is possible. WSFC provides failure detection and failover functionalities. If a node or a service fails in the cluster, the failure is detected, and another node automatically or manually begins to provide the services hosted on the failed node. As such, users only experience minimum disruptions in services, and service availability is improved.

### Prerequisites

The Windows Server operating system is installed on all instances, and all updates are patched.

> [!NOTE]  
> To avoid any potential incompatibility issues, install the same Windows version and the same feature set on all the instances.

### Install failover cluster feature

Complete the following steps for each Windows Server instance to install the WSFC feature on each instance. You need administrator permissions.

1. Open **Server Manager** in Windows Server, and select **Add Roles and Features** in the right pane. This action launches the **Add Roles and Feature Wizard**.

1. Select **Next** until you get to the **Features** page.

1. Select the **Failover Clustering** checkbox, and then select **Next** to finish the installation. See Figure 2.

   If you're asked for confirmation to **Add features that are required for Failover clustering**, select **Add Features**. See Figure 3.

   :::image type="content" source="media/install-mds-availability-group-environment/figure-2-select-features.png" alt-text="Screenshot of the Add Roles and Features Wizard, Failover Clustering.":::

   Figure 2

   :::image type="content" source="media/install-mds-availability-group-environment/figure-3-required-features-failover.png" alt-text="Screenshot of the Add Roles and Features Wizard, features required for failover clustering.":::

   Figure 3

1. On the **Confirmation** page, select **Install** to install the failover clustering feature.

1. On the **Result** page, ensure everything is installed successfully without errors or warnings.

### Create a Windows Server failover cluster

After you install the WSFC feature on all instances, you can configure WSFC. You should only need to configure WSFC on one node.

1. Open **Server Manager** in Windows Server, and select **Failover Cluster Manager** on the **Tool** menu at the top right corner to launch the manager.

1. In **Failover Cluster Manager**, select **Validate Configuration** in the right pane. See Figure 4.

   :::image type="content" source="media/install-mds-availability-group-environment/figure-4-validate-config.png" alt-text="Screenshot of Failover Cluster Manager, Validate Configuration.":::

   Figure 4

1. In the **Validate a Configuration** **Wizard**, select **Next**.

1. In the **Select Servers or a Cluster** dialog box, add the server names that host SQL Server, and then select **Next**. See Figure 5.

   In this example, you added two instances, `MDS-HA1` and `MDS-HA2`.

   :::image type="content" source="media/install-mds-availability-group-environment/figure-5-add-server.png" alt-text="Screenshot of the Validate a Configuration Wizard, Select Servers or a Cluster page.":::

   Figure 5

1. On the **Testing Options** page, select **Run all tests**, and then select **Next**.

1. Select **Next** to finish the validation.

   The **Validating** page shows you the progress, and the **Summary** page shows you the validation summary. See Figures 6 and 7.

1. On the **Summary** page, check for any warning or error messages.

   You must fix errors. However, warnings might not be an issue. A warning message means that "the tested item might meet the requirement, but there's something you should check". For example, Figure 7 shows a "validate disk access latency" warning, which might be due to the disk being busy on other tasks temporarily, and you might ignore it. Check the online document for each warning and error message for more details. See Figure 7.

   :::image type="content" source="media/install-mds-availability-group-environment/figure-6-validation-tests.png" alt-text="Screenshot of the Validate a Configuration Wizard, Validating page.":::

   Figure 6

   :::image type="content" source="media/install-mds-availability-group-environment/figure-7-validation-summary.png" alt-text="Screenshot of the Validate a Configuration Wizard, Summary page.":::

   Figure 7

1. On the **Summary** page, confirm that the **Create the cluster now using the validated nodes** checkbox is selected, and then select **Finish** to start the **Create Cluster** **Wizard**.

1. In the **Create Cluster** **Wizard**, select **Next**.

1. On the **Access Point for Administering the Cluster** page, enter the WSFC cluster name, and then select **Next**. In this example, you use `MDS-HA` as the cluster name. See Figure 8.

   :::image type="content" source="media/install-mds-availability-group-environment/figure-8-enter-cluster-name.png" alt-text="Screenshot showing where to enter the cluster name.":::

   Figure 8

1. Continue to select **Next** to finish creating the cluster. The **Summary of Cluster MDS-HA** section displays the cluster information. See Figure 9.

   :::image type="content" source="media/install-mds-availability-group-environment/figure-9-cluster-summary.png" alt-text="Screenshot of the summary information for the cluster.":::

   Figure 9

If you need to add a node later, select **Add Node** action in the right pane in **Failover Cluster Manager**.

Additional notes:

- The WSFC feature might not be available on all Windows Server editions. Ensure that your edition includes this feature.

- You need the proper permissions to set up WSFC in the active directory. If you encounter any issues, see [Failover Cluster Step-by-Step Guide: Configure Accounts in Active Directory](/previous-versions/windows/it-pro/windows-server-2008-R2-and-2008/cc731002(v=ws.10)).

For more detailed information about WSFC, see [Failover Clusters](/previous-versions/windows/it-pro/windows-server-2008-R2-and-2008/cc732488(v=ws.10)).

## SQL Server availability group

This section covers the following tasks:

1. [Enable SQL Server Always On Availability Group](#enable-sql-server-ags-on-every-sql-server-instance).

1. [Create an AG](#create-an-availability-group).
1. [Validate and test the AG](#validate-and-test-the-availability-group).

Always On has two features to provide high availability and disaster recovery for MDS, both are built on top of WSFC.

- Always On Availability Group (AG)
- Always On Failover Cluster Instance (FCI).

An AG provides database-level availability. The AG (a set of user databases) and its virtual network name are registered as resources in WSFC.

FCIs provide instance-level high availability. The SQL Server service and its related services are registered as resources in WSFC. Also, the FCI solution requires symmetrical shared disk storage, such as SAN or SMB file shares, which must be available to all nodes in the WFC cluster.

### Prerequisites

- Install SQL Server on all nodes. For more information, see [SQL Server installation guide](../database-engine/install-windows/install-sql-server.md).

- (Recommended) Install the exact same SQL Server feature set and version on every node. In particular, MDS must be installed.

- (Recommended) Use the same configuration on every SQL Server instance. In particular, configure the same server collation on all SQL Server instances.

- (Recommended) Use the same service account to run every SQL Server instance. Otherwise, you need to grant permission on each SQL Server instance to ensure the SQL Server instances can communicate with each other.

- Confirm that the Windows firewall setting allows the SQL Server instances to communicate with each other.

### Enable SQL Server AGs on every SQL Server instance

1. In **SQL Server Configuration Manager**, select **SQL Server service** in the left pane, right-click **SQL Server** in the right pane, and then select **Properties**. See Figure 10.

   :::image type="content" source="media/install-mds-availability-group-environment/figure-10-sql-server-properties.png" alt-text="Screenshot of the SQL Server Properties window.":::

   Figure 10

1. In **SQL Server (MSSQLSERVER) Properties**, select the **Always On High Availability** tab, and then select the **Enable Always On Availability Groups** check box. When a value displays in the **Windows failover cluster name** text box, select **OK** to continue. See Figure 11.

   :::image type="content" source="media/install-mds-availability-group-environment/figure-11-enable-always-on.png" alt-text="Screenshot of the Enable Always On Availability Groups option.":::

   Figure 11

1. When a warning page displays, select **OK** to continue. See Figure 12.

   :::image type="content" source="media/install-mds-availability-group-environment/figure-12-warning-service-stop-start.png" alt-text="Screenshot of the confirmation prompt to stop and restart the service.":::

   Figure 12

1. Select **Restart**, to restart the **SQL Server** service and make this change effective. See Figure 10.

> [!NOTE]  
> You can change the service account running the SQL Server service by using **SQL Server Configuration Manager**. Select the **Log On** tab in **SQL Server (MSSQLSERVER) Properties**. See Figure 11.

### Create an availability group

After you enable the AG feature in all SQL Server instances, create a new AG that contains the MDS database on one node.

You can only create an AG on existing databases. So either create an MDS database on one node, or create a temporary database and then drop the temporary database. In this example, you create an empty MDS database and create an AG on this MDS database.

1. Launch **SQL Server Management Studio** (**SSMS**) on a node, and connect to the local SQL Server instance with appropriate credentials.

1. In SSMS, open a **new query** window and run the following script to create an empty database. Replace `C:\temp` with the location you want to use to perform a full backup.

   ```sql
   CREATE DATABASE MDS_Sample
   GO

   BACKUP DATABASE MDS_Sample
   TO DISK = 'C:\temp\MDS_Sample.bak'
   GO
   ```

   > [!NOTE]  
   > A full database backup is necessary for creating the AG on this database.

1. In the **Object Explorer**, expand the **Always On High Availability** folder and select **New Availability Group Wizard** to launch the **New Availability Group Wizard**. See Figure 13.

   :::image type="content" source="media/install-mds-availability-group-environment/figure-13-availability-groups-folder.png" alt-text="Screenshot showing how to launch the New Availability Group Wizard.":::

   Figure 13

1. In the **New Availability Group** wizard, select **Next** to display the **Specify Name** page. Type a name for the AG, and then select **Next**. See Figure 14.

   :::image type="content" source="media/install-mds-availability-group-environment/figure-14-availability-group-name.png" alt-text="Screenshot showing where to enter the name of the availability group.":::

   Figure 14

1. Select the database you just created on the **Select Database** page, and then select **Next**. See Figure 15.

   :::image type="content" source="media/install-mds-availability-group-environment/figure-15-availability-group-select-database.png" alt-text="Screenshot showing how to select the database.":::

   Figure 15

1. On the **Specify Replicas** page, add another replica by selecting **Add Replica**. This page already lists the current, local SQL Server instances as a replica. See Figure 16.

1. In the **Connect to Server** dialog box, add the appropriate credentials and select **Connect**.

   :::image type="content" source="media/install-mds-availability-group-environment/figure-16-add-replica-connect-server.png" alt-text="Screenshot showing how to connect to a SQL Server instance.":::

   Figure 16

   Now you see two replicas in the list. Repeat this step to add other nodes as replicas. See Figure 17.

   :::image type="content" source="media/install-mds-availability-group-environment/figure-17-availability-group-sql-replicas.png" alt-text="Screenshot of the list of replicas.":::

   Figure 17

   For each replica, configure the following **Synchronous Commit**, **Automatic Failover**, and **Readable Secondary** settings. See Figure 17.

   - **Synchronous Commit**: This setting guarantees that if a transaction is committed on the primary replica of a database, then the transaction is also committed on all other synchronous replicas. Asynchronous commit doesn't guarantee this, and it might lag behind the primary replica.

     You should usually enable synchronous commit only when the two nodes are in the same data center. If they are in different data centers, synchronous commit might slow down the database performance. If this checkbox isn't selected, then asynchronous commit is used.

   - **Automatic failover**: When the primary replica is down, the AG automatically fails over to its secondary replica when automatic failover is selected. You can only enable this option on the replicas with synchronous commits.

   - **Readable Secondary**: By default, users can't connect to any secondary replicas. This setting enables users to connect to the secondary replica with read-only access.

1. On the **Specify Replicas** page, select the **Listener** tab and do the following. See Figure 18.

   a. Select **Create an availability group listener** to set up an availability group listener for the MDS database connection.

   b. Enter a **listener DNS Name**, such as `MDSSQLServer`.

   c. Enter the default SQL Server port `1433` in the **Port** text box.

   d. Enter DHCP in the **Network Mode** text box, and then select **Next** to continue.

   > [!NOTE]  
   > Optionally, choose **Static IP** as the **Network Mode** and enter a static IP. You can also enter a port other than `1433`.

   :::image type="content" source="media/install-mds-availability-group-environment/figure-18-availability-group-create-listener.png" alt-text="Screenshot showing how to configure the listener.":::

   Figure 18

1. On the **Select Data Synchronization** page, select **Full**, and specify a network share that every node can access. Select **Next** to continue. See Figure 19.

   Use this network share to store the database backup for creating secondary replicas. If your organization can't provide this network share, choose another data synchronization preference. For more information about using other options to create secondary replicas, see [What is an Always On availability group?](../database-engine/availability-groups/windows/overview-of-always-on-availability-groups-sql-server.md) Figure 17 also lists other options.

   :::image type="content" source="media/install-mds-availability-group-environment/figure-19-availability-group-data-sync.png" alt-text="Screenshot showing how to configure data synchronization.":::

   Figure 19

1. On the **Validation** page, ensure all validations pass successfully, and correct any errors. Select **Next** to continue.

1. On the **Summary** page, review all the configuration settings and select **Finish**. This action creates and configures the availability group.

1. On the **Result** page, confirm that the wizard completed all necessary steps.

### Validate and test the availability group

1. Open SSMS and connect to the listener DNS name you created in the [Create an Availability Group](#create-an-availability-group) section. In this example, it's `MDSSQLServer`.

1. In **Object Explorer**, expand the **Always On High Availability** folder, right-click the AG you created in the [Create an Availability Group](#create-an-availability-group) section, and then select **Show Dashboard**. See Figure 20. The status of the new AG and its replicas appears.

   :::image type="content" source="media/install-mds-availability-group-environment/figure-20-show-dashboard.png" alt-text="Screenshot of the dashboard.":::

   Figure 20

1. Select **Failover** to fail over to a synchronous replica and an asynchronous replica. This step verifies that failover happens correctly without any issues.

   The AG setup is completed.

For more information about availability groups, see [What is an Always On availability group?](../database-engine/availability-groups/windows/overview-of-always-on-availability-groups-sql-server.md)

## Configure MDS to run on a WSFC node

This solution requires only the MDS backend database to run on WSFC. You can run other parts of MDS, such as web applications and MDS configuration manager, on the node in WSFC or outside WSFC, as long as MDS can connect to the AG.

1. Open **Master Data Service Configuration Manager** on one node, select **Database Configuration**, and then select **Create Database** to launch the **Create Database Wizard**.

1. On the **Database Server** page, type the AG listener DNS name in the **SQL Server instance** text box, select **Test Connection**, and then select **Next**. See Figure 21.

   :::image type="content" source="media/install-mds-availability-group-environment/figure-21-mds-database-server-listener.png" alt-text="Screenshot showing how to configure the database server with the AG listener.":::

   Figure 21

1. On the **Database** page, type the name of the database that you created in the [Create an Availability Group](#create-an-availability-group) section, and then select **Next**. See Figure 22.

   :::image type="content" source="media/install-mds-availability-group-environment/figure-22-mds-create-database.png" alt-text="Screenshot showing how to create and configure the database.":::

   Figure 22

1. Complete the **Create Database** **Wizard**. For more information, see [Master Data Services Installation and Configuration](master-data-services-installation-and-configuration.md).

1. Select **Web Applications** in **Master Data Service Configuration Manager** to configure the web application, and then select **Apply** to apply the settings to MDS. See Figure 23. For more information, see [Master Data Services Installation and Configuration](master-data-services-installation-and-configuration.md).

   :::image type="content" source="media/install-mds-availability-group-environment/figure-23-mds-web-application.png" alt-text="Screenshot showing how to configure the web application.":::

   Figure 23

   The MDS setup is complete. You can repeat the preceding steps to set up MDS to run on all nodes. The backend database is the same on the same AG.

1. If you previously created a temporary database (see [Create an Availability Group](#create-an-availability-group) section) to create an AG, drop the temporary database.

   For more information about Master Data Services, see [Master Data Services Overview (MDS)](master-data-services-overview-mds.md).

## Conclusion

In this white paper, you learned how to set up and configure the Master Data Services backend database as part of an AG. This configuration provides high availability and disaster recovery on the Master Data Services backend database. To implement this configuration, you need to install and configure Windows Server Failover Cluster, AG, and Master Data Services.
