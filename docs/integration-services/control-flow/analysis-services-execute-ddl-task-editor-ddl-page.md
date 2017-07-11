---
title: "Analysis Services Execute DDL Task Editor (DDL Page) | Microsoft Docs"
ms.custom: ""
ms.date: "03/04/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "integration-services"
ms.tgt_pltfrm: ""
ms.topic: "article"
f1_keywords: 
  - "sql13.dts.designer.asexecuteddltask.ddl.f1"
helpviewer_keywords: 
  - "Analysis Services Execute DDL Task Editor"
ms.assetid: f21bf8d0-ec5f-4c18-9de0-8875addb927b
caps.latest.revision: 30
author: "douglaslMS"
ms.author: "douglasl"
manager: "jhubbard"
---
# Analysis Services Execute DDL Task Editor (DDL Page)
  Use the **DDL** page of the **Analysis Services Execute DDL Task Editor** dialog box to specify a connection to an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] project or an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] database and to provide information about the source of data definition language (DDL) statements.  
  
 To learn about this task, see [Analysis Services Execute DDL Task](../../integration-services/control-flow/analysis-services-execute-ddl-task.md).  
  
## Static Options  
 **Connection**  
 Select an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] project or an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] connection manager in the list, or click \<**New connection...**> and use the **Add Analysis Services Connection Manager** dialog box to create a new connection.  
  
 **Related Topics:** [Add Analysis Services Connection Manager Dialog Box UI Reference](../../integration-services/connection-manager/add-analysis-services-connection-manager-dialog-box-ui-reference.md), [Analysis Services Connection Manager](../../integration-services/connection-manager/analysis-services-connection-manager.md)  
  
 **SourceType**  
 Specify the source type of the DDL statements. This property has the options listed in the following table:  
  
|Value|Description|  
|-----------|-----------------|  
|**Direct Input**|Set the source to the DDL statement stored in the **SourceDirect** text box. Selecting this value displays the dynamic options in the following section.|  
|**File Connection**|Set the source to a file that contains the DDL statement. Selecting this value displays the dynamic options in the following section.|  
|**Variable**|Set the source to a variable. Selecting this value displays the dynamic options in the following section.|  
  
## Dynamic Options  
  
### SourceType = Direct Input  
 **Source**  
 Type the DDL statements or click the ellipsis **(…)** and then type the statements in the **DDL Statements** dialog box.  
  
### SourceType = File Connection  
 **Source**  
 Select a File connection in the list, or click \<**New connection...**> and use the **File Connection Manager** dialog box to create a new connection.  
  
 **Related Topics:** [File Connection Manager](../../integration-services/connection-manager/file-connection-manager.md)  
  
### SourceType = Variable  
 **Source**  
 Select a variable in the list, or click \<**New variable...**> and use the **Add Variable** dialog box to create a new variable.  
  
 **Related Topics:** [Integration Services &#40;SSIS&#41; Variables](../../integration-services/integration-services-ssis-variables.md)  
  
## See Also  
 [Integration Services Error and Message Reference](../../integration-services/integration-services-error-and-message-reference.md)   
 [Analysis Services Execute DDL Task Editor &#40;General Page&#41;](../../integration-services/control-flow/analysis-services-execute-ddl-task-editor-general-page.md)   
 [Expressions Page](../../integration-services/expressions/expressions-page.md)   
 [Control Flow](../../integration-services/control-flow/control-flow.md)   
 [Analysis Services Scripting Language &#40;ASSL for XMLA&#41;](../../analysis-services/scripting/analysis-services-scripting-language-assl-for-xmla.md)   
 [XML for Analysis  &#40;XMLA&#41; Reference](../../analysis-services/xmla/xml-for-analysis-xmla-reference.md)  
  
  