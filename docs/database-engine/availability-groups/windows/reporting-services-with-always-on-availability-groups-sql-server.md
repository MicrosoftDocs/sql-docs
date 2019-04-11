---
title: "Reporting Services with Always On Availability Groups (SQL Server) | Microsoft Docs"
ms.custom: ""
ms.date: "05/17/2016"
ms.prod: sql
ms.reviewer: ""
ms.technology: high-availability


ms.topic: conceptual
helpviewer_keywords: 
  - "Reporting Services, AlwaysOn Availability Groups"
  - "Availability Groups [SQL Server], interoperability"
ms.assetid: edeb5c75-fb13-467e-873a-ab3aad88ab72
author: MashaMSFT
ms.author: mathoma
manager: "erikre"
---
# Reporting Services with Always On Availability Groups (SQL Server)
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]

  This topic contains information about configuring [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)] to work with [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)] (AG) in [!INCLUDE[ssCurrent](../../../includes/sscurrent-md.md)]. The three scenarios for using [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)] and [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)] are databases for report data sources, report server databases, and report design. The supported functionality and required configuration is different for the three scenarios.  
  
 A key benefit of using [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)] with [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)] data sources is to leverage readable secondary replicas as a reporting data source while, at the same time the secondary replicas are providing a failover for a primary database.  
  
 For general information on [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)], see [Always On FAQ for SQL Server 2012 (https://msdn.microsoft.com/sqlserver/gg508768)](https://msdn.microsoft.com/sqlserver/gg508768).  
  
 **In This Topic:**  
  
-   [Requirements for using Reporting Services and Always On Availability Groups](#bkmk_requirements)  
  
-   [Report Data Sources and Availability Groups](#bkmk_reportdatasources)  
  
-   [Report Design and Availability Groups](#bkmk_reportdesign)  
  
-   [Report Server Databases and Availability Groups](#bkmk_reportserverdatabases)  
  
-   -   [Differences between SharePoint Native Mode](#bkmk_differences_in_server_mode)  
  
    -   [Prepare Report Server Databases for Availability Groups](#bkmk_prepare_databases)  
  
    -   [Steps to complete disaster recovery of Report Server Databases](#bkmk_steps_to_complete_failover)  
  
    -   [Report Server Behavior When a Failover Occurs](#bkmk_failover_behavior)  
  
##  <a name="bkmk_requirements"></a> Requirements for using Reporting Services and Always On Availability Groups  
 [!INCLUDE[ssCurrent](../../../includes/sscurrent-md.md)] [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)] and Power BI Report Server uses the .Net framework 4.0 and supports [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)] connection string properties for use with data sources.  
  
 To use [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)] with  [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)] 2014, and earlier, you need to download and install a hotfix for .Net 3.5 SP1. The hotfix adds support to SQL Client for AG features and support of the connection string properties **ApplicationIntent** and **MultiSubnetFailover**. If the Hotfix is not installed on each computer that hosts a report server, then users attempting to preview reports will see an error message similar to the following, and the error message will be written to the report server trace log:  
  
> **Error message:** "Keyword not supported 'applicationintent'"  
  
 The message occurs when you include one of the [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)] properties in the [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)] connection string, but the server does not recognize the property. The noted error message will be seen when you click the 'Test Connection' button in [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)] user interfaces and when you preview the report if remote errors are enabled on the report servers.  
  
 For more information on the required hotfix, see [KB 2654347A hotfix introduces support for the Always On features from SQL Server 2012 to the .NET Framework 3.5 SP1](https://go.microsoft.com/fwlink/?LinkId=242896).  
  
 For information on other [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)] requirements, see [Prerequisites, Restrictions, and Recommendations for Always On Availability Groups &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/prereqs-restrictions-recommendations-always-on-availability.md).  
  
> [!NOTE]  
>  [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)] configuration files such as **RSreportserver.config** are not supported as part of [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)] functionality. If you manually make changes to a configuration file on one of the report servers, you will need to manually update the replicas.  
  
##  <a name="bkmk_reportdatasources"></a> Report Data Sources and Availability Groups  
 The behavior of [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)] data sources based on [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)] can vary depending on how your administrator has configured the AG environment.  
  
 To utilize [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)] for report data sources you need to configure the report data source connection string is to use the availability group *Listener DNS name*. Supported data sources are the following:  
  
-   ODBC data source using SQL Native Client.  
  
-   SQL Client, with the .Net hotfix applied to the report server.  
  
 The connection string can also contain new Always On connection properties that configure the report query requests to use secondary replica for read-only reporting. Use of secondary replica for reporting requests will reduce the load on a read-write primary replica. The following illustration is an example of a three replica AG configuration where the [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)] data source connection strings have been configured with ApplicationIntent=ReadOnly. In this example the report query requests are sent to a secondary replica and not the primary replica.  
  
 
  
 The following is an example connection string, where the [AvailabilityGroupListenerName] is the **Listener DNS Name** that was configured when replicas were created:  
  
 `Data Source=[AvailabilityGroupListenerName];Initial Catalog = AdventureWorks2016; ApplicationIntent=ReadOnly`  
  
 The **Test Connection** button in [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)] user interfaces will validate if a connection can be established but it will not validate AG configuration. For example if you include ApplicationIntent in a connection string to a server that is not part of AG, the extra parameter is ignored and the **Test Connection** button will only validate a connection can be established to the specified server.  
  
 Depending on how your reports are created and published will determine where you edit the connection string:  
  
-   **Native mode:** Use the [!INCLUDE[ssRSWebPortal-Non-Markdown](../../../includes/ssrswebportal-non-markdown-md.md)] for shared data sources and reports that are already published to a native mode report server.  
  
-   **SharePoint Mode:** Use SharePoint configuration pages within the document libraries for reports that are already published to a SharePoint server.  
  
-   **Report Design:** [!INCLUDE[ssRBnoversion](../../../includes/ssrbnoversion.md)] or [!INCLUDE[ssBIDevStudioFull](../../../includes/ssbidevstudiofull-md.md)] when you are creating new reports. See the 'Report Design' section in this topic or more information.  
  
 **Additional Resources:**  
  
-   [Manage Report Data Sources](../../../reporting-services/report-data/manage-report-data-sources.md)  
  
-   For more information on the available connection string properties, see [Using Connection String Keywords with SQL Server Native Client](../../../relational-databases/native-client/applications/using-connection-string-keywords-with-sql-server-native-client.md).  
  
-   For more information on availability group listeners, see [Create or Configure an Availability Group Listener &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/create-or-configure-an-availability-group-listener-sql-server.md).  
  
 **Considerations:** Secondary replicas will typically experience a delay in receiving data changes from the primary replica. The following factors can affect the update latency between the primary and secondary replicas:  
  
-   The number of secondary replicas. The delay increases with each secondary replica added to the configuration.  
  
-   Geographic location and distance between the primary and secondary replicas. For example the delay is typically larger if the secondary replicas are in a different data center than if they were in the same building as the primary replica.  
  
-   Configuration of the availability mode for each replica. The availability mode determines whether the primary replica waits to commit transactions on a database until a secondary replica has written the transaction to disk. For more information, see the 'Availability Modes' section of [Overview of Always On Availability Groups &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/overview-of-always-on-availability-groups-sql-server.md).  
  
 When using a read-only secondary as a [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)] data source, it is important to ensure that data update latency meets the needs of the report users.  
  
##  <a name="bkmk_reportdesign"></a> Report Design and Availability Groups  
 When designing reports in [!INCLUDE[ssRBnoversion](../../../includes/ssrbnoversion.md)] or a report project in [!INCLUDE[ssBIDevStudioFull](../../../includes/ssbidevstudiofull-md.md)], a user can configure a report data source connection string to contain new connection properties provided by [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)]. Support for the new connection properties depends on where a user previews the report.  
  
-   **Local preview:** [!INCLUDE[ssRBnoversion](../../../includes/ssrbnoversion.md)] and [!INCLUDE[ssBIDevStudioFull](../../../includes/ssbidevstudiofull-md.md)] use the .Net framework 4.0 and support [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)] connection string properties.  
  
-   **Remote or server mode preview:** If after publishing reports to the report server or using preview in [!INCLUDE[ssRBnoversion](../../../includes/ssrbnoversion.md)], you see an error similar to the following, it is an indication you are previewing reports against the report server and the .Net Framework 3.5 SP1 Hotfix for [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)] has not been installed on the report server.  
  
> **Error message:** "Keyword not supported 'applicationintent'"  
  
##  <a name="bkmk_reportserverdatabases"></a> Report Server Databases and Availability Groups  
 Reporting Services and Power BI Report Server offers limited support for using [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)] with report server databases. The report server databases can be configured in AG to be part of a replica; however [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)] will not automatically use a different replica for the report server databases when a failover occurs. The use of MultiSubnetFailover, with the report server databases, is not supported.  
  
 Manual actions or custom automation scripts need to be used to complete the failover and recovery. Until these actions are completed, some features of the report server may not work correctly after the [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)] failover.  
  
> [!NOTE]  
>  When planning failover and disaster recovery for the report server databases, it is advised you always backup a copy of the report server encryption key.  
  
###  <a name="bkmk_differences_in_server_mode"></a> Differences between SharePoint Native Mode  
 This section summarizes the differences between how SharePoint mode and Native mode report servers interact with [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)].  
  
 A SharePoint report server creates **3** databases for each [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)] service application you create. The connection to the report server databases in SharePoint mode is configured in SharePoint Central Administration when you create the service application. The default names of the databases include a GUID that is associated with the service application. The following are example database names, for a SharePoint mode report server:  
  
-   ReportingService_85c08ac3c8e64d3cb400ad06ed5da5d6  
  
-   ReportingService_85c08ac3c8e64d3cb400ad06ed5da5d6TempDB  
  
-   ReportingService_85c08ac3c8e64d3cb400ad06ed5da5d6_Alerting  
  
 Native mode report servers use **2** databases. The following are example database names, for a native mode report server:  
  
-   ReportServer  
  
-   ReportServerTempDB  
  
 Native mode does not support or use the Alerting databases and related features. You configure native mode report servers in the [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)] Configuration Manager. For SharePoint mode, you configure the service application database name to be the name of the "client access point" you created as part of the SharePoint configuration. For more information on configuring SharePoint with [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)], see [Configure and manage SQL Server availability groups for SharePoint Server (https://go.microsoft.com/fwlink/?LinkId=245165)](https://go.microsoft.com/fwlink/?LinkId=245165).  
  
> [!NOTE]
>  SharePoint mode report servers use a synchronization process between the [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)] service application databases and the SharePoint content databases. It is important to maintain the report server databases and content databases together. You should consider configuring them in the same availability groups so they failover and recover as a set. Consider the following scenario:  
> 
>  -   You restore or failover to a copy of the content database that has not received the same recent updates that the report server database has received.  
> -   The [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)] synchronization process will detect differences between the list of items in the content database and the report server databases.  
> -   The synchronization process will delete or update items in the content database.  
  
###  <a name="bkmk_prepare_databases"></a> Prepare Report Server Databases for Availability Groups  
 The following are the basic steps of preparing and adding the report server databases to an [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)]:  
  
-   Create your Availability Group and configure a *Listener DNS name*.  
  
-   **Primary Replica:** Configure the report server databases to be part of a single availability group and create a primary replica that includes all of the report server databases.  
  
-   **Secondary Replicas:** Create one or more secondary replicas. The common approach to copying the databases from the primary replica to the secondary replica(s) is to restore the databases to each secondary replica using 'RESTORE WITH NORECOVERY'. For more information on creating secondary replicas and verifying data synchronization is working, see [Start Data Movement on an Always On Secondary Database &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/start-data-movement-on-an-always-on-secondary-database-sql-server.md).  
  
-   **Report Server Credentials:** You need to create the appropriate report server credentials on the secondary replicas that you created on the primary. The exact steps depend on what type of authentication you are using in your [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)] environment; Window [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)] service account, Windows user account, or SQL Server authentication. For more information, see [Configure a Report Server Database Connection  &#40;SSRS Configuration Manager&#41;](../../../reporting-services/install-windows/configure-a-report-server-database-connection-ssrs-configuration-manager.md)  
  
-   Update the database connection to use the Listener DNS Name. for natve mode report servers, change the **Report Server Database Name** in [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)] configuration manager. For SharePoint mode, change the **Database server name** for the [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)] service application(s).  
  
###  <a name="bkmk_steps_to_complete_failover"></a> Steps to complete disaster recovery of Report Server Databases  
 The following steps need to be completed after a [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)] failover to a secondary replica:  
  
1.  Stop the instance of the SQL Agent service that was being used by the primary database engine hosting the [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)] databases.  
  
2.  Start SQL Agent service on the computer that is the new primary replica.  
  
3.  Stop the Report Server service.  
  
     If the report server is in native mode, stop the report server Windows server using [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)] configuration manager.  
  
     If the report server is configured for SharePoint mode, stop the [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)] shared service in SharePoint Central Administration.  
  
4.  Start the report server service or [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)] SharePoint service.  
  
5.  Verify that reports can run against the new primary replica.  
  
###  <a name="bkmk_failover_behavior"></a> Report Server Behavior When a Failover Occurs  
 When report server databases failover and you have updated the report server environment to use the new primary replica, there are some operational issues that result from the failover and recovery process. The impact of these issues will vary depending on the [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)] load at the time of failover as well as the length of time it takes for [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)] to failover to a secondary replica and for the report server administrator to update the reporting environment to use the new primary replica.  
  
-   The execution of background processing may occur more than once due to retry logic and the inability of the report server to mark scheduled work as completed during the failover period.  
  
-   The execution of background processing that would have normally been triggered to run during the period of the failover will not occur because SQL Server Agent will not be able to write data into the report server database and this data will not be synchronized to the new primary replica.  
  
-   After the database failover completes and after the report server service is re-started, SQL Server Agent jobs will be re-created automatically. Until the SQL agent jobs are recreated, any background executions associated with SQL Server Agent jobs will not be processed. This includes [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)] subscriptions, schedules, an snapshots.  
  
## See Also  
 [SQL Server Native Client Support for High Availability, Disaster Recovery](../../../relational-databases/native-client/features/sql-server-native-client-support-for-high-availability-disaster-recovery.md)   
 [Always On Availability Groups &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/always-on-availability-groups-sql-server.md)   
 [Getting Started with Always On Availability Groups &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/getting-started-with-always-on-availability-groups-sql-server.md)   
 [Using Connection String Keywords with SQL Server Native Client](../../../relational-databases/native-client/applications/using-connection-string-keywords-with-sql-server-native-client.md)   
 [SQL Server Native Client Support for High Availability, Disaster Recovery](../../../relational-databases/native-client/features/sql-server-native-client-support-for-high-availability-disaster-recovery.md)   
 [About Client Connection Access to Availability Replicas &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/about-client-connection-access-to-availability-replicas-sql-server.md)  
  
  

