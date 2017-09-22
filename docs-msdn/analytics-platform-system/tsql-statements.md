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
* [ALTER DATABASE](../../docs/t-sql/statements/alter-database-azure-sql-data-warehouse.md)
* [ALTER INDEX](../../docs/t-sql/statements/alter-index-transact-sql.md)
* [ALTER PROCEDURE](../../docs/t-sql/statements/alter-procedure-transact-sql.md)
* [ALTER SCHEMA](../../docs/t-sql/statements/alter-schema-transact-sql.md)
* [ALTER TABLE](../../docs/t-sql/statements/alter-table-transact-sql.md)
* [CREATE COLUMNSTORE INDEX](../../docs/t-sql/statements/create-columnstore-index-transact-sql.md)
* [CREATE DATABASE](../../docs/t-sql/statements/create-database-azure-sql-data-warehouse.md)
* [CREATE DATABASE SCOPED CREDENTIAL](../../docs/t-sql/statements/create-database-scoped-credential-transact-sql.md)
* [CREATE EXTERNAL DATA SOURCE](../../docs/t-sql/statements/create-external-data-source-transact-sql.md)
* [CREATE EXTERNAL FILE FORMAT](../../docs/t-sql/statements/create-external-file-format-transact-sql.md)
* [CREATE EXTERNAL TABLE](../../docs/t-sql/statements/create-external-table-transact-sql.md)
* [CREATE FUNCTION](../../docs/t-sql/statements/create-function-sql-data-warehouse.md)
* [CREATE INDEX](../../docs/t-sql/statements/create-index-transact-sql.md)
* [CREATE PROCEDURE](../../docs/t-sql/statements/create-procedure-transact-sql.md)
* [CREATE SCHEMA](../../docs/t-sql/statements/create-schema-transact-sql.md)
* [CREATE STATISTICS](../../docs/t-sql/statements/create-statistics-transact-sql.md)
* [CREATE TABLE](../../docs/t-sql/statements/create-table-azure-sql-data-warehouse.md)
* [CREATE TABLE AS SELECT](../../docs/t-sql/statements/create-table-as-select-azure-sql-data-warehouse.md)
* [CREATE VIEW](../../docs/t-sql/statements/create-view-transact-sql.md)
* [DROP EXTERNAL DATA SOURCE](../../docs/t-sql/statements/drop-external-data-source-transact-sql.md)
* [DROP EXTERNAL FILE FORMAT](../../docs/t-sql/statements/drop-external-file-format-transact-sql.md)
* [DROP EXTERNAL TABLE](../../docs/t-sql/statements/drop-external-table-transact-sql.md)
* [DROP INDEX](../../docs/t-sql/statements/drop-index-transact-sql.md)
* [DROP PROCEDURE](../../docs/t-sql/statements/drop-procedure-transact-sql.md)
* [DROP STATISTICS](../../docs/t-sql/statements/drop-statistics-transact-sql.md)
* [DROP TABLE](../../docs/t-sql/statements/drop-table-transact-sql.md)
* [DROP SCHEMA](../../docs/t-sql/statements/drop-schema-transact-sql.md)
* [DROP VIEW](../../docs/t-sql/statements/drop-view-transact-sql.md)
* [RENAME](../../docs/t-sql/statements/rename-transact-sql.md)
* [TRUNCATE TABLE](../../docs/t-sql/statements/truncate-table-transact-sql.md)
* [UPDATE STATISTICS](../../docs/t-sql/statements/update-statistics-transact-sql.md)

## Data Manipulation Language (DML) statements
* [DELETE](../../docs/t-sql/statements/delete-transact-sql.md)
* [INSERT](../../docs/t-sql/statements/insert-transact-sql.md)
* [UPDATE](../../docs/t-sql/queries/update-transact-sql.md)

## Database Console Commands
* [DBCC DROPCLEANBUFFERS](../../docs/t-sql/database-console-commands/dbcc-dropcleanbuffers-transact-sql.md)
* [DBCC FREEPROCCACHE](https://msdn.microsoft.com/library/mt204018.aspx)
* [DBCC SHRINKLOG](https://msdn.microsoft.com/library/mt204020.aspx)
* [DBCC PDW_SHOWEXECUTIONPLAN](https://msdn.microsoft.com/library/mt204017.aspx)
* [DBCC PDW_SHOWPARTITIONSTATS](https://msdn.microsoft.com/library/mt204013.aspx)
* [DBCC PDW_SHOWSPACEUSED](/sql-docs/docs/t-sql/database-console-commands/dbcc-pdw-showspaceused-transact-sql)
* [DBCC SHOW_STATISTICS](https://msdn.microsoft.com/library/mt204043.aspx)

## Query statements
* [SELECT](../../docs/t-sql/queries/select-transact-sql.md)
* [WITH common_table_expression](/sql-docs/docs/t-sql/queries/with-common-table-expression-transact-sql)
* [EXCEPT and INTERSECT](../../docs/t-sql/language-elements/set-operators-except-and-intersect-transact-sql.md)
* [EXPLAIN](../../docs/t-sql/queries/explain-transact-sql.md)
* [FROM](../../docs/t-sql/queries/from-transact-sql.md)
* [Using PIVOT and UNPIVOT](../../docs/t-sql/queries/from-using-pivot-and-unpivot.md)
* [GROUP BY](../../docs/t-sql/queries/select-group-by-transact-sql.md)
* [HAVING](../../docs/t-sql/queries/select-having-transact-sql.md)
* [ORDER BY](../../docs/t-sql/queries/select-order-by-clause-transact-sql.md)
* [OPTION](../../docs/t-sql/queries/option-clause-transact-sql.md)
* [UNION](../../docs/t-sql/language-elements/set-operators-union-transact-sql.md)
* [WHERE](../../docs/t-sql/queries/where-transact-sql.md)
* [TOP](../../docs/t-sql/queries/top-transact-sql.md)
* [Aliasing](../../docs/t-sql/queries/aliasing-azure-sql-data-warehouse-parallel-data-warehouse.md)
* [Search condition](../../docs/t-sql/queries/search-condition-transact-sql.md)
* [Subqueries](../../docs/t-sql/queries/subqueries-azure-sql-data-warehouse-parallel-data-warehouse.md)

## Security statements
* Permissions: [GRANT](../../docs/t-sql/statements/grant-transact-sql.md), [DENY](../../docs/t-sql/statements/deny-transact-sql.md), [REVOKE](../../docs/t-sql/statements/revoke-transact-sql.md)
* [ALTER AUTHORIZATION](../../docs/t-sql/statements/alter-authorization-transact-sql.md)
* [ALTER CERTIFICATE](../../docs/t-sql/statements/alter-certificate-transact-sql.md)
* [ALTER DATABASE ENCRYPTION KEY](../../docs/t-sql/statements/alter-database-encryption-key-transact-sql.md)
* [ALTER LOGIN](../../docs/t-sql/statements/alter-login-transact-sql.md)
* [ALTER MASTER KEY](../../docs/t-sql/statements/alter-master-key-transact-sql.md)
* [ALTER ROLE](../../docs/t-sql/statements/alter-role-transact-sql.md)
* [ALTER USER](../../docs/t-sql/statements/alter-user-transact-sql.md)
* [BACKUP CERTIFICATE](../../docs/t-sql/statements/backup-certificate-transact-sql.md)
* [CLOSE MASTER KEY](../../docs/t-sql/statements/close-master-key-transact-sql.md)
* [CREATE CERTIFICATE](../../docs/t-sql/statements/create-certificate-transact-sql.md)
* [CREATE DATABASE ENCRYPTION KEY](../../docs/t-sql/statements/create-database-encryption-key-transact-sql.md)
* [CREATE LOGIN](../../docs/t-sql/statements/create-login-transact-sql.md)
* [CREATE MASTER KEY](../../docs/t-sql/statements/create-master-key-transact-sql.md)
* [CREATE ROLE](../../docs/t-sql/statements/create-role-transact-sql.md)
* [CREATE USER](../../docs/t-sql/statements/create-user-transact-sql.md)
* [DROP CERTIFICATE](../../docs/t-sql/statements/drop-certificate-transact-sql.md)
* [DROP DATABASE ENCRYPTION KEY](../../docs/t-sql/statements/drop-database-encryption-key-transact-sql.md)
* [DROP LOGIN](../../docs/t-sql/statements/drop-login-transact-sql.md)
* [DROP MASTER KEY](../../docs/t-sql/statements/drop-master-key-transact-sql.md)
* [DROP ROLE](../../docs/t-sql/statements/drop-role-transact-sql.md)
* [DROP USER](../../docs/t-sql/statements/drop-user-transact-sql.md)
* [OPEN MASTER KEY](../../docs/t-sql/statements/open-master-key-transact-sql.md)

## Next steps
For more reference information, see [T-SQL language elements](tsql-language-elements.md) and [T-SQL system views](tsql-system-views.md).

<!--Image references-->

<!--Article references-->

<!--MSDN references-->

<!--Other Web references-->
