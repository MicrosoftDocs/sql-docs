---
title: "Analysis Services with availability groups"
description: "If you are using Always On availability groups as your high availability solution, you can use a database in that group as a data source in an Analysis Services tabular or multidimensional solution."
author: MashaMSFT
ms.author: mathoma
ms.date: "05/17/2016"
ms.service: sql
ms.subservice: availability-groups
ms.topic: how-to
ms.custom: seodec18
monikerRange: ">=sql-server-2016"
---
# Analysis Services with Always On Availability Groups

[!INCLUDE[sql windows only](../../../includes/applies-to-version/sql-windows-only.md)]

  An Always On availability group is a predefined collection of SQL Server relational databases that failover together when conditions trigger a failover in any one database, redirecting requests to a mirrored database on another instance in the same availability group. If you are using availability groups as your high availability solution, you can use a database in that group as a data source in an Analysis Services tabular or multidimensional solution. All of the following Analysis Services operations work as expected when using an availability database: processing or importing data, querying relational data directly (using ROLAP storage or DirectQuery mode), and writeback.  
  
 Processing and querying are read-only workloads. You can improve performance by offloading these workloads to a readable secondary replica. Additional configuration is required for this scenario. Use the checklist in this topic to ensure you follow all the steps.  
  
##  <a name="bkmk_prereq"></a> Prerequisites  
 You must have a SQL Server login on all replicas. You must be a **sysadmin** to configure availability groups, listeners, and databases, but users only need **db_datareader** permissions to access the database from an Analysis Services client.  
  
 Use a data provider that supports the tabular data stream (TDS) protocol version 7.4 or newer, such as the SQL Server Native Client 11.0 or the Data Provider for SQL Server in .NET Framework 4.02.  
  
 **(For read-only workloads)**. The secondary replica role must be configured for read-only connections, the availability group must have a routing list, and the connection in the Analysis Services data source must specify the availability group listener. Instructions are provided in this topic.  
  
##  <a name="bkmk_UseSecondary"></a> Checklist: Use a secondary replica for read-only operations  
 Unless your Analysis Services solution includes writeback, you can configure a data source connection to use a readable secondary replica. If you have a fast network connection, the secondary replica has very low data latency, providing nearly identical data as the primary replica. By using the secondary replica for Analysis Services operations, you can reduce read-write contention on the primary replica and get better utilization of secondary replicas in your availability group.  
  
 By default, both read-write and read-intent access are allowed to the primary replica and no connections are allowed to secondary replicas. Additional configuration is required to set up a read-only client connection to a secondary replica. Configuration requires setting properties on the secondary replica and running a T-SQL script that defines a read-only routing list. Use the following procedures to ensure you have performed both steps.  
  
> [!NOTE]  
>  The following steps assume an existing Always On availability group and databases. If you are configuring a new group, use the New Availability Group Wizard to create the group and join the databases. The wizard checks for prerequisites, provides guidance for each step, and performs the initial synchronization. For more information, see [Use the Availability Group Wizard &#40;SQL Server Management Studio&#41;](../../../database-engine/availability-groups/windows/use-the-availability-group-wizard-sql-server-management-studio.md).  
  
#### Step 1: Configure access on an availability replica  
  
1.  In Object Explorer, connect to the server instance that hosts the primary replica, and expand the server tree.  
  
    > [!NOTE]  
    >  These steps are taken from [Configure Read-Only Access on an Availability Replica &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/configure-read-only-access-on-an-availability-replica-sql-server.md), which provides additional information and alternative instructions for performing this task.  
  
2.  Expand the **Always On High Availability** node and the **Availability Groups** node.  
  
3.  Click the availability group whose replica you want to change. Expand **Availability Replicas**.  
  
4.  Right-click the secondary replica, and click **Properties**.  
  
5.  In the **Availability Replica Properties** dialog box, change the connection access for the secondary role, as follows:  
  
    -   In the **Readable secondary** drop list, select **Read-intent only**.  
  
    -   In the **Connections in primary role** drop list, select **Allow all connections**. This is the default.  
  
    -   Optionally, in **Availability mode** drop list, select **Synchronous commit**. This step is not required, but setting it ensures that there is data parity between the primary and secondary replica.  
  
         This property is also a requirement for planned failover. If you want to perform a planned manual failover for testing purposes, set **Availability mode** to **Synchronous commit** for both the primary and secondary replica.  
  
#### Step 2: Configure read-only routing  
  
1.  Connect to the primary replica.  
  
    > [!NOTE]  
    >  These steps are taken from [Configure Read-Only Routing for an Availability Group &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/configure-read-only-routing-for-an-availability-group-sql-server.md), which provides additional information and alternative instructions for performing this task.  
  
2.  Open a query window and paste in the following script. This script does three things: enables readable connections to a secondary replica (which is off by default), sets the read-only routing URL, and creates the routing list that prioritizes how connection requests are directed.  The first statement, allowing readable connections, is redundant if you already set the properties in [!INCLUDE[ssManStudio](../../../includes/ssmanstudio-md.md)], but are included for completeness.  
  
    ```  
    ALTER AVAILABILITY GROUP [AG1]  
     MODIFY REPLICA ON  
    N'COMPUTER01' WITH   
    (SECONDARY_ROLE (ALLOW_CONNECTIONS = READ_ONLY));  
  
    ALTER AVAILABILITY GROUP [AG1]  
     MODIFY REPLICA ON  
    N'COMPUTER01' WITH   
    (SECONDARY_ROLE (READ_ONLY_ROUTING_URL = N'TCP://COMPUTER01.contoso.com:1433'));  
  
    ALTER AVAILABILITY GROUP [AG1]  
     MODIFY REPLICA ON  
    N'COMPUTER02' WITH   
    (SECONDARY_ROLE (ALLOW_CONNECTIONS = READ_ONLY));  
  
    ALTER AVAILABILITY GROUP [AG1]  
     MODIFY REPLICA ON  
    N'COMPUTER02' WITH   
    (SECONDARY_ROLE (READ_ONLY_ROUTING_URL = N'TCP://COMPUTER02.contoso.com:1433'));  
  
    ALTER AVAILABILITY GROUP [AG1]   
    MODIFY REPLICA ON  
    N'COMPUTER01' WITH   
    (PRIMARY_ROLE (READ_ONLY_ROUTING_LIST=('COMPUTER02','COMPUTER01')));  
  
    ALTER AVAILABILITY GROUP [AG1]   
    MODIFY REPLICA ON  
    N'COMPUTER02' WITH   
    (PRIMARY_ROLE (READ_ONLY_ROUTING_LIST=('COMPUTER01','COMPUTER02')));  
    GO  
    ```  
  
3.  Modify the script, replacing placeholders with values that are valid for your deployment:  
  
    -   Replace 'Computer01' with the name of the server instance that hosts the primary replica.  
  
    -   Replace 'Computer02' with the name of the server instance that hosts the secondary replica.  
  
    -   Replace 'contoso.com' with the name of your domain, or omit it from the script if all computers are in the same domain. Keep the port number if the listener is using the default port. The port that is actually used by the listener is listed in the properties page in [!INCLUDE[ssManStudio](../../../includes/ssmanstudio-md.md)].  
  
4.  Execute the script.  
  
     Next, create a data source in an Analysis Services model that uses a database from the group you just configured.  
  
##  <a name="bkmk_ssasAODB"></a> Create an Analysis Services data source using an Always On availability database  
 This section explains how to create an Analysis Services data source that connects to a database in an availability group. You can use these instructions to configure a connection to either a primary replica (default) or a readable secondary replica that you configured based on steps in a previous section. Always On configuration settings, plus the connection properties set in the client, will determine whether a primary or secondary replica is used.  
  
1.  In [!INCLUDE[ssBIDevStudio](../../../includes/ssbidevstudio-md.md)], in an Analysis Services Multidimensional and Data Mining Model project, right-click **Data Sources** and select **New Data Source**. Click **New** to create a new data source.  
  
     Alternatively, for a tabular model project, click the Model menu, and then click **Import from Data Source**.  
  
2.  In Connection Manager, in Provider, choose a provider that supports the Tabular Data Stream (TDS) protocol. The SQL Server Native Client 11.0 supports this protocol.  
  
3.  In Connection Manager, in Server Name, enter the name of the *availability group listener*, and then choose a database that is available in the group.  
  
     The availability group listener redirects a client connection to a primary replica for read-write requests or to a secondary replica if you specify read-intent in the connection string. Because replica roles will change during a failover (where the primary becomes the secondary and a secondary becomes a primary), you should always specify the listener so that the client connection is redirected accordingly.  
  
     To determine the name of the availability group listener, you can either ask a database administrator or connect to an instance in the availability group and view its Always On availability configuration.   
  
4.  Still in Connection Manager, click **All** in the left navigation pane to view the property grid of data provider.  
  
     Set **Application Intent** to **READONLY** if you are configuring a read-only client connection to a secondary replica. Otherwise, keep the **READWRITE** default to redirect the connection to the primary replica.  
  
5.  In Impersonation Information, select **Use a specific Windows user name and password**, and then enter a Windows domain user account that has a minimum of **db_datareader** permissions on the database.  
  
     Do not choose **Use the credentials of the current user** or **Inherit**. You can choose **Use the service account**, but only if that account has read permissions on the database.  
  
     Finish the data source and close the Data Source Wizard.  
  
6.  Add **MultiSubnetFailover=Yes** to the connection string to provide faster detection and connection to the active server. For more information about this property, see [SQL Server Native Client Support for High Availability, Disaster Recovery](../../../relational-databases/native-client/features/sql-server-native-client-support-for-high-availability-disaster-recovery.md).  
  
     This property is not visible in the property grid. To add the property, right-click the data source and choose **View Code**. Add `MultiSubnetFailover=Yes` to the connection string.  
  
 The data source is now defined. You can now proceed to build a model, starting with the data source view, or in the case of tabular models, creating relationships. When you are at a point where data must be retrieved from the availability database (for example when you are ready to process or deploy the solution), you can test the configuration to verify data is accessed from the secondary replica.  
  
##  <a name="bkmk_test"></a> Test the configuration  
 After you configure the secondary replica and create a data source connection in Analysis Services, you can confirm that processing and query commands are redirected to the secondary replica. You can also perform a planned manual failover to verify your recovery plan for this scenario.  
  
#### Step 1: Confirm the data source connection is redirected to the secondary replica  
  
1.  Start SQL Server Profiler and connect to the SQL Server instance hosting the secondary replica.  
  
     As the trace runs, the **SQL:BatchStarting** and **SQL:BatchCompleting** events will show the queries issued from Analysis Services that are executing on the database engine instance. These events are selected by default so all you need to do is start the trace.  
  
2.  In [!INCLUDE[ssBIDevStudio](../../../includes/ssbidevstudio-md.md)], open the Analysis Services project or solution containing a data source connection you want to test. Be sure that the data source specifies the availability group listener and not an instance in the group.  
  
     This step is important. Routing to the secondary replica will not occur if you specify a server instance name.  
  
3.  Arrange the application windows so that you can view SQL Server Profiler and [!INCLUDE[ssBIDevStudio](../../../includes/ssbidevstudio-md.md)] side by side.  
  
4.  Deploy the solution, and when it completes, stop the trace.  
  
     In the trace window, you should see events from the application **Microsoft SQL Server Analysis Services**. You should see **SELECT** statements that retrieve data from a database on the server instance that hosts the secondary replica, proving that the connection was made via the listener to the secondary replica.  
  
#### Step 2: Perform a planned failover to test the configuration  
  
1.  In [!INCLUDE[ssManStudio](../../../includes/ssmanstudio-md.md)] check the primary and secondary replicas to ensure that both are configured for synchronous-commit mode and are currently synchronized.  
  
     The following steps assume a secondary replica is configured for synchronous commit.  
  
     To verify synchronization, open a connection to each instance that hosts the primary and secondary replicas, expand the Databases folder, and ensure that the database has **(Synchronized)** and **(Synchronizing)** appended to its name in each replica.  
  
    > [!NOTE]  
    >  These steps are taken from [Perform a Planned Manual Failover of an Availability Group &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/perform-a-planned-manual-failover-of-an-availability-group-sql-server.md), which provides additional information and alternative instructions for performing this task.  
  
2.  In SQL Server Profiler, start traces for each replica and view the traces side-by-side. In the following steps, you will compare traces, confirming that the SQL queries used for processing or querying from Analysis Services switch from one replica to the other.  
  
3.  Execute a processing or query command from within Analysis Services. Because you configured the data source for a read-only connection, you should see the command execute on the secondary replica.  
  
4.  In [!INCLUDE[ssManStudio](../../../includes/ssmanstudio-md.md)], connect to the secondary replica.  
  
5.  Expand the **Always On High Availability** node and the **Availability Groups** node.  
  
6.  Right-click the availability group to be failed over, and select the **Failover** command. This starts the Fail Over Availability Group Wizard. Use the wizard to choose which replica to make the new primary replica.  
  
7.  Confirm that failover succeeded:  
  
    -   In [!INCLUDE[ssManStudio](../../../includes/ssmanstudio-md.md)], expand the availability groups to view the (primary) and (secondary) designations. The instance that was previously a primary replica should now be a secondary replica.  
  
    -   View the dashboard to determine if any health issues were detected. Right-click the availability group and select **Show Dashboard**.  
  
8.  Wait one or two minutes for the failover to complete on the backend.  
  
9. Repeat the processing or query command in the Analysis Services solution, and then watch the traces side by side in SQL Server Profiler. You should see evidence of processing on the other instance, which is now the new secondary replica.  
  
##  <a name="bkmk_whathappens"></a> What happens after a failover occurs  
 During a failover, a secondary replica transitions to the primary role and the former primary replica transitions to the secondary role. All client connections are terminated, ownership of the availability group listener moves with the primary replica role to a new SQL Server instance, and the listener endpoint is bound to the new instance's virtual IP addresses and TCP ports. For more information, see [About Client Connection Access to Availability Replicas &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/about-client-connection-access-to-availability-replicas-sql-server.md).  
  
 If failover occurs during processing, the following error occurs in Analysis Services in the log file or output window: "OLE DB error: OLE DB or ODBC error: Communication link failure; 08S01; TPC Provider: An existing connection was forcibly closed by the remote host. ; 08S01."  
  
 This error should resolve if you wait a minute and try again. If the availability group is configured correctly for readable secondary replica, processing will resume on the new secondary replica when you retry processing.  
  
 Persistent errors are most likely due to a configuration problem. You can try re-running the T-SQL script to resolve problems with the routing list, read-only routing URLs, and read-intent on the secondary replica. You should also verify that the primary replica allows all connections.  
  
##  <a name="bkmk_writeback"></a> Writeback when using an Always On availability database  
 Writeback is an Analysis Services feature that supports What If analysis in Excel. It is also commonly used for budgeting and forecasting tasks in custom applications.  
  
 Support for writeback requires a READWRITE client connection. In Excel, if you attempt to write back on a read-only connection, the following error will occur: "Data could not be retrieved from the external data source." "Data could not be retrieved from the external data source."  
  
 If you configured a connection to always access a readable secondary replica, you must now configure a new connection that uses a READWRITE connection to the primary replica.  
  
 To do this, create an additional data source in an Analysis Services model to support the read-write connection. When creating the additional data source, use the same listener name and database that you specified in the read-only connection, but instead of modifying **Application Intent**, keep the default that supports READWRITE connections. You can now add new fact or dimension tables to your data source view that are based on the read-write data source, and then enable writeback on the new tables.  
  
## See Also  
 [Availability Group Listeners, Client Connectivity, and Application Failover &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/listeners-client-connectivity-application-failover.md)   
 [Active Secondaries: Readable Secondary Replicas &#40;Always On Availability Groups&#41;](../../../database-engine/availability-groups/windows/active-secondaries-readable-secondary-replicas-always-on-availability-groups.md)   
 [Always On Policies for Operational Issues with Always On Availability Groups &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/always-on-policies-for-operational-issues-always-on-availability.md)   
 [Create a Data Source &#40;SSAS Multidimensional&#41;](/analysis-services/multidimensional-models/create-a-data-source-ssas-multidimensional)   
 [Enable Dimension Writeback](/analysis-services/multidimensional-models/bi-wizard-enable-dimension-writeback)  
  
