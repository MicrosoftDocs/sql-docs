---
title: T-SQL statements - Analytics Platform System SQL Server Parallel Data Warehouse | Microsoft Docs
description: Transact-SQL (T-SQL) statements for Analytic Platform System (APS) SQL Server Parallel Data Warehouse (PDW).
services: sql-data-warehouse
documentationcenter: NA
author: barbkess
manager: jhubbard
editor: ''

ms.assetid: 0abc5934-1e67-491a-b7d7-8b520d1ae98e
ms.service: sql-data-warehouse
ms.devlang: NA
ms.topic: article
ms.tgt_pltfrm: NA
ms.workload: data-services
ms.date: 12/15/2016
ms.author: barbkess

---
# T-SQL topics
Transact-SQL (T-SQL) statements for Analytic Platform System (APS) SQL Server Parallel Data Warehouse (PDW).

## Data Definition Language (DDL) statements
* [ALTER DATABASE](/sql-docs/docs/t-sql/statements/alter-database-azure-sql-data-warehouse)
* [ALTER INDEX](/sql-docs/docs/t-sql/statements/alter-index-transact-sql)
* [ALTER PROCEDURE](/sql-docs/docs/t-sql/statements/alter-procedure-transact-sql)
* [ALTER SCHEMA](/sql-docs/docs/t-sql/statements/alter-schema-transact-sql)
* [ALTER TABLE](/sql-docs/docs/t-sql/statements/alter-table-transact-sql)
* [CREATE COLUMNSTORE INDEX](/sql-docs/docs/t-sql/statements/create-columnstore-index-transact-sql)
* [CREATE DATABASE](/sql-docs/docs/t-sql/statements/create-database-azure-sql-data-warehouse)
* [CREATE DATABASE SCOPED CREDENTIAL](/sql-docs/docs/t-sql/statements/create-database-scoped-credential-transact-sql)
* [CREATE EXTERNAL DATA SOURCE](/sql-docs/docs/t-sql/statements/create-external-data-source-transact-sql)
* [CREATE EXTERNAL FILE FORMAT](/sql-docs/docs/t-sql/statements/create-external-file-format-transact-sql)
* [CREATE EXTERNAL TABLE](/sql-docs/docs/t-sql/statements/create-external-table-transact-sql)
* [CREATE FUNCTION](/sql-docs/docs/t-sql/statements/create-function-sql-data-warehouse)
* [CREATE INDEX](/sql-docs/docs/t-sql/statements/create-index-transact-sql)
* [CREATE PROCEDURE](/sql-docs/docs/t-sql/statements/create-procedure-transact-sql)
* [CREATE SCHEMA](/sql-docs/docs/t-sql/statements/create-schema-transact-sql)
* [CREATE STATISTICS](/sql-docs/docs/t-sql/statements/create-statistics-transact-sql)
* [CREATE TABLE](/sql-docs/docs/t-sql/statements/create-table-azure-sql-data-warehouse)
* [CREATE TABLE AS SELECT](/sql-docs/docs/t-sql/statements/create-table-as-select-azure-sql-data-warehouse)
* [CREATE VIEW](/sql-docs/docs/t-sql/statements/create-view-transact-sql)
* [DROP EXTERNAL DATA SOURCE](/sql-docs/docs/t-sql/statements/drop-external-data-source-transact-sql)
* [DROP EXTERNAL FILE FORMAT](/sql-docs/docs/t-sql/statements/drop-external-file-format-transact-sql)
* [DROP EXTERNAL TABLE](/sql-docs/docs/t-sql/statements/drop-external-table-transact-sql)
* [DROP INDEX](/sql-docs/docs/t-sql/statements/drop-index-transact-sql)
* [DROP PROCEDURE](/sql-docs/docs/t-sql/statements/drop-procedure-transact-sql)
* [DROP STATISTICS](/sql-docs/docs/t-sql/statements/drop-statistics-transact-sql)
* [DROP TABLE](/sql-docs/docs/t-sql/statements/drop-table-transact-sql)
* [DROP SCHEMA](/sql-docs/docs/t-sql/statements/drop-schema-transact-sql)
* [DROP VIEW](/sql-docs/docs/t-sql/statements/drop-view-transact-sql)
* [RENAME](/sql-docs/docs/t-sql/statements/rename-transact-sql)
* [TRUNCATE TABLE](/sql-docs/docs/t-sql/statements/truncate-table-transact-sql)
* [UPDATE STATISTICS](/sql-docs/docs/t-sql/statements/update-statistics-transact-sql)

## Data Manipulation Language (DML) statements
* [DELETE](/sql-docs/docs/t-sql/statements/delete-transact-sql)
* [INSERT](/sql-docs/docs/t-sql/statements/insert-transact-sql)
* [UPDATE](/sql-docs/docs/t-sql/queries/update-transact-sql)

## Database Console Commands
* [DBCC DROPCLEANBUFFERS](/sql-docs/docs/t-sql/database-console-commands/dbcc-dropcleanbuffers-transact-sql)
* [DBCC FREEPROCCACHE](https://msdn.microsoft.com/library/mt204018.aspx)
* [DBCC SHRINKLOG](https://msdn.microsoft.com/library/mt204020.aspx)
* [DBCC PDW_SHOWEXECUTIONPLAN](https://msdn.microsoft.com/library/mt204017.aspx)
* [DBCC PDW_SHOWPARTITIONSTATS](https://msdn.microsoft.com/library/mt204013.aspx)
* [DBCC PDW_SHOWSPACEUSED](/sql-docs/docs/t-sql/database-console-commands/dbcc-pdw-showspaceused-transact-sql)
* [DBCC SHOW_STATISTICS](https://msdn.microsoft.com/library/mt204043.aspx)

## Query statements
* [SELECT](/sql-docs/docs/t-sql/queries/select-transact-sql)
* [WITH common_table_expression](/sql-docs/docs/t-sql/queries/with-common-table-expression-transact-sql)
* [EXCEPT and INTERSECT](/sql-docs/docs/t-sql/language-elements/set-operators-except-and-intersect-transact-sql)
* [EXPLAIN](/sql-docs/docs/t-sql/queries/explain-transact-sql)
* [FROM](/sql-docs/docs/t-sql/queries/from-transact-sql)
* [Using PIVOT and UNPIVOT](/sql-docs/docs/t-sql/queries/from-using-pivot-and-unpivot)
* [GROUP BY](/sql-docs/docs/t-sql/queries/select-group-by-transact-sql)
* [HAVING](/sql-docs/docs/t-sql/queries/select-having-transact-sql)
* [ORDER BY](/sql-docs/docs/t-sql/queries/select-order-by-clause-transact-sql)
* [OPTION](/sql-docs/docs/t-sql/queries/option-clause-transact-sql)
* [UNION](/sql-docs/docs/t-sql/language-elements/set-operators-union-transact-sql)
* [WHERE](/sql-docs/docs/t-sql/queries/where-transact-sql)
* [TOP](/sql-docs/docs/t-sql/queries/top-transact-sql)
* [Aliasing](/sql-docs/docs/t-sql/queries/aliasing-azure-sql-data-warehouse-parallel-data-warehouse)
* [Search condition](/sql-docs/docs/t-sql/queries/search-condition-transact-sql)
* [Subqueries](/sql-docs/docs/t-sql/queries/subqueries-azure-sql-data-warehouse-parallel-data-warehouse)

## Security statements
* Permissions: [GRANT](/sql-docs/docs/t-sql/statements/grant-transact-sql), [DENY](/sql-docs/docs/t-sql/statements/deny-transact-sql), [REVOKE](/sql-docs/docs/t-sql/statements/revoke-transact-sql)
* [ALTER AUTHORIZATION](/sql-docs/docs/t-sql/statements/alter-authorization-transact-sql)
* [ALTER CERTIFICATE](/sql-docs/docs/t-sql/statements/alter-certificate-transact-sql)
* [ALTER DATABASE ENCRYPTION KEY](/sql-docs/docs/t-sql/statements/alter-database-encryption-key-transact-sql)
* [ALTER LOGIN](/sql-docs/docs/t-sql/statements/alter-login-transact-sql)
* [ALTER MASTER KEY](/sql-docs/docs/t-sql/statements/alter-master-key-transact-sql)
* [ALTER ROLE](/sql-docs/docs/t-sql/statements/alter-role-transact-sql)
* [ALTER USER](/sql-docs/docs/t-sql/statements/alter-user-transact-sql)
* [BACKUP CERTIFICATE](/sql-docs/docs/t-sql/statements/backup-certificate-transact-sql)
* [CLOSE MASTER KEY](/sql-docs/docs/t-sql/statements/close-master-key-transact-sql)
* [CREATE CERTIFICATE](/sql-docs/docs/t-sql/statements/create-certificate-transact-sql)
* [CREATE DATABASE ENCRYPTION KEY](/sql-docs/docs/t-sql/statements/create-database-encryption-key-transact-sql)
* [CREATE LOGIN](/sql-docs/docs/t-sql/statements/create-login-transact-sql)
* [CREATE MASTER KEY](/sql-docs/docs/t-sql/statements/create-master-key-transact-sql)
* [CREATE ROLE](/sql-docs/docs/t-sql/statements/create-role-transact-sql)
* [CREATE USER](/sql-docs/docs/t-sql/statements/create-user-transact-sql)
* [DROP CERTIFICATE](/sql-docs/docs/t-sql/statements/drop-certificate-transact-sql)
* [DROP DATABASE ENCRYPTION KEY](/sql-docs/docs/t-sql/statements/drop-database-encryption-key-transact-sql)
* [DROP LOGIN](/sql-docs/docs/t-sql/statements/drop-login-transact-sql)
* [DROP MASTER KEY](/sql-docs/docs/t-sql/statements/drop-master-key-transact-sql)
* [DROP ROLE](/sql-docs/docs/t-sql/statements/drop-role-transact-sql)
* [DROP USER](/sql-docs/docs/t-sql/statements/drop-user-transact-sql)
* [OPEN MASTER KEY](/sql-docs/docs/t-sql/statements/open-master-key-transact-sql)

## Next steps
For more reference information, see [T-SQL language elements](tsql-language-elements.md) and [T-SQL system views](tsql-system-views.md).

<!--Image references-->

<!--Article references-->

<!--MSDN references-->

<!--Other Web references-->
