---
title: "Connect to a Data Source (SSAS) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.asvs.bidtoolset.conndatasource.f1"
ms.assetid: 1e2b17e9-092b-4af1-8a58-c52b8fe10ff1
author: minewiskan
ms.author: owend
manager: craigg
---
# Connect to a Data Source (SSAS)
  This page of the **Table Import Wizard** enables you to create a new data source connection to a variety of data sources, such as relational databases, data feeds, and files. To access the wizard from the [!INCLUDE[ssBIDevStudio](../includes/ssbidevstudio-md.md)], on the **Model** menu, click **Import from Data Source**.  
  
 To connect to a data source, you must have the appropriate provider installed on your machine. You must also have the appropriate provider installed on the workspace database server. For 32-bit (x86) servers, 32-bit providers must be installed. For 64-bit (x64) servers, 64-bit providers must be installed.  
  
 [!INCLUDE[ssBIDevStudio](../includes/ssbidevstudio-md.md)] always runs in a 32-bit process, regardless of architecture. When running [!INCLUDE[ssBIDevStudio](../includes/ssbidevstudio-md.md)] on a 64-bit machine, you should be aware of the following when installing data providers:  
  
-   For providers that support side-by-side installation of 32-bit and 64-bit providers, you should install both providers.  
  
-   For the ACE provider, you must install the 64-bit version of the provider. Because Office automatically installs the ACE provider, you should not run a 32-bit version of Microsoft Office on a 64-bit machine hosting the workspace database server.  
  
     The ACE provider is used to import from Text, Excel, and Access files. If support for these data sources is not needed, you can then run a 32-bit version of Microsoft Office on a machine running a 64-bit workspace database server.  
  
-   For other providers that do not support side-by-side installation of 32-bit and 64-bit providers, you must install the 32-bit provider. If only 64-bit machines are available, you must use a remote machine with a 64-bit provider installed as the workspace database server.  
  
  
