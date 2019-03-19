---
title: "Troubleshoot the SQL Server Utility | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology:
ms.topic: conceptual
ms.assetid: f5f47c2a-38ea-40f8-9767-9bc138d14453
author: mashamsft
ms.author: mathoma
manager: craigg
---
# Troubleshoot the SQL Server Utility
  Troubleshooting [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Utility issues might include resolving a failed operation to enroll an instance of SQL Server with a UCP, troubleshooting failed data collection resulting in gray icons in the managed instance list view on a UCP, mitigating performance bottlenecks, or resolving resource health issues. For more information about mitigating resource health issues identified by a [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] UCP, see [Troubleshoot SQL Server Resource Health &#40;SQL Server Utility&#41;](../relational-databases/manage/troubleshoot-sql-server-resource-health-sql-server-utility.md).  
  
## Failed Operation to Enroll an Instance of SQL Server into a SQL Server Utility  
 If you connect to the instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] to enroll using [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Authentication, and you specify a proxy account that belongs to a different Active Directory domain than the domain where the UCP is located, instance validation succeeds, but the enrollment operation fails with the following error message:  
  
 An exception occurred while executing a Transact-SQL statement or batch. (Microsoft.SqlServer.ConnectionInfo)  
  
 Additional information:  Could not obtain information about Windows NT group/user '\<DomainName\AccountName>', error code 0x5. (Microsoft SQL Server, Error: 15404)  
  
 This issue occurs in the following example scenario:  
  
1.  The UCP is a member of "Domain_1."  
  
2.  A one-way domain trust relationship is in place: that is, "Domain_1" is not trusted by "Domain_2" but "Domain_2" is trusted by "Domain_1."  
  
3.  The instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] to enroll into the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Utility is also a member of "Domain_1."  
  
4.  During the enroll operation, connect to the instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] to enroll using "sa". Specify a proxy account from "Domain_2."  
  
5.  Validation succeeds but enrollment fails.  
  
 The workaround for this issue, using the example above, is to connect to the instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] to enroll into the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Utility using "sa" and provide a proxy account from "Domain_1."  
  
## Failed WMI Validation  
 If WMI is not properly configured on an instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)], the Create UCP and Enroll Managed Instance operations display a warning, but the operation is not blocked. Additionally, if you change the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Agent account configuration so that [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Agent does not have permission to required WMI classes, data collection on the affected managed instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] fails to upload to the UCP. This results in gray icons in the UCP.  
  
 Failed data collection results in gray status icons in the UCP list view for affected managed instances of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]. The job history on the managed instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] shows that sysutility_mi_collect_and_upload fails on step 2 (Stage Data Collected from PowerShell Script).  
  
 The simplified error messages are:  
  
 Command execution stopped because the shell variable "ErrorActionPreference" is set to Stop: Access denied.  
  
 ERROR: \<Date-time (MM/DD/YYYY HH:MM:SS)>: Caught exception while collecting cpu properties.  A WMI query might have failed.  WARNING.  
  
 To resolve this issue, verify the following configuration settings:  
  
-   On Windows Server 2003, the SQL Server Agent service must be part of the Windows Performance Monitoring group on the managed instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)].  
  
-   The WMI service must be enabled and configured on the managed instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)].  
  
-   The WMI repository might be corrupt on the managed instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)].  
  
-   The performance library might be missing or corrupt on the managed instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)].  
  
 To verify that the specified instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] is configured properly to report data to the UCP, verify that the following classes are available on the specified instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)], and that they are accessible to SQL Server Agent service account:  
  
-   Win32_MountPoint  
  
-   Win32_PerfRawData_PerfProc_Process  
  
-   Win32_PerfRawData_PerfOS_Processor  
  
-   Win32_Processor  
  
-   Win32_Volume  
  
-   Win32_LogicalDisk  
  
 You can use the Get-WmiObject PowerShell cmdlet on each of the classes to verify that each class is accessible. Run the following cmdlets on the managed instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]:  
  
```  
Get-WmiObject Win32_MountPoint -ErrorAction Stop | Out-Null  
Get-WmiObject Win32_PerfRawData_PerfProc_Process -ErrorAction Stop| Out-Null  
Get-WmiObject Win32_PerfRawData_PerfOS_Processor -ErrorAction Stop | Out-Null  
Get-WmiObject Win32_Processor -ErrorAction Stop | Out-Null  
Get-WmiObject Win32_Volume -ErrorAction Stop | Out-Null  
Get-WmiObject Win32_LogicalDisk -ErrorAction Stop | Out-Null  
```  
  
 For more information about troubleshooting WMI, see [Troubleshooting WMI](https://go.microsoft.com/fwlink/?LinkId=178250). Note that queries in these SQL Server Utility operations are running locally, so the DCOM and remote troubleshooting content does not apply.  
  
## Failed Data Collection  
 If [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Utility data collection events fail, consider the following possibilities:  
  
-   Do not change any properties of the "Utility Information" collection set on a managed instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)], and do not turn data collection on/off manually, as data collection is controlled by a Utility agent job.  
  
-   Failed or unsupported WMI validation. For more information, see the Failed WMI Validation section earlier in this topic.  
  
-   Refresh data in the managed instance list view, as data in [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Utility viewpoints does not refresh automatically. To refresh data, right-click the **Managed Instances** node in the **Utility Explorer Navigation** pane, then select **Refresh**, or right-click on the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] instance name in the list view, then select **Refresh**. Note that after an instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] has been enrolled with a UCP, it can take up to 30 minutes for data to first appear in the dashboard and viewpoints in the Utility Explorer content pane.  
  
-   Use SQL Server Configuration Manager to verify that the instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] is running.  
  
-   If data collection or data upload fail due to timeout issues, update the function dbo.fn_sysutility_mi_get_collect_script() in the MSDB database. Specifically, in the function "Invoke-BulkCopyCommand()" add line:  
  
    ```  
    $bulkCopy.BulkCopyTimeout=180  
    ```  
  
     The default timeout value is 30 seconds.  
  
-   If the instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] is not clustered, verify that the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Agent service is running and that the service is set to start automatically on the UCP and on the managed instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)].  
  
-   Verify that a valid account is being used to run data collection on the managed instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]. For example, the password may have expired. If the proxy password has expired, update password credentials in SSMS, as follows:  
  
    1.  In SSMS **Object Explorer**, expand the **Security** node, then expand the **Credentials** node.  
  
    2.  Right-click on **UtilityAgentProxyCredential_\<GUID>** and select **Properties**.  
  
    3.  On the Credential Properties dialog, update credentials as necessary for the **UtilityAgentProxyCredential_\<GUID>** credential.  
  
    4.  Click **OK** to confirm the change.  
  
-   TCP/IP should be enabled on the UCP and on the managed instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]. Enable TCP/IP through [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Configuration Manager.  
  
-   The SQL Server Browser service on the UCP should be started and configured to start automatically. If your organization prevents use of the SQL Server Browser service, use the following steps to allow a managed instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] to connect to the UCP:  
  
    1.  On the Windows taskbar on the managed instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)], click **Start**, then click **Run...**.  
  
    2.  Type "cliconfg.exe" in the space provided, then click **OK**.  
  
    3.  If prompted to allow "SQL Client Configuration Utility EXE" to start, click "**Continue**."  
  
    4.  On the **SQL Server Client Network Utility** dialog box, select the **Alias** tab, then click **Add...**.  
  
    5.  On the **Add Network Library Configuration** dialog box:  
  
    6.  Specify TCP/IP from the list of network libraries.  
  
    7.  Specify the ComputerName\InstanceName of the UCP in the **Server Alias** text box.  
  
    8.  Specify the ComputerName of the UCP in the **Server Name** text box.  
  
    9. Uncheck the **Dynamically determine port** checkbox.  
  
    10. Specify the port number that the UCP is listening on in the **Port number** text box.  
  
    11. Click **OK** to save your changes.  
  
    12. Repeat these steps for each managed instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] that connects to a UCP where the SQL Server Browser service is not enabled.  
  
-   Ensure that managed instances of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] are connected to the network.  
  
-   If there are databases with the same name but different case-sensitivity settings on a managed instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)], the identification between the database and its viewpoints can be incorrect, resulting in failed data collection. For example, a database named "MYDATABASE" might show health states for a database named "MyDatabase". No error is generated in this scenario. Failed data collection can also result from case-sensitivity mismatches in other objects displayed in the UCP, like database file and file group names.  
  
-   If a managed instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] is hosted on a Windows Server 2003 computer, then the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Agent service account must belong to the Performance Monitor Users security group or the local Administrators group. Otherwise, data collection will fail with an access denied error. To add a [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Agent service account to the Performance Monitor Users security group, use the following steps:  
  
    1.  Open **Computer Management**, then **Local Users and Groups**, then **Groups**.  
  
    2.  Right-click **Performance Monitor Users** and select **Add to Group**.  
  
    3.  Click **Add**.  
  
    4.  Enter the account that the SQL Server Agent service is running under, then click **OK**.  
  
    5.  If the instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] was already enrolled with the UCP before adding the user to this group, restart the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Agent service.  
  
## See Also  
 [SQL Server Utility Features and Tasks](../relational-databases/manage/sql-server-utility-features-and-tasks.md)   
 [Troubleshoot SQL Server Resource Health &#40;SQL Server Utility&#41;](../relational-databases/manage/troubleshoot-sql-server-resource-health-sql-server-utility.md)  
  
  
