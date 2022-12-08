---
title: Service Broker Transport Security
description: "Service Broker transport security allows database administrators to restrict network connections to a database and can encrypt messages on the network."
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: mikeray, maghan
ms.date: "03/30/2022"
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
---

# Service Broker Transport Security

[!INCLUDE [sql-asdbmi](../../includes/applies-to-version/sql-asdbmi.md)]

Service Broker transport security allows database administrators to restrict network connections to a database and can encrypt messages on the network. Service Broker endpoints support both certificate-based authentication and Windows Authentication.

Transport security applies to the network connection between the two instances. Transport security controls which instances can communicate and provides encryption between two instances.

Transport security applies to the instance as a whole. Transport security does not secure the contents of individual messages, nor does transport security control access to individual services within an instance. Service Broker dialog security encrypts individual messages when the message leaves the sending instance until the messages reach the destination instance.

The type of authentication an instance uses depends on the AUTHENTICATION option for the Service Broker endpoint of each instance. When an endpoint specifies more than one authorization method, the exact authorization method used depends on the order in which the methods are specified for the instance initiating the connection. During negotiation each instance reports all their supported authentication types and algorithms. The initiator attempts the authentication methods supported by BOTH endpoints, in the order specified by the acceptor. This means that, for a long running conversation, messages may be exchanged over more than one connection and the authentication for the connection may differ depending on which instance initiates the conversation.

Service Broker endpoints support two kinds of encryption. As with authentication, the exact encryption method used for a connection depends on the order in which the methods are specified for the instance initiating the connection.

## See also

- [CREATE ENDPOINT (Transact-SQL)](../../t-sql/statements/create-endpoint-transact-sql.md)
- [CREATE CERTIFICATE (Transact-SQL)](../../t-sql/statements/create-certificate-transact-sql.md)
- [Service Broker Communication Protocols](service-broker-communication-protocols.md)