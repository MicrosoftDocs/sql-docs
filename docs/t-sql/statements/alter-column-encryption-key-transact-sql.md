---
title: "ALTER COLUMN ENCRYPTION KEY (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "09/24/2018"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "ALTER COLUMN ENCRYPTION"
  - "ALTER_COLUMN_ENCRYPTION_TSQL"
  - "ALTER COLUMN ENCRYPTION KEY"
  - "ALTER_COLUMN_ENCRYPTION_KEY_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "column encryption key, alter"
  - "ALTER COLUMN ENCRYPTION KEY statement"
ms.assetid: c79a220d-e178-4091-a330-c924cc0f0ae0
author: VanMSFT
ms.author: vanto
manager: craigg
---
# ALTER COLUMN ENCRYPTION KEY (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2016-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2016-asdb-xxxx-xxx-md.md)]

  Alters a column encryption key in a database, adding or dropping an encrypted value. A CEK can have up to two values, which allows for the rotation of the corresponding column master key. A CEK is used when encrypting columns using the [Always Encrypted &#40;Database Engine&#41;](../../relational-databases/security/encryption/always-encrypted-database-engine.md) feature. Before adding a CEK value, you must define the column master key that was used to encrypt the value by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or the [CREATE MASTER KEY](../../t-sql/statements/create-column-master-key-transact-sql.md) statement.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
ALTER COLUMN ENCRYPTION KEY key_name   
    [ ADD | DROP ] VALUE   
    (  
        COLUMN_MASTER_KEY = column_master_key_name   
        [, ALGORITHM = 'algorithm_name' , ENCRYPTED_VALUE =  varbinary_literal ]   
    ) [;]  
```  
  
## Arguments  
 *key_name*  
 The column encryption key that you are changing.  
  
 *column_master_key_name*  
 Specifies the name of the column master key (CMK) used for encrypting the column encryption key (CEK).  
  
 *algorithm_name*  
 Name of the encryption algorithm used to encrypt the value. The algorithm for the system providers must be **RSA_OAEP**. This argument is not valid when dropping a column encryption key value.  
  
 *varbinary_literal*  
 The CEK BLOB encrypted with the specified master encryption key. This argument is not valid when dropping a column encryption key value.  
  
> [!WARNING]  
>  Never pass plaintext CEK values in this statement. Doing so will comprise the benefit of this feature.  
  
## Remarks  
 Typically, a column encryption key is created with just one encrypted value. When a column master key needs to be rotated (the current column master key needs to be replaced with the new column master key), you can add a new value of the column encryption key, encrypted with the new column master key. This workflow allows you to ensure client applications can access data encrypted with the column encryption key, while the new column master key is being made available to client applications. An Always Encrypted enabled driver in a client application that does not have access to the new master key, will be able to use the column encryption key value encrypted with the old column master key to access sensitive data. The encryption algorithms, Always Encrypted supports, require the plaintext value to have 256 bits. An encrypted value should be generated using a key store provider that encapsulates the key store holding the column master key.  

 Column master keys are rotated for following reasons:
- Compliance regulations may require keys are periodically rotated.
- A column master key is compromised, and it needs to be rotated for security reasons.
- To enable or disable sharing column encryption keys with a secure enclave on the server side. For example, if your current column master key does not support enclave computations (has not been defined with the ENCLAVE_COMPUTATIONS property) and you want to enable enclave computations on columns protected with a column encryption key that your column master key encrypts, you need to replace the column master key with the new key with the ENCLAVE_COMPUTATIONS property. For more information, see [Always Encrypted with secure enclaves](../../relational-databases/security/encryption/always-encrypted-enclaves.md).


Use [sys.columns &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-columns-transact-sql.md), [sys.column_encryption_keys  &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-column-encryption-keys-transact-sql.md) and [sys.column_encryption_key_values &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-column-encryption-key-values-transact-sql.md) to view information about column encryption keys.  
  
## Permissions  
 Requires **ALTER ANY COLUMN ENCRYPTION KEY** permission on the database.  
  
## Examples  
  
### A. Adding a column encryption key value  
 The following example alters a column encryption key called `MyCEK`.  
  
```  
ALTER COLUMN ENCRYPTION KEY MyCEK  
ADD VALUE  
(  
    COLUMN_MASTER_KEY = MyCMK2,   
    ALGORITHM = 'RSA_OAEP',   
    ENCRYPTED_VALUE = 0x016E000001630075007200720065006E00740075007300650072002F006D0079002F0064006500650063006200660034006100340031003000380034006200350033003200360066003200630062006200350030003600380065003900620061003000320030003600610037003800310066001DDA6134C3B73A90D349C8905782DD819B428162CF5B051639BA46EC69A7C8C8F81591A92C395711493B25DCBCCC57836E5B9F17A0713E840721D098F3F8E023ABCDFE2F6D8CC4339FC8F88630ED9EBADA5CA8EEAFA84164C1095B12AE161EABC1DF778C07F07D413AF1ED900F578FC00894BEE705EAC60F4A5090BBE09885D2EFE1C915F7B4C581D9CE3FDAB78ACF4829F85752E9FC985DEB8773889EE4A1945BD554724803A6F5DC0A2CD5EFE001ABED8D61E8449E4FAA9E4DD392DA8D292ECC6EB149E843E395CDE0F98D04940A28C4B05F747149B34A0BAEC04FFF3E304C84AF1FF81225E615B5F94E334378A0A888EF88F4E79F66CB377E3C21964AACB5049C08435FE84EEEF39D20A665C17E04898914A85B3DE23D56575EBC682D154F4F15C37723E04974DB370180A9A579BC84F6BC9B5E7C223E5CBEE721E57EE07EFDCC0A3257BBEBF9ADFFB00DBF7EF682EC1C4C47451438F90B4CF8DA709940F72CFDC91C6EB4E37B4ED7E2385B1FF71B28A1D2669FBEB18EA89F9D391D2FDDEA0ED362E6A591AC64EF4AE31CA8766C259ECB77D01A7F5C36B8418F91C1BEADDD4491C80F0016B66421B4B788C55127135DA2FA625FB7FD195FB40D90A6C67328602ECAF3EC4F5894BFD84A99EB4753BE0D22E0D4DE6A0ADFEDC80EB1B556749B4A8AD00E73B329C95827AB91C0256347E85E3C5FD6726D0E1FE82C925D3DF4A9  
);  
GO  
  
```  
  
### B. Dropping a column encryption key value  
 The following example alters a column encryption key called `MyCEK` by dropping a value.  
  
```  
ALTER COLUMN ENCRYPTION KEY MyCEK  
DROP VALUE  
(  
    COLUMN_MASTER_KEY = MyCMK  
);  
GO  
```  
  
## See Also  
 [CREATE COLUMN ENCRYPTION KEY &#40;Transact-SQL&#41;](../../t-sql/statements/create-column-encryption-key-transact-sql.md)   
 [DROP COLUMN ENCRYPTION KEY &#40;Transact-SQL&#41;](../../t-sql/statements/drop-column-encryption-key-transact-sql.md)   
 [CREATE COLUMN MASTER KEY &#40;Transact-SQL&#41;](../../t-sql/statements/create-column-master-key-transact-sql.md)   
 [Always Encrypted &#40;Database Engine&#41;](../../relational-databases/security/encryption/always-encrypted-database-engine.md)   
 [sys.column_encryption_keys  &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-column-encryption-keys-transact-sql.md)   
 [sys.column_encryption_key_values &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-column-encryption-key-values-transact-sql.md)   
 [sys.columns &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-columns-transact-sql.md)  
  
  
