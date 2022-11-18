---
description: "MSSQLSERVER_1785"
title: MSSQLSERVER_1785
ms.custom: ""
ms.date: 12/25/2020
ms.service: sql
ms.reviewer: ramakoni1, pijocoder, suresh-kandoth, vencher, tejasaks, docast
ms.subservice: supportability
ms.topic: "reference"
helpviewer_keywords: 
  - "1785 (Database Engine error)"
ms.assetid: 
author: suresh-kandoth
ms.author: ramakoni
---
# MSSQLSERVER_1785
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

## Details

|Attribute|Value|
|---|---|
|Product Name|SQL Server|
|Event ID|1785|
|Event Source|MSSQLSERVER|
|Component|SQLEngine|
|Symbolic Name|CRTFKINVTOPO|
|Message Text|Introducing FOREIGN KEY constraint '%.ls' on table '%.ls' may cause cycles or multiple cascade paths. Specify ON DELETE NO ACTION or ON UPDATE NO ACTION, or modify other FOREIGN KEY constraints.|

## Explanation

You receive this error message because in SQL Server, a table cannot appear more than one time in a list of all the cascading referential actions that are started by either a `DELETE` or an `UPDATE` statement. The tree of cascading referential actions must only have one path to a particular table on the cascading referential actions tree.

An error message like the following is reported to the user:

> Server: Msg 1785, Level 16, State 1, Line 1 Introducing FOREIGN KEY constraint 'fk_two' on table 'table2' may cause cycles or multiple cascade paths. Specify ON DELETE NO ACTION or ON UPDATE NO ACTION, or modify other FOREIGN KEY constraints. Server: Msg 1750, Level 16, State 1, Line 1 Could not create constraint. See previous errors

## User action

To resolve this problem, create a foreign key that will create a single path to a table in a list of cascading referential actions.

You can enforce referential integrity in several ways. Declarative Referential Integrity (DRI) is the most basic way, but it is also the least flexible one. If you need more flexibility, but you still want a high degree of integrity, you can use triggers instead.

## More information

The following sample code is an example of a FOREIGN KEY creation attempt that generates the error message:

```sql
USE tempdb
GO

CREATE TABLE table1 (user_ID INTEGER NOT NULL PRIMARY KEY, user_name
CHAR(50) NOT NULL)
GO

CREATE TABLE table2 (author_ID INTEGER NOT NULL PRIMARY KEY, author_name
CHAR(50) NOT NULL, lastModifiedBy INTEGER NOT NULL, addedby INTEGER NOT NULL)
GO

ALTER TABLE table2 ADD CONSTRAINT fk_one FOREIGN KEY (lastModifiedby)
REFERENCES table1 (user_ID) ON DELETE CASCADE ON UPDATE cascade
GO

ALTER TABLE table2 ADD CONSTRAINT fk_two FOREIGN KEY (addedby)
REFERENCES table1(user_ID) ON DELETE NO ACTION ON UPDATE cascade
GO
--this fails with the error because it provides a second cascading path to table2.

ALTER TABLE table2 ADD CONSTRAINT fk_two FOREIGN KEY (addedby)
REFERENCES table1 (user_ID) ON DELETE NO ACTION ON UPDATE NO ACTION
GO
-- this works.
```

### See also

[Cascading Referential Integrity](../tables/primary-and-foreign-key-constraints.md#referential-integrity)
