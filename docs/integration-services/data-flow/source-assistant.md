---
title: "Source Assistant | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
f1_keywords: 
  - "sql13.dts.designer.sourceassistant.f1"
  - "sql13.dts.designer.addNewSource.f1"
ms.assetid: 5ca9d821-7d61-4727-9133-5f9cb485c7f3
author: janinezhang
ms.author: janinez
manager: craigg
---
# Source Assistant
  The Source Assistant component helps create a source component and connection manager. The component is located in the **Favorites** section of the SSIS Toolbox.  
  
> [!NOTE]  
>  Source Assistant replaces the Integration Services Connections project and the corresponding wizard.  
  
## Add a source with Source Assistant
This section provides steps to add a new source using the Source Assistant and also lists the options that are available on the **Add New Source** dialog, which you will see when you drag-drop the Source Assistant to the SSIS Designer.  

1.  In [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], open the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] package that you want to add a source component to.  
  
2.  Drag the **Source Assistant** component from the SSIS Toolbox to the **Data Flow** tab. You should see the **Add New Source** dialog box. The next section provides you details about the options available in the dialog box.  
  
3.  Select the type of the destination in the **Types** list.  
  
4.  Select an existing connection manager in the **Connection Managers** list or select **\<New>** to create a new connection manager.  
  
5.  If you select an existing connection manager, click **OK** to close the **Add New Destination** dialog box. You should see the destination and connection managers added to the data flow.  
  
6.  If you click **\<New>** to create a new connection manager, you should see a **Connection Manager** dialog box, which lets you specify parameters for the connection. After you are done with creating the new connection manager, you will see the destination and the connection manager in SSIS Designer.  

## Add New Source dialog box
The following table lists options available in the **Add New Source** dialog box.  
  
|Option|Description|  
|------------|-----------------|  
|Types|Select the type of source you want to connect to.|  
|Connection Managers|Select an existing connection manager or click **\<New>** to create a new connection manager.|  
|Show installed only|Specify whether to view only installed sources.|  
|OK|Click to save your changes and open any subsequent dialog boxes to configure additional options.| 
  
