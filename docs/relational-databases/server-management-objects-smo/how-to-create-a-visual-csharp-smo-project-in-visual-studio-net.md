---
title: "Create a Visual C# SMO Project in Visual Studio .NET | Microsoft Docs"
ms.custom: ""
ms.date: "07/27/2016"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
helpviewer_keywords: 
  - "Visual C# [SMO]"
ms.assetid: 1e7abb16-23a0-4a18-91ad-253261e6bf84
caps.latest.revision: 43
author: "JennieHubbard"
ms.author: "jhubbard"
manager: "jhubbard"
---
# How to Create a Visual C# SMO Project in Visual Studio .NET
  This section describes how to build a simple SMO console application.  
  
 This example imports namespaces, which enables the program to reference SMO types. The import of the **Agent** namespace is optional. Use it when you are writing a program that uses [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent. The **Common** namespace is required to establish a secure connection to the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. The **SqlClient** namespace is used to process SQL exception errors.  
  
### Creating a Visual C# SMO project in Visual Studio.NET  
  
1.  Start Visual Studio 2015. 
  
2.  On the **File** menu, click **New** and then **Project**.  The **New Project** dialog box appears.   
  
3.  In the [!INCLUDE[vsprvs](../../includes/vsprvs-md.md)] **Installed** pane, navigate to **Templates**\\**Visual C#**\\**Windows** and select **Console Application**.  
  
4.  (Optional) In the **Name** text box, type the name of the new application.  

5.  Click **OK** to load the console application template.  

6.  On the **Project** menu, select **Add Reference**. The **Reference Manager** dialog box appears.  
   
7.  Click **Browse** to locate the SMO assemblies.  Navigate to C:\Program Files\Microsoft SQL Server\130\SDK\Assemblies and then select the following files. These are three of four minimum files that are required to build an SMO application:  

    - Microsoft.SqlServer.Management.Sdk.Sfc
     
    - Microsoft.SqlServer.Smo.dll
     
    - Microsoft.SqlServer.SqlEnum.dll
  
    > [!NOTE]  
    >  Use the **Ctrl** key to select more than one file.  
    
8.  Click **Add**.
  
9.  Click **Browse** to locate an additional SMO assembly.  Navigate to C:\Program Files (x86)\Microsoft SQL Server\130\SDK\Assemblies and then select the file referenced below.  This is the fourth minimum file required to build an SMO application.

    - Microsoft.SqlServer.ConnectionInfo.dll

10.  Click **Add**. 
  
11.  Add any additional SMO assemblies that are required. For example, if you are specifically programming [!INCLUDE[ssSB](../../includes/sssb-md.md)], add the following assemblies:  
  
      - Microsoft.SqlServer.ServiceBrokerEmum.dll  
     
12. Click **OK**.
  
13. On the **View** menu, click **Code**.
    
14. In the code, before the namespace statement, type the following **using** statements to qualify the types in the SMO namespace:  
  
    ```  
    using Microsoft.SqlServer.Management.Smo;  
    using Microsoft.SqlServer.Management.Common;  
    ```  
  
15. SMO has various namespaces under Microsoft.SqlServer.Management.Smo, such as Microsoft.SqlServer.Management.Smo.Agent. Add these namespaces as they are required.  
  
16. You can now add your SMO code.  
  
  
