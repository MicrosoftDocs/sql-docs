---
title: "Find the product key for SQL Server Reporting Services | Microsoft Docs"
description: "Learn how to find the  product key for SQL Server Reporting Services (SSRS) 2017 and 2019 so you can install your server in a production environment."
ms.date: 12/04/2019
ms.service: reporting-services
ms.custom: seo-lt-2019​, seo-mmd-2019

ms.topic: conceptual
author: maggiesMSFT
ms.author: maggies
monikerRange: ">= sql-server-2017"
---
# Find the product key for SQL Server Reporting Services

[!INCLUDE[ssrs-appliesto](../../includes/ssrs-appliesto.md)] [!INCLUDE[ssrs-appliesto-2017-and-later](../../includes/ssrs-appliesto-2017-and-later.md)] [!INCLUDE[ssrs-appliesto-not-pbirsi](../../includes/ssrs-appliesto-not-pbirs.md)]

Learn how to find the  product key for SQL Server Reporting Services (SSRS) 2017 and 2019 so you can install your server in a production environment.

To find your product key, you start by downloading and running setup for SQL Server.

1. [Download SQL Server](../../database-engine/install-windows/install-sql-server.md).
1. Run SQL Server setup and copy the pre-populated key:

    ![Copy the SQL Server product key](media/find-reporting-services-product-key-ssrs/ssrs-ss2017-copy-product-key.png)

1. [Download Reporting Services](install-reporting-services.md), run setup, and paste the key:

     ![Paste the product key](media/find-reporting-services-product-key-ssrs/ssrs-ssrs2017-paste-product-key.png)

You should only have to do this step the first time you install Reporting Services. Servicing updates shouldn't require you to enter the key.

## Next steps

- [Install SQL Server Reporting Services](install-reporting-services.md)
- More questions? [Try asking the Reporting Services forum](https://go.microsoft.com/fwlink/?LinkId=620231)
