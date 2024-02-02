---
title: "CREATE WORKLOAD Classifier (Transact-SQL)"
description: CREATE WORKLOAD creates a classifier object for use in workload management.
author: markingmyname
ms.author: maghan
ms.reviewer: wiassaf, randolphwest
ms.date: 12/04/2023
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "WORKLOAD CLASSIFIER"
  - "WORKLOAD_CLASSIFIER_TSQL"
  - "CREATE WORKLOAD CLASSIFIER"
  - "CREATE_WORKLOAD_CLASSIFIER_TSQL"
helpviewer_keywords:
  - "CREATE WORKLOAD CLASSIFIER statement"
dev_langs:
  - "TSQL"
monikerRange: "=azure-sqldw-latest"
---
# CREATE WORKLOAD CLASSIFIER (Transact-SQL)

[!INCLUDE [asa](../../includes/applies-to-version/asa.md)]

Creates a classifier object for use in workload management. The classifier assigns incoming requests to a workload group based on the parameters specified in the classifier statement definition. Classifiers are evaluated with every request submitted. If a request isn't matched to a classifier, it's assigned to the default workload group. The default workload group is the `smallrc` resource class.

> [!NOTE]  
> Classifying managed identities (MI) behavior differs between the dedicated SQL pool in Azure Synapse workspaces and the standalone dedicated SQL pool (formerly SQL DW). While the standalone dedicated SQL pool MI maintains the assigned identity, Azure Synapse workspaces adds MI to the **dbo** role. This can't be changed. The **dbo** role, by default, is classified  to `smallrc`. Creating a classifier for the **dbo** role allows for assigning requests to a workload group other than `smallrc`. If **dbo** alone is too generic for classification and has broader impacts, consider using label, session or time-based classification in conjunction with the **dbo** role classification.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
CREATE WORKLOAD CLASSIFIER classifier_name
WITH
    ( WORKLOAD_GROUP = 'name'
    , MEMBERNAME = 'security_account'
    [ [ , ] WLM_LABEL = 'label' ]
    [ [ , ] WLM_CONTEXT = 'context' ]
    [ [ , ] START_TIME = 'HH:MM' ]
    [ [ , ] END_TIME = 'HH:MM' ]
    [ [ , ] IMPORTANCE = { LOW | BELOW_NORMAL | NORMAL | ABOVE_NORMAL | HIGH } ] )
[ ; ]
```

> [!NOTE]
> [!INCLUDE [synapse-analytics-od-unsupported-syntax](../../includes/synapse-analytics-od-unsupported-syntax.md)]

## Arguments

#### *classifier_name*

Specifies the name by which the workload classifier is identified. *classifier_name* is **sysname**. This parameter can be up to 128 characters long, and must be unique within the instance.

#### WORKLOAD_GROUP = '*name*'

When the conditions are met by the classifier rules, name maps the request to a workload group. *name* is **sysname**. This parameter can be up to 128 characters long, and must be a valid workload group name at the time of classifier creation.

Available workload groups can be found in [sys.workload_management_workload_groups](../../relational-databases/system-catalog-views/sys-workload-management-workload-groups-transact-sql.md) catalog view.

#### MEMBERNAME = '*security_account*'

The security account used to classify against. *security_account* is **sysname**, with no default. *security_account* can be a database user, database role, Microsoft Entra login, or Microsoft Entra group.

> [!NOTE]  
> Use the `user_name()` function, when connected to the system, to verify the `MEMBERNAME` that the classification process will use to classify the request. Verifying the `MEMBERNAME` with the `user_name()` function can be helpful troubleshooting Microsoft Entra ID or service principal classification issues. If `user_name()` returns `dbo`, you can use **dbo** as the `MEMBERNAME` to classify the requests. All members of the **dbo** role will be classified. Additional classification parameters such as `WLM_LABEL` or `WLM_CONTEXT` can also be used to specifically classify requests from multiple Microsoft Entra accounts mapping to the **dbo** role.

#### WLM_LABEL

Specifies the label value that a request can be classified against. Label is an optional parameter of type **nvarchar(255)**. Use the [OPTION (LABEL)](/azure/sql-data-warehouse/sql-data-warehouse-develop-label) in the request to match the classifier configuration.

For example:

```sql
CREATE WORKLOAD CLASSIFIER wcELTLoads
WITH (
    WORKLOAD_GROUP = 'wgDataLoad',
    MEMBERNAME = 'ELTRole',
    WLM_LABEL = 'dimension_loads'
);
    
SELECT COUNT(*) FROM DimCustomer
OPTION (LABEL = 'dimension_loads');
```

#### WLM_CONTEXT

Specifies the session context value that a request can be classified against. `WLM_CONTEXT` is an optional parameter of type **nvarchar(255)**. Use the [sys.sp_set_session_context](../../relational-databases/system-stored-procedures/sp-set-session-context-transact-sql.md?view=azure-sqldw-latest&preserve-view=true) with the variable name equal to `wlm_context` before submitting a request to set the session context.

For example:

```sql
CREATE WORKLOAD CLASSIFIER wcDataLoad
WITH (
    WORKLOAD_GROUP = 'wgDataLoad',
    MEMBERNAME = 'ELTRole',
    WLM_CONTEXT = 'dim_load'
);

--set session context
EXEC sys.sp_set_session_context @key = 'wlm_context', @value = 'dim_load';

--run multiple statements using the wlm_context setting
SELECT COUNT(*) FROM stg.daily_customer_load;
SELECT COUNT(*) FROM stg.daily_sales_load;

--turn off the wlm_context session setting
EXEC sys.sp_set_session_context @key = 'wlm_context', @value = NULL;
```

#### START_TIME and END_TIME

Specifies the start time and end time that a request can be classified against. Both `START_TIME` and `END_TIME` are of the `HH:mm` format in the UTC time zone. `START_TIME` and `END_TIME` must be specified together.

For example:

```sql
CREATE WORKLOAD CLASSIFIER wcELTLoads
WITH (
    WORKLOAD_GROUP = 'wgDataLoads',
    MEMBERNAME = 'ELTRole',
    START_TIME = '22:00',
    END_TIME = '02:00'
);
```

#### IMPORTANCE = { LOW | BELOW_NORMAL | NORMAL | ABOVE_NORMAL | HIGH }

Specifies the relative importance of a request. `IMPORTANCE` is one of the following options:

- `LOW`
- `BELOW_NORMAL`
- `NORMAL` (default)
- `ABOVE_NORMAL`
- `HIGH`

If `IMPORTANCE` isn't specified, the importance setting of the workload group is used. The default workload group importance is `NORMAL`. Importance influences the order which requests are scheduled, thus giving first access to resources and locks.

## Remarks

The maximum number of user-defined classifiers is `100`. Additional configurations beyond this limit aren't supported. We recommend that you group classifiers under a common category for efficient application of a single setting, across multiple workloads or users.

## Classification parameter weighting

A request can match against multiple classifiers. There's a weighting for the classifier parameters. The higher weighted matching classifier is used to assign a workload group and importance. The weighting goes as follows:

| Classifier parameter | Weight |
| --- | --- |
| `USER` | 64 |
| `ROLE` | 32 |
| `WLM_LABEL` | 16 |
| `WLM_CONTEXT` | 8 |
| `START_TIME`/`END_TIME` | 4 |

Consider the following classifier configurations.

```sql
CREATE WORKLOAD CLASSIFIER classifierA
WITH (
    WORKLOAD_GROUP = 'wgDashboards',
    MEMBERNAME = 'userloginA',
    IMPORTANCE = HIGH,
    WLM_LABEL = 'salereport'
);

CREATE WORKLOAD CLASSIFIER classifierB
WITH (
    WORKLOAD_GROUP = 'wgUserQueries',
    MEMBERNAME = 'userloginA',
    IMPORTANCE = LOW,
    START_TIME = '18:00',
    END_TIME = '07:00'
);
```

The user `userloginA` is configured for both classifiers. If `userloginA` runs a query with a label equal to `salesreport` between 6PM and 7AM UTC, the request is classified to the `wgDashboards` workload group with `HIGH` importance. The expectation might be to classify the request to `wgUserQueries` with `LOW` importance for off-hours reporting, but the weighting of `WLM_LABEL` is higher than `START_TIME`/`END_TIME`. The weighting of `classifierA` is `80` (`64` for the user, plus `16` for `WLM_LABEL`). The weighting of `classifierB` is `68` (`64` for the user, `4` for `START_TIME`/`END_TIME`). In this case, you can add `WLM_LABEL` to `classifierB`.

For more information, see [workload weighting](/azure/sql-data-warehouse/sql-data-warehouse-workload-classification#classification-weighting).

## Permissions

Requires `CONTROL DATABASE` permission.

## Examples

The following example shows how to create a workload classifier named `wgcELTRole`. It uses the `staticrc20` workload group, the user `ELTRole`, and sets the IMPORTANCE to `ABOVE_NORMAL`.

```sql
CREATE WORKLOAD CLASSIFIER wgcELTRole
WITH (
    WORKLOAD_GROUP = 'staticrc20',
    MEMBERNAME = 'ELTRole',
    IMPORTANCE = ABOVE_NORMAL
);
```

## Related content

- [[!INCLUDE[ssazuresynapse-md](../../includes/ssazuresynapse-md.md)] Classification](/azure/sql-data-warehouse/sql-data-warehouse-workload-classification)
- [DROP WORKLOAD CLASSIFIER (Transact-SQL)](drop-workload-classifier-transact-sql.md)
- [sys.workload_management_workload_classifiers](../../relational-databases/system-catalog-views/sys-workload-management-workload-classifiers-transact-sql.md)
- [sys.workload_management_workload_classifier_details](../../relational-databases/system-catalog-views/sys-workload-management-workload-classifier-details-transact-sql.md)
