---
title: "Create a Visual Basic SMO Project in Visual Studio .NET | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
  - "docset-sql-devref"
ms.topic: "reference"
dev_langs: 
  - "VB"
helpviewer_keywords: 
  - "Visual Basic [SMO]"
ms.assetid: d7a3892c-0f1c-4c4d-8480-b58dce3720bc
author: mashamsft
ms.author: mathoma
manager: craigg
---
# Create a Visual Basic SMO Project in Visual Studio .NET
  This section describes how to build a simple SMO console application.  
  
 This example imports namespaces, which enables the program to reference SMO types. The import of the `Agent` namespace is optional. Use it when you are writing a program that uses [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent. The `Common` namespace is required to establish a secure connection to the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. The `SqlClient` namespace is used to process SQL exception errors.  
  
### Creating a Visual Basic SMO project in Visual Studio.NET  
  
1.  Start [!INCLUDE[vsOrcas](../../includes/vsorcas-md.md)] (or [!INCLUDE[vsprvslong](../../includes/vsprvslong-md.md)]).  
  
2.  On the **File** menu, click **NewProject.** The **New Project** dialog box appears.  
  
3.  In **Project Types** dialog box, select **Visual Basic**, and then select **Windows**. In the [!INCLUDE[vsprvs](../../includes/vsprvs-md.md)] Installed Templates pane, select **Console Application.**  
  
4.  (Optional) In the **Name** field, type the name of the new application.  
  
5.  Click **OK** to load the [!INCLUDE[vbprvb](../../includes/vbprvb-md.md)] console application template.  
  
6.  On the **Project** menu, select **Add Reference**. The **Add Reference** dialog box appears.  
  
7.  Click **Browse**, locate the SMO assemblies in the C:\Program Files\Microsoft SQL Server\120\SDK\Assemblies folder, and then select the following files. These are the minimum files that are required to build an SMO application:  
  
     Microsoft.SqlServer.ConnectionInfo.dll  
  
     Microsoft.SqlServer.SqlEnum.dll  
  
     Microsoft.SqlServer.Smo.dll  
  
     Microsoft.SqlServer.Management.Sdk.Sfc  
  
    > [!NOTE]  
    >  Use the `Ctrl` key to select more than one file.  
  
8.  Add any additional SMO assemblies that are required. For example, if you are specifically programming [!INCLUDE[ssSB](../../includes/sssb-md.md)], add the following assemblies:  
  
     Microsoft.SqlServer.ServiceBrokerEmum.dll  
  
9. Click **Open**.  
  
10. On the **View** menu, click **Code**.-Or-Select the Module1.vb window to show the code window.  
  
11. In the code, before any declarations, type the following **Imports** statements to qualify the types in the SMO namespace.  
  
    ```  
    Imports Microsoft.SqlServer.Management.Smo  
    Imports Microsoft.SqlServer.Management.Common  
    ```  
  
12. SMO has various namespaces under Microsoft.SqlServer.Management.Smo, such as Microsoft.SqlServer.Management.Smo.Agent. Add these namespaces as they are required.  
  
13. You can now add your SMO code.  
  
  
