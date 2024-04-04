---
author: rwestMSFT
ms.author: randolphwest
ms.date: 01/29/2024
ms.service: sql
ms.topic: include
---
  Beginning with SQL Server 2005, the behavior of schemas changed. As a result, code that assumes that schemas are equivalent to database users may no longer return correct results. Old catalog views, including sysobjects, should not be used in a database in which any of the following DDL statements have ever been used: CREATE SCHEMA, ALTER SCHEMA, DROP SCHEMA, CREATE USER, ALTER USER, DROP USER, CREATE ROLE, ALTER ROLE, DROP ROLE, CREATE APPROLE, ALTER APPROLE, DROP APPROLE, ALTER AUTHORIZATION. In such databases you must instead use the new catalog views. The new catalog views take into account the separation of principals and schemas that was introduced in SQL Server 2005. For more information about catalog views, see [Catalog Views &#40;Transact-SQL&#41;](../relational-databases/system-catalog-views/catalog-views-transact-sql.md).
   
