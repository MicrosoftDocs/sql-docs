---
description: "Developing a User Interface for a Custom Log Provider"
title: "Developing a User Interface for a Custom Log Provider | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: integration-services
ms.topic: "reference"
helpviewer_keywords: 
  - "custom user interface [Integration Services], custom log providers"
  - "custom log providers [Integration Services], developing custom user interface"
ms.assetid: 6fd2d269-d87a-4134-82a1-40a09b3b5453
author: chugugrace
ms.author: chugu
---
# Developing a User Interface for a Custom Log Provider

[!INCLUDE[sqlserver-ssis](../../../includes/applies-to-version/sqlserver-ssis.md)]


  Many [!INCLUDE[ssISnoversion](../../../includes/ssisnoversion-md.md)] log providers have a custom user interface that implements <xref:Microsoft.SqlServer.Dts.Runtime.Design.IDtsLogProviderUI> and replaces the **Configuration** text box in the **Configure SSIS Logs** dialog box with a filtered dropdown list of available connection managers. However, custom user interfaces for custom log providers are not implemented in [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] [!INCLUDE[ssISnoversion](../../../includes/ssisnoversion-md.md)].  
  
## See Also  
 [Creating a Custom Log Provider](../../../integration-services/extending-packages-custom-objects/log-provider/creating-a-custom-log-provider.md)   
 [Coding a Custom Log Provider](../../../integration-services/extending-packages-custom-objects/log-provider/coding-a-custom-log-provider.md)  
  
  
