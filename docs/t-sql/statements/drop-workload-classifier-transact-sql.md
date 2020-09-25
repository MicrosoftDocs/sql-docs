---
description: "DROP WORKLOAD Classifier (Transact-SQL)"
title: "DROP WORKLOAD Classifier (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: 11/04/2019
ms.prod: sql
ms.prod_service: "sql-data-warehouse"
ms.reviewer: "jrasnick"
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "WORKLOAD CLASSIFIER"
  - "WORKLOAD_CLASSIFIER_TSQL"
  - "DROP_WORKLOAD_CLASSIFIER_TSQL"
  - "DROP WORKLOAD GROUP"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "DROP WORKLOAD CLASSIFIER statement"
author: ronortloff
ms.author: rortloff
monikerRange: "=azure-sqldw-latest||=sqlallproducts-allversions"
---
# DROP WORKLOAD CLASSIFIER (Transact-SQL)

[!INCLUDE [asa](../../includes/applies-to-version/asa.md)]

Drops an existing user-defined workload management classifier.  If requests are running or in the request queue in suspended state, they will keep their classification and the classifier can be dropped immediately. Dropping and recreating the classifier with different importance will not affect an already classified request.
  
![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md).  
  
## Syntax  

```syntaxsql
DROP WORKLOAD CLASSIFIER classifier_name;
```

## Arguments

*classifier_name*  
Specifies the name by which the workload classifier is identified.
  
## Permissions

Requires CONTROL DATABASE permission.  
  
## Examples

The following example drops the workload classifier named `wgcELTROLE`.  

```sql
DROP WORKLOAD CLASSIFIER wgcELTRole;
```

> [!NOTE]
> A request submitted without a matching classifier, is classified to the default workload group.  The default workload group is the smallrc resource class.
  
## See Also

[CREATE WORKLOAD CLASSIFIER &#40;Transact-SQL&#41;](../../t-sql/statements/create-workload-classifier-transact-sql.md)</br>
[[!INCLUDE[ssSDW](../../includes/sssdwfull-md.md)] Workload Classification](/azure/sql-data-warehouse/sql-data-warehouse-workload-classification)
