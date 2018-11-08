---
title: "Utility Administration (SQL Server Utility) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology:
ms.topic: conceptual
ms.assetid: 3e5a00c3-8905-40f0-9ddc-d924df9c2f0d
author: mashamsft
ms.author: mathoma
manager: craigg
---
# Utility Administration (SQL Server Utility)
  Use the Utility Administration tabs to manage policy, security, and data warehouse settings for a [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Utility. For more information about [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Utility concepts, see [SQL Server Utility Features and Tasks](../relational-databases/manage/sql-server-utility-features-and-tasks.md).  
  
## UIElement List  
 Policy tab - Use the policy tab to view or specify global monitoring policies.  
  
 Set global data-tier application monitoring policies. To expand the list of values for this option, click on the arrow next to the policy name, or click on the policy title.  
 When is an application running out of processor capacity? To change this policy, use the control to the right of the policy description, then click **Apply**. You can also restore default values or discard changes using buttons at the bottom of the display.  
  
-   The default maximum value for processor utilization is 70%.  
  
-   The default minimum value for processor utilization is 0%.  
  
 When is an application running out of file space? To change the policy for data file or log file space utilization, use the control to the right of the policy description, then click **Apply**. You can also restore default values or discard changes using buttons at the bottom of the display.  
  
-   The default maximum value for file space utilization is 70%.  
  
-   The default minimum value for file space utilization is 0%.  
  
 Set global SQL Server managed instance application monitoring policies. To expand the list of values for this option, click on the arrow next to the policy name, or click on the policy title.  
 When is a managed instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] running out of processor capacity? To change this policy, use the control to the right of the policy description, then click **Apply**. You can also restore default values or discard changes using buttons at the bottom of the display.  
  
-   The default maximum value for instance processor utilization is 70%.  
  
-   The default minimum value for instance processor utilization is 0%.  
  
 When is a managed instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] computer running out of processor capacity? To change this policy, use the control to the right of the policy description, then click **Apply**. You can also restore default values or discard changes using buttons at the bottom of the display.  
  
-   The default maximum value for computer processor utilization is 70%.  
  
-   The default minimum value for computer processor utilization is 0%.  
  
 When is a managed instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] running out of file space? To change the policy for data file or log file space utilization , use the control to the right of the policy description, then click **Apply**. You can also restore default values or discard changes using buttons at the bottom of the display.  
  
-   The default maximum value for file space utilization is 70%.  
  
-   The default minimum value for file space utilization is 0%.  
  
 When is a managed instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] computer running out of storage volume space? To change this policy, use the control to the right of the policy description, then click **Apply**. You can also restore default values or discard changes using buttons at the bottom of the display.  
  
-   The default maximum value for computer volume space utilization is 70%.  
  
-   The default minimum value for computer volume space utilization is 0%.  
  
 Reducing policy violation noise from highly volatile resources. To expand the controls for this feature, click on the down-arrow on the right side of the display.  
 For more information, see [Reduce Noise in CPU Utilization Policies &#40;SQL Server Utility&#41;](../relational-databases/manage/reduce-noise-in-cpu-utilization-policies-sql-server-utility.md)  
  
## UIElement List  
 Security tab - Displays login names with permissions to administer or read from the UCP.  
  
 Select the logins from the UCP that will be added to the Utility Reader role.  
 The Utility Reader privilege allows the user account to:  
  
-   Connect to the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Utility.  
  
-   See all viewpoints in the Utility Explorer in SSMS.  
  
-   See settings on the Utility Administration node in Utility Explorer in SSMS.  
  
 Utility administrators can enroll instances of SQL Server into and remove instances of SQL Server from a SQL Server Utility, as well as modify policies on managed instances and modify administration settings on the UCP.  
  
 To be a Utility administrator, you must have sysadmin privileges on the instance of SQL Server. To add or change user accounts for the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] UCP, use Object Explorer in SSMS to add the user to the server logins of the UCP instance of SQL Server. For more information, see [sp_addlogin &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-addlogin-transact-sql).  
  
## UIElement List  
 Data Warehouse tab - Displays configuration details for the utility management data warehouse.  
  
 Data Retention  
 Specify the data retention period for utilization information collected for managed instances of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]. The default time period is 1 year. The minimum value is 1 month. The longest supported value is 2 years.  
  
 Utility Data Warehouse Configuration Information  
 The following configuration settings are not configurable in this release of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]:  
  
-   UMDW name: Sysutility_mdw_\<GUID>_DATA.  
  
-   Collection set upload frequency: Every 15 minutes.  
  
 The UMDW directory is configurable: \<System drive>:\Program Files\Microsoft SQL Server\MSSQL10_50.<UCP_Name>\MSSQL\Data\\, where \<System drive> is normally the C:\ drive. The log file, UMDW_\<GUID>_LOG, is located in the same directory.  
  
> [!NOTE]  
>  The UMDW (sysutility_mdw) file location can be changed using detach/attach or ALTER DATABASE. We recommend the use of ALTER DATABASE. For more information, see [ALTER DATABASE &#40;Transact-SQL&#41;](/sql/t-sql/statements/alter-database-transact-sql).  
  
 Go back to out-of-the-box defaults  
 To reset settings on this tab to default values, click the **Restore Defaults** button, then click **Apply**.  
  
## See Also  
 [Utility Dashboard &#40;SQL Server Utility&#41;](../../2014/database-engine/utility-dashboard-sql-server-utility.md)   
 [Deployed Data-tier Application Details &#40;SQL Server Utility&#41;](../../2014/database-engine/deployed-data-tier-application-details-sql-server-utility.md)   
 [Managed Instance Details &#40;SQL Server Utility&#41;](../../2014/database-engine/managed-instance-details-sql-server-utility.md)   
 [Monitor Instances of SQL Server in the SQL Server Utility](../relational-databases/manage/monitor-instances-of-sql-server-in-the-sql-server-utility.md)  
  
  
