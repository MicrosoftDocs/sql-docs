---
title: "rskeymgmt Utility | Microsoft Docs"
description: Learn about the reskeymgmt utility that manages the symmetric key that protects sensitive report server data against unauthorized access.
ms.date: 03/20/2017
ms.service: reporting-services
ms.subservice: tools


ms.topic: conceptual
helpviewer_keywords: 
  - "report servers [Reporting Services], encryption"
  - "joining report server instances [SQL Server]"
  - "report server scale-out deployments [Reporting Services]"
  - "cryptography [Reporting Services]"
  - "multiple report server instances"
  - "command prompt utilities [Reporting Services]"
  - "report servers [Reporting Services], scale-out deployments"
  - "encryption [Reporting Services]"
  - "rskeymgmt utility"
  - "scale-out deployments [Reporting Services]"
ms.assetid: 53f1318d-bd2d-4c08-b19f-c8b698b5b3d3
author: maggiesMSFT
ms.author: maggies
---
# rskeymgmt Utility (SSRS)
  Extracts, restores, creates, and deletes the symmetric key used to protect sensitive report server data against unauthorized access. This utility is also used to join report server instances in a scale-out deployment. A *report server scale-out deployment* refers to multiple report server instances that share a single report server database.  
  
## Syntax  
  
```  
  
rskeymgmt {-?}  
{-eextract}  
{-aapply}  
{-ddeleteall}  
{-srecreatekey}  
{-rremoveinstancekey}  
{-jjoinfarm}  
{-iinstance}  
{-ffile}  
{-pencryptionpassword}  
{-mremotecomputer}  
{-ninstancenameofremotecomputer}  
{-uadministratoruseraccount}  
{-vadministratorpassword}  
{-ttrace}  
```  
  
## Arguments  
 **-?**  
 Displays the syntax of **rskeymgmt** arguments.  
  
 **-e**  
 Extracts the symmetric key used to encrypt and decrypt data for the report server instance so that you can copy it to a file.  
  
 This argument does not take a value. However, you must include additional arguments on the command line to complete the extraction. The arguments that you must specify include **-f** and**-p**.  
  
 **-a**  
 Replaces an existing symmetric key with a copy that you provide in a password protected backup file. All instances of the symmetric key are updated.  
  
 This argument does not take a value. However, you must include additional arguments on the command line to select the file that contains the key to be applied. The arguments that you can specify include **-f** and**-p**.  
  
 **-d**  
 Deletes all symmetric key instances and all encrypted data in a report server database. This argument does not take a value.  
  
 **-s**  
 Generates a new symmetric key and re-encrypts all encrypted content using the new key. All instances of the symmetric key are regenerated.  
  
 **-j**  
 Configures a remote report server instance to share the report server database that is used by the local report server instance.  
  
 **-r**  *installationID*  
 Removes the symmetric key information for a specific report server instance, thereby removing the report server from a scale-out deployment. The *installationID* is a GUID value that can be found in the RSReportserver.config file.  
  
 **-f**  *file*  
 Specifies a fully qualified path to the file that stores a backup copy of the symmetric keys.  
  
 For **rskeymgmt -e**, the symmetric key is written to the file you specify.  
  
 For **rskeymgmt -a**, the symmetric key value stored in the file is applied to the report server instance.  
  
 **-p**  *password*  
 (Required for **-f**) Specifies the password used to back up or apply a symmetric key. This value cannot be empty.  
  
 **-i**  
 Specifies a local report server instance. This argument is optional if you installed the report server on the default [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance (the default value for **-i** is MSSQLSERVER). If you installed the report server as a named instance, **-i** is required.  
  
 **-m**  
 Specifies the name of the remote computer that hosts the report server instance you are joining to the report server scale-out deployment. Use the name of the computer that identifies it on your network.  
  
 **-n**  
 Specifies the name of the report server instance on a remote computer. This argument is optional if you installed the report server on the default [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance (the default value for **-n** is MSSQLSERVER). If you installed the report server as a named instance, **-n** is required.  
  
 **-u**  *useraccount*  
 Specifies the administrator account on the remote computer that you are joining to the scale-out deployment. If an account is not specified, the credentials of the current user are used.  
  
 **-v**  *password*  
 (Required for **-u**) Specifies the password of an administrator account on the remote computer that you want to join to the scale-out deployment.  
  
 **-t**  *trace*  
 Outputs error messages to the trace log. This argument does not take a value. For more information, see [Report Server Service Trace Log](../../reporting-services/report-server/report-server-service-trace-log.md).  
  
## Permissions  
 You must be a local administrator to run the tool, and you must run it locally on the computer that hosts the report server. The rskeymgmt utility works with the local Report Server Windows instance (the utility cannot connect to remote instances of the Report Server Windows service so it cannot be used to manage the encryption keys of a remote report server instance).  
  
> [!NOTE]  
>  If you are using the **-u** and **-v** arguments, be sure to specify an account that has administrator permissions on the remote computer.  
  
## Examples  
 The following examples illustrate ways of using **rskeymgmt**. The following examples show how to extract, restore, and delete encryption keys, and how to configure a report server scale-out deployment.  
  
#### Extracting Encryption Keys  
 This example shows how to create a backup copy of the encryption key and save it to a password-protected file on a floppy disk. If the report server is installed as a named instance, add the **-i** argument.  
  
```  
rskeymgmt -e -f a:\backupkey\keys -p <password>  
```  
  
#### Restoring Encryption Keys  
 This example shows how to replace the encryption key. You must specify the location of the backup copy of the key and the password that unlocks the file.  
  
```  
rskeymgmt -a -f a:\backupkey\keys -p <password>  
```  
  
#### Deleting Encryption Keys and Encrypted Content  
 This example shows how to delete all encryption keys stored in a report server. If your installation is a report server scale-out deployment, the encryption keys for all report server instances that are included in the deployment will be deleted. Deleting an encryption key also deletes any existing encrypted values in the report server database. For more information about encrypted content, see [Store Encrypted Report Server Data &#40;Report Server Configuration Manager&#41;](../../reporting-services/install-windows/ssrs-encryption-keys-store-encrypted-report-server-data.md).  
  
```  
rskeymgmt -d  
```  
  
#### Joining a Remote Report Server Named Instance to a Scale-out Deployment  
 This example shows how to add a report server instance that is installed on a remote computer to a report server scale-out deployment. You must run the command on one of the computers that is already configured to use the shared database. The command arguments specify the remote report server instance you want to join to the scale-out deployment.  
  
```  
rskeymgmt -j -m <remotecomputer> -n <namedreportserverinstance> -u <administratoraccount> -v <administratorpassword>  
```  
  
> [!NOTE]  
>  A report server scale-out deployment refers to a deployment model where multiple report server instances share the same report server database. A report server database can be used by any report server instance that stores its symmetric keys in the database. For example, if a report server database contains key information for three report server instances, all three instances are considered to members of the same scale-out deployment.  
  
#### Joining Report Server Instances on the Same Computer  
 You can create a scale-out deployment from multiple report server instances that are installed on the same computer. Do not set the **-u** and **-v** arguments if you are joining report server instances that are installed locally. The **-u** and **-v** arguments are used only when you are joining an instance from a remote computer. If you specify the arguments, you will get the following error: "User credentials cannot be used for local connections."  
  
 The following example illustrates the syntax for creating a scale-out deployment using multiple local instances. In this example, \<**initializedinstance**> is the name of an instance that is already initialized to use the report server database, and \<**newinstance**> is the name of the instance that you want to add to the deployment:  
  
```  
rskeymgmt -j -i <initializedinstance> -m <computer name> -n <newinstance>  
```  
  
#### Removing Encryption Keys for a Single Report Server in a Scale-out Deployment  
 This example shows how to remove the encryption keys for a single report server in a report server scale-out deployment. The keys are removed from the report server database. Once the keys for that report server instance are removed, that report server instance can no longer access encrypted data in the database, effectively removing it from the scale-out deployment.  
  
 Removing a report server instance from a scale-out deployment requires you to specify an installation ID. The installation ID is a GUID stored in the RSReportserver.config file of the report server instance for which you want to remove encryption keys. You must run the following command on the computer that you want to remove from the scale-out deployment. If the report server is installed as a named instance, use the **-i** argument to specify the instance. For more information, see [RsReportServer.config Configuration File](../../reporting-services/report-server/rsreportserver-config-configuration-file.md).  
  
```  
rskeymgmt -r <installationID>  
```  
  
## File Location  
 Rskeymgmt.exe is located at **\<*drive*>:\Program Files\Microsoft SQL Server\110\Tools\Binn** or at **\<*drive*>:\Program Files (x86)\Microsoft SQL Server\110\Tools\Binn**. You can run the utility from any folder on your file system.  
  
## Remarks  
 A report server encrypts stored credentials and connection information. A public key and a symmetric key are used to encrypt data. A report server database must have valid keys in order for the report server to run. You can use **rskeymgmt** to back up, delete, or restore the keys. If the keys cannot be restored, this tool provides a way to delete encrypted content that can no longer be used.  
  
 The **rskeymgmt** utility is used to manage the key set that is defined during Setup or during initialization. It connects to the local Report Server Windows service through a Remote Procedure Call (RPC) endpoint. The Report Server Windows service must be running in order for this utility to work.  
  
 For more information about the encryption keys, see [Configure and Manage Encryption Keys &#40;Report Server Configuration Manager&#41;](../../reporting-services/install-windows/ssrs-encryption-keys-manage-encryption-keys.md) and [Initialize a Report Server &#40;Report Server Configuration Manager&#41;](../../reporting-services/install-windows/ssrs-encryption-keys-initialize-a-report-server.md).  
  
## See Also  
 [Scale-out Deployment  - Reporting Services Native mode &#40;Configuration Manager&#41;](/previous-versions/sql/sql-server-2016/ms181357(v=sql.130))   
 [Reporting Services Report Server &#40;Native Mode&#41;](../../reporting-services/report-server/reporting-services-report-server-native-mode.md)   
 [Report Server Command Prompt Utilities &#40;SSRS&#41;](../../reporting-services/tools/report-server-command-prompt-utilities-ssrs.md)   
 [Configure and Manage Encryption Keys &#40;Report Server Configuration Manager&#41;](../../reporting-services/install-windows/ssrs-encryption-keys-manage-encryption-keys.md)  
  
