---
title: "Preview | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
ms.assetid: 551494c4-9e27-4592-9200-c6bf19e80c9a
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# Preview
  Use the **Preview** dialog box to preview the data that the SAP BW source will extract.  
  
> [!IMPORTANT]  
>  The **Preview** option, that is available on the **Connection Manager** page of the **SAP BW Source Editor**, actually extracts data. If you have configured SAP Netweaver BW to extract only data that has changed since the previous extraction, selecting **Preview** will exclude the previewed data from the next extraction.  
  
 To learn more about the SAP BW source component of the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Connector 1.1 for SAP BW, see [SAP BW Source](../../integration-services/data-flow/sap-bw-source.md).  
  
> [!IMPORTANT]  
>  The documentation for the Microsoft Connector 1.1 for SAP BW assumes familiarity with the SAP Netweaver BW environment. For more information about SAP Netweaver BW, or for information about how to configure SAP Netweaver BW objects and processes, see your SAP documentation.  
  
> [!IMPORTANT]  
>  Extracting data from SAP Netweaver BW requires additional SAP licensing. Check with SAP to verify these requirements.  
  
 **To open the Preview dialog box**  
  
1.  In [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], open the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] package that contains the SAP BW source.  
  
2.  On the **Data Flow** tab, double-click the SAP BW source.  
  
3.  In the **SAP BW Source Editor**, click **Connection Manager** to open the **Connection Manager** page of the editor.  
  
4.  Configure the SAP BW source.  
  
5.  After you configure the SAP BW source, on the **Connection Manager** page, click **Preview** to preview the data in the **Preview** dialog box.  
  
    > [!NOTE]  
    >  Clicking **Preview** also opens the **Request Log** dialog box. For more information about this dialog box, see [Request Log](../../integration-services/data-flow/request-log.md).  
  
## Options  
 The **Preview** dialog box displays the rows that are requested from the SAP Netweaver BW system. The columns that are displayed are the columns that are defined in the source data.  
  
 There are no other options in this dialog box.  
  
## See Also  
 [SAP BW Source Editor &#40;Connection Manager Page&#41;](../../integration-services/data-flow/sap-bw-source-editor-connection-manager-page.md)   
 [Microsoft Connector for SAP BW F1 Help](../../integration-services/microsoft-connector-for-sap-bw-f1-help.md)  
  
  
