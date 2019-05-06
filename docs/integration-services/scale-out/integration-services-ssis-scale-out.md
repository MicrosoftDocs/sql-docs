---
title: "SQL Server Integration Services (SSIS) Scale Out | Microsoft Docs"
description: "This article provides an overview of the SQL Server Integration Services (SSIS) Scale Out feature, which provides high-performance execution of SSIS packages"
ms.custom: performance
ms.date: "12/13/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
ms.assetid: dcfbd1c5-c001-4fb7-b9ae-916e49ab6a96
author: janinezhang
ms.author: janinez
manager: craigg
---
# Integration Services (SSIS) Scale Out
SQL Server [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] (SSIS) Scale Out provides high-performance execution of SSIS packages by distributing package executions across multiple computers. After you set up Scale Out, you can run multiple package executions in parallel, in scale-out mode, from SQL Server Management Studio (SSMS).

## Components
[!INCLUDE[ssIS_md](../../includes/ssis-md.md)] Scale Out consists of an [!INCLUDE[ssIS_md](../../includes/ssis-md.md)] Scale Out Master and one or more [!INCLUDE[ssIS_md](../../includes/ssis-md.md)] Scale Out Workers.

-   The Scale Out Master is responsible for Scale Out management and receives package execution requests from users. For more info, see [Scale Out Master](integration-services-ssis-scale-out-master.md).

-   The Scale Out Workers pull execution tasks from the Scale Out Master and run the packages. For more info, see [Scale Out Worker](integration-services-ssis-scale-out-worker.md).

## Configuration options
You can set up Scale Out in the following configurations:

-   **On a single computer**, where a Scale Out Master and a Scale Out Worker run side by side on the same computer.

-   **On multiple computers**, where each Scale Out Worker is on a different computer.

## What you can do
After you set up Scale Out, you can do the following things:

-   Run multiple packages deployed to the SSISDB catalog in parallel. For more info, see [Run packages in Scale Out](run-packages-in-integration-services-ssis-scale-out.md).

-   Manage the Scale Out topology in the Scale Out Manager app. For more info, see [Integration Services Scale Out Manager](integration-services-ssis-scale-out-manager.md).

## Next steps
-   [Get started with Integration Services (SSIS) Scale Out on a single computer](get-started-with-ssis-scale-out-onebox.md)

-   [Walkthrough: Set up Integration Services Scale Out](walkthrough-set-up-integration-services-scale-out.md)
