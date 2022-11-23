---
description: "sp_describe_parameter_encryption (Transact-SQL)"
title: "sp_describe_parameter_encryption (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "07/27/2016"
ms.service: sql
ms.reviewer: "vanto"
ms.subservice: system-objects
ms.topic: "reference"
dev_langs: 
  - "TSQL"
f1_keywords: 
  - "sp_describe_parameter_encryption"
  - "sp_describe_parameter_encryption_TSQL"
  - "sys.sp_describe_parameter_encryption"
  - "sys.sp_describe_parameter_encryption_TSQL"
helpviewer_keywords: 
  - "sp_describe_parameter_encryption"
ms.assetid: 706ed441-2881-4934-8d5e-fb357ee067ce
author: jaszymas
ms.author: jaszymas
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sp_describe_parameter_encryption (Transact-SQL)

[!INCLUDE [sqlserver2016-asdb-asdbmi](../../includes/applies-to-version/sqlserver2016-asdb-asdbmi.md)]

  Analyzes the specified [!INCLUDE[tsql](../../includes/tsql-md.md)] statement and its parameters, to determine which parameters correspond to database columns that are protected by using the Always Encrypted feature. Returns encryption metadata for the parameters that correspond to encrypted columns.  
  
## Syntax  
  
```  
sp_describe_parameter_encryption   
    [ @tsql = ] N'Transact-SQL_batch' ,   
    [ @params = ] N'parameters'   
[ ;]  
```  
  
## Arguments  
 [ \@tsql = ] 'Transact-SQL_batch'  
 One or more [!INCLUDE[tsql](../../includes/tsql-md.md)] statements. Transact-SQL_batch may be nvarchar(n) or nvarchar(max).  
  
 [ \@params = ] N'parameters'  
 *\@params* provides a declaration string for parameters for the Transact-SQL batch, which is similar to sp_executesql. Parameters may be nvarchar(n) or nvarchar(max).  
  
 Is one string that contains the definitions of all parameters that have been embedded in the [!INCLUDE[tsql](../../includes/tsql-md.md)]_batch. The string must be either a Unicode constant or a Unicode variable. Each parameter definition consists of a parameter name and a data type. *n* is a placeholder that indicates additional parameter definitions. Every parameter specified in the statement must be defined in *\@params*. If the [!INCLUDE[tsql](../../includes/tsql-md.md)] statement or batch in the statement does not contain parameters, *\@params* is not required. NULL is the default value for this parameter.  
  
## Return Value  
 0 indicates success. Anything else indicate failure.  
  
## Result Sets  
 **sp_describe_parameter_encryption** returns two result sets:  
  
-   The result set describing cryptographic keys configured for database columns, the parameters of the specified [!INCLUDE[tsql](../../includes/tsql-md.md)] statement correspond to.  
  
-   The result set describing how particular parameters should be encrypted. This result set references the keys described in the first result set.  
  
 Each row of the first result set describes a pair of keys; an encrypted column encryption key and its corresponding column master key.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**column_encryption_key_ordinal**|**int**|Id of the row in the resultset.|  
|**database_id**|**int**|Database id.|  
|**column_encryption_key_id**|**int**|The column encryption key id. Note: this id denotes a row in the [sys.column_encryption_keys  &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-column-encryption-keys-transact-sql.md) catalog view.|  
|**column_encryption_key_version**|**int**|Reserved for future use. Currently, always contains 1.|  
|**column_encryption_key_metadata_version**|**binary(8)**|A timestamp representing the creation time of the column encryption key.|  
|**column_encryption_key_encrypted_value**|**varbinary(4000)**|The encrypted value of the column encryption key.|  
|**column_master_key_store_provider_name**|**sysname**|The name of the provider for the key store that contains the column master key, which was used to produce the encrypted value of the column encryption key.|  
|**column_master_key_path**|**nvarchar(4000)**|The key path of the column master key, which was used to produce the encrypted value of the column encryption key.|  
|**column_encryption_key_encryption_algorithm_name**|**sysname**|The name of the encryption algorithm used to produce the encryption value of the column encryption key.|  
  
 Each row of the second result set contains encryption metadata for one parameter.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**parameter_ordinal**|**int**|Id of the row in the result set.|  
|**parameter_name**|**sysname**|Name of one of the parameters specified in the *\@params* argument.|  
|**column_encryption_algorithm**|**tinyint**|Code indicating the encryption algorithm configured for the column, the parameter corresponds to. The currently supported values are: 2 for **AEAD_AES_256_CBC_HMAC_SHA_256**.|  
|**column_encryption_type**|**tinyint**|Code indicating the encryption type configured for the column, the parameter corresponds to. The supported values are:<br /><br /> 0 - plaintext (the column is not encrypted)<br /><br /> 1 - deterministic encryption<br /><br /> 2 - randomized encryption.|  
|**column_encryption_key_ordinal**|**int**|Code of the row in the first result set. The referenced row describes the column encryption key configured for the column, the parameter corresponds to.|  
|**column_encryption_normalization_rule_version**|**tinyint**|Version number of the type normalization algorithm.|  
  
## Remarks  
 A [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] client driver, supporting Always Encrypted, automatically calls **sp_describe_parameter_encryption** to retrieve encryption metadata for parameterized queries, issued by the application. Subsequently, the driver uses the encryption metadata to encrypt the values of parameters that correspond to database columns protected with Always Encrypted and substitutes the plaintext parameter values, submitted by the application, with the encrypted parameter values, before sending the query to the database engine.  
  
## Permissions  
 Require the **VIEW ANY COLUMN ENCRYPTION KEY DEFINITION** and **VIEW ANY COLUMN MASTER KEY DEFINITION** permissions in the database.  
  
## Examples  
  
```sql  
CREATE COLUMN MASTER KEY [CMK1]  
WITH  
(  
       KEY_STORE_PROVIDER_NAME = N'MSSQL_CERTIFICATE_STORE',  
    KEY_PATH = N'CurrentUser/my/A66BB0F6DD70BDFF02B62D0F87E340288E6F9305'  
);  
GO  
  
CREATE COLUMN ENCRYPTION KEY [CEK1]  
WITH VALUES  
(  
       COLUMN_MASTER_KEY = [CMK1],  
    ALGORITHM = 'RSA_OAEP',  
    ENCRYPTED_VALUE =   
0x016E000001630075007200720065006E00740075007300650072002F006D007  
9002F00610036003600620062003000660036006400640037003000620064006  
6006600300032006200360032006400300066003800370065003300340030003  
200380038006500360066003900330030003500CA0D0CEC74ECADD1804CF991  
37B4BD06BBAB15D7EA74E0C249A779C7768A5B659E0125D24FF827F5EA8CA51  
7A8E197ECA1353BA814C2B0B2E6C8AB36E3AE6A1E972D69C3C573A963ADAB6  
686CF5D24F95FE43140C4F9AF48FBA7DF2D053F3B4A1F5693A1F905440F8015B  
DB43AF8A04BE4E045B89876A0097E5FBC4E6A3B9C3C0D278C540E46C53938B8  
C957B689C4DC095821C465C73117CBA95B758232F9E5B2FCC7950B8CA00AFE3  
74DE42847E3FBC2FDD277035A2DEF529F4B735C20D980073B4965B4542A3472  
3276A1646998FC6E1C40A3FDB6ABCA98EE2B447F114D2AC7FF8C7D51657550E  
C5C2BABFFE8429B851272086DCED94332CF18FA854C1D545A28B1EF4BE64F8E0  
35175C1650F6FC5C4702ACF99850A4542B3747EAEC0CC726E091B36CE24392D8  
01ECAA684DE344FECE05812D12CD72254A014D42D0EABDA41C89FC4F545E88B  
4B8781E5FAF40D7199D4842D2BFE904D209728ED4F527CBC169E2904F6E711FF8  
1A8F4C25382A2E778DD2A58552ED031AFFDA9D9D891D98AD82155F93C58202F  
C24A77F415D4F8EF22419D62E188AC609330CCBD97CEE1AEF8A18B0195883360  
4707FDF03B2B386487CC679D7E352D0B69F9FB002E51BCD814D077E82A09C14E  
9892C1F8E0C559CFD5FA841CEF647DAB03C8191DC46B772E94D579D8C80FE93C  
3827C9F0AE04D5325BC73111E07EEEDBE67F1E2A73580085  
);  
GO  
  
CREATE TABLE t1 (  
c1 INT ENCRYPTED WITH (  
    COLUMN_ENCRYPTION_KEY = [CEK1],   
    ENCRYPTION_TYPE = Randomized,   
    ALGORITHM = 'AEAD_AES_256_CBC_HMAC_SHA_256') NULL,  
);  
  
EXEC sp_describe_parameter_encryption N'INSERT INTO t1 VALUES(@c1)',  N'@c1 INT';  
```  
  
 Here is the first result set:  
  
|column_encryption_key_ordinal|database_id|column_encryption_key_id|column_encryption_key_version|column_encryption_key_metadata_version|column_encryption_key_encrypted_value|  
|--------------------------------------|------------------|---------------------------------|--------------------------------------|------------------------------------------------|-----------------------------------------------|  
|1|5|1|1|0x99EDA60083A50000|0x016E000001630075007200720065006E00740075007300650072002F006D0079002F006100360036006200620030006600360064006400370030006200640066006600300032006200360032006400300066003800370065003300340030003200380038006500360066003900330030003500CA0D0CEC74ECADD1804CF99137B4BD06BBAB15D7EA74E0C249A779C7768A5B659E0125D24FF827F5EA8CA517A8E197ECA1353BA814C2B0B2E6C8AB36E3AE6A1E972D69C3C573A963ADAB6686CF5D24F95FE43140C4F9AF48FBA7DF2D053F3B4A1F5693A1F905440F8015BDB43AF8A04BE4E045B89876A0097E5FBC4E6A3B9C3C0D278C540E46C53938B8C957B689C4DC095821C465C73117CBA95B758232F9E5B2FCC7950B8CA00AFE374DE42847E3FBC2FDD277035A2DEF529F4B735C20D980073B4965B4542A34723276A1646998FC6E1C40A3FDB6ABCA98EE2B447F114D2AC7FF8C7D51657550EC5C2BABFFE8429B851272086DCED94332CF18FA854C1D545A28B1EF4BE64F8E035175C1650F6FC5C4702ACF99850A4542B3747EAEC0CC726E091B36CE24392D801ECAA684DE344FECE05812D12CD72254A014D42D0EABDA41C89FC4F545E88B4B8781E5FAF40D7199D4842D2BFE904D209728ED4F527CBC169E2904F6E711FF81A8F4C25382A2E778DD2A58552ED031AFFDA9D9D891D98AD82155F93C58202FC24A77F415D4F8EF22419D62E188AC609330CCBD97CEE1AEF8A18B01958833604707FDF03B2B386487CC679D7E352D0B69F9FB002E51BCD814D077E82A09C14E9892C1F8E0C559CFD5FA841CEF647DAB03C8191DC46B772E94D579D8C80FE93C3827C9F0AE04D5325BC73111E07EEEDBE67F1E2A73580085|  
  
 (Results continue.)  
  
|column_master_key_store_provider_name|column_master_key_path|column_encryption_key_encryption_algorithm_name|  
|------------------------------------------------|-------------------------------|----------------------------------------------------------|  
|MSSQL_CERTIFICATE_STORE|CurrentUser/my/A66BB0F6DD70BDFF02B62D0F87E340288E6F9305|RSA_OAEP|  
  
 Here is the second result set:  
  
|parameter_ordinal|parameter_name|column_encryption_algorithm|column_encryption_type|  
|------------------------|---------------------|-----------------------------------|------------------------------|  
|1|\@c1|1|1|  
  
 (Results continue.)  
  
|column_encryption_key_ordinal|column_encryption_normalization_rule_version|  
|--------------------------------------|------------------------------------------------------|  
|1|1|  
  
## See Also  
 [Always Encrypted](../../relational-databases/security/encryption/always-encrypted-database-engine.md)   
 [Develop application using Always Encrypted](../../relational-databases/security/encryption/always-encrypted-client-development.md)  
  
  
