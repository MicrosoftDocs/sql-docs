---
title: Message Types
description: "Applications that use Service Broker communicate by sending messages to each other as part of a conversation."
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: mikeray, maghan
ms.date: "03/30/2022"
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
---

# Message Types

[!INCLUDE [sql-asdbmi](../../includes/applies-to-version/sql-asdbmi.md)]

Applications that use Service Broker communicate by sending messages to each other as part of a conversation. The participants in a conversation must agree on the name and content of each message. A message type object defines a name for a message type and defines the type of data that the message contains. Message types persist in the database where the message type is created. You create an identical message type in each database that participates in a conversation.

Each message type specifies the validation that SQL Server performs for messages of that type. SQL Server can validate that the message contains valid XML, that the message contains XML that conforms to a particular schema, or that the message contains no data at all. For arbitrary or binary data, the message type can specify that SQL Server does not validate the content of the message.

Validation is performed when the destination service receives the message. If the content of the message does not match the validation specified, Service Broker returns an error message to the service that sent the message.

> [!IMPORTANT]
> <P>Regardless of the validation specified, an application must verify that the content of a message is appropriate for the application before the program uses the data.</P>

For an empty message type, the body of the message must not contain data. For a message type that specifies well-formed XML, the body of the message must be well-formed XML. For a message type that specifies XML conforming to a particular schema collection, the message must contain well-formed XML that is valid for one of the schemas in the collection. For a message type that specifies no validation, SQL Server accepts any message content. This includes binary data, XML, or empty messages.

Service Broker offers a built-in message type named DEFAULT. If the message type is not specified in a Service Broker SEND command, the system will use the DEFAULT message type.

Service Broker includes system messages types that are used to report errors and the status of dialogs. For more information, see [Broker System Messages](broker-system-messages.md).

## See also

- [BEGIN DIALOG CONVERSATION (Transact-SQL)](../../t-sql/statements/begin-dialog-conversation-transact-sql.md)
- [CREATE CONTRACT (Transact-SQL)](../../t-sql/statements/create-contract-transact-sql.md)[CREATE MESSAGE TYPE (Transact-SQL)](../../t-sql/statements/create-message-type-transact-sql.md)
- [CREATE SERVICE (Transact-SQL)](../../t-sql/statements/create-service-transact-sql.md)
- [SEND (Transact-SQL)](../../t-sql/statements/send-transact-sql.md)
- [RECEIVE (Transact-SQL)](../../t-sql/statements/receive-transact-sql.md)
- [Contracts](contracts.md)
- [Dialog Conversations](dialog-conversations.md)
- [Messages](messages.md)
- [Queues](queues.md)
- [Services](services.md)