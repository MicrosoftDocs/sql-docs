---
title: "Disable Resource Governor"
description: Learn how to disable the Resource Governor by using either SQL Server Management Studio or Transact-SQL. You must have the CONTROL SERVER permission.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: 12/16/2022
ms.service: sql
ms.subservice: performance
ms.topic: conceptual
helpviewer_keywords:
  - "Resource Governor, disabling"
---
# Disable Resource Governor

[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

You can disable the Resource Governor by using either [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or Transact-SQL.

- **Before you begin:**  [Limitations and Restrictions](#LimitationsRestrictions), [Permissions](#Permissions)

- **To disable Resource Governor, using:**  [Object Explorer](#RGOffObjEx), [Resource Governor Properties](#RGOffProp), [Transact-SQL](#RGOffTSQL)

## <a id="BeforeYouBegin"></a> Before You Begin

 Disabling the Resource Governor has the following immediate effects:

- The classifier function is not run.

- All new connections are automatically classified into the default workload group.

- System-initiated requests are classified into the internal workload group.

- All existing workload group and resource pool settings are reset to their default values. In this case, no events are fired when limits are reached.

- Normal system monitoring is not affected.

- Configuration changes can be made, but the changes do not take effect until the Resource Governor is enabled.

- Upon restarting SQL Server, the Resource Governor will not load its configuration, but instead will have only the default and internal workload groups and resource pools.

### <a id="LimitationsRestrictions"></a> Limitations and Restrictions

 You cannot use the **ALTER RESOURCE GOVERNOR** statement to disable Resource Governor when in a user transaction.

### <a id="Permissions"></a> Permissions

 Disabling the Resource Governor requires CONTROL SERVER permission.

## <a id="RGOffObjEx"></a> Disable Resource Governor Using Object Explorer

 **To disable the Resource Governor by using Object Explorer**

1. In [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], open Object Explorer and recursively expand the **Management** node down to **Resource Governor**.

1. Right-click **Resource Governor**, and then select **Disable**.

## <a id="RGOffProp"></a> Disable Resource Governor Using Resource Governor Properties

 **To disable the Resource Governor by using the Resource Governor Properties page**

1. In [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], open Object Explorer and recursively expand the **Management** node down to **Resource Governor**.

1. Right-click **Resource Governor** and then select **Properties**, this opens the **Resource Governor Properties** page.

1. Select the **Enable Resource Governor** check box, ensure that the box is not selected, and then select **OK**.

## <a id="RGOffTSQL"></a> Disable Resource Governor Using Transact-SQL

 **To disable the Resource Governor by using Transact-SQL**

1. Run the **ALTER RESOURCE GOVERNOR DISABLE** statement.

### Example (Transact-SQL)

 The following example enables the Resource Governor.

```sql
ALTER RESOURCE GOVERNOR DISABLE;
GO
```

## Next steps

- [Resource Governor](../../relational-databases/resource-governor/resource-governor.md)
- [Enable Resource Governor](../../relational-databases/resource-governor/enable-resource-governor.md)
- [Resource Governor Resource Pool](../../relational-databases/resource-governor/resource-governor-resource-pool.md)
- [Resource Governor Workload Group](../../relational-databases/resource-governor/resource-governor-workload-group.md)
- [Resource Governor Classifier Function](../../relational-databases/resource-governor/resource-governor-classifier-function.md)
- [ALTER RESOURCE GOVERNOR (Transact-SQL)](../../t-sql/statements/alter-resource-governor-transact-sql.md)