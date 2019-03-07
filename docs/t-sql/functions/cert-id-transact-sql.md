---
title: "CERT_ID (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "07/24/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "CERT_ID"
  - "CERT_ID_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "identification numbers [SQL Server], certificates"
  - "CERT_ID function"
  - "IDs [SQL Server], certificates"
  - "certificates [SQL Server], IDs"
ms.assetid: 59cc06f5-272e-4936-8afe-afba7aba8eea
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# CERT_ID (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

This function returns the ID value of a certificate.
  
![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)
  
## Syntax  
  
```sql
Cert_ID ( 'cert_name' )  
```  
  
## Arguments  
**'** *cert_name* **'**  

The name of a certificate in the database.
  
## Return types
 **int**  
  
## Remarks  
The [sys.certificates](../../relational-databases/system-catalog-views/sys-certificates-transact-sql.md) catalog view shows certificate names.
  
## Permissions  
Requires appropriate permission(s) on the certificate, and requires that the caller has not been denied VIEW DEFINITION permission on the certificate. See [CREATE CERTIFICATE &#40;Transact-SQL&#41;](../../t-sql/statements/create-certificate-transact-sql.md#permissions) for more information about certificate permissions.
  
## Examples  
This example returns the ID of a certificate named `ABerglundCert3`.
  
```sql
SELECT Cert_ID('ABerglundCert3');  
GO  
```  
  
## See also
[sys.certificates &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-certificates-transact-sql.md)  
[CREATE CERTIFICATE &#40;Transact-SQL&#41;](../../t-sql/statements/create-certificate-transact-sql.md)  
[Encryption Hierarchy](../../relational-databases/security/encryption/encryption-hierarchy.md)
  
  
