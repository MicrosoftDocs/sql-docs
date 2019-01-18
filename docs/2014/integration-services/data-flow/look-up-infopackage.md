---
title: "Look Up InfoPackage | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
ms.assetid: 7c0cb7a4-cd07-44cc-85cb-eb1ad91f85fd
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# Look Up InfoPackage
  Use the **Look Up InfoPackage** dialog box to look up an InfoPackage that is defined in the SAP Netweaver BW system. When the list of InfoPackages appears, select the InfoPackage that you want, and the destination will populate the associated options with the required values.  
  
 The SAP BW destination of the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Connector 1.1 for SAP BW uses the **Look Up Process Chain** dialog box. To learn more about the SAP BW destination, see [SAP BW Destination](sap-bw-destination.md).  
  
> [!IMPORTANT]  
>  The documentation for the Microsoft Connector 1.1 for SAP BW assumes familiarity with the SAP Netweaver BW environment. For more information about SAP Netweaver BW, or for information about how to configure SAP Netweaver BW objects and processes, see your SAP documentation.  
  
 **To open the Look Up InfoPackage dialog box**  
  
1.  In [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], open the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] package that contains the SAP BW destination.  
  
2.  On the **Data Flow** tab, double-click the SAP BW destination.  
  
3.  In the **SAP BW Destination Editor**, click **Connection Manager** to open the **Connection Manager** page of the editor.  
  
4.  On the **Connection Manager** page, in the **InfoPackage/InfoSource** group box, click **Look up** to display the **Look Up InfoPackage** dialog box.  
  
## Lookup Options  
 In the lookup fields, you can filter results by using the asterisk wildcard character (*), or by using a partial string in combination with the asterisk wildcard character. However, if you leave a lookup field empty, the lookup operation will only match empty strings in that field.  
  
 **InfoPackage**  
 Enter the name of the InfoPackage that you want to look up, or a partial name with the asterisk wildcard character (*). Or, use the asterisk wildcard character alone to include all InfoPackages.  
  
 **InfoSource**  
 Enter the name of the InfoSource, or a partial name with the asterisk wildcard character (*). Or, use the asterisk wildcard character alone to include all InfoPackages regardless of InfoSource.  
  
 **Source system**  
 Enter the name of the source system, or a partial name with the asterisk wildcard character (*). Or, use the asterisk wildcard character alone to include all InfoPackages regardless of source systems.  
  
 **Look up**  
 Look up matching InfoPackages that are defined in the SAP Netweaver BW system.  
  
## Lookup Results  
 After you click the Look up button, a list of the InfoPackages in the SAP Netweaver BW system appears in a table with the following column headings.  
  
 **InfoPackage**  
 Displays the name of the InfoPackage that is defined in the SAP Netweaver BW system.  
  
 **Type**  
 Displays the type of the InfoPackage. The following table lists the possible values for the type.  
  
|Value|Description|  
|-----------|-----------------|  
|Trans.|Transaction data.|  
|Attr.|Attribute data.|  
|Texts|Texts.|  
  
 **Description**  
 Displays the description of the InfoPackage.  
  
 **InfoSource**  
 Displays the name of the InfoSource, if any, that is associated with the InfoPackage.  
  
 **Source System**  
 Displays the name of the source system.  
  
 When the list of InfoPackages appears, select the InfoPackage that you want, and the destination will populate the associated options with the required values.  
  
## See Also  
 [SAP BW Destination Editor &#40;Connection Manager Page&#41;](sap-bw-destination-editor-connection-manager-page.md)   
 [Microsoft Connector 1.1 for SAP BW F1 Help](../microsoft-connector-for-sap-bw-f1-help.md)  
  
  
