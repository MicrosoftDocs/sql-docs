---
description: "Configure Dialog Security for Event Notifications"
title: "Configure Dialog Security for Event Notifications | Microsoft Docs"
ms.custom: ""
ms.date: "03/09/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: security
ms.topic: conceptual
helpviewer_keywords: 
  - "event notifications [SQL Server], security"
ms.assetid: 12afbc84-2d2a-4452-935e-e1c70e8c53c1
author: VanMSFT
ms.author: vanto
---
# Configure Dialog Security for Event Notifications
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  [!INCLUDE[ssSB](../../includes/sssb-md.md)] dialog security should be configured for event notifications that send messages to a service broker on a remote server. Dialog security must be manually configured according to the [!INCLUDE[ssSB](../../includes/sssb-md.md)] dialog full-security model. The full-security model enables encryption and decryption of messages that are sent to and from remote servers. Although event notifications are sent in one direction, other messages, such as errors, are also returned in the opposite direction.  
  
## Configuring Dialog Security for Event Notifications  
 The process required to implement dialog security for event notification is described in the following steps. These steps include actions to take on both the source server and the target server. The source server is the server on which the event notification is being created. The target server is the server that receives the event notification message. You must complete the actions in each step for both the source server and the target server before you continue to the next step.  
  
> [!IMPORTANT]  
>  All certificates must be created with valid start and expiration dates.  
  
 **Step 1: Establish a TCP port number and target service name.**  
  
 Establish the TCP port on which both the source server and the target server will receive messages. You must also determine the name of the target service.  
  
 **Step 2: Configure encryption and certificate sharing for database-level authentication.**  
  
 Complete the following actions on both the source and target servers.  
  
|Source server|Target server|  
|-------------------|-------------------|  
|Choose or create a database to hold the event notification and master key.|Choose or create a database to hold the master key.|  
|If no master key exists for the source database, [create a master key](../../t-sql/statements/create-master-key-transact-sql.md). A master key is required on both the source and target databases to help secure their respective certificates.|If no master key exists for the target database, create a master key.|  
|[Create a login](../../t-sql/statements/create-login-transact-sql.md) and a corresponding [user](../../t-sql/statements/create-user-transact-sql.md) for the source database.|Create a login and a corresponding user for the target database.|  
|[Create a certificate](../../t-sql/statements/create-certificate-transact-sql.md) that is owned by the user of the source database.|Create a certificate that is owned by the user of the target database.|  
|[Back up the certificate](../../t-sql/statements/backup-certificate-transact-sql.md) to a file that can be accessed by the target server.|Back up the certificate to a file that can be accessed by the source server.|  
|[Create a user](../../t-sql/statements/create-user-transact-sql.md), specifying the user of the target database, and WITHOUT LOGIN. This user will own the target database certificate to be created from the backup file. The user does not have to be mapped to a login, because the only purpose of this user is to own the target database certificate created in step 3 that follows.|Create a user, specifying the user of the source database, and WITHOUT LOGIN. This user will own the source database certificate to be created from the backup file. The user does not have to be mapped to a login, because the only purpose of this user is to own the source database certificate created in step 3 that follows.|  
  
 **Step 3: Share certificates and grant permissions for database-level authentication.**  
  
 Complete the following actions on both the source and target servers.  
  
|Source server|Target server|  
|-------------------|-------------------|  
|[Create a certificate](../../t-sql/statements/create-certificate-transact-sql.md) from the backup file of the target certificate, specifying the target database user as the owner.|Create a certificate from the backup file of the source certificate, specifying the source database user as the owner.|  
|[Grant permission](../../t-sql/statements/grant-transact-sql.md) to create the event notification to the source database user. For more information about this permission, see [CREATE EVENT NOTIFICATION &#40;Transact-SQL&#41;](../../t-sql/statements/create-event-notification-transact-sql.md).|Grant REFERENCES permission to the target database user on the existing event notifications [!INCLUDE[ssSB](../../includes/sssb-md.md)] contract: `https://schemas.microsoft.com/SQL/Notifications/PostEventNotification`.|  
|[Create a remote service binding](../../t-sql/statements/create-remote-service-binding-transact-sql.md) to the target service and specify the credentials of the target database user. The remote service binding guarantees that the public key in the certificate owned by the source database user will authenticate messages that are sent to the target server.|[Grant](../../t-sql/statements/grant-transact-sql.md) CREATE QUEUE, CREATE SERVICE, and CREATE SCHEMA permissions to the target database user.|  
||If not already connected to the database as the target database user, do so now.|  
||[Create a queue](../../t-sql/statements/create-queue-transact-sql.md) to receive the event notification messages and [create a service](../../t-sql/statements/create-service-transact-sql.md) to deliver the messages.|  
||[Grant SEND permission](../../t-sql/statements/grant-transact-sql.md) on the target service to the source database user.|  
|Provide the service broker identifier of the source database to the target server. This identifier can be obtained by querying the **service_broker_guid** column of the [sys.databases](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md) catalog view. For a server-level event notification, use the service broker identifier of **msdb**.|Provide the service broker identifier of the target database to the source server.|  
  
 **Step 4: Create routes and set up server-level authentication.**  
  
 Complete the following actions on both the source and target servers.  
  
|Source server|Target server|  
|-------------------|-------------------|  
|[Create a route](../../t-sql/statements/create-route-transact-sql.md) to the target service, and specify the service broker identifier of the target database and the agreed-upon TCP port number.|Create a route to the source service, and specify the service broker identifier of the source database and the agreed-upon TCP port number. To specify the source service, use the following supplied service: `https://schemas.microsoft.com/SQL/Notifications/EventNotificationService`.|  
|Switch to the **master** database to configure server-level authentication.|Switch to the **master** database to configure server-level authentication.|  
|If no master key exists for the **master** database, [create a master key](../../t-sql/statements/create-master-key-transact-sql.md).|If no master key exists for the **master** database, create a master key.|  
|[Create a certificate](../../t-sql/statements/create-certificate-transact-sql.md) that authenticates the database.|Create a certificate that authenticates the database.|  
|[Back up the certificate](../../t-sql/statements/backup-certificate-transact-sql.md) to a file that can be accessed by the target server.|Back up the certificate to a file that can be accessed by the source server.|  
|[Create an endpoint](../../t-sql/statements/create-endpoint-transact-sql.md), and specify the agreed-upon TCP port number, FOR SERVICE_BROKER (AUTHENTICATION = CERTIFICATE *certificate_name*), and the name of the authenticating certificate.|Create an endpoint, and specify the agreed-upon TCP port number, FOR SERVICE_BROKER (AUTHENTICATION = CERTIFICATE *certificate_name*), and the name of the authenticating certificate.|  
|[Create a login](../../t-sql/statements/create-login-transact-sql.md), and specify the login of the target server.|Create a login, and specify the login of the source server.|  
|[Grant CONNECT permission](../../t-sql/statements/grant-transact-sql.md) on the endpoint to the target authenticator login.|Grant CONNECT permission on the endpoint to the source authenticator login.|  
|[Create a user](../../t-sql/statements/create-user-transact-sql.md), and specify the target authenticator login.|Create a user, and specify the source authenticator login.|  
  
 **Step 5: Share certificates for server-level authentication and create the event notification.**  
  
 Complete the following actions on both the source and target servers.  
  
|Source server|Target server|  
|-------------------|-------------------|  
|[Create a certificate](../../t-sql/statements/create-certificate-transact-sql.md) from the backup file of the target certificate, specifying the target authenticator user as the owner.|Create a certificate from the backup file of the source certificate, specifying the source authenticator user as the owner.|  
|Switch to the source database on which to create the event notification, and if you are not already connected as the source database user, do so now.|Switch to the target database to receive event notification messages.|  
|[Create the event notification](../../t-sql/statements/create-event-notification-transact-sql.md), and specify the broker service and identifier of the target database.||  
  
## See Also  
 [GRANT &#40;Transact-SQL&#41;](../../t-sql/statements/grant-transact-sql.md)   
 [BACKUP CERTIFICATE &#40;Transact-SQL&#41;](../../t-sql/statements/backup-certificate-transact-sql.md)   
 [sys.databases &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md)   
 [Encryption Hierarchy](../../relational-databases/security/encryption/encryption-hierarchy.md)   
 [Implement Event Notifications](../../relational-databases/service-broker/implement-event-notifications.md)   
 [CREATE MASTER KEY &#40;Transact-SQL&#41;](../../t-sql/statements/create-master-key-transact-sql.md)   
 [CREATE LOGIN &#40;Transact-SQL&#41;](../../t-sql/statements/create-login-transact-sql.md)   
 [CREATE USER &#40;Transact-SQL&#41;](../../t-sql/statements/create-user-transact-sql.md)   
 [CREATE CERTIFICATE &#40;Transact-SQL&#41;](../../t-sql/statements/create-certificate-transact-sql.md)   
 [CREATE REMOTE SERVICE BINDING &#40;Transact-SQL&#41;](../../t-sql/statements/create-remote-service-binding-transact-sql.md)   
 [GRANT &#40;Transact-SQL&#41;](../../t-sql/statements/grant-transact-sql.md)   
 [CREATE ROUTE &#40;Transact-SQL&#41;](../../t-sql/statements/create-route-transact-sql.md)   
 [CREATE QUEUE &#40;Transact-SQL&#41;](../../t-sql/statements/create-queue-transact-sql.md)   
 [CREATE SERVICE &#40;Transact-SQL&#41;](../../t-sql/statements/create-service-transact-sql.md)   
 [CREATE ENDPOINT &#40;Transact-SQL&#41;](../../t-sql/statements/create-endpoint-transact-sql.md)   
 [CREATE EVENT NOTIFICATION &#40;Transact-SQL&#41;](../../t-sql/statements/create-event-notification-transact-sql.md)  
  
  
