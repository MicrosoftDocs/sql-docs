---
title: "SAP BW Source Editor (Columns Page) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
f1_keywords: 
  - "sql13.dts.designer.sapbwsource.columns.f1"
ms.assetid: c2ec8bb7-be9b-4783-ad88-32512de784b0
author: janinezhang
ms.author: janinez
manager: craigg
---
# SAP BW Source Editor (Columns Page)
  Use the **Columns** page of the **SAP BW Source Editor** to map an output column to each external (source) column.  
  
 To learn more about the SAP BW source component of the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Connector 1.1 for SAP BW, see [SAP BW Source](../../integration-services/data-flow/sap-bw-source.md).  
  
> [!IMPORTANT]  
>  The documentation for the Microsoft Connector 1.1 for SAP BW assumes familiarity with the SAP Netweaver BW environment. For more information about SAP Netweaver BW, or for information about how to configure SAP Netweaver BW objects and processes, see your SAP documentation.  
  
> [!IMPORTANT]  
>  Extracting data from SAP Netweaver BW requires additional SAP licensing. Check with SAP to verify these requirements.  
  
 **To open the Columns page**  
  
1.  In [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], open the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] package that contains the SAP BW source.  
  
2.  On the **Data Flow** tab, double-click the SAP BW source.  
  
3.  In the **SAP BW Source Editor**, click **Columns** to open the **Columns** page of the editor.  
  
## Options  
  
> [!NOTE]  
>  If you do not know all the values that are required to configure the source, you might have to ask your SAP administrator.  
  
 **Available External Columns**  
 View the list of available external columns in the data source, and then select the columns to be included in the data flow.  
  
 To include a column in the data flow, select the check box that corresponds to that column. The order in which you select the check boxes determines the order in which columns will be output. That is, the first check box that you select will be the first output column, the second check box will be the second output columns, and so on.  
  
 **External Column**  
 View the selected external (source) columns. The selected columns appear in the order in which you will see their corresponding output columns when you configure downstream components that consume data from this source.  
  
 To change the order of the columns, in the **Available External Columns** list, clear the check boxes for all columns. Then, select the columns in the order that you want them to appear.  
  
 **Output Column**  
 Provide a unique name for each output column. The default is the name of the selected external (source) column. However, you can enter any unique, descriptive name. [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer will display the **Output Column** names for the columns when you configure downstream components that consume data from this source.  
  
## See Also  
 [SAP BW Source Editor &#40;Connection Manager Page&#41;](../../integration-services/data-flow/sap-bw-source-editor-connection-manager-page.md)   
 [SAP BW Source Editor &#40;Error Output Page&#41;](../../integration-services/data-flow/sap-bw-source-editor-error-output-page.md)   
 [SAP BW Source Editor &#40;Advanced Page&#41;](../../integration-services/data-flow/sap-bw-source-editor-advanced-page.md)   
 [Microsoft Connector for SAP BW F1 Help](../../integration-services/microsoft-connector-for-sap-bw-f1-help.md)  
  
  
