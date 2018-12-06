---
title: "Encryption Keys (SSRS Native Mode) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
f1_keywords: 
  - "SQL12.rsconfigtool.encryptionkeypanel.F1"
ms.assetid: cc7e6f84-80e1-4b5e-9409-d0e074edd147
author: markingmyname
ms.author: maghan
manager: craigg
---
# Encryption Keys (SSRS Native Mode)
  Use the Encryption Keys page to manage the symmetric key that is used to encrypt and decrypt data in a report server. Managing the encryption keys is an important part of report server configuration. The symmetric key is created and applied automatically when you create the report server database. Create a backup copy of the symmetric key so that you can perform routine maintenance operations. The following maintenance tasks require that you have a valid copy of the symmetric key:  
  
-   Changing the service account for the Report Server service.  
  
-   Migrating a [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] installation to a different computer.  
  
-   Configuring a new report server instance to share or use an existing report server database.  
  
 [!INCLUDE[applies](../../includes/applies-md.md)] [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Native mode.  
  
> [!IMPORTANT]  
>  Periodically changing the Reporting Services encryption key is a security best practice. A recommended time to change the key is immediately following a major version upgrade of Reporting Services. Changing the key after an upgrade minimizes additional service interruption caused by changing the Reporting Services encryption key outside of the upgrade cycle.  
  
 Restoring the symmetric key is necessary if you updated the user account of the Report Server service (and you used a tool other than the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration Manager to change the account), or if you are migrating a report server installation to a new server.  
  
 To protect the symmetric key from unauthorized access, the symmetric key is encrypted using the private key of the Report Server service. Only the Report Server service is able to unlock and use the symmetric key to store sensitive data in the report server database. If you change the identity of the Report Server service, or if you migrate the report server to a new computer, the private key of the Report Server service will no longer be able to unlock the symmetric key. To restore access to the symmetric key, re-encrypt the symmetric key using the private key of the new Report Server service identity. Restoring the symmetric key is the process by which the re-encryption occurs.  
  
 Only restore a symmetric key if it is the same key that is currently used to encrypt and decrypt data in the report server database. If you restore a symmetric key that is not valid, you can no longer access sensitive data. In this case, delete and re-create the key.  
  
> [!IMPORTANT]  
>  The action of deleting and recreating the symmetric key cannot be reversed or undone. Deleting or recreating the key can have important ramifications on your current installation. If you delete the key, any existing data encrypted by the symmetric key will also deleted. Deleted data includes connection strings to external report data sources, stored connection strings, and some subscription information.  
  
 To open this page, start the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration Manager and select the link in the navigation pane. For more information, see [Reporting Services Configuration Manager &#40;Native Mode&#41;](../../../2014/sql-server/install/reporting-services-configuration-manager-native-mode.md).  
  
## Options  
 **Backup**  
 Copies the symmetric key to a file that you specify. The symmetric key is never stored in plain text. You must type a password to protect the file.  
  
 **Restore**  
 Applies a previously saved copy of the symmetric key to the report server database. You must provide the password to unlock the file.  
  
 The previous copy of the symmetric key for the report server instance you are currently connected to is overwritten by the restored version. After you restore the symmetric key, you must initialize all the report servers that use the report server database. For more information about initializing report servers, see [Initialize a Report Server &#40;SSRS Configuration Manager&#41;](../../reporting-services/install-windows/ssrs-encryption-keys-initialize-a-report-server.md).  
  
 **Change**  
 Recreates the symmetric key and re-encrypts all encrypted values in the report server database. Be sure to stop the Report Server service before recreating the symmetric key.  
  
 In a scale-out deployment, all copies of the symmetric key are replaced with newer versions. Before changing the symmetric key, be sure to review the list of servers that are joined to the scale-out deployment to verify that only valid report server instances are given access to the new key. The servers that are part of a scale-out deployment are listed in the **Scale-out Deployment** page. Stop the service on each report server in the deployment before recreating the key.  
  
 Note that regenerating the symmetric key can be a long-running process if you have many data sources and subscriptions.  
  
 **Delete**  
 Deletes the symmetric key and all encrypted content, including connection strings and stored credentials. You should only delete the symmetric key if you cannot restore it.  
  
 Once you delete the symmetric key, you must re-enter the missing connection strings and stored credentials in the reports and shared data sources that no longer have these values. You must also update all subscriptions that use delivery extensions that store encrypted data. This includes the file share delivery extension and any third-party delivery extension that use encrypted value.  
  
 There is no automated way to update this information. Each report, subscription, and shared data source that uses stored credentials and connection strings must be updated one at a time.  
  
## See Also  
 [Reporting Services Configuration Manager F1 Help Topics &#40;SSRS Native Mode&#41;](../../../2014/sql-server/install/reporting-services-configuration-manager-f1-help-topics-ssrs-native-mode.md)   
 [Back Up and Restore Reporting Services Encryption Keys](../../reporting-services/install-windows/ssrs-encryption-keys-back-up-and-restore-encryption-keys.md)   
 [Delete and Re-create Encryption Keys  &#40;SSRS Configuration Manager&#41;](../../reporting-services/install-windows/ssrs-encryption-keys-delete-and-re-create-encryption-keys.md)   
 [Initialize a Report Server &#40;SSRS Configuration Manager&#41;](../../reporting-services/install-windows/ssrs-encryption-keys-initialize-a-report-server.md)   
 [Store Encrypted Report Server Data &#40;SSRS Configuration Manager&#41;](../../reporting-services/install-windows/ssrs-encryption-keys-store-encrypted-report-server-data.md)  
  
  
