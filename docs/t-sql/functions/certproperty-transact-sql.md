---
title: "CERTPROPERTY (Transact-SQL)"
description: "CERTPROPERTY (Transact-SQL)"
author: VanMSFT
ms.author: vanto
ms.date: "07/24/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "CERTPROPERTY"
  - "CERTPROPERTY_TSQL"
helpviewer_keywords:
  - "certificates [SQL Server], schema names"
  - "schemas [SQL Server], names"
  - "CERTPROPERTY function"
dev_langs:
  - "TSQL"
---
# CERTPROPERTY (Transact-SQL)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

Returns the value of a specified certificate property.
  
![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)
  
## Syntax  
  
```syntaxsql
CertProperty ( Cert_ID , '<PropertyName>' )  
  
<PropertyName> ::=  
   Expiry_Date | Start_Date | Issuer_Name   
   | Cert_Serial_Number | Subject | SID | String_SID   
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
*Cert_ID*  
The certificate ID value, of data type int.
  
*Expiry_Date*  
The certificate expiration date.
  
*Start_Date*  
The date when the certificate becomes valid.
  
*Issuer_Name*  
The name of the certificate issuer.
  
*Cert_Serial_Number*  
The certificate serial number.
  
*Subject*  
The certificate subject.
  
 *SID*  
The certificate SID. This is also the SID of any login or user mapped to this certificate.
  
*String_SID*  
The SID of the certificate as a character string. This is also the SID of any login or user mapped to the certificate.
  
## Return types
Single quotation marks must enclose the property specification.
  
The return type depends on the property specified in the function call. The return type **sql_variant** wraps all return values.
-   *Expiry_Date* and *Start_Date* return **datetime**.  
-   *Cert_Serial_Number*, *Issuer_Name*, *String_SID*, and *Subject* all return **nvarchar**.  
-   *SID* returns **varbinary**.  
  
## Remarks  
See certificate information in the [sys.certificates](../../relational-databases/system-catalog-views/sys-certificates-transact-sql.md) catalog view.
  
## Permissions  
Requires appropriate permission(s) on the certificate, and requires that the caller has not been denied VIEW permission on the certificate. See [CREATE CERTIFICATE &#40;Transact-SQL&#41;](../../t-sql/statements/create-certificate-transact-sql.md) and [GRANT CERTIFICATE PERMISSIONS &#40;Transact-SQL&#41;](../../t-sql/statements/grant-certificate-permissions-transact-sql.md) for more information about certificate permissions.
  
## Examples  
The following example returns the certificate subject.
  
```sql
-- First create a certificate.  
CREATE CERTIFICATE Marketing19 WITH   
    START_DATE = '04/04/2004' ,  
    EXPIRY_DATE = '07/07/2040' ,  
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
  
  
