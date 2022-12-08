---
title: Security Overview (Service Broker)
description: "Service Broker helps you write highly scalable database applications that are also secure and reliable."
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: mikeray, maghan
ms.date: "03/30/2022"
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
---

# Security Overview (Service Broker)

[!INCLUDE [sql-asdbmi](../../includes/applies-to-version/sql-asdbmi.md)]

Service Broker helps you write highly scalable database applications that are also secure and reliable. Service Broker security allows services hosted by different SQL Server instances to communicate securely, even where the instances are on different computers that have no other trust relationship or where the source and destination computers are not connected to the same network at the same time.

Service Broker security relies on certificates. The general approach is to use certificates to establish the credentials of a remote database, and then to map operations from the remote database to a local user. The permissions for the local user apply to any operation on behalf of the remote service. The certificate is shared between databases. No other information for the user is shared.

Service Broker provides two distinct types of security - dialog security and transport security. Understanding these two types of security, and how they work together, will help you to design, deploy, and administer Service Broker applications.

- **Dialog security** - Encrypts messages in an individual dialog conversation and verifies the identities of participants in the dialog. Dialog security also provides remote authorization and message integrity checking. Dialog security establishes authenticated and encrypted communication between two services.

- **Transport security** - Prevents unauthorized databases from sending Service Broker messages to databases in the local instance. Transport security establishes an authenticated network connection between two databases.

Notice that the dialog protocol and the adjacent broker protocol are designed around passing messages between databases, rather than executing commands on a remote database. This style of communication allows Service Broker to provide services without requiring databases to share SQL Server logins or Windows security credentials.

For more information on certificates, see [CREATE CERTIFICATE (Transact-SQL)](../../t-sql/statements/create-certificate-transact-sql.md).

## Adventure Works Cycles Security Scenario

[!INCLUDE [SQL Server Service Broker AdventureWorks2008R2](../../includes/service-broker-adventureworks-2008-r2.md)]

In a sample business scenario, Adventure Works Cycles, a fictitious company, creates a Service Broker service for delivering parts orders to vendors. This service requires security for both Adventure Works and the vendors. Each vendor must be able to guarantee that only existing customers can submit orders. Adventure Works must be able to guarantee that only qualified vendors can receive orders. Messages between the AdventureWorks2008R2 database and a vendor must be encrypted so that no third-party can read a message. To ensure the highest level of security possible, only qualified vendors may connect to the AdventureWorks2008R2 database.

To satisfy the requirement that messages must be encrypted, Adventure Works and the vendors use Service Broker dialog security:

1. To set up dialog security, the AdventureWorks2008R2 administrator creates a local user named VendorOutgoing and creates a key pair for that user.

2. The administrator distributes the certificate that contains the public key of the key pair to vendors that need to access the service.

3. Each vendor installs the certificate from Adventure Works Cycles into the database and creates a user that owns the certificate.

4. The vendor then creates a key pair, and sends information on the service name for the vendor service and a certificate with the public key of that key pair to the AdventureWorks2008R2 administrator.

5. The AdventureWorks2008R2 administrator creates a user for each vendor and associates the certificate from that vendor with the user.

6. The administrator also creates a remote service binding for each vendor that associates the name of the vendor service with the user created for the vendor.

To satisfy the requirement that only qualified vendors can connect to the AdventureWorks2008R2 database, the AdventureWorks2008R2 administrator uses Service Broker transport security:

1. To set up transport security, the AdventureWorks2008R2 administrator creates a certificate in the master database of the SQL Server instance that will send messages.

2. The AdventureWorks2008R2 administrator sends the certificate to each vendor.

3. Each vendor administrator creates a user in the master database to own the certificate, and then installs the certificate in the SQL Server instance that will receive messages.

4. The vendor administrator next creates a certificate in the master database of the instance, and sends the public key for that user to the AdventureWorks2008R2 administrator.

5. Finally, the AdventureWorks2008R2 administrator creates a user in the master database to own each vendor public key certificate and installs each vendor certificate in the database.

The combination of transport security and dialog security helps the AdventureWorks2008R2 administrator meet the security requirements of this application. Notice that, in this scenario, vendors cannot log on to the AdventureWorks2008R2 database, and the Adventure Works administrator cannot log on to the vendor databases. Only Service Broker messages can be exchanged between the databases.

## See also

- [CREATE CERTIFICATE (Transact-SQL)](../../t-sql/statements/create-certificate-transact-sql.md)
- [Service Broker Communication Protocols](service-broker-communication-protocols.md)
