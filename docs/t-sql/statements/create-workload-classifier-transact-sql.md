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
monikerRange: "=azure-sqldw-latest||=sqlallproducts-allversions"
---
# CREATE WORKLOAD CLASSIFIER (Transact-SQL)

[!INCLUDE[tsql-appliesto-xxxxxx-xxxx-asdw-xxx-md](../../includes/tsql-appliesto-xxxxxx-xxxx-asdw-xxx-md.md)]

Creates a Workload Management Classifier.  The classifier assigns incoming requests to a workload group based on the parameters specified in the classifier statement definition.  Classifiers are evaluated with every request submitted.  If a request is not matched to a classifier, it is assigned to the default workload group.  The default workload group is the smallrc resource class.

> [!NOTE]
> Workload groups and resources classes are used interchangeably.  The workload classifier takes the place of sp_addrolemember resource class assignment.  After workload classifiers are created, execute sp_droprolemember to remove any redundant resource class mappings.

 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md).  
  
## Syntax

```
CREATE WORKLOAD CLASSIFIER classifier_name  
WITH  
    (   WORKLOAD_GROUP = ‘name’  
    ,   MEMBERNAME = ‘security_account’ 
[ [ , ] WLM_LABEL = ‘label’ ]  
[ [ , ] WLM_CONTEXT = ‘context’ ]  
[ [ , ] START_TIME = ‘HH:MM’ ]  
[ [ , ] END_TIME = ‘HH:MM’ ]  
  
[ [ , ] IMPORTANCE = { LOW | BELOW NORMAL | NORMAL | ABOVE NORMAL | HIGH }]) [;]

```

## Arguments

 *classifier_name*  
 Specifies the name by which the workload classifier is identified.  classifier_name is a sysname.  It can be up to 128 characters long and must be unique within the instance.

*WORKLOAD_GROUP* = *'name'*
When the conditions are met by the classifier rules, name maps the request to a workload group.  name is a sysname.  It can be up to 128 characters long and must be a valid workload group name at the time of classifier creation.

Available workload groups can be found in [sys.workload_management_workload_groups](/sql/relational-databases/system-catalog-views/sys-workload-management-workload-groups-transact-sql?view=azure-sqldw-latest
) catalog view.

*MEMBERNAME* ='security_account'*
This is the security account being added to the role.  Security_account is a sysname, with no default. Security_account can be a database user, database role, Azure Active Directory login, or Azure Active Directory group.

*WLM_LABEL*
Specifies the label value that a request can be classified against.  label is an optional parameter of type nvarchar(255).  Use the OPTION (LABEL) [link TDB] in the request match the classifier configuration.

Example:

```sql
CREATE WORKLOAD CLASSIFIER wcELTLoads WITH  
( WORKLOAD_GROUP = 'wgDataLoad'
 ,MEMBERNAME     = 'ELTRole'  
 ,WLM_LABEL      = 'fact_loads' )

SELECT COUNT(*) 
  FROM DimCustomer
  OPTION (LABEL = 'fact_loads')
```

*WLM_CONTEXT*
Specifies the session context value that a request can be classified against.  context is an optional parameter of type nvarchar(255).  Use the sys.sp_set_session_context with the variable name equal to ‘wlm_context’ prior to submitting a request to set the session context.

Example:

```sql
CREATE WORKLOAD CLASSIFIER wcDataLoad WITH  
( WORKLOAD_GROUP = 'wgDataLoad'
 ,MEMBERNAME     = 'ELTRole'
 ,WLM_CONTEXT    = 'dim_load' )
--set session context
EXEC sys.sp_set_session_context @key = 'wlm_context', @value = 'dim_load'

--run multiple statements using the wlm_context setting
SELECT COUNT(*) FROM stg.daily_customer_load
SELECT COUNT(*) FROM stg.daily_sales_load

--turn off the wlm_context session setting
EXEC sys.sp_set_session_context @key = 'wlm_context', @value = null
```

*START_TIME* and *END_TIME*
Specifies the start_time and end_time that a request can be classified against.  Both start_time and end_time are of the HH:MM format in UTC time zone.  Start_time and end_time must be specified together.

Example:
```sql
CREATE WORKLOAD CLASSIFIER wcELTLoads WITH  
( WORKLOAD_GROUP = 'wgDataLoads'
 ,MEMBERNAME     = 'ELTRole'  
 ,START_TIME     = '22:00'
 ,END_TIME       = '02:00' )
```


*IMPORTANCE* = { LOW | BELOW_NORMAL | NORMAL | ABOVE_NORMAL | HIGH }
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
[SQL Data Warehouse Classification](/azure/sql-data-warehouse/sql-data-warehouse-workload-classification)
