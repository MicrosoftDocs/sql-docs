---
title: "CERT_ID (Transact-SQL)"
description: "CERT_ID (Transact-SQL)"
author: VanMSFT
ms.author: vanto
ms.date: "07/24/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "CERT_ID"
  - "CERT_ID_TSQL"
helpviewer_keywords:
  - "identification numbers [SQL Server], certificates"
  - "CERT_ID function"
  - "IDs [SQL Server], certificates"
  - "certificates [SQL Server], IDs"
dev_langs:
  - "TSQL"
---
# CERT_ID (Transact-SQL)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

This function returns the ID value of a certificate.
  
![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)
  
## Syntax  
  
```syntaxsql
CERT_ID ( 'cert_name' )  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

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
  
  
