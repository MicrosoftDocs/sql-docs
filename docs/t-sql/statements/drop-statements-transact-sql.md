---
title: "DROP Statements (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "05/02/2016"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
f1_keywords: 
  - "DROP"
  - "DROP_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "DROP statements (Transact-SQL)"
ms.assetid: 3f97cf5c-9a20-4c4d-aebf-98067028b910
caps.latest.revision: 17
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# DROP Statements (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-all_md](../../includes/tsql-appliesto-ss2008-all-md.md)]

  [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[tsql](../../includes/tsql-md.md)] contains the following DROP statements. Use DROP statements to remove existing entities. For example, use DROP TABLE to remove a table from a database.  
  
## In This Section  
  
||||  
|-|-|-|  
|[DROP AGGREGATE](../../t-sql/statements/drop-aggregate-transact-sql.md)|[DROP EXTERNAL FILE FORMAT](../../t-sql/statements/drop-external-file-format-transact-sql.md)|[DROP SCHEMA](../../t-sql/statements/drop-schema-transact-sql.md)|  
|[DROP APPLICATION ROLE](../../t-sql/statements/drop-application-role-transact-sql.md)|[DROP EXTERNAL RESOURCE POOL](../../t-sql/statements/drop-external-resource-pool-transact-sql.md)|[DROP SEARCH PROPERTY LIST](../../t-sql/statements/drop-search-property-list-transact-sql.md)|  
|[DROP ASSEMBLY](../../t-sql/statements/drop-assembly-transact-sql.md)|[DROP EXTERNAL TABLE](../../t-sql/statements/drop-external-table-transact-sql.md)|[DROP SECURITY POLICY](../../t-sql/statements/drop-security-policy-transact-sql.md)|  
|[DROP ASYMMETRIC KEY](../../t-sql/statements/drop-asymmetric-key-transact-sql.md)|[DROP FULLTEXT CATALOG](../../t-sql/statements/drop-fulltext-catalog-transact-sql.md)|[DROP SEQUENCE](../../t-sql/statements/drop-sequence-transact-sql.md)|  
|[DROP AVAILABILITY GROUP](../../t-sql/statements/drop-availability-group-transact-sql.md)|[DROP FULLTEXT INDEX](../../t-sql/statements/drop-fulltext-index-transact-sql.md)|[DROP SERVER AUDIT](../../t-sql/statements/drop-server-audit-transact-sql.md)|  
|[DROP BROKER PRIORITY](../../t-sql/statements/drop-broker-priority-transact-sql.md)|[DROP FULLTEXT STOPLIST](../../t-sql/statements/drop-fulltext-stoplist-transact-sql.md)|[DROP SERVER AUDIT SPECIFICATION](../../t-sql/statements/drop-server-audit-specification-transact-sql.md)|  
|[DROP CERTIFICATE](../../t-sql/statements/drop-certificate-transact-sql.md)|[DROP FUNCTION](../../t-sql/statements/drop-function-transact-sql.md)|[DROP SERVER ROLE](../../t-sql/statements/drop-server-role-transact-sql.md)|  
|[DROP COLUMN ENCRYPTION KEY](../../t-sql/statements/drop-column-encryption-key-transact-sql.md)|[DROP INDEX](../../t-sql/statements/drop-index-transact-sql.md)|[DROP SERVICE](../../t-sql/statements/drop-service-transact-sql.md)|  
|[DROP COLUMN MASTER KEY DEFINITION](../../t-sql/statements/drop-column-master-key-transact-sql.md)|[DROP INDEX (XML Selective Indexes)](../../t-sql/statements/drop-index-selective-xml-indexes.md)|[DROP SIGNATURE](../../t-sql/statements/drop-signature-transact-sql.md)|  
|[DROP CONTRACT](../../t-sql/statements/drop-contract-transact-sql.md)|[DROP LOGIN](../../t-sql/statements/drop-login-transact-sql.md)|[DROP STATISTICS](../../t-sql/statements/drop-statistics-transact-sql.md)|  
|[DROP CREDENTIAL](../../t-sql/statements/drop-credential-transact-sql.md)|[DROP MASTER KEY](../../t-sql/statements/drop-master-key-transact-sql.md)|[DROP SYMMETRIC KEY](../../t-sql/statements/drop-symmetric-key-transact-sql.md)|  
|[DROP CRYPTOGRAPHIC PROVIDER](../../t-sql/statements/drop-cryptographic-provider-transact-sql.md)|[DROP MESSAGE TYPE](../../t-sql/statements/drop-message-type-transact-sql.md)|[DROP SYNONYM](../../t-sql/statements/drop-synonym-transact-sql.md)|  
|[DROP DATABASE](../../t-sql/statements/drop-database-transact-sql.md)|[DROP PARTITION FUNCTION](../../t-sql/statements/drop-partition-function-transact-sql.md)|[DROP TABLE](../../t-sql/statements/drop-table-transact-sql.md)|  
|[DROP DATABASE AUDIT SPECIFICATION](../../t-sql/statements/drop-database-audit-specification-transact-sql.md)|[DROP PARTITION SCHEME](../../t-sql/statements/drop-partition-scheme-transact-sql.md)|[DROP TRIGGER](../../t-sql/statements/drop-trigger-transact-sql.md)|  
|[DROP DATABASE ENCRYPTION KEY](../../t-sql/statements/drop-database-encryption-key-transact-sql.md)|[DROP PROCEDURE](../../t-sql/statements/drop-procedure-transact-sql.md)|[DROP TYPE](../../t-sql/statements/drop-type-transact-sql.md)|  
|[DROP DATABASE SCOPED CREDENTIAL](../../t-sql/statements/drop-database-encryption-key-transact-sql.md)|[DROP QUEUE](../../t-sql/statements/drop-queue-transact-sql.md)|[DROP USER](../../t-sql/statements/drop-user-transact-sql.md)|  
|[DROP DEFAULT](../../t-sql/statements/drop-default-transact-sql.md)|[DROP REMOTE SERVICE BINDING](../../t-sql/statements/drop-remote-service-binding-transact-sql.md)|[DROP VIEW](../../t-sql/statements/drop-view-transact-sql.md)|  
|[DROP ENDPOINT](../../t-sql/statements/drop-endpoint-transact-sql.md)|[DROP RESOURCE POOL](../../t-sql/statements/drop-resource-pool-transact-sql.md)|[DROP WORKLOAD GROUP](../../t-sql/statements/drop-workload-group-transact-sql.md)|  
|[DROP EVENT NOTIFICATION](../../t-sql/statements/drop-event-notification-transact-sql.md)|[DROP ROLE](../../t-sql/statements/drop-role-transact-sql.md)|[DROP XML SCHEMA COLLECTION](../../t-sql/statements/drop-xml-schema-collection-transact-sql.md)|  
|[DROP EVENT SESSION](../../t-sql/statements/drop-event-session-transact-sql.md)|[DROP ROUTE](../../t-sql/statements/drop-route-transact-sql.md)||  
|[DROP EXTERNAL DATA SOURCE](../../t-sql/statements/drop-external-data-source-transact-sql.md)|[DROP RULE](../../t-sql/statements/drop-rule-transact-sql.md)||  
  
## See Also  
 [ALTER Statements &#40;Transact-SQL&#41;](../../t-sql/statements/alter-statements-transact-sql.md)   
 [CREATE Statements &#40;Transact-SQL&#41;](../../t-sql/statements/create-statements-transact-sql.md)  
  
  
