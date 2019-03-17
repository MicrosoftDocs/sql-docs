---
title: Configure the Report Server Service Account (SSRS Configuration Manager) | Microsoft Docs
author: markingmyname
ms.author: maghan
manager: kfile
ms.reviewer: ""
ms.prod: sql-server-2014
ms.technology: 
ms.topic: conceptual
ms.custom: "seodec18"
ms.date: 12/10/2018
---
# Configure the Report Server Service Account (SSRS Configuration Manager)

  [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] is implemented as a single service that contains a Report Server Web service, Report Manager, and a background processing application that is used for scheduled report processing and subscription delivery. This topic explains how the service account is initially configured and how to modify the account or password using the Reporting Services Configuration tool.  
  
## Initial Configuration

 The Report Server service account is defined during Setup. You can run the service under a domain user account or a built-in such as `NetworkService` account. There is no default account; whatever account you specify in the [Server Configuration - Service Accounts](../../sql-server/install/server-configuration-service-accounts.md) page of the Installation Wizard becomes the initial account of the Report Server service.  
  
> [!IMPORTANT]
> Although the Report Server Web service and Report Manager are [!INCLUDE[vstecasp](../../includes/vstecasp-md.md)] applications, they do not run under the [!INCLUDE[vstecasp](../../includes/vstecasp-md.md)] account. The single service architecture runs both ASP.NET applications within the same Report Server process identity. This is an important change from previous releases, where both the Report Server Web service and Report Manager ran under the [!INCLUDE[vstecasp](../../includes/vstecasp-md.md)] worker process identity specified in IIS.
  
## Changing the Service Account

 To view and reconfigure service account information, always use the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration tool. Service identity information is stored internally in multiple locations. Using the tool ensures that all references are updated accordingly whenever you change the account or password. The [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration tool performs the following additional steps to ensure the report server remains available:  
  
- Automatically adds the new account to the report server group created on the local computer. This group is specified in the access control lists (ACLs) that secure [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] files.  
  
- Automatically updates the login permissions on the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssDE](../../includes/ssde-md.md)] instance used to host the report server database. The new account will be added to the **RSExecRole**.  
  
     The database login for the old account will not be removed automatically. Be sure to remove accounts that are no longer in use. For more information, see [Administer a Report Server Database &#40;SSRS Native Mode&#41;](../report-server/report-server-database-ssrs-native-mode.md) in SQL Server Books Online.  
  
     Granting database permissions to new service account only occurs if you configured the report server database connection to use the service account in the first place. If you configured the report server database connection to use a domain user account or a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database login, the connection information is not affected by the service account update.  
  
- Automatically updates the encryption key to include the profile information of the new account.  
  
    > [!NOTE]  
    > If the report server is part of the scale-out deployment, only the report server that you are updating is affected. The encryption keys for other report servers in the deployment are unaffected by the service account change.  
  
 For instructions on how to set the account, see [Configure a Service Account &#40;SSRS Configuration Manager&#41;](../../sql-server/install/configure-a-service-account-ssrs-configuration-manager.md).  
  
## Choosing an Account

 You can configure the Report Server service to run under any of these account types:  
  
- Least-privileged Windows user account  
  
- NetworkService  
  
- LocalSystem  
  
- LocalService  
  
 There is no single best approach for choosing an account type. Each account has advantages and disadvantages that you must consider. If you are deploying [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] on a production server, best practices suggest that you configure the service to run under a domain user account so that you can avoid widespread damage if a shared account is compromised by a malicious user. It also makes it easier to audit the logon activity for this account. A trade-off to using a Windows user account is that if you are deploying [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] in a network that uses Kerberos authentication, you must register the service with the user account. For more information, see [Register a Service Principal Name &#40;SPN&#41; for a Report Server](../report-server/register-a-service-principal-name-spn-for-a-report-server.md).  
  
 The following guidelines and links in this section can help you decide on an approach that is best for your deployment.  
  
- [Service Account &#40;SSRS Native Mode&#41;](../../sql-server/install/service-account-ssrs-native-mode.md).  
  
- [Configure Windows Service Accounts and Permissions](../../database-engine/configure-windows/configure-windows-service-accounts-and-permissions.md) in SQL Server Books Online.  
  
- [The Services and Service Accounts Security Planning Guide](http://usergroup.doubletake.com/file_cabinet/download/0x000021733).  
  
## Updating an Expired Password

 If the Report Server service runs under a domain account and the password expires before you can update it in the Reporting Services Configuration tool, the service will not start until you specify a new password. If the service cannot be started, you cannot use the Reporting Services Configuration tool to connect to that server to update the account. In this case, you must use a combination of tools to bring the server back online.  
  
 To reset the password, do the following:  
  
1. On the **Start** menu, point to **Control Panel**, point to **Administrator Tools**, and click **Services**.  
  
2. Right-click **SQL Server Reporting Services**, select **Properties**.  
  
3. Click **Log On**, and type the new password.  
  
4. After you update the password, start the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration tool and update the password in the Service Account page. This additional step is necessary to update the account information that is stored internally by the report server.  
  
 If the service account password for the [!INCLUDE[ssDE](../../includes/ssde-md.md)] expires, the `rsReportServerDatabaseUnavailable` error occurs when you try to connect to the report server. Resetting the password resolves this error.  
  
## Configuring the Report Server Service for a SharePoint Integrated Report Server

 If you are running a report server in SharePoint integrated mode, you must update the service account information that is stored in the SharePoint configuration database if either of the following conditions are true:  
  
- Modifying the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] service account (for example, switching from NetworkService to a domain user account).  
  
- Extending a SharePoint farm to include an additional SharePoint Web application. If the server farm is configured for report server integration, and newly added application is configured to run under a different user account than other applications in the farm, you must update the database access information.  
  
 After you reset the database access information, you should then restart the [!INCLUDE[winSPServ](../../includes/winspserv-md.md)] service to ensure that the old connection is no longer used.  
  
1. In **Administrative Tools**, click **SharePoint 2010 Central Administration**.  
  
2. Click **Application Management**.  
  
3. In the Reporting Services section, click **Grant Database Access**.  
  
4. Click **OK**. The Enter Credentials dialog box appears.  
  
5. Enter the credentials of a user who is a member of the Local Administrator's group on the computer that hosts the report server. The credentials will be used for a one-time connection to the report server computer for the purpose of retrieving service account information. The database login that is created for each service account will be updated in SharePoint databases.  
  
6. To restart the service, click **Operations**.  
  
7. In Topology and Services, click **Services on Server**.  
  
8. For Windows SharePoint Services Web Application, click **Stop**.  
  
9. Wait for the service stop.  
  
10. Click **Start**.  
  
> [!NOTE]  
> SharePoint products and technologies require domain accounts for service configuration like reporting services SharePoint mode.  
  
## Next Steps

 [Configure a Service Account &#40;SSRS Configuration Manager&#41;](../../sql-server/install/configure-a-service-account-ssrs-configuration-manager.md)
 [Service Account &#40;SSRS Native Mode&#41;](../../sql-server/install/service-account-ssrs-native-mode.md)
 [Configure Report Server URLs  &#40;SSRS Configuration Manager&#41;](configure-report-server-urls-ssrs-configuration-manager.md)
 [Reporting Services Configuration Manager &#40;Native Mode&#41;](../../sql-server/install/reporting-services-configuration-manager-native-mode.md)
