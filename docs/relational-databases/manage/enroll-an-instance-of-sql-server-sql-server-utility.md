---
title: "Enroll an Instance of SQL Server (SQL Server Utility) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: configuration
ms.topic: conceptual
f1_keywords: 
  - "sql13.SWB.makemanaged.agentaccount.F1"
  - "sql13.SWB.makemanaged.welcome.F1"
  - "sql13.SWB.makemanaged.enrolling.F1"
  - "sql13.SWB.makemanaged.instancename.F1"
  - "sql13.SWB.makemanaged.Summary.F1"
  - "sql13.SWB.makemanaged.progress.F1"
  - "sql13.SWB.makemanaged.validation.F1"
helpviewer_keywords: 
  - "Enroll instance"
ms.assetid: a801c619-611b-4e82-a8d8-d1e01691b7a1
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# Enroll an Instance of SQL Server (SQL Server Utility)
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  Enroll an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] into an existing [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Utility to monitor its performance and configuration as a managed instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. The utility control point (UCP) collects configuration and performance information from managed instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] every 15 minutes. This information is stored in the utility management data warehouse (UMDW) on the UCP; the UMDW file name is sysutility_mdw. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] performance data is compared to policies to help identify resource use bottlenecks and consolidation opportunities.  
  
 In this release, the UCP and all managed instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] must satisfy the following requirements:  
  
-   [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] must be version 10.50 or higher.  
  
-   The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance type must be [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
-   The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Utility must operate within a single Windows domain, or domains with two-way trust relationships.  
  
-   The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] service accounts on the UCP and all managed instances of SQL Server must have read permission to Users in Active Directory.  
  
-   The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance to enroll cannot be SQL Azure.  
  
 In this release, the UCP must satisfy the following requirements:  
  
-   The instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] must be a supported edition. For a list of features that are supported by the editions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], see [Features Supported by the Editions of SQL Server 2016](~/sql-server/editions-and-supported-features-for-sql-server-2016.md).  
  
-   We recommend that the UCP is hosted by a case-sensitive instance of SQL Server.  
  
-   Consider the following recommendations for capacity planning on the UCP computer:  
  
    -   In a typical scenario, disk space used by the UMDW database (sysutility_mdw) on the UCP is approximately 2 GB per managed instance of SQL Server per year. This estimate can vary depending on the number of database and system objects collected by the managed instance. The UMDW (sysutility_mdw) disk space growth rate is highest during the first two days.  
  
    -   In a typical scenario, disk space used by msdb on the UCP is approximately 20 MB per managed instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Note that this estimate can vary depending on the resource utilization policies and the number of database and system objects collected by the managed instance. In general, disk space usage increases as the number of policy violations increases and the duration of the moving time window for volatile resources increases.  
  
    -   Note that removing a managed instance from the UCP will not reduce the disk space used by UCP databases until expiration of data retention periods for the managed instance.  
  
 In this release, all managed instances of SQL Server must satisfy the following requirements:  
  
-   We recommend that if the UCP is hosted by a case-insensitive instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], then managed instances of SQL Server should also be case-insensitive.  
  
-   FILESTREAM data are not supported for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Utility monitoring.  
  
 For more information, see [Maximum Capacity Specifications for SQL Server](../../sql-server/maximum-capacity-specifications-for-sql-server.md) and [Features Supported by the Editions of SQL Server 2016](~/sql-server/editions-and-supported-features-for-sql-server-2016.md).  
  
 For more information about [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Utility concepts, see [SQL Server Utility Features and Tasks](../../relational-databases/manage/sql-server-utility-features-and-tasks.md).  
  
> [!IMPORTANT]  
>  The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Utility collection set is supported side-by-side with non- [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Utility collection sets. That is, a managed instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] can be monitored by other collection sets while it is a member of a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Utility. Note, however, that all collection sets on the managed instance upload their data to the utility management data warehouse. For more information, see [Considerations for Running Utility and non-Utility Collection Sets on the Same Instance of SQL Server](../../relational-databases/manage/run-utility-and-non-utility-collection-sets-on-same-sql-instance.md) and [Configure Your Utility Control Point Data Warehouse &#40;SQL Server Utility&#41;](../../relational-databases/manage/configure-your-utility-control-point-data-warehouse-sql-server-utility.md).  
  
## Wizard Steps  
 The following sections provide detailed information about each page in the Wizard work flow. Click on a link to navigate to details for a page in the Wizard. For more information about a PowerShell script of this operation, see the PowerShell [example](#PowerShell_enroll).  
  
-   [Introduction to Enroll Instance Wizard](#Welcome)  
  
-   [Specify the Instance of SQL Server](#Instance_name)  
  
-   [Connection Dialog](#Connection_dialog)  
  
-   [Utility Collection Set Account](#Proxy_configuration)  
  
-   [SQL Server Instance Validation](#Validation_rules)  
  
-   [Summary of Instance Enrollment](#Summary)  
  
-   [Enrolling the Instance of SQL Server](#Enrolling)  
  
##  <a name="Welcome"></a> Introduction to Enroll Instance Wizard  
 To launch the Wizard, expand the Utility Explorer tree on a utility control point, right-click on **Managed Instances**, and select **Add Managed Instance...**.  
  
 To continue, click **Next**.  
  
##  <a name="Instance_name"></a> Specify the Instance of SQL Server  
 To select an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] from the connection dialog box, click **Connect...**. Provide the computer name and the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance name in the format ComputerName\InstanceName. For more information, see [Connect to Server &#40;Database Engine&#41;](https://msdn.microsoft.com/library/ee9017b4-8a19-4360-9003-9e6484082d41).  
  
 To continue, click **Next**.  
  
##  <a name="Connection_dialog"></a> Connection Dialog  
 On the Connect to Server dialog box, verify the server type, computer name, and [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance name information. For more information, see [Connect to Server &#40;Database Engine&#41;](https://msdn.microsoft.com/library/ee9017b4-8a19-4360-9003-9e6484082d41).  
  
> [!NOTE]  
>  If the connection is encrypted, the encrypted connection is used. If the connection is not encrypted, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Utility reconnects using an encrypted connection.  
  
 To continue, click **Connect...**.  
  
##  <a name="Proxy_configuration"></a> Utility Collection Set Account  
 Specify a Windows domain account to run the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Utility collection set. This account is used as the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent proxy account for the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Utility collection set. Alternatively, you can use the existing [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent service account. To pass validation requirements, use the following guidelines to specify the account.  
  
 If you specify the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent service account option:  
  
-   The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent service account must be a Windows domain account that is not a built-in account like LocalSystem, NetworkService, or LocalService.  
  
 To continue, click **Next**.  
  
##  <a name="Validation_rules"></a> SQL Server Instance Validation  
 In this release, the following conditions must be true on the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to be enrolled into the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Utility:  
  
|Condition|Corrective Action|  
|---------------|-----------------------|  
|You must have administrator privileges on the specified instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and on the UCP.|Log on with an account that has administrator privileges on the specified instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and on the UCP.|  
|The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] edition must support instance enrollment.|For a list of features that are supported by the editions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], see [Features Supported by the Editions of SQL Server 2016](~/sql-server/editions-and-supported-features-for-sql-server-2016.md).|  
|The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] UCP should have TCP/IP enabled.|Enable TCP/IP on the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] UCP.|  
|The instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] cannot already be enrolled with any other [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] UCP.|If the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] you specify is already managed as part of an existing [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Utility, you cannot enroll it with a different UCP.|  
|The instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] cannot already be a UCP.|If the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] you specify is already a UCP that is different than the UCP you are connected to, you cannot enroll it in this UCP.|  
|The instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] must have [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Utility collection sets installed.|Re-install the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].|  
|Collection sets on the specified instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] must be stopped.|Stop pre-existing collection sets on the specified instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. If the data collector is disabled, enable it, stop any running collection sets, then re-run validation rules for the Create UCP operation.<br /><br /> To enable the data collector:<br /><br /> In Object Explorer, expand the **Management** node.<br /><br /> Right-click **Data Collection**, and then click **Enable Data Collection**.<br /><br /> To stop a collection set:<br /><br /> In Object Explorer, expand the Management node, expand **Data Collection**, and then expand **System Data Collection Sets**.<br /><br /> Right-click the collection set that you want to stop, and then click **Stop Data Collection Set**.<br /><br /> A message box will display the result of this action, and a red circle on the icon for the collection set indicates that the collection set has stopped.|  
|The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent service on the specified instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] must be started.|Start the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent service on the specified instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. If the specified instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] failover cluster instance, configure the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent service to start manually. Otherwise, configure the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent service to start automatically.|  
|The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent service on the UCP must be started.|Start the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent service on the UCP. If the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] UCP is a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] failover cluster instance, configure the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent service to start manually. Otherwise, configure the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent service to start automatically.|  
|WMI must be configured correctly.|To troubleshoot WMI configuration, see [Troubleshoot the SQL Server Utility](https://msdn.microsoft.com/library/f5f47c2a-38ea-40f8-9767-9bc138d14453).|  
|The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent proxy account must be a valid Windows domain account on the UCP.|Specify a valid Windows domain account. To ensure that the account is valid, logon to the UCP using the Windows domain account.|  
|If you select the proxy account option, the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent proxy account must be a valid Windows domain account on the specified instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].|Specify a valid Windows domain account. To ensure that the account is valid, logon to the specified instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] using the Windows domain account.|  
|The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent service account cannot be a built-in account, like Network Service.|Re-assign the account to a Windows domain account. To ensure that the account is valid, logon to the specified instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] using the Windows domain account.|  
|The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent service account must be a valid Windows domain account on the UCP.|Specify a valid Windows domain account. To ensure that the account is valid, logon to the UCP using the Windows domain account.|  
|If you select the service account option, the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent service account must be a valid Windows domain account on the specified instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].|Specify a valid Windows domain account. To ensure that the account is valid, logon to the specified instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] using the Windows domain account.|  
  
 If there are failed conditions in the validation results, correct the blocking issues and then click **Rerun Validation** to verify the computer configuration.  
  
 To save the validation report, click **Save Report** then specify a location for the file.  
  
 To continue, click **Next**.  
  
##  <a name="Summary"></a> Summary of Instance Enrollment  
 The summary page lists the information about the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to add to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Utility.  
  
 Managed Instance Settings:  
  
-   SQL Server Instance Name: ComputerName\InstanceName  
  
-   Utility Collection Set Account: DomainName\UserName  
  
 To continue, click **Next**.  
  
##  <a name="Enrolling"></a> Enrolling the Instance of SQL Server  
 The Enrolling page provides status of the operation:  
  
-   Preparing the instance for enrollment.  
  
-   Creating the cache directory for the collected data.  
  
-   Configuring the utility collection set.  
  
 To save a report about the enroll operation, click **Save Report** then specify a location for the file.  
  
 To complete the Wizard, click **Finish**.  
  
> [!NOTE]  
>  If you use SQL Server Authentication to connect to the instance of SQL Server to enroll, and you specify a proxy account that belongs to a different Active Directory domain than the domain where the UCP is located, instance validation succeeds, but the enrollment operation fails with the following error message:  
>   
>  An exception occurred while executing a Transact-SQL statement or batch. (Microsoft.SqlServer.ConnectionInfo)  
>   
>  Additional information:  Could not obtain information about Windows NT group/user '\<DomainName\AccountName>', error code 0x5. (Microsoft SQL Server, Error: 15404)  
>   
>  For more information about troubleshooting this failure, see [Troubleshoot the SQL Server Utility](https://msdn.microsoft.com/library/f5f47c2a-38ea-40f8-9767-9bc138d14453).  
  
> [!IMPORTANT]  
>  Do not change any properties of the "Utility Information" collection set on a managed instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], and do not turn data collection on/off manually, as data collection is controlled by a Utility agent job.  
  
 After completing the Enroll Instance Wizard, click on the **Managed Instances** node in the **Utility Explorer Navigation** pane in SSMS. Enrolled instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] are displayed in the list view in **Utility Explorer Content** pane.  
  
 The data collection process begins immediately, but it can take up to 30 minutes for data to first appear in the dashboard and viewpoints in the Utility Explorer content pane. Data collection continues one time every 15 minutes. To refresh data, right-click the **Managed Instances** node the **Utility Explorer Navigation** pane, then select **Refresh**, or right-click on the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance name in the list view, then select **Refresh**.  
  
 To remove managed instances from the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Utility, select **Managed Instances** in the **Utility Explorer Navigation** pane to populate the list view of managed instances, right-click on the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance name in the **Utility Explorer Content** list view, then select **Make Instance Unmanaged**.  
  
##  <a name="PowerShell_enroll"></a> Enroll an Instance of SQL Server using PowerShell  
 Use the following example to enroll an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] into an existing [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Utility:  
  
```  
> $UtilityInstance = new-object -Type Microsoft.SqlServer.Management.Smo.Server "ComputerName\UCP-Name";  
> $SqlStoreConnection = new-object -Type Microsoft.SqlServer.Management.Sdk.Sfc.SqlStoreConnection $UtilityInstance.ConnectionContext.SqlConnectionObject;  
> $Utility = [Microsoft.SqlServer.Management.Utility.Utility]::Connect($SqlStoreConnection);  
> $Instance = new-object -Type Microsoft.SqlServer.Management.Smo.Server "ComputerName\ManagedInstanceName";  
> $InstanceConnection = new-object -Type Microsoft.SqlServer.Management.Sdk.Sfc.SqlStoreConnection $Instance.ConnectionContext.SqlConnectionObject;  
> $ManagedInstance = $Utility.EnrollInstance($InstanceConnection, "ProxyAccount", "ProxyPassword");  
```  
  
## See Also  
 [SQL Server Utility Features and Tasks](../../relational-databases/manage/sql-server-utility-features-and-tasks.md)   
 [Monitor Instances of SQL Server in the SQL Server Utility](../../relational-databases/manage/monitor-instances-of-sql-server-in-the-sql-server-utility.md)   
 [Troubleshoot the SQL Server Utility](https://msdn.microsoft.com/library/f5f47c2a-38ea-40f8-9767-9bc138d14453)  
  
  
