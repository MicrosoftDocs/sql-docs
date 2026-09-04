---
title: "How To: Create Certificates for Service Broker Transport Security (Transact-SQL)"
description: "To set up Service Broker transport security for an instance of SQL Server, you first create a certificate in the master database by using the Transact-SQL CREATE CERTIFICATE statement."
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: maghan
ms.date: 09/02/2025
ms.service: sql
ms.subservice: configuration
ms.topic: how-to
---

# How to: Create certificates for Service Broker transport security (Transact-SQL)

[!INCLUDE [sql-asdbmi](../../includes/applies-to-version/sql-asdbmi.md)]

To set up Service Broker transport security for an instance of SQL Server, you first create a certificate in the `master` database by using the `CREATE CERTIFICATE` Transact-SQL statement. This statement creates both a public key and a private key. You can also use the `CREATE CERTIFICATE` statement to load an existing X.509 certificate. For more information on creating certificates, see [CREATE CERTIFICATE](../../t-sql/statements/create-certificate-transact-sql.md). After creating the certificate, use the `CREATE ENDPOINT` or `ALTER ENDPOINT` statement to set the Service Broker endpoint to use the new certificate.

For more information using certificates for Service Broker transport security, see [How to: Allow Service Broker network access by using certificates](how-to-allow-service-broker-network-access-by-using-certificates-transact-sql.md).

## Create a certificate for Service Broker transport security

- Create a certificate in the `master` database.

## Examples

```sql
USE master;
GO

-- Create a certificate owned by dbo.
CREATE CERTIFICATE TransportSecurity
    AUTHORIZATION [dbo]
    ENCRYPTION BY PASSWORD = '<password>'
    WITH SUBJECT = 'Instance certificate for transport security';
GO
```

## Related content

- [CREATE CERTIFICATE (Transact-SQL)](../../t-sql/statements/create-certificate-transact-sql.md)
- [CREATE ENDPOINT (Transact-SQL)](../../t-sql/statements/create-endpoint-transact-sql.md)
- [ALTER ENDPOINT (Transact-SQL)](../../t-sql/statements/alter-endpoint-transact-sql.md)
