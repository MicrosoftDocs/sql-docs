---
title: "Debugging Stored Procedures | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: "analysis-services"
ms.topic: "reference"
helpviewer_keywords: 
  - "debugging stored procedures [Analysis Services]"
  - "stored procedures [Analysis Services], debugging"
ms.assetid: 34f51b85-02b3-40dd-bf93-375a9e522385
author: minewiskan
ms.author: owend
manager: craigg
---
# Debugging Stored Procedures
  [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] stored procedures are actually CLR or COM libraries (normally DLLs) that are written in C# (or any other CLR or COM language). Therefore, debugging a stored procedure is much like debugging any other application in the Visual Studio debugging environment. You debug stored procedures in the Visual Studio development environment using the integrated debugging functions. These allow you to stop at procedure locations, inspect memory and register values, change variables, observe message traffic and get a close look at how your code works.  
  
### To debug a stored procedure  
  
1.  Open the project used to create the DLL in Visual Studio.  
  
2.  Create breakpoints in the method or function corresponding to the procedure you want to debug.  
  
3.  Use Visual Studio to create a debug build of a stored procedure DLL.  
  
4.  Deploy the DLL to the server. For more information about deploying the DLL to the server, see [Creating Stored Procedures](creating-stored-procedures.md).  
  
5.  You need an application that calls the stored procedure that you want to test. If you do not have one ready, you can use the MDX Query Editor in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] to create an MDX query that calls the stored procedure that you want to test.  
  
6.  In Visual Studio, attach to the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] process (Msmdsrv.exe).  
  
    1.  From the **Debug** menu, choose **Attatch toProcess**.  
  
    2.  In the **Attatch toProcess** dialog box, select **Show processes from all users**.  
  
    3.  In the **Available Processes** list, in the **Process** column, click **Msmdsrv.exe**. If there is more than one instance of [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] running on the server, you need to identify the process by the ID of the instance you want to use.  
  
    4.  In the **Attach to** text box, make sure that the appropriate program type is selected. For a CLR DLL, click **Select**, then click **Debug these code types**, then click **Managed**, then click **OK**. For a COM DLL, click **Select**, then click **Debug these code types**, then click **Native**, then click **OK**.  
  
    5.  Click **Attach**.  
  
7.  In [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)], invoke the program or MDX script that calls the stored procedure. The debugger breaks when it reaches a line containing a breakpoint. You can evaluate variables in the watch window, view locals, and step through the code.  
  
 If you have problems debugging a library, make sure that the corresponding program database (PDB) file was copied to the deployment location on the server. If this file was not copied during registration or deployment, you must copy it manually to the same location as the DLL. For native code (COM DLL), the PDB file resides in the \debug subdirectory. For managed code (CLR DLL), it resides in the \WINDEBUG subdirectory.  
  
## See Also  
 [Multidimensional Model Assemblies Management](../multidimensional-models/multidimensional-model-assemblies-management.md)   
 [Defining Stored Procedures](defining-stored-procedures.md)  
  
  
