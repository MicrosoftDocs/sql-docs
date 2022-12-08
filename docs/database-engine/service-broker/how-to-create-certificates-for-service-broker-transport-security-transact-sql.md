---
title: "How to: Create Certificates for Service Broker Transport Security (Transact-SQL)"
description: "To set up Service Broker transport security for an instance of SQL Server, you first create a certificate in the master database by using the Transact-SQL CREATE CERTIFICATE statement."
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: mikeray, maghan
ms.date: "03/30/2022"
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
---

# How to: Create Certificates for Service Broker Transport Security (Transact-SQL)

[!INCLUDE [sql-asdbmi](../../includes/applies-to-version/sql-asdbmi.md)]

To set up Service Broker transport security for an instance of SQL Server, you first create a certificate in the **master** database by using the Transact-SQL CREATE CERTIFICATE statement. This statement creates both a public key and a private key. You can also use the CREATE CERTIFICATE statement to load an existing X.509 certificate. For more information on creating certificates, see [CREATE CERTIFICATE (Transact-SQL)](../../t-sql/statements/create-certificate-transact-sql.md). After creating the certificate, use the CREATE ENDPOINT or ALTER ENDPOINT statement to set the Service Broker endpoint to use the new certificate.

For more information using certificates for Service Broker transport security, see [How to: Allow Service Broker Network Access by Using Certificates (Transact-SQL)](how-to-allow-service-broker-network-access-by-using-certificates-transact-sql.md).

## To create a certificate for Service Broker transport security

- Create a certificate in the **master** database.

## Example

```sql
    USE master ;
    GO

    -- Create a certificate owned by dbo.

    CREATE CERTIFICATE TransportSecurity
        AUTHORIZATION [dbo]
        ENCRYPTION BY PASSWORD = '**(34ader#$lqQEUer13'
        WITH SUBJECT='Instance certificate for transport security';
    GO
```

## See also

- [CREATE CERTIFICATE (Transact-SQL)](../../t-sql/statements/create-certificate-transact-sql.md)
- [CREATE ENDPOINT (Transact-SQL)](../../t-sql/statements/create-endpoint-transact-sql.md)
- [ALTER ENDPOINT (Transact-SQL)](../../t-sql/statements/alter-endpoint-transact-sql.md)
