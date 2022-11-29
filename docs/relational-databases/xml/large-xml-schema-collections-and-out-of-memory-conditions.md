---
title: "Out-of-memory with large XML schema collections"
description: Learn about solutions for handling out-of-memory conditions when loading or dropping a large XML schema collection.
ms.date: 05/05/2022
ms.service: sql
ms.reviewer: randolphwest
ms.subservice: xml
ms.topic: conceptual
helpviewer_keywords:
  - "out-of-memory conditions"
  - "XML schema collections [SQL Server], large"
author: MikeRayMSFT
ms.author: mikeray
ms.custom: "seo-lt-2019"
---
# Large XML schema collections and out-of-memory conditions

[!INCLUDE [SQL Server Azure SQL Database](../../includes/applies-to-version/sql-asdb.md)]

During a call to the built-in XML_SCHEMA_NAMESPACE() function on a large XML schema collection, or when you try to drop large XML schema collections, an out-of-memory condition may occur. The following are solutions you can use to handle this:

- When the system load is light, use the DROP_XML_SCHEMA_COLLECTION command. If this fails, put the database in single-user mode by using the ALTER DATABASE statement and trying DROP XML SCHEMA COLLECTION again. If the XML schema collection exists in `master`, `model`, or `tempdb`, a server restart is required for single-user mode.

- When you call the XML_SCHEMA_NAMESPACE, you can try to retrieve a single XML schema namespace, you can try the call when the system load is lighter, or you can try the call in single-user mode.

## See also

- [Requirements and Limitations for XML Schema Collections on the Server](../../relational-databases/xml/requirements-and-limitations-for-xml-schema-collections-on-the-server.md)
