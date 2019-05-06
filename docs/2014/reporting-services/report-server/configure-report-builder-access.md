---
title: "Configure Report Builder Access | Microsoft Docs"
ms.custom: ""
ms.date: "05/24/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
helpviewer_keywords: 
  - "Reporting Services, Report Builder"
  - "Report Builder 1.0, configuring access"
  - "configuring servers [Reporting Services]"
ms.assetid: a79003d0-c905-4d4c-9560-93a7cc1e1dd4
author: maggiesMSFT
ms.author: maggies
manager: kfile
---
# Configure Report Builder Access
  Report Builder is an ad hoc reporting tool that installs with a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] report server configured for either native mode or SharePoint integration mode.  
  
 Access to Report Builder depends on the following factors:  
  
-   Server properties that determine whether Report Builder is available on the report server.  
  
-   Role assignments or permissions that make Report Builder available to individual users or groups.  
  
-   Authentication settings that determine whether user credentials can be passed through to the report server or anonymous access is configured on application files.  
  
 To use Report Builder, you must have a published report model to work with.  
  
## Prerequisites  
 Report Builder is not available in every edition of [!INCLUDE[msCoName](../../includes/msconame-md.md)][!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. For a list of features that are supported by the editions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], see [Features Supported by the Editions of SQL Server 2014](../../getting-started/features-supported-by-the-editions-of-sql-server-2014.md).  
  
 The client computer must have the [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)] 2.0 installed. The [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)] provides the infrastructure for running [!INCLUDE[ndptecclick](../../includes/ndptecclick-md.md)] applications.  
  
 You must use [!INCLUDE[msCoName](../../includes/msconame-md.md)] Internet Explorer 6.0 or later.  
  
 Report Builder always runs in full trust; you cannot configure it to run in partial trust. In previous releases, it was possible to run Report Builder in partial trust, but that option is not supported in [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] and later versions.  
  
## Enabling and Disabling Report Builder  
 Report Builder is enabled by default. Report server administrators have the option of disabling the Report Builder feature by setting the report server system property `EnableReportDesignClientDownload` to `false`. Setting this property will disable Report Builder downloads for that report server.  
  
 To set report server system properties, you can use Management Studio or script:  
  
-   To use Management Studio, connect to the report server and use the Advanced Server Properties page to set `EnableReportDesignClientDownload` to `false`. For more information about how to open this page, see [Set Report Server Properties &#40;Management Studio&#41;](../tools/set-report-server-properties-management-studio.md).  
  
-   To view a sample script that sets a report server property, see [Script Deployment and Administrative Tasks](../tools/script-deployment-and-administrative-tasks.md).  
  
## Role Assignments Granting Report Builder Access on a Native Mode Report Server  
 On a native mode report server, create user role assignments that include tasks for using Report Builder. You must be a Content Manager and System Administrator to create or modify role definitions and role assignments on items and at the site level.  
  
 The following instructions assume that you are using predefined roles. If you modified the role definitions or if you upgraded from SQL Server 2000, check the roles to verify they contain the necessary tasks. For more information about creating role assignments, see [Grant User Access to a Report Server &#40;Report Manager&#41;](../security/grant-user-access-to-a-report-server.md).  
  
 After you create the role assignments, users will have permission to do the following:  
  
-   Users assigned to the System User and Browser roles can view published Report Builder reports on a report server, without having to launch Report Builder.  
  
-   Users assigned to the System User and Report Builder roles can generate models, start Report Builder and create reports, and save reports to the report server.  
  
-   Users assigned to the System User and Publisher roles can publish models from Model Designer to the report server. Models are used as data sources in Report Builder.  
  
-   Users assigned to the System Administrator and Content Manager roles have full permissions to create, view, and manage Report Builder reports.  
  
#### To verify required tasks are in the role definitions  
  
1.  Start Management Studio and connect to the report server.  
  
2.  Open the **Security** folder.  
  
3.  Open the **System Roles** folder.  
  
4.  Right-click **System Administrator**, and select **Properties**.  
  
5.  Select **Execute report definitions** and click **OK**.  
  
6.  Right-click **System User**, and select **Properties**.  
  
7.  Select **Execute report definitions** and click **OK**.  
  
8.  Open the **Roles** folder.  
  
9. Right-click **Browser**, and select **Properties**.  
  
10. Select **View models** and click **OK**.  
  
11. Right-click **Content Manager**, and select **Properties**.  
  
12. Select **View models**, **Manage models**, **Consume reports**, and click **OK**.  
  
13. Right-click **Publisher**, and select **Properties**.  
  
14. Select **Manage models** and click **OK**.  
  
15. Create the Report Builder role if it does not exist:  
  
    1.  Open the **Security** folder.  
  
    2.  Right-click **Roles**, and select **New Role**.  
  
    3.  In Name, type **Report Builder**.  
  
    4.  In Description, enter a description for the role so that users in Report Manager know what the role is for.  
  
    5.  Add the following tasks: **Consume reports**, **View reports**, **View models**, **View resources**, **View folders**, and **Manage individual subscription**s.  
  
    6.  Click **OK** to save the role.  
  
#### To create role assignments that grant access to Report Builder  
  
1.  Start Report Manager.  
  
2.  Click **Site Settings**.  
  
3.  Click **Security**.  
  
4.  If a role assignment already exists for the user or group for which you want to configure Report Builder access, click **Edit**.  
  
     Otherwise, click **New Role Assignment**. In Group or user, enter a Windows domain user or group account in this format: \<domain>\\<account\>. If you are using forms authentication or custom security, specify the user or group account in the format that is correct for your deployment.  
  
5.  Select **System User**, and then click **OK**.  
  
6.  Click **Home**.  
  
7.  Click the **Folder Settings** tab.  
  
8.  Click the **Security** tab.  
  
9. If a role assignment already exists for the user or group for which you want to configure Report Builder access, click **Edit**.  
  
     Otherwise, click **New Role Assignment**. In Group or user, enter a Windows domain user or group account in this format: \<domain>\\<account\>. If you are using forms authentication or custom security, specify the user or group account in the format that is correct for your deployment.  
  
10. Select **Report Builder**, and then click **Apply**.  
  
11. Repeat to create or modify role assignments for additional users or groups.  
  
## Permissions Granting Report Builder Access on a SharePoint Integrated Mode Report Server  
 On a SharePoint integrated mode report server, Report Builder access is granted to SharePoint users who have either Contribute or Full Control permission levels.  
  
 If you use custom permission levels, you must include Add Items and Edit Items in the permission level. For more information about Report Builder access through built-in permission levels, see [Use Built-in Security in Windows SharePoint Services for Report Server Items](../security/use-built-in-security-in-windows-sharepoint-services-for-report-server-items.md). For more information about permission requirements for custom permission levels, see [Set Permissions for Report Server Operations in a SharePoint Web Application](../security/set-permissions-for-report-server-operations-in-a-sharepoint-web-application.md).  
  
## Authentication Considerations and Credential Reuse  
 Report Builder uses ClickOnce technology to download and install its application files on a client computer. ClickOnce technology is intended for one-way application deployment that places program files on a client computer and runs the application as a separate process under the identity of the default user. Because Report Builder must connect back to the report server to get application files and report server data, it is important to understand how ClickOnce sets the security context and issues requests to remote computers under different scenarios:  
  
-   ClickOnce always runs as a separate process on the client computer. The process identity is the default Windows user credentials. ClickOnce does not share session data with Internet Explorer or obtain the current user security context from Internet Explorer.  
  
-   ClickOnce sends requests that specify Windows integrated security in the authentication header. If a server is configured for a different authentication type, the server will fail requests from ClickOnce with an authentication error. To work around this issue, you must either configure a server for Windows integrated security or you must enable Anonymous access to eliminate the authentication check.  
  
-   Report Builder opens its own connection to a report server. If you are not using Windows integrated security with single sign on, users must re-type their credentials for the Report Builder connection to the report server.  
  
 The following table describes the authentication types supported by the report server, and whether additional configuration is required to access Report Builder.  
  
|Report Server Authentication Type|How Report Builder and ClickOnce Application launcher responds|  
|---------------------------------------|--------------------------------------------------------------------|  
|Negotiate (default)<br /><br /> NTLM (default)|Under Windows integrated security, authenticated requests from ClickOnce and Report Builder typically succeed if the client and server are deployed in the same domain, the user is logged in to the client computer using a domain account with permission to access the Report Builder, and the report server is configured for Windows Authentication.<br /><br /> Requests succeed because ClickOnce and the browser connection to the report server have the same user identity.<br /><br /> Requests will fail if the user opened Internet Explorer with Run As and specified non-default credentials. If the user session on the report server is established under a specific account, and ClickOnce runs under a different account, the report server will deny access to the files.|  
|Kerberos|Internet Explorer, which is required for using Report Builder, does not support Kerberos directly.|  
|Basic authentication|ClickOnce does not support Basic authentication. It will not formulate requests that specify Basic authentication in the authentication header. It will not pass credentials or prompt the user to provide them. You can work around these issues by enabling Anonymous access to the Report Builder application files.<br /><br /> Requests will succeed if you enable Anonymous access to the Report Builder application files because the report server ignores the authentication header. For more information about how to enable Anonymous access to Report Builder, see [Configure Basic Authentication on the Report Server](../security/configure-basic-authentication-on-the-report-server.md).<br /><br /> After ClickOnce retrieves the application files, Report Builder opens a separate connection to a report server. Users must re-type their credentials to get Report Builder to connect to the report server. Report Builder does not collect credentials from Internet Explorer or ClickOnce.<br /><br /> Requests will fail if the report server is configured for Basic authentication and you did not enable Anonymous access to the Report Builder program files. The request fails because ClickOnce specifies Windows integrated security on its requests. If you configure the report server for Basic authentication, the server will reject the request because it specifies an invalid security package and because it lacks the credentials that the report server expects.<br /><br /> Additionally, if the report server is configured to use SharePoint integrated mode and the SharePoint site uses Basic authentication, users will encounter a 401 error when they try to use ClickOnce to install Report Builder on their client computers. This happens because SharePoint uses a cookie to keep a user authenticated for the duration of the session, but ClickOnce does not support the cookie. When a user launches a ClickOnce application, such as Report Builder, the application does not pass the cookie to SharePoint and thus SharePoint denies access and returns a 401 error.<br /><br /> You can work around this issue by trying one of the following options:<br /><br /> Select the **Remember my password** option when you provide your user credentials.<br /><br /> Enable Anonymous access to the SharePoint site collection.<br /><br /> Configure the environment so that the user does not provide credentials. For example, in an intranet environment you might configure the SharePoint server to belong to a Workgroup and then create user accounts on the local computer.|  
|Custom|When you configure a report server to use custom authentication, Anonymous access is enabled on the report server and requests are accepted with no authentication check.<br /><br /> After ClickOnce retrieves the application files, Report Builder opens a separate connection to a report server. Users must re-type their credentials to get Report Builder to connect to the report server. Report Builder does not collect credentials from Internet Explorer or ClickOnce.|  
  
## See Also  
 [Authentication with the Report Server](../security/authentication-with-the-report-server.md)   
 [Planning for Reporting Services and Power View Browser Support &#40;Reporting Services 2014&#41;](../browser-support-for-reporting-services-and-power-view.md)   
 [Start Report Builder &#40;Report Builder&#41;](../report-builder/start-report-builder.md)   
 [Report Manager  &#40;SSRS Native Mode&#41;](../report-manager-ssrs-native-mode.md)   
 [Connect to a Report Server in Management Studio](../tools/connect-to-a-report-server-in-management-studio.md)   
 [Report Server System Properties](../report-server-web-service/net-framework/reporting-services-properties-report-server-system-properties.md)  
  
  
