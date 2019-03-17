---
title: "Importing and Exporting Knowledge | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: data-quality-services
ms.topic: conceptual
ms.assetid: 12537c9d-31e4-40b0-a411-cb343abbe96a
author: leolimsft
ms.author: lle
manager: craigg
---
# Importing and Exporting Knowledge
  You can create knowledge bases and domains directly in the [!INCLUDE[ssDQSClient](../includes/ssdqsclient-md.md)] application, or you can import knowledge into, or export it from, a knowledge base. In the [!INCLUDE[ssDQSClient](../includes/ssdqsclient-md.md)] application, you can use a data file for import and export operations, or an Excel file for import operations. The data file used is an encrypted file that is created by [!INCLUDE[ssDQSnoversion](../includes/ssdqsnoversion-md.md)] (DQS) with a .dqs extension. The files created by Microsoft Excel can have an extension of .xlsx, .xls, or .csv. These operations give you more flexibility in building and sharing the knowledge that you use to perform data cleansing and matching.  
  
> [!IMPORTANT]  
>  You can export *all* the knowledge bases in your [!INCLUDE[ssDQSServer](../includes/ssdqsserver-md.md)] to a DQS backup file (.dqsb) at once by running the DQSInstaller.exe file from the command prompt. Similarly, you can import *all* the knowledge bases from a DQS backup file (.dqsb) to your [!INCLUDE[ssDQSServer](../includes/ssdqsserver-md.md)] at once by running the DQSInstaller.exe file from the command prompt. For information about doing so, see [Export and Import DQS Knowledge Bases Using DQSInstaller.exe](install-windows/export-and-import-dqs-knowledge-bases-using-dqsinstaller-exe.md) in the DQS installation guide.  
  
## In This Section  
 You can perform the following import and export operations:  
  
|||  
|-|-|  
|Export a domain in a knowledge base to a .dqs data file|[Export a Domain to a .dqs File](../../2014/data-quality-services/export-a-domain-to-a-dqs-file.md)|  
|Import a domain from a .dqs data file into an existing knowledge base|[Import a Domain from a .dqs File](../../2014/data-quality-services/import-a-domain-from-a-dqs-file.md)|  
|Export an entire knowledge base to a .dqs data file|[Export a Knowledge Base to a .dqs File](../../2014/data-quality-services/export-a-knowledge-base-to-a-dqs-file.md)|  
|Import an entire knowledge base to a .dqs data file|[Import a Knowledge Base from a .dqs File](../../2014/data-quality-services/import-a-knowledge-base-from-a-dqs-file.md)|  
|Import values from an Excel file into a domain|[Import Values from an Excel File into a Domain](../../2014/data-quality-services/import-values-from-an-excel-file-into-a-domain.md)|  
|Import domains from an Excel file into a knowledge base|[Import Domains from an Excel File in Knowledge Discovery](../../2014/data-quality-services/import-domains-from-an-excel-file-in-knowledge-discovery.md)|  
|Import knowledge gathered during cleansing into a knowledge base|[Import Cleansing Project Values into a Domain](../../2014/data-quality-services/import-cleansing-project-values-into-a-domain.md)|  
  
## Related Tasks  
  
|Task Description|Topic|  
|----------------------|-----------|  
|Building a knowledge base by running knowledge discovery and interactively managing knowledge|[Building a Knowledge Base](../../2014/data-quality-services/building-a-knowledge-base.md)|  
|Creating a single domain, and adding knowledge to the domain.|[Managing a Domain](../../2014/data-quality-services/managing-a-domain.md)|  
|Creating a composite domain, and adding knowledge to the domain.|[Managing a Composite Domain](../../2014/data-quality-services/managing-a-composite-domain.md)|  
  
  
