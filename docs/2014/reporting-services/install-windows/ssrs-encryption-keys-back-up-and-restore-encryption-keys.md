---
title: "Back Up and Restore Reporting Services Encryption Keys | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
helpviewer_keywords: 
  - "backing up encryption keys [Reporting Services]"
  - "restoring encryption keys [Reporting Services]"
  - "encryption keys [Reporting Services]"
  - "symmetric keys [Reporting Services]"
ms.assetid: 6773d5df-03ef-4781-beb7-9f6825bac979
author: markingmyname
ms.author: maghan
manager: kfile
---
# Back Up and Restore Reporting Services Encryption Keys
  An important part of report server configuration is creating a backup copy of the symmetric key used for encrypting sensitive information. A backup copy of the key is required for many routine operations, and enables you to reuse an existing report server database in a new installation.  
  
 **[!INCLUDE[applies](../../includes/applies-md.md)]**  [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Native Mode | [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] SharePoint mode  
  
 It is necessary to restore the backup copy of the encryption key when any of the following events occur:  
  
-   Changing the Report Server Windows service account name or resetting the password. When you use the Reporting Services Configuration Manager, backing up the key is part of a service account name change operation.  
  
    > [!NOTE]  
    >  Resetting the password is not the same as changing the password. A password reset requires permission to overwrite account information on the domain controller. Password resets are performed by a system administrator when you forget or do not know a particular password. Only password resets require symmetric key restoration. Periodically changing an account password does not require you to reset the symmetric key.  
  
-   Renaming the computer or instance that hosts the report server (a report server instance is based on a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance name).  
  
-   Migrating a report server installation or configuring a report server to use a different report server database.  
  
-   Recovering a report server installation due to hardware failure.  
  
 You only need to back up one copy of the symmetric key. There is a one-to-one correspondence between a report server database and a symmetric key. Although you only need to back up one copy, you might need to restore the key multiple times if you are running multiple report servers in a scale-out deployment model. Each report server instance will need its copy of the symmetric key to lock and unlock data in the report server database.  
  
  
## Backing Up the Encryption Keys  
 Backing up the symmetric key is a process that writes the key to a file that you specify, and then scrambles the key using a password that you provide. The symmetric key can never be stored in an unencrypted state so you must provide a password to encrypt the key when you save it to disk. After the file is created, you must store it in a secure location **and remember the password** that is used to unlock the file. To backup the symmetric key, you can use the following tools:  
  
 **Native mode:** Either the Reporting Services Configuration Manager or the **rskeymgmt** utility.  
  
 **SharePoint mode:** SharePoint Central Administration pages or PowerShell.  
  
####  <a name="bkmk_backup_sharepoint"></a> Backup SharePoint Mode Report Servers  
 For SharePoint mode report servers you can either use PowerShell commands or use the management pages for the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] service application. For more information, see the "Key Management" section of [Manage a Reporting Services SharePoint Service Application](../manage-a-reporting-services-sharepoint-service-application.md)  
  
####  <a name="bkmk_backup_configuration_manager"></a> Back up encryption keys -Reporting Services Configuration Manager (Native Mode)  
  
1.  Start the Reporting Services Configuration Manager, and then connect to the report server instance you want to configure.  
  
2.  Click **Encryption Keys**, and then click **Back Up**.  
  
3.  Type a strong password.  
  
4.  Specify a file to contain the stored key. [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] appends a .snk file extension to the file. Consider storing the file on a disk separate from the report server.  
  
5.  [!INCLUDE[clickOK](../../includes/clickok-md.md)]  
  
####  <a name="bkmk_backup_rskeymgmt"></a> Back up encryption keys -rskeymgmt (Native Mode)  
  
1.  Run **rskeymgmt.exe** locally on the computer that hosts the report server. You must use the `-e` extract argument to copy the key, provide a file name, and specify a password. The following example illustrates the arguments you must specify:  
  
    ```  
    rskeymgmt -e -f d:\rsdbkey.snk -p<password>  
    ```  
  
## Restore Encryption Keys  
 Restoring the symmetric key overwrites the existing symmetric key that is stored in the report server database. Restoring an encryption key replaces an unusable key with a copy that you previously saved to disk. Restoring encryption keys results in the following actions:  
  
-   The symmetric key is opened from the password protected backup file.  
  
-   The symmetric key is encrypted using the public key of the Report Server Windows service.  
  
-   The encrypted symmetric key is stored in the report server database.  
  
-   The previously stored symmetric key data (for example, key information that was already in the report server database from a previous deployment) is deleted.  
  
 To restore the encryption key, you must have a copy of the encryption key on file. You must also know the password that unlocks the stored copy. If you have the key and the password, you can run the Reporting Services Configuration tool or **rskeymgmt** utility to restore the key. The symmetric key must be the same one that locks and unlocks encrypted data currently stored in the report server database. If you restore a copy that is not valid, the report server cannot access the encrypted data currently stored in the report server database. If this occurs, you might need to delete all encrypted values if you cannot restore a valid key. If for some reason you cannot restore the encryption key (for example, if you do not have a backup copy), you must delete the existing key and encrypted content. For more information, see [Delete and Re-create Encryption Keys  &#40;SSRS Configuration Manager&#41;](ssrs-encryption-keys-delete-and-re-create-encryption-keys.md). For more information about creating symmetric keys, see [Initialize a Report Server &#40;SSRS Configuration Manager&#41;](ssrs-encryption-keys-initialize-a-report-server.md).  
  
####  <a name="bkmk_restore_configuration_manager"></a> Restore encryption keys -Reporting Services Configuration Manager (Native Mode)  
  
1.  Start the Reporting Services Configuration Manager, and then connect to the report server instance you want to configure.  
  
2.  On the Encryption Keys page, click **Restore**.  
  
3.  Select the .snk file that contains the back up copy.  
  
4.  Type the password that unlocks the file.  
  
5.  [!INCLUDE[clickOK](../../includes/clickok-md.md)]  
  
####  <a name="bkmk_restore_rskeymgmt"></a> Restore encryption keys - rskeymgmt (Native Mode)  
  
1.  Run **rskeymgmt.exe** locally on the computer that hosts the report server. Use the `-a` argument to restore the keys. You must provide a fully-qualified file name and specify a password. The following example illustrates the arguments you must specify:  
  
    ```  
    rskeymgmt -a -f d:\rsdbkey.snk -p<password>  
    ```  
  
## See Also  
 [Configure and Manage Encryption Keys &#40;SSRS Configuration Manager&#41;](ssrs-encryption-keys-manage-encryption-keys.md)  
  
  
