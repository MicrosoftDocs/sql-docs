---
title: "Disconnect Users and Sessions on Analysis Services Server | Microsoft Docs"
ms.date: 05/02/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom:
ms.topic: conceptual
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# Disconnect Users and Sessions on Analysis Services Server
[!INCLUDE[ssas-appliesto-sqlas](../../includes/ssas-appliesto-sqlas.md)]
  An administrator of [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] may want to end user activity as part of workload management. You do this by canceling sessions and connections. Sessions can be formed automatically when a query is run (implicit), or named at the time of creation by the administrator (explicit). Connections are open conduits over which queries can be run. Both sessions and connections can be ended while they are active. For example, an administrator may want to end processing for a session if the processing is taking too long or if some doubt has arisen as to whether the command being executed was written correctly.  
  
## Ending Sessions and Connections  
 To manage sessions and connections, you can use Dynamic Management Views (DMVs) and XMLA:  
  
1.  In [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], connect to an Analysis Services instance.  
  
2.  Paste any one of the following DMV queries in an MDX query window to get a list of all sessions, connections, and commands that are currently executing:  
  
     `Select * from $System.Discover_Sessions`  
  
     `Select * from $System.Discover_Connections`  
  
     `Select * from $System.Discover_Commands`  
  
3.  Press F5 to execute the query.  
  
     The DMV query returns session and connection information in a tabular result set that is easier read and copy from.  
  
 Keep the query window open. In the next step, you will want to return to this page to copy the SPIDs of the session you want to disconnect.  
  
 To end a session, open a second XMLA query window.  
  
1.  Paste the following syntax into an MDX query window, replacing the ConnectionID, SessionID, or SPID placeholder with a valid value copied from the previous step.  
  
    ```  
    <Cancel xmlns="http://schemas.microsoft.com/analysisservices/2003/engine">  
  
       <ConnectionID>111</ConnectionID>  
       <SessionID>222</SessionID>  
       <SPID>333</SPID>  
  
    <CancelAssociated>1</CancelAssociated>  
    </Cancel>  
  
    ```  
  
2.  Press F5 to execute the cancel command.  
  
 Ending a connection cancels all sessions and SPIDs, closing the host session.  
  
 Ending a session stops all commands (SPIDs) that are running as part of that session.  
  
 Ending a SPID cancels a particular commend.  
  
 In rare cases, [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] will not close a connection if it cannot track all the sessions and SPIDs associated with the connection (for example, when multiple sessions are open in an HTTP scenario).  
  
 For more information about the XMLA referenced in this topic, see [Execute Method &#40;XMLA&#41;](https://docs.microsoft.com/bi-reference/xmla/xml-elements-methods-execute) and [Cancel Element &#40;XMLA&#41;](https://docs.microsoft.com/bi-reference/xmla/xml-elements-commands/cancel-element-xmla).  
  
## See Also  
 [Managing Connections and Sessions &#40;XMLA&#41;](../../analysis-services/multidimensional-models-scripting-language-assl-xmla/managing-connections-and-sessions-xmla.md)   
 [BeginSession Element &#40;XMLA&#41;](https://docs.microsoft.com/bi-reference/xmla/xml-elements-headers/beginsession-element-xmla)   
 [EndSession Element &#40;XMLA&#41;](https://docs.microsoft.com/bi-reference/xmla/xml-elements-headers/endsession-element-xmla)   
 [Session Element &#40;XMLA&#41;](https://docs.microsoft.com/bi-reference/xmla/xml-elements-headers/session-element-xmla)  
  
  
