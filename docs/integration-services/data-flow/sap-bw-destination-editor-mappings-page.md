---
description: "SAP BW Destination Editor (Mappings Page)"
title: "SAP BW Destination Editor (Mappings Page) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: integration-services
ms.topic: conceptual
f1_keywords: 
  - "sql13.dts.designer.sapbwdestination.columns.f1"
ms.assetid: dfa1f1d6-6b64-4331-bdc5-eaa8b7aa41a1
author: chugugrace
ms.author: chugu
---
# SAP BW Destination Editor (Mappings Page)

[!INCLUDE[sqlserver-ssis](../../includes/applies-to-version/sqlserver-ssis.md)]


  Use the **Mappings** page of the **SAP BW Destination Editor** to map input columns to destination columns.  
  
 To learn more about the SAP BW destination of the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Connector 1.1 for SAP BW, see [SAP BW Destination](../../integration-services/data-flow/sap-bw-destination.md).  
  
> [!IMPORTANT]  
>  The documentation for the Microsoft Connector 1.1 for SAP BW assumes familiarity with the SAP Netweaver BW environment. For more information about SAP Netweaver BW, or for information about how to configure SAP Netweaver BW objects and processes, see your SAP documentation.  
  
 **To open the Mappings page**  
  
1.  In [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], open the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] package that contains the SAP BW destination.  
  
2.  On the **Data Flow** tab, double-click the SAP BW destination.  
  
3.  In the **SAP BW Destination Editor**, click **Mappings** to open the **Mappings** page of the editor.  
  
## Options  
  
> [!NOTE]  
>  If you do not know all the values that are required to configure the destination, you might have to ask your SAP administrator.  
  
 The **Mappings** page of the **SAP BW Destination Editor consists** of two sections:  
  
-   The upper section shows the available input and destination columns and lets you create mappings between these two types of columns.  
  
-   The lower section is a table that lists which input columns have been mapped to which output columns.  
  
### Upper Section Options  
 The upper section has the following options:  
  
 **Available Input Columns**  
 View the list of available input columns.  
  
 To map an input column to a destination column, drag a column from the **Available Input Columns** list and drop that column onto a column in the **Available Destination Columns** list.  
  
 **Available Destination Columns**  
 View the list of available destination columns.  
  
 To map an input column to destination column, drag a column from the **Available Input Columns** list and drop that column onto a column in the **Available Destination Columns** list.  
  
 The upper section also has a context menu that you can open by right-clicking on the background. This context menu contains the following options:  
  
-   **Select All Mappings**  
  
-   **Delete Selected Mappings**  
  
-   **Map Items by Matching Names**  
  
### Lower Section Columns  
 The lower section is a table of the mappings, and has the following columns:  
  
 **Input Column**  
 View the input columns that you have selected.  
  
 To map a different input column to the same destination column, select a different input column in the list. To remove a mapping, select **\<ignore>** to exclude the input column from the output.  
  
 **Destination Column**  
 View each available destination column, regardless of whether that column is mapped or not.  
  
## See Also  
 [SAP BW Destination Editor &#40;Connection Manager Page&#41;](../../integration-services/data-flow/sap-bw-destination-editor-connection-manager-page.md)   
 [SAP BW Destination Editor &#40;Error Output Page&#41;](../../integration-services/data-flow/sap-bw-destination-editor-error-output-page.md)   
 [SAP BW Destination Editor &#40;Advanced Page&#41;](../../integration-services/data-flow/sap-bw-destination-editor-advanced-page.md)   
 [Microsoft Connector for SAP BW F1 Help](../../integration-services/microsoft-connector-for-sap-bw-f1-help.md)  
  
  
