---
title: "SQL Server Connector maintenance & troubleshooting"
description: Learn about maintenance instructions and common troubleshooting steps for the SQL Server Connector. 
ms.custom: seo-lt-2019
ms.date: "04/12/2022"
ms.service: sql
ms.reviewer: vanto
ms.subservice: security
ms.topic: conceptual
helpviewer_keywords: 
  - "SQL Server Connector, appendix, EKM"
ms.assetid: 7f5b73fc-e699-49ac-a22d-f4adcfae62b1
author: Rupp29
ms.author: arupp
---
# SQL Server Connector Maintenance & Troubleshooting

[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]

  Supplemental information about the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Connector is provided in this topic. For more information about the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] connector, see [Extensible Key Management Using Azure Key Vault &#40;SQL Server&#41;](../../../relational-databases/security/encryption/extensible-key-management-using-azure-key-vault-sql-server.md), [Setup Steps for Extensible Key Management Using the Azure Key Vault](../../../relational-databases/security/encryption/setup-steps-for-extensible-key-management-using-the-azure-key-vault.md),  and [Use SQL Server Connector with SQL Encryption Features](../../../relational-databases/security/encryption/use-sql-server-connector-with-sql-encryption-features.md).  
  
##  <a name="AppendixA"></a> A. Maintenance Instructions for [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Connector  
  
### Key Rollover  
  
> [!IMPORTANT]  
> The [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Connector requires the key name to only use the characters "a-z", "A-Z", "0-9", and "-", with a 26-character limit.
> Different key versions under the same key name in Azure Key Vault will not work with [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Connector. To rotate an Azure Key Vault key that's being used by [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)], a new key with a new key name must be created.  
  
 Typically, server asymmetric keys for [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] encryption need to be versioned every 1-2 years. It's important to note that although the Key Vault allows keys to be versioned, customers should not use that feature to implement versioning. The [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Connector cannot deal with changes in Key Vault key version. To implement key versioning, create a new key in the Key Vault and then re-encrypt the data encryption key in [!INCLUDE[ssManStudio](../../../includes/ssmanstudio-md.md)].  
  
 For TDE, this is how this would be achieved:  
  
- **In PowerShell:** Create a new asymmetric key (with a different name from your current TDE asymmetric key) in the Key Vault.  
  
    ```powershell  
    Add-AzKeyVaultKey -VaultName 'ContosoDevKeyVault' `  
      -Name 'Key2' -Destination 'Software'  
    ```  
  
- **Using [!INCLUDE[ssManStudio](../../../includes/ssmanstudio-md.md)] or sqlcmd.exe:** Use the following statements as shown in Step 3, section 3.  
  
     Import the new asymmetric key.  
  
    ```sql  
    USE master  
    CREATE ASYMMETRIC KEY [MASTER_KEY2]
    FROM PROVIDER [EKM]
    WITH PROVIDER_KEY_NAME = 'Key2',
    CREATION_DISPOSITION = OPEN_EXISTING
    GO  
    ```  
  
     Create a new login to be associated with the new asymmetric key (as shown under the TDE instructions).  
  
    ```sql  
    USE master  
    CREATE LOGIN TDE_Login2
    FROM ASYMMETRIC KEY [MASTER_KEY2]  
    GO  
    ```  
  
     Create a new credential to be mapped to the login.  
  
    ```sql  
    CREATE CREDENTIAL Azure_EKM_TDE_cred2  
        WITH IDENTITY = 'ContosoDevKeyVault',
       SECRET = 'EF5C8E094D2A4A769998D93440D8115DAADsecret123456789='
    FOR CRYPTOGRAPHIC PROVIDER EKM;  
  
    ALTER LOGIN TDE_Login2  
    ADD CREDENTIAL Azure_EKM_TDE_cred2;  
    GO  
    ```  
  
     Choose the database whose database encryption key you would like to re-encrypt.  
  
    ```sql  
    USE [database]  
    GO  
    ```  
  
     Re-encrypt the database encryption key.  
  
    ```sql  
    ALTER DATABASE ENCRYPTION KEY
    ENCRYPTION BY SERVER ASYMMETRIC KEY [MASTER_KEY2];  
    GO  
    ```  
  
### Upgrade of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Connector  

Versions 1.0.0.440 and older have been replaced and are no longer supported in production environments. Versions 1.0.1.0 and newer are supported in production environments. Use the following instructions to upgrade to the latest version available on the [Microsoft Download Center](https://www.microsoft.com/download/details.aspx?id=45344).

### Upgrade

1. Stop SQL Server service using SQL Server Configuration Manager
1. Uninstall the old version using **Control Panel\Programs\Programs and Features**
    1. Application name: SQL Server Connector for Microsoft Azure Key Vault
    1. Version: 15.0.300.96 (or older)
    1. DLL file date: 01/30/2018 3:00 PM (or older)
1. Install (upgrade) new SQL Server Connector for Microsoft Azure Key Vault
    1. Version: 15.0.2000.440
    1. DLL file date: 09/11/2020 ‏‎5:17 AM
1. Start SQL Server service
1. Test encrypted DB(s) is/are accessible

### Rollback

1. Stop SQL Server service using SQL Server Configuration Manager

1. Uninstall the new version using **Control Panel\Programs\Programs and Features**
    1. Application name: SQL Server Connector for Microsoft Azure Key Vault
    1. Version: 15.0.2000.440
    1. DLL file date: 09/11/2020 ‏‎5:17 AM

1. Install old version of SQL Server Connector for Microsoft Azure Key Vault
    1. Version: 15.0.300.96
    1. DLL file date: 01/30/2018 3:00 PM
1. Start SQL Server service

1. Check that the databases using TDE are accessible.  
  
1. After validating that the update works, you may delete the old [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Connector folder (if you chose to rename it instead of uninstalling in Step 3.)

### Older versions of the SQL Server Connector
  
Deep links to older versions of the SQL Server Connector

- Current: [1.0.5.0 (version 15.0.2000.440) – File date September 11, 2020](https://download.microsoft.com/download/8/0/9/809494f2-bac9-4388-ad07-7eaf9745d77b/1033_15.0.2000.440/SQLServerConnectorforMicrosoftAzureKeyVault.msi)
- [1.0.5.0 (version 15.0.300.96) – File date January 30, 2018](https://download.microsoft.com/download/8/0/9/809494f2-bac9-4388-ad07-7eaf9745d77b/SQL%20Server%20Connector%20for%20Microsoft%20Azure%20Key%20Vault%201.0.5.0.msi)
- [1.0.4.0: (version 13.0.811.168)](https://download.microsoft.com/download/8/0/9/809494f2-bac9-4388-ad07-7eaf9745d77b/SQL%20Server%20Connector%20for%20Microsoft%20Azure%20Key%20Vault%201.0.4.0.msi)

### Rolling the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Service Principal

 [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] uses Service Principals created in Azure Active Directory as credentials to access the Key Vault. Service Principal has a Client ID and Authentication Key. A [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] credential is set up with the **VaultName**, **Client ID**, and **Authentication Key**. The **Authentication Key** is valid for a certain period of time (one or two years). Before the time period expires a new key must be generated in Azure AD for the Service Principal. Then the credential has to be changed in [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)]. [!INCLUDE[ssManStudio](../../../includes/ssmanstudio-md.md)] maintains a cache for the credential in the current session, so when a credential is changed, [!INCLUDE[ssManStudio](../../../includes/ssmanstudio-md.md)] should be restarted.  
  
### Key Backup and Recovery

The Key vault should be regularly backed up. If an asymmetric key in the vault is lost, it can be restored from backup. The key must be restored using the same name as before, which the Restore PowerShell command will do (see below steps).  
If the vault has been lost, you will need to recreate a vault and restore the asymmetric key to the vault using the same name as before. The vault name can be different (or the same as before). Set the access permissions on the new vault to grant to the SQL Server service principal the access that is needed for the SQL Server encryption scenarios, and then adjust the SQL Server credential so that the new vault name is reflected.

In summary, here are the steps:  
  
- Back up the vault key (using the Backup-AzureKeyVaultKey PowerShell cmdlet).  
- In the case of vault failure, create a new vault in the same geographic region. The user creating the vault should be in the same default directory as the service principal setup for SQL Server.  
- Restore the key to the new vault using the Restore-AzureKeyVaultKey PowerShell cmdlet, which restores the key using the same name as before. If there's already a key with the same name, the restore will fail.  
- Grant permissions to the SQL Server service principal to use this new vault.
- Modify the SQL Server credential used by the Database Engine to reflect the new vault name (if needed).  
  
Key backups can be restored across Azure regions, as long as they remain in the same geographic region or national cloud: USA, Canada, Japan, Australia, India, APAC, Europe Brazil, China, US Government, or Germany.  
  
##  <a name="AppendixB"></a> B. Frequently Asked Questions

### On Azure Key Vault
  
**How do key operations work with Azure Key Vault?**  
 The asymmetric key in the key vault is used to protect [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] encryption keys. Only the public portion of the asymmetric key ever leaves the vault; the private portion is never exported by the vault. All cryptographic operations using the asymmetric key are done within the Azure Key Vault service, and are protected by the service's security.  
  
 **What is a Key URI?**  
 Every key in Azure Key Vault has a Uniform Resource Identifier (URI), which you can use to reference the key in your application. Use the format `https://ContosoKeyVault.vault.azure.net/keys/ContosoFirstKey` to get the current version, and use the format `https://ContosoKeyVault.vault.azure.net/keys/ContosoFirstKey/cgacf4f763ar42ffb0a1gca546aygd87` to get a specific version.  
  
### On Configuring [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)]  

**What are the endpoints that the SQL Server Connector needs access to?**
 The Connector talks to two endpoints, which need to be allowed. The only port required for outbound communication to these other services is 443 for Https:

- login.microsoftonline.com/*:443
- *.vault.azure.net/*:443

Additionally, checking the certificate revocation list might create HTTP traffic on port 80. 


 > [!NOTE]
 > Using the SQL Server Connector for Azure Key Vault behind a firewall or proxy server can affect performance if traffic is delayed or blocked. Familiarize yourself with [Access Azure Key Vault behind a firewall](/azure/key-vault/general/access-behind-firewall) to ensure the correct rules are in place. 


**How do I connect to Azure Key Vault through an HTTP(S) Proxy Server?**
  The Connector uses Internet Explorer's Proxy configuration settings. These settings can be controlled via [Group Policy](/archive/blogs/askie/how-to-configure-proxy-settings-for-ie10-and-ie11-as-iem-is-not-available) or via the Registry, but it's important to note that they aren't system-wide settings and will need to be targeted to the service account running the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] instance. If a Database Administrator views or edits the settings in Internet Explorer, they'll only affect the Database Administrator's account rather than the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] engine. Logging on to the server interactively using the service account isn't recommended and is blocked in many secure environments. Changes to the configured proxy settings may require restarting the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] instance to take effect as they're cached when the Connector first attempts to connect to a key vault.

**Which key sizes in Azure Key Vault are supported by the SQL Server Connector?**
  The latest build of SQL Server Connector supports Azure Key Vault keys of sizes 2048 and 3072.
  
 > [!NOTE] 
 > The "sys.asymmetric_keys" view reports the key size as 2048 even if key size 3072 is used. This is a known gap in this view and the SQL Server product team will address this in a future release.

**What are the minimum permission levels required for each configuration step in [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)]?**  
 Though you could perform all the configuration steps as a member of the sysadmin fixed server role, [!INCLUDE[msCoName](../../../includes/msconame-md.md)] encourages you to minimize the permissions you use. The following list defines the minimum permission level for each action.  
  
- To create a cryptographic provider, requires `CONTROL SERVER` permission or membership in the **sysadmin** fixed server role.  
  
- To change a configuration option and run the `RECONFIGURE` statement, you must be granted the `ALTER SETTINGS` server-level permission. The `ALTER SETTINGS` permission is implicitly held by the sysadmin and **serveradmin** fixed server roles.  
  
- To create a credential, requires `ALTER ANY CREDENTIAL` permission.  
  
- To add a credential to a login, requires `ALTER ANY LOGIN` permission.  
  
- To create an asymmetric key, requires `CREATE ASYMMETRIC KEY` permission.  

**How do I change my default Active Directory so my key vault is created in the same subscription and Active Directory as the service principal I created for the [!INCLUDE[ssNoVersion_md](../../../includes/ssnoversion-md.md)] Connector?**

![Azure AD change default directory helpsteps](../../../relational-databases/security/encryption/media/azure-ad-change-default-directory-helpsteps.png)

1. Go to the [Azure portal](https://portal.azure.com/).
2. On the upper-right corner of the page, select the settings icon, or your user profile.
3. On the **Directories + subscriptions** page, select **All Directories** to see all the Azure Active Directories that you belong to.
4. You can change your **Startup directory**, or switch to a different directory if you have multiple directories.

    > [!NOTE] 
    > You may not have permissions to actually change the default directory on your Azure subscription. In this case, create the Azure AD service principal within your default directory so that it is in the same directory as the Azure Key Vault used later.

To learn more about Active Directory, read [How Azure subscription are related to Azure Active Directory](/azure/active-directory/fundamentals/active-directory-how-subscriptions-associated-directory)
  
##  <a name="AppendixC"></a> C. Error Code Explanations for [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Connector

**Provider Error Codes:**  
  
Error code  |Symbol  |Description
---------|---------|---------  
0 | scp_err_Success | The operation has succeeded.
1 | scp_err_Failure | The operation has failed.
2 | scp_err_InsufficientBuffer | This error tells engine to allocate more memory for the buffer.
3 | scp_err_NotSupported | The operation is not supported. For example, the key type or algorithm specified is not supported by the EKM provider.
4 | scp_err_NotFound | The specified key or algorithm could not be found by the EKM provider.
5 | scp_err_AuthFailure | The authentication has failed with EKM provider.
6 | scp_err_InvalidArgument | The provided argument is invalid.
7 | scp_err_ProviderError | There is an unspecified error happened in EKM provider that is caught by SQL engine.
401 | acquireToken | Server responded 401 for the request. Make sure the client ID and secret are correct, and the credential string is a concatenation of AAD client ID and secret without hyphens.
404 | getKeyByName | The server responded 404, because the key name was not found. Please make sure the key name exists in your vault.
2049 | scp_err_KeyNameDoesNotFitThumbprint | The key name is too long to fit into SQL engine's thumbprint. The key name must not exceed 26 characters.
2050 | scp_err_PasswordTooShort | The secret string that is the concatenation of AAD client ID and secret is shorter than 32 characters.
2051 | scp_err_OutOfMemory | SQL engine has run out of memory and failed to allocate memory for EKM provider.
2052 | scp_err_ConvertKeyNameToThumbprint | Failed to convert key name to thumbprint.
2053 | scp_err_ConvertThumbprintToKeyName|  Failed to convert thumbprint to key name.
2058 | scp_err_FailureInRegistry|  Failed to perform the operation in registry. SQL Server service account does not have permission to create the registry key.
3000 | ErrorSuccess | The AKV operation has succeeded.
3001 | ErrorUnknown | The AKV operation has failed with an unspecified error.
3002 | ErrorHttpCreateHttpClientOutOfMemory | Cannot create an HttpClient for AKV operation due to out of memory.
3003 | ErrorHttpOpenSession | Cannot open an Http session because of network error.
3004 | ErrorHttpConnectSession | Cannot connect an Http session because of network error.
3005 | ErrorHttpAttemptConnect | Cannot attempt a connect because of network error.
3006 | ErrorHttpOpenRequest | Cannot open a request due to network error.
3007 | ErrorHttpAddRequestHeader | Cannot add request header.
3008 | ErrorHttpSendRequest | Cannot send a request due to network error.
3009 | ErrorHttpGetResponseCode | Cannot get a response code due to network error.
3010 | ErrorHttpResponseCodeUnauthorized | Server responded 401 for the request.
3011 | ErrorHttpResponseCodeThrottled | Server has throttled the request.
3012 | ErrorHttpResponseCodeClientError | The request sent from the connector is invalid. This usually means the key name is invalid or contains invalid characters.
3013 | ErrorHttpResponseCodeServerError | Server responded a response code between 500 and 600.
3014 | ErrorHttpQueryHeader | Cannot query for response header.
3015 | ErrorHttpQueryHeaderOutOfMemoryCopyHeader | Cannot copy the response header due to out of memory.
3016 | ErrorHttpQueryHeaderOutOfMemoryReallocBuffer | Cannot query the response header due to out of memory when reallocating a buffer.
3017 | ErrorHttpQueryHeaderNotFound | Cannot find the query header in the response.
3018 | ErrorHttpQueryHeaderUpdateBufferLength | Cannot update the buffer length when querying the response header.
3019 | ErrorHttpReadData | Cannot read response data due to network error.
3076 | ErrorHttpResourceNotFound | The server responded 404, because the key name was not found. Make sure the key name exists in your vault.
3077 | ErrorHttpOperationForbidden | The server responded 403, because the user doesn't have proper permission to perform the action. Make sure you have the permission for the specified operation. At minimum, the connector requires 'get, list, wrapKey, unwrapKey' permissions to function properly.
3100 | ErrorHttpCreateHttpClientOutOfMemory               | Cannot create a HttpClient for AKV operation due to out of memory.
3101 | ErrorHttpOpenSession                               | Cannot open a Http session due to network error.
3102 | ErrorHttpConnectSession                            | Cannot connect a Http session due to network error.
3103 | ErrorHttpAttemptConnect                            | Cannot attempt a connect due to network error.
3104 | ErrorHttpOpenRequest                               | Cannot open a request due to network error.
3105 | ErrorHttpAddRequestHeader                          | Cannot add request header.
3106 | ErrorHttpSendRequest                               | Cannot send a request due to network error.
3107 | ErrorHttpGetResponseCode                           | Cannot get a response code due to network error.
3108 | ErrorHttpResponseCodeUnauthorized                  | Server responded 401 for the request. Make sure the client Id and secret are correct, and the credential string is a concatenation of AAD client Id and secret without hyphens.
3109 | ErrorHttpResponseCodeThrottled                     | Server has throttled the request.
3110 | ErrorHttpResponseCodeClientError                    | The request is invalid. This usually means the key name is invalid or contains invalid characters.
3111 | ErrorHttpResponseCodeServerError                   | Server responded a response code between 500 and 600.
3112 | ErrorHttpResourceNotFound                          | The server responded 404, because the key name was not found. Please make sure the key name exists in your vault.
3113 | ErrorHttpOperationForbidden                         | The server responded 403, because the user does not have proper permission to perform the action. Please make sure you have the permission for the specified operation. At minimum,'get, wrapKey, unwrapKey' permissions are required.
3114 | ErrorHttpQueryHeader                               | Cannot query for response header.
3115 | ErrorHttpQueryHeaderOutOfMemoryCopyHeader          | Cannot copy the response header due to out of memory.
3116 | ErrorHttpQueryHeaderOutOfMemoryReallocBuffer       | Cannot query the response header due to out of memory when reallocating a buffer.
3117 | ErrorHttpQueryHeaderNotFound                       | Cannot find the query header in the response.
3118 | ErrorHttpQueryHeaderUpdateBufferLength             | Cannot update the buffer length when querying the response header.
3119 | ErrorHttpReadData                                  | Cannot read response data due to network error.
3120 | ErrorHttpGetResponseOutOfMemoryCreateTempBuffer    | Cannot get response body due to out of memory when creating a temp buffer.
3121 | ErrorHttpGetResponseOutOfMemoryGetResultString     | Cannot get response body due to out of memory when get result string.
3122 | ErrorHttpGetResponseOutOfMemoryAppendResponse      | Cannot get response body due to out of memory when appending response.
3200 | ErrorGetAADValuesOutOfMemoryConcatPath | Cannot get Azure Active Directory challenge header values due to out of memory when concatenating the path.
3201 | ErrorGetAADDomainUrlStartPosition | Cannot find the starting position for Azure Active Directory domain Url in malformatted response challenge header.
3202 | ErrorGetAADDomainUrlStopPosition | Cannot find the ending position for Azure Active Directory domain Url in malformatted response challenge header.
3203 | ErrorGetAADDomainUrlMalformatted | The Azure Active Directory response challenge header is malformatted and doesn't contain the AAD domain Url.
3204 | ErrorGetAADDomainUrlOutOfMemoryAlloc | Out of memory when allocating buffer for Azure Active Directory domain Url.
3205 | ErrorGetAADTenantIdOutOfMemoryAlloc | Out of memory when allocating buffer for Azure Active Directory tenantId.
3206 | ErrorGetAKVResourceUrlStartPosition | Cannot find the starting position for Azure Key Vault resource Url in malformatted response challenge header.
3207 | ErrorGetAKVResourceUrlStopPosition | Cannot find the ending position for Azure Key Vault resource Url in malformatted response challenge header.
3208 | ErrorGetAKVResourceUrlOutOfMemoryAlloc | Out of memory when allocating buffer for Azure Key Vault resource Url.
3300 | ErrorGetTokenOutOfMemoryConcatPath | Cannot get token due to out of memory when concatenating the request path.
3301 | ErrorGetTokenOutOfMemoryConcatBody | Cannot get token due to out of memory when concatenating the response body.
3302 | ErrorGetTokenOutOfMemoryConvertResponseString | Cannot get token due to out of memory when converting the response string.
3303 | ErrorGetTokenBadCredentials | Cannot get token due to incorrect credentials. Make sure the credential string or certificate is valid.
3304 | ErrorGetTokenFailedToGetToken | While the credentials are correct, the operation still failed to get a valid token.
3305 | ErrorGetTokenRejected | The token is valid but is rejected by server.
3306 | ErrorGetTokenNotFound | Cannot find the token in response.
3307 | ErrorGetTokenJsonParser | Cannot parse the JSON response of server.
3308 | ErrorGetTokenExtractToken | Cannot extract the token from the JSON response.
3400 | ErrorGetKeyByNameOutOfMemoryConvertResponseString | Cannot get the key by name due to out of memory converting the response string.
3401 | ErrorGetKeyByNameOutOfMemoryConcatPath | Cannot get the key by name due to out of memory when concatenating the path.
3402 | ErrorGetKeyByNameOutOfMemoryConcatHeader | Cannot get the key by name due to out of memory when concatenating the header.
3403 | ErrorGetKeyByNameNoResponse | Cannot get the key by name due to no response from server.
3404 | ErrorGetKeyByNameJsonParser | Cannot get the key by name due to failed to parse the JSON response.
3405 | ErrorGetKeyByNameExtractKeyNode | Cannot get the key by name due to failed to extract the key node from the response.
3406 | ErrorGetKeyByNameExtractKeyId | Cannot get the key by name due to failed to extract the key Id from the response.
3407 | ErrorGetKeyByNameExtractKeyType | Cannot get the key by name due to failed to extract the key type from the response.
3408 | ErrorGetKeyByNameExtractKeyN | Cannot get the key by name due to failed to extract the key N from the response.
3409 | ErrorGetKeyByNameBase64DecodeN | Cannot get the key by name due to failed to Base64 decode the N.
3410 | ErrorGetKeyByNameExtractKeyE | Cannot get the key by name due to failed to extract the key E from the response.
3411 | ErrorGetKeyByNameBase64DecodeE | Cannot get the key by name due to failed to Base64 decode the E.
3412 | ErrorGetKeyByNameExtractKeyUri | Cannot extract the key Uri from the response.
3500 | ErrorBackupKeyOutOfMemoryConvertResponseString | Cannot back up key due to out of memory when converting the response string.
3501 | ErrorBackupKeyOutOfMemoryConcatPath | Cannot back up key due to out of memory when concatenating the path.
3502 | ErrorBackupKeyOutOfMemoryConcatHeader | Cannot back up key due to out of memory when concatenating the request header.
3503 | ErrorBackupKeyNoResponse | Cannot back up key due to no response from server.
3504 | ErrorBackupKeyJsonParser | Cannot back up key due to failed to parse the JSON response.
3505 | ErrorBackupKeyExtractValue | Cannot back up key due to failed to extract the value from JSON response.
3506 | ErrorBackupKeyBase64DecodeValue | Cannot back up key due to failed to Base64 decode the value field.
3600 | ErrorWrapKeyOutOfMemoryConvertResponseString | Cannot wrap key due to out of memory when converting response string.
3601 | ErrorWrapKeyOutOfMemoryConcatPath | Cannot wrap key due to out of memory when concatenating the path.
3602 | ErrorWrapKeyOutOfMemoryConcatHeader | Cannot wrap key due to out of memory when concatenating the header.
3603 | ErrorWrapKeyOutOfMemoryConcatBody | Cannot wrap key due to out of memory when concatenating the body.
3604 | ErrorWrapKeyOutOfMemoryConvertEncodedBody | Cannot wrap key due to out of memory when converting the encoded body.
3605 | ErrorWrapKeyBase64EncodeKey | Cannot wrap key due to failed to Base64 encode the key.
3606 | ErrorWrapKeyBase64DecodeValue | Cannot wrap key due to failed to Base64 decode the response value.
3607 | ErrorWrapKeyJsonParser | Cannot wrap key due to failed to parse the JSON response.
3608 | ErrorWrapKeyExtractValue | Cannot wrap key due to failed to extract value from response.
3609 | ErrorWrapKeyNoResponse | Cannot wrap key due to no response from server.
3700 | ErrorUnwrapKeyOutOfMemoryConvertResponseString | Cannot unwrap key due to out of memory when converting response string.
3701 | ErrorUnwrapKeyOutOfMemoryConcatPath | Cannot unwrap key due to out of memory when concatenating the path.
3702 | ErrorUnwrapKeyOutOfMemoryConcatHeader | Cannot unwrap key due to out of memory when concatenating the header.
3703 | ErrorUnwrapKeyOutOfMemoryConcatBody | Cannot unwrap key due to out of memory when concatenating the body.
3704 | ErrorUnwrapKeyOutOfMemoryConvertEncodedBody | Cannot unwrap key due to out of memory when converting the encoded body.
3705 | ErrorUnwrapKeyBase64EncodeKey | Cannot unwrap key due to failed to Base64 encode the key.
3706 | ErrorUnwrapKeyBase64DecodeValue | Cannot unwrap key due to failed to Base64 decode the response value.
3707 | ErrorUnwrapKeyJsonParser | Cannot unwrap key due to failed to extract value from response.
3708 | ErrorUnwrapKeyExtractValue | Cannot unwrap key due to failed to extract value from response.
3709 | ErrorUnwrapKeyNoResponse | Cannot unwrap key due to no response from server.
3800 | ErrorSecretAuthParamsGetRequestBody | Error creating request body using AAD clientId and secret.
3801 | ErrorJWTTokenCreateHeader | Error creating JWT token header for authentication with AAD.
3802 | ErrorJWTTokenCreatePayloadGUID | Error creating GUID for JWT token payload for authentication with AAD.
3803 | ErrorJWTTokenCreatePayload | Error creating JWT token payload for authentication with AAD.
3804 | ErrorJWTTokenCreateSignature | Error creating JWT token signature for authentication with AAD.
3805 | ErrorJWTTokenSignatureHashAlg | Error getting SHA256 hash algorithm for authentication with AAD.
3806 | ErrorJWTTokenSignatureHash | Error creating SHA256 hash for JWT token authentication with AAD.
3807 | ErrorJWTTokenSignatureSignHash | Error signing JWT token hash for authentication with AAD.
3808 | ErrorJWTTokenCreateToken | Error creating JWT token for authentication with AAD.
3809 | ErrorPfxCertAuthParamsImportPfx | Error importing Pfx certificate for authentication with AAD.
3810 | ErrorPfxCertAuthParamsGetThumbprint | Error getting thumbprint from Pfx certificate for authentication with AAD.
3811 | ErrorPfxCertAuthParamsGetPrivateKey | Error getting private key from Pfx certificate for authentication with AAD.
3812 | ErrorPfxCertAuthParamsSignAlg | Error getting RSA signing algorithm for Pfx certificate authentication with AAD.
3813 | ErrorPfxCertAuthParamsImportForSign | Error importing Pfx private key for RSA signing for authentication with AAD.
3814 | ErrorPfxCertAuthParamsCreateRequestBody | Error creating request body from Pfx certificate for authentication with AAD.
3815 | ErrorPEMCertAuthParamsGetThumbprint | Error Base64 decoding Thumbprint for authentication with AAD.
3816 | ErrorPEMCertAuthParamsGetPrivateKey | Error getting RSA private key from PEM for authentication with AAD.
3817 | ErrorPEMCertAuthParamsSignAlg | Error getting RSA signing algorithm for PEM private key authentication with AAD.
3818 | ErrorPEMCertAuthParamsImportForSign | Error importing PEM private key for RSA signing for authentication with AAD.
3819 | ErrorPEMCertAuthParamsCreateRequestBody | Error creating request body from PEM private key for authentication with AAD.
3820 | ErrorLegacyPrivateKeyAuthParamsSignAlg | Error getting RSA signing algorithm for Legacy private key authentication with AAD.
3821 | ErrorLegacyPrivateKeyAuthParamsImportForSign | Error importing Legacy private key for RSA signing for authentication with AAD.
3822 | ErrorLegacyPrivateKeyAuthParamsCreateRequestBody        | Error creating request body from Legacy private key for authentication with AAD.
3900 | ErrorAKVDoesNotExist | Error internet name not resolved. This typically indicates the Azure Key Vault is deleted.
4000 | ErrorCreateKeyVaultRetryManagerOutOfMemory | Cannot create a RetryManager for AKV operation due to out of memory.

If you don't see your error code in this table, here are some other reasons the error may be happening:
  
- You may not have Internet access and cannot access your Azure Key Vault. Check your Internet connection.  
  
- The Azure Key Vault service may be down. Try again at another time.  
  
- You may have dropped the asymmetric key from Azure Key Vault or [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)]. Restore the key.  
  
- If you receive a "Cannot load library" error, make sure you have the appropriate version of the Visual Studio C++ redistributable installed based on the version of SQL Server that you are running. The table below specifies which version to install from the Microsoft Download Center.

The Windows event log also logs errors associated with the SQL Server Connector, which can help with additional context on why the error is actually happening. The source in the Windows Application Event Log will be "SQL Server Connector for Microsoft Azure Key Vault".
  
#### C++ Runtime Libraries for SQL Server Connector 1.0.5.0
SQL Server Version  |Redistributable Install Link if using SQL Server Connector 1.0.5.0
---------|---------
2008, 2008 R2, 2012, 2014 | [Visual C++ Redistributable Packages for Visual Studio 2013](https://www.microsoft.com/download/details.aspx?id=40784)
2016, 2017, 2019 | [Visual C++ Redistributable for Visual Studio 2015](https://www.microsoft.com/download/details.aspx?id=48145)

#### C++ Runtime Libraries for SQL Server Connector 1.0.4.0
SQL Server Version  |Redistributable Install Link if using SQL Server Connector 1.0.4.0
---------|---------
2008, 2008 R2, 2012, 2014, 2016, 2017, 2019 | [Visual C++ Redistributable Packages for Visual Studio 2013](https://www.microsoft.com/download/details.aspx?id=40784)

## Additional References

 More About Extensible Key Management:  
  
- [Extensible Key Management &#40;EKM&#41;](../../../relational-databases/security/encryption/extensible-key-management-ekm.md)  
  
 SQL Encryptions supporting EKM:  
  
- [Enable TDE on SQL Server Using EKM](../../../relational-databases/security/encryption/enable-tde-on-sql-server-using-ekm.md)  
  
- [Backup Encryption](../../../relational-databases/backup-restore/backup-encryption.md)  
  
- [Create an Encrypted Backup](../../../relational-databases/backup-restore/create-an-encrypted-backup.md)  
  
 Related [!INCLUDE[tsql](../../../includes/tsql-md.md)] Commands:  
  
- [sp_configure &#40;Transact-SQL&#41;](../../../relational-databases/system-stored-procedures/sp-configure-transact-sql.md)  
  
- [CREATE CRYPTOGRAPHIC PROVIDER &#40;Transact-SQL&#41;](../../../t-sql/statements/create-cryptographic-provider-transact-sql.md)  
  
- [CREATE CREDENTIAL &#40;Transact-SQL&#41;](../../../t-sql/statements/create-credential-transact-sql.md)  
  
- [CREATE ASYMMETRIC KEY &#40;Transact-SQL&#41;](../../../t-sql/statements/create-asymmetric-key-transact-sql.md)  
  
- [CREATE SYMMETRIC KEY &#40;Transact-SQL&#41;](../../../t-sql/statements/create-symmetric-key-transact-sql.md)  
  
- [CREATE LOGIN &#40;Transact-SQL&#41;](../../../t-sql/statements/create-login-transact-sql.md)  
  
- [ALTER LOGIN &#40;Transact-SQL&#41;](../../../t-sql/statements/alter-login-transact-sql.md)  
  
 Azure Key Vault documentation:  
  
- [What is Azure Key Vault?](/azure/key-vault/general/basic-concepts)  
  
- [Get Started with Azure Key Vault](/azure/key-vault/general/overview)  
  
- PowerShell [Azure Key Vault Cmdlets](/powershell/module/azurerm.keyvault/) reference  
  
## See Also

 [Extensible Key Management Using Azure Key Vault](../../../relational-databases/security/encryption/extensible-key-management-using-azure-key-vault-sql-server.md)  
 [Use SQL Server Connector with SQL Encryption Features](../../../relational-databases/security/encryption/use-sql-server-connector-with-sql-encryption-features.md)  
 [EKM provider enabled Server Configuration Option](../../../database-engine/configure-windows/ekm-provider-enabled-server-configuration-option.md)  
 [Setup Steps for Extensible Key Management Using the Azure Key Vault](../../../relational-databases/security/encryption/setup-steps-for-extensible-key-management-using-the-azure-key-vault.md)

For additional sample scripts, see the blog at [SQL Server Transparent Data Encryption and Extensible Key Management with Azure Key Vault](https://techcommunity.microsoft.com/t5/sql-server/intro-sql-server-transparent-data-encryption-and-extensible-key/ba-p/1427549)
