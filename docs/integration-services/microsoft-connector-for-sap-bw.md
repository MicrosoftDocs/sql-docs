---
title: "Microsoft Connector for SAP BW | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
ms.assetid: 5281f080-53d5-4679-aa26-f4cd4ac7a2df
author: janinezhang
ms.author: janinez
manager: craigg
---
# Microsoft Connector for SAP BW
  The [!INCLUDE[msCoName](../includes/msconame-md.md)] Connector for SAP BW consists of a set of three components that let you extract data from, or load data into, an SAP Netweaver BW version 7 system.  
  
 The [!INCLUDE[msCoName](../includes/msconame-md.md)] Connector for SAP BW for SQL Server 2016 is a component of the SQL Server 2016 Feature Pack. To install the Connector for SAP BW and its documentation, download and run the installer from the [SQL Server 2016 Feature Pack web page](https://go.microsoft.com/fwlink/?LinkId=746297).  

> [!IMPORTANT]
> Microsoft does not anticipate providing an updated version of the Connector for SAP BW. Microsoft does not own the source code for the SAP BW components, which were developed by a third party, and as a result cannot update them. Consider purchasing the latest SAP connectivity components from a Microsoft ISV partner such as [Theobald Software](https://theobald-software.com/en/xtract-is-productinfo.html). Microsoft's ISV partners have adapted their SAP connectivity components for SSIS for installation in Azure.
 
> [!IMPORTANT]  
>  The documentation for the Microsoft Connector for SAP BW assumes familiarity with the SAP Netweaver BW environment. For more information about SAP Netweaver BW, or for information about how to configure SAP Netweaver BW objects and processes, see your SAP documentation.  
  
> [!IMPORTANT]  
>  Extracting data from SAP Netweaver BW requires additional SAP licensing. Check with SAP to verify these requirements.  
  
## Components  
 The [!INCLUDE[msCoName](../includes/msconame-md.md)] Connector for SAP BW has the following components:  
  
-   **SAP BW Source**-The SAP BW source is a data flow source component that lets you extract data from an SAP Netweaver BW version 7 system.  
  
-   **SAP BW Destination**-The SAP BW destination is a data flow destination component that lets you load data into an SAP Netweaver BW version 7 system.  
  
-   **SAP BW Connection Manager**-The SAP BW connection manager connects either an SAP BW source or SAP BW destination to an SAP Netweaver BW version 7 system.  
  
 For a walkthrough that demonstrates how to configure and use the SAP BW connection manager, source, and destination, see the white paper, [Using SQL Server Integration Services with SAP BI 7.0](https://go.microsoft.com/fwlink/?LinkId=301897). This white paper also shows how to configure the required objects in SAP BW.  
  
## Documentation  
 This Help file for the [!INCLUDE[msCoName](../includes/msconame-md.md)] Connector for SAP BW contains the following topics and sections:  
  
 [Installing the Microsoft Connector for SAP BW](../integration-services/installing-the-microsoft-connector-for-sap-bw.md)  
 Describes the installation requirements for the [!INCLUDE[msCoName](../includes/msconame-md.md)] Connector for SAP BW.  
  
 [Microsoft Connector for SAP BW Components](../integration-services/microsoft-connector-for-sap-bw-components.md)  
 Describes each component of the [!INCLUDE[msCoName](../includes/msconame-md.md)] Connector for SAP BW.  
  
 [Microsoft Connector for SAP BW F1 Help](../integration-services/microsoft-connector-for-sap-bw-f1-help.md)  
 Describes the user interface of each component of the [!INCLUDE[msCoName](../includes/msconame-md.md)] Connector for SAP BW.  
  
  
