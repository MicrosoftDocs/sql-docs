---
title: "rsModelGenerationError - Reporting Services Error | Microsoft Docs"
description: "In this error reference page, learn about event ID 'rsModelGenerationError': An error occurred while generating model."
ms.date: 03/14/2017
ms.service: reporting-services
ms.subservice: troubleshooting


ms.topic: conceptual
helpviewer_keywords: 
  - "rsModelGenerationError"
ms.assetid: 3a0ad63f-87f9-4ca1-b0c2-c85fa991634a
author: maggiesMSFT
ms.author: maggies
---
# rsModelGenerationError - Reporting Services Error
    
## Details  
  
|Category|Value|  
|-|-|  
|Product Name|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]|  
|Event ID|rsRenderingError|  
|Event Source|Microsoft.ReportingServices.Diagnostics.Utilities.ErrorStrings|  
|Component|[!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)]|  
|Message Text|An error occurred while generating model. (rsModelGenerationError) (ReportingServicesLibrary) %1|  
  
## Explanation  
 The report model could not be generated. In [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]2005 SP1 and earlier versions, this error is most likely displayed when the System.Data.DataSet object cannot handle a table or relationship within the database schema, such as when, for example, two foreign keys are defined on the same column within a table.  
  
## User Action  
 To determine the specific reason that caused this message to appear, review the report server log files, which are located at \Microsoft SQL Server\\<SQL Server Instance\>\Reporting Services\LogFiles.  
  
## Internal-Only  
  
