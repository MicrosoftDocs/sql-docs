---
title: "Add an SSIS Scale Out Worker with Scale Out Manager | Microsoft Docs"
ms.custom: ""
ms.date: "07/18/2017"
ms.prod: "sql-server-2017"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "integration-services"
ms.tgt_pltfrm: ""
ms.topic: "article"
caps.latest.revision: 1
author: "haoqian"
ms.author: "haoqian"
manager: "jhubbard"
---
# Add a Scale Out Worker with Scale Out Manager

Integration Services Scale Out Manager greatly relieves the complexity to add Scale Out Worker to your existing Scale Out environment. 

The below steps allow you to add a Scale Out Worker to your Scale Out topology:

## 1. Install Scale Out Worker
In the SQL Server installation wizard, select Integration Services and Scale Out Worker on the **Feature Selection** page. 
![Feature Select Worker](media/feature-select-worker.PNG)

On the **Integration Services Scale Out Configuration - Worker Node** page, 
you can simply click "Next" to skip configuration here and use **Scale Out Manager** to do the configuration after installation.

Finish the installation wizard.

## 2. Open firewall on Scale Out Master computer
Open the port specified during the Scale Out Master installation (8391, by default) and the port of SQL Server (1433, by default), using Windows Firewall on the Scale Out Master computer.

## 3. Add Scale Out Worker with Scale Out Manager
Run SQL Server Management Studio as administrator and connect to the SQL Server instance of Scale Out Master.

Right-click **SSISDB** in the object explorer and select **Manage Scale Out...**. 

![Manage Scale Out](media/manage-scale-out.PNG)

In the popped up **Scale Out Manager**, switch to **Worker Manager**. Click "+" button and follow the instructions on Connect Worker dialog. For details, see [Scale Out Manager](integration-services-ssis-scale-out-manager.md).
