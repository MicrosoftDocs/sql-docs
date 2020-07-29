---
title: "Key Vault authentication with Managed Identities | Microsoft Docs"
ms.custom: ""
ms.date: "07/31/2020"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
ms.assetid: 
author: peterbae
ms.author: v-hyba
---
# Key Vault authentication with Managed Identities

[!INCLUDE[Driver_JDBC_Download](../../includes/driver_jdbc_download.md)]

Starting with JDBC Driver **v8.3.0**, the driver added support to authenticate to Azure Key Vaults using Managed Identities.

[Always Encrypted](https://docs.microsoft.com/sql/relational-databases/security/encryption/always-encrypted-database-engine?view=sql-server-ver15) is a security feature introduced in SQL Server 2016 to ensure the data stored in a database remains encrypted at all times during SQL Server query processing. It allows clients to encrypt sensitive data, such as credit card numbers and national identification numbers, inside the client application and never reveal the encryption key to the database engine.

The JDBC Driver supports Always Encrypted as of version 6.0 (or higher). See [Using Always Encrypted with the JDBC driver](https://docs.microsoft.com/sql/connect/jdbc/using-always-encrypted-with-the-jdbc-driver?view=sql-server-ver15) for description on how to use Always Encrypted with the JDBC driver.

[Azure Key Vault](https://docs.microsoft.com/azure/key-vault/basic-concepts) is a convenient option to store and manage column master keys for Always Encrypted. If the application is hosted in Azure, the user can use [Managed Identities](https://docs.microsoft.com/azure/active-directory/managed-identities-azure-resources/overview) to authenticate to the Azure Key Vault, thus eliminating the need to provide and expose any credentials in the code. 

## Connection properties for Key Vault authentication with Managed Identities

For JDBC Driver 8.3.0 and later, the driver introduced the following connection properties:

| ConnectionProperty    || Values||
| ---|---|---|----|
| keyStoreAuthentication| KeyVaultClientSecret   |KeyVaultManagedIdentity |JavaKeyStorePassword |  
| keyStorePrincipalId   | \<Azure AD Application Client ID\>    | \<Azure AD Application object ID\> (optional)| n/a |
| keyStoreSecret        | \<Azure AD Application Client Secret\>|n/a|\<secret/password for the Java Key Store\> |

The following examples show how the connection properties are used in a connection string.

## Use Managed Identity to authenticate to AKV
```
"jdbc:sqlserver://<server>:<port>;columnEncryptionSetting=Enabled;keyStoreAuthentication=keyStoreManagedIdentity;"
```
## Use Managed Identity and the principal ID to authenticate to AKV
```
"jdbc:sqlserver://<server>:<port>;columnEncryptionSetting=Enabled;keyStoreAuthentication=keyStoreManagedIdentity;keyStorePrincipal=<principalId>"
```
## Use clientId and clientSecret to authentication to AKV
```
"jdbc:sqlserver://<server>:<port>;columnEncryptionSetting=Enabled;keyStoreAuthentication=keyStoreSecret;keyStorePrincipalId=<clientId>;keyStoreSecret=<clientSecret>"
```
Users are encouraged to use these connection properties to specify the type of authentication used for the Key Stores instead of using the `SQLServerColumnEncryptionAzureKeyVaultProvider` interface as this interface will be deprecated in a future release.

Note: Previously added connection properties `keyVaultProviderClientId` and `keyVaultProviderClientKey` are deprecated and replaced by the connection properties described above and will be removed in a future release.

For more information about using Azure Key Vaults, see [Azure Key Vault documentation](https://docs.microsoft.com/azure/key-vault/).

For information on how to configure Managed Identities, see [Configure managed identities for Azure resources on a VM using the Azure portal](https://docs.microsoft.com/azure/active-directory/managed-identities-azure-resources/qs-configure-portal-windows-vm).


## See also

[Azure Key Vault sample](../../connect/jdbc/azure-key-vault-sample-version-7.0.md)
