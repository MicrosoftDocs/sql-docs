---
title: "Lesson 1: Create the Databases"
description: "In this lesson, you learn to create the databases and enable the trustworthy option."
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: maghan
ms.date: 09/03/2025
ms.service: sql
ms.subservice: configuration
ms.topic: how-to
---

# Lesson 1: Create the databases

[!INCLUDE [sql-asdbmi](../../includes/applies-to-version/sql-asdbmi.md)]

In this lesson, you learn to create the databases and enable the trustworthy option.

## Procedures

### Create the databases and set the trustworthy option

- Copy and paste the following code into a Query Editor window, then run it to create the databases for this tutorial. By default, new databases have the `ENABLE_BROKER` option set to on. Setting the `TRUSTWORTHY` option in the `InitiatorDB` lets you start conversations in that database that reference target services in the `TargetDB`.

  ```sql
  USE master;
  GO

  IF EXISTS (SELECT *
             FROM sys.databases
             WHERE name = N'TargetDB')
      DROP DATABASE TargetDB;
  GO

  CREATE DATABASE TargetDB;
  GO

  IF EXISTS (SELECT *
             FROM sys.databases
             WHERE name = N'InitiatorDB')
      DROP DATABASE InitiatorDB;
  GO

  CREATE DATABASE InitiatorDB;
  GO

  ALTER DATABASE InitiatorDB
      SET TRUSTWORTHY ON;
  GO
  ```

## Next step

You've successfully created the databases for the tutorial. Next, you configure the `TargetDB` with the objects that are required to support the target end of a Service Broker conversation.

> [!div class="nextstepaction"]
> [Lesson 2: Create the target conversation objects](lesson-2-creating-the-target-conversation-objects.md)

## Related content

- [CREATE DATABASE](../../t-sql/statements/create-database-transact-sql.md)
- [ALTER DATABASE (Transact-SQL)](../../t-sql/statements/alter-database-transact-sql.md)
- [Service Broker dialog security](service-broker-dialog-security.md)
