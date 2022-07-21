---
title: "Monitor digest uploads"
description: This article provides information on monitoring the digest uploads for ledger.
ms.date: 07/25/2022
ms.service: sql-database
ms.subservice: security
ms.custom:
ms.reviewer:
ms.topic: conceptual
author: VanMSFT
ms.author: vanto
monikerRange: "= azuresqldb-current||>= sql-server-ver16||>= sql-server-linux-ver16"
zone_pivot_groups: as1-azuresql-sql
---

# Monitor digest uploads

[!INCLUDE [SQL Server 2022 Azure SQL Database](../../../includes/applies-to-version/sqlserver2022-asdb.md)]

::: zone pivot="as1-azure-sql-database"

You can monitor failed and successful ledger digest uploads in the [Azure portal](https://portal.azure.com) in the **Metrics** view of your Azure SQL Database.

:::image type="content" source="media/ledger/monitor-ledger-digest-uploads.png" alt-text="Metrics view of the Azure SQL Database failed and successful ledger digest uploads in the Azure portal.":::

::: zone-end

::: zone pivot="as1-sql-server"

You can monitor failed and successful ledger digest uploads with [Extended Events](../../extended-events/extended-events.md) in SQL Server. Select the events *ledger_digest_upload_failed* and *ledger_digest_upload_success* in the Extended Event session.

::: zone-end

## Digest upload alerts recommendation

::: zone pivot="as1-azure-sql-database"

We recommend you configure alerts on failed ledger digest uploads if you want to be notified when a digest upload failed. Failures might occur due to revoked permissions on the storage account or network configuration that makes the storage account inaccessible. Optionally, you could also configure an alert on successful ledger digest uploads. If the number of successful ledger digest uploads drops under a certain value or zero due to someone disabling the automatic digest upload, the alert can help raise attention to this matter. Digest uploads that are explicitly disabled wouldn't be considered a failure in this case.

::: zone-end

::: zone pivot="as1-sql-server"

We recommend you configure an alert on event number *37417 - Uploading ledger digest failed*. The alert can be configured using [SQL Agent Alert](../../../ssms/agent/create-an-alert-using-an-error-number.md) or your favorite third-party monitoring tool.

::: zone-end

## Next steps

- [Ledger overview](ledger-overview.md)
- [Enable automatic digest storage](ledger-how-to-enable-automatic-digest-storage.md)
