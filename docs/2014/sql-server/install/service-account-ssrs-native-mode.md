---
title: "Service Account (SSRS Native Mode) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
f1_keywords: 
  - "SQL12.rsconfigtool.serviceaccount.F1"
ms.assetid: face8120-4d32-4c6c-a1e8-99f27d1ff15d
author: markingmyname
ms.author: maghan
manager: craigg
---
# Service Account (SSRS Native Mode)
  Use the Service Account page to specify the account under which the Report Server service runs. This account is initially configured during Setup. You can modify it if you want to change the account or password. The Report Server Web service, Report Manager, and the background processing application all run under the service identity you specify on this page.  
  
 [!INCLUDE[applies](../../includes/applies-md.md)] [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Native mode.  
  
 The account you specify for the Report Server service requires permission to access the registry, report server program files, and the report server database. All permissions are configured for the account automatically when you use the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration Manager to set the account. If you use the service account to connect to the report server database, the Configuration Manager creates a database login for the account and configures database permissions by assigning the account to the RSExecRole on the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance that hosts the report server database. The report server database is the only data store that a report server writes to. The service account does not require permissions to any other data stores.  
  
 To open this page, start the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration Manager and select the link in the navigation pane. For more information, see [Reporting Services Configuration Manager &#40;Native Mode&#41;](../../../2014/sql-server/install/reporting-services-configuration-manager-native-mode.md).  
  
> [!IMPORTANT]  
>  Whenever you need to update the account or password, it is strongly recommended that you use the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration Manager. Using the Configuration Manager to update the account ensures that other internal settings that depend on the service identity are automatically updated at the same time.  
  
## Options  
 **Use a built-in account**  
 Select **Network Service**, **Local System**, or **Local Service** from the list. Only **Network Service** is recommended; however, you can configure the account to use any account that is available.  
  
 **Use another account**  
 Select this option to specify a Windows user account. You can enter a local Windows user account or domain user account. Specify a domain account in this format: *\<domain>\\<user\>*. Specify a local Windows user account in this format: *\<computer name>\\<user\>*. You can only select an existing account; you cannot create new accounts in [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration.  
  
 The maximum character limit on the account is 20 characters.  
  
 If your network uses Kerberos authentication and you configure the report server to run under a domain user account, you must register the service with the user account. For more information, see [Register a Service Principal Name &#40;SPN&#41; for a Report Server](../../reporting-services/report-server/register-a-service-principal-name-spn-for-a-report-server.md).  
  
 If you switch the account type (for example, replacing one Windows account with another or replacing a built-in account with a Windows domain account), you will be prompted to create a backup copy of the encryption key. The backup copy will be restored automatically when you select the new account.  
  
> [!NOTE]  
>  The [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration manager prompts you to back up and restore the encryption key whenever you modify the service account. These steps are necessary for ensuring that encrypted data remains available to the report server. For more information about these actions, see [Encryption Keys &#40;SSRS Native Mode&#41;](../../../2014/sql-server/install/encryption-keys-ssrs-native-mode.md).  
  
 Additionally, if you have a report server that is configured to run in SharePoint Integrated mode and you change the service account by using the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration Manager, you must also open SharePoint Central Administration and use the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] **Grant Database Access** page to re-apply the report server and instance settings. This step will grant the new service account access to the SharePoint databases, which is required for integrating [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] with a SharePoint product or technology. For more information about how to grant database access in SharePoint Central Administration, see [Configuration and Administration of a Report Server &#40;Reporting Services SharePoint Mode&#41;](../../../2014/reporting-services/configure-administer-report-server-reporting-services-sharepoint-mode.md) and [Reporting Services SharePoint Mode Installation &#40;SharePoint 2010 and SharePoint 2013&#41;](../../reporting-services/install-windows/install-reporting-services-sharepoint-mode.md).  
  
## Choosing an Account  
 For best results, specify an account that has network connection permissions, with access to network domain controllers and corporate SMTP servers or gateways. The following table summarizes the accounts and provides recommendations for using them.  
  
|Account|Explanation|  
|-------------|-----------------|  
|Domain user accounts|If you have a Windows domain user account that has the minimum permissions required for report server operations, you should use it.<br /><br /> A domain user account is recommended because it isolates the Report Server service from other applications. Running multiple applications under a shared account, such as Network Service, increases the risk of a malicious user taking control of the report server because a security breach for any one application can easily extend to all applications that run under the same account.<br /><br /> A domain user account is required if you are configuring the report server for constrained delegation, or for SharePoint integrated mode with SharePoint 2010 Products which require domain user accounts rather than built-in machine accounts.<br /><br /> Note that if you use a domain user account, you will have to change the password periodically if your organization enforces a password expiration policy. You might also need to register the service with the user account. For more information, see [Register a Service Principal Name &#40;SPN&#41; for a Report Server](../../reporting-services/report-server/register-a-service-principal-name-spn-for-a-report-server.md).<br /><br /> Avoid using a local Windows user account. Local accounts typically do not have sufficient permission to access resources on other computers. For more information about how using a local account limits report server functionality, see [Considerations for Using Local Accounts](#localaccounts) in this topic.|  
|**Network Service**|**Network Service** is a built-in least-privilege account that has network logon permissions. This account is recommended if you do not have a domain user account available or if you want to avoid any service disruptions that might occur as a result of password expiration policies.<br /><br /> If you select **Network Service**, try to minimize the number of other services that run under the same account. A security breach for any one application will compromise the security of all other applications that run under the same account.|  
|**Local Service**|**Local Service** is a built-in account that is like an authenticated local Windows user account. Services that run as the **Local Service** account access network resources as a null session with no credentials. This account is not appropriate for intranet deployment scenarios where the report server must connect to a remote report server database or a network domain controller to authenticate a user prior to opening a report or processing a subscription.|  
|**Local System**|**Local System** is a highly privileged account that is not required for running a report server. Avoid this account for report server installations. Choose a domain account or **Network Service** instead.|  
  
##  <a name="localaccounts"></a> Considerations for Using Local Accounts  
 The primary consideration for using local accounts is whether the report server requires access to remote database servers, mail servers, and domain controllers. If you configure the report server to run as a local Windows user account, Local Service, or Local System, you introduce considerations that must be factored into how you set other configuration settings, and on subscription creation and delivery:  
  
-   Running the service under a local account will limit your options later if you configure a connection to a remote report server database. Specifically, if you are using a remote report server database, you will have to configure the connection to use a domain user account or [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database user that has permission to log on to the remote [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance.  
  
-   Running the service under a local account will introduce new requirements on subscription creation. The report server stores information about the user who creates the subscription. If the user creates the subscription while logged on under a domain account, the Report Server service will try to connect to a domain controller to authenticate the user when the subscription is processed. If the service runs under a local account, the authentication request will fail when the report server tries to send the request to a remote domain controller. To work around this limitation, you can use a custom forms-based authentication extension or have all users connect to a report server under a local user account.  
  
-   Running the service under a local account will introduce new requirements for subscription delivery. Some delivery extensions have user account information in the subscription definition. If you are sending reports to e-mail addresses that are based on domain user accounts and you run the Report Server service under a local account, it cannot access a remote domain controller to resolve the target e-mail account.  
  
-   Built-in Windows service accounts (Local Service or Network Service) are not supported as report server service accounts on a computer that is a domain controller.  
  
## See Also  
 [Configure the Report Server Service Account &#40;SSRS Configuration Manager&#41;](../../reporting-services/install-windows/configure-the-report-server-service-account-ssrs-configuration-manager.md)   
 [Configure a Service Account &#40;SSRS Configuration Manager&#41;](../../../2014/sql-server/install/configure-a-service-account-ssrs-configuration-manager.md)   
 [Reporting Services Configuration Manager F1 Help Topics &#40;SSRS Native Mode&#41;](../../../2014/sql-server/install/reporting-services-configuration-manager-f1-help-topics-ssrs-native-mode.md)  
  
  
