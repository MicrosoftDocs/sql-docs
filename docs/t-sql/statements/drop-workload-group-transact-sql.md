---
title: "DROP WORKLOAD GROUP (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: 11/04/2019
ms.prod: sql
ms.prod_service: "sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "DROP_WORKLOAD_GROUP_TSQL"
  - "DROP WORKLOAD GROUP"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "DROP WORKLOAD GROUP statement"
author: CarlRabeler
ms.author: carlrab
monikerRange: ">=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azure-sqldw-latest||=azuresqldb-mi-current"
---
# DROP WORKLOAD GROUP (Transact-SQL)

## Click a product!

In the following row, click whichever product name you're interested in. The click displays different content here on this webpage, appropriate for whichever product you click.

::: moniker range=">=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current||=sqlallproducts-allversions"

> |||||
> |---|---|---|---|
> |**_\* SQL Server \*_** &nbsp;|[SQL Database<br />managed instance](drop-workload-group-transact-sql.md?view=azuresqldb-mi-current)|[SQL Data<br />Warehouse](drop-workload-group-transact-sql.md?view=azure-sqldw-latest)|

&nbsp;

## SQL Server and SQL Database managed instance


  Drops an existing user-defined Resource Governor workload group.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md).  
  
## Syntax  
  
```  
  
DROP WORKLOAD GROUP group_name  
[;]  
```  
  
## Arguments  
 *group_name*  
 Is the name of an existing user-defined workload group.  
  
## Remarks  
 The DROP WORKLOAD GROUP statement is not allowed on the Resource Governor internal or default groups.  
  
 When you are executing DDL statements, we recommend that you be familiar with Resource Governor states. For more information, see [Resource Governor](../../relational-databases/resource-governor/resource-governor.md).  
  
 If a workload group contains active sessions, dropping or moving the workload group to a different resource pool will fail when the ALTER RESOURCE GOVERNOR RECONFIGURE statement is called to apply the change. To avoid this problem, you can take one of the following actions:  
  
-   Wait until all the sessions from the affected group have disconnected, and then rerun the ALTER RESOURCE GOVERNOR RECONFIGURE statement.  
  
-   Explicitly stop sessions in the affected group by using the KILL command, and then rerun the ALTER RESOURCE GOVERNOR RECONFIGURE statement.  
  
-   Restart the server. After the restart process is completed, the deleted group will not be created, and a moved group will use the new resource pool assignment.  
  
-   In a scenario in which you have issued the DROP WORKLOAD GROUP statement but decide that you do not want to explicitly stop sessions to apply the change, you can re-create the group by using the same name that it had before you issued the DROP statement, and then move the group to the original resource pool. To apply the changes, run the ALTER RESOURCE GOVERNOR RECONFIGURE statement.  
  
## Permissions

 Requires CONTROL SERVER permission.  
  
## Examples

 The following example drops the workload group named `adhoc`.  
  
```  
DROP WORKLOAD GROUP adhoc;  
GO  
ALTER RESOURCE GOVERNOR RECONFIGURE;  
GO  
```  
  
## See Also  
 [Resource Governor](../../relational-databases/resource-governor/resource-governor.md)   
 [CREATE WORKLOAD GROUP &#40;Transact-SQL&#41;](../../t-sql/statements/create-workload-group-transact-sql.md)   
 [ALTER WORKLOAD GROUP &#40;Transact-SQL&#41;](../../t-sql/statements/alter-workload-group-transact-sql.md)   
 [CREATE RESOURCE POOL &#40;Transact-SQL&#41;](../../t-sql/statements/create-resource-pool-transact-sql.md)   
 [ALTER RESOURCE POOL &#40;Transact-SQL&#41;](../../t-sql/statements/alter-resource-pool-transact-sql.md)   
 [DROP RESOURCE POOL &#40;Transact-SQL&#41;](../../t-sql/statements/drop-resource-pool-transact-sql.md)   
 [ALTER RESOURCE GOVERNOR &#40;Transact-SQL&#41;](../../t-sql/statements/alter-resource-governor-transact-sql.md)  
  
::: moniker-end
::: moniker range="=azure-sqldw-latest||=sqlallproducts-allversions"

> ||||
> |---|---|---|
> |[SQL Server](drop-workload-group-transact-sql.md?view=sql-server-2017)||[SQL Database<br />managed instance](drop-workload-group-transact-sql.md?view=azuresqldb-mi-current)||**_\* SQL Data<br />Warehouse \*_** &nbsp;||||

&nbsp;

## SQL Data Warehouse (Preview)

Drops a workload group.  Once the statement completes, the settings are in effect.

 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md).

## Syntax

```
DROP WORKLOAD GROUP group_name  
```

## Arguments

 *group_name*  
 Is the name of an existing user-defined workload group.

## Remarks

A workload group cannot be dropped if classifiers exist for the workload group.  Drop the classifiers before the workload group is dropped.  If there are active requests using resources from the workload group being dropped, the drop workload statement is blocked behind them.

## Examples

Use the following code example to determine which classifiers need to be dropped before the workload group can be dropped.

```sql
SELECT c.name as classifier_name
      ,'DROP WORKLOAD CLASSIFIER '+c.name as drop_command
  FROM sys.workload_management_workload_classifiers c
  JOIN sys.workload_management_workload_groups g
    ON c.group_name = g.name
  WHERE g.name = 'wgXYZ' --change the filter to the workload being dropped
```

## Permissions

Requires CONTROL DATABASE permission

## See also
 [CREATE WORKLOAD GROUP &#40;Transact-SQL&#41;](../../t-sql/statements/create-workload-group-transact-sql.md)   
 
::: moniker-end
