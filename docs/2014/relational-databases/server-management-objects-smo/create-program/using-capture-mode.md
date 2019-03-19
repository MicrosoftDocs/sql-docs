---
title: "Using Capture Mode | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
ms.topic: "reference"
helpviewer_keywords: 
  - "SQL Server Management Objects, capture mode"
  - "capture mode [SMO]"
  - "SMO [SQL Server], capture mode"
ms.assetid: ace29bf0-705a-434f-82e4-db99d01c5008
author: stevestein
ms.author: sstein
manager: craigg
---
# Using Capture Mode
  SMO programs can capture and record the equivalent [!INCLUDE[tsql](../../../includes/tsql-md.md)] statements issued by the program in place of, or in addition to, the statements that are executed by the program. You enable capture mode by using the <xref:Microsoft.SqlServer.Management.Common.ServerConnection> object, or by using the <xref:Microsoft.SqlServer.Management.Smo.Server.ConnectionContext%2A> property of the <xref:Microsoft.SqlServer.Management.Smo.Server> object.  
  
## Example  
 [!INCLUDE[ssChooseProgEnv](../../../includes/sschooseprogenv-md.md)]  
  
## Enabling Capture Mode in Visual Basic  
 This code example enables capture mode, and then displays the [!INCLUDE[tsql](../../../includes/tsql-md.md)] commands held in the capture buffer.  
  
<!-- TODO: review snippet reference  [!CODE [SMO How to#SMO_VBCapture1](SMO How to#SMO_VBCapture1)]  -->  
  
## Enabling Capture Mode in Visual C#  
 This code example enables capture mode, and then displays the [!INCLUDE[tsql](../../../includes/tsql-md.md)] commands held in the capture buffer.  
  
```  
{   
// Connect to the local, default instance of SQL Server.   
Server srv;   
srv = new Server();   
// Set the execution mode to CaptureSql for the connection.   
srv.ConnectionContext.SqlExecutionModes = SqlExecutionModes.CaptureSql;   
// Make a modification to the server that is to be captured.   
srv.UserOptions.AnsiNulls = true;   
srv.Alter();   
// Iterate through the strings in the capture buffer and display the captured statements.   
string s;   
foreach ( String p_s in srv.ConnectionContext.CapturedSql.Text ) {   
   Console.WriteLine(p_s);   
}   
// Execute the captured statements.   
srv.ConnectionContext.ExecuteNonQuery(srv.ConnectionContext.CapturedSql.Text);   
// Revert to immediate execution mode.   
srv.ConnectionContext.SqlExecutionModes = SqlExecutionModes.ExecuteSql;   
}  
```  
  
  
