---
title: "ADO NET Destination | Microsoft Docs"
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
  - "sql13.dts.designer.adonetdest.f1"
helpviewer_keywords: 
  - "destinations [Integration Services], ADO.NET"
  - "ADO.NET destination"
ms.assetid: cb883990-d875-4d8b-b868-45f9f15ebeae
caps.latest.revision: 28
author: "douglaslMS"
ms.author: "douglasl"
manager: "jhubbard"
---
# ADO NET Destination
  The ADO NET destination loads data into a variety of [!INCLUDE[vstecado](../../includes/vstecado-md.md)]-compliant databases that use a database table or view. You have the option of loading this data into an existing table or view, or you can create a new table and load the data into the new table.  
  
 You can use the ADO NET destination to connect to [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssSDSFull](../../includes/sssdsfull-md.md)]. Connecting to [!INCLUDE[ssSDS](../../includes/sssds-md.md)] by using OLE DB is not supported. For more information about [!INCLUDE[ssSDS](../../includes/sssds-md.md)], see [General Guidelines and Limitations (Windows Azure SQL Database)](http://go.microsoft.com/fwlink/?LinkId=248228).  
  
## Troubleshooting the ADO NET Destination  
 You can log the calls that the ADO NET destination makes to external data providers. You can use this logging capability to troubleshoot the saving of data to external data sources that the ADO NET destination performs. To log the calls that the ADO NET destination makes to external data providers, enable package logging and select the **Diagnostic** event at the package level. For more information, see [Troubleshooting Tools for Package Execution](../../integration-services/troubleshooting/troubleshooting-tools-for-package-execution.md).  
  
## Configuring the ADO NET Destination  
 This destination uses an [!INCLUDE[vstecado](../../includes/vstecado-md.md)] connection manager to connect to a data source and the connection manager specifies the [!INCLUDE[vstecado](../../includes/vstecado-md.md)] provider to use. For more information, see [ADO.NET Connection Manager](../../integration-services/connection-manager/ado-net-connection-manager.md).  
  
 An ADO NET destination includes mappings between input columns and columns in the destination data source. You do not have to map input columns to all destination columns. However, the properties of some destination columns can require the mapping of input columns. Otherwise, errors might occur. For example, if a destination column does not allow for null values, you must map an input column to that destination column. In addition, the data types of mapped columns must be compatible. For example, you cannot map an input column with a string data type to a destination column with a numeric data type if the [!INCLUDE[vstecado](../../includes/vstecado-md.md)] provider does not support this mapping.  
  
> [!NOTE]  
>  [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] does not support inserting text into columns whose data type is set to image. For more information about [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data types, see [Data Types &#40;Transact-SQL&#41;](../../t-sql/data-types/data-types-transact-sql.md).  
  
> [!NOTE]  
>  The ADO NET destination does not support mapping an input column whose type is set to DT_DBTIME to a database column whose type is set to datetime. For more information about [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] data types, see [Integration Services Data Types](../../integration-services/data-flow/integration-services-data-types.md).  
  
 The ADO NET destination has one regular input and one error output.  
  
 You can set properties through [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer or programmatically.  
  
 For more information about the properties that you can set in the **ADO NET Destination Editor** dialog box, click one of the following topics:  
  
-   [ADO NET Destination Editor &#40;Connection Manager Page&#41;](../../integration-services/data-flow/ado-net-destination-editor-connection-manager-page.md)  
  
-   [ADO NET Destination Editor &#40;Mappings Page&#41;](../../integration-services/data-flow/ado-net-destination-editor-mappings-page.md)  
  
-   [ADO NET Destination Editor &#40;Error Output Page&#41;](../../integration-services/data-flow/ado-net-destination-editor-error-output-page.md)  
  
 The **Advanced Editor** dialog box reflects the properties that can be set programmatically. For more information about the properties that you can set in the **Advanced Editor** dialog box or programmatically, click one of the following topics:  
  
-   [Common Properties](http://msdn.microsoft.com/library/51973502-5cc6-4125-9fce-e60fa1b7b796)  
  
-   [ADO NET Custom Properties](../../integration-services/data-flow/ado-net-custom-properties.md)  
  
 For more information about how to set properties, see [Set the Properties of a Data Flow Component](../../integration-services/data-flow/set-the-properties-of-a-data-flow-component.md).  
  
  