---
title: "Configure a Service Account (SSRS Configuration Manager) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
helpviewer_keywords: 
  - "Report Server Web service, accounts"
  - "service accounts [Reporting Services]"
  - "Report Server Windows service, accounts"
  - "Web service [Reporting Services], report server"
ms.assetid: 25000ad5-3f80-4210-8331-d4754dc217e0
author: markingmyname
ms.author: maghan
manager: craigg
---
# Configure a Service Account (SSRS Configuration Manager)
  In a [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] installation, the Report Server Web service, Report Manager, and the background processing application run within a single service. The account under which the service runs is defined during Setup when you specify the account in the Service Identity page, but you can use the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration tool if you want use a different account or update the password.  
  
 If you have a report server that is configured to use SharePoint Integrated mode and you change the service account by using the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration tool, you must also open SharePoint Central Administration and use the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] **Grant Database Access** page to re-apply the report server and instance settings. This step will grant the new service account access to the SharePoint databases, which is required for integrating [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] with [!INCLUDE[SPF2010](../../includes/spf2010-md.md)] or [!INCLUDE[SPS2010](../../includes/sps2010-md.md)].  
  
 Always use the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration tool to update the service account so that other settings that depend on the service identity can be updated concurrently.  
  
> [!NOTE]  
>  Built-in Windows service accounts (Local Service or Network Service) are not supported as report server service accounts on a computer that is a domain controller.  
  
 Procedures  
  
### To configure the Report Server service account  
  
1.  Start the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration manager and connect to the report server.  
  
2.  On the Service Account page, select the option that describes the type of account you want to use. For recommendations about which account type to specify, see [Configure the Report Server Service Account &#40;SSRS Configuration Manager&#41;](../../reporting-services/install-windows/configure-the-report-server-service-account-ssrs-configuration-manager.md).  
  
3.  If you selected a Windows user account, specify the new account and password. The account cannot be more than 20 characters.  
  
     If the report server is deployed in a network that supports Kerberos authentication, you must register the report server Service Principal Name (SPN) with the domain user account you just specified. For more information, see [Register a Service Principal Name &#40;SPN&#41; for a Report Server](../../reporting-services/report-server/register-a-service-principal-name-spn-for-a-report-server.md).  
  
4.  Click **Apply**.  
  
5.  When prompted to back up the symmetric key, type a file name and location for the symmetric key backup, type a password to lock and unlock the file, and then click **OK**.  
  
6.  If the report server uses the service account to connect to the report server database, the connection information will be updated to use the new account or password. Updating the connection information requires that you connect to the database. If the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] **Database Connection** dialog box appears, enter credentials that have permission to connect to the database, and then click **OK**.  
  
7.  When prompted to restore the symmetric key, type the password you specified in step 5, and then click **OK**.  
  
8.  Review the status messages in the Results pane to verify all tasks completed successfully.  
  
## Troubleshooting Service Identity Update Errors  
 Changing the service identity initiates a series of events that include restarting the service, updating the password-protected encryption key, updating URL reservations, and updating the report server database connection information if you are using the service account to connect to the report server database. You can monitor the status of these events by viewing the notifications in the Results panel at the bottom of the page. If errors occur during this process, you can try to resolve them using the following techniques:  
  
-   If the symmetric key cannot be restored, you can try to restore it manually by using **Restore** in the Encryption Keys page. If that does not work, consider deleting the encrypted content. You will have to re-create data source connection information and subscriptions, but the rest of your content will still be available. For more information, see [Back Up and Restore Reporting Services Encryption Keys](../../reporting-services/install-windows/ssrs-encryption-keys-back-up-and-restore-encryption-keys.md).  
  
-   If the service will not start, restart it manually by using the Services console application in Administrator Tools.  
  
-   URL reservation errors can occur when you update the service account. Each URL reservation includes a security descriptor that includes a Discretionary Access Control List (DACL) that grants permission to the service account to accept requests on the URL. When you update the account, the URL must be recreated to update the DACL with the new account information. If the URL reservation cannot be recreated, and you know the account to be valid, try to restart the computer. If the error persists, try to use a different account.  
  
## See Also  
 [Reporting Services Configuration Manager &#40;Native Mode&#41;](../../../2014/sql-server/install/reporting-services-configuration-manager-native-mode.md)   
 [Configure the Report Server Service Account &#40;SSRS Configuration Manager&#41;](../../reporting-services/install-windows/configure-the-report-server-service-account-ssrs-configuration-manager.md)   
 [Configure a Report Server Database Connection  &#40;SSRS Configuration Manager&#41;](../../../2014/sql-server/install/configure-a-report-server-database-connection-ssrs-configuration-manager.md)   
 [Service Account &#40;SSRS Native Mode&#41;](../../../2014/sql-server/install/service-account-ssrs-native-mode.md)   
 [Configure and Manage Encryption Keys &#40;SSRS Configuration Manager&#41;](../../reporting-services/install-windows/ssrs-encryption-keys-manage-encryption-keys.md)  
  
  
