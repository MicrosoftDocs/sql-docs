---
title: "sp_dropmergearticle (Transact-SQL)"
description: "Removes an article from a merge publication. This stored procedure is executed at the Publisher on the publication database."
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 11/23/2023
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_dropmergearticle"
  - "sp_dropmergearticle_TSQL"
helpviewer_keywords:
  - "sp_dropmergearticle"
dev_langs:
  - "TSQL"
---
# sp_dropmergearticle (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Removes an article from a merge publication. This stored procedure is executed at the Publisher on the publication database.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_dropmergearticle
    [ @publication = ] N'publication'
    , [ @article = ] N'article'
    [ , [ @ignore_distributor = ] ignore_distributor ]
    [ , [ @reserved = ] reserved ]
    [ , [ @force_invalidate_snapshot = ] force_invalidate_snapshot ]
    [ , [ @force_reinit_subscription = ] force_reinit_subscription ]
    [ , [ @ignore_merge_metadata = ] ignore_merge_metadata ]
[ ; ]
```

## Arguments

#### [ @publication = ] N'*publication*'

The name of the publication from which to drop an article. *@publication* is **sysname**, with no default.

#### [ @article = ] N'*article*'

The name of the article to drop from the given publication. *@article* is **sysname**, with no default. If `all`, all existing articles in the specified merge publication are removed. Even if *@article* is `all`, the publication still must be dropped separately from the article.

#### [ @ignore_distributor = ] *ignore_distributor*

Indicates whether this stored procedure is executed without connecting to the Distributor. *@ignore_distributor* is **bit**, with a default of `0`.

#### [ @reserved = ] *reserved*

Reserved for future use. *@reserved* is **bit**, with a default of `0`.

#### [ @force_invalidate_snapshot = ] *force_invalidate_snapshot*

Enables or disables the ability to have a snapshot invalidated. *@force_invalidate_snapshot* is **bit**, with a default of `0`.

- `0` specifies that changes to the merge article don't cause the snapshot to be invalid.

- `1` means that changes to the merge article might cause the snapshot to be invalid, and if that is the case, a value of `1` gives permission for the new snapshot to occur.

#### [ @force_reinit_subscription = ] *force_reinit_subscription*

Acknowledges that dropping the article requires existing subscriptions to be reinitialized. *@force_reinit_subscription* is **bit**, with a default of `0`.

- `0` specifies that dropping the article doesn't cause the subscription to be reinitialized.

- `1` means that dropping the article causes existing subscriptions to be reinitialized, and gives permission for the subscription reinitialization to occur.

#### [ @ignore_merge_metadata = ] *ignore_merge_metadata*

[!INCLUDE [ssinternalonly-md](../../includes/ssinternalonly-md.md)]

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_dropmergearticle` is used in merge replication. For more information about dropping articles, see [Add Articles to and Drop Articles from Existing Publications](../replication/publish/add-articles-to-and-drop-articles-from-existing-publications.md).

Executing `sp_dropmergearticle` to drop an article from a publication doesn't remove the object from the publication database or the corresponding object from the subscription database. Use `DROP <object>` to remove these objects manually if necessary.

## Permissions

Only members of the **sysadmin** fixed server role or the **db_owner** fixed database role can execute `sp_dropmergearticle`.

## Examples

[!INCLUDE [article-uses-adventureworks](../../includes/article-uses-adventureworks.md)]

### A. Remove articles from a merge publication

```sql
USE [AdventureWorks2022];
GO

DECLARE @publication AS SYSNAME;
DECLARE @article1 AS SYSNAME;
DECLARE @article2 AS SYSNAME;

SET @publication = N'AdvWorksSalesOrdersMerge';
SET @article1 = N'SalesOrderDetail';
SET @article2 = N'SalesOrderHeader';

EXEC sp_dropmergearticle @publication = @publication,
    @article = @article1,
    @force_invalidate_snapshot = 1;

EXEC sp_dropmergearticle @publication = @publication,
    @article = @article2,
    @force_invalidate_snapshot = 1;
GO
```

### B. Drop merge join filters and related articles

```sql
USE [AdventureWorks2022];
GO

DECLARE @publication AS SYSNAME;
DECLARE @table1 AS SYSNAME;
DECLARE @table2 AS SYSNAME;
DECLARE @table3 AS SYSNAME;
DECLARE @salesschema AS SYSNAME;
DECLARE @hrschema AS SYSNAME;
DECLARE @filterclause AS NVARCHAR(1000);

SET @publication = N'AdvWorksSalesOrdersMerge';
SET @table1 = N'Employee';
SET @table2 = N'SalesOrderHeader';
SET @table3 = N'SalesOrderDetail';
SET @salesschema = N'Sales';
SET @hrschema = N'HumanResources';
SET @filterclause = N'Employee.LoginID = HOST_NAME()';

-- Drop the merge join filter between SalesOrderHeader and SalesOrderDetail.
EXEC sp_dropmergefilter @publication = @publication,
    @article = @table3,
    @filtername = N'SalesOrderDetail_SalesOrderHeader',
    @force_invalidate_snapshot = 1,
    @force_reinit_subscription = 1;

-- Drop the merge join filter between Employee and SalesOrderHeader.
EXEC sp_dropmergefilter @publication = @publication,
    @article = @table2,
    @filtername = N'SalesOrderHeader_Employee',
    @force_invalidate_snapshot = 1,
    @force_reinit_subscription = 1;

-- Drop the article for the SalesOrderDetail table.
EXEC sp_dropmergearticle @publication = @publication,
    @article = @table3,
    @force_invalidate_snapshot = 1,
    @force_reinit_subscription = 1;

-- Drop the article for the SalesOrderHeader table.
EXEC sp_dropmergearticle @publication = @publication,
    @article = @table2,
    @force_invalidate_snapshot = 1,
    @force_reinit_subscription = 1;

-- Drop the article for the Employee table.
EXEC sp_dropmergearticle @publication = @publication,
    @article = @table1,
    @force_invalidate_snapshot = 1,
    @force_reinit_subscription = 1;
GO
```

## Related content

- [Delete an Article](../replication/publish/delete-an-article.md)
- [Add Articles to and Drop Articles from Existing Publications](../replication/publish/add-articles-to-and-drop-articles-from-existing-publications.md)
- [sp_addmergearticle (Transact-SQL)](sp-addmergearticle-transact-sql.md)
- [sp_changemergearticle (Transact-SQL)](sp-changemergearticle-transact-sql.md)
- [sp_helpmergearticle (Transact-SQL)](sp-helpmergearticle-transact-sql.md)
- [Replication stored procedures (Transact-SQL)](replication-stored-procedures-transact-sql.md)
