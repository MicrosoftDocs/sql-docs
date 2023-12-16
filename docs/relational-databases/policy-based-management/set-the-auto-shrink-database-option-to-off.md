---
title: "Set the AUTO_SHRINK Database Option to OFF"
description: "Set the AUTO_SHRINK Database Option to OFF."
author: VanMSFT
ms.author: vanto
ms.date: 12/15/2023
ms.service: sql
ms.subservice: security
ms.topic: reference
helpviewer_keywords:
  - "Best Practices [Database Engine]"
---
# Set the auto_shrink database option to off

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

This rule checks whether the AUTO_SHRINK database option is set to OFF. Frequently shrinking and expanding a database can lead to physical fragmentation.

## Best practices recommendations

Set the AUTO_SHRINK database option to OFF. If you know that the space that you're reclaiming won't be needed in the future, you can reclaim the space by manually shrinking the database.

## For more information

Microsoft Knowledge Base article [315512](/troubleshoot/sql/admin/considerations-autogrow-autoshrink)

## Related content

- [Monitor and Enforce Best Practices by Using Policy-Based Management](monitor-and-enforce-best-practices-by-using-policy-based-management.md)
