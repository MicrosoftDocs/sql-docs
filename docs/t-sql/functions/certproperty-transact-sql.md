---
title: "CERTPROPERTY (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "07/24/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
f1_keywords: 
  - "CERTPROPERTY"
  - "CERTPROPERTY_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "certificates [SQL Server], schema names"
  - "schemas [SQL Server], names"
  - "CERTPROPERTY function"
ms.assetid: 966c09aa-bc4e-45b0-ba53-c8381871f638
caps.latest.revision: 22
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# CERTPROPERTY (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx_md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

Returns the value of a specified certificate property.
  
![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)
  
## Syntax  
  
```sql
CertProperty ( Cert_ID , '<PropertyName>' )  
  
<PropertyName> ::=  
   Expiry_Date | Start_Date | Issuer_Name   
   | Cert_Serial_Number | Subject | SID | String_SID   
```  
  
## Arguments  
*Cert_ID*  
Is the ID of the certificate. *Cert_ID* is an int.
  
*Expiry_Date*  
Is the date of expiration of the certificate.
  
*Start_Date*  
Is the date when the certificate becomes valid.
  
*Issuer_Name*  
Is the issuer name of the certificate.
  
*Cert_Serial_Number*  
Is the certificate serial number.
  
*Subject*  
Is the subject of the certificate.
  
 *SID*  
Is the SID of the certificate. This is also the SID of any login or user mapped to this certificate.
  
*String_SID*  
Is the SID of the certificate as a character string. This is also the SID of any login or user mapped to the certificate.
  
## Return types
The property specification must be enclosed in single quotation marks.
  
The return type depends on the property that is specified in the function call. All return values are wrapped in the return type of **sql_variant**.
-   *Expiry_Date* and *Start_Date* return **datetime**.  
-   *Cert_Serial_Number*, *Issuer_Name*, *Subject*, and *String_SID* return **nvarchar**.  
-   *SID* returns **varbinary**.  
  
## Remarks  
Information about certificates is visible in the [sys.certificates](../../relational-databases/system-catalog-views/sys-certificates-transact-sql.md) catalog view.
  
## Permissions  
Requires some permission on the certificate and that the caller has not been denied VIEW DEFINITION permission on the certificate.
  
## Examples  
The following example returns the certificate subject.
  
```sql
-- First create a certificate.  
CREATE CERTIFICATE Marketing19 WITH   
    START_DATE = '04/04/2004' ,  
    EXPIRY_DATE = '07/07/2007' ,  
    SUBJECT = 'Marketing Print Division';  
GO  
  
-- Now use CertProperty to examine certificate  
-- Marketing19's properties.  
DECLARE @CertSubject sql_variant;  
set @CertSubject = CertProperty( Cert_ID('Marketing19'), 'Subject');  
PRINT CONVERT(nvarchar, @CertSubject);  
GO  
```  
  
## See also
[CREATE CERTIFICATE &#40;Transact-SQL&#41;](../../t-sql/statements/create-certificate-transact-sql.md)  
[ALTER CERTIFICATE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-certificate-transact-sql.md)  
[CERT_ID &#40;Transact-SQL&#41;](../../t-sql/functions/cert-id-transact-sql.md)
[Encryption Hierarchy](../../relational-databases/security/encryption/encryption-hierarchy.md)
[sys.certificates &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-certificates-transact-sql.md)
[Security Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/security-catalog-views-transact-sql.md)
  
  
