---
title: "CDC Source Editor (Connection Manager Page) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "integration-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.ssis.designer.cdcsource.connection.f1"
ms.assetid: 304e6717-e160-4a7b-a06f-32182449fef8
author: janinezhang
ms.author: janinez
manager: craigg
---
# CDC Source Editor (Connection Manager Page)
  Use the **Connection Manager** page of the **CDC Source Editor** dialog box to select the ADO.NET connection manager for the [!INCLUDE[ssCurrent](../includes/sscurrent-md.md)] database that the CDC source reads change rows from (the CDC database). Once the CDC database is selected you need to select a captured table in the database.  
  
 For more information about the CDC source, see [CDC Source](data-flow/cdc-source.md).  
  
## Task List  
 **To open the CDC Source Editor Connection Manager Page**  
  
1.  In [!INCLUDE[ssBIDevStudio](../includes/ssbidevstudio-md.md)], open the [!INCLUDE[ssISCurrent](../includes/ssiscurrent-md.md)] package that has the CDC source.  
  
2.  On the **Data Flow** tab, double-click the CDC source.  
  
3.  In the **CDC Source Editor**, click **Connection Manager**.  
  
## Options  
 **ADO.NET connection manager**  
 Select an existing connection manager from the list, or click **New** to create a new connection. The connection must be to a [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] database that is enabled for CDC and where the selected change table is located.  
  
 **New**  
 Click **New**. The **Configure ADO.NET Connection Manager Editor** dialog box opens where you can create a new connection manager  
  
 **CDC Table**  
 Select the CDC source table that contains the captured changes that you want read and feed to downstream SSIS components for processing.  
  
 **Capture instance**  
 Select or type in the name of the CDC capture instance with the CDC table to be read.  
  
 A captured source table can have one or two captured instances to handle seamless transitioning of table definition through schema changes. If more than one capture instance is defined for the source table being captured, select the capture instance you want to use here. The default capture instance name for a table [schema].[table] is \<schema>_\<table> but that actual capture instance names in use may be different. The actual table that is read from is the CDC table **cdc .\<capture-instance>_CT**.  
  
 **CDC Processing Mode**  
 Select the processing mode that best handles your processing needs. The possible options are:  
  
-   **All**: Returns the changes in the current CDC range without the **Before Update** values.  
  
-   **All with old values**: Returns the changes in the current CDC processing range including the old values (**Before Update**). For each Update operation, there will be two rows, one with the before-update values and one with the after-update value.  
  
-   **Net**: Returns only one change row per source row modified in the current CDC processing range. If a source row was updated multiple times, the combined change is produced (for example, insert+update is produced as a single update and update+delete is produced as a single delete). When working in Net change processing mode, it is possible to split the changes to Delete, Insert and Update outputs and handle them in parallel because the single source row appears in more than one output.  
  
-   **Net with update mask**: This mode is similar to the regular Net mode but it also adds boolean columns with the name pattern **__$\<column-name>\__Changed** that indicate changed columns in the current change row.  
  
-   **Net with merge**: This mode is similar to the regular Net mode but with Insert and Update operations merged into a single Merge operation (UPSERT).  
  
> [!NOTE]  
>  For all Net change options, the source table must have a primary key or unique index. For tables without a primary key or unique indes, you must yse the **All** option.  
  
 **Variable containing the CDC state**  
 Select the SSIS string package variable that maintains the CDC state for the current CDC context. For more information about the CDC state variable, see [Define a State Variable](data-flow/define-a-state-variable.md).  
  
 **Include reprocessing indicator column**  
 Select this check box to create a special output column called **__$reprocessing**.  
  
 This column has a value of **true** when the CDC processing range overlaps with the initial processing range (the range of LSNs corresponding to the period of initial load) or when a CDC processing range is reprocessed following an error in a previous run. This indicator column lets the SSIS developer handle errors differently when reprocessing changes (for example, actions such as a delete of a non-existing row and an insert that failed on a duplicate key can be ignored).  
  
 For more information, see [CDC Source Custom Properties](data-flow/cdc-source-custom-properties.md).  
  
## See Also  
 [CDC Source Editor &#40;Columns Page&#41;](../../2014/integration-services/cdc-source-editor-columns-page.md)   
 [CDC Source Editor &#40;Error Output Page&#41;](../../2014/integration-services/cdc-source-editor-error-output-page.md)  
  
  
