---
title: "ALTER Statements (Transact-SQL) | Microsoft Docs"
ms.custom: 
  - "SQL2016_New_Updated"
ms.date: "05/02/2016"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
f1_keywords: 
  - "ALTER_TSQL"
  - "ALTER"
dev_langs: 
  - "TSQL"
ms.assetid: 914d5308-e56b-4c87-a7b1-ca9efda4733b
caps.latest.revision: 19
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# ALTER Statements (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-all_md](../../includes/tsql-appliesto-ss2008-all-md.md)]

  [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[tsql](../../includes/tsql-md.md)] contains the following ALTER statements. Use ALTER statements to modify the definition of existing entities. For example, use ALTER TABLE to add a new column to a table, or use ALTER DATABASE to set database options.  
  
## In This Section  
  
||||  
|-|-|-|  
|[ALTER APPLICATION ROLE](../../t-sql/statements/alter-application-role-transact-sql.md)|[ALTER DATABASE SCOPED CONFIGURATION &#40;Transact-SQL&#41;](../../t-sql/statements/alter-database-scoped-configuration-transact-sql.md)|[ALTER ROUTE](../../t-sql/statements/alter-route-transact-sql.md)|  
|[ALTER ASSEMBLY](../../t-sql/statements/alter-assembly-transact-sql.md)|[ALTER EVENT SESSION](../../t-sql/statements/alter-event-session-transact-sql.md)|[ALTER SCHEMA](../../t-sql/statements/alter-schema-transact-sql.md)|  
|[ALTER ASYMMETRIC KEY](../../t-sql/statements/alter-asymmetric-key-transact-sql.md)|[ALTER EXTERNAL DATA SOURCE](../../t-sql/statements/alter-external-data-source-transact-sql.md)|[ALTER SEARCH PROPERTY LIST &#40;Transact-SQL&#41;](../../t-sql/statements/alter-search-property-list-transact-sql.md)|  
|[ALTER AUTHORIZATION](../../t-sql/statements/alter-authorization-transact-sql.md)|[ALTER EXTERNAL RESOURCE POOL](../../t-sql/statements/alter-external-resource-pool-transact-sql.md)|[ALTER SECURITY POLICY](../../t-sql/statements/alter-security-policy-transact-sql.md)|  
|[ALTER AVAILABILITY GROUP](../../t-sql/statements/alter-availability-group-transact-sql.md)|[ALTER FULLTEXT CATALOG](../../t-sql/statements/alter-fulltext-catalog-transact-sql.md)|[ALTER SEQUENCE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-sequence-transact-sql.md)|  
|[ALTER BROKER PRIORITY](../../t-sql/statements/alter-broker-priority-transact-sql.md)|[ALTER FULLTEXT INDEX](../../t-sql/statements/alter-fulltext-index-transact-sql.md)|[ALTER SERVER AUDIT](../../t-sql/statements/alter-server-audit-transact-sql.md)|  
|[ALTER CERTIFICATE](../../t-sql/statements/alter-certificate-transact-sql.md)|[ALTER FULLTEXT STOPLIST](../../t-sql/statements/alter-fulltext-stoplist-transact-sql.md)|[ALTER SERVER AUDIT SPECIFICATION](../../t-sql/statements/alter-server-audit-specification-transact-sql.md)|  
|[ALTER COLUMN ENCRYPTION KEY](../../t-sql/statements/alter-column-encryption-key-transact-sql.md)|[ALTER FUNCTION](../../t-sql/statements/alter-function-transact-sql.md)|[ALTER SERVER CONFIGURATION](../../t-sql/statements/alter-server-configuration-transact-sql.md)|  
|[ALTER CREDENTIAL](../../t-sql/statements/alter-credential-transact-sql.md)|[ALTER INDEX](../../t-sql/statements/alter-index-transact-sql.md)|[ALTER SERVER ROLE](../../t-sql/statements/alter-server-role-transact-sql.md)|  
|[ALTER CRYPTOGRAPHIC PROVIDER](../../t-sql/statements/alter-cryptographic-provider-transact-sql.md)|[ALTER LOGIN](../../t-sql/statements/alter-login-transact-sql.md)|[ALTER SERVICE](../../t-sql/statements/alter-service-transact-sql.md)|  
|[ALTER DATABASE](../../t-sql/statements/alter-database-transact-sql.md)|[ALTER MASTER KEY](../../t-sql/statements/alter-master-key-transact-sql.md)|[ALTER SERVICE MASTER KEY](../../t-sql/statements/alter-service-master-key-transact-sql.md)|  
|[ALTER DATABASE AUDIT SPECIFICATION](../../t-sql/statements/alter-database-audit-specification-transact-sql.md)|[ALTER MESSAGE TYPE](../../t-sql/statements/alter-message-type-transact-sql.md)|[ALTER SYMMETRIC KEY](../../t-sql/statements/alter-symmetric-key-transact-sql.md)|  
|[ALTER DATABASE Compatibility Level](../../t-sql/statements/alter-database-transact-sql-compatibility-level.md)|[ALTER PARTITION FUNCTION](../../t-sql/statements/alter-partition-function-transact-sql.md)|[ALTER TABLE](../../t-sql/statements/alter-table-transact-sql.md)|  
|[ALTER DATABASE Database Mirroring](../../t-sql/statements/alter-database-transact-sql-database-mirroring.md)|[ALTER PARTITION SCHEME](../../t-sql/statements/alter-partition-scheme-transact-sql.md)|[ALTER TRIGGER](../../t-sql/statements/alter-trigger-transact-sql.md)|  
|[ALTER DATABASE ENCRYPTION KEY](../../t-sql/statements/alter-database-encryption-key-transact-sql.md)|[ALTER PROCEDURE](../../t-sql/statements/alter-procedure-transact-sql.md)|[ALTER USER](../../t-sql/statements/alter-user-transact-sql.md)|  
|[ALTER ENDPOINT](../../t-sql/statements/alter-endpoint-transact-sql.md)|[ALTER QUEUE](../../t-sql/statements/alter-queue-transact-sql.md)|[ALTER VIEW](../../t-sql/statements/alter-view-transact-sql.md)|  
|[ALTER DATABASE File and Filegroup Options](../../t-sql/statements/alter-database-transact-sql-file-and-filegroup-options.md)|[ALTER REMOTE SERVICE BINDING](../../t-sql/statements/alter-remote-service-binding-transact-sql.md)|[ALTER WORKLOAD GROUP](../../t-sql/statements/alter-workload-group-transact-sql.md)|  
|[ALTER DATABASE SET HADR](../../t-sql/statements/alter-database-transact-sql-set-hadr.md)|[ALTER RESOURCE GOVERNOR](../../t-sql/statements/alter-resource-governor-transact-sql.md)|[ALTER XML SCHEMA COLLECTION](../../t-sql/statements/alter-xml-schema-collection-transact-sql.md)|  
|[ALTER DATABASE SET Options](../../t-sql/statements/alter-database-transact-sql-set-options.md)|[ALTER RESOURCE POOL](../../t-sql/statements/alter-resource-pool-transact-sql.md)||  
|[ALTER DATABASE SCOPED CREDENTIAL &#40;Transact-SQL&#41;](../../t-sql/statements/alter-database-scoped-credential-transact-sql.md)|[ALTER ROLE](../../t-sql/statements/alter-role-transact-sql.md)||  
  
## See Also  
 [CREATE Statements &#40;Transact-SQL&#41;](../../t-sql/statements/create-statements-transact-sql.md)   
 [DROP Statements &#40;Transact-SQL&#41;](../../t-sql/statements/drop-statements-transact-sql.md)  
  
  
