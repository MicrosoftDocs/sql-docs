---
author: adbadram
ms.author: adbadram
ms.date: 04/01/2026
ms.service: azure-arc
ms.topic: include
---

> [!NOTE]
> After you [delete](../delete-from-azure-arc.md) a *SQL Server - Azure Arc* resource, the resource might continue to appear in the Azure portal for a period of time. This behavior is expected and is caused by Azure Resource Manager caching. The resource typically disappears after the cache refreshes. If the resource still appears after several hours, you can verify that it was successfully deleted by querying Azure Resource Graph or by using the Azure CLI. No further action is required — the resource isn't functional and doesn't incur charges after deletion.
