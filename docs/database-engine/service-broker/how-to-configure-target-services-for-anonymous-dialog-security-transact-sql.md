---
title: "How to: Configure Target Services for Anonymous Dialog Security (Transact-SQL)"
description: "SQL Server uses dialog security for any conversation to a service for which a remote service binding exists in the database that hosts the initiating service. If the remote service binding specifies ANONYMOUS = ON, the dialog uses anonymous security."
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: mikeray, maghan
ms.date: "03/30/2022"
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
---

# How to: Configure Target Services for Anonymous Dialog Security (Transact-SQL)

[!INCLUDE [sql-asdbmi](../../includes/applies-to-version/sql-asdbmi.md)]

SQL Server uses dialog security for any conversation to a service for which a remote service binding exists in the database that hosts the initiating service. If the remote service binding specifies ANONYMOUS = ON, the dialog uses anonymous security. In this case, there is no need for the target database to contain a user for the initiating service. The initiating service acts as public in the target database.

## To configure a target service for anonymous dialog security

1. Create a user without a login.

2. Create a certificate for the user.

    > [!NOTE]
    > The certificate must be encrypted with the master key. For more information, see [CREATE MASTER KEY (Transact-SQL)](../../t-sql/statements/create-master-key-transact-sql.md).

3. Back up the certificate to a file.

    > [!NOTE]
    > Only back up the certificate for this user. Do not back up or distribute the private key associated with the certificate.

4. Grant permission for the target service user to receive messages from the queue that the target service uses.

5. Grant permission for public to send messages to the target service.

6. Provide the certificate and the name of the target service to the database administrator for the remote database.

## Example

[!INCLUDE [SQL Server Service Broker AdventureWorks2008R2](../../includes/service-broker-adventureworks-2008-r2.md)]

```sql
    USE AdventureWorks2008R2;
    GO

    --------------------------------------------------------------------
    -- This script configures security for a local user in the database.
    -- The script creates a user in this database, creates a certificate
    -- for the user, writes the certificate to the file system, and
    -- grants permissions to the user. Since this service is a target
    -- service, no remote service binding is necessary.

    -- Create a user without a login. For convenience,
    -- the name of the user is based on the name of the
    -- the remote service.

    CREATE USER [SupplierOrdersUser]
        WITHOUT LOGIN;
    GO

    -- Create a certificate for the initiating service
    -- to use to send messages to the target service.

    CREATE CERTIFICATE [SupplierOrdersCertificate]
        AUTHORIZATION [SupplierOrdersUser]
        WITH SUBJECT = 'Certificate for the SupplierOrders service user.';
    GO

    -- Backup the certificate. Provide the certificate file
    -- to the administrator for the database that hosts
    -- the other service.

    BACKUP CERTIFICATE [SupplierOrdersCertificate]
       TO FILE = 'C:\Certificates\SupplierOrders.cer';
    GO

    -- Grant receive on the orders queue to the local user.

    GRANT RECEIVE ON SupplierOrdersQueue
        TO [SupplierOrdersUser];
    GO

    -- Grant send on the service to public.

    GRANT SEND ON SERVICE::[SupplierOrders] TO public ;
```

## See also

- [How to: Configure Permissions for a Local Service (Transact-SQL)](how-to-configure-permissions-for-a-local-service-transact-sql.md)
- [How to: Configure Initiating Services for Anonymous Dialog Security (Transact-SQL)](how-to-configure-initiating-services-for-anonymous-dialog-security-transact-sql.md)
- [CREATE CERTIFICATE (Transact-SQL)](../../t-sql/statements/create-certificate-transact-sql.md)
- [CREATE USER (Transact-SQL)](../../t-sql/statements/create-user-transact-sql.md)
- [CREATE REMOTE SERVICE BINDING (Transact-SQL)](../../t-sql/statements/create-remote-service-binding-transact-sql.md)
- [CREATE MASTER KEY (Transact-SQL)](../../t-sql/statements/create-master-key-transact-sql.md)
