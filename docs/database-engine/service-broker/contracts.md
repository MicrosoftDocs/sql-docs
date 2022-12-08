---
title: Contracts
description: "A contract defines which message types an application uses to accomplish a particular task."
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: mikeray, maghan
ms.date: "03/30/2022"
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
---

# Contracts

[!INCLUDE [sql-asdbmi](../../includes/applies-to-version/sql-asdbmi.md)]

A contract defines which message types an application uses to accomplish a particular task. A contract is an agreement between two services about which messages each service sends to accomplish a particular task. Contract definitions persist in the database where the type is created.

You create an identical contract in each database that participates in a conversation. For example, if a human resources application wants to verify an employee ID, the service that requests the verification must know which types of messages the other service expects. The requesting service also must know which types of messages it can expect to receive so that it is prepared to process them.

The contract specifies which message types can be used to accomplish the desired work. The contract also specifies which participant in the conversation can use each message type. Some message types can be sent by either participant; other message types are restricted to be sent only by the initiator or only by the target. A contract must contain a message type sent by the initiator or a message type sent by either participant; otherwise, there is no way for the initiator to begin a conversation that uses the contract.

Service Broker also includes a built-in contract named DEFAULT. The DEFAULT contract contains only the message type **SENT BY ANY**. If no contract is specified in the BEGIN DIALOG statement, Service Broker uses the DEFAULT contract.

For example, a contract can have message types **SubmitRequest**, **ProcessRequest**, and **RequestStatus**. Only the initiating endpoint can use **SubmitRequest**, and only the target endpoint can send **ProcessRequest.** Either participant in the conversation can send the **RequestStatus** message type. The **RequestStatus** message type lets the participant either see where the target is in its processing, or check with the initiator on the status of any parallel processing relating to this request.

## See also

- [CREATE CONTRACT (Transact-SQL)](../../t-sql/statements/create-contract-transact-sql.md)
- [DROP CONTRACT (Transact-SQL)](../../t-sql/statements/drop-contract-transact-sql.md)
- [Message Types](message-types.md)
- [Creating Service Broker Contracts](creating-service-broker-contracts.md)