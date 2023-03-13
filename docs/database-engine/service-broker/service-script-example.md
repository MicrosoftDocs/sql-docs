---
title: Service Script Example
description: "This Transact-SQL code sample defines a service that archives untyped XML documents."
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: mikeray, maghan
ms.date: "03/30/2022"
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
---

# Service Script Example

[!INCLUDE [sql-asdbmi](../../includes/applies-to-version/sql-asdbmi.md)]

This Transact-SQL code sample defines a service that archives untyped XML documents. Two scripts are included: the contract script and the service definition script. The contract script defines the message types and the contract for the service. The message type definition and the contract definition should match for both the initiating service and the target service. Therefore, the definitions are included in a separate service definition script that can be distributed to the databases that host the initiating service. The service definition script defines the service itself. This script should be run only in a database that implements the target service.

> [!NOTE]
> The service definition script defines the target service, but does not include an implementation of the service.## Contract Script

[!INCLUDE [SQL Server Service Broker AdventureWorks2008R2](../../includes/service-broker-adventureworks-2008-r2.md)]

```sql
    -- The contract script contains definitions that must be
    -- present for both the intiating service and the target
    -- service.

    USE AdventureWorks2008R2;
    GO

    -- Create messages for each broker-to-broker
    -- communication needed to complete the task.

    -- Message for the initiator to send XML
    -- to be archived.

    CREATE MESSAGE TYPE
        [//Adventure-Works.com/messages/ArchiveXML]
        VALIDATION = WELL_FORMED_XML ;
    GO

    -- Message to return event archiving information.

    CREATE MESSAGE TYPE
        [//Adventure-Works.com/messages/AcknowledgeArchiveXML]
        VALIDATION = WELL_FORMED_XML ;
    GO

    -- Create a service contract to structure
    -- an event archiving conversation, using
    -- the message types defined above.

    CREATE CONTRACT
        [//Adventure-Works.com/contracts/ArchiveXML/v1.0]
        (
            [//Adventure-Works.com/messages/ArchiveXML]
            SENT BY INITIATOR,
            [//Adventure-Works.com/messages/AcknowledgeArchiveXML]
            SENT BY TARGET
         ) ;
    GO
```

## Service Definition Script

```sql
    -- This script defines the target service. The
    -- objects created by this script are only
    -- required in a database that hosts the target
    -- service.

    USE AdventureWorks2008R2 ;
    GO

    -- Create the service queue that will receive
    -- messages for conversations that implement
    -- the ArchiveXML contract.

    CREATE QUEUE ArchiveQueue ;
    GO

    -- Create the service object that exposes the
    -- ArchiveEvents service contract and maps
    -- it to the ArchiveQueue service queue.

    CREATE SERVICE [//Adventure-Works.com/ArchiveService]
        ON QUEUE ArchiveQueue
        ([//Adventure-Works.com/contracts/ArchiveXML/v1.0]) ;
    GO
```

## See also

- [Creating Service Broker Objects](creating-service-broker-objects.md)
