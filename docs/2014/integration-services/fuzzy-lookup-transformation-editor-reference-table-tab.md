---
title: "Fuzzy Lookup Transformation Editor (Reference Table Tab) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "integration-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.dts.designer.fuzzylookuptransformation.referencetable.f1"
helpviewer_keywords: 
  - "Fuzzy Lookup Transformation Editor"
ms.assetid: 451f4e7d-1c8e-4784-b540-df0806509bf1
author: janinezhang
ms.author: janinez
manager: craigg
---
# Fuzzy Lookup Transformation Editor (Reference Table Tab)
  Use the **Reference Table** tab of the **Fuzzy Lookup Transformation Editor** dialog box to specify the source table and the index to use for the lookup. The reference data source must be a table in a [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] database.  
  
> [!NOTE]  
>  The Fuzzy Lookup transformation creates a working copy of the reference table. The indexes described below are created on this working table by using a special table, not an ordinary [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] index. The transformation does not modify the existing source tables unless you select **Maintain stored index**. In this case, it creates a trigger on the reference table that updates the working table and the lookup index table based on changes to the reference table.  
  
> [!NOTE]  
>  The `Exhaustive` and the `MaxMemoryUsage` properties of the Fuzzy Lookup transformation are not available in the **Fuzzy Lookup Transformation Editor**, but can be set by using the **Advanced Editor**. In addition, a value greater than 100 for `MaxOutputMatchesPerInput` can be specified only in the **Advanced Editor**. For more information on these properties, see the Fuzzy Lookup Transformation section of [Transformation Custom Properties](data-flow/transformations/transformation-custom-properties.md).  
  
 To learn more about the Fuzzy Lookup transformation, see [Fuzzy Lookup Transformation](data-flow/transformations/lookup-transformation.md).  
  
## Options  
 **OLE DB connection manager**  
 Select an existing OLE DB connection manager from the list, or create a new connection by clicking **New**.  
  
 **New**  
 Create a new connection by using the **Configure OLE DB Connection Manager** dialog box.  
  
 **Generate new index**  
 Specify that the transformation should create a new index to use for the lookup.  
  
 **Reference table name**  
 Select the existing table to use as the reference (lookup) table.  
  
 **Store new index**  
 Select this option if you want to save the new lookup index.  
  
 **New index name**  
 If you have chosen to save the new lookup index, type a descriptive name for the index.  
  
 **Maintain stored index**  
 If you have chosen to save the new lookup index, specify whether you also want [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] to maintain the index.  
  
> [!NOTE]  
>  When you select **Maintain stored index** on the **Reference Table** tab of the **Fuzzy Lookup Transformation Editor**, the transformation uses managed stored procedures to maintain the index. These managed stored procedures use the common language runtime (CLR) integration feature in [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]. By default, CLR integration in [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] is not enabled. To use the **Maintain stored index** functionality, you must enable CLR integration. For more information, see [Enabling CLR Integration](../relational-databases/clr-integration/clr-integration-enabling.md).  
>   
>  Because the **Maintain stored index** option requires CLR integration, this feature works only when you select a reference table on an instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] where CLR integration is enabled.  
  
 **Use existing index**  
 Specify that the transformation should use an existing index for the lookup.  
  
 **Name of an existing index**  
 Select a previously created lookup index from the list.  
  
## See Also  
 [Integration Services Error and Message Reference](../../2014/integration-services/integration-services-error-and-message-reference.md)   
 [Fuzzy Lookup Transformation Editor &#40;Columns Tab&#41;](../../2014/integration-services/fuzzy-lookup-transformation-editor-columns-tab.md)   
 [Fuzzy Lookup Transformation Editor &#40;Advanced Tab&#41;](../../2014/integration-services/fuzzy-lookup-transformation-editor-advanced-tab.md)  
  
  
