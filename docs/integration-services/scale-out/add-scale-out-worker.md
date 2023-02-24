---
title: "Add an SSIS Scale Out Worker with Scale Out Manager | Microsoft Docs"
description: "This article describes how to add an SSIS Scale Out Worker to an existing Scale Out environment by using Scale Out Manager."
ms.custom: performance
ms.date: "12/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: integration-services
ms.topic: conceptual
author: "haoqian"
ms.author: "haoqian"
---
# Add a Scale Out Worker with Scale Out Manager

[!INCLUDE[sqlserver-ssis](../../includes/applies-to-version/sqlserver-ssis.md)]



Integration Services Scale Out Manager simplifies the process of adding a Scale Out Worker to your existing Scale Out environment. 

Follow these steps to add a Scale Out Worker to your Scale Out topology:

## 1. Install Scale Out Worker
In the SQL Server installation wizard, select Integration Services and Scale Out Worker on the **Feature Selection** page. 
![Feature Select Worker](media/feature-select-worker.PNG)

On the **Integration Services Scale Out Configuration - Worker Node** page, you can click **Next** to skip configuration at this time and use **Scale Out Manager** to do the configuration after installation.

Finish the installation wizard.

## 2. Open the firewall on the Scale Out Master computer
Open the port specified during the Scale Out Master installation (8391, by default), and the port for SQL Server (1433, by default), in the Windows Firewall on the Scale Out Master computer.

## 3. Add a Scale Out Worker with Scale Out Manager
Run SQL Server Management Studio as administrator and connect to the SQL Server instance of Scale Out Master.

In Object Explorer, right-click **SSISDB** and select **Manage Scale Out**. 

![Manage Scale Out](media/manage-scale-out.PNG)

In the **Scale Out Manager** dialog box, switch to **Worker Manager**. Select **+** and follow the instructions in the **Connect Worker** dialog box. 

## Next steps
For more info, see [Scale Out Manager](integration-services-ssis-scale-out-manager.md).
