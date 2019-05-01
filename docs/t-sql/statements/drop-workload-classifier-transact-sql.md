---
title: "DROP WORKLOAD Classifier (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "05/01/2019"
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
ms.assetid: 
author: ronortloff
ms.author: rortloff
manager: craigg
monikerRange: "=azure-sqldw-latest||=sqlallproducts-allversions"
---
# DROP WORKLOAD CLASSIFIER (Transact-SQL)

[!INCLUDE[tsql-appliesto-xxxxxx-xxxx-asdw-xxx-md](../../includes/tsql-appliesto-xxxxxx-xxxx-asdw-xxx-md.md)]

Drops an existing user-defined Workload Management Classifier.  
  
![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md).  
  
## Syntax  

```
DROP WORKLOAD CLASSIFIER classifier_name;
```

## Arguments

*classifier_name*  
Specifies the name by which the workload classifier is identified.  classifier_name is a sysname.  It can be up to 128 characters long and must be unique within the instance.
  
## Remarks

The DROP WORKLOAD CLASSIFIER statement is not allowed on system workload classifiers.

If requests are running or in the request queue in suspended state, they will keep their classification and the classifier can be dropped immediately.  Dropping and recreating the classifier with different importance will not affect an already classified request.

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
[SQL Data Warehouse Workload Classification](/azure/sql-data-warehouse/sql-data-warehouse-workload-classification)
