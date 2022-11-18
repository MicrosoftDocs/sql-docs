---
title: ADD SIGNATURE (Transact-SQL)
description: ADD SIGNATURE (Transact-SQL)
author: VanMSFT
ms.author: vanto
ms.date: 03/16/2021
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "ADD SIGNATURE"
  - "ADD_SIGNATURE_TSQL"
helpviewer_keywords:
  - "ADD SIGNATURE statement"
  - "adding digital signatures"
  - "signatures [SQL Server]"
  - "digital signatures [SQL Server]"
dev_langs:
  - "TSQL"
---

# ADD SIGNATURE (Transact-SQL)

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

Adds a digital signature to a stored procedure, function, assembly, or DML-trigger. Also adds a countersignature to a stored procedure, function, assembly, or DML-trigger.

![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  

## Syntax

```syntaxsql
ADD [ COUNTER ] SIGNATURE TO module_class::module_name
    BY <crypto_list> [ ,...n ]
  
<crypto_list> ::=  
    CERTIFICATE cert_name  
    | CERTIFICATE cert_name [ WITH PASSWORD = 'password' ]
    | CERTIFICATE cert_name WITH SIGNATURE = signed_blob
    | ASYMMETRIC KEY Asym_Key_Name  
    | ASYMMETRIC KEY Asym_Key_Name [ WITH PASSWORD = 'password'.]
    | ASYMMETRIC KEY Asym_Key_Name WITH SIGNATURE = signed_blob
```

[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments

*module_class*  
Is the class of the module to which the signature is added. The default for schema-scoped modules is OBJECT.  
  
 *module_name*  
 Is the name of a stored procedure, function, assembly, or trigger to be signed or countersigned.  
  
 CERTIFICATE *cert_name*  
 Is the name of a certificate with which to sign or countersign the stored procedure, function, assembly, or trigger.  
  
 WITH PASSWORD ='*password*'  
 Is the password that is required to decrypt the private key of the certificate or asymmetric key. This clause is only required if the private key isn't protected by the database master key.  
  
 SIGNATURE =*signed_blob*  
 Specifies the signed, binary large object (BLOB) of the module. This clause is useful if you want to ship a module without shipping the private key. When you use this clause, only the module, signature, and public key are required to add the signed binary large object to a database. *signed_blob* is the blob itself in hexadecimal format.  
  
 ASYMMETRIC KEY *Asym_Key_Name*  
 Is the name of an asymmetric key with which to sign or counter-sign the stored procedure, function, assembly, or trigger.  
  
## Remarks

The module being signed or countersigned and the certificate or asymmetric key used to sign it must already exist. Every character in the module is included in the signature calculation. This includes leading carriage returns and line feeds.  
  
 A module can be signed and countersigned by any number of certificates and asymmetric keys.  
  
 The signature of a module is dropped when the module is changed.  
  
 If a module contains an EXECUTE AS clause, the security ID (SID) of the principal is also included as a part of the signing process.  
  
> [!CAUTION]
> Module signing should only be used to grant permissions, never to deny or revoke permissions.  
  
 Data definition language (DDL) triggers and Inline table-valued functions cannot be signed.  
  
 Information about signatures is visible in the sys.crypt_properties catalog view.  
  
> [!WARNING]
>  When recreating a procedure for signature, all the statements in the original batch must match recreation batch. If any portion of the batch differs, even in spaces or comments, the resultant signature will be different.  
  
## Countersignatures  
 When executing a signed module, the signatures will be temporarily added to the SQL token, but the signatures are lost if the module executes another module or if the module terminates execution. A countersignature is a special form of signature. By itself, a countersignature doesn't grant any permissions, however, it allows signatures made by the same certificate or asymmetric key to be kept for the duration of the call made to the countersigned object.  
  
 For example, presume that user Alice calls procedure ProcForAlice, which calls procedure ProcSelectT1, which selects from table T1. Alice has EXECUTE permission on ProcForAlice and ProcSelectT1, but she doesn't have SELECT permission on T1, and no ownership chaining is involved in this entire chain. Alice cannot access table T1, either directly, or through the use of ProcForAlice and ProcSelectT1. Since we want Alice to always use ProcForAlice for access, we don't want to grant her permission to execute ProcSelectT1. How can we accomplish this?  
  
-   If we sign ProcSelectT1, such that ProcSelectT1 can access T1, then Alice can invoke ProcSelectT1 directly and she doesn't have to call ProcForAlice.  
  
-   We could deny EXECUTE permission on ProcSelectT1 to Alice, but then Alice would not be able to call ProcSelectT1 through ProcForAlice.
  
-   Signing ProcForAlice would not work by itself, because the signature would be lost in the call to ProcSelectT1.  
  
However, by countersigning ProcSelectT1 with the same certificate used to sign ProcForAlice, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] the signature will be kept across the call chain and will allow access to T1. If Alice attempts to call ProcSelectT1 directly, she cannot access T1, because the countersignature doesn't grant any rights. Example C below, shows the [!INCLUDE[tsql](../../includes/tsql-md.md)] for this example.  

:::image type="content" source="media/signing-and-countersignature.png" alt-text="signature example":::

## Permissions  

Requires ALTER permission on the object and CONTROL permission on the certificate or asymmetric key. If an associated private key is protected by a password, the user also must have the password.  
  
## Examples  
  
### A. Signing a stored procedure by using a certificate

 The following example signs the stored procedure `HumanResources.uspUpdateEmployeeLogin` with the certificate `HumanResourcesDP`.  
  
```sql  
USE AdventureWorks2012;  
ADD SIGNATURE TO HumanResources.uspUpdateEmployeeLogin   
    BY CERTIFICATE HumanResourcesDP;  
GO  
```  
  
### B. Signing a stored procedure by using a signed BLOB

The following example creates a new database and creates a certificate to use in the example. The example creates and signs a simple stored procedure and retrieves the signature BLOB from `sys.crypt_properties`. The signature is then dropped and added again. The example signs the procedure by using the WITH SIGNATURE syntax.  
  
```sql  
CREATE DATABASE TestSignature ;  
GO  
USE TestSignature ;  
GO  
-- Create a CERTIFICATE to sign the procedure.  
CREATE CERTIFICATE cert_signature_demo   
    ENCRYPTION BY PASSWORD = 'pGFD4bb925DGvbd2439587y'  
    WITH SUBJECT = 'ADD SIGNATURE demo';  
GO  

-- Create a simple procedure.  
CREATE PROC [sp_signature_demo]  
AS  
    PRINT 'This is the content of the procedure.' ;  
GO  
-- Sign the procedure.  
ADD SIGNATURE TO [sp_signature_demo]   
    BY CERTIFICATE [cert_signature_demo]   
    WITH PASSWORD = 'pGFD4bb925DGvbd2439587y' ;  
GO  

-- Get the signature binary BLOB for the sp_signature_demo procedure.  
SELECT cp.crypt_property  
    FROM sys.crypt_properties AS cp  
    JOIN sys.certificates AS cer  
        ON cp.thumbprint = cer.thumbprint  
    WHERE cer.name = 'cert_signature_demo' ;  
GO  
```  
  
 The `crypt_property` signature that is returned by this statement will be different each time you create a procedure. Make a note of the result for use later in this example. For this example, the result demonstrated is: `0x831F5530C86CC8ED606E5BC2720DA835351E46219A6D5DE9CE546297B88AEF3B6A7051891AF3EE7A68EAB37CD8380988B4C3F7469C8EABDD9579A2A5C507A4482905C2F24024FFB2F9BD7A953DD5E98470C4AA90CE83237739BB5FAE7BAC796E7710BDE291B03C43582F6F2D3B381F2102EEF8407731E01A51E24D808D54B373`.  
  
```sql  
-- Drop the signature so that it can be signed again.  
DROP SIGNATURE FROM [sp_signature_demo]   
    BY CERTIFICATE [cert_signature_demo];  
GO  

-- Add the signature. Use the signature BLOB obtained earlier.  
ADD SIGNATURE TO [sp_signature_demo]   
    BY CERTIFICATE [cert_signature_demo]  
    WITH SIGNATURE = 0x831F5530C86CC8ED606E5BC2720DA835351E46219A6D5DE9CE546297B88AEF3B6A7051891AF3EE7A68EAB37CD8380988B4C3F7469C8EABDD9579A2A5C507A4482905C2F24024FFB2F9BD7A953DD5E98470C4AA90CE83237739BB5FAE7BAC796E7710BDE291B03C43582F6F2D3B381F2102EEF8407731E01A51E24D808D54B373 ;  
GO  
```  
  
### C. Accessing a procedure using a countersignature

The following example shows how countersigning can help control access to an object.  
  
```sql  
-- Create tesT1 database  
CREATE DATABASE testDB;  
GO  
USE testDB;  
GO  
-- Create table T1  
CREATE TABLE T1 (c VARCHAR(11));  
INSERT INTO T1 VALUES ('This is T1.');  
  
-- Create a TestUser user to own table T1  
CREATE USER TestUser WITHOUT LOGIN;  
ALTER AUTHORIZATION ON T1 TO TestUser;  
  
-- Create a certificate for signing  
CREATE CERTIFICATE csSelectT  
  ENCRYPTION BY PASSWORD = 'SimplePwd01'  
  WITH SUBJECT = 'Certificate used to grant SELECT on T1';  
CREATE USER ucsSelectT1 FROM CERTIFICATE csSelectT;  
GRANT SELECT ON T1 TO ucsSelectT1;  
  
-- Create a principal with low privileges  
CREATE LOGIN Alice WITH PASSWORD = 'SimplePwd01';  
CREATE USER Alice;  
  
-- Verify Alice cannoT1 access T1;  
EXECUTE AS LOGIN = 'Alice';  
    SELECT * FROM T1;  
REVERT;  
GO  
-- Create a procedure that directly accesses T1  
CREATE PROCEDURE procSelectT1 AS  
BEGIN  
    PRINT 'Now selecting from T1...';  
    SELECT * FROM T1;  
END;  
GO  
GRANT EXECUTE ON ProcSelectT1 to public;  
GO
-- Create special procedure for accessing T1  
CREATE PROCEDURE  ProcForAlice AS  
BEGIN  
   IF USER_ID() <> USER_ID('Alice')  
    BEGIN  
        PRINT 'Only Alice can use this.';  
        RETURN  
    END  
   EXEC ProcSelectT1;  
END;  
GO;  
GRANT EXECUTE ON ProcForAlice TO PUBLIC;  
  
-- Verify procedure works for a sysadmin user  
EXEC ProcForAlice;  
  
-- Alice still can't use the procedure yet  
EXECUTE AS LOGIN = 'Alice';  
    EXEC ProcForAlice;  
REVERT;  
  
-- Sign procedure to grant it SELECT permission  
ADD SIGNATURE TO ProcForAlice BY CERTIFICATE csSelectT   
WITH PASSWORD = 'SimplePwd01';  
  
-- Counter sign ProcSelectT1, to make this work  
ADD COUNTER SIGNATURE TO ProcSelectT1 BY CERTIFICATE csSelectT   
WITH PASSWORD = 'SimplePwd01';  
  
-- Now the proc works.   
-- Note that calling ProcSelectT1 directly still doesn't work  
EXECUTE AS LOGIN = 'Alice';  
    EXEC ProcForAlice;  
    EXEC ProcSelectT1;  
REVERT;  
  
-- Cleanup  
USE master;  
GO  
DROP DATABASE testDB;  
DROP LOGIN Alice;  
```  
  
## See Also

- [sys.crypt_properties &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-crypt-properties-transact-sql.md)
- [DROP SIGNATURE &#40;Transact-SQL&#41;](../../t-sql/statements/drop-signature-transact-sql.md)
