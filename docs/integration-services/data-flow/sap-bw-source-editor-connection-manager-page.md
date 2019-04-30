---
title: "SAP BW Source Editor (Connection Manager Page) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
f1_keywords: 
  - "sql13.dts.designer.sapbwsource.connection.f1"
ms.assetid: 2a6dc531-85ca-43c5-a65f-3ad3f7d537c4
author: janinezhang
ms.author: janinez
manager: craigg
---
# SAP BW Source Editor (Connection Manager Page)

[!INCLUDE[ssis-appliesto](../../includes/ssis-appliesto-ssvrpluslinux-asdb-asdw-xxx.md)]


  Use the **Connection Manager** page of the **SAP BW Source Editor** to select the SAP BW connection manager for the SAP BW source. On this page, you also select the execution mode and the parameters for extracting the data from the SAP Netweaver BW system.  
  
 To learn more about the SAP BW source component of the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Connector 1.1 for SAP BW, see [SAP BW Source](../../integration-services/data-flow/sap-bw-source.md).  
  
> [!IMPORTANT]  
>  The documentation for the Microsoft Connector 1.1 for SAP BW assumes familiarity with the SAP Netweaver BW environment. For more information about SAP Netweaver BW, or for information about how to configure SAP Netweaver BW objects and processes, see your SAP documentation.  
  
> [!IMPORTANT]  
>  Extracting data from SAP Netweaver BW requires additional SAP licensing. Check with SAP to verify these requirements.  
  
 **To open the Connection Manager page**  
  
1.  In [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], open the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] package that contains the SAP BW source.  
  
2.  On the **Data Flow** tab, double-click the SAP BW source.  
  
3.  In the **SAP BW Source Editor**, click **Connection Manager** to open the **Connection Manager** page of the editor.  
  
## Static Options  
  
> [!NOTE]  
>  If you do not know all the values that are required to configure the source, you might have to ask your SAP administrator.  
  
 **SAP BW connection manager**  
 Select an existing connection manager from the list, or create a new connection by clicking **New**.  
  
 **New**  
 Create a new connection manager by using the **SAP BW Connection Manager** dialog box.  
  
 For more information about this dialog box, see [SAP BW Connection Manager Editor](../../integration-services/connection-manager/sap-bw-connection-manager-editor.md).  
  
 **OHS destination**  
 Select the Open Hub Service (OHS) destination to use to extract data from the source.  
  
 **Execution mode**  
 Specify the method for extracting data from the source.  
  
|Option|Description|  
|------------|-----------------|  
|**P - Trigger Process Chain**|Trigger a process chain. In this case, the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] package starts the extraction process.|  
|**W - Wait for Notify**|Wait for notification from the SAP Netweaver BW system to begin extracting the data. In this case, the SAP Netweaver BW system starts the extraction process.|  
|**E - Extract Only**|Retrieve the data that is associated with a particular Request ID. In this case, the SAP Netweaver BW system has already extracted the data into an internal table, and the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] package just reads the data.|  
  
 **Preview**  
 Open the **Preview** dialog box in which you can preview results. For more information, see [Preview](../../integration-services/data-flow/preview.md).  
  
> [!IMPORTANT]  
>  The **Preview** option, that is available on the **Connection Manager** page of the SAP BW Source Editor, actually extracts data. If you have configured SAP Netweaver BW to extract only data that has changed since the previous extraction, selecting **Preview** will exclude the previewed data from the next extraction.  
  
 When you click **Preview**, you also open the **Request Log** dialog box. You can use this dialog box to view the events that are logged during the request that is made to the SAP Netweaver BW system for sample data. For more information, see [Request Log](../../integration-services/data-flow/request-log.md).  
  
## Execution Mode Dynamic Options  
  
> [!NOTE]  
>  If you do not know all the values that are required to configure the source, you might have to ask your SAP administrator.  
  
### Execution Mode = P - Trigger Process Chain  
  
#### RFC Destination Options  
 You do not have to know and enter these values in advance. Use the **Look up** button to find and select the appropriate RFC destination. After you select an RFC destination, the component enters the appropriate values for these options.  
  
 **Gateway host**  
 Enter the server name or IP address of the gateway host. Usually, the name or IP address is the same as the SAP application server.  
  
 **Gateway service**  
 Enter the name of the gateway service, in the format **sapgwNN**, where **NN** is the system number.  
  
 **Program ID**  
 Enter the Program ID that is associated with the RFC destination.  
  
 **Look up**  
 Look up the RFC destination by using the **Look Up RFC Destination** dialog box. For more information about this dialog box, see [Look Up RFC Destination](../../integration-services/data-flow/look-up-rfc-destination.md).  
  
#### Process Chain Options  
 You do not have to know and enter these values in advance. Use the **Look up** button to find and select the appropriate process chain. After you select a process chain, the component enters the appropriate value for the option.  
  
 **Process chain**  
 Enter the name of the process chain to be triggered by the source.  
  
 **Look up**  
 Look up the process chain by using the **Look Up Process Chain** dialog box. For more information about this dialog box, see [Look Up Process Chain](../../integration-services/data-flow/look-up-process-chain.md).  
  
### Execution Mode = W - Wait for Notify  
  
#### RFC Destination Options  
 You do not have to know and enter these values in advance. Use the **Look up** button to find and select the appropriate RFC destination. After you select an RFC destination, the component enters the appropriate values for the options.  
  
 **Gateway host**  
 Enter the server name or IP address of the gateway host. Usually, the name or IP address is the same as the SAP application server.  
  
 **Gateway service**  
 Enter the name of the gateway service, in the format **sapgwNN**, where **NN** is the system number.  
  
 **Program ID**  
 Enter the Program ID that is associated with the RFC destination.  
  
 **Look up**  
 Look up the RFC destination by using the **Look Up RFC Destination** dialog box. For more information about this dialog box, see [Look Up RFC Destination](../../integration-services/data-flow/look-up-rfc-destination.md).  
  
### Execution Mode = E - Extract Only  
 **Request ID**  
 Enter the Request ID that is associated with the extraction.  
  
## See Also  
 [SAP BW Source Editor &#40;Columns Page&#41;](../../integration-services/data-flow/sap-bw-source-editor-columns-page.md)   
 [SAP BW Source Editor &#40;Error Output Page&#41;](../../integration-services/data-flow/sap-bw-source-editor-error-output-page.md)   
 [SAP BW Source Editor &#40;Advanced Page&#41;](../../integration-services/data-flow/sap-bw-source-editor-advanced-page.md)   
 [Microsoft Connector for SAP BW F1 Help](../../integration-services/microsoft-connector-for-sap-bw-f1-help.md)  
  
  
