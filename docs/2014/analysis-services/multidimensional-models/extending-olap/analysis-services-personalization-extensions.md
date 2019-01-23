---
title: "Analysis Services Personalization Extensions | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
helpviewer_keywords: 
  - "personalization extensions [Multidimensional Databases]"
ms.assetid: 0f144059-24e0-40c0-bde4-d48c75e46598
author: minewiskan
ms.author: owend
manager: craigg
---
# Analysis Services Personalization Extensions
  [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] personalization extensions are the foundation of the idea of implementing a plug-in architecture. In a plug-in architecture, you can develop new cube objects and functionality dynamically and share them easily with other developers. As such, [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] personalization extensions provide the functionality that makes it possible to achieve the following:  
  
-   **Dynamic design and deployment** Immediately after you design and deploy [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] personalization extensions, users have access to the objects and functionality at the start of the next user session.  
  
-   **Interface independence** Regardless of the interface that you use to create the [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] personalization extensions, users can use any interface to access the objects and functionality.  
  
-   **Session context** [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] personalization extensions are not permanent objects in the existing infrastructure and do not require the cube to be reprocessed. They become exposed and created for the user at the time that the user connects to the database, and remain available for the length of that user session.  
  
-   **Rapid distribution** Share [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] personalization extensions with other software developers without having to go into detailed specifications about where or how to find this extended functionality.  
  
 [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] personalization extensions have many uses. For example, your company has sales that involve different currencies. You create a calculated member that returns the consolidated sales in the local currency of the person who is accessing the cube. You create this member as a personalization extension. You then share this calculated member to a group of users. Once shared, those users have immediate access to the calculated member as soon as they connect to the server. They have access even if they are not using the same interface as the one that was used to create the calculated member.  
  
 [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] personalization extensions are a simple and elegant modification to the existing managed assembly architecture and are exposed throughout the [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] <xref:Microsoft.AnalysisServices.AdomdServer> object model, Multidimensional Expressions (MDX) syntax, and schema rowsets.  
  
## Logical Architecture  
 The architecture for [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] personalization extensions is based on the managed assembly architecture and the following four basic elements:  
  
 The [PlugInAttribute] custom attribute  
 When starting the service, [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] loads the required assemblies and determines which classes have the <xref:Microsoft.AnalysisServices.AdomdServer.PlugInAttribute> custom attribute.  
  
> [!NOTE]  
>  The [!INCLUDE[dnprdnshort](../../../includes/dnprdnshort-md.md)] defines custom attributes as a way to describe your code and affect run-time behavior. For more information, see the topic, "[Attributes Overview](https://go.microsoft.com/fwlink/?LinkId=82929)," in the [!INCLUDE[dnprdnshort](../../../includes/dnprdnshort-md.md)] Developer's Guide on MSDN.  
  
 For all classes with the <xref:Microsoft.AnalysisServices.AdomdServer.PlugInAttribute> custom attribute, [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] invokes their default constructors. Invoking all the constructors at startup provides a common location from which to build new objects and that is independent of any user activity.  
  
 In addition to building a small cache of information about authoring and managing personalization extensions, the class constructor typically subscribes to the <xref:Microsoft.AnalysisServices.AdomdServer.Server.SessionOpened> and <xref:Microsoft.AnalysisServices.AdomdServer.Server.SessionClosing> events. Failing to subscribe to these events may cause the class to be inappropriately marked for cleanup by the common language runtime (CLR) garbage collector.  
  
 Session context  
 For those objects that are based on personalization extensions, [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] creates an execution environment during the client session and dynamically builds most of those objects in this environment. Like any other CLR assembly, this execution environment also has access to other functions and stored procedures. When the user session ends, [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] removes the dynamically created objects and closes the execution environment.  
  
 Events  
 Object creation is triggered by the session events `On-Cube-OpenedCubeOpened` and `On-Cube-ClosingCubeClosing`.  
  
 Communication between the client and the server occurs through specific events. These events make the client aware of the situations that lead to the client's objects being built. The client's environment is dynamically created using two sets of events: session events and cube events.  
  
 Session events are associated with the server object. When a client logs on to a server, [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] creates a session and triggers the <xref:Microsoft.AnalysisServices.AdomdServer.Server.SessionOpened> event. When a client ends the session on the server, [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] triggers the <xref:Microsoft.AnalysisServices.AdomdServer.Server.SessionClosing> event.  
  
 Cube events are associated with the connection object. Connecting to a cube triggers the <xref:Microsoft.AnalysisServices.AdomdServer.AdomdConnection.CubeOpened> event. Closing the connection to a cube, by either closing the cube or by changing to a different cube, triggers a <xref:Microsoft.AnalysisServices.AdomdServer.AdomdConnection.CubeClosing> event.  
  
 Traceability and error handling  
 All activity is traceable by using [!INCLUDE[ssSqlProfiler](../../../includes/sssqlprofiler-md.md)]. Unhandled errors are reported to the Windows event log.  
  
 All object authoring and management is independent of this architecture and is the sole responsibility of the developers of the objects.  
  
## Infrastructure Foundations  
 [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] personalization extensions are based on existing components. The following is a summary of enhancements and improvements that provide the personalization extensions functionality.  
  
### Assemblies  
 The custom attribute, <xref:Microsoft.AnalysisServices.AdomdServer.PlugInAttribute>, can be added to your custom assemblies to identify [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] personalization extensions classes.  
  
### Changes to the AdomdServer Object Model  
 The following objects in the <xref:Microsoft.AnalysisServices.AdomdServer> object model have been enhanced or added to the model.  
  
#### New AdomdConnection Class  
 The <xref:Microsoft.AnalysisServices.AdomdServer.AdomdConnection> class is new and exposes several personalization extensions through both properties and events.  
  
 **Properties**  
  
-   <xref:Microsoft.AnalysisServices.AdomdServer.AdomdConnection.SessionID%2A>, a read-only string value representing the session Id of the current connection.  
  
-   <xref:Microsoft.AnalysisServices.AdomdServer.AdomdConnection.ClientCulture%2A>, a read-only reference to the client culture associated with current session.  
  
-   <xref:Microsoft.AnalysisServices.AdomdServer.AdomdConnection.User%2A>, a read-only reference to the identity interface representing the current user.  
  
 **Events**  
  
-   <xref:Microsoft.AnalysisServices.AdomdServer.AdomdConnection.CubeOpened>  
  
-   <xref:Microsoft.AnalysisServices.AdomdServer.AdomdConnection.CubeClosing>  
  
#### New Properties in the Context class  
 The <xref:Microsoft.AnalysisServices.AdomdServer.Context> class has two new properties:  
  
-   <xref:Microsoft.AnalysisServices.AdomdServer.Context.Server%2A>, a read-only reference to the new server object.  
  
-   <xref:Microsoft.AnalysisServices.AdomdServer.Context.CurrentConnection%2A>, a read-only reference to the new <xref:Microsoft.AnalysisServices.AdomdServer.AdomdConnection> object.  
  
#### New Server class  
 The <xref:Microsoft.AnalysisServices.AdomdServer.Server> class is new and exposes several personalization extensions through both class properties and events.  
  
 **Properties**  
  
-   <xref:Microsoft.AnalysisServices.AdomdServer.Server.Name%2A>, a read-only string value representing the server name.  
  
-   <xref:Microsoft.AnalysisServices.AdomdServer.Server.Culture%2A>, A read-only reference to the global culture associated with the server.  
  
 **Events**  
  
-   <xref:Microsoft.AnalysisServices.AdomdServer.Server.SessionOpened>  
  
-   <xref:Microsoft.AnalysisServices.AdomdServer.Server.SessionClosing>  
  
#### AdomdCommand class  
 The <xref:Microsoft.AnalysisServices.AdomdServer.AdomdCommand> class now supports of the following MDX commands:  
  
-   [CREATE MEMBER Statement &#40;MDX&#41;](/sql/mdx/mdx-data-definition-create-member)  
  
-   [UPDATE MEMBER Statement &#40;MDX&#41;](/sql/mdx/mdx-data-definition-update-member)  
  
-   [DROP MEMBER Statement &#40;MDX&#41;](/sql/mdx/mdx-data-definition-drop-member)  
  
-   [CREATE SET Statement &#40;MDX&#41;](/sql/mdx/mdx-data-definition-create-set)  
  
-   [DROP SET Statement &#40;MDX&#41;](/sql/mdx/mdx-data-definition-drop-set)  
  
-   [CREATE KPI Statement &#40;MDX&#41;](/sql/mdx/mdx-data-definition-create-kpi)  
  
-   [DROP KPI Statement &#40;MDX&#41;](/sql/mdx/mdx-data-definition-drop-kpi)  
  
### MDX extensions and enhancements  
 The CREATE MEMBER command is enhanced with the `caption` property, the `display_folder` property, and the `associated_measure_group` property.  
  
 The UPDATE MEMBER command is added to avoid member re-creation when an update is needed with the consequent loss of precedence in solving calculations. Updates cannot change the scope of the calculated member, move the calculated member to a different parent, or define a different `solveorder`.  
  
 The CREATE SET command is enhanced with the `caption` property, the `display_folder` property, and the new `STATIC | DYNAMIC` keyword. *Static* means that set is evaluated only at creation time. *Dynamic* means that the set is evaluated every time that the set is used in a query. The default value is `STATIC` if a keyword is omitted.  
  
 CREATE KPI and DROP KPI commands are added to the MDX syntax. KPIs can be created dynamically from any MDX script.  
  
### Schema Rowsets extensions  
 On MDSCHEMA_MEMBERS *scope* column is added. Scope values are as follows: MDMEMBER_SCOPE_GLOBAL=1, MDMEMBER_SCOPE_SESSION=2.  
  
 On MDSCHEMA_SETS *set_evaluation_context* column is added. Set evaluation context values are as follows: MDSET_RESOLUTION_STATIC = 1, MDSET_RESOLUTION_DYNAMIC = 2.  
  
 On MDSCHEMA_KPIS scope column is added. Scope values are as follows: MDKPI_SCOPE_GLOBAL=1, MDKPI_SCOPE_SESSION=2.  
  
  
