---
title: Managing Security (Service Broker)
description: "Service Broker provides a flexible security framework for helping you secure your applications."
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: mikeray, maghan
ms.date: "03/30/2022"
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
---

# Managing Security (Service Broker)

[!INCLUDE [sql-asdbmi](../../includes/applies-to-version/sql-asdbmi.md)]

Service Broker provides a flexible security framework for helping you secure your applications. This topic explains considerations for managing Service Broker security.

## Planning Security

Each application has unique security requirements. Part of managing security is to carefully plan the requirements for your application. Transport security, dialog security, and the security infrastructure built into SQL Server work together to help you secure your application.

All applications use the security infrastructure built into SQL Server. Each operation in SQL Server occurs in a specific security context. In most cases, you create SQL Server database principals specifically for the application. This helps you to ensure that each step in the application runs in a security context with only the privileges necessary for that step. For example, the principal that you specify for internal activation needs execute permissions on the stored procedure that Service Broker activates. The stored procedure itself may impersonate a user that has RECEIVE permission for the queue and UPDATE permission for a particular table. You design your application so that, at each stage, the security context for the application does not have permission to perform unexpected operations.

Applications that send messages between SQL Server instances may use transport security, dialog security, or both. Transport security and dialog security provide distinctly different protections.

Service Broker dialog security provides end-to-end encryption and authorization for conversations between specific services. Therefore, dialog security helps protect data against inspection or modification in transit. Applications that transmit confidential or sensitive data, or that transmit messages over untrusted networks, should use dialog security. Dialog security can help a participant in the conversation identify the other participant in the conversation.

Because dialog security applies to specific services, you must configure dialog security for each service that uses dialog security. However, an instance may use dialog security for some conversations and allow other conversations to be transmitted unencrypted. For example, conversations to a service that updates customer information may use dialog security, whereas conversations that simply look up part number information may not require dialog security.

Service Broker uses the remote service bindings in the database that begins the conversation to determine the security for the conversation. Service Broker therefore uses the service name to determine the security for the service. In cases where more than one instance of the same target service exists, routing for the initiating services must be carefully managed so that initiating services only communicate with target services that contain matching certificates. All services with the same name must be configured with the same certificate.

Service Broker transport security prevents unauthorized network connections to Service Broker endpoints, detects alterations to messages in transit, and optionally provides point-to-point encryption. This helps protect your database against receiving unwanted messages. Because transport security applies to network connections, transport security automatically applies to all conversations between the SQL Server instances. Notice, however, that transport security does not provide end-to-end encryption, and does not provide authentication for individual conversations.

## Maintaining Security

Maintaining security for Service Broker applications consists of two main tasks--auditing the application configuration and replacing the certificates that the application uses.

Periodically audit the application to determine that the security configuration is unchanged, and that the security configuration meets the business needs for the application.

Dialog security relies on certificates for authentication and encryption. Transport security may also use certificates. A certificate has a specified time during which the certificate is valid. The certificate is not valid before this time begins or after this time expires. Service Broker does not use certificates that are not currently valid. In addition, SQL Server includes the ACTIVE FOR BEGIN_DIALOG option to make a certificate available to Service Broker. To update certificates, create or load new certificates with the active for begin dialog option set to OFF. Once all of the certificates are loaded, alter the current certificates in all databases to make the certificates unavailable to Service Broker. Then alter the new certificates by setting the ACTIVE FOR BEGIN_DIALOG option so that those certificates are available to Service Broker.

For more information on certificates, see [Certificates and Service Broker](certificates-and-service-broker.md) and [CREATE CERTIFICATE (Transact-SQL)](../../t-sql/statements/create-certificate-transact-sql.md).

## See also

- [Service Broker Dialog Security](service-broker-dialog-security.md)
- [Service Broker Transport Security](service-broker-transport-security.md)
