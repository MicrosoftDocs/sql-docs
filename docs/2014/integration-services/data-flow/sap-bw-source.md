---
title: "SAP BW Source | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
ms.assetid: 749afb64-3567-4dc9-8431-783d650c25db
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# SAP BW Source
  The SAP BW source is the source component of the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Connector 1.1 for SAP BW. Thus, the SAP BW source extracts data from an SAP Netweaver BW version 7 system and makes this data available to the data flow in a [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] package.  
  
 This source has one output and one error output.  
  
> [!IMPORTANT]  
>  The documentation for the Microsoft Connector 1.1 for SAP BW assumes familiarity with the SAP Netweaver BW environment. For more information about SAP Netweaver BW, or for information about how to configure SAP Netweaver BW objects and processes, see your SAP documentation.  
  
> [!IMPORTANT]  
>  Extracting data from SAP Netweaver BW requires additional SAP licensing. Check with SAP to verify these requirements.  
  
 To use the SAP BW source, you have to do the following tasks:  
  
-   [Prepare the SAP Netweaver BW objects](#bkmk_Prepare_Objects)  
  
-   [Connect to the SAP Netweaver BW system](#bkmk_Connect_Database)  
  
-   [Configure the SAP BW source](#bkmk_Configure_Source)  
  
##  <a name="bkmk_Prepare_Objects"></a> Preparing the SAP Netweaver BW Objects That the Source Requires  
 The SAP BW source requires that certain objects are in the SAP Netweaver BW system before the source can function. If these objects do not already exist, you have to follow these steps to create and configure these objects in the SAP Netweaver BW system.  
  
> [!NOTE]  
>  For additional details about these objects and these configuration steps, see the SAP Netweaver BW documentation.  
  
1.  Log on to SAP Netweaver BW through the SAP GUI, enter transaction code SM59, and create an RFC destination:  
  
    1.  For **Connection Type**, select **TCP/IP**.  
  
    2.  For **Activation Type**, select **Registered Server Program**.  
  
    3.  For **Communication Type with Target System**, select **Non-Unicode (Inactive MDMP Settings)**.  
  
    4.  Assign an appropriate Program ID.  
  
2.  Create an Open Hub Destination:  
  
    1.  Go to the Administrator Workbench (transaction code RSA1) and, in the left pane, select **Open Hub Destination**.  
  
    2.  In the middle pane, right-click an InfoArea, and then select **"Create Open Hub Destination"**.  
  
    3.  For **Destination Type**, select **"Third Party Tool"**, and then enter the previously created RFC Destination.  
  
    4.  Save and activate the new Open Hub Destination.  
  
3.  Create a Data Transfer Process (DTP):  
  
    1.  In the middle pane of the InfoArea, right-click the previously created destination, and then select **"Create data transfer process"**.  
  
    2.  Configure, save, and activate the DTP.  
  
    3.  On the menu, click **Goto**, and then click **Settings for Batch Manager**.  
  
    4.  Update **Number of processes** to 1 for serial processing.  
  
4.  Create a process chain:  
  
    1.  When configuring the process chain, select the **Start Using Metadata Chain or API** as the **Scheduling Options** of the **Start Process**, and then add the previously created DTP as the subsequent node.  
  
    2.  Save and activate the process chain.  
  
     The SAP BW source can call the process chain to activate the data transfer process.  
  
##  <a name="bkmk_Connect_Database"></a> Connecting to the SAP Netweaver BW System  
 To connect to the SAP Netweaver BW version 7 system, the SAP BW source uses the SAP BW connection manager that is part of the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Connector 1.1 for SAP BW package. The SAP BW connection manager is the only [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] connection manager that the SAP BW source can use.  
  
 For more information about the SAP BW connection manager, see [SAP BW Connection Manager](../connection-manager/sap-bw-connection-manager.md).  
  
##  <a name="bkmk_Configure_Source"></a> Configuring the SAP BW Source  
 You can configure the SAP BW source in the following ways:  
  
-   Look up and select the Open Hub Service (OHS) destination to use to extract data.  
  
-   Select one of the following methods for extracting data:  
  
    -   Trigger a process chain. In this case, the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] package starts the extraction process.  
  
    -   Wait for notification from the SAP Netweaver BW system to begin an extraction. In this case, the SAP Netweaver BW system starts the extraction process.  
  
    -   Retrieve the data that is associated with a particular Request ID. In this case, the SAP Netweaver BW system has already extracted the data to an internal table, and the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] package just reads the data.  
  
-   Depending on the method selected for extracting data, provide the following additional information:  
  
    -   For the **P - Trigger Process Chain** option, provide the gateway host name, gateway service name, program ID for the RFC destination, and name of the process chain.  
  
    -   For the **W - Wait for Notify** option, provide the gateway host name, the gateway server name, and the program ID for the RFC destination. You can also specify the timeout (in seconds). The timeout is the maximum period of time that the source will wait to be notified.  
  
    -   For the **E - Extract Only** option, provide the Request ID.  
  
-   Specify rules for string conversion. (For example, convert all strings depending on whether the SAP Netweaver BW system is Unicode or not, or convert all strings to `varchar` or `nvarchar`).  
  
-   Use the options that you have selected to preview the data to be extracted.  
  
 You can also enable logging of RFC function calls by the source. (This logging is separate from the optional logging that you can enable on [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] packages.) You enable logging of RFC function calls when you configure the SAP BW connection manager that the source will use. For more information about how to configure the SAP BW connection manager, see [SAP BW Connection Manager](../connection-manager/sap-bw-connection-manager.md).  
  
 If you do not know all the values that are required to configure the source, you might have to ask your SAP administrator.  
  
 For a walkthrough that demonstrates how to configure and use the SAP BW connection manager, source, and destination, see the white paper, [Using SQL Server 2008 Integration Services with SAP BI 7.0](https://go.microsoft.com/fwlink/?LinkID=137090). This white paper also shows how to configure the required objects in SAP BW.  
  
### Using the SSIS Designer to Configure the Source  
 For more information about the properties of the SAP BW source that you can set in [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer, click one of the following topics:  
  
-   [SAP BW Source Editor &#40;Connection Manager Page&#41;](sap-bw-source-editor-connection-manager-page.md)  
  
-   [SAP BW Source Editor &#40;Columns Page&#41;](sap-bw-source-editor-columns-page.md)  
  
-   [SAP BW Source Editor &#40;Error Output Page&#41;](sap-bw-source-editor-error-output-page.md)  
  
-   [SAP BW Source Editor &#40;Advanced Page&#41;](sap-bw-source-editor-advanced-page.md)  
  
 While you are configuring the SAP BW source, you can also use various dialog boxes to look up SAP Netweaver BW objects or to preview the source data. For more information about these dialog boxes, click one of the following topics:  
  
-   [Look Up RFC Destination](look-up-rfc-destination.md)  
  
-   [Look Up Process Chain](look-up-process-chain.md)  
  
-   [Request Log](request-log.md)  
  
-   [Preview](preview.md)  
  
## See Also  
 [Microsoft Connector 1.1 for SAP BW Components](../microsoft-connector-for-sap-bw-components.md)  
  
  
