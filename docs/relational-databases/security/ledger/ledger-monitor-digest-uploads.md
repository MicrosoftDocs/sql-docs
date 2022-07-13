---
title: "Monitor Digest Uploads"
description: This article provides information on monitoring the digest uploads for ledger.
ms.date: "05/24/2022"
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

You can monitor failed and successful ledger digest uploads in the Azure portal in the **Metrics** view of the Azure SQL Database. 

:::image type="content" source="media/ledger/monitor-ledger-digest-uploads.png" alt-text="Metrics view of the Azure SQL Database failed and successful ledger digest uploads in the Azure portal":::

We recommend you configure alerts on failed ledger digest uploads if you want to be notified when a digest upload failed. Failures might occur due to revoked permissions on the storage account or network configuration that makes the storage account inaccessible. Optionally, you could also configure an alert on successful ledger digest uploads. This could be interesting if the number of uploads dropped under a certain value or zero, due to someone that disabled the automatic digest upload. This would not be considered a failure since the digest upload was explicitly disabled.

::: zone-end

::: zone pivot="as1-sql-server"

You can monitor failed and successful ledger digest uploads with [Extended Events](../../extended-events/extended-events.md) in SQL Server. Select the events *ledger_digest_upload_failed* and *ledger_digest_upload_success* in the Extended Event session.

We recommend you configure an alert on event number *37417 - Uploading ledger digest failed*.
This could be done with a [SQL Agent Alert](../../../ssms/agent/create-an-alert-using-an-error-number.md) or your favorite third-party monitoring tool. 

::: zone-end

## Next steps

- [Ledger overview](ledger-overview.md)
- [Enable automatic digest storage](ledger-how-to-enable-automatic-digest-storage.md)
