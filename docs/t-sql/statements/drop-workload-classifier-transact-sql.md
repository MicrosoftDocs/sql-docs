---
title: "DROP WORKLOAD Classifier (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "02/06/2019"
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
monikerRange: "||=azure-sqldw-latest||=sqlallproducts-allversions"
---
# DROP WORKLOAD CLASSIFIER (Transact-SQL) (Preview)
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
 The DROP WORKLOAD CLASSIFIER  statement is not allowed on the default workload classifier.  
  
 When you are executing DDL statements, we recommend that you be familiar with Resource Governor states. For more information, see [Resource Governor](../../relational-databases/resource-governor/resource-governor.md).  
  
 If a workload group contains active sessions, dropping or moving the workload group to a different resource pool will fail when the ALTER RESOURCE GOVERNOR RECONFIGURE statement is called to apply the change. To avoid this problem, you can take one of the following actions:  
  
-   Wait until all the sessions from the affected group have disconnected, and then rerun the ALTER RESOURCE GOVERNOR RECONFIGURE statement.  
  
-   Explicitly stop sessions in the affected group by using the KILL command, and then rerun the ALTER RESOURCE GOVERNOR RECONFIGURE statement.  
  
-   Restart the server. After the restart process is completed, the deleted group will not be created, and a moved group will use the new resource pool assignment.  
  
-   In a scenario in which you have issued the DROP WORKLOAD GROUP statement but decide that you do not want to explicitly stop sessions to apply the change, you can re-create the group by using the same name that it had before you issued the DROP statement, and then move the group to the original resource pool. To apply the changes, run the ALTER RESOURCE GOVERNOR RECONFIGURE statement.  
  
## Permissions  
 Requires CONTROL DATABASE permission.  
  
## Examples  
 The following example drops the workload classifier named `wgcELTROLE`.  
  
```  
DROP WORKLOAD CLASSIFIER wgcELTRole;
```

> [!NOTE]
> A request submitted without a matching classifier, is classified to the default workload group.  The default workload group is currently the smallrc resource class.
  
## See Also 
[CREATE WORKLOAD CLASSIFIER &#40;Transact-SQL&#41;](../../t-sql/statements/create-workload-classifier-transact-sql.md)

  
  
