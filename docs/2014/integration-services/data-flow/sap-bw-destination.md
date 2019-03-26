---
title: "SAP BW Destination | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
ms.assetid: a612ed91-b89b-4173-a0b1-0bce381e1e28
author: janinezhang
ms.author: janinez
manager: craigg
---
# SAP BW Destination
  The SAP BW destination is the destination component of the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Connector 1.1 for SAP BW. Thus, the SAP BW destination loads data from the data flow in an [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] package into an SAP Netweaver BW version 7 system.  
  
 This destination has one input and one error output.  
  
> [!IMPORTANT]  
>  The documentation for the Microsoft Connector 1.1 for SAP BW assumes familiarity with the SAP Netweaver BW environment. For more information about SAP Netweaver BW, or for information about how to configure SAP Netweaver BW objects and processes, see your SAP documentation.  
  
 To use the SAP BW destination, you have to do the following tasks:  
  
-   [Prepare the SAP Netweaver BW objects](#bkmk_Prepare_Objects)  
  
-   [Connect to the SAP Netweaver BW system](#bkmk_Connect_Database)  
  
-   [Configure the SAP BW destination](#bkmk_Configure_Destination)  
  
##  <a name="bkmk_Prepare_Objects"></a> Preparing the SAP Netweaver BW Objects That the Destination Requires  
 The SAP BW destination requires that certain objects are in the SAP Netweaver BW system before the destination can function. If these objects do not already exist, you have to follow these steps to create and configure these objects in the SAP Netweaver BW system.  
  
> [!NOTE]  
>  For additional details about these objects and these configuration steps, see the SAP Netweaver BW documentation.  
  
1.  Create a new Source System:  
  
    1.  Select the type, **"Third Party/Staging BAPIs"**.  
  
    2.  For **Communication Type with Target System**, select **Non-Unicode (Inactive MDMP Settings)**.  
  
    3.  Assign an appropriate Program ID.  
  
2.  Assign the Source System to an InfoSource.  
  
3.  Create an InfoPackage.  
  
     The InfoPackage is the most important object that is required by the SAP BW destination.  
  
 You can also create additional InfoObjects, InfoCubes, InfoSources, and InfoPackages that are required to support loading data into the SAP Netweaver BW system.  
  
##  <a name="bkmk_Connect_Database"></a> Connecting to the SAP Netweaver BW System  
 To connect to the SAP Netweaver BW version 7 system, the SAP BW destination uses the SAP BW connection manager that is part of the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Connector 1.1 for SAP BW package. The SAP BW connection manager is the only [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] connection manager that the SAP BW destination can use.  
  
 For more information about the SAP BW connection manager, see [SAP BW Connection Manager](../connection-manager/sap-bw-connection-manager.md).  
  
##  <a name="bkmk_Configure_Destination"></a> Configuring the SAP BW Destination  
 You can configure the SAP BW destination in the following ways:  
  
-   Look up and select the InfoPackage to use to load data.  
  
-   Map each column in the data flow to the appropriate InfoObject in the InfoPackage.  
  
-   Specify how many rows of data will be transferred at a time by configuring the `PackageSize` property.  
  
-   Choose to wait until the loading of data is completed by the SAP Netweaver BW system.  
  
-   Choose not to trigger the InfoPackage, but to wait for notification that the SAP Netweaver BW system has started the loading of data.  
  
-   Optionally, trigger another process chain after the loading of data is completed.  
  
-   Optionally, create the SAP Netweaver BW objects that are required by the destination. This includes creating InfoObjects, InfoCubes, InfoSources, and InfoPackages.  
  
-   Test the loading of data with the options that you have selected.  
  
 You can also enable logging of RFC function calls by the destination. (This logging is separate from the optional logging that you can enable on [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] packages.) You enable logging of RFC function calls when you configure the SAP BW connection manager that the destination will use. For more information about how to configure the SAP BW connection manager, see [SAP BW Connection Manager](../connection-manager/sap-bw-connection-manager.md).  
  
 If you do not know all the values that are required to configure the destination, you might have to ask your SAP administrator.  
  
 For a walkthrough that demonstrates how to configure and use the SAP BW connection manager, source, and destination, see the white paper, [Using SQL Server 2008 Integration Services with SAP BI 7.0](https://go.microsoft.com/fwlink/?LinkID=137090). This white paper also shows how to configure the required objects in SAP BW.  
  
### Using the SSIS Designer to Configure the Destination  
 For more information about the properties of the SAP BW destination that you can set in [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer, click one of the following topics:  
  
-   [SAP BW Destination Editor &#40;Connection Manager Page&#41;](sap-bw-destination-editor-connection-manager-page.md)  
  
-   [SAP BW Destination Editor &#40;Mappings Page&#41;](sap-bw-destination-editor-mappings-page.md)  
  
-   [SAP BW Destination Editor &#40;Error Output Page&#41;](sap-bw-destination-editor-error-output-page.md)  
  
-   [SAP BW Destination Editor &#40;Advanced Page&#41;](sap-bw-destination-editor-advanced-page.md)  
  
 While you are configuring the SAP BW destination, you can also use various dialog boxes to look up or to create SAP Netweaver BW objects. For more information about these dialog boxes, click one of the following topics:  
  
-   [Look Up InfoPackage](look-up-infopackage.md)  
  
-   [Create New InfoObject](create-new-infoobject.md)  
  
-   [Create InfoCube for Transaction Data](create-infocube-for-transaction-data.md)  
  
-   [Look Up InfoObject](look-up-infoobject.md)  
  
-   [Create InfoSource](create-infosource.md)  
  
-   [Create InfoSource for Transaction Data](create-infosource-for-transaction-data.md)  
  
-   [Create InfoSource for Master Data](create-infosource-for-master-data.md)  
  
-   [Create InfoPackage](create-infopackage.md)  
  
## See Also  
 [Microsoft Connector 1.1 for SAP BW Components](../microsoft-connector-for-sap-bw-components.md)  
  
  
