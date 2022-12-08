---
title: Creating Service Broker Message Types
description: "A message type defines the name of a specific kind of message and the validation that Service Broker performs on that kind of message."
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: mikeray, maghan
ms.date: "03/30/2022"
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
---

# Creating Service Broker Message Types

[!INCLUDE [sql-asdbmi](../../includes/applies-to-version/sql-asdbmi.md)]

A message type defines the name of a specific kind of message and the validation that Service Broker performs on that kind of message. To determine the message types that your application will use, you first plan out the tasks that your application must perform, and the data that is necessary to perform each task.

The most common approach for an application is to structure the messages so that each message includes the information required for one step of the task. When each message contains the information for one step of the task, the application can easily receive the message, complete the step, and send a response within a single transaction. Therefore, for many applications, the easiest way to determine the message types and the content for the message is to determine the transaction boundaries for the tasks performed by the application. Each distinct step is a transaction, and each transaction corresponds to a message type exchanged between the services. Status information, results, or output are also message types.

The Service Broker communications protocols are designed to work with this messaging style. The Dialog Protocol fragments large messages for transit and guarantees that large messages do not prevent small messages from being transmitted.

## Choosing a Validation Type

The validation specified for the message depends on the content of the message. A common practice is to use the most restrictive validation available during testing, and then to choose less-restrictive validation to improve performance when the application is deployed. For example, it is possible to exchange a typed XML document as the body of a message that specifies a validation of NONE. In this case, your application validates the message when processing the XML.

The network format for a message includes the name of the message type. Therefore, message type names are often chosen to avoid collation issues and naming conflicts. For more information on naming, see [Naming Service Broker Objects](naming-service-broker-objects.md).

## Indicating Success and Failure

An application typically does not define new message types to indicate success or failure. Instead, use the END CONVERSATION statement to indicate that the conversation is complete and that the task succeeded. If the task failed, include the WITH ERROR option to return an error message on the conversation.

In general, only one of the participants in the conversation should end the conversation when the task is complete. The other participant only issues an END CONVERSATION in response to an End Dialog or Error message. The documentation for a service generally specifies which participant ends the conversation if the conversation completes successfully. Providing this documentation helps to avoid problems where neither participant ends the conversation, or where one participant ends the conversation while the other participant is still performing tasks. Both endpoints must be able to process error messages because internal Service Broker messages are delivered to both endpoints. For example, if the dialog lifetime expires before the dialog is closed, both endpoints receive a Service Broker error message.

Either participant can end a conversation with an error at any time. For a discussion of handling Service Broker error messages, see [Handling Service Broker Error Messages](handling-service-broker-error-messages.md).

## See also

- [CREATE MESSAGE TYPE (Transact-SQL)](../../t-sql/statements/create-message-type-transact-sql.md)
- [Service Broker Communication Protocols](service-broker-communication-protocols.md)