---
title: "Canceling Commands (XMLA) | Microsoft Docs"
ms.custom: ""
ms.date: "03/08/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
helpviewer_keywords: 
  - "connections [XML for Analysis]"
  - "associated connections [XML for Analysis]"
  - "XML for Analysis, canceling"
  - "associated sessions [XML for Analysis]"
  - "canceling connections"
  - "canceling commands"
  - "canceling sessions"
  - "SPID"
  - "XMLA, canceling"
  - "server process IDs [XML for Analysis]"
  - "sessions [XML for Analysis]"
ms.assetid: b59f8197-c33d-4e65-9022-848ccba540f5
author: minewiskan
ms.author: owend
manager: craigg
---
# Canceling Commands (XMLA)
  Depending on the administrative permissions of the user issuing the command, the [Cancel](https://docs.microsoft.com/bi-reference/xmla/xml-elements-commands/cancel-element-xmla) command in XML for Analysis (XMLA) can cancel a command on a session, a session, a connection, a server process, or an associated session or connection.  
  
## Canceling Commands  
 A user can cancel the currently executing command within the context of the current explicit session by sending a `Cancel` command with no specified properties.  
  
> [!NOTE]  
>  A command running in an implicit session cannot be canceled by a user.  
  
### Canceling Batch Commands  
 If a user cancels a `Batch` command, then all remaining commands not yet executed within the `Batch` command are canceled. If the `Batch` command was transactional, any commands that were executed before the `Cancel` command runs are rolled back.  
  
## Canceling Sessions  
 By specifying a session identifier for an explicit session in the [SessionID](https://docs.microsoft.com/bi-reference/xmla/xml-elements-properties/id-element-xmla) property of the `Cancel` command, a database administrator or server administrator can cancel a session, including the currently executing command. A database administrator can only cancel sessions for databases on which he or she has administrative permissions.  
  
 A database administrator can retrieve the active sessions for a specified database by retrieving the DISCOVER_SESSIONS schema rowset. To retrieve the DISCOVER_SESSIONS schema rowset, the database administrator uses the XMLA `Discover` method and specifies the appropriate database identifier for the SESSION_CURRENT_DATABASE restriction column in the [Restrictions](https://docs.microsoft.com/bi-reference/xmla/xml-elements-properties/restrictions-element-xmla) property of the `Discover` method.  
  
## Canceling Connections  
 By specifying a connection identifier in the [ConnectionID](https://docs.microsoft.com/bi-reference/xmla/xml-elements-properties/connectionid-element-xmla) property of the `Cancel` command, a server administrator can cancel all of the sessions associated with a given connection, including all running commands, and cancel the connection.  
  
> [!NOTE]  
>  If the instance of [!INCLUDE[msCoName](../../includes/msconame-md.md)][!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)][!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] cannot locate and cancel the sessions associated with a connection, such as when the data pump opens multiple sessions while providing HTTP connectivity, the instance cannot cancel the connection. If this case is encountered during the execution of a `Cancel` command, an error occurs.  
  
 A server administrator can retrieve the active connections for an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] instance by retrieving the DISCOVER_CONNECTIONS schema rowset using the XMLA `Discover` method.  
  
## Canceling Server Processes  
 By specifying a server process identifier (SPID) in the [SPID](https://docs.microsoft.com/bi-reference/xmla/xml-elements-properties/id-element-xmla) property of the `Cancel` command, a server administrator can cancel the commands associated with a given SPID.  
  
## Canceling Associated Sessions and Connections  
 You can set the [CancelAssociated](https://docs.microsoft.com/bi-reference/xmla/xml-elements-properties/cancelassociated-element-xmla) property to true to cancel the connections, sessions, and commands associated with the connection, session, or SPID specified in the `Cancel` command.  
  
## See Also  
 [Discover Method &#40;XMLA&#41;](https://docs.microsoft.com/bi-reference/xmla/xml-elements-methods-discover)   
 [Developing with XMLA in Analysis Services](developing-with-xmla-in-analysis-services.md)  
  
  
