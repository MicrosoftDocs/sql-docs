---
title: "Add a Source using Source Assistant | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "integration-services"
ms.topic: conceptual
ms.assetid: 5e850b7c-4b89-42ad-b0a6-d63ac7cc9568
author: janinezhang
ms.author: janinez
manager: craigg
---
# Add a Source using Source Assistant
  This topic provides steps to add a new source using the Source Assistant and also lists the options that are available on the **Add New Source** dialog, which you will see when you drag-drop the Source Assistant to the SSIS Designer.  
  
### To use Source Assistant to add a source  
  
1.  In [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)], open the [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] package that you want to add a source component to.  
  
2.  Drag the **Source Assistant** component from the SSIS Toolbox to the **Data Flow** tab. You should see the **Add New Source** dialog box. The next section provides you details about the options available in the dialog box.  
  
3.  Select the type of the destination in the **Types** list.  
  
4.  Select an existing connection manager in the **Connection Managers** list or select **\<New>** to create a new connection manager.  
  
5.  If you select an existing connection manager, click **OK** to close the **Add New Destination** dialog box. You should see the destination and connection managers added to the data flow.  
  
6.  If you click **\<New>** to create a new connection manager, you should see a **Connection Manager** dialog box, which lets you specify parameters for the connection. After you are done with creating the new connection manager, you will see the destination and the connection manager in SSIS Designer.  
  
  
