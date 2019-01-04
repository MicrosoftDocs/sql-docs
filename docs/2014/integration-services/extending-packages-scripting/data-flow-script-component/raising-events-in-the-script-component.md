---
title: "Raising Events in the Script Component | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: "reference"
helpviewer_keywords: 
  - "Script component [Integration Services], raising events"
ms.assetid: bb389073-e1d0-4794-8d29-c8b293b6a5e3
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# Raising Events in the Script Component
  Events provide a way to report errors, warnings, and other information, such as task progress or status, to the containing package. The package provides event handlers for managing event notifications. The Script component can raise events by calling methods on the <xref:Microsoft.SqlServer.Dts.Pipeline.ScriptComponent.ComponentMetaData%2A> property of the `ScriptMain` class. For more information about how [!INCLUDE[ssISnoversion](../../../includes/ssisnoversion-md.md)] packages handle events, see [Integration Services &#40;SSIS&#41; Event Handlers](../../integration-services-ssis-event-handlers.md).  
  
 Events can be logged to any log provider that is enabled in the package. Log providers store information about events in a data store. The Script component can also use the <xref:Microsoft.SqlServer.Dts.Pipeline.ScriptComponent.Log%2A> method to log information to a log provider without raising an event. For more information about how to use the <xref:Microsoft.SqlServer.Dts.Pipeline.ScriptComponent.Log%2A> method, see the following section.  
  
 To raise an event, the Script task calls one of the following methods of the <xref:Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSComponentMetaData100> interface exposed by the <xref:Microsoft.SqlServer.Dts.Pipeline.ScriptComponent.ComponentMetaData%2A> property:  
  
|Event|Description|  
|-----------|-----------------|  
|<xref:Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSComponentMetaData100.FireCustomEvent%2A>|Raises a user-defined custom event in the package.|  
|<xref:Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSComponentMetaData100.FireError%2A>|Informs the package of an error condition.|  
|<xref:Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSComponentMetaData100.FireInformation%2A>|Provides information to the user.|  
|<xref:Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSComponentMetaData100.FireProgress%2A>|Informs the package of the progress of the component.|  
|<xref:Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSComponentMetaData100.FireWarning%2A>|Informs the package that the component is in a state that warrants user notification, but is not an error condition.|  
  
 Here is a simple example of raising an Error event:  
  
 `Dim myMetadata as IDTSComponentMetaData100`  
  
 `myMetaData = Me.ComponentMetaData`  
  
 `myMetaData.FireError(...)`  
  
![Integration Services icon (small)](../../media/dts-16.gif "Integration Services icon (small)")  **Stay Up to Date with Integration Services**<br /> For the latest downloads, articles, samples, and videos from Microsoft, as well as selected solutions from the community, visit the [!INCLUDE[ssISnoversion](../../../includes/ssisnoversion-md.md)] page on MSDN:<br /><br /> [Visit the Integration Services page on MSDN](https://go.microsoft.com/fwlink/?LinkId=136655)<br /><br /> For automatic notification of these updates, subscribe to the RSS feeds available on the page.  
  
## See Also  
 [Integration Services &#40;SSIS&#41; Event Handlers](../../integration-services-ssis-event-handlers.md)   
 [Add an Event Handler to a Package](../../add-an-event-handler-to-a-package.md)  
  
  
