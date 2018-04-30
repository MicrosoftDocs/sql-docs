---
title: "Tutorial: Configure Replication between Two Fully Connected Servers (Transactional) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: "sql"
ms.prod_service: "database-engine"
ms.service: ""
ms.component: "replication"
ms.reviewer: ""
ms.suite: "sql"
ms.technology: 
  - "replication"
ms.tgt_pltfrm: ""
ms.topic: "article"
applies_to: 
  - "SQL Server 2016"
helpviewer_keywords: 
  - "tutorials [SQL Server replication]"
  - "replication [SQL Server], tutorials"
  - "wizards [SQL Server replication]"
ms.assetid: 7b18a04a-2c3d-4efe-a0bc-c3f92be72fd0
caps.latest.revision: 21
author: "MashaMSFT"
ms.author: "mathoma"
manager: "craigg"
---
# Tutorial: Configure Replication between Two Fully Connected Servers (Transactional)
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
Transactional Replication is a good solution to the problem of moving data between continuously connected servers. Using  the Replication Wizard, you can easily configure and administer a replication topology. This tutorial shows you how to configure a Transactional Replication topology for continuously connected servers. For more information about how Transactional Replication works, please see [Transactional Replication Overview](https://docs.microsoft.com/en-us/sql/relational-databases/replication/transactional/transactional-replication). 
  
## What You Will Learn  
This tutorial will show you how to publish data from one database to another using transactional replication. 

In this tutorial, you will learn how to:
> [!div class="checklist"]
> * Create a publisher via Transactional Replication
> * Create a subscriber for the Transactional publisher
> * Validate the Subscription and measure latency

  
  
## Prerequisites  
This tutorial is intended for users who are familiar with basic database operations, but who have limited experience with replication. This tutorial requires that you have completed the previous tutorial, [Preparing the Server for Replication](../../relational-databases/replication/tutorial-preparing-the-server-for-replication.md).  
  
To use this Tutorial, your system must have SQL Server Management Studio (SSMS)  and these components:  
  
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
  
  
**Estimated time to complete this tutorial: 60 minutes.**  
  
## Configure the Publisher for Transactional Replication
In this section, you will create a transactional publication using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] to publish a filtered subset of the **Product** table in the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] sample database. You will also add the SQL Server login used by the Distribution Agent to the publication access list (PAL). Before starting this tutorial, you should have completed the previous tutorial, [Preparing the Server for Replication](../../relational-databases/replication/tutorial-preparing-the-server-for-replication.md).


### Create a publication and define articles
1.  Connect to the Publisher in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], and then expand the server node.  
  
2. Right-click the **SQL Server Agent** and select **Start**. The SQL Server Agent should be running before you create the publication. If this does not start your agent, you'll need to do so manually from the **SQL Server Configuration Manager**. 
3. Expand the **Replication** folder, right-click the **Local Publications** folder, and select **New Publication**.  This will launch the Publication Configuration Wizard:  

    ![New Publication](media/tutorial-replicating-data-between-continuously-connected-servers/newpublication.png)
  
  
3. On the Publication Database page, select [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)], and then select **Next**.  
  
4. On the Publication Type page, select **Transactional publication**, and then select **Next**:  

    ![Transactional Replication](media/tutorial-replicating-data-between-continuously-connected-servers/tranrepl.png)
  
5. On the Articles page, expand the **Tables** node, select the **Product** check box. Then expand **Product** and clear the checkboxes next to  **ListPrice** and **StandardCost**. Select **Next**:  

    ![Articles to publish](media/tutorial-replicating-data-between-continuously-connected-servers/replarticles.png)
  
6. On the Filter Table Rows page, select **Add**.   
  
7. In the **Add Filter** dialog box, select the **SafetyStockLevel** column, select the right arrow to add the column to the Filter statement WHERE clause of the filter query. Then manually type in the WHERE clause modifier as follows:  
  
    ```sql  
    WHERE [SafetyStockLevel] < 500  
    ```
  
    ![Filter Statement](media/tutorial-replicating-data-between-continuously-connected-servers/filter.png)
  
8. Select **OK**, and then select **Next**.  
  
9. Select the **Create a snapshot immediately and keep the snapshot available to initialize subscriptions** check box, and select **Next**:  

    ![Snapshot Agent](media/tutorial-replicating-data-between-continuously-connected-servers/snapshot.png)
  
10. On the Agent Security page, clear **Use the security settings from the Snapshot Agent** check box.   
  
    a. Select **Security Settings** for the Snapshot Agent, enter <*Publisher_Machine_Name>***\repl_snapshot** in the **Process account** box, supply the password for this account, and then select **OK**:  

    ![Snapshot Agent Security](media/tutorial-replicating-data-between-continuously-connected-servers/snapshotagentsecurity.png)
  
12. Repeat the previous step to set <*Publisher_Machine_Name*>**\repl_logreader** as the process account for the Log Reader Agent, and then select **OK**:  

    ![Log Reader Agent Security](media/tutorial-replicating-data-between-continuously-connected-servers/logreaderagentsecurity.png)   

  
13. On the Complete the Wizard page, type **AdvWorksProductTrans** in the **Publication name** box, and select **Finish**:  

    ![Name the Publication](media/tutorial-replicating-data-between-continuously-connected-servers/advworksproducttrans.png)
  
14. After the publication is created, select **Close** to complete the wizard. 

    You may encounter the following error if your SQL Server Agent is not running when you attempt to create the publication. This is an indication that your publication was created successfully, but your Snapshot Agent was unable to start. If this happens, you'll need to start the SQL Server Agent, and then manually start the Snapshot Agent. Instructions for this are covered in the next section: 

    ![Snapshot Agent Fails to Start](media/tutorial-replicating-data-between-continuously-connected-servers/snapshotagenterror.png)
    
  
### To view the status of snapshot generation  
  
1.  Connect to the Publisher in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], expand the server node, and then expand the **Replication** folder.  
  
2.  In the **Local Publications** folder, right-click **AdvWorksProductTrans**, and then select **View Snapshot Agent Status**:  

    ![Snapshot Agent Status](media/tutorial-replicating-data-between-continuously-connected-servers/viewsnapshot.png)
  
3.  The current status of the Snapshot Agent job for the publication is displayed. Verify that the snapshot job has succeeded before you continue to the next section.
          
    If your SQL Server Agent was not running when you first created the publication, you'll see that the Snapshot Agent was 'never run' when you check the **Snapshot Agent Status** for your publication. If that's the case, select **Start** to start your Snapshot Agent: 

       ![Start Snapshot Agent](media/tutorial-replicating-data-between-continuously-connected-servers/startsnapshotagent.png)
     
       If you see an error here, please see [Troubleshooting Snapshot Agent Errors](../../troubleshooters/replication/troubleshoot-tran-repl-errors.md#troubleshoot-errors-with-snapshot-agent). 

  
### To add the Distribution Agent login to the PAL  
  
1.  Connect to the Publisher in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], expand the server node, and then expand the **Replication** folder.  
  
2.  In the **Local Publications** folder, right-click **AdvWorksProductTrans**, and then select **Properties**.  The **Publication Properties** dialog box is displayed.    
  
    a. Select the **Publication Access List** page, and select **Add**.  
    b. In the **Add Publication Access** dialog box, select <*Publisher_Machine_Name>***\repl_distribution** and select **OK**. Select **OK**:

   
   ![Add Login to PAL List](media/tutorial-replicating-data-between-continuously-connected-servers/tranreplproperties.png)

**See Also**:  
[Replication Programming Concepts](../../relational-databases/replication/concepts/replication-programming-concepts.md)  
  

## Create a subscription to the Transactional publication
In this section, you will add a subscriber to the Publication that was previously created. This tutorial uses a remote subscriber (NODE2\SQL2016) but a subscription can also be added locally to the publisher. 

### To create the subscription  
  
1.  Connect to the Publisher in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], expand the server node, and then expand the **Replication** folder.  
  
2.  In the **Local Publications** folder, right-click the **AdvWorksProductTrans** publication, and then select **New Subscriptions**.  The New Subscription Wizard launches: 
 
    ![New Subscription](media/tutorial-replicating-data-between-continuously-connected-servers/newsub.png)     
  
3.  On the Publication page, select **AdvWorksProductTrans**, and then select **Next**:  

    ![Select Tran Publisher](media/tutorial-replicating-data-between-continuously-connected-servers/selectpub.png)
  
4.  On the Distribution Agent Location page, select **Run all agents at the Distributor**, and then select **Next**.  For more information on pull and push subscriptions, please see [Subscribe to Publications](https://docs.microsoft.com/en-us/sql/relational-databases/replication/subscribe-to-publications):

    ![Run Agents at Dist](media/tutorial-replicating-data-between-continuously-connected-servers/runagentsatdist.png)
  
5.  On the Subscribers page, if the name of the Subscriber instance is not displayed, select **Add Subscriber** and then select **Add SQL Server Subscriber** from the drop-down. This will launch the **Connect to Server** dialog box. Enter the Subscriber instance name and then select **Connect**.  
    
    a. Once the Subscriber has been added, check the box next to the instance name of your Subscriber. Then select **New Database** under **Subscription Database**:   

  ![Add Subscriber Server](media/tutorial-replicating-data-between-continuously-connected-servers/addsub.png)

7. This will launch the **New Database** dialog box. Enter **ProductReplica** in the **Database name** box, select **OK**, and then select **Next**: 
  
    ![Product Replica DB](media/tutorial-replicating-data-between-continuously-connected-servers/productreplica.png)
  
8.  In the **Distribution Agent Security** dialog box, select the ellipsis (**…**) button. Enter <*Publisher_Machine_Name>***\repl_distribution** in the **Process account** box, enter the password for this account, select **OK**, and then select **Next**:

    ![Add Distribution Account](media/tutorial-replicating-data-between-continuously-connected-servers/adddistaccount.png)
  
9. Select **Finish** to accept the default values on the remaining pages and complete the wizard.  
  
### Setting database permissions at the Subscriber  
  
1.  Connect to the Subscriber in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], expand **Security**, right-click **Logins**, and then select **New Login**.     
  
    a. On the **General** page, under **Login Name** select **Search** and add the login for <*Subscriber_Machine_Name>***\repl_distribution**.
    b. On the **User Mappings** page, grant the login **db_owner** for the **ProductReplica** database: 

    ![Login on Subscriber](media/tutorial-replicating-data-between-continuously-connected-servers/loginforsub.png)

2. Select **OK** to close the **New Login** dialog box. 

  
### To view the synchronization status of the subscription  
  
1.  Connect to the Publisher in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], expand the server node, and then expand the **Replication** folder.  
  
2.  In the **Local Publications** folder, expand the **AdvWorksProductTrans** publication, right-click the subscription in the **ProductReplica** database, and then select **View Synchronization Status**. The current synchronization status of the subscription is displayed:  
    ![View Synchronization Status](media/tutorial-replicating-data-between-continuously-connected-servers/viewsyncstatus.png)
3.  If the subscription is not visible under **AdvWorksProductTrans**, press F5 to refresh the list.  
  
**See Also**:  
[Initialize a Subscription with a Snapshot](../../relational-databases/replication/initialize-a-subscription-with-a-snapshot.md)  
[Create a Push Subscription](../../relational-databases/replication/create-a-push-subscription.md)  
[Subscribe to Publications](../../relational-databases/replication/subscribe-to-publications.md)  

## Measuring Replication Latency
In this section, you will use tracer tokens to verify that changes are being replicated to the Subscriber and to determine latency. Latency is the time it takes for a change made at the Publisher to appear to the Subscriber.
  
1. Connect to the Publisher in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], expand the server node, right-click the **Replication** folder, and then select **Launch Replication Monitor**:

    ![Launch Replication Monitor](media/tutorial-replicating-data-between-continuously-connected-servers/launchreplmonitor.png)

2. Expand a Publisher group in the left pane, expand the Publisher instance, and then select the **AdvWorksProductTrans** publication.  
  
    a. Select the **Tracer Tokens** tab.  
    b. Select **Insert Tracer**.    
    c. View elapsed time for the tracer token in the following columns: **Publisher to Distributor**, **Distributor to Subscriber**, **Total Latency**. A value of **Pending** indicates that the token has not reached a given point:


   ![Tracer Token](media/tutorial-replicating-data-between-continuously-connected-servers/tracertoken.png)


**See Also**   
[Measure Latency and Validate Connections for Transactional Replication](../../relational-databases/replication/monitor/measure-latency-and-validate-connections-for-transactional-replication.md)
[Troubleshooting Transactional Replication Sync Errors](../../troubleshooters/replication/troubleshoot-tran-repl-errors.md)



  ## Next steps
You have successfully configured both your Publisher and your Subscriber for your Transactional Replication.  You can now insert, update, or delete data in the **Product**  table at the Publisher. Then you can query the **Product** table at the Subscriber to view the replicated changes. The next article will teach you how to configure Merge replication.  

Advance to the next article to learn more
> [!div class="nextstepaction"]
> [Next steps](tutorial-replicating-data-with-mobile-clients.md)

  
