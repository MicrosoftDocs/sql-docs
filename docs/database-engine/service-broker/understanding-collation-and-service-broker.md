---
title: Understand Collation and Service Broker
description: "Service Broker is designed to let services and applications in instances with different collation configurations communicate easily and efficiently."
author: adbadram
ms.author: adbadram
ms.reviewer: vanto
ms.date: 09/10/2025
ms.service: sql
ms.subservice: configuration
ms.topic: concept-article
---

# Understand collation and Service Broker

[!INCLUDE [sql-asdbmi](../../includes/applies-to-version/sql-asdbmi.md)]

Service Broker is designed to let services and applications communicate easily and efficiently in instances with different collation configurations. The database that hosts a service that sends a message might not use the same collation as the database that hosts the service that receives the message. Therefore, Service Broker uses a consistent collation for names, regardless of the collation of the database that hosts the service. To remove collation information from the communication process, Service Broker uses a byte-by-byte comparison to match service names, contract names, and message type names. By matching names as sequences of bytes, Service Broker makes it simple for services to exchange messages correctly without the extra overhead of exchanging collation information.

The byte-by-byte match is effectively a binary comparison that doesn't consider the current collation. For this reason, many broker services find it convenient to follow the recommendations in [Name Service Broker objects](naming-service-broker-objects.md). An application that follows these guidelines and treats all names as case-sensitive should function correctly regardless of differences in collation between the database that hosts the target service and the database that hosts the initiating service.

## Queue collation considerations

Queues use a consistent collation regardless of the default collation of the SQL Server instance or the default collation of the database that hosts the queue. If a queue is the target of a `SELECT` statement that includes a `JOIN` statement with another table in the database, such as a table used to maintain state, you might be required to explicitly specify the collation for the comparison.

For example, an application that uses message retention might need to preserve some messages for a conversation before the application ends the conversation. The following Transact-SQL code sample saves all messages for a given conversation that have a message type name in the table `AuditedMessageTypes`:

```sql
IF @messageTypeName = 'https://schemas.microsoft.com/SQL/ServiceBroker/EndDialog'
    BEGIN
        INSERT INTO dbo.AuditRecord
        SELECT q.message_type_name,
               q.message_body
        FROM dbo.ApplicationQueue AS q
             INNER JOIN   dbo.AuditedMessageTypes AS am
                 ON am.message_type_name = q.message_type_name COLLATE Latin1_General_BIN
                AND q.conversation_handle = @conversationHandle;
        END CONVERSATION @conversationHandle;
    END
```

The `SELECT` statement explicitly specifies a binary comparison for matching the message type names. Because a queue doesn't use the database default collation, the `COLLATE` clause is required for the comparison `am.message_type_name = q.message_type_name` to succeed. The statement specifies a binary collation, `Latin1_General_BIN`, to match the behavior of the internal comparison that Service Broker uses to match service names.

## Application collation considerations

Service Broker transmits the message body as binary data and doesn't modify the content of the message. If applications exchange data that's collation-sensitive, the applications must handle any collation differences. Applications that exchange text generally use Unicode types to help minimize collation problems.

## Related content

- [COLLATE (Transact-SQL)](../../t-sql/statements/collations.md)
