---
title: "XML System Stored Procedures"
description: Learn about the XML system stored procedures provided by SQL Server that are used to write queries with OPENXML.
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: randolphwest
ms.date: 05/05/2022
ms.service: sql
ms.subservice: xml
ms.topic: conceptual
helpviewer_keywords:
  - "system stored procedures [SQL Server], XML"
  - "sp_xml_removedocument"
  - "OPENXML statement, system stored procedures"
  - "sp_xml_preparedocument"
  - "XML [SQL Server], system stored procedures"
---
# XML system stored procedures

[!INCLUDE [SQL Server Azure SQL Database](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

SQL Server provides the following system stored procedures that are used together with OPENXML:

- [sp_xml_preparedocument &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-xml-preparedocument-transact-sql.md)

- [sp_xml_removedocument &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-xml-removedocument-transact-sql.md)

To write queries by using OPENXML, you must first create an internal representation of the XML document by calling `sp_xml_preparedocument`. The stored procedure returns a handle to the internal representation of the XML document. This handle is then passed to OPENXML. OPENXML provides rowset views of the document based on XPaths. Specifically, this is one row pattern and one or more column patterns.

> [!NOTE]
>  The document handle that is returned by `sp_xml_preparedocument` is valid for the duration of the session.

The internal representation of an XML document can be removed from memory by calling the `sp_xml_removedocument` system stored procedure.

## See also

- [OPENXML &#40;Transact-SQL&#41;](../../t-sql/functions/openxml-transact-sql.md)
- [OPENXML &#40;SQL Server&#41;](../../relational-databases/xml/openxml-sql-server.md)
