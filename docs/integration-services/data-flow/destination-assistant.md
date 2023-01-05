---
description: "Destination Assistant"
title: "Destination Assistant | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: integration-services
ms.topic: conceptual
f1_keywords: 
  - "sql13.dts.designer.destinationassistant.f1"
  - "sql13.DTS.DESIGNER.DESTINATIONASSIST.F1"
ms.assetid: 10a40921-a2c2-4ac8-be28-311f8500fbf6
author: chugugrace
ms.author: chugu
---
# Destination Assistant

[!INCLUDE[sqlserver-ssis](../../includes/applies-to-version/sqlserver-ssis.md)]


  The Destination Assistant component helps create a destination component and connection manager. The component is located in the **Favorites** section of the SSIS Toolbox.  
  
> [!NOTE]  
>  Destination Assistant replaces the Integration Services Connections project and the corresponding wizard.  

## Add a destination with Destination Assistant
This topic provides steps to add a new destination using the Destination Assistant and also lists the options that are available on the **Add New Destination** dialog, which you will see when you drag-drop the Destination Assistant to the SSIS Designer.  

1.  In [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], open the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] package that you want to add a destination component to.  
  
2.  Drag the **Destination Assistant** component from the SSIS Toolbox to the **Data Flow** tab. You should see the **Add New Destination** dialog box. The next section provides you details about the options available in the dialog box.  
  
3.  Select the type of the destination in the **Types** list.  
  
4.  Select an existing connection manager in the **Connection Managers** list or select **\<New>** to create a new connection manager.  
  
5.  If you select an existing connection manager, click **OK** to close the **Add New Destination** dialog box. You should see the destination and connection managers added to the data flow.  
  
6.  If you click **\<New>** to create a new connection manager, you should see a **Connection Manager** dialog box, which lets you specify parameters for the connection. After you are done with creating the new connection manager, you will see the destination and the connection manager in SSIS Designer. 
  
## Add New Destination dialog box
The following table lists the options available on the **Add New Destination** dialog box.  
  
|Option|Description|  
|------------|-----------------|  
|Types|Select the type of destination you want to connect to.|  
|Connection Managers|Select an existing connection manager or click **\<New>** to create a new connection manager.|  
|Show installed only|Specify whether to view only installed destinations.|  
|OK|Click to save your changes and open any subsequent dialog boxes to configure additional options.|  
