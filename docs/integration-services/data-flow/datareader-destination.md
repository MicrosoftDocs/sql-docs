---
description: "DataReader Destination"
title: "DataReader Destination | Microsoft Docs"
ms.custom: ""
ms.date: "03/17/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: integration-services
ms.topic: conceptual
f1_keywords: 
  - "sql13.dts.designer.datareaderdest.f1"
helpviewer_keywords: 
  - "DataReader destination"
  - "destinations [Integration Services], DataReader"
ms.assetid: 56fcc4bf-c901-42c3-a71d-110039293431
author: chugugrace
ms.author: chugu
---
# DataReader Destination

[!INCLUDE[sqlserver-ssis](../../includes/applies-to-version/sqlserver-ssis.md)]


  The DataReader destination exposes the data in a data flow by using the ADO.NET **DataReader** interface. The data can then be consumed by other applications. For example, you can configure the data source of a [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] report to use the result of running a [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] package. To do this, you create a data flow that implements the DataReader destination.  
  
 For information about accessing and reading values in the DataReader destination programmatically, see [Loading the Output of a Local Package](../../integration-services/run-manage-packages-programmatically/loading-the-output-of-a-local-package.md).  
  
## Configuration of the DataReader Destination  
 You can specify a time-out value for the DataReader destination and indicate whether the destination should fail if a time-out occurs. A time-out occurs if the application does not ask for data within the specified time.  
  
 The DataReader destination has one input. It does not support an error output.  
  
 You can set properties through [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer or programmatically.  
  
 For more information about the properties that you can set in the **Advanced Editor** dialog box or programmatically, click one of the following topics:  
  
-   [Common Properties](./set-the-properties-of-a-data-flow-component.md)  
  
-   [DataReader Destination Custom Properties](../../integration-services/data-flow/datareader-destination-custom-properties.md)  
  
 For more information about how to set properties, see [Set the Properties of a Data Flow Component](../../integration-services/data-flow/set-the-properties-of-a-data-flow-component.md).  
  
