---
title: "Get Started with SSIS Scale Out on a Single Computer| Microsoft Docs"
description: "This article shows you everything you need to know to get started with SSIS Scale Out on a single computer"
ms.custom: performance
ms.date: "12/13/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
author: "haoqian"
ms.author: "haoqian"
ms.reviewer: "douglasl"
manager: craigg
---
# Get started with Integration Services (SSIS) Scale Out on a single computer

[!INCLUDE[ssis-appliesto](../../includes/ssis-appliesto-ssvrpluslinux-asdb-asdw-xxx.md)]


This section provides guidance for setting up Integration Services Scale Out in a single-computer environment with the default settings.

## 1. Install SQL Server features
In the SQL Server installation wizard, on the **Feature Selection** page, select the following items:
-   Database Engine Services
-   Integration Services
    -   Scale Out Master
    -   Scale Out Worker

![Feature Selection first half of list](media/feature-select-onebox1.PNG)

![Feature Selection second half of list](media/feature-select-onebox2.PNG)

On the **Server Configuration** page, click **Next** to accept the default service accounts and startup types.

On the **Database Engine Configuration** page, select **Mixed Mode** and click **Add Current User**. 

![Engine Configuration](media/engine-config.PNG)

On the **Integration Services Scale Out Configuration - Master Node** and **Integration Services Scale Out Configuration - Worker Node** pages, click **Next** to accept the default settings for the port and certificates.

Finish the SQL Server installation Wizard.

## 2. Install SQL Server Management Studio

Download and install [SQL Server Management Studio (SSMS)](../../ssms/download-sql-server-management-studio-ssms.md).

## 3. Enable Scale Out
Open SSMS and connect to the local Sql Server instance.
In Object Explorer, right-click **Integration Services Catalogs** and select **Create Catalog**.

In the **Create Catalog** dialog, the option **Enable this server as SSIS scale out master** is selected by default.

## 4. Enable a Scale Out Worker
In SSMS, right-click **SSISDB** and select **Manage Scale Out**. 

![Manage Scale Out](media/manage-scale-out.PNG)

The Integration Services Scale Out Manager app opens. For more info, see [Scale Out Manager](integration-services-ssis-scale-out-manager.md).

To enable a Scale Out Worker, switch to **Worker Manager** and select the worker you want to enable. The workers are disabled by default. Click **Enable Worker** to enable the selected worker.

## 5. Run packages in Scale Out
Now you're ready to run SSIS packages in Scale Out. For more info, see [Run Packages in Integration Services (SSIS) Scale Out](run-packages-in-integration-services-ssis-scale-out.md).

## Next steps
-   [Add a Scale Out Worker with Scale Out Manager](add-scale-out-worker.md).
