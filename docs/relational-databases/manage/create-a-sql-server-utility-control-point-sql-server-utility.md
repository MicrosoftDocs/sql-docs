---
title: "Create a SQL Server Utility Control Point (SQL Server Utility) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: configuration
ms.topic: conceptual
f1_keywords: 
  - "sql13.SWB.create.ucp.validation.F1"
  - "sql13.SWB.create.ucp.Summary.F1"
  - "sql13.SWB.create.ucp.progress.F1"
  - "sql13.SWB.create.ucp.agentconfiguration.F1"
  - "sql13.SWB.create.ucp.welcome.F1"
  - "sql13.SWB.create.ucp.instancename.F1"
helpviewer_keywords: 
  - "Create UCP"
  - "UCP"
ms.assetid: d5335124-1625-47ce-b4ac-36078967158c
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# Create a SQL Server Utility Control Point (SQL Server Utility)
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  An enterprise can have multiple [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Utilities, and each [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Utility can manage many instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and data-tier applications. Every [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Utility has one and only one utility control point (UCP). You must create a new UCP for each [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Utility. Each managed instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and every data-tier application is a member of one and only one [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Utility, and is managed by a single UCP.  
  
 The UCP collects configuration and performance information from managed instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] every 15 minutes. This information is stored in the utility management data warehouse (UMDW) on the UCP; the UMDW file name is sysutility_mdw. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] performance data is compared to policies to help identify resource use bottlenecks and consolidation opportunities.  
  
## Before You Begin  
 Before you create a UCP, review the following requirements and recommendations.  
  
 In this release, the UCP and all managed instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] must satisfy the following requirements:  
  
-   [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] must be version 10.50 or higher.  
  
-   The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance type must be [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
-   The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Utility must operate within a single Windows domain, or across domains with two-way trust relationships.  
  
-   The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] service accounts on the UCP and all managed instances of SQL Server must have read permission to Users in Active Directory.  
  
 In this release, the UCP must satisfy the following requirements:  
  
-   The instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] must be a supported edition. For a list of features that are supported by the editions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], see [Features Supported by the Editions of SQL Server 2016](~/sql-server/editions-and-supported-features-for-sql-server-2016.md).  
  
-   We recommend that the UCP is hosted by a case-sensitive instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
 Consider the following recommendations for capacity planning on the UCP computer:  
  
-   In a typical scenario, disk space used by the UMDW database (sysutility_mdw) on the UCP is approximately 2 GB per managed instance of SQL Server per year. This estimate can vary depending on the number of database and system objects collected by the managed instance. The UMDW (sysutility_mdw) disk space growth rate is highest during the first two days.  
  
-   In a typical scenario, disk space used by msdb on the UCP is approximately 20 MB per managed instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Note that this estimate can vary depending on the resource utilization policies and the number of database and system objects collected by the managed instance. In general, disk space usage increases as the number of policy violations increases and the duration of the moving time window for volatile resources increases.  
  
-   Note that removing a managed instance from the UCP will not reduce the disk space used by UCP databases until expiration of data retention periods for the managed instance.  
  
 In this release, all managed instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] must satisfy the following requirements:  
  
-   We recommend that if the UCP is hosted by a case-insensitive instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], then managed instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] should also be case-insensitive.  
  
-   FILESTREAM data are not supported for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Utility monitoring.  
  
 For more information, see [Maximum Capacity Specifications for SQL Server](../../sql-server/maximum-capacity-specifications-for-sql-server.md) and [Features Supported by the Editions of SQL Server 2016](~/sql-server/editions-and-supported-features-for-sql-server-2016.md).  
  
### Remove Previous Utility Control Points Before Installing a New One  
 If you are installing a utility control point (UCP) on an instance of SQL Server that was ever configured as a UCP, you must remove all managed instances of SQL Server and remove the UCP before doing so. You do this by running the **sp_sysutility_ucp_remove** stored procedure.  
  
 Before you run the procedure, note the following requirements:  
  
-   This procedure must be run on a computer that is a UCP.  
  
-   This procedure must be run by a user with sysadmin permissions, the same permissions required to create a UCP.  
  
-   All managed instances of SQL Server must be removed from the UCP. Note that the UCP is a managed instance of SQL Server. For more information, see [How to: Remove an Instance of SQL Server from the SQL Server Utility](https://go.microsoft.com/fwlink/?LinkId=169392).  
  
 Use this procedure to remove a SQL Server UCP from the SQL Server Utility. After the operation is complete, a UCP can be created on the instance of SQL Server again.  
  
 Use SQL Server Management Studio to connect to the UCP, then run this script:  
  
```  
EXEC msdb.dbo.sp_sysutility_ucp_remove;  
```  
  
> [!NOTE]  
>  If the instance of SQL Server where the UCP was removed has a non-Utility data collection set, the sysutility_mdw database is not dropped by the procedure. If this is the case, the sysutility_mdw database must be dropped manually before the UCP can be created again.  
  
 Each managed instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and every data-tier application is a member of one and only one [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Utility, and is managed by a single UCP. For more information about [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Utility concepts, see [SQL Server Utility Features and Tasks](../../relational-databases/manage/sql-server-utility-features-and-tasks.md).  
  
 A UCP is the central reasoning point of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Utility. Using the UCP, you can view configuration and performance information collected from managed instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data-tier applications, and perform general capacity planning activities. The UCP is the launch point for enrolling and removing instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] from the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Utility.  
  
 After enrolling instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] in the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Utility, you can monitor resource health for managed instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and data-tier applications to identify consolidation opportunities and isolate resource bottlenecks. For more information, see [Monitor Instances of SQL Server in the SQL Server Utility](../../relational-databases/manage/monitor-instances-of-sql-server-in-the-sql-server-utility.md).  
  
> [!IMPORTANT]  
>  The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Utility collection set is supported side-by-side with non- [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Utility collection sets. That is, a managed instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] can be monitored by other collection sets while it is a member of a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Utility. Note, however, that all collection sets on the managed instance will upload their data to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Utility management data warehouse. For more information, see [Considerations for Running Utility and non-Utility Collection Sets on the Same Instance of SQL Server](../../relational-databases/manage/run-utility-and-non-utility-collection-sets-on-same-sql-instance.md) and [Configure Your Utility Control Point Data Warehouse &#40;SQL Server Utility&#41;](../../relational-databases/manage/configure-your-utility-control-point-data-warehouse-sql-server-utility.md).  
  
## Wizard Steps  
 ![](../../relational-databases/manage/media/create-ucp.gif "Create_UCP")  
  
 The following sections provide information about each page in the wizard work flow to create a new [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] UCP. To launch the wizard to create a new UCP, open the Utility Explorer pane from the View menu in SSMS, then click on the ![](../../relational-databases/manage/media/create-ucp.gif "Create_UCP") **Create UCP** button at the top of the Utility Explorer pane.  
  
 Click on a link in the list below to navigate to details for a page in the Wizard.  
  
 For more information about a PowerShell script of this operation, see the [example](#PowerShell_create_UCP).  
  
-   [Introduction to Create UCP Wizard](#Welcome)  
  
-   [Specify Instance](#Instance_name)  
  
-   [Connection Dialog](#Connection_dialog)  
  
-   [Utility Collection Set Account](#Agent_configuration)  
  
-   [Validation Rules](#Validation_rules)  
  
-   [Summary](#Summary)  
  
-   [Creating the Utility Control Point](#Creating_UCP)  
  
##  <a name="Welcome"></a> Introduction to Create UCP Wizard  
 If you open Utility Explorer and there is no connected utility control point, you must connect to one or create a new one.  
  
 **Connect to existing UCP** - If there is already a utility control point in your deployment, you can connect to it by clicking the ![](../../relational-databases/manage/media/connect-to-utility.gif "Connect_to_Utility")**Connect to Utility** button at the top of the Utility Explorer pane. To connect to an existing UCP, you must have administrator credentials or be a member of the Utility Reader role. Note that there can only be one UCP per [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Utility and you can only be connected to one UCP from an instance of SSMS.  
  
 **Create a new UCP** - To create a new utility control point, click the ![](../../relational-databases/manage/media/create-ucp.gif "Create_UCP")**Create UCP** button at the top of the Utility Explorer pane. To create a new UCP, you must specify the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance name and provide administrator credentials in the connection dialog. Note that there can only be one UCP per [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Utility.  
  
##  <a name="Instance_name"></a> Specify Instance  
 Specify the following information about the UCP you are creating:  
  
-   **Instance Name** - To select an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] from the connection dialog, click **Connect...**. Provide the computer name and the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance name in the format ComputerName\InstanceName.  
  
-   **Utility Name** - Specify a name that will be used to identify the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Utility on the network.  
  
 To continue, click **Next**.  
  
##  <a name="Connection_dialog"></a> Connection Dialog  
 On the Connect to Server dialog box, verify the server type, computer name, and [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance name information. For more information, see [Connect to Server &#40;Database Engine&#41;](https://msdn.microsoft.com/library/ee9017b4-8a19-4360-9003-9e6484082d41).  
  
> [!NOTE]  
>  If the connection is encrypted, the encrypted connection will be used. If the connection is not encrypted, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Utility will reconnect using an encrypted connection.  
  
 To continue, click **Connect...**.  
  
##  <a name="Agent_configuration"></a> Utility Collection Set Account  
 Specify a Windows domain account to run the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Utility collection set. This account is used as the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent proxy account for the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Utility collection set. Alternatively, you can use the existing [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent service account. To pass validation requirements, use the following guidelines to specify the account.  
  
 If you specify the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent service account option:  
  
-   The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent service account must be a Windows domain account that is not a built-in account like LocalSystem, NetworkService, or LocalService.  
  
 To continue, click **Next**.  
  
##  <a name="Validation_rules"></a> Validation Rules  
 In this release of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], the following conditions must be true on the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] where the UCP will be created:  
  
|Validation rule|Corrective action|  
|---------------------|-----------------------|  
|You must have administrator privileges on the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] where the utility control point will be created.|Log on with an account that has administrator privileges on the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].|  
|The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] version must be 10.50 or higher.|Specify a different instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to host the UCP.|  
|The instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] must be a supported edition. For a list of features that are supported by the editions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], see [Features Supported by the Editions of SQL Server 2016](~/sql-server/editions-and-supported-features-for-sql-server-2016.md).|Specify a different instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to host the UCP.|  
|The instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] must not be an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] enrolled with any other [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] UCP.|Specify a different instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to host the UCP, or unenroll the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] from the UCP where it is currently a managed instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].|  
|The instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] cannot already be host to a utility control point.|Specify a different instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to host the UCP.|  
|The specified instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] should have TCP/IP enabled.|Enable TCP/IP for the specified instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].|  
|The instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] cannot have a database named "sysutility_mdw."|The create UCP operation will create a utility management data warehouse (UMDW) named "sysutility_mdw." The operation requires that the name does not exist on the computer at the time that validation rules are run. To continue, you must remove or rename any database named "sysutility_mdw." For more information about renaming operations, see [ALTER DATABASE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-database-transact-sql.md).|  
|Collection sets on the specified instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] must be stopped.|Stop pre-existing collection sets while the UCP is created on the specified instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. If the data collector is disabled, enable it, stop any running collection sets, then re-run validation rules for the Create UCP operation.<br /><br /> To enable the data collector:<br /><br /> In Object Explorer, expand the **Management** node.<br /><br /> Right-click **Data Collection**, and then click **Enable Data Collection**.<br /><br /> To stop a collection set:<br /><br /> In Object Explorer, expand the Management node, expand **Data Collection**, and then expand **System Data Collection Sets**.<br /><br /> Right-click the collection set that you want to stop, and then click **Stop Data Collection Set**.<br /><br /> A message box will display the result of this action, and a red circle on the icon for the collection set indicates that the collection set has stopped.|  
|The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent service on the specified instance must be started. If the specified instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] failover cluster instance, the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent service must be configured to start manually. Otherwise, the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent service must be configured to start automatically.|Start the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent service. If the specified instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] failover cluster instance, configure the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent service to start manually. Otherwise, configure the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent service to start automatically.|  
|WMI must be configured correctly.|To troubleshoot WMI configuration, see [Troubleshoot the SQL Server Utility](https://msdn.microsoft.com/library/f5f47c2a-38ea-40f8-9767-9bc138d14453).|  
|The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent proxy account cannot be a built-in account, like Network Service.|If the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent proxy account is a built-in account, like Network Service, re-assign the account to a Windows domain account that is sysadmin.|  
|If you select the proxy account option, the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent proxy account must be a valid Windows domain account.|Specify a valid Windows domain account. To ensure that the account is valid, logon to the specified instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] using the Windows domain account.|  
|If you select the service account option, the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent service account cannot be a built-in account, like Network Service.|If the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent service account is a built-in account, like Network Service, re-assign the account to a Windows domain account.|  
|If you select the service account option, the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent service account must be a valid Windows domain account.|Specify a valid Windows domain account. To ensure that the account is valid, logon to the specified instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] using the Windows domain account.|  
  
 If there are failed conditions in the validation results, correct the blocking issues and then click **Rerun Validation** to verify the computer configuration.  
  
 To save the validation report, click **Save Report** then specify a location for the file.  
  
 To continue, click **Next**.  
  
##  <a name="Summary"></a> Summary  
 The summary page displays the information that you provided about the UCP:  
  
-   The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance name that hosts the UCP.  
  
-   The name of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Utility.  
  
-   The name of the account that will be used to run jobs for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Utility data collection.  
  
 To change UCP configuration settings, click **Previous**. To continue, click **Next**.  
  
##  <a name="Creating_UCP"></a> Creating the Utility Control Point  
 During the operation to create the UCP, the wizard will display the steps and provide status:  
  
-   Preparing the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance for UCP creation.  
  
-   Creating the utility management data warehouse (UMDW).  
  
-   Initializing the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] UMDW; the UMDW file name is sysutility_mdw.  
  
-   Configuring the UCP.  
  
-   Configuring the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Utility collection set.  
  
 To save a report about the create UCP operation, click **Save Report** then specify a location for the file.  
  
 To complete the wizard, click **Finish**.  
  
 After completing the Create UCP Wizard, the Utility Explorer navigation pane in SSMS displays a node for the UCP with nodes under it for Deployed Data-tier Applications, Managed Instances, and Utility Administration. The UCP automatically becomes a managed instance.  
  
 The data collection process begins immediately, but it can take up to 30 minutes for data to first appear in the dashboard and viewpoints in the Utility Explorer content pane. Data collection continues once every 15 minutes. Initial data will be from the UCP itself. That is, the UCP is the first managed instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] in the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Utility.  
  
 To display the dashboard, click **View** then select **Utility Explorer Content** from the SSMS menu. To refresh data, right-click the utility name in the Utility Explorer pane, then select **Refresh**.  
  
 For more information about how to enroll additional instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] into the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Utility, see [Enroll an Instance of SQL Server &#40;SQL Server Utility&#41;](../../relational-databases/manage/enroll-an-instance-of-sql-server-sql-server-utility.md). To remove the UCP as a managed instance from the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Utility, select **Managed Instances** in the **Utility Explorer** pane to populate the list view of managed instances, right-click on the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance name in the **Utility Explorer Content** list view, then select **Make Instance Unmanaged**.  
  
##  <a name="PowerShell_create_UCP"></a> Create a New Utility Control Point Using PowerShell  
 Use the following example to create a new utility control point:  
  
```  
> $UtilityInstance = new-object -Type Microsoft.SqlServer.Management.Smo.Server "ComputerName\UCP-Name";  
> $SqlStoreConnection = new-object -Type Microsoft.SqlServer.Management.Sdk.Sfc.SqlStoreConnection $UtilityInstance.ConnectionContext.SqlConnectionObject;  
> $Utility = [Microsoft.SqlServer.Management.Utility.Utility]::CreateUtility("Utility", $SqlStoreConnection, "ProxyAccount", "ProxyAccountPassword");  
```  
  
## See Also  
 [SQL Server Utility Features and Tasks](../../relational-databases/manage/sql-server-utility-features-and-tasks.md)   
 [Troubleshoot the SQL Server Utility](https://msdn.microsoft.com/library/f5f47c2a-38ea-40f8-9767-9bc138d14453)  
  
  
