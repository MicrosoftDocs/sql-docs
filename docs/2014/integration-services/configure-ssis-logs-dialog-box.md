---
title: "Configure SSIS Logs Dialog Box | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "integration-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.dts.designer.configuredtslogs.loggingdetails.f1"
  - "sql12.dts.designer.configuredtslogs.providersandlogs.f1"
  - "sql12.dts.designer.configuredtslogs.containers.f1"
helpviewer_keywords: 
  - "Configure SSIS Logs dialog box"
ms.assetid: 4b980275-cd9a-4943-8c36-727d51f9a484
author: janinezhang
ms.author: janinez
manager: craigg
---
# Configure SSIS Logs Dialog Box
  Use the **Configure SSIS Logs** dialog box to define logging options for a package.  
  
 **What do you want to do?**  
  
1.  [Open the Configure SSIS Logs Dialog Box](#open_dialog)  
  
2.  [Configure the Options in the Containers Pane](#container)  
  
3.  [Configure the Options on the Providers and Logs Tab](#provider)  
  
4.  [Configure the Options on the Details Tab](#detail)  
  
##  <a name="open_dialog"></a> Open the Configure SSIS Logs Dialog Box  
 **To open the Configure SSIS Logs dialog box**  
  
-   In the [!INCLUDE[ssIS](../includes/ssis-md.md)] Designer, click **Logging** on the **SSIS** menu.  
  
##  <a name="container"></a> Configure the Options in the Containers Pane  
 Use the **Containers** pane of the **Configure SSIS Logs** dialog box to enable the package and its containers for logging.  
  
### Options  
 **Containers**  
 Select the check boxes in the hierarchical view to enable the package and its containers for logging:  
  
-   If cleared, the container is not enabled for logging. Select to enable logging.  
  
-   If dimmed, the container uses the logging options of its parent. This option is not available for the package.  
  
-   If checked, the container defines its own logging options.  
  
 If a container is dimmed and you want to set logging options on the container, click its check box twice. The first click clears the check box, and the second click selects it, enabling you to choose the log providers to use and select the information to log.  
  
##  <a name="provider"></a> Configure the Options on the Providers and Logs Tab  
 Use the **Providers and Logs** tab of the **Configure SSIS Logs** dialog box to create and configure logs for capturing run-time events.  
  
### Options  
 **Provider type**  
 Select a type of log provider from the list.  
  
 **Add**  
 Add a log of the specified type to the collection of log providers of the package.  
  
 **Name**  
 Enable or disable logs for containers or tasks selected in the **Containers** pane of the **Configure SSIS Logs** dialog box, by using the check boxes. The name field is editable. Use the default name for the provider, or type a unique descriptive name.  
  
 **Description**  
 The description field is editable. Click and then modify the default description of the log.  
  
 **Configuration**  
 Select an existing connection manager in the list, or click \<**New connection...**> to create a new connection manager. Depending on the type of log provider, you can configure an OLE DB connection manager or a File connection manager. The log provider for the [!INCLUDE[msCoName](../includes/msconame-md.md)] Windows Event Log requires no connection.  
  
 Related Topics: [OLE DB Connection Manager](connection-manager/ole-db-connection-manager.md) manager, [File Connection Manager](connection-manager/file-connection-manager.md)  
  
 **Delete**  
 Select a log provider and then click **Delete**.  
  
##  <a name="detail"></a> Configure the Options on the Details Tab  
 Use the **Details** tab of the **Configure SSIS Logs** dialog box to specify the events to enable for logging and the information details to log. The information that you select applies to all the log providers in the package. For example, you cannot write some information to the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] instance and different information to a text file.  
  
### Options  
 **Events**  
 Enable or disable events for logging.  
  
 **Description**  
 View a description of the event.  
  
 **Advanced**  
 Select or clear events to log, and select or clear information to log for each event. Click **Basic** to hide all logging details, except the list of events. The following information is available for logging:  
  
|Value|Description|  
|-----------|-----------------|  
|**Computer**|The name of the computer on which the logged event occurred.|  
|**Operator**|The user name of the person who started the package.|  
|**SourceName**|The name of the package, container, or task in which the logged event occurred.|  
|**SourceID**|The global unique identifier (GUID) of the package, container, or task in which the logged event occurred.|  
|**ExecutionID**|The global unique identifier of the package execution instance.|  
|**MessageText**|A message associated with the log entry.|  
|**DataBytes**|Reserved for future use.|  
  
 **Basic**  
 Select or clear events to log. This option hides logging details except the list of events. If you select an event, all logging details are selected for the event by default. Click **Advanced** to show all logging details.  
  
 **Load**  
 Specify an existing XML file to use as a template for setting logging options.  
  
 **Save**  
 Save configuration details as a template to an XML file.  
  
  
