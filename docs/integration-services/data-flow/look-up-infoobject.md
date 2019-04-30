---
title: "Look Up InfoObject | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
ms.assetid: e7f4c132-a5ec-49d8-a964-45775432731f
author: janinezhang
ms.author: janinez
manager: craigg
---
# Look Up InfoObject

[!INCLUDE[ssis-appliesto](../../includes/ssis-appliesto-ssvrpluslinux-asdb-asdw-xxx.md)]


  Use the **Look Up InfoObject** dialog box to look up an InfoObject that is defined in the SAP Netweaver BW system. When the list of available InfoObjects appears, select the InfoObject that you want, and the SAP BW destination will populate the associated options with the required values.  
  
 The SAP BW destination of the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Connector 1.1 for SAP BW uses the **Look Up InfoObject** dialog box. To learn more about the SAP BW destination, see [SAP BW Destination](../../integration-services/data-flow/sap-bw-destination.md).  
  
> [!IMPORTANT]  
>  The documentation for the Microsoft Connector 1.1 for SAP BW assumes familiarity with the SAP Netweaver BW environment. For more information about SAP Netweaver BW, or for information about how to configure SAP Netweaver BW objects and processes, see your SAP documentation.  
  
 **To open the Look Up InfoObject dialog box**  
  
1.  In [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], open the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] package that contains the SAP BW destination.  
  
2.  On the **Data Flow** tab, double-click the SAP BW destination.  
  
3.  In the **SAP BW Destination Editor**, click **Connection Manager** to open the **Connection Manager** page of the editor.  
  
4.  On the **Connection Manager** page, in the **Create SAP BW Objects** group box, select one of the following options:  
  
    1.  Select **InfoCube**. Then, click **Create**. In the **Create InfoCube for Transaction Data** dialog box, click **Search** in the **IObject** column for one of the rows in the list. Each row represents a column in the data flow of the package.  
  
    2.  Select **InfoSource**. Then click **Create**. In the **Create InfoSource** dialog box, select **Transaction data**. In the **Create InfoSource for Transaction Data** dialog box, click **Search** in the **IObject** column for one of the rows in the list. Each row represents a column in the data flow of the package.  
  
    3.  Select **InfoSource**. Then click **Create**. In the **Create InfoSource** dialog box, select **Master data**. In the **Create InfoSource for Master Data** dialog box, click **Look up**.  
  
 You can also open the **Look Up InfoObject** dialog box by clicking **Add** in the **Attributes** section of the **Create New InfoObject** dialog box.  
  
## Lookup Options  
 In the lookup field text boxes, you can filter results by using the asterisk wildcard character (*), or by using a partial string in combination with the asterisk wildcard character. However, if you leave a lookup field empty, the lookup process will only match empty strings in that field.  
  
 **Characteristics**  
 Look up InfoObjects that represent characteristics.  
  
 **Units**  
 Look up InfoObjects that represent units.  
  
 **Key figures**  
 Look up InfoObjects that represent key figures.  
  
 **Time characteristics**  
 Look up InfoObjects that represent time characteristics.  
  
 **Name**  
 Enter the name of the InfoObject that you want to look up, or a partial name with the asterisk wildcard character (*). Or, use the asterisk wildcard character alone to include all InfoObjects.  
  
 **Description**  
 Enter the description, or a partial description with the asterisk wildcard character (*). Or, use the asterisk wildcard character alone to include all InfoObjects regardless of description.  
  
 **Look up**  
 Look up matching InfoObjects that are defined in the SAP Netweaver BW system.  
  
## Lookup Results  
 After you click the Look up button, a list of the InfoObjects in the SAP Netweaver BW system appears in a table with the following column headings.  
  
 **InfoObject**  
 Displays the name of the InfoObject that is defined in the SAP Netweaver BW system.  
  
 **Text (short)**  
 Displays the short text that is associated with the InfoObject.  
  
 When the list of available InfoObjects appears, select the InfoObject that you want, and the destination will populate the associated options with the required values.  
  
## See Also  
 [Create InfoCube for Transaction Data](../../integration-services/data-flow/create-infocube-for-transaction-data.md)   
 [Create InfoSource](../../integration-services/data-flow/create-infosource.md)   
 [Create InfoSource for Transaction Data](../../integration-services/data-flow/create-infosource-for-transaction-data.md)   
 [Create InfoSource for Master Data](../../integration-services/data-flow/create-infosource-for-master-data.md)   
 [Create New InfoObject](../../integration-services/data-flow/create-new-infoobject.md)   
 [SAP BW Destination Editor &#40;Connection Manager Page&#41;](../../integration-services/data-flow/sap-bw-destination-editor-connection-manager-page.md)   
 [Microsoft Connector for SAP BW F1 Help](../../integration-services/microsoft-connector-for-sap-bw-f1-help.md)  
  
  
