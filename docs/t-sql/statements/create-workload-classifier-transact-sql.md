---
title: "CREATE WORKLOAD Classifier (Transact-SQL) | Microsoft Docs"
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
  - "CREATE WORKLOAD CLASSIFIER"
  - "CREATE_WORKLOAD_CLASSIFIER_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "CREATE WORKLOAD CLASSIFIER statement"
ms.assetid: 
author: ronortloff
ms.author: rortloff
manager: craigg
monikerRange: "=azure-sqldw-latest||=sqlallproducts-allversions"
---
# CREATE WORKLOAD CLASSIFIER (Transact-SQL)

[!INCLUDE[tsql-appliesto-xxxxxx-xxxx-asdw-xxx-md](../../includes/tsql-appliesto-xxxxxx-xxxx-asdw-xxx-md.md)]

Creates a Workload Management Classifier.  The classifier assigns incoming requests to a workload group and assigns importance based on the parameters specified in the classifier statement definition.  Classifiers are evaluated with every request submitted.  If a request is not matched to a classifier, it is assigned to the default workload group.  The default workload group is the smallrc resource class.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md).  
  
## Syntax

```
CREATE WORKLOAD CLASSIFIER classifier_name  
WITH  
    ( WORKLOAD_GROUP = 'name'  
     ,MEMBERNAME = 'security_account'
 [ [ , ] IMPORTANCE = { LOW | BELOW_NORMAL | NORMAL (default) | ABOVE_NORMAL | HIGH }])
[;]
```

## Arguments

 *classifier_name*  
 Specifies the name by which the workload classifier is identified.  classifier_name is a sysname.  It can be up to 128 characters long and must be unique within the instance.

WORKLOAD_GROUP = *'name'*
When the conditions are met by the classifier rules, name maps the request to a workload group.  name is a sysname.  It can be up to 128 characters long and must be a valid workload group name at the time of classifier creation.

WORKLOAD_GROUP should map to an existing resource class:

|Static Resource Classes|Dynamic Resource Classes|
|------------------------|-----------------------|
|staticrc10|smallrc|
|staticrc20|mediumrc|
|staticrc30|largerc|
|staticrc40|xlargerc|
|staticrc50||
|staticrc60||
|staticrc70||
|staticrc80||

MEMBERNAME = *'security_account'*
This is the security account being added to the role.  Security_account is a sysname, with no default. Security_account can be a database user, database role, Azure Active Directory login, or Azure Active Directory group.

IMPORTANCE = { LOW | BELOW_NORMAL | NORMAL | ABOVE_NORMAL | HIGH }
Specifies the relative importance of a request.  Importance is one of the following:

- LOW
- BELOW_NORMAL
- NORMAL (default)
- ABOVE_NORMAL
- HIGH  

Importance influences the order in which requests are scheduled, thus giving first access to resources and locks.

If a user is a member of multiple roles with different resource classes assigned or matched in multiple classifiers, the user is given the highest resource class assignment. For more information see, [workload classification](/azure/sql-data-warehouse/sql-data-warehouse-workload-classification#classification-precedence)

## Permissions

 Requires CONTROL DATABASE permission.  
  
## Examples

 The following example shows how to create a workload classifier named `wgcELTRole`. It uses the staticrc20 workload group, the user `ELTRole`, and sets the importance to `above_normal`.

```sql
CREATE WORKLOAD CLASSIFIER wgcELTRole
  WITH (WORKLOAD_GROUP = 'staticrc20'
       ,MEMBERNAME = 'ELTRole'
      ,IMPORTANCE = above_normal);
```

## See Also

[DROP WORKLOAD CLASSIFIER &#40;Transact-SQL&#41;](../../t-sql/statements/drop-workload-classifier-transact-sql.md)</br>
Catalog view [sys.workload_management_workload_classifier_details](../../relational-databases/system-catalog-views/sys-workload-management-workload-classifier-details-transact-sql.md)</br>
Catalog view [sys.workload_management_workload_classifiers](../../relational-databases/system-catalog-views/sys-workload-management-workload-classifiers-transact-sql.md)
[SQL Data Warehouse Classification](/azure/sql-data-warehouse/classification)
