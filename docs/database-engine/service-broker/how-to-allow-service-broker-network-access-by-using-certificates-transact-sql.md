---
title: "How to: Allow Service Broker Network Access by Using Certificates (Transact-SQL)"
description: "To allow another instance to send messages using certificate-based Service Broker transport security, you create a user for the other instance and install the certificate for the other instance."
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: mikeray, maghan
ms.date: "03/30/2022"
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
---

# How to: Allow Service Broker Network Access by Using Certificates (Transact-SQL)

[!INCLUDE [sql-asdbmi](../../includes/applies-to-version/sql-asdbmi.md)]

To allow another instance to send messages using certificate-based Service Broker transport security, you create a user for the other instance and install the certificate for the other instance.

## To permit access from another instance using certificates

1. Obtain the certificate for the other instance from a trusted source. Typically, this involves sending the certificate using encrypted e-mail or transferring the certificate on physical media such as a floppy disk.

    > [!NOTE]
    > Only install certificates from trusted sources.

2. Create a login.

3. Create a user for the login in the **master** database.

4. Install the certificate for the other instance in the **master** database. The user created in step 3 owns the certificate.

5. Grant the login CONNECT access to the Service Broker endpoint.

6. Dump the certificate that is used for Service Broker transport security in the local instance.

   > [!NOTE]
   > Only dump the certificate used for transport security. Do not dump or distribute the private key associated with the certificate.

7. Provide the certificate to the administrator of the other database. The administrator of the remote database installs this certificate using steps 1-4 above.

Once access is configured in each instance, then communications between the two instances use Service Broker transport security when the endpoints for both instances are configured to allow transport security.

## Example

```sql
    USE master ;
    GO

    -- Create a login for the remote instance.

    CREATE LOGIN RemoteInstanceLogin
        WITH PASSWORD = '#gh!3A%!1@f' ;
    GO

    -- Create a user for the login in the master database.

    CREATE USER RemoteInstanceUser
        FOR LOGIN RemoteInstanceLogin ;
    GO

    -- Load the certificate from the file system. Notice that
    -- the login owns the certificate.

    CREATE CERTIFICATE RemoteInstanceCertificate
        AUTHORIZATION RemoteInstanceUser
        FROM FILE='C:\Certificates\AceBikeComponentsCertificate.cer' ;
    GO
    GRANT CONNECT ON ENDPOINT::ThisInstanceEndpoint to RemoteInstanceLogin ;
    GO
    -- Write the certificate from this instance
    -- to the file system. This command assumes
    -- that the certificate used by the Service Broker
    -- endpoint is named TransportSecurity.

    BACKUP CERTIFICATE TransportSecurity
        TO FILE = 'C:\Certificates\ThisInstanceCertificate.cer' ;
    GO
```

## See also

- [How to: Create Certificates for Service Broker Transport Security (Transact-SQL)](how-to-create-certificates-for-service-broker-transport-security-transact-sql.md)
- [CREATE CERTIFICATE (Transact-SQL)](../../t-sql/statements/create-certificate-transact-sql.md)
- [CREATE LOGIN (Transact-SQL)](../../t-sql/statements/create-login-transact-sql.md)
- [CREATE USER (Transact-SQL)](../../t-sql/statements/create-user-transact-sql.md)
- [BACKUP CERTIFICATE (Transact-SQL)](../../t-sql/statements/backup-certificate-transact-sql.md)
