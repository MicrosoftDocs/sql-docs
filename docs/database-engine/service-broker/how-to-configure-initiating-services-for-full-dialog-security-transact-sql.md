---
title: "How to: Configure Initiating Services for Full Dialog Security (Transact-SQL)"
description: "SQL Server uses dialog security for any conversation to a service for which a remote service binding exists in the database that hosts the initiating service.  If the database that hosts the target service contains a user that corresponds to the user that created the dialog, and the remote service binding does not specify anonymous security, then the dialog uses full security."
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: mikeray, maghan
ms.date: "03/30/2022"
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
---

# How to: Configure Initiating Services for Full Dialog Security (Transact-SQL)

[!INCLUDE [sql-asdbmi](../../includes/applies-to-version/sql-asdbmi.md)]

SQL Server uses dialog security for any conversation to a service for which a remote service binding exists in the database that hosts the initiating service. If the database that hosts the target service contains a user that corresponds to the user that created the dialog, and the remote service binding does not specify anonymous security, then the dialog uses full security.

To make sure that an initiating service uses dialog security, you create a remote service binding for the service. For SQL Server to use full security, the remote service binding must not specify anonymous security, and the target database must be configured to use full security for this service.

## To configure an initiating service for full dialog security

1. Obtain a certificate for the owner of the target service in the remote database from a trusted source. Typically, this involves sending the certificate using encrypted e-mail or transferring the certificate on physical media such as a floppy disk.

   > [!NOTE]
   > Only install certificates from trusted sources.

   > [!NOTE]
   > The certificate must be encrypted with the master key for the database. For more information, see [CREATE MASTER KEY (Transact-SQL)](../../t-sql/statements/create-master-key-transact-sql.md).

2. Create a user without a login for the remote service.

3. Install the certificate for the remote service user. The user created in the previous step owns the certificate.

4. Create a remote service binding that specifies the remote service user and the service.

5. Create a user without a sign in to own the local service.

6. Create a certificate for the local service. The user created in the previous step owns the certificate.

   > [!NOTE]
   > The certificate must be encrypted with the master key for the database. For more information, see [CREATE MASTER KEY (Transact-SQL)](../../t-sql/statements/create-master-key-transact-sql.md).

7. Back up the certificate.

   > [!NOTE]
   > Only back up the certificate for this user. Do not back up or distribute the private key associated with the certificate.

8. Provide the certificate and the name of the initiating service to the database administrator for the remote database. For example, you may exchange the certificate on physical media such as a floppy disk or a CD-ROM, by placing the certificate on a file share, or through secured e-mail.

   > [!NOTE]
   > For SQL Server to use full dialog security, the certificate must be installed in the remote database, and the user created in step 7 must be the user who begins the conversation.

## Example

[!INCLUDE [SQL Server Service Broker AdventureWorks2008R2](../../includes/service-broker-adventureworks-2008-r2.md)]

```sql
    USE AdventureWorks2008R2 ;
    GO

    --------------------------------------------------------------------
    -- The first part of the script configures security to allow the
    -- remote service to send messages in this database. The script creates
    -- a user in this database, loads the certificate for the remote service,
    -- grants permission to the user, and creates a remote service binding.

    -- Given a certificate for the owner of the remote target service
    -- SupplierOrders, create a remote service binding for
    -- the service.  The remote user will be granted permission
    -- to send messages to the local service OrderParts.
    -- This example assumes that the certificate for the service
    -- is saved in the file'C:\Certificates\SupplierOrders.cer' and that
    -- the initiating service already exists.


    -- Create a user without a login.  For convenience,
    -- the name of the user is based on the name of the
    -- the remote service.

    CREATE USER [SupplierOrdersUser]
        WITHOUT LOGIN ;
    GO

    -- Install a certificate for a user
    -- in the remote database. The certificate is
    -- provided by the owner of the remote service. The
    -- user for the remote service owns the certificate.

    CREATE CERTIFICATE [SupplierOrdersCertificate]
        AUTHORIZATION [SupplierOrdersUser]
        FROM FILE='C:\Certificates\SupplierOrders.cer' ;
    GO

    -- Create the remote service binding. Notice
    -- that the user specified in the binding
    -- does not own the binding itself.

    -- Creating this binding specifies that messages from
    -- this database are secured using the certificate for
    -- the [SupplierOrdersUser] user.

    -- When the anonymous option is omitted, anonymous is OFF.
    -- Therefore, the credentials for the user that begins
    -- are used in the remote database.

    CREATE REMOTE SERVICE BINDING [SupplierOrdersBinding]
        TO SERVICE 'SupplierOrders'
        WITH USER = [SupplierOrdersUser] ;
    GO

    --------------------------------------------------------------------
    -- The second part of the script creates a local user that will begin
    -- conversations to the remote service. The certificate for this
    -- user must be provided to the owner of the remote service.


    -- Create a user without a login for the local service.

    CREATE USER [OrderPartsUser]
        WITHOUT LOGIN ;
    GO

    -- Create a certificate for the local service.
    CREATE CERTIFICATE [OrderPartsCertificate]
        AUTHORIZATION [OrderPartsUser]
        WITH SUBJECT = 'Certificate for the order service user.';
    GO

    -- Make this user the owner of the initiator service.

    ALTER AUTHORIZATION ON SERVICE::OrderParts TO OrderPartsUser

    -- Backup the certificate for the user that initiates the
    -- conversation. This example assumes that the certificate
    -- is named OrderServiceCertificate.
    BACKUP CERTIFICATE [OrderPartsCertificate]
        TO FILE = 'C:\Certificates\OrderParts.cer' ;
    GO

    -- Grant RECEIVE permissions on the queue for the service.
    -- This allows the local user to begin conversations from
    -- services that use the queue.

    GRANT RECEIVE ON [OrderPartsQueue] TO [OrderPartsUser] ;
    GO
```

## See also

- [How to: Configure Target Services for Full Dialog Security (Transact-SQL)](how-to-configure-target-services-for-full-dialog-security-transact-sql.md)
- [How to: Configure Permissions for a Local Service (Transact-SQL)](how-to-configure-permissions-for-a-local-service-transact-sql.md)
- [CREATE LOGIN (Transact-SQL)](../../t-sql/statements/create-login-transact-sql.md)
- [CREATE USER (Transact-SQL)](../../t-sql/statements/create-user-transact-sql.md)
- [CREATE REMOTE SERVICE BINDING (Transact-SQL)](../../t-sql/statements/create-remote-service-binding-transact-sql.md)
- [Encryption Hierarchy](../../relational-databases/security/encryption/encryption-hierarchy.md)
