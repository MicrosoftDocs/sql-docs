---
title: "Always On for SSIS Catalog (SSISDB) | Microsoft Docs"
ms.custom: 
  - "SQL2016_New_Updated"
ms.date: "03/01/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "integration-services"
ms.tgt_pltfrm: ""
ms.topic: "article"
f1_keywords: 
  - "sql13.ssis.ssms.alwayson.f1"
ms.assetid: 7f089455-35ee-4c13-884e-9254b685d07d
caps.latest.revision: 17
author: "douglaslMS"
ms.author: "douglasl"
manager: "jhubbard"
---
# Always On for SSIS Catalog (SSISDB)
  The Always On Availability Groups feature is a high-availability and disaster-recovery solution that provides an enterprise-level alternative to database mirroring. An availability group supports a failover environment for a discrete set of user databases, known as availability databases, that fail over together. For more information,  see [Always On Availability Groups](https://msdn.microsoft.com/library/hh510230.aspx).  
  
 In order to provide the high-availability for the SSIS catalog (SSISDB) and its contents (projects, packages, execution logs, etc.), you can add the SSISDB database (just the same as any other user database) to an Always On Availability Group. When a failover occurs, one of the secondary nodes automatically becomes the new primary node.  
 
 > [!IMPORTANT]
 > When a failover occurs, packages that were running do not restart or resume. 
 
 **In this topic:**  
  
1.  [Prerequisites](../../integration-services/service/always-on-for-ssis-catalog-ssisdb.md#prereq)  
  
2.  [Configure SSIS support for Always On](../../integration-services/service/always-on-for-ssis-catalog-ssisdb.md#Firsttime)  
  
3.  [Upgrading SSISDB in an availability group](../../integration-services/service/always-on-for-ssis-catalog-ssisdb.md#Upgrade)  
  
##  <a name="prereq"></a> Prerequisites  
 You must perform the following pre-requisite steps before enabling Always On support for the SSISDB database.  
  
1.  Set up a Windows failover cluster. See [Installing the Failover Cluster Feature and Tools for Windows Server 2012](http://blogs.msdn.com/b/clustering/archive/2012/04/06/10291601.aspx) blog post for instructions. You should install the feature and tools on all cluster nodes.  
  
2.  Install SQL Server 2016 with Integration Services (SSIS) feature on each node of the cluster.  
  
3.  Enable Always On feature for each SQL Server instance. See [Enable Always On Availability Groups](https://msdn.microsoft.com/library/ff878259.aspx) for details.  
  
##  <a name="Firsttime"></a> Configure SSIS support for Always On  
  
-   [Step 1: Create Integration Services Catalog](../../integration-services/service/always-on-for-ssis-catalog-ssisdb.md#Step1)  
  
-   [Step 2: Add SSISDB to an Always On Availability Group](../../integration-services/service/always-on-for-ssis-catalog-ssisdb.md#Step2)  
  
-   [Step 3: Enable SSIS support for Always On](../../integration-services/service/always-on-for-ssis-catalog-ssisdb.md#Step3)  
  
> [!IMPORTANT]  
>  -   You must perform these steps on the **primary node** of the availability group.  
> -   You must enable **SSIS support for Always On** after you add SSISDB to an Always On group.  
  
###  <a name="Step1"></a> Step 1: Create Integration Services Catalog  
  
1.  Launch **SQL Server Management Studio** and connect to a SQL Server instance in the cluster that you want to set as the **primary node** of Always On high availability group for SSISDB.  
  
2.  In Object Explorer, expand the server node, right-click the **Integration Services Catalogs** node, and then click **Create Catalog**.  
  
3.  Click **Enable CLR Integration**. The catalog uses CLR stored procedures.  
  
4.  Click **Enable automatic execution of Integration Services stored procedure at SQL Server startup** to enable the [catalog.startup](https://msdn.microsoft.com/library/hh230984.aspx) stored procedure to run each time the SSIS server instance is restarted. The stored procedure performs maintenance of the state of operations for the SSISDB catalog. It fixes the status of any packages there were running if and when the SSIS server instance goes down.  
  
5.  Enter a **password**, and then click **Ok**. The password protects the database master key that is used for encrypting the catalog data. Save the password in a secure location. It is recommended that you also back up the database master key. For more information, see [Back Up a Database Master Key](https://msdn.microsoft.com/library/aa337546.aspx).  
  
###  <a name="Step2"></a> Step 2: Add SSISDB to an Always On Availability Group  
 Adding the SSISDB database to an Always On Availability Group is almost same as adding any other user database into an availability group. See [Use the Availability Group Wizard](https://msdn.microsoft.com/library/hh403415.aspx).  
  
 You need to provide the password that you specified while creating the SSIS Catalog in the **Select Databases** page of the **New Availability Group** wizard.  
  
 ![New Availability Group](../../integration-services/service/media/ssis-newavailabilitygroup.png "New Availability Group")  
  
###  <a name="Step3"></a> Step 3: Enable SSIS support for Always On  
 After you create the Integration Service Catalog, right click the **Integration Service Catalogs** node, and click **Enable Always On Support….** You should see the following **Enable Support for Always On** dialog box. If this menu item is disabled, confirm that you have all the prerequisites installed and click **Refresh**.  
  
 ![Enable Support for AlwaysOn](../../integration-services/service/media/ssis-enablesupportforalwayson.png "Enable Support for AlwaysOn")  
  
> [!WARNING]  
>  Auto-failover of SSISDB database is not supported until you enable SSIS Support for Always On.  
  
 The newly added secondary replicas from the AlwayOn availability group will be shown in the table. Click **Connect…** button for each replica in the list and enter authentication credentials to connect to the replica. The user account must be a member of sysadmin group on each replica to enable SSIS Always On support. After you successfully connect to each replica, click **OK** to enable SSIS support for Always On.  
  
##  <a name="Upgrade"></a> Upgrading SSISDB in an availability group  
 If you're upgrading SQL Server from a previous version, and SSISDB is in an Always On availability group, your upgrade may be blocked by the “SSISDB in Always On Availability Group check” rule. This blocking occurs because upgrade runs in single-user mode, while an availability database must be a multi-user database. Therefore, during upgrade or patching, all availability databases including SSISDB are taken offline and are not upgraded or patched. To let upgrade continue, you have to to first remove SSISDB from the availability group, then upgrade or patch each node, then add SSISDB back to the availability group.  
  
 If you are blocked by the “SSISDB in Always On Availability Group check” rule, you have to follow these steps to upgrade SQL Server.  
  
1.  Remove the SSISDB database from the availability group. For more info, see [Remove a Secondary Database from an Availability Group &#40;SQL Server&#41;](../../database-engine/availability-groups/windows/remove-a-secondary-database-from-an-availability-group-sql-server.md) and [Remove a Primary Database from an Availability Group &#40;SQL Server&#41;](../../database-engine/availability-groups/windows/remove-a-primary-database-from-an-availability-group-sql-server.md).  
  
2.  Click **Re-run** in the upgrade wizard. The “SSISDB in Always On Availability Group check” rule will pass.  
  
3.  Click the **Next** to continue the upgrade.  
  
4.  After you have upgraded all the nodes, add the SSISDB database back to the Always On availability group. For more info, see [Add a Database to an Availability Group &#40;SQL Server&#41;](../../database-engine/availability-groups/windows/availability-group-add-a-database.md).  
  
 If you're not blocked when you upgrade SQL Server,  and SSISDB is in an Always On availability group, you have to upgrade SSISDB separately after you upgrade the SQL Server database engine. Use the SSIS Upgrade Wizard to upgrade the SSISDB as described in the following procedure.  
  
1.  Move the SSISDB database out of the availability group, or delete the availability group if SSISDB is the only database in the availability group. You have to launch **SQL Server Management Studio** on the **primary node** of the availability group to perform this task.  
  
2.  Remove the SSISDB database from all **replica nodes**.  
  
3.  Upgrade the SSISDB database on the **primary node**. In**Object Explorer** in SQL Server Management Studio, expand **Integration Services Catalogs**, right-click **SSISDB**, and then select **Database Upgrade**. Follow the instructions in the **SSISDB Upgrade Wizard** to upgrade the database. You have to launch the **SSIDB Upgrade Wizard** locally on the **primary node**.  
  
4.  Follow the instructions in [Step 2: Add SSISDB to an Always On Availability Group](../../integration-services/service/always-on-for-ssis-catalog-ssisdb.md#Step2) to add the SSISDB back to an availability group.  
  
5.  Follow the instructions in [Step 3: Enable SSIS support for Always On](../../integration-services/service/always-on-for-ssis-catalog-ssisdb.md#Step3).  
  
  