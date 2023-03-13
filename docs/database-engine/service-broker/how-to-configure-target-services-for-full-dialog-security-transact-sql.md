---
title: "How to: Configure Target Services for Full Dialog Security (Transact-SQL)"
description: "SQL Server uses dialog security for any conversation to a service for which a remote service binding exists in the database that hosts the initiating service."
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: mikeray, maghan
ms.date: "03/30/2022"
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
---

# How to: Configure Target Services for Full Dialog Security (Transact-SQL)

[!INCLUDE [sql-asdbmi](../../includes/applies-to-version/sql-asdbmi.md)]

SQL Server uses dialog security for any conversation to a service for which a remote service binding exists in the database that hosts the initiating service. When the database that hosts the target service contains a user that corresponds to the user that created the dialog, then the dialog uses full security.

To make sure that a target service uses dialog security, create a user for the initiating service to log in as. For each initiating service, create a user and install the certificate for the initiating user. Notice that a target service does not use a remote service binding.

## To configure a target service for full dialog security

1. Create a user without a login.

2. Create a certificate for the user.

   > [!NOTE]
   > The certificate must be encrypted with the master key. For more information, see [CREATE MASTER KEY (Transact-SQL)](../../t-sql/statements/create-master-key-transact-sql.md).

3. Make that user the owner of the target service.

4. Back up the certificate to a file.

   > [!NOTE]
   > Only back up the certificate for this user. Do not back up or distribute the private key associated with the certificate.

5. Grant permission for the target service user to receive messages from the queue that the target service uses.

6. Provide the certificate and the name of the initiating service to the database administrator for the remote database.

   > [!NOTE]
   > For SQL Server to use full dialog security, the certificate must be installed in the remote database, and the user for the certificate must be the user specified in the remote service binding for the target service.

7. Obtain a certificate for a user in the remote database from a trusted source. Typically, this involves sending the certificate using encrypted e-mail or transferring the certificate on physical media such as a floppy disk.

   > [!NOTE]
   > Only install certificates from trusted sources.

8. Create a user without a login.

9. Install the certificate for the initiating service. The user created in the previous step owns the certificate.

10. Create a user without a login for the initiating service certificate.

11. Grant permission for the initiating user to send messages to the target service.

## Example

[!INCLUDE [SQL Server Service Broker AdventureWorks2008R2](../../includes/service-broker-adventureworks-2008-r2.md)]

```sql
    USE AdventureWorks2008R2 ;
    GO

    --------------------------------------------------------------------
    -- The first part of the script configures security for the local user.
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

    -- Dump the certificate. Provide the certificate file
    -- to the administrator for the database that hosts
    -- the other service.

    BACKUP CERTIFICATE [SupplierOrdersCertificate]
       TO FILE = 'C:\Certificates\SupplierOrders.cer';
    GO
    -- Make this user the owner of the target service.

    ALTER AUTHORIZATION ON SERVICE::SupplierOrders TO [SupplierOrdersUser];
    GO

    -- Grant receive on the orders queue to the local user.

    GRANT RECEIVE ON SupplierOrdersQueue
        TO [SupplierOrdersUser];
    GO

    ---------------------------------------------------------------
    -- The second part of the script configures security in this
    -- database for the remote service. This consists of creating
    -- a user in this database, loading the certificate for the remote
    -- service, and granting permissions for the user.


    -- Create a user without a login.

    CREATE USER [OrderPartsUser]
        WITHOUT LOGIN;
    GO

    -- Install a certificate for the initiating user.
    -- The certificate is provided by the owner of the
    -- initiating service.

    CREATE CERTIFICATE [OrderPartsCertificate]
        AUTHORIZATION [OrderPartsUser]
        FROM FILE='C:\Certificates\OrderParts.cer';
    GO

    -- Grant send on the target service to the user for the
    -- initating service.

    GRANT SEND ON SERVICE::[SupplierOrders]
        TO [OrderPartsUser];
    GO
```

## See also

- [How to: Configure Initiating Services for Full Dialog Security (Transact-SQL)](how-to-configure-initiating-services-for-full-dialog-security-transact-sql.md)
- [How to: Configure Permissions for a Local Service (Transact-SQL)](how-to-configure-permissions-for-a-local-service-transact-sql.md)
- [CREATE CERTIFICATE (Transact-SQL)](../../t-sql/statements/create-certificate-transact-sql.md)
- [CREATE USER (Transact-SQL)](../../t-sql/statements/create-user-transact-sql.md)
- [CREATE REMOTE SERVICE BINDING (Transact-SQL)](../../t-sql/statements/create-remote-service-binding-transact-sql.md)
- [CREATE MASTER KEY (Transact-SQL)](../../t-sql/statements/create-master-key-transact-sql.md)
