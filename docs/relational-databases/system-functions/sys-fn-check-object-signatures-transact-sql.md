---
description: "sys.fn_check_object_signatures (Transact-SQL)"
title: "sys.fn_check_object_signatures (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/10/2016"
ms.service: sql
ms.reviewer: ""
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords: 
  - "sys.fn_check_object_signatures_TSQL"
  - "fn_check_object_signatures_TSQL"
  - "fn_check_object_signatures"
  - "sys.fn_check_object_signatures"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.fn_check_object_signatures function"
ms.assetid: 47509566-d3d7-46a9-89c1-91b4895d56b9
author: rwestMSFT
ms.author: randolphwest
monikerRange: ">=aps-pdw-2016||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.fn_check_object_signatures (Transact-SQL)
[!INCLUDE [sql-asdbmi-pdw](../../includes/applies-to-version/sql-asdbmi-pdw.md)]

  Returns a list of all signable objects and indicates whether an object is signed by a specified certificate or asymmetric key. If the object is signed by the specified certificate or asymmetric key signed, it also returns whether the object's signature is valid.  
  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
fn_ check_object_signatures (   
    { '@class' } , { @thumbprint }   
  )   
```  
  
## Arguments  
 { '\@*class*' }  
 Identifies the type of thumbprint being provided:  
  
-   'certificate'  
  
-   'asymmetric key'  
  
 \@*class* is **sysname**.  
  
 { \@*thumbprint* }  
 SHA-1 hash of the certificate with which the key is encrypted, or the GUID of the asymmetric key with which the key is encrypted. \@*thumbprint* is **varbinary(20)**.  
  
## Tables Returned  
 The following table lists the columns that **fn_check_object_signatures** returns.  
  
|Column|Type|Description|  
|------------|----------|-----------------|  
|type|**nvarchar(120)**|Returns type description or assembly.|  
|entity_id|**int**|Returns the object id of the object being evaluated.|  
|is_signed|**int**|Returns 0 when the object is not signed by the provided thumbprint. Returns 1 when the object is signed by the provided thumbprint.|  
|is_signature_valid|**int**|When the is_signed value is 1, returns 0 when the signature is not valid. Returns 1 when the signature is valid.<br /><br /> When the is_signed value is 0, always returns 0.|  
  
## Remarks  
 Use **fn_check_object_signatures** to confirm that malicious users have not tampered with objects.  
  
## Permissions  
 Requires VIEW DEFINITION on the certificate or asymmetric key.  
  
## Examples  
 The following example finds the schema signing certificate for the `master` database, and returns the `is_signed` value of 1 and the `is_signature_valid` value of 1 for those objects that are signed by the schema signing certificate and which have valid signatures.  
  
```  
USE master;  
-- Declare a variable to hold the thumbprint.  
DECLARE @thumbprint varbinary(20) ;  
-- Populate the thumbprint variable with the master database schema signing certificate.  
SELECT @thumbprint = thumbprint   
FROM sys.certificates   
WHERE name LIKE '%SchemaSigningCertificate%' ;  
-- Evaluates the objects signed by the schema signing certificate  
SELECT type, entity_id, OBJECT_NAME(entity_id) AS [object name], is_signed, is_signature_valid  
FROM sys.fn_check_object_signatures ('certificate', @thumbprint) ;  
GO  
  
```  
  
## See Also  
 [IS_OBJECTSIGNED &#40;Transact-SQL&#41;](../../t-sql/functions/is-objectsigned-transact-sql.md)  
  
  
