---
title: "Extract Change Data Using the CDC Source | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
ms.assetid: 604fbafb-15fa-4d11-8487-77d7b626eed8
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# Extract Change Data Using the CDC Source
  To add and configure a CDC source, the package must already include at least one Data Flow task and a CDC Control task.  
  
 For more information about the CDC Control task, see [CDC Control Task](../../integration-services/control-flow/cdc-control-task.md).  
  
 For more information about the CDC source, see [CDC Source](../../integration-services/data-flow/cdc-source.md).  
  
### To extract change data using a CDC source  
  
1.  In [!INCLUDE[ssBIDevStudio](../../includes/ssbidevstudio-md.md)], open the [!INCLUDE[ssISCurrent](../../includes/ssiscurrent-md.md)] project that contains the package you want.  
  
2.  In the Solution Explorer, double-click the package to open it.  
  
3.  Click the **Data Flow** tab, and then from the **Toolbox**, drag the CDC source to the design surface.  
  
4.  Double-click the CDC source.  
  
5.  In the **CDC Source Editor** dialog box, on the **Connection Manager** page, select an existing ADO.NET connection manager from the list, or click **New** to create a new connection. The connection should be to a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database that contains the change tables to read.  
  
6.  Select the **CDC table** where you want to process changes.  
  
7.  Select or type in the name of the **CDC capture instance** with the CDC table to be read.  
  
     A captured source table can have one or two captured instances to handle seamless transitioning of table definition through schema changes. If more than one capture instance is defined for the source table being captured, select the capture instance you want to use here. The default capture instance name for a table [schema].[table] is \<schema>_\<table> but that actual capture instance names in use may be different. The actual table that is read from is the CDC table **cdc .\<capture-instance>_CT**.  
  
8.  Select the processing mode that best handles your processing needs. The possible options are:  
  
    -   **All**: Returns the changes in the current CDC range without the **Before Update** values.  
  
    -   **All with old values**: Returns the changes in the current CDC processing range including the old values (**Before Update**). For each Update operation, there will be two rows, one with the before-update values and one with the after-update value.  
  
    -   **Net**: Returns only one change row per source row modified in the current CDC processing range. If a source row was updated multiple times, the combined change is produced (for example, insert+update is produced as a single update and update+delete is produced as a single delete). When working in Net change processing mode, it is possible to split the changes to Delete, Insert and Update outputs and handle them in parallel because the single source row appears in more than one output.  
  
    -   **Net with update mask**: This mode is similar to the regular Net mode but it also adds boolean columns with the name pattern **__$\<column-name>\__Changed** that indicate changed columns in the current change row.  
  
    -   **Net with merge**: This mode is similar to the regular Net mode but with Insert and Update operations merged into a single Merge operation (UPSERT).  
  
9. Select the SSIS string package variable that maintains the CDC state for the current CDC context. For more information about the CDC state variable, see [Define a State Variable](../../integration-services/data-flow/define-a-state-variable.md).  
  
10. Select the **Include reprocessing indicator column** check box to create a special output column called **__$reprocessing**. This column has a value of **true** when the CDC processing range overlaps with the initial processing range (the range of LSNs corresponding to the period of initial load) or when a CDC processing range is reprocessed following an error in a previous run. This indicator column lets the SSIS developer handle errors differently when reprocessing changes (for example, actions such as a delete of a non-existing row and an insert that failed on a duplicate key can be ignored).  
  
     For more information, see [CDC Source Custom Properties](../../integration-services/data-flow/cdc-source-custom-properties.md).  
  
11. To update the mapping between external and output columns, click **Columns** and select different columns in the **External Column** list.  
  
12. Optionally update the values of the output columns by deleting values in the **Output Column** list.  
  
13. To configure the error output, click **Error Output**.  
  
14. You can click **Preview** to view up to 200 rows of data extracted by the CDC source.  
  
15. Click **OK**.  
  
## See Also  
 [CDC Source Editor &#40;Connection Manager Page&#41;](../../integration-services/data-flow/cdc-source-editor-connection-manager-page.md)   
 [CDC Source Editor &#40;Columns Page&#41;](../../integration-services/data-flow/cdc-source-editor-columns-page.md)   
 [CDC Source Editor &#40;Error Output Page&#41;](../../integration-services/data-flow/cdc-source-editor-error-output-page.md)  
  
  
