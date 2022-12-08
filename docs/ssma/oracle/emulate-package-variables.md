---
title: "Emulating Oracle Package Variables"
description: "Describes how SQL Server Migration Assistant (SSMA) for Oracle emulates Oracle Package Variables in SQL Server."
author: cpichuka
ms.service: sql
ms.subservice: ssma
ms.devlang: "sql"
ms.topic: "article"
ms.date: "1/22/2020"
ms.author: cpichuka
---

# Emulating Oracle Package Variables

Oracle supports encapsulating variables, types, stored procedures, and functions into a package. When you convert Oracle packages, you need to convert:

* Procedures and functions - both public and private
* Variables
* Cursors
* Initialization routines

This article describes how SQL Server Migration Assistant (SSMA) for Oracle converts package variables to SQL Server.

## Conversion basics

To store package variables, SSMA for Oracle uses stored procedures that reside in a special `ssma_oracle` schema along with `ssma_oracle.db_storage` table. This table is filtered by `SPID` (session identifier) and login time. This filtering enables you to distinguish between variables of different sessions.

At the beginning of each converted package procedure SSMA places a call to the `ssma_oracle.db_check_init_package` special procedure, which checks if the package is initialized and will initialize it if needed. Each initialization procedure cleans the storage table and sets default values for each package variable.

## Example

Consider the following example for converting several package variables:

```sql
CREATE OR REPLACE PACKAGE MY_PACKAGE
IS
    space varchar(1) := ' ';
    unitname varchar(128) := 'My Simple Package';
    ts date := sysdate;
END;
```

The SSMA converts it into the following Transact-SQL code:

```sql
CREATE PROCEDURE dbo.MY_PACKAGE$SSMA_Initialize_Package
AS
BEGIN
    EXECUTE ssma_oracle.db_clean_storage

    EXECUTE ssma_oracle.set_pv_varchar
        DB_NAME(),
        'DBO',
        'MY_PACKAGE',
        'SPACE',
        ' '

    EXECUTE ssma_oracle.set_pv_varchar
        DB_NAME(),
        'DBO',
        'MY_PACKAGE',
        'UNITNAME',
        'My Simple Package'

    DECLARE
        @temp datetime2

    SET @temp = sysdatetime()

    EXECUTE sysdb.ssma_oracle.set_pv_datetime2
      DB_NAME(),
      'DBO',
      'MY_PACKAGE',
      'TS',
      @temp
END
```

## Emulating Get and Set methods for Package Variables

Oracle uses `Get` and `Set` methods for the package variables, to avoid letting other subprograms read and write them directly. If there is a requirement to keep some variables available between subprogram calls in the same session, these variables are treated like global variables.

To overcome this scoping rule, SSMA for Oracle uses stored procedures like `ssma_oracle.set_pv_varchar` for each variable type. For accessing these variables, SSMA uses a set of transaction-independent `get_*` and `set_*` procedures and functions.

| Data type in Oracle | SSMA `Set` procedure           |
| ------------------- | ------------------------------ |
| VARCHAR             | `ssma_oracle.set_pv_varchar`   |
| DATE                | `ssma_oracle.set_pv_datetime2` |
| CHAR                | `ssma_oracle.set_pv_varchar`   |
| INT                 | `ssma_oracle.set_pv_float`     |
| FLOAT               | `ssma_oracle.set_pv_float`     |

To distinguish between variables from different sessions, SSMA stores the variables along with their `SPID` (session identifier) and session's login time. Thus the `get_*` and `set_*` procedures keeps the variables independent from the sessions running them.
