---
title: "Back up and restore SQL Server Reporting Services (SSRS) encryption keys"
description: "Learn how to back up and restore SSRS encryption keys Learn how to back up and restore SSRS encryption keys by using Report Server Configuration Manager."
author: maggiesMSFT
ms.author: maggies
ms.date: 07/10/2024
ms.service: reporting-services
ms.subservice: report-server
ms.topic: how-to
ms.custom: updatefrequency5
helpviewer_keywords:
  - "backing up encryption keys [Reporting Services]"
  - "restoring encryption keys [Reporting Services]"
  - "encryption keys [Reporting Services]"
  - "symmetric keys [Reporting Services]"
#customer intent: As a SQL Server system administrator, I want to ensure the security and recoverability of my SSRS encryption keys so that I can maintain the integrity and availability of encrypted data within my report server environment.

---
# Back up and restore SQL Server Reporting Services (SSRS) encryption keys
[!INCLUDE [sqlserver2016](../../includes/applies-to-version/sqlserver2016.md)] :::image type="icon" source="../../includes/media/yes-icon.svg" border="false"::: [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Native Mode :::image type="icon" source="../../includes/media/yes-icon.svg" border="false"::: [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] SharePoint mode 

Learn how to back up and restore SSRS encryption keys by using the Report Server Configuration Manager and the rskeymgmt utility. You back up these keys so you can maintain the security and recoverability of your encrypted data. This process is essential when you change service account credentials, migrate installations, or recover from hardware failures. Use this process to ensure the integrity and availability of your report server environment.
  
> [!NOTE]
> Reporting Services integration with SharePoint is no longer available after SQL Server 2016.
  
You must restore the backup copy of the encryption key when you:  
  
- Change the Report Server Windows service account name or reset the password. When you use the Report Server Configuration Manager, backing up the key is part of a service account name change operation.  
    > [!NOTE]
    > Resetting the password isn't the same as changing the password. A password reset requires permission to overwrite account information on the domain controller. System administrators reset passwords when you forget or don't know a particular password. Only password resets require symmetric key restoration. Periodically changing an account password doesn't require you to reset the symmetric key.  
- Rename the computer or instance that hosts the report server. A report server instance is based on a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance name.  
- Migrate a report server installation or configure a report server to use a different report server database.  
- Recover a report server installation due to hardware failure.

::: moniker range="=sql-server-2016"
  
###  <a name="bkmk_backup_sharepoint"></a> Back up SharePoint mode report servers  
 For SharePoint mode report servers, you can either use PowerShell commands or use the management pages for the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] service application. For more information, see the "Key Management" section of [Manage a Reporting Services SharePoint service application](../../reporting-services/report-server-sharepoint/manage-a-reporting-services-sharepoint-service-application.md). 

::: moniker-end  
  
## Prerequisites

- SQL Server 2016 (13.x) or later.
- Connection to a report server database.
- Access to the Report Server Configuration Manager or the rskeymgmt utility.
- Secure storage location for the backup file.
 
##  <a name="bkmk_backup_configuration_manager"></a> Back up encryption keys 

You only need to back up one copy of the symmetric key. There's a one-to-one relationship between a report server database and a symmetric key. Although you only need to back up one copy, you might need to restore the key multiple times if you're running multiple report servers in a scale-out deployment model. Each report server instance needs its copy of the symmetric key to lock and unlock data in the report server database.

Backing up the symmetric key is a process that writes the key to a file that you specify, and then scrambles the key by using a password that you provide. The symmetric key can never be stored in an unencrypted state so you must provide a password to encrypt the key when you save it to disk. After you create the file, you must store it in a secure location **and remember the password** that's used to unlock the file.

### Back up encryption keys with the Report Server Configuration Manager (Native mode)  
  
1.  Start the Report Server Configuration Manager and connect to the report server instance you want to configure.  
1.  Select **Encryption Keys**, and then select **Backup**.   
1.  Specify a file to contain the stored key. [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] appends a `.snk` file extension to the file. Consider storing the file on a disk separate from the report server.  
1.  Enter a strong password.  
1.  Select **OK**.  
  
### <a name="bkmk_backup_rskeymgmt"></a> Back up encryption keys with the rskeymgmt utility (Native mode)  
  
1.  Run **rskeymgmt.exe** locally on the computer that hosts the report server. You must use the `-e` extract argument to copy the key, provide a file name, and specify a password. The following example illustrates the arguments you must specify:  
  
    ```  
    rskeymgmt -e -f d:\rsdbkey.snk -p<password>  
    ```  
  
## Restore encryption keys 
 
When you restore the symmetric key, you replace the existing key in the report server database. Here's what happens:  
  
- The symmetric key is retrieved from the password-protected backup file.  
- The symmetric key is encrypted by using the Report Server Windows service public key.  
- The newly encrypted symmetric key is stored in the report server database.  
- The previous symmetric key data is deleted.  
  
To restore the encryption key, you must have the encryption key backup and the password you used to protect it. If you have the key and the password, you can run the Reporting Services Configuration Manager or rskeymgmt utility to restore the key. Keep in mind:

- The symmetric key must match the one currently used to lock and unlock the encrypted data in the report server database. 
- If the restored key is valid, the report server can't access the encrypted data. 
- If you can't restore the key, you might need to delete all encrypted values. 
- If you don't have a backup copy, you must delete the existing key and encrypted content. For more information, see [Delete and re-create encryption keys &#40;Report Server Configuration Manager&#41;](../../reporting-services/install-windows/ssrs-encryption-keys-delete-and-re-create-encryption-keys.md). 

For more information about creating symmetric keys, see [Initialize a Report Server &#40;Report Server Configuration Manager&#41;](../../reporting-services/install-windows/ssrs-encryption-keys-initialize-a-report-server.md).  
  
###  <a name="bkmk_restore_configuration_manager"></a> Restore encryption keys with the Report Server Configuration Manager (Native Mode)  
  
1.  Start the Report Server Configuration Manager and connect to the report server instance you want to configure.  
1.  Select **Encryption Keys**, and then select **Restore**.  
1.  Select the `.snk` file that contains the backup copy of the encryption keys.  
1.  Enter the password that unlocks the file.  
1.  Select **OK**. 
  
###  <a name="bkmk_restore_rskeymgmt"></a> Restore encryption keys with the rskeymgmt utility (Native mode)  
  
1.  Run **rskeymgmt.exe** locally on the computer that hosts the report server. Use the `-a` argument to restore the keys. Provide a fully qualified file name and specify a password. The following example illustrates the arguments you must specify:  
  
    ```  
    rskeymgmt -a -f d:\rsdbkey.snk -p<password>  
    ```  
  
## Related content
 
- [Configure and manage encryption keys &#40;Report Server Configuration Manager&#41;](../../reporting-services/install-windows/ssrs-encryption-keys-manage-encryption-keys.md)  
  
  
