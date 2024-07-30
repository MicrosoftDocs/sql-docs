---
title: "Monitor digest uploads"
description: This article provides information on monitoring the digest uploads for ledger.
author: VanMSFT
ms.author: vanto
ms.date: 11/14/2023
ms.service: azure-sql-database
ms.subservice: security
ms.custom: ignite-2023
ms.topic: conceptual
zone_pivot_groups: as1-azuresql-sql
monikerRange: "= azuresqldb-current||>= sql-server-ver16||>= sql-server-linux-ver16||=azuresqldb-mi-current"
---

# Monitor digest uploads

[!INCLUDE [SQL Server 2022 Azure SQL Database Azure SQL Managed Instance](../../../includes/applies-to-version/sqlserver2022-asdb-asmi.md)]

::: zone pivot="as1-azure-sql-database"

You can monitor failed and successful ledger digest uploads in the [Azure portal](https://portal.azure.com) in the **Metrics** view of your Azure SQL Database.

:::image type="content" source="media/ledger/monitor-ledger-digest-uploads.png" alt-text="Metrics view of the Azure SQL Database failed and successful ledger digest uploads in the Azure portal.":::

::: zone-end

::: zone pivot="as1-sql-server"

You can monitor failed and successful ledger digest uploads with [Extended Events](../../extended-events/extended-events.md) in SQL Server. Select the events *ledger_digest_upload_failed* and *ledger_digest_upload_success* in the Extended Event session.

::: zone-end

::: zone pivot="as1-azure-sql-managed-instance"

You can monitor failed and successful ledger digest uploads with [Extended Events](../../extended-events/extended-events.md) in Azure SQL Managed Instance. Select the events *ledger_digest_upload_failed* and *ledger_digest_upload_success* in the Extended Event session.

::: zone-end

## Digest upload alerts recommendation

::: zone pivot="as1-azure-sql-database"

We recommend you configure alerts on failed ledger digest uploads if you want to be notified when a digest upload failed. Failures might occur due to revoked permissions on the storage account or network configuration that makes the storage account inaccessible. Optionally, you could also configure an alert on successful ledger digest uploads. If the number of successful ledger digest uploads drops under a certain value or zero due to someone disabling the automatic digest upload, the alert can help raise attention to this matter. Digest uploads that are explicitly disabled wouldn't be considered a failure in this case.

::: zone-end

::: zone pivot="as1-sql-server"

We recommend you configure an alert on event number *37417 - Uploading ledger digest failed*. The alert can be configured using [SQL Agent Alert](../../../ssms/agent/create-an-alert-using-an-error-number.md) or your favorite third-party monitoring tool.

::: zone-end

::: zone pivot="as1-azure-sql-managed-instance"

We recommend you to use custom SQL Agent jobs to read and process an Extended Event session and alert using Database Mail. Another option is to use a scheduled Azure function, in case Database Mail cannot be used due to any reason.

::: zone-end

## Related content

- [Ledger overview](ledger-overview.md)
- [Enable automatic digest storage](ledger-how-to-enable-automatic-digest-storage.md)
