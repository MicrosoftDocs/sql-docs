---
title: "sp_scriptdynamicupdproc (Transact-SQL)"
description: sp_scriptdynamicupdproc generates the CREATE PROCEDURE statement that creates a dynamic update stored procedure.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 04/08/2024
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_scriptdynamicupdproc_TSQL"
  - "sp_scriptdynamicupdproc"
helpviewer_keywords:
  - "sp_scriptdynamicupdproc"
dev_langs:
  - "TSQL"
---
# sp_scriptdynamicupdproc (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Generates the `CREATE PROCEDURE` statement that creates a dynamic update stored procedure. The `UPDATE` statement within the custom stored procedure is built dynamically based on the `MCALL` syntax that indicates which columns to change. Use this stored procedure if the number of indexes on the subscribing table is growing and the number of columns being changed is small. This stored procedure is run at the Publisher on the publication database.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_scriptdynamicupdproc [ @artid = ] artid
[ ; ]
```

## Arguments

#### [ @artid = ] *artid*

The article ID. *@artid* is **int**, with no default.

## Result set

Returns a result set that consists of a single **nvarchar(4000)** column. The result set forms the complete `CREATE PROCEDURE` statement used to create the custom stored procedure.

## Remarks

`sp_scriptdynamicupdproc` is used in transactional replication. The default `MCALL` scripting logic includes all columns within the `UPDATE` statement and uses a bitmap to determine the columns that have changed. If a column didn't change, the column is set back to itself, which usually causes no problems. If the column is indexed, extra processing occurs. The dynamic approach includes only the columns that have changed, which provides an optimal `UPDATE` string. However, extra processing is incurred at runtime when the dynamic `UPDATE` statement is built. We recommend that you test the dynamic and static approaches, and then choose the optimal solution.

## Permissions

Only members of the **sysadmin** fixed server role or **db_owner** fixed database role can execute `sp_scriptdynamicupdproc`.

## Examples

This example creates an article (with *@artid* set to `1`) on the `authors` table in the `pubs` database, and specifies that the `UPDATE` statement is the custom procedure to execute: `'MCALL sp_mupd_authors'`.

Generate the custom stored procedures to be executed by the Distribution Agent at the Subscriber by running the following stored procedure at the Publisher:

```sql
EXEC sp_scriptdynamicupdproc @artid = '1';
```

The statement returns:

```sql
CREATE PROCEDURE [sp_mupd_authors] @c1 VARCHAR(11),
    @c2 VARCHAR(40),
    @c3 VARCHAR(20),
    @c4 CHAR(12),
    @c5 VARCHAR(40),
    @c6 VARCHAR(20),
    @c7 CHAR(2),
    @c8 CHAR(5),
    @c9 BIT,
    @pkc1 VARCHAR(11),
    @bitmap BINARY (2)
AS
DECLARE @stmt NVARCHAR(4000),
    @spacer NVARCHAR(1);

SELECT @spacer = N'';

SELECT @stmt = N'UPDATE [authors] SET ';

IF SUBSTRING(@bitmap, 1, 1) & 2 = 2
BEGIN
    SELECT @stmt = @stmt + @spacer + N'[au_lname]' + N'=@2'
    SELECT @spacer = N','
END;

IF SUBSTRING(@bitmap, 1, 1) & 4 = 4
BEGIN
    SELECT @stmt = @stmt + @spacer + N'[au_fname]' + N'=@3'
    SELECT @spacer = N','
END;

IF SUBSTRING(@bitmap, 1, 1) & 8 = 8
BEGIN
    SELECT @stmt = @stmt + @spacer + N'[phone]' + N'=@4'
    SELECT @spacer = N','
END;

IF SUBSTRING(@bitmap, 1, 1) & 16 = 16
BEGIN
    SELECT @stmt = @stmt + @spacer + N'[address]' + N'=@5'
    SELECT @spacer = N','
END;

IF SUBSTRING(@bitmap, 1, 1) & 32 = 32
BEGIN
    SELECT @stmt = @stmt + @spacer + N'[city]' + N'=@6'
    SELECT @spacer = N','
END;

IF SUBSTRING(@bitmap, 1, 1) & 64 = 64
BEGIN
    SELECT @stmt = @stmt + @spacer + N'[state]' + N'=@7'
    SELECT @spacer = N','
END;

IF SUBSTRING(@bitmap, 1, 1) & 128 = 128
BEGIN
    SELECT @stmt = @stmt + @spacer + N'[zip]' + N'=@8'
    SELECT @spacer = N','
END;

IF SUBSTRING(@bitmap, 2, 1) & 1 = 1
BEGIN
    SELECT @stmt = @stmt + @spacer + N'[contract]' + N'=@9'
    SELECT @spacer = N','
END;

SELECT @stmt = @stmt + N' where [au_id] = @1'

EXEC sp_executesql @stmt,
    N' @1 varchar(11),@2 varchar(40),@3 varchar(20),@4 char(12),@5 varchar(40),
    @6 varchar(20),@7 char(2),@8 char(5),@9 bit',
    @pkc1, @c2, @c3, @c4, @c5, @c6, @c7, @c8, @c9;

IF @@rowcount = 0
    IF @@microsoftversion > 0x07320000
        EXEC sp_MSreplraiserror 20598;
```

After running this stored procedure, you can use the resulting script to create the stored procedure manually at the Subscribers.

## Related content

- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
