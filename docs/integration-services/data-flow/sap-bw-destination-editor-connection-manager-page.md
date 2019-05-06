---
title: "SAP BW Destination Editor (Connection Manager Page) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
f1_keywords: 
  - "sql13.dts.designer.sapbwdestination.connection.f1"
ms.assetid: 04ae38f8-5287-45a3-826a-8aac5dd15a91
author: janinezhang
ms.author: janinez
manager: craigg
---
# SAP BW Destination Editor (Connection Manager Page)
  Use the **Connection Manager** page of the **SAP BW Destination Editor** to select the SAP BW connection manager that the SAP BW destination will use. On this page, you also select the parameters for loading the data into the SAP Netweaver BW system.  
  
 To learn more about the SAP BW destination of the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Connector 1.1 for SAP BW, see [SAP BW Destination](../../integration-services/data-flow/sap-bw-destination.md).  
  
> [!IMPORTANT]  
>  The documentation for the Microsoft Connector 1.1 for SAP BW assumes familiarity with the SAP Netweaver BW environment. For more information about SAP Netweaver BW, or for information about how to configure SAP Netweaver BW objects and processes, see your SAP documentation.  
  
 **To open the Connection Manager page**  
  
1.  In [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], open the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] package that contains the SAP BW destination.  
  
2.  On the **Data Flow** tab, double-click the SAP BW destination.  
  
3.  In the **SAP BW Destination Editor**, click **Connection Manager** to open the **Connection Manager** page of the editor.  
  
## Options  
  
> [!NOTE]  
>  If you do not know all the values that are required to configure the destination, you might have to ask your SAP administrator.  
  
 **SAP BW connection manager**  
 Select an existing connection manager from the list, or create a new connection by clicking **New**.  
  
 **New**  
 Create a new connection manager by using the **SAP BW Connection Manager** dialog box.  
  
 **Test Load**  
 Perform a test of the loading process that uses the settings that you have selected and loads zero rows.  
  
### InfoPackage/InfoSource Options  
 You do not have to know and enter these values in advance. Use the **Look up** button to find and select the appropriate InfoPackage. After you select an InfoPackage, the component enters the appropriate values for these options.  
  
 **InfoPackage**  
 Enter the name of the InfoPackage.  
  
 **InfoSource**  
 Enter the name of the InfoSource.  
  
 **Type**  
 Enter the single-character that identifies the type of the InfoSource. The following table lists the acceptable single-character values.  
  
|Value|Description|  
|-----------|-----------------|  
|**D**|Transaction data|  
|**M**|Master data|  
|**T**|Texts|  
|**H**|Hierarchy data|  
  
 **Logical system**  
 Enter the name of the logical system that is associated with the InfoPackage.  
  
 **Look up**  
 Look up the InfoPackage by using the **Look Up InfoPackage** dialog box. For more information about this dialog box, see [Look Up InfoPackage](../../integration-services/data-flow/look-up-infopackage.md).  
  
### RFC Destination Options  
 You do not have to know and enter these values in advance. Use the **Look up** button to find and select the appropriate RFC destination. After you select an RFC destination, the component enters the appropriate values for these options.  
  
 **Gateway host**  
 Enter the server name or IP address of the gateway host. Usually, the name or IP address is the same as the SAP application server.  
  
 **Gateway service**  
 Enter the name of the gateway service, in the format **sapgwNN**, where **NN** is the system number.  
  
 **Program ID**  
 Enter the Program ID that is associated with the RFC destination.  
  
 **Look up**  
 Look up the RFC destination by using the **Look Up RFC Destination** dialog box. For more information about this dialog box, see [Look Up RFC Destination](../../integration-services/data-flow/look-up-rfc-destination.md).  
  
### Create SAP BW Objects Options  
 **Select object type**  
 Select the type of SAP Netweaver BW object that you want to create. You can select one of the following types:  
  
-   InfoObject  
  
-   InfoCube  
  
-   InfoSource  
  
-   InfoPackage  
  
 **Create**  
 Create the selected type of SAP Netweaver BW object.  
  
|Object Type|Result|  
|-----------------|------------|  
|**InfoObject**|Create a new InfoObject by using the **Create New InfoObject** dialog box. For more information about this dialog box, see [Create New InfoObject](../../integration-services/data-flow/create-new-infoobject.md).|  
|**InfoCube**|Create a new InfoCube by using the **Create InfoCube for Transaction Data** dialog box. For more information about this dialog box, see [Create InfoCube for Transaction Data](../../integration-services/data-flow/create-infocube-for-transaction-data.md).|  
|**InfoSource**|Create a new InfoSource by using the **Create InfoSource** dialog box, and then by using the **Create InfoSource for Transaction Data** or the **Create InfoSource for Master Data** dialog box. For more information about these dialog boxes, see [Create InfoSource](../../integration-services/data-flow/create-infosource.md), [Create InfoSource for Transaction Data](../../integration-services/data-flow/create-infosource-for-transaction-data.md) and [Create InfoSource for Master Data](../../integration-services/data-flow/create-infosource-for-master-data.md).|  
|**InfoPackage**|Create a new InfoPackage by using the **Create InfoPackage** dialog box. For more information about this dialog box, see [Create InfoPackage](../../integration-services/data-flow/create-infopackage.md).|  
  
## See Also  
 [SAP BW Destination Editor &#40;Mappings Page&#41;](../../integration-services/data-flow/sap-bw-destination-editor-mappings-page.md)   
 [SAP BW Destination Editor &#40;Error Output Page&#41;](../../integration-services/data-flow/sap-bw-destination-editor-error-output-page.md)   
 [SAP BW Destination Editor &#40;Advanced Page&#41;](../../integration-services/data-flow/sap-bw-destination-editor-advanced-page.md)   
 [Microsoft Connector for SAP BW F1 Help](../../integration-services/microsoft-connector-for-sap-bw-f1-help.md)  
  
  
