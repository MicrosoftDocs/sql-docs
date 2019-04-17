---
title: "ADO NET Source Editor (Columns Page) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "integration-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.dts.designer.adonetsource.columns.f1"
ms.assetid: fbc205b9-2001-4737-a76b-1ba777344bd9
author: janinezhang
ms.author: janinez
manager: craigg
---
# ADO NET Source Editor (Columns Page)
  Use the **Columns** page of the **ADO NET Source Editor** dialog box to map an output column to each external (source) column.  
  
 To learn more about the ADO NET source, see [ADO NET Source](data-flow/ado-net-source.md).  
  
 **To open the Columns page**  
  
1.  In [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)], open the [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] package that has the ADO NET source.  
  
2.  On the **Data Flow** tab, double-click the ADO NET source.  
  
3.  In the **ADO NET Source Editor**, click **Columns**.  
  
## Options  
 **Available External Columns**  
 View the list of available external columns in the data source. You cannot use this table to add or delete columns.  
  
 **External Column**  
 View external (source) columns in the order in which you will see them when configuring components that consume data from this source.  
  
 **Output Column**  
 Provide a unique name for each output column. The default is the name of the selected external (source) column; however, you can choose any unique, descriptive name. The name provided will be displayed within the [!INCLUDE[ssIS](../includes/ssis-md.md)] Designer.  
  
## See Also  
 [ADO NET Source Editor &#40;Connection Manager Page&#41;](../../2014/integration-services/ado-net-source-editor-connection-manager-page.md)   
 [ADO NET Source Editor &#40;Error Output Page&#41;](../../2014/integration-services/ado-net-source-editor-error-output-page.md)   
 [ADO.NET Connection Manager](connection-manager/ado-net-connection-manager.md)  
  
  
