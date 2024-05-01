---
title: "rsModelGenerationError - Reporting Services error"
description: "In this error reference page, learn about event ID 'rsModelGenerationError': An error occurred while generating model."
author: maggiesMSFT
ms.author: maggies
ms.date: 03/14/2017
ms.service: reporting-services
ms.subservice: troubleshooting
ms.topic: conceptual
ms.custom: updatefrequency5
helpviewer_keywords:
  - "rsModelGenerationError"
---
# rsModelGenerationError - Reporting Services error
    
## Details  
  
|Category|Value|  
|-|-|  
|Product Name|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]|  
|Event ID|rsRenderingError|  
|Event Source|Microsoft.ReportingServices.Diagnostics.Utilities.ErrorStrings|  
|Component|[!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)]|  
|Message Text|An error occurred while generating model. (rsModelGenerationError) (ReportingServicesLibrary) %1|  
  
## Explanation  
 The report model couldn't be generated. In [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]2005 SP1 and earlier versions, this error is most likely displayed when the System.Data.DataSet object can't handle a table or relationship within the database schema. For example, when two foreign keys are defined on the same column within a table.  
  
## User action  
 Determine the specific reason that caused this message to appear. Then, review the report server log files, which are located at \Microsoft SQL Server\\&lt;SQL Server Instance&gt;\Reporting Services\LogFiles.  
  
## Internal-only  
  
