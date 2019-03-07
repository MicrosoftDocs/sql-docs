---
title: "Delete and Re-create Encryption Keys  (SSRS Configuration Manager) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
helpviewer_keywords: 
  - "re-creating encryption keys"
  - "encryption keys [Reporting Services]"
  - "deleting encryption keys"
  - "symmetric keys [Reporting Services]"
  - "removing encryption keys"
  - "resetting encryption keys"
ms.assetid: 201afe5f-acc9-4a37-b5ec-121dc7df2a61
author: markingmyname
ms.author: maghan
manager: kfile
---
# Delete and Re-create Encryption Keys  (SSRS Configuration Manager)
  Deleting and re-creating encryption keys are activities that fall outside of routine encryption key maintenance. You perform these tasks in response to a specific threat to your report server, or as a last resort when you can no longer access a report server database.  
  
-   Re-create the symmetric key when you believe the existing symmetric key is compromised. You can also re-create the key on a regular basis as a security best practice.  
  
-   Delete existing encryption keys and unusable encrypted content when you cannot restore the symmetric key.  
  
## Re-creating Encryption Keys  
 If you have evidence that the symmetric key is known to unauthorized users, or if your report server has been under attack and you want to reset the symmetric key as a precaution, you can re-create the symmetric key. When you re-create the symmetric key, all encrypted values will be re-encrypted using the new value. If you are running multiple report servers in a scale-out deployment, all copies of the symmetric key will be updated to the new value. The report server uses the public keys available to it to update the symmetric key for each server in the deployment.  
  
 You can only re-create the symmetric key when the report server is in a working state. Re-creating the encryption keys and re-encrypting content disrupts server operations. You must take the server offline while re-encryption is underway. There should be no requests made to the report server during re-encryption.  
  
 You can use the Reporting Services Configuration tool or the **rskeymgmt** utility to reset the symmetric key and encrypted data. For more information about how the symmetric key is created, see [Initialize a Report Server &#40;SSRS Configuration Manager&#41;](ssrs-encryption-keys-initialize-a-report-server.md).  
  
#### How to re-create encryption keys (Reporting Services Configuration Tool)  
  
1.  Disable the Report Server Web service and HTTP access by modifying the `IsWebServiceEnabled` property in the rsreportserver.config file. This step temporarily stops authentication requests from being sent to the report server without completely shutting down the server. You must have minimal service so that you can recreate the keys.  
  
     If you are recreating encryption keys for a report server scale-out deployment, disable this property on all instances in the deployment.  
  
    1.  Open Windows Explorer and navigate to *drive*:\Program Files\Microsoft SQL Server\\*report_server_instance*\Reporting Services. Replace *drive* with your drive letter and *report_server_instance* with the folder name that corresponds to the report server instance for which you want to disable the Web service and HTTP access. For example, C:\Program Files\Microsoft SQL Server\MSRS10_50.MSSQLSERVER\Reporting Services.  
  
    2.  Open the rsreportserver.config file.  
  
    3.  For the `IsWebServiceEnabled` property, specify `False`, and then save your changes.  
  
2.  Start the Reporting Services Configuration tool, and then connect to the report server instance you want to configure.  
  
3.  On the Encryption Keys page, click **Change**. [!INCLUDE[clickOK](../../includes/clickok-md.md)]  
  
4.  Restart the Report Server Windows service. If you are recreating encryption keys for a scale-out deployment, restart the service on all instances.  
  
5.  Re-enable the Web service and HTTP access by modifying the `IsWebServiceEnabled` property in the rsreportserver.config file. Do this for all instances if you are working with a scale out deployment.  
  
#### How to re-create encryption keys (rskeymgmt)  
  
1.  Disable the Report Server Web service and HTTP access. Use the instructions in the previous procedure to stop Web service operations.  
  
2.  Run **rskeymgmt.exe** locally on the computer that hosts the report server. Use the `-s` argument to reset the symmetric key. No other arguments are required:  
  
    ```  
    rskeymgmt -s  
    ```  
  
3.  Restart the Windows service and enable Web service operations.  
  
## Deleting Unusable Encrypted Content  
 If for some reason you cannot restore the encryption key, the report server will never be able to decrypt and use any data that is encrypted with that key. To return the report server to a working state, you must delete the encrypted values that are currently stored in the report server database and then manually re-specify the values you need.  
  
 Deleting the encryption keys removes all symmetric key information from the report server database and deletes any encrypted content. All unencrypted data is left intact; only encrypted content is removed. When you delete the encryption keys, the report server re-initializes itself automatically by adding a new symmetric key. The following occurs when you delete encrypted content:  
  
-   Connection strings in shared data sources are deleted. Users who run reports get the error "The ConnectionString property has not been initialized."  
  
-   Stored credentials are deleted. Reports and shared data sources are reconfigured to use prompted credentials.  
  
-   Reports that are based on models (and require shared data sources configured with stored or no credentials) will not run.  
  
-   Subscriptions are deactivated.  
  
 Once you delete encrypted content, you cannot recover it. You must re-specify connection strings and stored credentials, and you must activate subscriptions.  
  
 You can use the Reporting Services Configuration tool or the **rskeymgmt** utility to remove the values.  
  
#### How to delete encryption keys (Reporting Services Configuration Tool)  
  
1.  Start the Reporting Services Configuration tool, and then connect to the report server instance you want to configure.  
  
2.  Click **Encryption Keys**, and then click **Delete**. [!INCLUDE[clickOK](../../includes/clickok-md.md)]  
  
3.  Restart the Report Server Windows service. For a scale-out deployment, do this on all report server instances.  
  
#### How to delete encryption keys (rskeymmgt)  
  
1.  Run **rskeymgmt.exe** locally on the computer that hosts the report server. You must use the **-d** apply argument. The following example illustrates the argument you must specify:  
  
    ```  
    rskeymgmt -d  
    ```  
  
2.  Restart the Report Server Windows service. For a scale-out deployment, do this on all report server instances.  
  
#### How to re-specify encrypted values  
  
1.  For each shared data source, you must retype the connection string.  
  
2.  For each report and shared data source that uses stored credentials, you must retype the user name and password, and then save. For more information, see [Specify Credential and Connection Information for Report Data Sources](../../integration-services/connection-manager/data-sources.md) in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Books Online.  
  
3.  For each data-driven subscription, open each subscription and retype the credentials to the subscription database.  
  
4.  For subscriptions that use encrypted data (this includes the File Share delivery extension and any third-party delivery extension that uses encryption), open each subscription and retype credentials. Subscriptions that use Report Server e-mail delivery do not use encrypted data and are unaffected by the key change.  
  
## See Also  
 [Configure and Manage Encryption Keys &#40;SSRS Configuration Manager&#41;](ssrs-encryption-keys-manage-encryption-keys.md)   
 [Store Encrypted Report Server Data &#40;SSRS Configuration Manager&#41;](ssrs-encryption-keys-store-encrypted-report-server-data.md)  
  
  
