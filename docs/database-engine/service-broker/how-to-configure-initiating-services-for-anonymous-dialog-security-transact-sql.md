---
title: "How to: Configure Initiating Services for Anonymous Dialog Security (Transact-SQL)"
description: "SQL Server uses dialog security for any conversation to a service for which a remote service binding exists."
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: mikeray, maghan
ms.date: "03/30/2022"
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
---

# How to: Configure Initiating Services for Anonymous Dialog Security (Transact-SQL)

[!INCLUDE [sql-asdbmi](../../includes/applies-to-version/sql-asdbmi.md)]

SQL Server uses dialog security for any conversation to a service for which a remote service binding exists. If the database that hosts the target service does not contain a user that corresponds to the user that created the dialog, the dialog uses anonymous security.

> [!NOTE]
> Only install certificates from trusted sources.

## To make sure that an initiating service uses dialog security

1. Obtain a certificate for a user in the remote database from a trusted source.

2. Create a user without a login.

3. Install the certificate for the remote service. The user created in step 3 owns the certificate. By default the certificate is active for BEGIN DIALOG.

4. Create a remote service binding that specifies the user and the target service. For anonymous dialog security, the remote service binding specifies ANONYMOUS = ON.

## Example

This example configures anonymous dialog security for conversations between the service named OrderParts in the current instance and the service named SupplierOrders in the remote instance.

[!INCLUDE [SQL Server Service Broker AdventureWorks2008R2](../../includes/service-broker-adventureworks-2008-r2.md)]

```sql
    USE AdventureWorks2008R2 ;
    GO

    -- Given a certificate for a remote user for the remote service
    -- SupplierOrders, create a remote service binding for
    -- the service.  The remote user will be granted permission
    -- to send messages to the local service OrderParts.
    -- This example assumes that the certificate for the service
    -- is saved in the file'C:\Certificates\SupplierOrders.cer' and that
    -- the initiating service already exists.


    -- Create a user without a login.

    CREATE USER [SupplierOrdersUser]
        WITHOUT LOGIN ;
    GO

    -- Install a certificate for the owner of the service
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

    -- Since anonymous is ON, the credentials for the user
    -- that begins the conversation are not used for the
    -- conversation.

    CREATE REMOTE SERVICE BINDING [SupplierOrdersBinding]
        TO SERVICE 'SupplierOrders'
        WITH USER = [SupplierOrdersUser],
             ANONYMOUS = ON ;
    GO
```

## See also

- [How to: Configure Permissions for a Local Service (Transact-SQL)](how-to-configure-permissions-for-a-local-service-transact-sql.md)
- [CREATE CERTIFICATE (Transact-SQL)](../../t-sql/statements/create-certificate-transact-sql.md)
- [CREATE USER (Transact-SQL)](../../t-sql/statements/create-user-transact-sql.md)
- [CREATE REMOTE SERVICE BINDING (Transact-SQL)](../../t-sql/statements/create-remote-service-binding-transact-sql.md)
- [OPEN MASTER KEY (Transact-SQL)](../../t-sql/statements/open-master-key-transact-sql.md)
- [CLOSE MASTER KEY (Transact-SQL)](../../t-sql/statements/close-master-key-transact-sql.md)
