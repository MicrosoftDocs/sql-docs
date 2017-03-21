---
title: "Active Operations Dialog Box | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "integration-services"
ms.tgt_pltfrm: ""
ms.topic: "article"
f1_keywords: 
  - "sql13.ssis.ssms.isoperations.executions.f1"
  - "sql13.ssis.ssms.isoperations.general.f1"
ms.assetid: 5bb0fcd6-0ce9-488a-85b8-25dddaa03cda
caps.latest.revision: 8
author: "douglaslMS"
ms.author: "douglasl"
manager: "jhubbard"
---
# Active Operations Dialog Box
  Use the **Active Operations** dialog box to view the status of currently running [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] operations on the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] server, such as deployment, validation, and package execution. This data is stored in the SSISDB catalog.  
  
 For more information about related [!INCLUDE[tsql](../../includes/tsql-md.md)] views, see [catalog.operations &#40;SSISDB Database&#41;](../../integration-services/system-views/catalog-operations-ssisdb-database.md), [catalog.validations &#40;SSISDB Database&#41;](../../integration-services/system-views/catalog-validations-ssisdb-database.md), and [catalog.executions &#40;SSISDB Database&#41;](../../integration-services/system-views/catalog-executions-ssisdb-database.md)  
  
 **What do you want to do?**  
  
-   [Open the Active Operations Dialog Box](../../integration-services/performance/active-operations-dialog-box.md#open_dialog)  
  
-   [Options](#options)  
  
##  <a name="open_dialog"></a> Open the Active Operations Dialog Box  
  
1.  Open [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)][!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)].  
  
2.  Connect Microsoft SQL Server Database Engine  
  
3.  In Object Explorer, expand the **Integration Services** node, right-click **SSISDB**, and then click **Active Operations**.  
  
## Configure the Options  
  
###  <a name="options"></a> Options  
 **Type**  
 Specifies the type of operation. The following are the possible values for the **Type** field and the corresponding values in the operations_type column of the Transact-SQL **catalog.operations** view.  
  
|||  
|-|-|  
|Integration Services initialization|1|  
|Operations cleanup (SQL Agent job)|2|  
|Project versions cleanup (SQL Agent job)|3|  
|Deploy project|101|  
|Restore project|106|  
|Create and start package execution|200|  
|Stop operation (stopping a validation or execution|202|  
|Validate project|300|  
|Validate package|301|  
|Configure catalog|1000|  
  
 **Stop**  
 Click to stop a currently running operation.  
  
  