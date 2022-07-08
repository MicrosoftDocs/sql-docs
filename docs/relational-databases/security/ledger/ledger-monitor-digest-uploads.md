---
title: "Monitor Digest Uploads"
description: This article provides information on monitoring the digest uploads for ledger.
ms.date: "05/24/2022"
ms.service: sql-database
ms.subservice: security
ms.custom:
- event-tier1-build-2022
ms.reviewer: kendralittle, mathoma
ms.topic: conceptual
author: VanMSFT
ms.author: vanto
monikerRange: "= azuresqldb-current||>= sql-server-ver16||>= sql-server-linux-ver16"
---

# Monitor digest uploads

[!INCLUDE [Azure SQL Database](../../../includes/applies-to-version/asdb.md)]

::: zone pivot="as1-azure-sql-database"

You can monitor Failed and Successful Ledger Digest Uploads in the Azure portal in the Metrics view of the Azure SQL Database. 

:::image type="content" source="media/ledger/monitor-ledger-digest-uploads.png" alt-text="Metrics view of the Azure SQL Database Failed and Successful Ledger Digest Uploads in the Azure portal":::

We recommend to configure alerts on Failed Ledger Digest Uploads if you want to be notified when a digest upload failed. Optionally, you could also configure an alert on Successful Ledger Digest Uploads. This could be interesting if the number of uploads dropped under a certain value or zero due to someone that disabled the automatic digest upload. This is not considered as a failure but the upload just stopped.

::: zone-end

::: zone pivot="as1-sql-server"
[!INCLUDE [SQL Server 2022 Azure SQL Database](../../../includes/applies-to-version/sqlserver2022-asdb.md)]

You can monitor Failed and Successful Ledger Digest Uploads with [Extended Events](docs/relational-databases/extended-events/extended-events.md) in SQL Server. Select the events *ledger_digest_upload_failed* and *ledger_digest_upload_success* in the Extended Event session.

We recommend to configure an alert on event number *37417 - Uploading ledger digest failed*.
This could be done with a [SQL Agent Alert](docs/ssms/agent/create-an-alert-using-an-error-number.md) or your favorite third-party monitoring tool. 

::: zone-end

## Next steps

- [Ledger overview](ledger-overview.md)
- [Enable automatic digest storage](ledger-how-to-enable-automatic-digest-storage.md)