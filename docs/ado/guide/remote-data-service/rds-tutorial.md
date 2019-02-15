---
title: "RDS Tutorial | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: 11/09/2018
ms.reviewer: ""
ms.topic: conceptual
helpviewer_keywords: 
  - "RDS tutorial [ADO]"
ms.assetid: 6e3305a0-7bc7-40d1-9122-235c15d23ab2
author: MightyPen
ms.author: genemi
manager: craigg
---
# RDS Tutorial
This tutorial illustrates using the RDS programming model to query and update a data source. First, it describes the steps necessary to accomplish this task. Then the tutorial is repeated in Microsoft® Visual Basic Scripting Edition (featuring ADO for Windows Foundation Classes (ADO/WFC)).  
  
 This tutorial is coded in different languages for two reasons:  
  
-   The documentation for RDS assumes the reader codes in Visual Basic. This makes the documentation convenient for Visual Basic programmers, but less useful for programmers who use other languages.  
  
-   If you are uncertain about a particular RDS feature and you know a little of another language, you might be able to resolve your question by looking for the same feature expressed in another language.  
  
> [!IMPORTANT]
>  Beginning with Windows 8 and Windows Server 2012, RDS server components are no longer included in the Windows operating system (see Windows 8 and [Windows Server 2012 Compatibility Cookbook](https://www.microsoft.com/download/details.aspx?id=27416) for more detail). RDS client components will be removed in a future version of Windows. Avoid using this feature in new development work, and plan to modify applications that currently use this feature. Applications that use RDS should migrate to [WCF Data Service](https://go.microsoft.com/fwlink/?LinkId=199565).  
  
## How the Tutorial is Presented  
 This tutorial is based on the RDS programming model. It discusses each step of the programming model individually. In addition, it illustrates each step with a fragment of Visual Basic code.  
  
 The code example is repeated in other languages with minimal discussion. Each step in a given programming language tutorial is marked with the corresponding step in the programming model and descriptive tutorial. Use the number of the step to refer to the discussion in the descriptive tutorial.  
  
 The RDS programming model is stated in the following section. Use it as a roadmap as you proceed through the tutorial.  
  
## RDS Programming Model with Objects  
  
-   Specify the program to be invoked on the server, and obtain a way (proxy) to refer to it from the client.  
  
-   Invoke the server program. Pass parameters to the server program that identifies the data source and the command to issue.  
  
-   The server program obtains a [Recordset](../../../ado/reference/ado-api/recordset-object-ado.md) object from the data source, typically by using ADO. Optionally, the **Recordset** object is processed on the server.  
  
-   The server program returns the final **Recordset** object to the client application.  
  
-   On the client, the **Recordset** object is optionally put into a form that can be easily used by visual controls.  
  
-   Changes to the **Recordset** object are sent back to the server and used to update the data source.  
  
 This tutorial contains the following topics.  
  
-   [Step 1: Specify a Server Program (RDS Tutorial)](../../../ado/guide/remote-data-service/step-1-specify-a-server-program-rds-tutorial.md)  
  
-   [Step 2: Invoke the Server Program (RDS Tutorial)](../../../ado/guide/remote-data-service/step-2-invoke-the-server-program-rds-tutorial.md)  
  
-   [Step 3: Server Obtains a Recordset (RDS Tutorial)](../../../ado/guide/remote-data-service/step-3-server-obtains-a-recordset-rds-tutorial.md)  
  
-   [Step 4: Server Returns the Recordset (RDS Tutorial)](../../../ado/guide/remote-data-service/step-4-server-returns-the-recordset-rds-tutorial.md)  
  
-   [Step 5: DataControl is Made Usable (RDS Tutorial)](../../../ado/guide/remote-data-service/step-5-datacontrol-is-made-usable-rds-tutorial.md)  
  
-   [Step 6: Changes are Sent to the Server (RDS Tutorial)](../../../ado/guide/remote-data-service/step-6-changes-are-sent-to-the-server-rds-tutorial.md)  
  
-   [RDS Tutorial (VBScript)](../../../ado/guide/remote-data-service/rds-tutorial-vbscript.md)  
  
## See Also  
 [Step 1: Specify a Server Program (RDS Tutorial)](../../../ado/guide/remote-data-service/step-1-specify-a-server-program-rds-tutorial.md)   
 [RDS Tutorial (VBScript)](../../../ado/guide/remote-data-service/rds-tutorial-vbscript.md)   
