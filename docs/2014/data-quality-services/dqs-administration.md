---
title: "DQS Administration | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: data-quality-services
ms.topic: conceptual
helpviewer_keywords: 
  - "dqs administration"
  - "administration"
  - "dqs,adminstration"
ms.assetid: 9940ef5d-f6f6-4dec-9414-1077a4d7f12b
author: leolimsft
ms.author: lle
manager: craigg
---
# DQS Administration
  [!INCLUDE[ssDQSnoversion](../includes/ssdqsnoversion-md.md)] (DQS) allows you to administer and manage various DQS activities performed on [!INCLUDE[ssDQSServer](../includes/ssdqsserver-md.md)], configure server-level properties related to DQS activities, configure the Reference Data Service settings, and configure DQS log settings. These things are done through the **Administration** feature in [!INCLUDE[ssDQSClient](../includes/ssdqsclient-md.md)]. Depending upon your security access (role) in DQS, you are granted/denied access to certain functionalities in this area.  
  
 Apart from these administration activities, this topic also provides information about an administration activity, backing up and restoring DQS databases, which is not performed using [!INCLUDE[ssDQSClient](../includes/ssdqsclient-md.md)].  
  
 The administration feature in [!INCLUDE[ssDQSClient](../includes/ssdqsclient-md.md)] has the following benefits:  
  
-   Enables data stewards to monitor various DQS activities on a [!INCLUDE[ssDQSServer](../includes/ssdqsserver-md.md)] from a [!INCLUDE[ssDQSClient](../includes/ssdqsclient-md.md)].  
  
-   Enables DQS administrators to monitor the DQS activities on a [!INCLUDE[ssDQSServer](../includes/ssdqsserver-md.md)] from a [!INCLUDE[ssDQSClient](../includes/ssdqsclient-md.md)], and *terminate* a running activity or *stop* a running process within an activity, if required.  
  
-   Configure reference data service settings such as setting up connectivity with Windows Azure Marketplace and managing direct third-party reference data service providers.  
  
-   Configure threshold values for the cleansing and matching activities.  
  
-   Enable/disable notifications in [!INCLUDE[ssDQSClient](../includes/ssdqsclient-md.md)].  
  
-   Configure logging based on the severity level of the events.  
  
##  <a name="AdminUsingClent"></a> Administration Activities by Using Data Quality Client  
 These activities are performed by using the **Administration** feature in [!INCLUDE[ssDQSClient](../includes/ssdqsclient-md.md)].  
  
### Activity Monitoring  
 The **Activity Monitoring** screen in [!INCLUDE[ssDQSClient](../includes/ssdqsclient-md.md)] displays detailed information about each activity performed on a [!INCLUDE[ssDQSServer](../includes/ssdqsserver-md.md)]. This screen will be primarily used by the data steward to perform a high-level monitoring of all the activities performed on the [!INCLUDE[ssDQSServer](../includes/ssdqsserver-md.md)] that the [!INCLUDE[ssDQSClient](../includes/ssdqsclient-md.md)] application is connected to. This screen does not provide any system-level monitoring. Additionally, this screen also enables the DQS administrators to control an activity or a process within an activity by terminating a running activity or stopping a running process within an activity, if required. The data is displayed for knowledge discovery, domain management, matching policy, cleansing, matching, and SQL Server Integration Services (SSIS)-based cleansing.  
  
### Configuration  
 The **Configuration** screen in [!INCLUDE[ssDQSClient](../includes/ssdqsclient-md.md)] enables the DQS administrator to do the following things:  
  
-   **Reference Data**: Configure reference data service providers: Windows Azure Marketplace or direct reference data service providers. After you set up the reference data service providers, you can map a domain/composite domain with the reference data during domain management activity in a knowledge base, and then use the same knowledge base for the cleansing activity in a data quality project. It also enables you to specify the proxy settings for connecting to the Internet to use Windows Azure Marketplace.  
  
-   **General Settings**: Specify the threshold values for data cleansing and data matching, and whether to enable notifications for profiling in [!INCLUDE[ssDQSClient](../includes/ssdqsclient-md.md)]. These threshold values are used by DQS during the computer-assisted cleansing and matching activities in a data quality project.  
  
-   **Log Settings**: The log files in DQS record the activities performed in DQS, and are useful for tracking operational issues during maintenance and troubleshooting. You can filter the messages that you want to be logged for various DQS features (domain management, knowledge discovery, cleansing, matching, and reference data services) and DQS modules based on the severity level of the events.  
  
> [!NOTE]  
>  The **Configuration** screen is available only for those users who have the dqs_administrator role on the DQS_MAIN database.  
  
##  <a name="AdminOutsideClient"></a> Administration Activities Outside of Data Quality Client  
 There activities are performed outside of Data Quality Client:  
  
-   **Backup and Restore DQS Databases**: The backup and restore of DQS databases is same as backing up and restoring any SQL Server database with some considerations that are specific to DQS.  
  
-   **Detach and Attach DQS Databases**: The steps to detach and attach DQS databases is same as detaching and attaching any SQL Server database with some considerations that are specific to DQS.  
  
 For more information, see [Manage DQS Databases](../../2014/data-quality-services/manage-dqs-databases.md).  
  
## Related Tasks  
  
|Task Description|Topic|  
|----------------------|-----------|  
|Describes how to monitor activities in DQS.|[Monitor DQS Activities](../../2014/data-quality-services/monitor-dqs-activities.md)|  
|Describes how to configure reference data settings in DQS.|[Configure DQS to Use Reference Data](../../2014/data-quality-services/configure-dqs-to-use-reference-data.md)|  
|Describes how to configure threshold values for the cleansing and matching activities.|[Configure Threshold Values for Cleansing and Matching](../../2014/data-quality-services/configure-threshold-values-for-cleansing-and-matching.md)|  
|Describes how to enable or disable notifications in DQS.|[Enable or Disable Profiling Notifications in DQS](../../2014/data-quality-services/enable-or-disable-profiling-notifications-in-dqs.md)|  
|Describes how to configure DQS logging based on the severity level of the events.|[Configure Severity Levels for DQS Log Files](../../2014/data-quality-services/configure-severity-levels-for-dqs-log-files.md)|  
|Describes how to configure advanced settings for DQS logging.|[Configure Advanced Settings for DQS Log Files](../../2014/data-quality-services/configure-advanced-settings-for-dqs-log-files.md)|  
|Describes how to back up and restore DQS databases.|[Backing Up and Restoring DQS Databases](../../2014/data-quality-services/backing-up-and-restoring-dqs-databases.md)|  
|Describes how to detach and attach DQS databases.|[Detaching and Attaching DQS Databases](../../2014/data-quality-services/detaching-and-attaching-dqs-databases.md)|  
  
## See Also  
 [Reference Data Services in DQS](../../2014/data-quality-services/reference-data-services-in-dqs.md)   
 [Manage DQS Log Files](../../2014/data-quality-services/manage-dqs-log-files.md)   
 [Manage DQS Databases](../../2014/data-quality-services/manage-dqs-databases.md)  
  
  
