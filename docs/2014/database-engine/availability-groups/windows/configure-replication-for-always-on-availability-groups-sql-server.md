---
title: "Configure Replication for Always On Availability Groups (SQL Server) | Microsoft Docs"
ms.custom: ""
ms.date: "06/14/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: high-availability
ms.topic: conceptual
helpviewer_keywords: 
  - "Availability Groups [SQL Server], interoperability"
  - "replication [SQL Server], AlwaysOn Availability Groups"
ms.assetid: 4e001426-5ae0-4876-85ef-088d6e3fb61c
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# Configure Replication for Always On Availability Groups (SQL Server)
  Configuring [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] replication and AlwaysOn availability groups involves seven steps. Each step is described in more detail in the following sections.  
  

  
##  <a name="step1"></a> 1. Configure the Database Publications and Subscriptions  
 **Configure the distributor**  
  
 The distributor should not be a host for any of the current (or intended) replicas of the availability group that the publishing database is (or will become) a member of.  
  
1.  Configure distribution at the distributor. If stored procedures are being used for configuration, run `sp_adddistributor`. Use the *@password* parameter to identify the password that will be used when a remote publisher connects to the distributor. The password will also be needed at each remote publisher when the remote distributor is set up.  
  
    ```  
    USE master;  
    GO  
    EXEC sys.sp_adddistributor  
        @distributor = 'MyDistributor',  
        @password = '**Strong password for distributor**';  
    ```  
  
2.  Create the distribution database at the distributor. If stored procedures are being used for configuration, run `sp_adddistributiondb`.  
  
    ```  
    USE master;  
    GO  
    EXEC sys.sp_adddistributiondb  
        @database = 'distribution',  
        @security_mode = 1;  
    ```  
  
3.  Configure the remote publisher. If stored procedures are being used to configure the distributor, run `sp_adddistpublisher`. The *@security_mode* parameter is used to determine how the publisher validation stored procedure that is run from the replication agents, connects to the current primary. If set to 1 Windows authentication is used to connect to the current primary. If set to 0, [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] authentication is used with the specified *@login* and *@password* values. The login and password specified must be valid at each secondary replica for the validation stored procedure to successfully connect to that replica.  
  
    > [!NOTE]  
    >  If any modified replication agents run on a computer other than the distributor, use of Windows authentication for the connection to the primary will require Kerberos authentication to be configured for the communication between the replica host computers. Use of a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] login for the connection to the current primary will not require Kerberos authentication.  
  
    ```  
    USE master;  
    GO  
    EXEC sys.sp_adddistpublisher  
        @publisher = 'AGPrimaryReplicaHost',  
        @distribution_db = 'distribution',  
        @working_directory = '\\MyReplShare\WorkingDir',  
        @login = 'MyPubLogin',  
        @password = '**Strong password for publisher**';  
    ```  
  
 For more information, see [sp_adddistpublisher &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-adddistpublisher-transact-sql).  
  
 **Configure the publisher at the original publisher**  
  
1.  Configure remote distribution. If stored procedures are being used to configure the publisher, run `sp_adddistributor`. Specify the same value for *@password* as that used when `sp_adddistrbutor` was run at the distributor to set up distribution.  
  
    ```  
    exec sys.sp_adddistributor  
        @distributor = 'MyDistributor',  
        @password = 'MyDistPass'  
    ```  
  
2.  Enable the database for replication. If stored procedures are being used to configure the publisher, run `sp_replicationdboption`. If both transactional and merge replication are to be configured for the database, each must be enabled.  
  
    ```  
    USE master;  
    GO  
    EXEC sys.sp_replicationdboption  
        @dbname = 'MyDBName',  
        @optname = 'publish',  
        @value = 'true';  
  
    EXEC sys.sp_replicationdboption  
        @dbname = 'MyDBName',  
        @optname = 'merge publish',  
        @value = 'true';  
    ```  
  
3.  Create the replication publication, articles, and subscriptions. For more information about how to configure replication, see Publishing Data and Database objects.  
  
##  <a name="step2"></a> 2. Configure the AlwaysOn Availability Group  
 At the intended primary, create the availability group with the published (or to be published) database as a member database. If using the Availability Group Wizard, you can either allow the wizard to initially synchronize the secondary replica databases or you can perform the initialization manually by using backup and restore.  
  
 Create a DNS listener for the availability group that will be used by the replication agents to connect to the current primary. The listener name that is specified will be used as the target of redirection for the original publisher/published database pair. For example, if you are using DDL to configure the availability group, the following code example can be used to specify an availability group listener for an existing availability group named `MyAG`:  
  
```  
ALTER AVAILABILITY GROUP 'MyAG'   
    ADD LISTENER 'MyAGListenerName' (WITH IP (('10.120.19.155', '255.255.254.0')));  
```  
  
 For more information, see [Creation and Configuration of Availability Groups &#40;SQL Server&#41;](creation-and-configuration-of-availability-groups-sql-server.md).  
  
##  <a name="step3"></a> 3. Insure that all of the Secondary Replica Hosts are Configured for Replication  
 At each secondary replica host, verify that [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] has been configured to support replication. The following query can be run at each secondary replica host to determine whether replication is installed:  
  
```  
USE master;  
GO  
DECLARE @installed int;  
EXEC @installed = sys.sp_MS_replication_installed;  
SELECT @installed;  
```  
  
 If *@installed* is 0, replication must be added to the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] installation.  
  
##  <a name="step4"></a> 4. Configure the Secondary Replica Hosts as Replication Publishers  
 A secondary replica cannot act as a replication publisher or republisher but replication must be configured so that the secondary can take over after a failover. At the distributor, configure distribution for each secondary replica host. Specify the same distribution database and working directory as was specified when the original publisher was added to the distributor. If you are using stored procedures to configure distribution, use `sp_adddistpublisher` to associate the remote publishers with the distributor. If *@login* and *@password* were used for the original publisher, specify the same values for each when you add the secondary replica hosts as publishers.  
  
```  
EXEC sys.sp_adddistpublisher  
    @publisher = 'AGSecondaryReplicaHost',  
    @distribution_db = 'distribution',  
    @working_directory = '\\MyReplShare\WorkingDir',  
    @login = 'MyPubLogin',  
    @password = '**Strong password for publisher**';  
```  
  
 At each secondary replica host, configure distribution. Identify the distributor of the original publisher as the remote distributor. Use the same password as that used when `sp_adddistributor` was run originally at the distributor. If stored procedures are being used to configure distribution, the *@password* parameter of `sp_adddistributor` is used to specify the password.  
  
```  
EXEC sp_adddistributor   
    @distributor = 'MyDistributor',  
    @password = '**Strong password for distributor**';  
```  
  
 At each secondary replica host, make sure that the push subscribers of the database publications appear as linked servers. If stored procedures are being used to configure the remote publishers, use `sp_addlinkedserver` to add the subscribers (if not already present) as linked servers to the publishers.  
  
```  
EXEC sys.sp_addlinkedserver   
    @server = 'MySubscriber';  
```  
  
##  <a name="step5"></a> 5. Redirect the Original Publisher to the AG Listener Name  
 At the distributor, in the distribution database, run the stored procedure `sp_redirect_publisher` to associate the original publisher and the published database with the availability group listener name of the availability group.  
  
```  
USE distribution;  
GO  
EXEC sys.sp_redirect_publisher   
@original_publisher = 'MyPublisher',  
    @publisher_db = 'MyPublishedDB',  
    @redirected_publisher = 'MyAGListenerName';  
```  
  
##  <a name="step6"></a> 6. Run the Replication Validation Stored Procedure to Verify the Configuration  
 At the distributor, in the distribution database, run the stored procedure `sp_validate_replica_hosts_as_publishers` to verify that all replica hosts are now configured to serve as publishers for the published database.  
  
```  
USE distribution;  
GO  
DECLARE @redirected_publisher sysname;  
EXEC sys.sp_validate_replica_hosts_as_publishers  
    @original_publisher = 'MyPublisher',  
    @publisher_db = 'MyPublishedDB',  
    @redirected_publisher = @redirected_publisher output;  
```  
  
 The stored procedure `sp_validate_replica_hosts_as_publishers` should be run from a login with sufficient authorization at each availability group replica host to query for information about the availability group. Unlike `sp_validate_redirected_publisher`, it uses the credentials of the caller and does not use the login retained in msdb.dbo.MSdistpublishers to connect to the availability group replicas.  
  
> [!NOTE]  
>  `sp_validate_replica_hosts_as_publishers` will fail with the following error when validating secondary replica hosts that do not allow read access, or require read intent to be specified.  
>   
>  Msg 21899, Level 11, State 1, Procedure `sp_hadr_verify_subscribers_at_publisher`, Line 109  
>   
>  The query at the redirected publisher 'MyReplicaHostName' to determine whether there were sysserver entries for the subscribers of the original publisher 'MyOriginalPublisher' failed with error '976', error message 'Error 976, Level 14, State 1, Message: The target database, 'MyPublishedDB', is participating in an availability group and is currently not accessible for queries. Either data movement is suspended or the availability replica is not enabled for read access. To allow read-only access to this and other databases in the availability group, enable read access to one or more secondary availability replicas in the group.  For more information, see the `ALTER AVAILABILITY GROUP` statement in [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Books Online.'.  
>   
>  One or more publisher validation errors were encountered for replica host 'MyReplicaHostName'.  
  
 This is expected behavior. You must verify the presence of the subscriber server entries at these secondary replica hosts by querying for the sysserver entries directly at the host.  
  
##  <a name="step7"></a> 7. Add the Original Publisher to Replication Monitor  
 At each availability group replica, add the original publisher to Replication Monitor.  
  
##  <a name="RelatedTasks"></a> Related Tasks  
 **Replication**  
  
-   [Maintaining an AlwaysOn Publication Database &#40;SQL Server&#41;](maintaining-an-always-on-publication-database-sql-server.md)  
  
-   [Replication, Change Tracking, Change Data Capture, and AlwaysOn Availability Groups &#40;SQL Server&#41;](replicate-track-change-data-capture-always-on-availability.md)  
  
-   [Replication Administration FAQ](../../../relational-databases/replication/administration/frequently-asked-questions-for-replication-administrators.md)  
  
 **To create and configure an availability group**  
  
-   [Use the Availability Group Wizard &#40;SQL Server Management Studio&#41;](use-the-availability-group-wizard-sql-server-management-studio.md)  
  
-   [Use the New Availability Group Dialog Box &#40;SQL Server Management Studio&#41;](use-the-new-availability-group-dialog-box-sql-server-management-studio.md)  
  
-   [Create an Availability Group &#40;Transact-SQL&#41;](create-an-availability-group-transact-sql.md)  
  
-   [Create an Availability Group &#40;SQL Server PowerShell&#41;](../../../powershell/sql-server-powershell.md)  
  
-   [Specify the Endpoint URL When Adding or Modifying an Availability Replica &#40;SQL Server&#41;](specify-endpoint-url-adding-or-modifying-availability-replica.md)  
  
-   [Create a Database Mirroring Endpoint for AlwaysOn Availability Groups &#40;SQL Server PowerShell&#41;](database-mirroring-always-on-availability-groups-powershell.md)  
  
-   [Join a Secondary Replica to an Availability Group &#40;SQL Server&#41;](join-a-secondary-replica-to-an-availability-group-sql-server.md)  
  
-   [Manually Prepare a Secondary Database for an Availability Group &#40;SQL Server&#41;](manually-prepare-a-secondary-database-for-an-availability-group-sql-server.md)  
  
-   [Join a Secondary Database to an Availability Group &#40;SQL Server&#41;](join-a-secondary-database-to-an-availability-group-sql-server.md)  
  
-   [Create or Configure an Availability Group Listener &#40;SQL Server&#41;](create-or-configure-an-availability-group-listener-sql-server.md)  
  
## See Also  
 [Prerequisites, Restrictions, and Recommendations for AlwaysOn Availability Groups &#40;SQL Server&#41;](prereqs-restrictions-recommendations-always-on-availability.md)   
 [Overview of AlwaysOn Availability Groups &#40;SQL Server&#41;](overview-of-always-on-availability-groups-sql-server.md)   
 [AlwaysOn Availability Groups: Interoperability (SQL Server)](always-on-availability-groups-interoperability-sql-server.md)   
 [SQL Server Replication](../../../relational-databases/replication/sql-server-replication.md)  
  
  
