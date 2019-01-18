---
title: "Configure PowerPivot Service Accounts | Microsoft Docs"
ms.custom: ""
ms.date: "03/09/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
ms.assetid: 76a85cd0-af93-40c9-9adf-9eb0f80b30c1
author: minewiskan
ms.author: owend
manager: craigg
---
# Configure PowerPivot Service Accounts
  A [!INCLUDE[ssGeminiShort](../../includes/ssgeminishort-md.md)] installation includes two services that support server operations. The **SQL Server Analysis Services (PowerPivot)** service is a Windows service that provides PowerPivot data processing and query support on an application server. The login account for this service is always specified during SQL Server Setup when you install Analysis Services in SharePoint integrated mode.  
  
 A second account must be specified for the PowerPivot service application, a shared web service that runs under an application pool identity in a SharePoint farm. This account is specified when you configure a [!INCLUDE[ssGeminiShort](../../includes/ssgeminishort-md.md)] installation using either the PowerPivot Configuration tool or PowerShell.  
  
 Each service should run under a dedicated account so that you can audit connections or enable Kerberos authentication protocol in the farm.  
  
 Once the service accounts are set, any changes to either account must be made through SharePoint Central Administration. If you use alternative tools (such as the Services console application, IIS Manager, or SQL Server Configuration Manager), permissions will not be updated for database access in the farm or for local file access on the physical server.  
  
 This topic contains the following sections:  
  
 [Update an expired password for SQL Server Analysis Services (PowerPivot) instance](#bkmk_passwordssas)  
  
 [Update an expired password for the PowerPivot Service Application](configure-power-pivot-service-accounts.md#bkmk_passwordapp)  
  
 [Change the account under which each service runs](#bkmk_newacct)  
  
 [Create or change the application pool for a PowerPivot service application](#bkmk_appPool)  
  
 [Account Requirements and Permissions](#requirements)  
  
 [Troubleshooting: Grant Administrative Permissions Manually](#updatemanually)  
  
 [Troubleshooting: Resolve HTTP 503 Errors Due to Expired Passwords for Central Administration or the SharePoint Foundation Web Application Service](#expired)  
  
##  <a name="bkmk_passwordssas"></a> Update an expired password for SQL Server Analysis Services (PowerPivot) instance  
  
1.  Point to Start, click **Administrative Tools**, and then click **Services**. Double-click **SQL Server Analysis Services (PowerPivot)**. Click **Log On**, and then enter the new password for the account.  
  
2.  In Central Administration, in the Security section, click **Configure managed accounts**.  
  
3.  Click **Edit** to change a specific account.  
  
4.  Select **Change password now**.  
  
5.  Select **Set account password to new value**. All services that run under the managed account will use the updated credentials.  
  
##  <a name="bkmk_passwordapp"></a> Update an expired password for the PowerPivot Service Application  
  
1.  In Central Administration, in the Security section, click **Configure managed accounts**.  
  
2.  Click **Edit** to change a specific account.  
  
3.  Select **Change password now**.  
  
4.  Select **Set account password to new value**. All services that run under the managed account will use the updated credentials.  
  
##  <a name="bkmk_newacct"></a> Change the account under which each service runs  
  
1.  In Central Administration, in the Security section, click **Configure service accounts**.  
  
2.  Select **Windows Service - SQL Server Analysis Services** to change the Analysis Services service account.  
  
3.  In **Select an account for this service**, choose an existing managed account or create a new one. The account must be a domain user account.  
  
4.  Select **Service Application Pool - SharePoint Web Services System** to change the application pool identity of the default PowerPivot service application. Depending on how your installation was configured, the service might be running under an existing service application pool created for SharePoint services. By default, the PowerPivot Configuration Tool registers the service as **Default PowerPivot Service Application (PowerPivot Service Application)**.  
  
     If the service was configured manually by a SharePoint administrator, the service most likely has its own service application pool.  
  
5.  In **Select an account for this service**, choose an existing managed account or create a new one. The account must be a domain user account.  
  
6.  Click **OK**.  
  
##  <a name="bkmk_appPool"></a> Create or change the application pool for a PowerPivot service application  
  
1.  In Central Administration, in Application Management, click **Manage service applications**.  
  
2.  Select, but do not click, the PowerPivot service application. Clicking on the application name opens the PowerPivot Management Dashboard, which does not provide a link to the properties page that specifies the application pool.  You can click the empty white space in the row, or click on type name, to select the PowerPivot service application.  
  
3.  Click **Properties** on the ribbon.  
  
4.  Select **Create a new application pool**. Provide a name for the application pool and specify a managed account for its identity.  
  
##  <a name="requirements"></a> Account Requirements and Permissions  
 When planning a PowerPivot for SharePoint deployment, you must plan for the following service accounts.  
  
-   Analysis Services service account. Analysis Services processes PowerPivot queries and data refresh jobs in the farm. This account is always specified during SQL Server Setup when you install PowerPivot for SharePoint.  
  
-   PowerPivot Service Application Pool. A PowerPivot service application is associated with a PowerPivot System Service that provides SharePoint integration and infrastructure for PowerPivot query processing in a farm. The application pool that you specify for a PowerPivot service application is the service identity of the PowerPivot System Service. You can have multiple PowerPivot service applications in a farm. Each one that you create should run in its own application pool.  
  
#### Analysis Services Service Account  
  
|Requirement|Description|  
|-----------------|-----------------|  
|Provisioning requirement|This account must be specified during SQL Server Setup using the **Analysis Services - Configuration page** in the installation wizard (or the `ASSVCACCOUNT` installation parameter in a command line setup).<br /><br /> You can modify the user name or password using Central Administration, PowerShell, or the PowerPivot Configuration Tool. Using other tools to change accounts and passwords is not supported.|  
|Domain user account requirement|This account must be a Windows domain user account. Built-in machine accounts (such as Network Service or Local Service) are prohibited. SQL Server Setup enforces the domain user account requirement by blocking installation whenever a machine account is specified.|  
|Permission requirements|This account must be a member of the SQLServerMSASUser$\<server>$PowerPivot security group and the WSS_WPG security groups on the local computer. These permissions should be granted automatically. For more information on how to check or grant permissions, see [Grant the PowerPivot Service Account Administrative Permissions Manually](#updatemanually) in this topic and [Initial Configuration &#40;PowerPivot for SharePoint&#41;](../../sql-server/install/initial-configuration-powerpivot-for-sharepoint.md).|  
|Scale-out requirements|If you install multiple PowerPivot for SharePoint server instances in a farm, all of the Analysis Services server instances must run under the same domain user account. For example, if you configure the first [!INCLUDE[ssGeminiSrv](../../includes/ssgeminisrv-md.md)] instance to run as Contoso\ssas-srv01, then all additional [!INCLUDE[ssGeminiSrv](../../includes/ssgeminisrv-md.md)] instances that you deploy thereafter in the same farm must also run as Contoso\ssas-srv01 (or whatever the current account happens to be).<br /><br /> Configuring all service instances to run under the same account allows the PowerPivot System service to allocate query processing or data refresh jobs to any Analysis Services service instance in the farm. In addition, it enables the use of the Managed Account feature in Central Administration for Analysis Services server instances. By using the same account for all [!INCLUDE[ssGeminiSrv](../../includes/ssgeminisrv-md.md)] instances, you can change account or password once, and all service instances that use those credentials are updated automatically.<br /><br /> SQL Server Setup enforces the same-account requirement. In a scale-out deployment where a SharePoint farm already has an instance of PowerPivot for SharePoint installed, Setup will block the new installation if the [!INCLUDE[ssGeminiSrv](../../includes/ssgeminisrv-md.md)] account you specified is different from the one already in use in the farm.|  
  
#### PowerPivot Service Application Pool  
  
|Requirement|Description|  
|-----------------|-----------------|  
|Provisioning requirement|PowerPivot System Service is a shared resource on the farm that becomes available when you create a service application. The service application pool must be specified when the service application is created. It can be specified two ways: using the PowerPivot Configuration Tool, or through PowerShell commands.<br /><br /> You might have configured the application pool identity to run under a unique account. But if you didn't, consider changing it now to run under a different account.|  
|Domain user account requirement|The application pool identity must be a Windows domain user account. Built-in machine accounts (such as Network Service or Local Service) are prohibited.|  
|Permission requirements|This account does not need local system Administrator permissions on the computer. However, this account must have Analysis Services system administrator permissions on the local [!INCLUDE[ssGeminiSrv](../../includes/ssgeminisrv-md.md)] that is installed on the same computer. These permissions are granted automatically by SQL Server Setup or when you set or change the application pool identity in Central Administration.<br /><br /> Administrative permissions are required for forwarding queries to the [!INCLUDE[ssGeminiSrv](../../includes/ssgeminisrv-md.md)]. They are also required for monitoring health, closing inactive sessions, and listening for trace events.<br /><br /> The account must have connect, read, and write permissions to the PowerPivot service application database. These permissions are granted automatically when the application is created, and updated automatically when you change accounts or passwords in Central Administration.<br /><br /> The PowerPivot service application will check that a SharePoint user is authorized to view data before retrieving the file, but it does not impersonate the user. There are no permission requirements for impersonation.|  
|Scale-out requirements|None.|  
  
##  <a name="updatemanually"></a> Troubleshooting: Grant Administrative Permissions Manually  
 Administrative permissions will fail to update if the person updating the credentials is not a local administrator on the computer. If this occurs, you can grant administrative permissions manually. The easiest way to do this is to run the PowerPivot Configuration timer job in Central Administration. With this approach, you can reset permissions for all PowerPivot servers in the farm. Note that this approach will only work if the SharePoint timer job is running as both farm administrator and as local administrator on the computer.  
  
1.  In Monitoring, click **Review job definitions**.  
  
2.  Select **PowerPivot Configuration Timer Job**.  
  
3.  Click **Run Now**.  
  
 As a last resort, you can ensure correct permissions by granting Analysis Services System Administration permissions to the PowerPivot service application, and then specifically add the service application identity to the SQLServerMSASUser$\<servername>$PowerPivot Windows security group. You must repeat these steps for every Analysis Services instance that is integrated with the SharePoint farm.  
  
 You must be a local administrator to update Windows security groups.  
  
1.  In SQL Server Management Studio, connect to the Analysis Services instance as \<server name>\POWERPIVOT.  
  
2.  Right-click the server name and select **Properties**.  
  
3.  Click **Security**.  
  
4.  Click **Add**.  
  
5.  Type the name of the account that is used for PowerPivot service application pool, and then click **OK**.  
  
6.  In Administrative Tools, click **Computer Management**.  
  
7.  Open **Local Users and Groups**.  
  
8.  Open **Groups**.  
  
9. Double-click SQLServerMSASUser$\<servername>$PowerPivot.  
  
10. Click **Add**.  
  
11. Type the name of the account that is used for PowerPivot service application pool, and then click **OK**.  
  
##  <a name="expired"></a> Troubleshooting: Resolve HTTP 503 Errors Due to Expired Passwords for Central Administration or the SharePoint Foundation Web Application Service  
 If Central Administration service or the SharePoint Foundation Web Application service stops working due to an account reset or password expiration, you will encounter HTTP 503 "Service unavailable" error messages when attempting to open SharePoint Central Administration or a SharePoint site. Follow these steps to bring your server back online. Once Central Administration is available, you can proceed to update expired account information.  
  
1.  In Administrative tools, click **Internet Information Services Manager**.  
  
2.  For the identity of the site or Central Administration application pool is a domain user account that has an expired password, do the following:  
  
    1.  Right-click the application pool name and select **Advanced Settings**.  
  
    2.  Select **Identity** and click the ... button to open the Application Pool Identity dialog box.  
  
    3.  Click **Set**.  
  
    4.  Type the username and password.  
  
3.  Run IISRESET. To do this, open an Administrator command prompt and type `iisreset` at the command.  
  
4.  In SharePoint Central Administration, in Security, select **Configure managed accounts**.  
  
5.  Click **Edit** to update the information of the managed account that has the expired password.  
  
6.  Select **Change password now**.  
  
7.  Click **Use existing password**.  
  
8.  Type the password, and then click **OK**.  
  
 If Reporting Services is installed, use the Reporting Services Configuration Manager to update passwords for the report server and the connection to the report server database. For more information, see [Configuration and Administration of a Report Server &#40;Reporting Services SharePoint Mode&#41;](../../reporting-services/configure-administer-report-server-reporting-services-sharepoint-mode.md).  
  
## See Also  
 [Start or Stop a PowerPivot for SharePoint Server](start-or-stop-a-power-pivot-for-sharepoint-server.md)   
 [Configure the PowerPivot Unattended Data Refresh Account &#40;PowerPivot for SharePoint&#41;](../configure-unattended-data-refresh-account-powerpivot-sharepoint.md)  
  
  
