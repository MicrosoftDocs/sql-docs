---
title: "Tutorial: Prepare SQL Server For Replication - Publisher, Distributor, Subscriber | Microsoft Docs"
ms.custom: ""
ms.date: "04/02/2018"
ms.prod: "sql-non-specified"
ms.prod_service: "database-engine"
ms.service: ""
ms.component: "replication"
ms.reviewer: ""
ms.suite: "sql"
ms.technology: 
  - "replication"
ms.tgt_pltfrm: ""
ms.topic: "get-started-article"
applies_to: 
  - "SQL Server 2016"
helpviewer_keywords: 
  - "replication [SQL Server], tutorials"
ms.assetid: ce30a095-2975-4387-9377-94a461ac78ee
caps.latest.revision: 15
author: "MashaMSFT"
ms.author: "mathoma"
manager: "craigg"
ms.workload: "On Demand"
---
# Tutorial: Prepare SQL Server For Replication - Publisher, Distributor, Subscriber
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
It is important to plan for security before you configure your replication topology. This tutorial shows you how to better secure a replication topology as well as how to configure distribution, which is the first step in replicating data. You must complete this tutorial before any of the others.  
  
> [!NOTE]  
> To replicate data securely between servers, you should implement all of the recommendations in [Replication Security Best Practices](../../relational-databases/replication/security/replication-security-best-practices.md).  
  
## What You Will Learn  
This tutorial teaches you to prepare a server so that replication can run securely with least privileges.  

In this tutorial, you will learn how to:
> [!div class="checklist"]
> * Create Windows Accounts for Replication
> * Prepare the Snapshot folder
> * Configure Distribution

## Prerequisites
This Tutorial is intended for users familiar with fundamental database operations, but who have limited exposure to replication. 

To complete this Tutorial, your system must have SQL Server Management Studio (SSMS)  and these components:  
  
-   At the Publisher server (source):  
  
    -   Any edition of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], except SQL Server Express or SQL Compact. These editions cannot be replication Publishers.   
    -   [!INCLUDE[ssSampleDBUserInputNonLocal](../../includes/sssampledbuserinputnonlocal-md.md)] sample database. To enhance security, the sample databases are not installed by default.  
  
-   Subscriber server (destination):  
  
    -   Any edition of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], except [!INCLUDE[ssEW](../../includes/ssew-md.md)]. [!INCLUDE[ssEW](../../includes/ssew-md.md)] cannot be a Subscriber in transactional replication.  
  
- Install [SQL Server Management Studio](https://docs.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms).
- Install [SQL Server 2017 Developer Edition](https://www.microsoft.com/en-us/sql-server/sql-server-downloads).
- Download an [AdventureWorks Sample Databases](https://github.com/Microsoft/sql-server-samples/releases). For instructions on restoring a database in SSMS, please see [Restoring a Database](https://docs.microsoft.com/en-us/sql/relational-databases/backup-restore/restore-a-database-backup-using-ssms). 
    
>[!NOTE]
> - Replication is not supported on SQL Servers that are more than two versions apart. For more information, please see [Supported SQL Versions in Repl Topology](https://blogs.msdn.microsoft.com/repltalk/2016/08/12/suppported-sql-server-versions-in-replication-topology/).
> - In [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], you must connect to the Publisher and Subscriber using a login that is a member of the **sysadmin** fixed server role. For more information on the sysadmin role, please see [Server Level Roles](https://docs.microsoft.com/en-us/sql/relational-databases/security/authentication-access/server-level-roles).  


**Estimated time to complete this tutorial:  30 minutes**
  
## Create Windows Accounts for Replication
In this section, you will create Windows accounts to run replication agents. You will create a separate Windows account on the local server for the following agents:  
  
|Agent|Location|Account name|  
|---------|------------|----------------|  
|Snapshot Agent|Publisher|<*machine_name*>\repl_snapshot|  
|Log Reader Agent|Publisher|<*machine_name*>\repl_logreader|  
|Distribution Agent|Publisher and Subscriber|<*machine_name*>\repl_distribution|  
|Merge Agent|Publisher and Subscriber|<*machine_name*>\repl_merge|  
  
> [!NOTE]  
> In the replication tutorials, the Publisher and Distributor share the same instance (NODE1\SQL2016) of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], while the Susbcriber instance (NODE2\SQL2016) is remote. The Publisher and Subscriber may share the same instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], but it is not a requirement. If the Publisher and Subscriber share the same instance, the steps that are used to create accounts at the Subscriber are not required.  

### Create local Windows accounts for replication agents at the Publisher
  
1.  At the Publisher, open **Computer Management** from **Administrative Tools** in Control Panel.  
  
2.  In **System Tools**, expand **Local Users and Groups**.  
  
3.  Right-click **Users** and then select **New User**.  
     
4.  Enter **repl_snapshot** in the **User name** box, provide the password and other relevant information, and then select **Create** to create the repl_snapshot account: 

       ![New user](media/tutorial-preparing-the-server-for-replication/newuser.png)
  
5.  Repeat the previous step to create the repl_logreader, repl_distribution, and repl_merge accounts:  
 
    ![Replication Users](media/tutorial-preparing-the-server-for-replication/replusers.png)
  
6.  Select **Close**.  
  
### To create local Windows accounts for replication agents at the Subscriber  
  
1.  At the Subscriber, open **Computer Management** from **Administrative Tools** in Control Panel.  
  
2.  In **System Tools**, expand **Local Users and Groups**.  
  
3.  Right-click **Users** and then select **New User**.  
  
4.  Enter **repl_distribution** in the **User name** box, provide the password and other relevant information, and then select **Create** to create the repl_distribution account.  
  
5.  Repeat the previous step to create the repl_merge account.  
  
6.  Select **Close**.  

**See Also**:
[Replication Agents Overview](../../relational-databases/replication/agents/replication-agents-overview.md)  

## Prepare the Snapshot Folder
In this section, you will learn to configure the snapshot folder that is used to create and store the publication snapshot. 

### Create a share for the snapshot folder and assign permissions  
  
1.  In Windows Explorer, navigate to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data folder. The default location is C:\Program Files\Microsoft SQL Server\MSSQL.X\MSSQL\Data.  
  
2.  Create a new folder named **repldata**.  
  
3.  Right-click this folder and select **Properties**.  
  
    a. On the **Sharing** tab in the **repldata Properties** dialog box, select **Advanced Sharing**.  
  
    b. In the **Advanced Sharing** dialog box, select **Share this Folder**, and then select **Permissions**:  

       ![Sharing Repl Data](media/tutorial-preparing-the-server-for-replication/repldata.png)

6.  In the **Permissions for repldata** dialog box, select **Add**. In the **Select User, Computers, Service Account, or Groups** text box, type the name of the Snapshot Agent account created previously, as <*Publisher_Machine_Name>***\repl_snapshot**. Select **Check Names**, and then select **OK**:  

    ![Add Sharing Permissions](media/tutorial-preparing-the-server-for-replication/addshareperms.png)

7. Repeat step 6 to add the other two accounts that were created previously: <*Publisher_Machine_Name>***\repl_merge** and <*Publisher_Machine_Name>***\repl_distribution**

8. Once these three accounts have been added, assign the following permissions:      
    - repl_distribution - Read  
    - repl_merge - Read  
    - repl_snapshot - Full Control    

  ![Shared Permissions](media/tutorial-preparing-the-server-for-replication/sharedpermissions.png)

9. Once your share permissions are configured correctly, select **OK** to close the **Permissions for repldata** dialog box. Select **OK** to close the **Advanced Sharing** dialog box. 

10.  On the **repldata Properties**, select the **Security** tab and select **Edit**:  

       ![Edit Security](media/tutorial-preparing-the-server-for-replication/editsecurity.png)   

11. In the **Permissions for repldata** dialog box, select **Add..**. In the **Select User, Computers, Service Account, or Groups** text box, type the name of the Snapshot Agent account created previously, as <*Publisher_Machine_Name>***\repl_snapshot**. Select **Check Names**, and then select **OK**:  

    ![Add Security Permissions](media/tutorial-preparing-the-server-for-replication/addsecuritypermissions.png)

  
12.  Repeat the previous step to add permissions for the Distribution Agent, as <*Publisher_Machine_Name>***\repl_distribution**, and for the Merge Agent as <*Publisher_Machine_Name>***\repl_merge**.  
    
  
13. Verify the following permissions are allowed:  
  
    - repl_distribution - Read
    - repl_merge - Read
    - repl_snapshot - Full Control   
 
    ![Repl Data User Permissions](media/tutorial-preparing-the-server-for-replication/replpermissions.png) 

14. Select the **Sharing** tab again and note down the **Network Path** for the share. You will need this path later when you're configuring your **Snapshot Folder**:  

    ![Network Path](media/tutorial-replicating-data-between-continuously-connected-servers/networkpath.png)

15. Select **OK** to close the **repldata Properties** dialog box. 
 
**See Also**:  
[Secure the Snapshot Folder](../../relational-databases/replication/security/secure-the-snapshot-folder.md)  
  

## Configure Distribution
In this section, you will configure distribution at the Publisher and set the required permissions on the publication and distribution databases. If you have already configured the Distributor, you must first disable publishing and distribution before you begin this section. Do not do this if you must retain an existing replication topology, especially in Production.   
  
Configuring a Publisher with a remote Distributor is outside the scope of this tutorial.  

### Configure distribution at the Publisher  
  
1.  Connect to the Publisher in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], and then expand the server node.  
  
2.  Right-click the **Replication** folder and select **Configure Distribution**:  

    ![Configure Distribution](media/tutorial-preparing-the-server-for-replication/configuredistribution.png)
  
    > [!NOTE]  
    > If you have connected to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] using **localhost** rather than the actual server name you will be prompted with a warning that [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is unable to connect to server **'localhost'**. Select **OK** on the warning dialog. In the **Connect to Server** dialog change the **Server name** from **localhost** to the name of your server. Select **Connect**.  
  
    The Distribution Configuration Wizard launches.  
  
3.  On the **Distributor** page, select **'ServerName' will act as its own Distributor; SQL Server will create a distribution database and log**, and then select **Next**:  

    ![Server Acts as Own Distributor](media/tutorial-preparing-the-server-for-replication/serverdistributor.png)
  
4.  If the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent is not running, on the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] **Agent Start** page, select **Yes**, configure the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent service to start automatically. Select **Next**.  

     
5.  Enter the path \\\\<*Publisher_Machine_Name>***\repldata** in the **Snapshot folder** text box, and then select **Next**. This path should match what you saw previously under **Network Path** of your repldata properties folder after configuring your share properties: 

    ![Repl Data Snapshot Folder](media/tutorial-preparing-the-server-for-replication/repldatasnapshot.png)
  
6.  Accept the default values on the remaining pages of the wizard:  
    
    ![Distribution Wizard Defaults](media/tutorial-preparing-the-server-for-replication/distributionwizarddefaults.png)
  
7.  Select **Finish** to enable distribution. 

    You might see this error when configuring the Distributor. It's an indication that the account used to start the SQL Server Agent account is not an administrator on the system. You'll either need to start the SQL Server Agent manually, grant those permissions to the existing account, or modify which account the SQL Server Agent is using: 

     ![Starting Agent Error](media/tutorial-preparing-the-server-for-replication/startingagenterror.png)

    If your SQL Server Management Studio is running with administrative rights, you can start the SQL Agent manually from within SSMS:  
        ![Start Agent from SSMS](media/tutorial-preparing-the-server-for-replication/ssmsstartagent.png) 

    >[!NOTE]
    > If the SQL Agent doesn't visibly start, right-click the SQL Server Agent in SSMS, and **Refresh**.  If it's still in the stopped state,  you'll need to start it manually from the **SQL Server Configuration Manager**.    
  
### Setting database permissions at the Publisher  
  
1.  In [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], expand **Security**, right-click **Logins**, and then select **New Login**:  

    ![New Login](media/tutorial-preparing-the-server-for-replication/newlogin.png)
  
2.  On the **General** page, select **Search**, enter <*Publisher_Machine_Name>***\repl_snapshot** in the **Enter the object name to select** box, select **Check Names**, and then select **OK**:  

    ![Add Repl Snapshot Login](media/tutorial-preparing-the-server-for-replication/addsnapshotlogin.png)
  
3.  On the **User Mapping** page, in the **Users mapped to this login** list select both the **distribution** and [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] databases.  
  
    In the Database role membership list, select the **db_owner** role for the login for both databases:  

    ![Repl Snapshot DB Owner](media/tutorial-preparing-the-server-for-replication/dbowner.png)
  
4.  Select **OK** to create the login.  
  
5.  Repeat steps 1-4 to create a login for the other local accounts (repl_distribution, repl_logreader, and repl_merge). These logins must also be mapped to users that are members of the **db_owner** fixed database role in the **distribution** and **AdventureWorks** databases:  

    ![Repl Users in SSMS](media/tutorial-preparing-the-server-for-replication/usersinssms.png)
  
  
**See Also**:  
[Configure Distribution](../../relational-databases/replication/configure-distribution.md)  
[Replication Agent Security Model](../../relational-databases/replication/security/replication-agent-security-model.md)  

## Next steps
You have now successfully prepared your server for replication. The next article teaches you how to configure Transactional Replication. 

Advance to the next article to learn more
> [!div class="nextstepaction"]
> [Next steps button](tutorial-replicating-data-between-continuously-connected-servers.md)

  
  
