---
title: "Always Encrypted | Microsoft Docs"
description: Overview of Always Encrypted that supports transparent client-side encryption and confidential computing in SQL Server and Azure SQL Database
ms.custom: ""
ms.date: "10/30/2019"
ms.prod: sql
ms.reviewer: vanto
ms.technology: security
ms.topic: conceptual
helpviewer_keywords: 
  - "encryption [SQL Server], Always Encrypted"
  - "Always Encrypted"
  - "TCE Always Encrypted"
  - "Always Encrypted, about"
  - "SQL13.SWB.COLUMNMASTERKEY.CLEANUP.F1"
ms.assetid: 54757c91-615b-468f-814b-87e5376a960f
author: jaszymas
ms.author: jaszymas
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Always Encrypted
[!INCLUDE [SQL Server Azure SQL Database](../../../includes/applies-to-version/sql-asdb.md)]

  ![Always Encrypted](../../../relational-databases/security/encryption/media/always-encrypted.png "Always Encrypted")  
  
 Always Encrypted is a feature designed to protect sensitive data, such as credit card numbers or national identification numbers (for example, U.S. social security numbers), stored in [!INCLUDE[ssSDSFull](../../../includes/sssdsfull-md.md)] or [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] databases. Always Encrypted allows clients to encrypt sensitive data inside client applications and never reveal the encryption keys to the [!INCLUDE[ssDE](../../../includes/ssde-md.md)] ([!INCLUDE[ssSDS](../../../includes/sssds-md.md)] or [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)]). As a result, Always Encrypted provides a separation between those who own the data and can view it, and those who manage the data but should have no access. By ensuring on-premises database administrators, cloud database operators, or other high-privileged unauthorized users, can't access the encrypted data, Always Encrypted enables customers to confidently store sensitive data outside of their direct control. This allows organizations to store their data in Azure, and enable delegation of on-premises database administration to third parties, or to reduce security clearance requirements for their own DBA staff.

 Always Encrypted provides confidential computing capabilities by enabling the [!INCLUDE[ssDE](../../../includes/ssde-md.md)] to process some queries on encrypted data, while preserving the confidentiality of the data and providing the above security benefits. In [!INCLUDE[ssSQL15](../../../includes/sssql15-md.md)], [!INCLUDE[sssSQLv14](../../../includes/sssqlv14-md.md)] and in [!INCLUDE[ssSDSFull](../../../includes/sssdsfull-md.md)], Always Encrypted supports equality comparison via deterministic encryption. See [Selecting Deterministic or Randomized Encryption](#selecting--deterministic-or-randomized-encryption). 

  > [!NOTE] 
  > In [!INCLUDE[sql-server-2019](../../../includes/sssqlv15-md.md)], secure enclaves substantially extend confidential computing capabilities of Always Encrypted with pattern matching, other comparison operators and in-place encryption. See [Always Encrypted with secure enclaves](always-encrypted-enclaves.md).

 Always Encrypted makes encryption transparent to applications. An Always Encrypted-enabled driver installed on the client computer achieves this by automatically encrypting and decrypting sensitive data in the client application. The driver encrypts the data in sensitive columns before passing the data to the [!INCLUDE[ssDE](../../../includes/ssde-md.md)], and automatically rewrites queries so that the semantics to the application are preserved. Similarly, the driver transparently decrypts data, stored in encrypted database columns, contained in query results.  
  
 Always Encrypted is available in all editions of [!INCLUDE[ssSDSFull](../../../includes/sssdsfull-md.md)], starting with [!INCLUDE[ssSQL15](../../../includes/sssql15-md.md)] and all service tiers of [!INCLUDE[ssSDS](../../../includes/sssds-md.md)]. (Prior to [!INCLUDE[ssSQL15_md](../../../includes/sssql15-md.md)] SP1, Always Encrypted was limited to the Enterprise Edition.) For a Channel 9 presentation that includes Always Encrypted, see [Keeping Sensitive Data Secure with Always Encrypted](https://channel9.msdn.com/events/DataDriven/SQLServer2016/AlwaysEncrypted).  

  
## Typical Scenarios  
  
### Client and data on-premises  
 A customer has a client application and [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] both running on-premises, at their business location. The customer wants to hire an external vendor to administer [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)]. In order to protect sensitive data stored in [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)], the customer uses Always Encrypted to ensure the separation of duties between database administrators and application administrators. The customer stores plaintext values of Always Encrypted keys in a trusted key store, which the client application can access. [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] administrators have no access to the keys and, therefore, are unable to decrypt sensitive data stored in [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)].  
  
### Client on-premises with data in Azure  
 A customer has an on-premises client application at their business location. The application operates on sensitive data stored in a database hosted in Azure ([!INCLUDE[ssSDS](../../../includes/sssds-md.md)] or [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] running in a virtual machine on Microsoft Azure). The customer uses Always Encrypted and stores Always Encrypted keys in a trusted key store hosted on-premises, to ensure [!INCLUDE[msCoName](../../../includes/msconame-md.md)] cloud administrators have no access to sensitive data.  
  
### Client and Data in Azure  
 A customer has a client application, hosted in Microsoft Azure (for example, in a worker role or a web role), which operates on sensitive data stored in a database hosted in Azure (SQL Database or SQL Server running in a virtual machine on Microsoft Azure). Although Always Encrypted doesn't provide complete isolation of data from cloud administrators, as both the data and keys are exposed to cloud administrators of the platform hosting the client tier, the customer still benefits from reducing the security attack surface area (the data is always encrypted in the database).  
 
## How it Works

You can configure Always Encrypted for individual database columns containing your sensitive data. When setting up encryption for a column, you specify the information about the encryption algorithm and cryptographic keys used to protect the data in the column. Always Encrypted uses two types of keys: column encryption keys and column master keys. A column encryption key is used to encrypt data in an encrypted column. A column master key is a key-protecting key that encrypts one or more column encryption keys. 

The Database Engine stores encryption configuration for each column in database metadata. Note, however, the Database Engine never stores or uses the keys of either type in plaintext. It only stores encrypted values of column encryption keys and the information about the location of column master keys, which are stored in external trusted key stores, such as Azure Key Vault, Windows Certificate Store on a client machine, or a hardware security module.

To access data stored in an encrypted column in plaintext, an application must use an Always Encrypted enabled client driver. When an application issues a parameterized query, the driver transparently collaborates with the Database Engine to determine which parameters target encrypted columns and, thus, should be encrypted. For each parameter that needs to be encrypted, the driver obtains the information about the encryption algorithm and the encrypted value of the column encryption key for the column, the parameter targets, as well as the location of its corresponding column master key.

Next, the driver contacts the key store, containing the column master key, in order to decrypt the encrypted column encryption key value and then, it uses the plaintext column encryption key to encrypt the parameter. The resultant plaintext column encryption key is cached to reduce the number of round trips to the key store on subsequent uses of the same column encryption key. The driver substitutes the plaintext values of the parameters targeting encrypted columns with their encrypted values, and it sends the query to the server for processing.

The server computes the result set, and for any encrypted columns included in the result set, the driver attaches the encryption metadata for the column, including the information about the encryption algorithm and the corresponding keys. The driver first tries to find the plaintext column encryption key in the local cache, and only makes a round to the column master key if it can't find the key in the cache. Next, the driver decrypts the results and returns plaintext values to the application.

 A client driver interacts with a key store, containing a column master key, using a column master key store provider, which is a client-side software component that encapsulates a key store containing the column master key. Providers for common types of key stores are available in client-side driver libraries from Microsoft or as standalone downloads. You can also implement your own provider. Always Encrypted capabilities, including built-in column master key store providers vary by a driver library and its version. 

For details of how to develop applications using Always Encrypted with particular client drivers, see [Develop applications using Always Encrypted](always-encrypted-client-development.md).

## Remarks

Encryption and decryption occurs via the client driver. This means that some actions that occur only server-side will not work when using Always Encrypted. Examples include copying data from one columng to another via an UPDATE, BULK INSERT(T-SQL), SELECT INTO, INSERT..SELECT. 

Here's an example of an UPDATE that attempts to move data from an encrypted column to an unencrypted column without returning a result set to the client: 

```sql
update dbo.Patients set testssn = SSN
```

If SSN is a column encrypted using Always Encrypted, the above update statement will fail with an error similar to:

```
Msg 206, Level 16, State 2, Line 89
Operand type clash: char(11) encrypted with (encryption_type = 'DETERMINISTIC', encryption_algorithm_name = 'AEAD_AES_256_CBC_HMAC_SHA_256', column_encryption_key_name = 'CEK_1', column_encryption_key_database_name = 'ssn') collation_name = 'Latin1_General_BIN2' is incompatible with char
```

To successfully update the column, do the following:

1. SELECT the data out of the SSN column, and store it as a result set in the application. This will allow for the application (client *driver*) to decrypt the column.
2. INSERT the data from the result set into SQL Server. 

 >[!IMPORTANT]
 > In this scenario, the data will be unencrypted when sent back to the server because the destination column is a regular varchar that does not accept encrypted data. 
  
## <a name="selecting--deterministic-or-randomized-encryption"></a> Selecting Deterministic or Randomized Encryption  
 The Database Engine never operates on plaintext data stored in encrypted columns, but it still supports some queries on encrypted data, depending on the encryption type for the column. Always Encrypted supports two types of encryption: randomized encryption and deterministic encryption.  
  
- Deterministic encryption always generates the same encrypted value for any given plain text value. Using deterministic encryption allows point lookups, equality joins, grouping and indexing on encrypted columns. However, it may also allow unauthorized users to guess information about encrypted values by examining patterns in the encrypted column, especially if there's a small set of possible encrypted values, such as True/False, or North/South/East/West region. Deterministic encryption must use a column collation with a binary2 sort order for character columns.

- Randomized encryption uses a method that encrypts data in a less predictable manner. Randomized encryption is more secure, but prevents searching, grouping, indexing, and joining on encrypted columns.

Use deterministic encryption for columns that will be used as search or grouping parameters. For example, a government ID number. Use randomized encryption for data such as confidential investigation comments, which aren't grouped with other records and aren't used to join tables.
For details on Always Encrypted cryptographic algorithms, see [Always Encrypted cryptography](../../../relational-databases/security/encryption/always-encrypted-cryptography.md).

## Configuring Always Encrypted

 The initial setup of Always Encrypted in a database involves generating Always Encrypted keys, creating key metadata, configuring encryption properties of selected database columns, and/or encrypting data that may already exist in columns that need to be encrypted. Please note that some of these tasks are not supported in Transact-SQL and require the use of client-side tools. As Always Encrypted keys and protected sensitive data are never revealed in plaintext to the server, the Database Engine can't be involved in key provisioning and perform data encryption or decryption operations. You can use SQL Server Management Studio or PowerShell to accomplish such tasks. 

|Task|SSMS|PowerShell|T-SQL|
|:---|:---|:---|:---
|Provisioning column master keys, column encryption keys and encrypted column encryption keys with their corresponding column master keys.|Yes|Yes|No|
|Creating key metadata in the database.|Yes|Yes|Yes|
|Creating new tables with encrypted columns|Yes|Yes|Yes|
|Encrypting existing data in selected database columns|Yes|Yes|No|

> [!NOTE]
> [Always Encrypted with secure enclaves](always-encrypted-enclaves.md), introduced in [!INCLUDE[sql-server-2019](../../../includes/sssqlv15-md.md)], does support encrypting existing data using Transact-SQL. It also eliminates the need to move the data outside of the data for cryptographic operations.

> [!NOTE]
> Make sure you run key provisioning or data encryption tools in a secure environment, on a computer that is different from the computer hosting your database. Otherwise, sensitive data or the keys could leak to the server environment, which would reduce the benefits of the using Always Encrypted.  

For details on configuring Always Encrypted see:
- [Configure Always Encrypted using SSMS](../../../relational-databases/security/encryption/configure-always-encrypted-using-sql-server-management-studio.md)
- [Configure Always Encrypted using PowerShell](../../../relational-databases/security/encryption/configure-always-encrypted-using-powershell.md)

## Getting Started with Always Encrypted

Use the [Always Encrypted Wizard](../../../relational-databases/security/encryption/always-encrypted-wizard.md) to quickly start using Always Encrypted. The wizard will provision the required keys and configure encryption for selected columns. If the columns you're setting encryption for already contain some data, the wizard will encrypt the data. The following example demonstrates the process for encrypting a column.

> [!NOTE]  
>  For a video that includes using the wizard, see [Getting Started with Always Encrypted with SSMS](https://channel9.msdn.com/Shows/Data-Exposed/Getting-Started-with-Always-Encrypted-with-SSMS).

1.	Connect to an existing database that contains tables with columns you wish to encrypt using the **Object Explorer** of Management Studio, or create a new database, create one or more tables with columns to encrypt, and connect to it.
2.	Right-click your database, point to **Tasks**, and then click **Encrypt Columns** to open the **Always Encrypted Wizard**.
3.	Review the **Introduction** page, and then click **Next**.
4.	On the **Column Selection** page, expand the tables, and select the columns that you want to encrypt.
5.	For each column selected for encryption, set the **Encryption Type** to either *Deterministic* or *Randomized*.
6.	For each column selected for encryption, select an **Encryption Key**. If you have not previously created any encryption keys for this database, select the default choice of a new autogenerated key, and then click **Next**.
7.	On the **Master Key Configuration** page, select a location to store the new key, and select a master key source, and then click **Next**.
8.	On the **Validation** page, choose whether to run the script immediately or create a PowerShell script, and then click **Next**.
9.	On the **Summary** page, review the options you've selected, and then click **Finish**. Close the wizard when completed.

  
## Feature Details  
  
-   Queries can perform equality comparison on columns encrypted using deterministic encryption, but no other operations (for example, greater/less than, pattern matching using the LIKE operator, or arithmetical operations).  
  
-   Queries on columns encrypted by using randomized encryption can't perform operations on any of those columns. Indexing columns encrypted using randomized encryption isn't supported.  
 > [!NOTE] 
 > [Always Encrypted with secure enclaves](always-encrypted-enclaves.md), introduced in [!INCLUDE[sql-server-2019](../../../includes/sssqlv15-md.md)], addresses the above limitation by enabling pattern matching, comparison operators and indexing on columns using randomized encryption.

-   A column encryption key can have up to two different encrypted values, each encrypted with a different column master key. This facilitates column master key rotation.  
  
-   Deterministic encryption requires a column to have one of the [*binary2* collations](../../../relational-databases/collations/collation-and-unicode-support.md).  

-   After changing the definition of an encrypted object, execute [sp_refresh_parameter_encryption](../../../relational-databases/system-stored-procedures/sp-refresh-parameter-encryption-transact-sql.md) to update the Always Encrypted metadata for the object.
  
Always Encrypted isn't supported for the columns with the below characteristics. For example, if any of the following conditions apply to the column, the `ENCRYPTED WITH` clause can't be used in `CREATE TABLE/ALTER TABLE` for a column:  
  
-   Columns using one of the following data types: `xml`, `timestamp`/`rowversion`, `image`, `ntext`, `text`, `sql_variant`, `hierarchyid`, `geography`, `geometry`, alias, user defined-types.  
- `FILESTREAM` columns  
- Columns with the `IDENTITY` property.  
- Columns with `ROWGUIDCOL` property.  
- String (`varchar`, `char`, etc.) columns with non-bin2 collations.  
- Columns that are keys for clustered and nonclustered indices when using randomized encryption (deterministic encryption is supported).
- Columns that are keys for fulltext indices when using randomized encryption (deterministic encryption is supported).  
- Computed columns.
- Columns referenced by computed columns (when the expression does unsupported operations for Always Encrypted).  
- Sparse column set.  
- Columns that are referenced by statistics when using randomized encryption (deterministic encryption is supported).  
- Columns using alias types.  
- Partitioning columns.  
- Columns with default constraints.  
- Columns referenced by unique constraints when using randomized encryption (deterministic encryption is supported).  
- Primary key columns when using randomized encryption (deterministic encryption is supported).  
- Referencing columns in foreign key constraints when using randomized encryption or when using deterministic encryption, if the referenced and referencing columns use different keys or algorithms.  
- Columns referenced by check constraints.  
- Columns captured/tracked using change data capture.  
- Primary key columns on tables that have change tracking.  
- Columns that are masked (using Dynamic Data Masking).  
- Columns in Stretch Database tables. (Tables with columns encrypted with Always Encrypted can be enabled for Stretch.)  
- Columns in external (PolyBase) tables (note: using external tables and tables with encrypted columns in the same query is supported).  
- Table-valued parameters targeting encrypted columns aren't supported.  

The following clauses can't be used for encrypted columns:

- `FOR XML`
- `FOR JSON PATH`

The following features don't work on encrypted columns:

- Transactional or merge replication
- Distributed queries (linked servers, `OPENROWSET`(T-SQL), `OPENDATASOURCE`(T-SQL))

Tool Requirements

- SQL Server Management Studio version 18 or higher is recommended to run queries that decrypt the results retrieved from encrypted columns or insert, update, or filter encrypted columns. 
- Requires `sqlcmd` version 13.1 or higher, which is available from the [Download Center](https://go.microsoft.com/fwlink/?LinkID=825643).

  
## Database Permissions  
 There are four permissions for Always Encrypted:  
  
*  `ALTER ANY COLUMN MASTER KEY` (Required to create and delete a column master key.)  
  
*  `ALTER ANY COLUMN ENCRYPTION KEY` (Required to create and delete a column encryption key.) 
  
*  `VIEW ANY COLUMN MASTER KEY DEFINITION` (Required to access and read the metadata of the column master keys to manage keys or query encrypted columns.)  
  
*  `VIEW ANY COLUMN ENCRYPTION KEY DEFINITION` (Required to access and read the metadata of the column encryption key to manage keys or query encrypted columns.)  
  
 The following table summarizes the permissions required for common actions.  
  
|Scenario|<code>ALTER ANY COLUMN MASTER KEY</code>|<code>ALTER ANY COLUMN ENCRYPTION KEY</code>|<code>VIEW ANY COLUMN MASTER KEY DEFINITION</code>|<code>VIEW ANY COLUMN ENCRYPTION KEY DEFINITION</code>|  
|--------------|-----------------------------------|---------------------------------------|---------------------------------------------|-------------------------------------------------|  
|Key management (creating/changing/reviewing key metadata in the database)|X|X|X|X|  
|Querying encrypted columns|||X|X|  
  
 **Important notes:**  
  
-   The permissions apply to actions using [!INCLUDE[tsql](../../../includes/tsql-md.md)], [!INCLUDE[ssManStudio](../../../includes/ssmanstudio-md.md)] (dialog boxes and wizard), or PowerShell.  
  
-   The two *VIEW* permissions are required when selecting encrypted columns, even if the user doesn't have permission to decrypt the columns.  
  
-   In [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)], both *VIEW* permissions are granted by default to the `public` fixed database role. A database administrator may choose to revoke (or deny) the *VIEW* permissions to the `public` role and grant them to specific roles or users to implement more restricted control.  
  
-   In [!INCLUDE[ssSDS](../../../includes/sssds-md.md)], the *VIEW* permissions aren't granted by default to the `public` fixed database role. This enables certain existing, legacy tools (using older versions of DacFx) to work properly. Consequently, to work with encrypted columns (even if not decrypting them) a database administrator must explicitly grant the two *VIEW* permissions.  

  
## Example  
 The following [!INCLUDE[tsql](../../../includes/tsql-md.md)] creates column master key metadata, column encryption key metadata, and a table with encrypted columns. For information how to create the keys, referenced in the metadata, see:
- [Configure Always Encrypted using SSMS](../../../relational-databases/security/encryption/configure-always-encrypted-using-sql-server-management-studio.md)
- [Configure Always Encrypted using PowerShell](../../../relational-databases/security/encryption/configure-always-encrypted-using-powershell.md)

  
```sql  
CREATE COLUMN MASTER KEY MyCMK  
WITH (  
     KEY_STORE_PROVIDER_NAME = 'MSSQL_CERTIFICATE_STORE',   
     KEY_PATH = 'Current User/Personal/f2260f28d909d21c642a3d8e0b45a830e79a1420'  
   );  
---------------------------------------------  
CREATE COLUMN ENCRYPTION KEY MyCEK   
WITH VALUES  
(  
    COLUMN_MASTER_KEY = MyCMK,   
    ALGORITHM = 'RSA_OAEP',   
    ENCRYPTED_VALUE = 0x01700000016C006F00630061006C006D0061006300680069006E0065002F006D0079002F003200660061006600640038003100320031003400340034006500620031006100320065003000360039003300340038006100350064003400300032003300380065006600620063006300610031006300284FC4316518CF3328A6D9304F65DD2CE387B79D95D077B4156E9ED8683FC0E09FA848275C685373228762B02DF2522AFF6D661782607B4A2275F2F922A5324B392C9D498E4ECFC61B79F0553EE8FB2E5A8635C4DBC0224D5A7F1B136C182DCDE32A00451F1A7AC6B4492067FD0FAC7D3D6F4AB7FC0E86614455DBB2AB37013E0A5B8B5089B180CA36D8B06CDB15E95A7D06E25AACB645D42C85B0B7EA2962BD3080B9A7CDB805C6279FE7DD6941E7EA4C2139E0D4101D8D7891076E70D433A214E82D9030CF1F40C503103075DEEB3D64537D15D244F503C2750CF940B71967F51095BFA51A85D2F764C78704CAB6F015EA87753355367C5C9F66E465C0C66BADEDFDF76FB7E5C21A0D89A2FCCA8595471F8918B1387E055FA0B816E74201CD5C50129D29C015895CD073925B6EA87CAF4A4FAF018C06A3856F5DFB724F42807543F777D82B809232B465D983E6F19DFB572BEA7B61C50154605452A891190FB5A0C4E464862CF5EFAD5E7D91F7D65AA1A78F688E69A1EB098AB42E95C674E234173CD7E0925541AD5AE7CED9A3D12FDFE6EB8EA4F8AAD2629D4F5A18BA3DDCC9CF7F352A892D4BEBDC4A1303F9C683DACD51A237E34B045EBE579A381E26B40DCFBF49EFFA6F65D17F37C6DBA54AA99A65D5573D4EB5BA038E024910A4D36B79A1D4E3C70349DADFF08FD8B4DEE77FDB57F01CB276ED5E676F1EC973154F86  
);  
---------------------------------------------  
CREATE TABLE Customers (  
    CustName nvarchar(60)   
        COLLATE  Latin1_General_BIN2 ENCRYPTED WITH (COLUMN_ENCRYPTION_KEY = MyCEK,  
        ENCRYPTION_TYPE = RANDOMIZED,  
        ALGORITHM = 'AEAD_AES_256_CBC_HMAC_SHA_256'),   
    SSN varchar(11)   
        COLLATE  Latin1_General_BIN2 ENCRYPTED WITH (COLUMN_ENCRYPTION_KEY = MyCEK,  
        ENCRYPTION_TYPE = DETERMINISTIC ,  
        ALGORITHM = 'AEAD_AES_256_CBC_HMAC_SHA_256'),   
    Age int NULL  
);  
GO  
  
```  
## See Also  
- [Configure Always Encrypted using SSMS](../../../relational-databases/security/encryption/configure-always-encrypted-using-sql-server-management-studio.md)   
- [Configure Always Encrypted using PowerShell](../../../relational-databases/security/encryption/configure-always-encrypted-using-powershell.md)   
- [Develop applications using Always Encrypted](always-encrypted-client-development.md) 
- [Configure column encryption using Always Encrypted Wizard](always-encrypted-wizard.md)
- [Always Encrypted cryptography](../../../relational-databases/security/encryption/always-encrypted-cryptography.md)   
- [CREATE COLUMN MASTER KEY &#40;Transact-SQL&#41;](../../../t-sql/statements/create-column-master-key-transact-sql.md)   
- [CREATE COLUMN ENCRYPTION KEY &#40;Transact-SQL&#41;](../../../t-sql/statements/create-column-encryption-key-transact-sql.md)   
- [CREATE TABLE &#40;Transact-SQL&#41;](../../../t-sql/statements/create-table-transact-sql.md)   
- [column_definition &#40;Transact-SQL&#41;](../../../t-sql/statements/alter-table-column-definition-transact-sql.md)   
- [sys.column_encryption_keys  &#40;Transact-SQL&#41;](../../../relational-databases/system-catalog-views/sys-column-encryption-keys-transact-sql.md)  
- [sys.column_encryption_key_values &#40;Transact-SQL&#41;](../../../relational-databases/system-catalog-views/sys-column-encryption-key-values-transact-sql.md)   
- [sys.column_master_keys &#40;Transact-SQL&#41;](../../../relational-databases/system-catalog-views/sys-column-master-keys-transact-sql.md)   
- [sys.columns &#40;Transact-SQL&#41;](../../../relational-databases/system-catalog-views/sys-columns-transact-sql.md)   
- [sp_refresh_parameter_encryption &#40;Transact-SQL&#41;](../../../relational-databases/system-stored-procedures/sp-refresh-parameter-encryption-transact-sql.md)   
  
  
