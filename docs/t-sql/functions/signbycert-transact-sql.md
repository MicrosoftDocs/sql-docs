---
title: "SIGNBYCERT (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "SIGNBYCERT"
  - "SIGNBYCERT_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "text signing [SQL Server]"
  - "encryption [SQL Server], certificates"
  - "certificates [SQL Server], text signing"
  - "SignByCert function"
  - "signing text [SQL Server]"
  - "SIGNBYCERT function"
  - "cryptography [SQL Server], certificates"
ms.assetid: b4c6bced-4473-4bae-85b9-56deced495f9
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# SIGNBYCERT (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

  Signs text with a certificate and returns the signature.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
SignByCert ( certificate_ID , @cleartext [ , 'password' ] )  
```  
  
## Arguments  
 *certificate_ID*  
 Is the ID of a certificate in the current database. *certificate_ID* is **int**.  
  
 *@cleartext*  
 Is a variable of type **nvarchar**, **char**, **varchar**, or **nchar** that contains data that will be signed.  
  
 **'** *password* **'**  
 Is the password with which the certificate's private key was encrypted. *password* is **nvarchar(128)**.  
  
## Return Types  
 **varbinary** with a maximum size of 8,000 bytes.  
  
## Remarks  
 Requires CONTROL permission on the certificate.  
  
## Examples  
 The following example signs the text in `@SensitiveData` with certificate `ABerglundCert07`, having first decrypted the certificate with password "pGFD4bb925DGvbd2439587y". It then inserts the cleartext and the signature in table `SignedData04`.  
  
```  
DECLARE @SensitiveData nvarchar(max);  
SET @SensitiveData = N'Saddle Price Points are   
    2, 3, 5, 7, 11, 13, 17, 19, 23, 29';  
INSERT INTO [SignedData04]  
    VALUES( N'data signed by certificate ''ABerglundCert07''',  
    @SensitiveData, SignByCert( Cert_Id( 'ABerglundCert07' ),   
    @SensitiveData, N'pGFD4bb925DGvbd2439587y' ));  
GO  
```  
  
## See Also  
 [VERIFYSIGNEDBYCERT &#40;Transact-SQL&#41;](../../t-sql/functions/verifysignedbycert-transact-sql.md)   
 [CERT_ID &#40;Transact-SQL&#41;](../../t-sql/functions/cert-id-transact-sql.md)   
 [CREATE CERTIFICATE &#40;Transact-SQL&#41;](../../t-sql/statements/create-certificate-transact-sql.md)   
 [ALTER CERTIFICATE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-certificate-transact-sql.md)   
 [DROP CERTIFICATE &#40;Transact-SQL&#41;](../../t-sql/statements/drop-certificate-transact-sql.md)   
 [Encryption Hierarchy](../../relational-databases/security/encryption/encryption-hierarchy.md)  
  
  
