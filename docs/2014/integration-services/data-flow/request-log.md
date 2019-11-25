---
title: "Request Log | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
ms.assetid: 165d3833-0493-490c-9f63-8a134a7fafb8
author: janinezhang
ms.author: janinez
manager: craigg
---
# Request Log
  Use the **Request Log** dialog box to view the events that are logged during the request that is made to the SAP Netweaver BW system for sample data. This information can be useful if you have to troubleshoot your configuration of the SAP BW source.  
  
 To learn more about the SAP BW source component of the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Connector 1.1 for SAP BW, see [SAP BW Source](sap-bw-source.md).  
  
> [!IMPORTANT]  
>  The documentation for the Microsoft Connector 1.1 for SAP BW assumes familiarity with the SAP Netweaver BW environment. For more information about SAP Netweaver BW, or for information about how to configure SAP Netweaver BW objects and processes, see your SAP documentation.  
  
> [!IMPORTANT]  
>  Extracting data from SAP Netweaver BW requires additional SAP licensing. Check with SAP to verify these requirements.  
  
 **To open the Request Log dialog box**  
  
1.  In [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], open the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] package that contains the SAP BW source.  
  
2.  On the **Data Flow** tab, double-click the SAP BW source.  
  
3.  In the **SAP BW Source Editor**, click **Connection Manager** to open the **Connection Manager** page of the editor.  
  
4.  After you configure the SAP BW source, click **Preview** to preview the events in the **Request Log** dialog box.  
  
    > [!NOTE]  
    >  Clicking **Preview** also opens the **Preview** dialog box. For more information about this dialog box, see [Preview](preview.md).  
  
## Options  
 **Time**  
 Displays the time that the event that was logged.  
  
 **Type**  
 Displays the type of the event that was logged. The following table lists the possible event types.  
  
|Value|Description|  
|-----------|-----------------|  
|S|Success message.|  
|E|Error message|  
|W|Warning message.|  
|I|Informational message.|  
|A|The operation was aborted.|  
  
 **Message**  
 Displays the message text that is associated with the logged event.  
  
## See Also  
 [SAP BW Source Editor &#40;Connection Manager Page&#41;](sap-bw-source-editor-connection-manager-page.md)   
 [Microsoft Connector 1.1 for SAP BW F1 Help](../microsoft-connector-for-sap-bw-f1-help.md)  
  
  
