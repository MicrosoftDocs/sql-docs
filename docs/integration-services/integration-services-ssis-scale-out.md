---
title: "Integration Services (SSIS) Scale Out | Microsoft Docs"
ms.custom: ""
ms.date: "2016-12-16"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "integration-services"
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: dcfbd1c5-c001-4fb7-b9ae-916e49ab6a96
caps.latest.revision: 6
author: "douglaslMS"
ms.author: "douglasl"
manager: "jhubbard"
---
# Integration Services (SSIS) Scale Out
[!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] Scale Out provides high performance package execution by distributing executions to multiple machines. You are able to submit a request for multiple package executions in SQL Server Management Studio. These packages will be executed in parallel, in a scale out mode.  


## SSIS Scale Out Master and Scale Out Worker
[!INCLUDE[ssIS_md](../includes/ssis-md.md)] Scale Out consists of a [!INCLUDE[ssIS_md](../includes/ssis-md.md)] Scale Out Master and several [!INCLUDE[ssIS_md](../includes/ssis-md.md)] Scale Out Workers. The Scale Out Master is responsible for Scale Out management and receives package execution requests from users. Scale Out Workers pull execution tasks from the Scale Out Master and do the package execution work. For more information, see [Scale Out Master](../integration-services/integration-services-ssis-scale-out-master.md), [Scale Out Worker](../integration-services/integration-services-ssis-scale-out-worker.md).


## How to set up Scale Out
[!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] Scale Out can run on one machine, where a Scale Out Master and a Scale Out Worker are set up side-by-side on the machine. Scale Out can also run on multiple machines, where each Scale Out Worker is on a different machine.
- [Walkthrough: Set up Integration Services Scale Out](../integration-services/walkthrough-set-up-integration-services-scale-out.md)


## Run packages in Scale Out
Scale Out supports running multiple packages in the SSISDB catalog in parallel. For more details, see [Run packages in Scale Out](../integration-services/run-packages-in-integration-services-ssis-scale-out.md).

