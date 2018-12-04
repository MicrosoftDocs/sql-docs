---
title: "Create a Visual C# SMO Project in Visual Studio .NET | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
ms.topic: "reference"
helpviewer_keywords: 
  - "Visual C# [SMO]"
ms.assetid: 1e7abb16-23a0-4a18-91ad-253261e6bf84
author: stevestein
ms.author: sstein
manager: craigg
---
# Create a Visual C# SMO Project in Visual Studio .NET
  This section describes how to build a simple SMO console application.  
  
 This example imports namespaces, which enables the program to reference SMO types. The import of the `Agent` namespace is optional. Use it when you are writing a program that uses [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent. The `Common` namespace is required to establish a secure connection to the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. The `SqlClient` namespace is used to process SQL exception errors.  
  
### Creating a Visual C# SMO project in Visual Studio.NET  
  
1.  Start [!INCLUDE[vsOrcas](../../includes/vsorcas-md.md)] (or [!INCLUDE[vsprvslong](../../includes/vsprvslong-md.md)]).  
  
2.  On the **File** menu, click **NewProject.** The **New Project** dialog box appears.  
  
3.  In **Project Types** dialog box, select **Visual C#**, and then select **Windows**. In the [!INCLUDE[vsprvs](../../includes/vsprvs-md.md)] Installed Templates pane, select **Windows Application**.  
  
4.  (Optional) In the **Name** field, type the name of the new application  
  
5.  Select the Visual C# application type. For the examples that follow, select **Console Application**.  
  
6.  On the **Project** menu, select **Add Reference**. The **Add Reference** dialog box appears.  
  
7.  Click **Browse**, locate the SMO assemblies in the [!INCLUDE[ssSampPathSDK](../../includes/sssamppathsdk-md.md)] folder, and then select the following files. These are the minimum files that are required to build an SMO application:  
  
     Microsoft.SqlServer.ConnectionInfo.dll  
  
     Microsoft.SqlServer.Smo.dll  
  
     Microsoft.SqlServer.Management.Sdk.Sfc.dll  
  
     Microsoft.SqlServer.SqlEnum.dll  
  
    > [!NOTE]  
    >  Use the `Ctrl` key to select more than one file.  
  
8.  Add any additional SMO assemblies that are required. For example, if you are specifically programming [!INCLUDE[ssSB](../../includes/sssb-md.md)], add the following assemblies:  
  
     Microsoft.SqlServer.ServiceBrokerEmum.dll  
  
9. Click **Open**.  
  
10. On the **View** menu, click **Code**.-Or-Select the Program1.cs [Design] Windows and double-click the windows form to show the code window.  
  
11. In the code, before the namespace statement, type the following `using` statements to qualify the types in the SMO namespace:  
  
    ```  
    using Microsoft.SqlServer.Management.Smo;  
    using Microsoft.SqlServer.Management.Common;  
    ```  
  
12. SMO has various namespaces under Microsoft.SqlServer.Management.Smo, such as Microsoft.SqlServer.Management.Smo.Agent. Add these namespaces as they are required.  
  
13. You can now add your SMO code.  
  
  
