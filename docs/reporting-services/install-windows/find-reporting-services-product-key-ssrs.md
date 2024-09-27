---
title: "Find the product key for SQL Server Reporting Services"
description: "Learn how to find the  product key for SQL Server Reporting Services (SSRS) 2017 and 2019 so you can install your server in a production environment."
author: maggiesMSFT
ms.author: maggies
ms.date: 09/25/2024
ms.service: reporting-services
ms.subservice: report-server
ms.topic: conceptual
ms.custom:
  - updatefrequency5
monikerRange: "=sql-server-2017||=sql-server-ver15"
---
# Find the product key for SQL Server Reporting Services

[!INCLUDE[ssrs-appliesto](../../includes/ssrs-appliesto.md)] [!INCLUDE [ssrs-appliesto-2017](../../includes/ssrs-appliesto-2017.md)][!INCLUDE [ssrs-appliesto-2019](../../includes/ssrs-appliesto-2019.md)] [!INCLUDE[ssrs-appliesto-not-pbirsi](../../includes/ssrs-appliesto-not-pbirs.md)]

Learn how to find the  product key for SQL Server Reporting Services (SSRS) 2017 and later so you can install your server in a production environment.

To find your product key, you start by downloading and running setup for SQL Server.

1. [Download SQL Server](../../database-engine/install-windows/install-sql-server.md).
1. Run SQL Server setup and copy the prepopulated key:

    :::image type="content" source="media/find-reporting-services-product-key-ssrs/ssrs-ss2017-copy-product-key.png" alt-text="Screenshot of the SQL Server 2017 Setup window highlighting the field for the product key.":::

1. [Download Reporting Services](install-reporting-services.md), run setup, and paste the key:

    :::image type="content" source="media/find-reporting-services-product-key-ssrs/ssrs-ssrs2017-paste-product-key.png" alt-text="Screenshot of the SQL Server 2017 Setup window highlighting the area to enter the key.":::

You should only have to do this step the first time you install Reporting Services. Servicing updates shouldn't require you to enter the key.

## Volume licensing in the Microsoft 365 Admin Center
If your volume licensing has moved to the Microsoft 365 Admin Center, you can find your product key by choosing a SQL Server version and then looking under the View Downloads section:

**Billing** > **Your Products** > **Volume Licensing** > **View Downloads and Keys** > **Find SQL Server** > **View Downloads** > **View additional instructions**.

:::image type="content" source="media/find-reporting-services-product-key-ssrs/m365-additional-instructions.png" alt-text="Screenshot of View additional instructions.":::

## Related content

- [Install SQL Server Reporting Services](install-reporting-services.md)
- [Try asking the Reporting Services forum](https://go.microsoft.com/fwlink/?LinkId=620231)
