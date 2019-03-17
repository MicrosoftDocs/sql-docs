---
title: "Scripting | Microsoft Docs"
ms.custom: ""
ms.date: "08/06/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: 

ms.topic: "reference"
helpviewer_keywords: 
  - "dependencies [SMO]"
  - "scripts [SMO]"
ms.assetid: 13a35511-3987-426b-a3b7-3b2e83900dc7
author: "stevestein"
ms.author: "sstein"
manager: craigg
monikerRange: "=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Scripting
[!INCLUDE[appliesto-ss-asdb-asdw-xxx-md](../../../includes/appliesto-ss-asdb-asdw-xxx-md.md)]

  Scripting in SMO is controlled by the <xref:Microsoft.SqlServer.Management.Smo.Scripter> object and its child objects, or the **Script** method on individual objects. The <xref:Microsoft.SqlServer.Management.Smo.Scripter> object controls the mapping out of dependency relationships for objects on an instance of [!INCLUDE[msCoName](../../../includes/msconame-md.md)][!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)].  
  
 Advanced scripting by using the <xref:Microsoft.SqlServer.Management.Smo.Scripter> object and its child objects is a three phase process:  
  
1.  Discovery  
  
2.  List generation  
  
3.  Script generation  
  
 The discovery phase uses the <xref:Microsoft.SqlServer.Management.Smo.DependencyWalker> object. Given an URN list of objects, the <xref:Microsoft.SqlServer.Management.Smo.DependencyWalker.DiscoverDependencies%2A> method of the <xref:Microsoft.SqlServer.Management.Smo.DependencyWalker> object returns a <xref:Microsoft.SqlServer.Management.Smo.DependencyTree> object for the objects in the URN list. The Boolean *fParents* parameter is used to select whether the parents or the children of the specified object are to be discovered. The dependency tree can be modified at this stage.  
  
 In the list generation phase, the tree is passed in and the resulting list is returned. This object list is in scripting order and can be manipulated.  
  
 The list generation phases use the <xref:Microsoft.SqlServer.Management.Smo.DependencyWalker.WalkDependencies%2A> method to return a <xref:Microsoft.SqlServer.Management.Smo.DependencyTree>. The <xref:Microsoft.SqlServer.Management.Smo.DependencyTree> can be modified at this stage.  
  
 In the third and final phase, a script is generated with the specified list and scripting options. The result is returned as a <xref:System.Collections.Specialized.StringCollection> system object. In this phase the dependent object names are then extracted from the Items collection of the <xref:Microsoft.SqlServer.Management.Smo.DependencyTree> object and properties such as <xref:Microsoft.SqlServer.Management.Smo.DependencyTree.NumberOfSiblings%2A> and <xref:Microsoft.SqlServer.Management.Smo.DependencyTree.FirstChild%2A>.  
  
## Example  
 To use any code example that is provided, you will have to choose the programming environment, the programming template, and the programming language in which to create your application. For more information, see [Create a Visual C&#35; SMO Project in Visual Studio .NET](../../../relational-databases/server-management-objects-smo/how-to-create-a-visual-csharp-smo-project-in-visual-studio-net.md).  
  
 This code example requires an **Imports** statement for the System.Collections.Specialized namespace. Insert this with the other Imports statements, before any declarations in the application.  
  
```  
Imports Microsoft.SqlServer.Management.Smo  
Imports Microsoft.SqlServer.Management.Common  
Imports System.Collections.Specialized  
```  
  
## Scripting Out the Dependencies for a Database in Visual Basic  
 This code example shows how to discover the dependencies and iterate through the list to display the results.  
  
```  
' compile with:   
' /r:Microsoft.SqlServer.Smo.dll   
' /r:Microsoft.SqlServer.ConnectionInfo.dll   
' /r:Microsoft.SqlServer.Management.Sdk.Sfc.dll   
  
Imports Microsoft.SqlServer.Management.Smo  
Imports Microsoft.SqlServer.Management.Sdk.Sfc  
  
Public Class A  
   Public Shared Sub Main()  
      ' database name  
      Dim dbName As [String] = "AdventureWorksLT2012"   ' database name  
  
      ' Connect to the local, default instance of SQL Server.   
      Dim srv As New Server()  
  
      ' Reference the database.    
      Dim db As Database = srv.Databases(dbName)  
  
      ' Define a Scripter object and set the required scripting options.   
      Dim scrp As New Scripter(srv)  
      scrp.Options.ScriptDrops = False  
      scrp.Options.WithDependencies = True  
      scrp.Options.Indexes = True   ' To include indexes  
      scrp.Options.DriAllConstraints = True   ' to include referential constraints in the script  
  
      ' Iterate through the tables in database and script each one. Display the script.  
      For Each tb As Table In db.Tables  
         ' check if the table is not a system table  
         If tb.IsSystemObject = False Then  
            Console.WriteLine("-- Scripting for table " + tb.Name)  
  
            ' Generating script for table tb  
            Dim sc As System.Collections.Specialized.StringCollection = scrp.Script(New Urn() {tb.Urn})  
            For Each st As String In sc  
               Console.WriteLine(st)  
            Next  
            Console.WriteLine("--")  
         End If  
      Next  
   End Sub  
End Class  
```  
  
## Scripting Out the Dependencies for a Database in Visual C#  
 This code example shows how to discover the dependencies and iterate through the list to display the results.  
  
```  
// compile with:   
// /r:Microsoft.SqlServer.Smo.dll   
// /r:Microsoft.SqlServer.ConnectionInfo.dll   
// /r:Microsoft.SqlServer.Management.Sdk.Sfc.dll   
  
using System;  
using Microsoft.SqlServer.Management.Smo;  
using Microsoft.SqlServer.Management.Sdk.Sfc;  
  
public class A {  
   public static void Main() {   
      String dbName = "AdventureWorksLT2012"; // database name  
  
      // Connect to the local, default instance of SQL Server.   
      Server srv = new Server();  
  
      // Reference the database.    
      Database db = srv.Databases[dbName];  
  
      // Define a Scripter object and set the required scripting options.   
      Scripter scrp = new Scripter(srv);  
      scrp.Options.ScriptDrops = false;  
      scrp.Options.WithDependencies = true;  
      scrp.Options.Indexes = true;   // To include indexes  
      scrp.Options.DriAllConstraints = true;   // to include referential constraints in the script  
  
      // Iterate through the tables in database and script each one. Display the script.     
      foreach (Table tb in db.Tables) {   
         // check if the table is not a system table  
         if (tb.IsSystemObject == false) {  
            Console.WriteLine("-- Scripting for table " + tb.Name);  
  
            // Generating script for table tb  
            System.Collections.Specialized.StringCollection sc = scrp.Script(new Urn[]{tb.Urn});  
            foreach (string st in sc) {  
               Console.WriteLine(st);  
            }  
            Console.WriteLine("--");  
         }  
      }   
   }  
}  
```  
  
## Scripting Out the Dependencies for a Database in PowerShell  
 This code example shows how to discover the dependencies and iterate through the list to display the results.  
  
```  
# Set the path context to the local, default instance of SQL Server.  
CD \sql\localhost\default  
  
# Create a Scripter object and set the required scripting options.  
$scrp = New-Object -TypeName Microsoft.SqlServer.Management.SMO.Scripter -ArgumentList (Get-Item .)  
$scrp.Options.ScriptDrops = $false  
$scrp.Options.WithDependencies = $true  
$scrp.Options.IncludeIfNotExists = $true  
  
# Set the path context to the tables in AdventureWorks2012.  
  
CD Databases\AdventureWorks2012\Tables  
  
foreach ($Item in Get-ChildItem)  
 {    
 $scrp.Script($Item)  
 }  
```  
  
  
